using System;
using System.Collections.Generic;

namespace PassSharp.Fields
{
	public class Field
	{
		public string attributedValue { get; set; }
		public string changeMessage { get; set; }
		public List<FieldDataType> dataDetectorTypes { get; set; }
		public string key { get; set; }
		public string label { get; set; }
		public FieldAlignment textAlignment { get; set; }
		public string value { get; set; }
	}
}
