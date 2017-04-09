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
	[Register ("WindowsController")]
	partial class WindowsController
	{
		[Outlet]
		AppKit.NSToolbarItem OpenDB { get; set; }

		[Outlet]
		AppKit.NSToolbarItem Print { get; set; }

		[Outlet]
		AppKit.NSToolbarItem Search { get; set; }

		[Outlet]
		AppKit.NSSearchField Zoekveld { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OpenDB != null) {
				OpenDB.Dispose ();
				OpenDB = null;
			}

			if (Print != null) {
				Print.Dispose ();
				Print = null;
			}

			if (Search != null) {
				Search.Dispose ();
				Search = null;
			}

			if (Zoekveld != null) {
				Zoekveld.Dispose ();
				Zoekveld = null;
			}
		}
	}
}
