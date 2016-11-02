using System;
using System.Collections.Generic;
using System.Globalization;

namespace PassSharp
{
	public class Localization
	{

		public Localization(CultureInfo cultureInfo)
		{
			this.cultureInfo = cultureInfo;
		}

		public Localization(CultureInfo cultureInfo, Dictionary<string, string> values)
		{
			this.cultureInfo = cultureInfo;
			this.values = values;
		}

		public void Add(string key, string value)
		{
			if (values == null) {
				values = new Dictionary<string, string>();
			}

			values.Add(key, value);
		}

		protected CultureInfo cultureInfo { get; set; }

		public string culture { get { return cultureInfo.TwoLetterISOLanguageName; } }
		public Dictionary<string, string> values { private set; get; }
		public Asset icon { get; set; }
		public Asset icon2x { get; set; }
		public Asset logo { get; set; }
		public Asset logo2x { get; set; }
		public Asset background { get; set; }
		public Asset background2x { get; set; }
		public Asset footer { get; set; }
		public Asset footer2x { get; set; }
		public Asset strip { get; set; }
		public Asset strip2x { get; set; }
		public Asset thumbnail { get; set; }
		public Asset thumbnail2x { get; set; }

	}
}
