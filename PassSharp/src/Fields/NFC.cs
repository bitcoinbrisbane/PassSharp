using System;
using System.Collections.Generic;

namespace PassSharp.Fields
{
	public class NFC
	{
		public List<Beacon> beacons { get; set; }
		public List<Location> locations { get; set; }
		public int maxDistance { get; set; }
		public DateTime? relevantDate { get; set; }
	}
}
