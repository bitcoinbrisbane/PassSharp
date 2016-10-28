using System;

namespace PassSharp.Fields
{
	public class Beacon
	{
		public ushort? major { get; set; }
		public ushort? minor { get; set; }
		public string proximityUUID { get; set; }
		public string relevantText { get; set; }
	}
}
