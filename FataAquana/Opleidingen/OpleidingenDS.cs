using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class OpleidingenDS : NSTableViewDataSource
	{
		#region Computed Properties
		public List<OpleidingModel> Opleidingen { get; set; } = new List<OpleidingModel>();

		public SqliteConnection Conn { get; set; }
		#endregion

		#region Constructors
		public OpleidingenDS(SqliteConnection conn)
		{
			Conn = conn;

			if (AppDelegate.Conn != null)
			{
				LoadOpleidingen(conn);
			}

			//if (AppDelegate.MainView != null)
			//{
			//	for (nuint i = 0; i < AppDelegate.MainView.Personen.Count; i++)
			//	{
			//		var persoon = (PersoonModel)AppDelegate.MainView.Personen.GetItem<NSObject>(i);
			//		Personen.Add(persoon);
			//	}
			//}
		}
		#endregion

		#region Override Methods
		public override nint GetRowCount(NSTableView tableView)
		{
			return (nint)Opleidingen.Count;
		}
		#endregion

		public void Sort(string key, bool ascending)
		{

			// Take action based on key
			switch (key)
			{
				case "Opleidingnaam":
					if (ascending)
					{
						Opleidingen.Sort((x, y) => string.Compare(x.OpleidingNaam, y.OpleidingNaam, StringComparison.CurrentCulture));
					}
					else {
						Opleidingen.Sort((x, y) => string.Compare(y.OpleidingNaam, x.OpleidingNaam, StringComparison.CurrentCulture));
					}
					break;
				case "Omschrijving":
					if (ascending)
					{
						Opleidingen.Sort((x, y) => string.Compare(x.Omschrijving, y.Omschrijving, StringComparison.CurrentCulture));
					}
					else {
						Opleidingen.Sort((x, y) => string.Compare(y.Omschrijving, x.Omschrijving, StringComparison.CurrentCulture));
					}
					break;
				//case "UitgeschrevenOp":
				//	if (ascending)
				//	{
				//		Lidmaatschappen.Sort((x, y) => x.UitgeschrevenOp.CompareTo(y.UitgeschrevenOp));
				//	}
				//	else {
				//		Lidmaatschappen.Sort((x, y) => -1 * x.UitgeschrevenOp.CompareTo(y.UitgeschrevenOp));
				//	}
				//	break;
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

		void LoadOpleidingen(SqliteConnection conn)
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
					command.CommandText = "SELECT DISTINCT ID FROM [Opleiding]";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var opleiding = new OpleidingModel();
							var id = (string)reader["ID"];

							opleiding.Load(conn, id);

							//AddPersoon(persoon);
							Opleidingen.Add(opleiding);
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

		public bool AddOpleiding(OpleidingModel _opleiding)
		{
			Opleidingen.Add(_opleiding);

			return true;
		}
	}
}

