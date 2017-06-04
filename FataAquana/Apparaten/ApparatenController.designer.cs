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
	[Register ("ApparatenController")]
	partial class ApparatenController
	{
		[Outlet]
		AppKit.NSTableColumn ApparaatnaamColumn { get; set; }

		[Outlet]
		AppKit.NSTableView ApparatenTable { get; set; }

		[Outlet]
		AppKit.NSTableColumn OmschrijvingColumn { get; set; }
		

		[Action("ApparaatAddClicked:")]
		partial void ApparaatAddClicked(AppKit.NSButton sender);

		[Action("ApparaatRemoveClicked:")]
		partial void ApparaatRemoveClicked(AppKit.NSButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (OmschrijvingColumn != null) {
				OmschrijvingColumn.Dispose ();
				OmschrijvingColumn = null;
			}

			if (ApparatenTable != null) {
				ApparatenTable.Dispose ();
				ApparatenTable = null;
			}

			if (ApparaatnaamColumn != null) {
				ApparaatnaamColumn.Dispose ();
				ApparaatnaamColumn = null;
			}
		}
	}
}
