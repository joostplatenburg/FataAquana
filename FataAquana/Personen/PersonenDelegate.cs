using System;
using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	public class PersonenDelegate : NSTableViewDelegate
	{
		#region Constants
		private const string CellIdentifier = "PersonenCell";
		#endregion

		#region Private Variables
		private PersonenDS DataSource;
		//private PersonenController Controller;
		#endregion

		#region Constructors
		public PersonenDelegate(PersonenDS dataSource)
		{
			// Initialize
			this.DataSource = dataSource;
		}

		public PersonenDelegate(PersonenController controller, PersonenDS dataSource)
		{
			// Initialize
			this.DataSource = dataSource;
		//	this.Controller = controller;
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

			// Setup view based on the column selected
			switch (tableColumn.Title)
			{
				case "Achternaam":
					view.StringValue = DataSource.Personen[(int)row].Achternaam;
					tableColumn.Width = 140;
					break;
				case "Adres":
					view.StringValue = DataSource.Personen[(int)row].Adres;
					tableColumn.Width = 200;
					break;
				case "Email":
					view.StringValue = DataSource.Personen[(int)row].Email;
					tableColumn.Width = 210;
					break;
				case "Geboortedatum":
					DateTime dt = AppDelegate.NSDateToDateTime(DataSource.Personen[(int)row].Geboortedatum);
					view.StringValue = dt.ToLongDateString();
					tableColumn.Width = 210;
					break;
				case "Imagepath":
					view.StringValue = DataSource.Personen[(int)row].Imagepath;
					tableColumn.Width = 210;
					break;
				case "Initialen":
					view.StringValue = DataSource.Personen[(int)row].Initialen;
					tableColumn.Width = 65;
					break;
				case "Mobiel":
					view.StringValue = DataSource.Personen[(int)row].Mobiel;
					tableColumn.Width = 100;
					break;
				case "Postcode":
					view.StringValue = DataSource.Personen[(int)row].Postcode;
					tableColumn.Width = 60;
					break;
				case "Telefoon":
					view.StringValue = DataSource.Personen[(int)row].Telefoon;
					tableColumn.Width = 100;
					break;
				case "Tussenvoegsel":
					view.StringValue = DataSource.Personen[(int)row].Tussenvoegsel;
					tableColumn.Width = 55;
					break;
				case "Voornamen":
					view.StringValue = DataSource.Personen[(int)row].Voornamen;
					tableColumn.Width = 100;
					break;
				case "Woonplaats":
					view.StringValue = DataSource.Personen[(int)row].Woonplaats;
					tableColumn.Width = 120;
					break;
				case "Opleidingen":
					view.StringValue = DataSource.Personen[(int)row].GevolgdeOpleidingenString;
					tableColumn.Width = 360;
					break;
				case "Aankopen":
					view.StringValue = DataSource.Personen[(int)row].AankopenString;
					tableColumn.Width = 360;
					break;
				case "Onderhoud":
					view.StringValue = DataSource.Personen[(int)row].InOnderhoudString;
					tableColumn.Width = 360;
					break;
				case "Lidmaatschappen":
					view.StringValue = DataSource.Personen[(int)row].LidmaatschappenString;
					tableColumn.Width = 360;
					break;
			}
			return view;
		}

		public override bool ShouldSelectRow(NSTableView tableView, nint row)
		{
            //Debug.WriteLine("Selected row: " + row);

            //AppDelegate.MainView.RaisePersoonSelected(row);

            //var selectedRowIndex = tableView.SelectedRow;
            //var selectedAchternaam = DataSource.Personen[(int)row].Achternaam;

            var selectedrows = tableView.SelectedRows;
            Debug.WriteLine("Selected row count: " + tableView.SelectedRowCount);

			return true;
		}


		#endregion
	}
}

