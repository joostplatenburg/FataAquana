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
	[Register ("OnderhoudController")]
	partial class OnderhoudController
	{
		[Outlet]
		AppKit.NSComboBox OnderhoudCombobox { get; set; }

		[Outlet]
		AppKit.NSButton OntvangenOpButton { get; set; }

		[Outlet]
		AppKit.NSDatePicker OntvangenOpDate { get; set; }

		[Outlet]
		AppKit.NSButton RetourOpButton { get; set; }

		[Outlet]
		AppKit.NSDatePicker RetourOpDate { get; set; }

		[Action ("CancelButton:")]
		partial void CancelButton (Foundation.NSObject sender);

		[Action ("CloseButton:")]
		partial void CloseButton (Foundation.NSObject sender);

		[Action ("OntvangenOpEnable:")]
		partial void OntvangenOpEnable (Foundation.NSObject sender);

		[Action ("RetourOpEnable:")]
		partial void RetourOpEnable (Foundation.NSObject sender);

		[Action ("SaveButton:")]
		partial void SaveButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (OnderhoudCombobox != null) {
				OnderhoudCombobox.Dispose ();
				OnderhoudCombobox = null;
			}

			if (OntvangenOpButton != null) {
				OntvangenOpButton.Dispose ();
				OntvangenOpButton = null;
			}

			if (OntvangenOpDate != null) {
				OntvangenOpDate.Dispose ();
				OntvangenOpDate = null;
			}

			if (RetourOpButton != null) {
				RetourOpButton.Dispose ();
				RetourOpButton = null;
			}

			if (RetourOpDate != null) {
				RetourOpDate.Dispose ();
				RetourOpDate = null;
			}
		}
	}
}
