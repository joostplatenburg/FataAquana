using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	public class OpleidingenComboDS : NSComboBoxDataSource
	{
		public List<OpleidingModel> Opleidingen { get; set; } = new List<OpleidingModel>();

		public OpleidingenComboDS()
		{
			LoadOpleidingen(AppDelegate.Conn);
		}

		public override string CompletedString(NSComboBox comboBox, string uncompletedString)
		{
			foreach (OpleidingModel om in Opleidingen)
			{
				if (om.OpleidingNaam.StartsWith(uncompletedString, StringComparison.InvariantCultureIgnoreCase))
					return om.OpleidingNaam;
			}
			return uncompletedString;
		}

		public override nint IndexOfItem(NSComboBox comboBox, string value)
		{
			nint index = 0;

			foreach (OpleidingModel om in Opleidingen)
			{
				if (om.OpleidingNaam.Equals(value, StringComparison.InvariantCultureIgnoreCase))
					return index;

				index++;
			}

			return index;
		}

		public override nint ItemCount(NSComboBox comboBox)
		{
			return Opleidingen.Count;
		}

		public override NSObject ObjectValueForItem(NSComboBox comboBox, nint index)
		{
			if (index >= 0)
			{
				return NSObject.FromObject(Opleidingen[(int)index].OpleidingNaam);
			}
			else
			{
				return null;
			}
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
	}
}
