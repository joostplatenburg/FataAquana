using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace FataAquana
{
	public partial class SplitViewController : NSSplitViewController
	{
		#region Computed Properties
		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
			}
		}
		#endregion

		#region Constructors

		// Called when created from unmanaged code
		public SplitViewController(IntPtr handle) : base(handle)
		{
		}
		#endregion

		private LeftViewController left;
		private RightViewController right;

		#region Override Methods
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Grab Elements
			left = LeftController.ViewController as LeftViewController;
			right = RightController.ViewController as RightViewController;

			// Wireup events
			left.ViewTypeChanged += (viewType) =>
			{
				right.ShowView(viewType);
			};

			AppDelegate.MainView = this;
		}

		public void EnableGroepList()
		{
			left.EnableGroepLijst();
			right.ShowView(SubviewType.Personen);
		}


		#endregion
	}
}
