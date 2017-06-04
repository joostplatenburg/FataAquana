// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FataAquana
{
	[Register ("PersonenController")]
	partial class PersonenController
	{
		[Outlet]
		AppKit.NSTableColumn AankopenColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn AchternaamColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn AdresColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn EmailColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn Geboortedatum { get; set; }

		[Outlet]
		AppKit.NSTableColumn InitialenColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn LidmaatschappenColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn MobielColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn OnderhoudColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn OpleidingenColumn { get; set; }

		[Outlet]
		AppKit.NSTableView PersonenTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn PostcodeColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn TelefoonColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn TussenvoegselColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn VoornamenColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn WoonplaatsColumn { get; set; }

		[Action ("PersoonAddClicked:")]
		partial void PersoonAddClicked (Foundation.NSObject sender);

		[Action ("PersoonRemoveClicked:")]
		partial void PersoonRemoveClicked (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AankopenColumn != null) {
				AankopenColumn.Dispose ();
				AankopenColumn = null;
			}

			if (AchternaamColumn != null) {
				AchternaamColumn.Dispose ();
				AchternaamColumn = null;
			}

			if (AdresColumn != null) {
				AdresColumn.Dispose ();
				AdresColumn = null;
			}

			if (EmailColumn != null) {
				EmailColumn.Dispose ();
				EmailColumn = null;
			}

			if (Geboortedatum != null) {
				Geboortedatum.Dispose ();
				Geboortedatum = null;
			}

			if (InitialenColumn != null) {
				InitialenColumn.Dispose ();
				InitialenColumn = null;
			}

			if (LidmaatschappenColumn != null) {
				LidmaatschappenColumn.Dispose ();
				LidmaatschappenColumn = null;
			}

			if (MobielColumn != null) {
				MobielColumn.Dispose ();
				MobielColumn = null;
			}

			if (OnderhoudColumn != null) {
				OnderhoudColumn.Dispose ();
				OnderhoudColumn = null;
			}

			if (PersonenTable != null) {
				PersonenTable.Dispose ();
				PersonenTable = null;
			}

			if (PostcodeColumn != null) {
				PostcodeColumn.Dispose ();
				PostcodeColumn = null;
			}

			if (TelefoonColumn != null) {
				TelefoonColumn.Dispose ();
				TelefoonColumn = null;
			}

			if (TussenvoegselColumn != null) {
				TussenvoegselColumn.Dispose ();
				TussenvoegselColumn = null;
			}

			if (VoornamenColumn != null) {
				VoornamenColumn.Dispose ();
				VoornamenColumn = null;
			}

			if (WoonplaatsColumn != null) {
				WoonplaatsColumn.Dispose ();
				WoonplaatsColumn = null;
			}

			if (OpleidingenColumn != null) {
				OpleidingenColumn.Dispose ();
				OpleidingenColumn = null;
			}
		}
	}
}
