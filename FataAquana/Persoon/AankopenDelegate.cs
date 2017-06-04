using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class AankopenDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "AankopenCell";
		#endregion

		#region Private Variables
		private AankopenDS DataSource;
		#endregion

		#region Constructors
		public AankopenDelegate(AankopenDS dataSource)
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

			DateTime dt;
			// Setup view based on the column selected
			switch (tableColumn.Title)
			{
				case "Apparaatnaam":
					view.StringValue = DataSource.Aankopen[(int)row].ApparaatNaam;
					tableColumn.Width = 140;
					break;
				case "Gekocht op":
					 dt = AppDelegate.NSDateToDateTime(DataSource.Aankopen[(int)row].GekochtOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
			}
			return view;
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			Debug.WriteLine("Start: AankopenDelegate.ShouldSelectRow(" + row + ")");

			//AppDelegate.MainView.RaisePersoonSelected(row);

			var selectedRowIndex = tableView.SelectedRow;
			var selectedAchternaam = DataSource.Aankopen[(int)row].ApparaatNaam;

			Debug.WriteLine("Einde: AankopenDelegate.ShouldSelectRow");

			return true;
		}
		#endregion
	}
}

