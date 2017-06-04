using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class ClubsDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<ClubModel> Clubs { get; set; } = new List<ClubModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public ClubsDS(SqliteConnection conn)
		{
			Conn = conn;

			if (AppDelegate.Conn != null)
			{
				LoadClubs(conn);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Clubs.Count;
		}
		#endregion

		public void Sort(string key, bool ascending)
		{

			// Take action based on key
			switch (key)
			{
				case "Clubnaam":
					if (ascending)
					{
						Clubs.Sort((x, y) => x.ClubNaam.CompareTo(y.ClubNaam));
					}
					else {
						Clubs.Sort((x, y) => -1 * x.ClubNaam.CompareTo(y.ClubNaam));
					}
					break;
				case "Adres":
					if (ascending)
					{
						Clubs.Sort((x, y) => x.Adres.CompareTo(y.Adres));
					}
					else {
						Clubs.Sort((x, y) => -1 * x.Adres.CompareTo(y.Adres));
					}
					break;
				case "Postcode":
					if (ascending)
					{
						Clubs.Sort((x, y) => x.Postcode.CompareTo(y.Postcode));
					}
					else
					{
						Clubs.Sort((x, y) => -1 * x.Postcode.CompareTo(y.Postcode));
					}
					break;
				case "Plaats":
					if (ascending)
					{
						Clubs.Sort((x, y) => x.Plaats.CompareTo(y.Plaats));
					}
					else
					{
						Clubs.Sort((x, y) => -1 * x.Plaats.CompareTo(y.Plaats));
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

		void LoadClubs(SqliteConnection conn)
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
					command.CommandText = "SELECT DISTINCT ID FROM [Club]";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var club = new ClubModel();
							var id = (string)reader["ID"];

							club.Load(conn, id);

							//AddPersoon(persoon);
							Clubs.Add(club);
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

		public bool AddClub(ClubModel _club)
		{
			Clubs.Add(_club);

			return true;
		}
	}
}

