using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class ApparatenDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<ApparaatModel> Apparaten { get; set; } = new List<ApparaatModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public ApparatenDS(SqliteConnection conn)
		{
			Conn = conn;

			if (AppDelegate.Conn != null)
			{
				LoadApparaten(conn);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Apparaten.Count;
		}
		#endregion

		public void Sort(string key, bool ascending)
		{

			// Take action based on key
			switch (key)
			{
				case "Apparaatnaam":
					if (ascending)
					{
						Apparaten.Sort((x, y) => x.ApparaatNaam.CompareTo(y.ApparaatNaam));
					}
					else {
						Apparaten.Sort((x, y) => -1 * x.ApparaatNaam.CompareTo(y.ApparaatNaam));
					}
					break;
				case "Omschrijving":
					if (ascending)
					{
						Apparaten.Sort((x, y) => x.Omschrijving.CompareTo(y.Omschrijving));
					}
					else {
						Apparaten.Sort((x, y) => -1 * x.Omschrijving.CompareTo(y.Omschrijving));
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

		void LoadApparaten(SqliteConnection conn)
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
					command.CommandText = "SELECT DISTINCT ID FROM [Apparaat]";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var apparaat = new ApparaatModel();
							var id = (string)reader["ID"];

							apparaat.Load(conn, id);

							//AddPersoon(persoon);
							Apparaten.Add(apparaat);
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

		public bool AddApparaat(ApparaatModel _apparaat)
		{
			Apparaten.Add(_apparaat);

			return true;
		}
	}
}

