using System;
using System.IO;

namespace PassSharp
{
	public class Asset
	{
		public byte[] asset { get; private set; }

		public Asset(string path)
		{
			asset = File.ReadAllBytes(path);
		}
	}
}
