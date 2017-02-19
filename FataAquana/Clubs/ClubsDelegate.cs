using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class ClubsDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "ClubsCell";
		#endregion

		#region Private Variables
		private ClubsDS DataSource;
		#endregion

		#region Contsructors
		public ClubsDelegate(ClubsDS dataSource)
		{
			// Initialize
			this.DataSource = dataSource;
		}
		#endregion

		#region Override Methods
		public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
		{
			// This pattern allows you reuse existing views when they are no-longer in use.
			// If the returned view is null, you instance up a new view
			// If a non-null view is returned, you modify it enough to reflect the new data
			NSTextField view = (NSTextField)tableView.MakeView(CellIdentifier, this);
			if (view == null)
			{
				view = new NSTextField();
				view.Identifier = CellIdentifier;
				view.BackgroundColor = NSColor.Clear;
				view.Bordered = false;
				view.Selectable = false;
				view.Editable = true;
			}

			// Setup view based on the column selected
			switch (tableColumn.Title)
			{
				case "Clubnaam":
					view.StringValue = DataSource.Clubs[(int)row].ClubNaam;
					break;
				case "Adres":
					view.StringValue = DataSource.Clubs[(int)row].Adres;
					break;
				case "Postcode":
					view.StringValue = DataSource.Clubs[(int)row].Postcode;
					break;
				case "Plaats":
					view.StringValue = DataSource.Clubs[(int)row].Plaats;
					break;
			}

			return view;
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			//Debug.WriteLine("Selected row: " + row);

			//AppDelegate.MainView.RaisePersoonSelected(row);

			return true;
		}

		#endregion
	}
}

