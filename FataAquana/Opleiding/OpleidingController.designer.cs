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
	[Register ("OpleidingController")]
	partial class OpleidingController
	{
		[Outlet]
		AppKit.NSTextField Omschrijving { get; set; }

		[Outlet]
		AppKit.NSTextField Opleidingnaam { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (AppKit.NSButton sender);

		[Action ("SaveButton:")]
		partial void SaveButton (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Opleidingnaam != null) {
				Opleidingnaam.Dispose ();
				Opleidingnaam = null;
			}

			if (Omschrijving != null) {
				Omschrijving.Dispose ();
				Omschrijving = null;
			}
		}
	}
}
