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
						Personen.Sort((x, y) => x.Achternaam.CompareTo(y.Achternaam));
					}
					else {
						Personen.Sort((x, y) => -1 * x.Achternaam.CompareTo(y.Achternaam));
					}
					break;
				case "Adres":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Adres.CompareTo(y.Adres));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Adres.CompareTo(y.Adres));
					}
					break;
				case "Email":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Email.CompareTo(y.Email));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Email.CompareTo(y.Email));
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
						Personen.Sort((x, y) => x.Initialen.CompareTo(y.Initialen));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Initialen.CompareTo(y.Initialen));
					}
					break;
				case "Mobiel":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Mobiel.CompareTo(y.Mobiel));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Mobiel.CompareTo(y.Mobiel));
					}
					break;
				case "Postcode":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Postcode.CompareTo(y.Postcode));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Postcode.CompareTo(y.Postcode));
					}
					break;
				case "Telefoon":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Telefoon.CompareTo(y.Telefoon));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Telefoon.CompareTo(y.Telefoon));
					}
					break;
				case "Tussenvoegsel":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Tussenvoegsel.CompareTo(y.Tussenvoegsel));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Tussenvoegsel.CompareTo(y.Tussenvoegsel));
					}
					break;
				case "Voornamen":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Voornamen.CompareTo(y.Voornamen));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Voornamen.CompareTo(y.Voornamen));
					}
					break;
				case "Woonplaats":
					if (ascending)
					{
						Personen.Sort((x, y) => x.Woonplaats.CompareTo(y.Woonplaats));
					}
					else
					{
						Personen.Sort((x, y) => -1 * x.Woonplaats.CompareTo(y.Woonplaats));
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

