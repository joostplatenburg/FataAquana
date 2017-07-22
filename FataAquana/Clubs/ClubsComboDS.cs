using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class ClubsComboDS : NSComboBoxDataSource
	{
		public List<ClubModel> Clubs { get; set; } = new List<ClubModel>();

		public ClubsComboDS()
		{
			LoadClubs(AppDelegate.Conn);
		}

		public override string CompletedString(NSComboBox comboBox, string uncompletedString)
		{
			foreach (ClubModel club in Clubs)
			{
				if (club.ClubNaam.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase))
					return club.ClubNaam;
			}
			return uncompletedString;
		}

		public override nint IndexOfItem(NSComboBox comboBox, string value)
		{
			nint index = 0;

			foreach (ClubModel club in Clubs)
			{
				if (club.ClubNaam.Equals(value, StringComparison.InvariantCultureIgnoreCase))
					return index;

				index++;
			}

			return index;
		}

		public override nint ItemCount(NSComboBox comboBox)
		{
			return Clubs.Count;
		}

		public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
		{
			if (index >= 0)
			{
				return NSObject.FromObject(Clubs[(int)index].ClubNaam);
			}
			else
			{
				return null;
			}
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
	}
}
