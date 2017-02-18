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
	[Register ("OpleidingenController")]
	partial class OpleidingenController
	{
		[Outlet]
		AppKit.NSTableColumn OmschrijvingColumn { get; set; }

		[Outlet]
		AppKit.NSTableView OpleidingenTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn OpleidingnaamColumn { get; set; }

		[Action ("OpleidingAddClicked:")]
		partial void OpleidingAddClicked (AppKit.NSButton sender);

		[Action ("OpleidingRemoveClicked:")]
		partial void OpleidingRemoveClicked (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (OmschrijvingColumn != null) {
				OmschrijvingColumn.Dispose ();
				OmschrijvingColumn = null;
			}

			if (OpleidingenTable != null) {
				OpleidingenTable.Dispose ();
				OpleidingenTable = null;
			}

			if (OpleidingnaamColumn != null) {
				OpleidingnaamColumn.Dispose ();
				OpleidingnaamColumn = null;
			}
		}
	}
}
