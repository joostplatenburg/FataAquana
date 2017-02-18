using System;

using Foundation;
using AppKit;
using System.Collections.Generic;

namespace FataAquana
{
	public class GevolgdeOpleidingEditSheetController : NSObject
	{
		#region Private Variables
		private GevolgdeOpleidingModel _gevolgdeopleiding;
		#endregion

		#region Outlets
		[Outlet]
		AppKit.NSButton CancelButton { get; set; }
		#endregion

		#region Computed Properties
		[Export("window")]
		public GevolgdeOpleidingEditSheet Window { get; set;}

		[Export("GevolgdeOpleiding")]
		public GevolgdeOpleidingModel GevolgdeOpleiding {
			get { return _gevolgdeopleiding; }
			set {
				WillChangeValue ("GevolgdeOpleiding");
				_gevolgdeopleiding = value;
				DidChangeValue ("GevolgdeOpleiding");
			}
		}
		#endregion

		#region Constructors
		public GevolgdeOpleidingEditSheetController (GevolgdeOpleidingModel gevolgdeopleiding, bool isNew)
		{
			// Save person
			GevolgdeOpleiding = gevolgdeopleiding;

			// Load the .xib file for the sheet
			NSBundle.LoadNib ("GevolgdeOpleidingEditSheet", this);

			CancelButton.Hidden = !isNew;

		}
		#endregion

		#region Public Methods
		public void ShowSheet(NSWindow inWindow) {
			NSApplication.SharedApplication.BeginSheet (Window, inWindow);
		}

		public void CloseSheet() {
			NSApplication.SharedApplication.EndSheet (Window);
			Window.Close();
		}
		#endregion

		#region Actions
		[Action ("CancelAction:")]
		public void CancelAction (Foundation.NSObject sender){
			CloseSheet();
		}

		[Action ("OkAction:")]
		public void OkAction (Foundation.NSObject sender){
			RaiseGevolgdeOpleidingModified(GevolgdeOpleiding);
			CloseSheet();
		}
		#endregion

		#region Events
		public delegate void GevolgdeOpleidingModifiedDelegate(GevolgdeOpleidingModel gevolgdeopleiding);
		public event GevolgdeOpleidingModifiedDelegate GevolgdeOpleidingModified;

		internal void RaiseGevolgdeOpleidingModified(GevolgdeOpleidingModel gevolgdeopleiding) {
			if (this.GevolgdeOpleidingModified!=null) this.GevolgdeOpleidingModified(gevolgdeopleiding);
		}
		#endregion
	}
}

