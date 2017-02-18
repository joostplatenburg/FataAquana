using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class GevolgdeOpleidingenDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<GevolgdeOpleidingModel> GevolgdeOpleidingen { get; set; } = new List<GevolgdeOpleidingModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public GevolgdeOpleidingenDS(SqliteConnection conn, PersoonModel _persoon)
		{
			Conn = conn;

			if (AppDelegate.Conn != null && _persoon != null)
			{
				LoadGevolgdeOpleidingen(conn, _persoon.ID);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)GevolgdeOpleidingen.Count;
		}
		#endregion

		public void Sort(string key, bool ascending)
		{

			// Take action based on key
			switch (key)
			{
				case "OpleidingNaam":
					if (ascending)
					{
						GevolgdeOpleidingen.Sort((x, y) => x.OpleidingNaam.CompareTo(y.OpleidingNaam));
					}
					else {
						GevolgdeOpleidingen.Sort((x, y) => -1 * x.OpleidingNaam.CompareTo(y.OpleidingNaam));
					}
					break;
				case "GeslaagdOp":
					if (ascending)
					{
						GevolgdeOpleidingen.Sort((x, y) => ((int)x.GeslaagdOp.Compare(y.GeslaagdOp)));
					}
					else
					{
						GevolgdeOpleidingen.Sort((x, y) => -1 * ((int)x.GeslaagdOp.Compare(y.GeslaagdOp)));
					}
					break;
				case "VerlopenOp":
					if (ascending)
					{
						GevolgdeOpleidingen.Sort((x, y) => ((int)x.VerlopenOp.Compare(y.VerlopenOp)));
					}
					else
					{
						GevolgdeOpleidingen.Sort((x, y) => -1 * ((int)x.VerlopenOp.Compare(y.VerlopenOp)));
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

		void LoadGevolgdeOpleidingen(SqliteConnection conn, string PersoonID)
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
					command.CommandText = "SELECT DISTINCT ID FROM [GevolgdeOpleiding] WHERE PersoonID = '" + PersoonID + "'";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var gevolgdeopleiding = new GevolgdeOpleidingModel();
							var id = (string)reader["ID"];

							gevolgdeopleiding.Load(conn, id);

							GevolgdeOpleidingen.Add(gevolgdeopleiding);
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

