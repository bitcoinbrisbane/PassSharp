# PassSharp

[![Travis CIBuild Status](https://travis-ci.org/daxko/PassSharp.svg?branch=master)](https://travis-ci.org/daxko/PassSharp) [![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/7xm3msq279h5pgah/branch/master?svg=true)](https://ci.appveyor.com/project/daxko/PassSharp) [![NuGet](https://img.shields.io/nuget/v/PassSharp.svg)](https://www.nuget.org/packages/PassSharp/)

An Apple Wallet Passbook Library in C#/.NET

## Certificates

In order to create passes you will need to download the [Apple WWDR intermediate certificate](http://developer.apple.com/certificationauthority/AppleWWDRCA.cer), and to [generate your own pass id certificate](https://developer.apple.com/library/content/documentation/IDEs/Conceptual/AppDistributionGuide/MaintainingCertificates/MaintainingCertificates.html#//apple_ref/doc/uid/TP40012582-CH31-SW32) from the [Apple developers portal](http://developer.apple.com/account).

You can then write passes by including the Apple WWDR certificate, and the pass id certificate that you previously generated.

```
var myPass = new Pass { ... };
using(var myStream = new MemoryStream()) {
  PassWriter.Write(myPass, myStream,
    new X509Certificate2("../path/to/appleWWDRCertificate"),
    new X509Certificate2("../path/to/passCertificate"));
}
```