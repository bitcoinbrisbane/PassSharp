using Machine.Specifications;
using PassSharp;
using PassSharp.Fields;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Reflection;
using ServiceStack.Text;
using System.IO.Compression;
using System.Security.Cryptography.Pkcs;

#pragma warning disable 414

namespace Test
{

  public class PassWriterMock : PassWriter
  {

    public static new Dictionary<string, string> GenerateManifest()
    {
      return PassWriter.GenerateManifest();
    }

    public static new byte[] GenerateSignature(byte[] bytes, X509Certificate2 appleCert, X509Certificate2 passCert)
    {
      return PassWriter.GenerateSignature(bytes, appleCert, passCert);
    }

    public static new string ToJson(Pass pass)
    {
      return PassWriter.ToJson(pass);
    }

  }

  [Subject("Pass Generation")]
  public class when_generating_a_pass
  {
    static Pass pass;
    static string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    static X509Certificate2 appleCert = new X509Certificate2(Path.Combine(path, @"fixtures/certificates/apple.cer"));
    static X509Certificate2 passCert = new X509Certificate2(Path.Combine(path, @"fixtures/certificates/test.p12"));
    static string imagePath = Path.Combine(path, @"fixtures/asset.png");

    Establish context = () => {
      pass = new Pass {
        type = PassType.generic,
        passTypeIdentifier = "pass.com.test",
        description = "a sample pass description",
        organizationName = "acme corp",
        serialNumber = "abc123456",
        teamIdentifier = "U1234567",
        icon = new Asset(imagePath)
      };
      pass.AddLocation(new Location { });
      pass.AddField(FieldType.Primary, new Field { key = "pass-field", label = "Pass Field Label", value = "foobar" });
    };

    public class when_generating_pass_json
    {
      static JsonObject json;

      Because of = () => json = JsonObject.Parse(PassWriterMock.ToJson(pass));

      It should_default_format_version = () => json.Get<int>("formatVersion").ShouldEqual(1);
      It should_not_contain_fields_key = () => json.Get("fields").ShouldBeNull();
      It should_not_contain_assets = () => json.Get("icon").ShouldBeNull();
      It should_not_contain_empty_lists = () => json.Get("beacons").ShouldBeNull();
      It should_set_pass_type = () => json.Get("generic").ShouldNotBeNull();
      It should_contain_pass_fields = () => json.Get<List<Field>>("generic").Count.ShouldEqual(1);
    }

    //public class when_generating_a_manifest
    //{
    //  static Dictionary<string, string> manifest;

    //  Because of = () => {
    //    using (var stream = new MemoryStream()) {
    //      PassWriterMock.WriteToStream(pass, stream, appleCert, passCert);
    //      manifest = PassWriterMock.GenerateManifest();
    //    }
    //  };

    //  It should_have_manifest_entries = () => manifest.Keys.Count.ShouldEqual(2);
    //  It should_not_contain_the_manifest = () => manifest.ContainsKey("manifest.json").ShouldBeFalse();
    //  It should_have_correct_hash_for_manifest_entry = () => manifest["pass.json"].ShouldEqual("1715e0ac7a30b2173ad5b616f483015f5911c418");
    //}

    public class when_generating_a_signature
    {
      static ZipArchive zip;

      Because of = () => {
        using (var stream = new MemoryStream()) {
          PassWriter.WriteToStream(pass, stream, appleCert, passCert);
          zip = new ZipArchive(stream);
        }
      };

      It should_have_a_signature_entry = () => zip.GetEntry("signature").ShouldNotBeNull();
    }

  }

}
