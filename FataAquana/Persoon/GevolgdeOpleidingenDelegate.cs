using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class GevolgdeOpleidingenDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "PersonenCell";
		#endregion

		#region Private Variables
		private GevolgdeOpleidingenDS DataSource;
		//private PersoonEditorSheetController Controller;
		#endregion

		#region Constructors
		public GevolgdeOpleidingenDelegate(GevolgdeOpleidingenDS dataSource)
		{
			// Initialize
			this.DataSource = dataSource;
		}
		/*
		public GevolgdeOpleidingenDelegate(PersoonEditorSheetController controller, GevolgdeOpleidingenDS dataSource)
		{
			// Initialize
			this.DataSource = dataSource;
			this.Controller = controller;
		}*/
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
				case "Opleidingnaam":
					view.StringValue = DataSource.GevolgdeOpleidingen[(int)row].OpleidingNaam;
					tableColumn.Width = 140;
					break;
				case "Geslaagd op":
					 dt = AppDelegate.NSDateToDateTime(DataSource.GevolgdeOpleidingen[(int)row].GeslaagdOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
				case "Verlopen op":
					dt = AppDelegate.NSDateToDateTime(DataSource.GevolgdeOpleidingen[(int)row].VerlopenOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
			}
			return view;
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			//Debug.WriteLine("Selected row: " + row);

			//AppDelegate.MainView.RaisePersoonSelected(row);

			var selectedRowIndex = tableView.SelectedRow;
			var selectedAchternaam = DataSource.GevolgdeOpleidingen[(int)row].OpleidingNaam;

			return true;
		}
		#endregion
	}
}

