using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class OnderhoudDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "OnderhoudCell";
		#endregion

		#region Private Variables
		private OnderhoudDS DataSource;
		#endregion

		#region Constructors
		public OnderhoudDelegate(OnderhoudDS dataSource)
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
				case "ApparaatnaamIO":
					view.StringValue = DataSource.Onderhoud[(int)row].ApparaatNaam;
					tableColumn.Width = 140;
					break;
				case "Ontvangen op":
					 dt = AppDelegate.NSDateToDateTime(DataSource.Onderhoud[(int)row].OntvangenOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
				case "Retour op":
					dt = AppDelegate.NSDateToDateTime(DataSource.Onderhoud[(int)row].RetourOp);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 200;
					break;
			}
			return view;
		}

		public override void MouseDownInHeaderOfTableColumn(NSTableView tableView, NSTableColumn tableColumn)
		{
			Debug.WriteLine("Start: OnderhoudDelegate.MouseDownInHeaderOfTableColumn");

			//base.MouseDownInHeaderOfTableColumn(tableView, tableColumn);

			Debug.WriteLine(tableColumn.Title + " ("+ tableColumn.Width + ")");

			for (int i = 0; i < tableView.ColumnCount; i++)
			//foreach (NSTableColumn tc in tableView.TableColumns())
			{
				//Debug.WriteLine(GetSizeToFitColumnWidth(tableView, i));
			}
			Debug.WriteLine("Einde: OnderhoudDelegate.MouseDownInHeaderOfTableColumn");
		}

		public override void ColumnDidResize(Foundation.NSNotification notification)
		{
			//base.ColumnDidResize(notification);
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			Debug.WriteLine("Start: OnderhoudDelegate.ShouldSelectRow(" + row + ")");

			//AppDelegate.MainView.RaisePersoonSelected(row);

			var selectedRowIndex = tableView.SelectedRow;
			var selectedAchternaam = DataSource.Onderhoud[(int)row].ApparaatNaam;

			Debug.WriteLine("Einde: OnderhoudDelegate.ShouldSelectRow");

			return true;
		}

		public override bool SelectionShouldChange(NSTableView tableView)
		{
			Debug.WriteLine("Start: OnderhoudDelegate.SelectionShouldChange");
			//base.SelectionShouldChange(tableView);

			Debug.WriteLine("Selection row: " + tableView.SelectedRow);

			Debug.WriteLine("Einde: OnderhoudDelegate.SelectionShouldChange");

			return true;
		}
		#endregion
	}
}

