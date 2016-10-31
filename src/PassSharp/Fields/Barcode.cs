using System;

namespace PassSharp.Fields
{
	public class Barcode
	{
		public BarcodeFormat format { get; set; }
		public string message { get; set; }
		public string messageEncoding { get; set; }
		public string altText { get; set; }
	}
}
