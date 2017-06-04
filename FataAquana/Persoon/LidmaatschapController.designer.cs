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
	[Register ("LidmaatschapController")]
	partial class LidmaatschapController
	{
		[Outlet]
		AppKit.NSButton IngeschrevenOpButton { get; set; }

		[Outlet]
		AppKit.NSDatePicker IngeschrevenOpDate { get; set; }

		[Outlet]
		AppKit.NSComboBox LidmaatschapCombobox { get; set; }

		[Outlet]
		AppKit.NSButton UitgeschrevenOpButton { get; set; }

		[Outlet]
		AppKit.NSDatePicker UitgeschrevenOpDate { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (Foundation.NSObject sender);

		[Action ("IngeschrevenOpEnable:")]
		partial void IngeschrevenOpEnable (Foundation.NSObject sender);

		[Action ("SaveButton:")]
		partial void SaveButton (Foundation.NSObject sender);

		[Action ("UitgeschrevenOpEnable:")]
		partial void UitgeschrevenOpEnable (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (LidmaatschapCombobox != null) {
				LidmaatschapCombobox.Dispose ();
				LidmaatschapCombobox = null;
			}

			if (IngeschrevenOpButton != null) {
				IngeschrevenOpButton.Dispose ();
				IngeschrevenOpButton = null;
			}

			if (IngeschrevenOpDate != null) {
				IngeschrevenOpDate.Dispose ();
				IngeschrevenOpDate = null;
			}

			if (UitgeschrevenOpButton != null) {
				UitgeschrevenOpButton.Dispose ();
				UitgeschrevenOpButton = null;
			}

			if (UitgeschrevenOpDate != null) {
				UitgeschrevenOpDate.Dispose ();
				UitgeschrevenOpDate = null;
			}
		}
	}
}
