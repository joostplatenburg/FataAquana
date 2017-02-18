using System;

using Foundation;
using AppKit;


namespace FataAquana
{
	public class ApparaatEditorSheetController : NSObject
	{
		#region Private Variables
		private ApparaatModel _apparaat;
		#endregion

		#region Outlets
		[Outlet]
		AppKit.NSButton CancelButton { get; set; }
		#endregion

		#region Computed Properties
		[Export("window")]
		public ApparaatEditSheet Window { get; set;}

		[Export("Apparaat")]
		public ApparaatModel Apparaat {
			get { return _apparaat; }
			set {
				WillChangeValue ("Apparaat");
				_apparaat = value;
				DidChangeValue ("Apparaat");
			}
		}
		#endregion

		#region Constructors
		public ApparaatEditorSheetController (ApparaatModel apparaat, bool isNew)
		{
			// Save person
			Apparaat = apparaat;

			// Load the .xib file for the sheet
			NSBundle.LoadNib ("ApparaatEditSheet", this);

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
			RaiseApparaatModified(Apparaat);
			CloseSheet();
		}
		#endregion

		#region Events
		public delegate void ApparaatModifiedDelegate(ApparaatModel apparaat);
		public event ApparaatModifiedDelegate ApparaatModified;

		internal void RaiseApparaatModified(ApparaatModel apparaat) {
			if (this.ApparaatModified!=null) this.ApparaatModified(apparaat);
		}
		#endregion
	}
}

