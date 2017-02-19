using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class AankopenDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<AankoopModel> Aankopen { get; set; } = new List<AankoopModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public AankopenDS(SqliteConnection conn, PersoonModel _persoon)
		{
			Conn = conn;

			if (AppDelegate.Conn != null && _persoon != null)
			{
				LoadAankopen(conn, _persoon.ID);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Aankopen.Count;
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
						Aankopen.Sort((x, y) => x.ApparaatNaam.CompareTo(y.ApparaatNaam));
					}
					else {
						Aankopen.Sort((x, y) => -1 * x.ApparaatNaam.CompareTo(y.ApparaatNaam));
					}
					break;
				case "GekochtOp":
					if (ascending)
					{
						Aankopen.Sort((x, y) => ((int)x.GekochtOp.Compare(y.GekochtOp)));
					}
					else
					{
						Aankopen.Sort((x, y) => -1 * ((int)x.GekochtOp.Compare(y.GekochtOp)));
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

		void LoadAankopen(SqliteConnection conn, string PersoonID)
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
					command.CommandText = "SELECT DISTINCT ID FROM [Aankoop] WHERE PersoonID = '" + PersoonID + "'";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var aankoop = new AankoopModel();
							var id = (string)reader["ID"];

							aankoop.Load(conn, id);

							Aankopen.Add(aankoop);
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
	}
}

