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
	[Register ("GevolgdeOpleidingController")]
	partial class GevolgdeOpleidingController
	{
		[Outlet]
		AppKit.NSButton GeslaagdOpButton { get; set; }

		[Outlet]
		AppKit.NSDatePicker GeslaagdOpDate { get; set; }

		[Outlet]
		AppKit.NSComboBox OpleidingenCombobox { get; set; }

		[Outlet]
		AppKit.NSButton VerlopenOpButton { get; set; }

		[Outlet]
		AppKit.NSDatePicker VerlopenOpDate { get; set; }

		[Action ("CloseButton:")]
		partial void CloseButton (Foundation.NSObject sender);

		[Action ("GeslaagdOpEnable:")]
		partial void GeslaagdOpEnable (Foundation.NSObject sender);

		[Action ("SaveButton:")]
		partial void SaveButton (Foundation.NSObject sender);

		[Action ("VerlopenOpEnable:")]
		partial void VerlopenOpEnable (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (GeslaagdOpDate != null) {
				GeslaagdOpDate.Dispose ();
				GeslaagdOpDate = null;
			}

			if (OpleidingenCombobox != null) {
				OpleidingenCombobox.Dispose ();
				OpleidingenCombobox = null;
			}

			if (VerlopenOpDate != null) {
				VerlopenOpDate.Dispose ();
				VerlopenOpDate = null;
			}

			if (GeslaagdOpButton != null) {
				GeslaagdOpButton.Dispose ();
				GeslaagdOpButton = null;
			}

			if (VerlopenOpButton != null) {
				VerlopenOpButton.Dispose ();
				VerlopenOpButton = null;
			}
		}
	}
}
