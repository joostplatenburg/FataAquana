using System;

using Foundation;
using AppKit;


namespace FataAquana
{
	public class OpleidingEditorSheetController : NSObject
	{
		#region Private Variables
		private OpleidingModel _opleiding;
		#endregion

		#region Outlets
		[Outlet]
		AppKit.NSButton CancelButton { get; set; }
		#endregion

		#region Computed Properties
		[Export("window")]
		public ClubEditSheet Window { get; set;}

		[Export("Opleiding")]
		public OpleidingModel Opleiding {
			get { return _opleiding; }
			set {
				WillChangeValue ("Opleiding");
				_opleiding = value;
				DidChangeValue ("Opleiding");
			}
		}
		#endregion

		#region Constructors
		public OpleidingEditorSheetController (OpleidingModel opleiding, bool isNew)
		{
			// Save person
			Opleiding = opleiding;

			// Load the .xib file for the sheet
			NSBundle.LoadNib ("OpleidingEditSheet", this);

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
			RaiseOpleidingModified(Opleiding);
			CloseSheet();
		}
		#endregion

		#region Events
		public delegate void OpleidingModifiedDelegate(OpleidingModel opleiding);
		public event OpleidingModifiedDelegate OpleidingModified;

		internal void RaiseOpleidingModified(OpleidingModel opleiding) {
			if (this.OpleidingModified!=null) this.OpleidingModified(opleiding);
		}
		#endregion
	}
}

