// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FataAquana
{
	[Register ("PersoonController")]
	partial class PersoonController
	{
		[Outlet]
		AppKit.NSTableView AankopenTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn Apparaatnaam { get; set; }

		[Outlet]
		AppKit.NSTableColumn ApparaatnaamIO { get; set; }

		[Outlet]
		AppKit.NSTableColumn Clubnaam { get; set; }

		[Outlet]
		AppKit.NSTableColumn GekochtOp { get; set; }

		[Outlet]
		AppKit.NSTableColumn GeslaagdOp { get; set; }

		[Outlet]
		AppKit.NSTableView GevolgdeOpleidingenTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn IngeschrevenOp { get; set; }

		[Outlet]
		AppKit.NSTableView LidmaatschappenTable { get; set; }

		[Outlet]
		AppKit.NSTableView OnderhoudTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn OntvangenOp { get; set; }

		[Outlet]
		AppKit.NSTableColumn Opleidingnaam { get; set; }

		[Outlet]
		AppKit.NSTableColumn RetourOp { get; set; }

		[Outlet]
		AppKit.NSTableColumn UitgeschrevenOp { get; set; }

		[Outlet]
		AppKit.NSTableColumn VerlopenOp { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (AppKit.NSButton sender);

		[Action ("OKButton:")]
		partial void OKButton (AppKit.NSButton sender);

		[Action ("SaveButton:")]
		partial void SaveButton (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (AankopenTable != null) {
				AankopenTable.Dispose ();
				AankopenTable = null;
			}

			if (Apparaatnaam != null) {
				Apparaatnaam.Dispose ();
				Apparaatnaam = null;
			}

			if (ApparaatnaamIO != null) {
				ApparaatnaamIO.Dispose ();
				ApparaatnaamIO = null;
			}

			if (Clubnaam != null) {
				Clubnaam.Dispose ();
				Clubnaam = null;
			}

			if (GekochtOp != null) {
				GekochtOp.Dispose ();
				GekochtOp = null;
			}

			if (GeslaagdOp != null) {
				GeslaagdOp.Dispose ();
				GeslaagdOp = null;
			}

			if (GevolgdeOpleidingenTable != null) {
				GevolgdeOpleidingenTable.Dispose ();
				GevolgdeOpleidingenTable = null;
			}

			if (IngeschrevenOp != null) {
				IngeschrevenOp.Dispose ();
				IngeschrevenOp = null;
			}

			if (LidmaatschappenTable != null) {
				LidmaatschappenTable.Dispose ();
				LidmaatschappenTable = null;
			}

			if (OnderhoudTable != null) {
				OnderhoudTable.Dispose ();
				OnderhoudTable = null;
			}

			if (OntvangenOp != null) {
				OntvangenOp.Dispose ();
				OntvangenOp = null;
			}

			if (Opleidingnaam != null) {
				Opleidingnaam.Dispose ();
				Opleidingnaam = null;
			}

			if (RetourOp != null) {
				RetourOp.Dispose ();
				RetourOp = null;
			}

			if (UitgeschrevenOp != null) {
				UitgeschrevenOp.Dispose ();
				UitgeschrevenOp = null;
			}

			if (VerlopenOp != null) {
				VerlopenOp.Dispose ();
				VerlopenOp = null;
			}
		}
	}
}
