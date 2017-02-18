using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class OpleidingenDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "OpleidingenCell";
		#endregion

		#region Private Variables
		private OpleidingenDS DataSource;
		#endregion

		#region Contsructors
		public OpleidingenDelegate(OpleidingenDS dataSource)
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
				case "Opleidingnaam":
					view.StringValue = DataSource.Opleidingen[(int)row].OpleidingNaam;
					break;
				case "Omschrijving":
					view.StringValue = DataSource.Opleidingen[(int)row].Omschrijving;
					break;
			}

			return view;
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
			Debug.WriteLine("Selected row: " + row);

			//AppDelegate.MainView.RaisePersoonSelected(row);

			return true;
		}

		#endregion
	}
}

