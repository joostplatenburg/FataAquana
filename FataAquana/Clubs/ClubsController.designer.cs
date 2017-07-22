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
	[Register ("ClubsController")]
	partial class ClubsController
	{
		[Outlet]
		AppKit.NSTableColumn AdresColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn ClubnaamColumn { get; set; }

		[Outlet]
		AppKit.NSTableView ClubsTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn PlaatsColumn { get; set; }

		[Outlet]
		AppKit.NSTableColumn PostcodeColumn { get; set; }

		[Action ("ClubAddClicked:")]
		partial void ClubAddClicked (AppKit.NSButton sender);

		[Action ("ClubRemoveClicked:")]
		partial void ClubRemoveClicked (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ClubnaamColumn != null) {
				ClubnaamColumn.Dispose ();
				ClubnaamColumn = null;
			}

			if (ClubsTable != null) {
				ClubsTable.Dispose ();
				ClubsTable = null;
			}

			if (AdresColumn != null) {
				AdresColumn.Dispose ();
				AdresColumn = null;
			}

			if (PostcodeColumn != null) {
				PostcodeColumn.Dispose ();
				PostcodeColumn = null;
			}

			if (PlaatsColumn != null) {
				PlaatsColumn.Dispose ();
				PlaatsColumn = null;
			}
		}
	}
}
