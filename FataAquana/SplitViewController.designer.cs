using Foundation;
using System.CodeDom.Compiler;

namespace FataAquana
{

	// Should subclass AppKit.NSView
	[Register("SplitViewController")]
	public partial class SplitViewController
	{
		[Outlet]
		AppKit.NSSplitViewItem LeftController { get; set; }

		[Outlet]
		AppKit.NSSplitViewItem RightController { get; set; }

		[Outlet]
		AppKit.NSSplitView SplitView { get; set; }

		void ReleaseDesignerOutlets()
		{
			if (SplitView != null) {
				SplitView.Dispose();
				SplitView = null;
			}
			if (LeftController != null) {
				LeftController.Dispose();
				LeftController = null;
			}
			if (RightController != null) {
				RightController.Dispose();
				RightController = null;
			}
}
	}
}
