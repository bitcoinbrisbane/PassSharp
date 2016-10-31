using System;
using System.Collections.Generic;
using PassSharp.Fields;

namespace PassSharp
{
	/// <summary>
	///   This class implement methods for creating Apple Wallet Passes as outlined in the PassKit Package documentation
	///   https://developer.apple.com/library/content/documentation/UserExperience/Reference/PassKit_Bundle/Chapters/Introduction.html
	/// </summary>
	public class Pass
	{

		public Pass()
		{
			barcodes = new List<Barcode>();
			nfc = new List<NFC>();
			beacons = new List<Beacon>();
			locations = new List<Location>();
			fields = new Dictionary<FieldType, List<Field>>();
		}

		// Required fields
		public int formatVersion { get { return 1; } }
		public string description { get; set; }
		public string organizationName { get; set; }
		public string passTypeIdentifier { get; set; }
		public string serialNumber { get; set; }
		public string teamIdentifier { get; set; }

		// Associated App Keys
		public string appLaunchURL { get; set; }
		public List<long> associatedStoreIdentifiers { get; set; }

		// Companion App Keys
		public Dictionary<string, object> userInfo { get; set; }

		// Expiration Keys
		public DateTime? expirationDate { get; set; }
		public bool? voided { get; set; }

		// Relevance Keys
		public List<Beacon> beacons { get; set; }
		public List<Location> locations { get; set; }
		public int maxDistance { get; set; }
		public DateTime? relevantDate { get; set; }

		// Style Keys
		public PassType type { get; set; }

		// Visual Appearance Keys
		public Barcode barcode { get; set; }
		public List<Barcode> barcodes { get; set; }
		public string backgroundColor { get; set; }
		public string foregroundColor { get; set; }
		public string groupingIdentifier { get; set; }
		public string labelColor { get; set; }
		public string logoText { get; set; }
		public bool? suppressStripShine { get; set; }

		// Web Service Keys
		public string authenticationToken { get; set; }
		public string webServiceURL { get; set; }

		// NFC-Enabled Pass Keys
		public List<NFC> nfc { get; set; }

		public TransitType? transitType { get; set; }
		public Dictionary<FieldType, List<Field>> fields { get; protected set; }

		// Assets
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

		public void AddField(FieldType type, Field field)
		{
			List<Field> fieldsForType;

			if (fields == null)
			{
				fields = new Dictionary<FieldType, List<Field>>();
			}

			if (fields.ContainsKey(type))
			{
				fieldsForType = fields[type];
			}
			else {
				fieldsForType = new List<Field>();
			}

			fieldsForType.Add(field);

			fields[type] = fieldsForType;
		}

		public void AddBeacon(Beacon beacon)
		{
			if (beacons == null)
			{
				beacons = new List<Beacon>();
			}

			beacons.Add(beacon);
		}

		public void AddLocation(Location location)
		{
			if (locations == null)
			{
				locations = new List<Location>();
			}

			locations.Add(location);
		}

		public void AddBarcode(Barcode barcode)
		{
			if (barcodes == null)
			{
				barcodes = new List<Barcode>();
			}

			barcodes.Add(barcode);
		}

		public void AddNFC(NFC _nfc)
		{
			if (nfc == null)
			{
				nfc = new List<NFC>();
			}

			nfc.Add(_nfc);
		}

	}

}
