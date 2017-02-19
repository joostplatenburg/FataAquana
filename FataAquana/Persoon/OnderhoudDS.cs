using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class OnderhoudDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<InOnderhoudModel> Onderhoud { get; set; } = new List<InOnderhoudModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public OnderhoudDS(SqliteConnection conn, PersoonModel _persoon)
		{
			Conn = conn;

			if (AppDelegate.Conn != null && _persoon != null)
			{
				LoadOnderhoud(conn, _persoon.ID);
			}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Onderhoud.Count;
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
						Onderhoud.Sort((x, y) => x.ApparaatNaam.CompareTo(y.ApparaatNaam));
					}
					else {
						Onderhoud.Sort((x, y) => -1 * x.ApparaatNaam.CompareTo(y.ApparaatNaam));
					}
					break;
				case "OntvangenOp":
					if (ascending)
					{
						Onderhoud.Sort((x, y) => ((int)x.OntvangenOp.Compare(y.OntvangenOp)));
					}
					else
					{
						Onderhoud.Sort((x, y) => -1 * ((int)x.OntvangenOp.Compare(y.OntvangenOp)));
					}
					break;
				case "RetourOp":
					if (ascending)
					{
						Onderhoud.Sort((x, y) => ((int)x.RetourOp.Compare(y.RetourOp)));
					}
					else
					{
						Onderhoud.Sort((x, y) => -1 * ((int)x.RetourOp.Compare(y.RetourOp)));
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

		void LoadOnderhoud(SqliteConnection conn, string PersoonID)
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
					command.CommandText = "SELECT DISTINCT ID FROM [InOnderhoud] WHERE PersoonID = '" + PersoonID + "'";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var onderhoud = new InOnderhoudModel();
							var id = (string)reader["ID"];

							onderhoud.Load(conn, id);

							Onderhoud.Add(onderhoud);
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

