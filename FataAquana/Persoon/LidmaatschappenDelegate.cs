using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class LidmaatschappenDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "LidmaatschapCell";
		#endregion

		#region Private Variables
		private LidmaatschappenDS DataSource;
		#endregion

		#region Constructors
		public LidmaatschappenDelegate(LidmaatschappenDS dataSource)
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
				view.BackgroundColor = NSColor.White;
				view.Bordered = false;
				view.Selectable = false;
                view.Editable = false;
			}

			DateTime dt;
			// Setup view based on the column selected
			switch (tableColumn.Title)
			{
				case "Clubnaam":
					view.StringValue = DataSource.Lidmaatschappen[(int)row].ClubNaam;
					tableColumn.Width = 140;
					break;
				case "Ingeschreven op":
					 dt = AppDelegate.NSDateToDateTime(DataSource.Lidmaatschappen[(int)row].IngeschrevenOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
				case "Uitgeschreven op":
					dt = AppDelegate.NSDateToDateTime(DataSource.Lidmaatschappen[(int)row].UitgeschrevenOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
			}
			return view;
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			Debug.WriteLine("Start: LidmaatschappenDelegate.ShouldSelectRow(" + row + ")");

			//AppDelegate.MainView.RaisePersoonSelected(row);

			var selectedRowIndex = tableView.SelectedRow;
			var selectedAchternaam = DataSource.Lidmaatschappen[(int)row].ClubNaam;

			Debug.WriteLine("Einde: LidmaatschappenDelegate.ShouldSelectRow");

			return true;
		}
		#endregion
	}
}

