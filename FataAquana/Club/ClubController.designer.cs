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
	[Register ("ClubController")]
	partial class ClubController
	{
		[Outlet]
		AppKit.NSTextField Clubadres { get; set; }

		[Outlet]
		AppKit.NSTextField Clubnaam { get; set; }

		[Outlet]
		AppKit.NSTextField Clubplaats { get; set; }

		[Outlet]
		AppKit.NSTextField Clubpostcode { get; set; }

		[Action ("CancelAction:")]
		partial void CancelAction (AppKit.NSButton sender);

		[Action ("CancelButton:")]
		partial void CancelButton (AppKit.NSButton sender);

		[Action ("OKaction:")]
		partial void OKaction (AppKit.NSButton sender);

		[Action ("SaveButton:")]
		partial void SaveButton (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Clubadres != null) {
				Clubadres.Dispose ();
				Clubadres = null;
			}

			if (Clubnaam != null) {
				Clubnaam.Dispose ();
				Clubnaam = null;
			}

			if (Clubplaats != null) {
				Clubplaats.Dispose ();
				Clubplaats = null;
			}

			if (Clubpostcode != null) {
				Clubpostcode.Dispose ();
				Clubpostcode = null;
			}
		}
	}
}
