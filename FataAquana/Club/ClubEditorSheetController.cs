using System;

using Foundation;
using AppKit;


namespace FataAquana
{
	public class ClubEditorSheetController : NSObject
	{
		#region Private Variables
		private ClubModel _club;
		#endregion

		#region Outlets
		[Outlet]
		AppKit.NSButton CancelButton { get; set; }
		#endregion

		#region Computed Properties
		[Export("window")]
		public ClubEditSheet Window { get; set;}

		[Export("Club")]
		public ClubModel Club {
			get { return _club; }
			set {
				WillChangeValue ("Club");
				_club = value;
				DidChangeValue ("Club");
			}
		}
		#endregion

		#region Constructors
		public ClubEditorSheetController (ClubModel club, bool isNew)
		{
			// Save person
			Club = club;

			// Load the .xib file for the sheet
			NSBundle.LoadNib ("ClubEditSheet", this);

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
			RaiseClubModified(Club);
			CloseSheet();
		}
		#endregion

		#region Events
		public delegate void ClubModifiedDelegate(ClubModel club);
		public event ClubModifiedDelegate ClubModified;

		internal void RaiseClubModified(ClubModel club) {
			if (this.ClubModified!=null) this.ClubModified(club);
		}
		#endregion
	}
}

