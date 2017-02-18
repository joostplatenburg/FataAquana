// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;

namespace FataAquana
{
	public partial class ClubsView : NSView
	{
		public ClubsView (IntPtr handle) : base (handle)
		{
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public ClubsView(NSCoder coder) : base (coder)
		{
			Initialize();
		}

		// Shared initialization code
		void Initialize()
		{
		}

		public override void AddConstraint(NSLayoutConstraint constraint)
		{
			base.AddConstraint(constraint);
		}

		public override void ResizeWithOldSuperviewSize(CoreGraphics.CGSize oldSize)
		{
			base.ResizeWithOldSuperviewSize(oldSize);

			//Console.WriteLine("PersonenView::ResizeWithOldSuperviewSize(" + oldSize.ToString() + ")");
			//Console.WriteLine("Frame.Size: " + Frame.Size.ToString());
			//Console.WriteLine("Frame.Location:  " + Frame.Location.ToString());

			//Console.WriteLine("");

			oldSize.Width = oldSize.Width + 2;
			SetFrameSize(oldSize);
			SetFrameOrigin(new CoreGraphics.CGPoint(0, 0));
		}
	}
}
