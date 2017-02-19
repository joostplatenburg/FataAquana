using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class ApparatenComboDS : NSComboBoxDataSource
	{
		public List<ApparaatModel> Apparaten { get; set; } = new List<ApparaatModel>();

		public ApparatenComboDS()
		{
			LoadApparaten(AppDelegate.Conn);
		}

		public override string CompletedString(NSComboBox comboBox, string uncompletedString)
		{
			foreach (ApparaatModel app in Apparaten)
			{
				if (app.ApparaatNaam.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase))
					return app.ApparaatNaam;
			}
			return uncompletedString;
		}

		public override nint IndexOfItem(NSComboBox comboBox, string value)
		{
			nint index = 0;

			foreach (ApparaatModel app in Apparaten)
			{
				if (app.ApparaatNaam.Equals(value, StringComparison.InvariantCultureIgnoreCase))
					return index;

				index++;
			}

			return index;
		}

		public override nint ItemCount(NSComboBox comboBox)
		{
			return Apparaten.Count;
		}

		public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
		{
			return NSObject.FromObject(Apparaten[(int)index].ApparaatNaam);
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
	}
}
