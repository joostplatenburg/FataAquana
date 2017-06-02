using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class PersonenDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<PersoonModel> Personen { get; set; } = new List<PersoonModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public PersonenDS(SqliteConnection conn)
		{
			Conn = conn;

			if (AppDelegate.Conn != null)
			{
				LoadPersonen(conn);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Personen.Count;
		}
		#endregion

		public void Sort(string key, bool ascending)
		{

			// Take action based on key
			switch (key)
			{
				case "Achternaam":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Achternaam, y.Achternaam, StringComparison.Ordinal));
					}
					else {
                        Personen.Sort((x, y) => -1 * string.Compare(x.Achternaam, y.Achternaam, StringComparison.Ordinal));
                    }
					break;
				case "Adres":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Adres, y.Adres, StringComparison.Ordinal));
					}
					else
					{
                        Personen.Sort((x, y) => -1 * string.Compare(x.Adres, y.Adres, StringComparison.Ordinal));
					}
					break;
				case "Email":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Email, y.Email, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Email, y.Email, StringComparison.Ordinal));
					}
					break;
				case "Geboortedatum":
					if (ascending)
					{
						Personen.Sort((x, y) => ((int)x.Geboortedatum.Compare(y.Geboortedatum)));
					}
					else
					{
						Personen.Sort((x, y) => -1 * ((int)x.Geboortedatum.Compare(y.Geboortedatum)));
					}
					break;
				case "Initialen":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Initialen, y.Initialen, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Initialen, y.Initialen, StringComparison.Ordinal));
					}
					break;
				case "Mobiel":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Mobiel, y.Mobiel, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Mobiel, y.Mobiel, StringComparison.Ordinal));
					}
					break;
				case "Postcode":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Postcode, y.Postcode, StringComparison.Ordinal));
					}
					else
					{
                        Personen.Sort((x, y) => -1 * string.Compare(x.Postcode, y.Postcode, StringComparison.Ordinal));
					}
					break;
				case "Telefoon":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Telefoon, y.Telefoon, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Telefoon, y.Telefoon, StringComparison.Ordinal));
					}
					break;
				case "Tussenvoegsel":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Tussenvoegsel, y.Tussenvoegsel, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Tussenvoegsel, y.Tussenvoegsel, StringComparison.Ordinal));
					}
					break;
				case "Voornamen":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Voornamen, y.Voornamen, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Voornamen, y.Voornamen, StringComparison.Ordinal));
					}
					break;
				case "Woonplaats":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.Woonplaats, y.Woonplaats, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.Woonplaats, y.Woonplaats, StringComparison.Ordinal));
					}
					break;
				case "OpleidingenString":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.GevolgdeOpleidingenString, y.GevolgdeOpleidingenString, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.GevolgdeOpleidingenString, y.GevolgdeOpleidingenString, StringComparison.Ordinal));
					}
					break;
				case "AankopenString":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.AankopenString, y.AankopenString, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.AankopenString, y.AankopenString, StringComparison.Ordinal));
					}
					break;
				case "InOnderhoudString":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.InOnderhoudString, y.InOnderhoudString, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.InOnderhoudString, y.InOnderhoudString, StringComparison.Ordinal));
					}
					break;
				case "LidmaatschappenString":
					if (ascending)
					{
						Personen.Sort((x, y) => string.Compare(x.LidmaatschappenString, y.LidmaatschappenString, StringComparison.Ordinal));
					}
					else
					{
						Personen.Sort((x, y) => -1 * string.Compare(x.LidmaatschappenString, y.LidmaatschappenString, StringComparison.Ordinal));
					}
					break;
			}

		}

		public override void SortDescriptorsChanged(NSTableView tableView, NSSortDescriptor[] oldDescriptors)
		{
			// Sort the data
			if (oldDescriptors.Length > 0)
			{
				// Update sort
				Sort(oldDescriptors[0].Key, oldDescriptors[0].Ascending);
			}
			else {
				// Grab current descriptors and update sort
				NSSortDescriptor[] tbSort = tableView.SortDescriptors;
				Sort(tbSort[0].Key, tbSort[0].Ascending);
			}

			// Refresh table
			tableView.ReloadData();
		}

		void LoadPersonen(SqliteConnection conn)
		{
			bool shouldClose = false;

			// Is the database already open?
			if (conn.State != ConnectionState.Open)
			{
				shouldClose = true;
				conn.Open();
			}

			// Execute query
			using (var command = conn.CreateCommand())
			{
				try
				{
					// Create new command
					command.CommandText = "SELECT DISTINCT ID FROM [Persoon]";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var persoon = new PersoonModel();
							var id = (string)reader["ID"];

							persoon.Load(conn, id);

							AddPersoon(persoon);
						}
					}
				}
				catch (Exception Exception)
				{
					Debug.WriteLine(Exception.Message);
				}
			}

			if (shouldClose)
			{
				conn.Close();
			}
		}

		public bool AddPersoon(PersoonModel _persoon)
		{
			Personen.Add(_persoon);

			return true;
		}
	}
}

