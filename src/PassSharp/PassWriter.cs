using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509.Store;
using PassSharp.Fields;
using ServiceStack;
using ServiceStack.Text;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace PassSharp
{

	public class PassWriter
	{
		static ZipArchive archive;

		public static void WriteToStream(Pass pass, Stream stream, X509Certificate2 appleCert, X509Certificate2 passCert)
		{
			using (archive = new ZipArchive(stream, ZipArchiveMode.Update, true)) {
				AddEntry(@"pass.json", ToJson(pass));

				AddAssetEntry(@"icon.png", pass.icon);
				AddAssetEntry(@"icon@2x.png", pass.icon2x);
				AddAssetEntry(@"logo.png", pass.logo);
				AddAssetEntry(@"logo@2x.png", pass.logo2x);
				AddAssetEntry(@"background.png", pass.background);
				AddAssetEntry(@"background@2x.png", pass.background2x);
				AddAssetEntry(@"footer.png", pass.footer);
				AddAssetEntry(@"footer@2x.png", pass.footer2x);
				AddAssetEntry(@"strip.png", pass.strip);
				AddAssetEntry(@"strip@2x.png", pass.strip2x);
				AddAssetEntry(@"thumbnail.png", pass.thumbnail);
				AddAssetEntry(@"thumbnail@2x.png", pass.thumbnail2x);

				var manifestJson = GenerateManifest().ToJson();
				AddEntry(@"manifest.json", manifestJson);
				AddEntry(@"signature", GenerateSignature(manifestJson.ToUtf8Bytes(), appleCert, passCert));
			}
		}

		public static void WriteToFile(Pass pass, string path, X509Certificate2 appleCert, X509Certificate2 passCert)
		{
			using (var stream = new FileStream(path, FileMode.OpenOrCreate)) {
				WriteToStream(pass, stream, appleCert, passCert);
			}
		}

		protected static Dictionary<string, string> GenerateManifest()
		{
			var hashManifest = new Dictionary<string, string>();

			foreach (var entry in archive.Entries) {
				hashManifest.Add(entry.Name, CalculateSHA1(entry.Open()));
			}

			return hashManifest;
		}

		protected static byte[] GenerateSignature(byte[] manifest, X509Certificate2 appleCert, X509Certificate2 passCert)
		{
			X509Certificate apple = DotNetUtilities.FromX509Certificate(appleCert);
			X509Certificate cert = DotNetUtilities.FromX509Certificate(passCert);

			var privateKey = DotNetUtilities.GetKeyPair(passCert.PrivateKey).Private;
			var generator = new CmsSignedDataGenerator();

			generator.AddSigner(privateKey, cert, CmsSignedGenerator.DigestSha1);

			var list = new List<X509Certificate>();
			list.Add(cert);
			list.Add(apple);

			X509CollectionStoreParameters storeParameters = new X509CollectionStoreParameters(list);
			IX509Store store509 = X509StoreFactory.Create("CERTIFICATE/COLLECTION", storeParameters);

			generator.AddCertificates(store509);

			var content = new CmsProcessableByteArray(manifest);
			var signature = generator.Generate(content, false).GetEncoded();

			return signature;
		}

		protected static void AddEntry(string name, string value)
		{
			AddEntry(name, value.ToUtf8Bytes());
		}

		protected static void AddEntry(string name, byte[] value)
		{
			using (var entry = archive.CreateEntry(name).Open()) {
				entry.Write(value, 0, value.Length);
			}
		}

		protected static void AddFileEntry(string name, string filename)
		{
			AddEntry(name, File.ReadAllBytes(filename));
		}

		protected static void AddAssetEntry(string name, Asset asset)
		{
			if (null != asset) {
				AddEntry(name, asset.asset);
			}
		}

		protected static string CalculateSHA1(Stream stream)
		{
			using (SHA1Managed managed = new SHA1Managed()) {
				byte[] checksum = managed.ComputeHash(stream);
				return BitConverter.ToString(checksum).Replace("-", string.Empty).ToLower();
			}
		}

		protected static string ToJson(Pass pass)
		{
			var properties = pass.GetType().GetProperties();
			var jsonDict = new Dictionary<object, object>();

			foreach (var property in properties) {
				object name = property.Name;
				object value = property.GetValue(pass);

				if (name.Equals("fields")) {
					jsonDict.Add(pass.type, value);
				} else if (name.Equals("type") || (value != null && value.GetType() == typeof(Asset))) {
					// do nothing
				} else {
					jsonDict.Add(name, value);
				}

			}

			string json = null;
			using (JsConfig.CreateScope("ExcludeTypeInfo")) {
				JsConfig<FieldType>.SerializeFn = SerializeFieldType;
				json = jsonDict.ToJson();
			}

			return json;
		}

		protected static Func<FieldType, string> SerializeFieldType = (value) => {
			switch (value) {
				case FieldType.Auxiliary:
					return "auxiliaryFields";
				case FieldType.Back:
					return "backFields";
				case FieldType.Header:
					return "headerFields";
				case FieldType.Primary:
					return "primaryFields";
				case FieldType.Secondary:
					return "secondaryFields";
				default:
					return "";
			}
		};
	}
}
