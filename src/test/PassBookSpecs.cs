using Machine.Specifications;
using PassSharp;
using PassSharp.Fields;

#pragma warning disable 414

namespace Test
{

	[Subject("Passbook Fields")]
	public class when_adding_fields
	{
		static Pass pass;

		Establish context = () =>
		{
			pass = new Pass();
		};

		public class when_adding_field_types
		{
			Because of = () => pass.AddField(FieldType.Header, new Field { });
			It should_not_have_empty_fields = () => pass.fields.ShouldNotBeNull();
			It should_have_one_field = () => pass.fields.Count.ShouldEqual(1);
		}

		public class when_adding_beacons
		{
			Because of = () => pass.AddBeacon(new Beacon { });
			It should_not_have_empty_beacons = () => pass.beacons.ShouldNotBeNull();
			It should_have_one_beacon = () => pass.beacons.Count.ShouldEqual(1);
		}

		public class when_adding_locations
		{
			Because of = () => pass.AddLocation(new Location { });
			It should_not_have_empty_locations = () => pass.locations.ShouldNotBeNull();
			It should_have_one_location = () => pass.locations.Count.ShouldEqual(1);
		}

		public class when_adding_barcodes
		{
			Because of = () => pass.AddBarcode(new Barcode { });
			It should_not_have_empty_barcodes = () => pass.barcodes.ShouldNotBeNull();
			It should_have_one_barcode = () => pass.barcodes.Count.ShouldEqual(1);
		}

		public class when_adding_nfc
		{
			Because of = () => pass.AddNFC(new NFC { });
			It should_not_have_empty_nfcs = () => pass.nfc.ShouldNotBeNull();
			It should_have_one_nfc = () => pass.nfc.Count.ShouldEqual(1);
		}

	}

}
