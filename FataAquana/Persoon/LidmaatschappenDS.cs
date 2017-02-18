using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class LidmaatschappenDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<ClublidmaatschapModel> Lidmaatschappen { get; set; } = new List<ClublidmaatschapModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public LidmaatschappenDS(SqliteConnection conn, PersoonModel _persoon)
		{
			Conn = conn;

			if (AppDelegate.Conn != null && _persoon != null)
			{
				LoadLidmaatschappen(conn, _persoon.ID);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Lidmaatschappen.Count;
		}
		#endregion

		public void Sort(string key, bool ascending)
		{

			// Take action based on key
			switch (key)
			{
				case "ClubNaam":
					if (ascending)
					{
						Lidmaatschappen.Sort((x, y) => x.ClubNaam.CompareTo(y.ClubNaam));
					}
					else {
						Lidmaatschappen.Sort((x, y) => -1 * x.ClubNaam.CompareTo(y.ClubNaam));
					}
					break;
				case "IngeschrevenOp":
					if (ascending)
					{
						Lidmaatschappen.Sort((x, y) => ((int)x.IngeschrevenOp.Compare(y.IngeschrevenOp)));
					}
					else
					{
						Lidmaatschappen.Sort((x, y) => -1 * ((int)x.IngeschrevenOp.Compare(y.IngeschrevenOp)));
					}
					break;
				case "UitgeschrevenOp":
					if (ascending)
					{
						Lidmaatschappen.Sort((x, y) => ((int)x.UitgeschrevenOp.Compare(y.UitgeschrevenOp)));
					}
					else
					{
						Lidmaatschappen.Sort((x, y) => -1 * ((int)x.UitgeschrevenOp.Compare(y.UitgeschrevenOp)));
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

		void LoadLidmaatschappen(SqliteConnection conn, string PersoonID)
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
					command.CommandText = "SELECT DISTINCT ID FROM [Clublidmaatschap] WHERE PersoonID = '" + PersoonID + "'";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var lidmaatschap = new ClublidmaatschapModel();
							var id = (string)reader["ID"];

							lidmaatschap.Load(conn, id);

							Lidmaatschappen.Add(lidmaatschap);
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

