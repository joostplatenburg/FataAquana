using System;
using System.Data;
using System.Diagnostics;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("GevolgdeOpleiding")]
	public class GevolgdeOpleidingModel : NSObject
	{
		#region Private Variables
		private string _ID = string.Empty;
		private string _persoonID = string.Empty;
		private string _opleidingID = string.Empty;
		private string _opleidingNaam = string.Empty;
		private NSDate _geslaagdOp = new NSDate();
		private NSDate _verlopenOp = new NSDate();

		private SqliteConnection _conn = null;
		#endregion

		#region Computed Properties
		public SqliteConnection Conn
		{
			get { return _conn; }
			set { _conn = value; }
		}

		[Export("ID")]
		public string ID
		{
			get { return _ID; }
			set
			{
				WillChangeValue("ID");
				_ID = value;
				DidChangeValue("ID");
			}
		}

		[Export("PersoonID")]
		public string PersoonID
		{
			get { return _persoonID; }
			set
			{
				WillChangeValue("PersoonID");
				_persoonID = value;
				DidChangeValue("PersoonID");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("OpleidingID")]
		public string OpleidingID
		{
			get { return _opleidingID; }
			set
			{
				WillChangeValue("OpleidingID");
				_opleidingID = value;

				var curOpleiding = new OpleidingModel();
				curOpleiding.Load(AppDelegate.Conn, _opleidingID);
				_opleidingNaam = curOpleiding.OpleidingNaam;

				DidChangeValue("OpleidingID");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("OpleidingNaam")]
		public string OpleidingNaam
		{
			get { return _opleidingNaam; }
			set
			{
				WillChangeValue("OpleidingNaam");
				_opleidingNaam = value;
				DidChangeValue("OpleidingNaam");
			}
		}

		[Export("GeslaagdOp")]
		public NSDate GeslaagdOp
		{
			get { return _geslaagdOp; }
			set
			{
				WillChangeValue("GeslaagdOp");
				_geslaagdOp = value;
				DidChangeValue("GeslaagdOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("VerlopenOp")]
		public NSDate VerlopenOp
		{
			get { return _verlopenOp; }
			set
			{
				WillChangeValue("VerlopenOp");
				_verlopenOp = value;
				DidChangeValue("VerlopenOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}
		#endregion

		#region Constructors
		public GevolgdeOpleidingModel()
		{
		}

		public GevolgdeOpleidingModel(NSDate geslaagdop, NSDate verlopenop, string persoonid, string opleidingid)
		{
			GeslaagdOp = geslaagdop;
			VerlopenOp = verlopenop;
			PersoonID = persoonid;
			OpleidingID = opleidingid;
		}
		#endregion

		#region SQLite Routines
		public void Create(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Create new record ID?
			if (ID == "")
			{
				ID = Guid.NewGuid().ToString();
			}

			// Execute query
			if (conn.State != ConnectionState.Open) { conn.Open(); }
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "INSERT INTO [GevolgdeOpleiding] (ID, PersoonID, OpleidingID, GeslaagdOp, VerlopenOp) VALUES (@COL1, @COL2, @COL3, @COL4, @COL5)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", OpleidingID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(GeslaagdOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(VerlopenOp));

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;
		}

		public void Update(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Execute query
			if (conn.State != ConnectionState.Open) { conn.Open(); }
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "UPDATE [GevolgdeOpleiding] SET PersoonID = @COL2, OpleidingID = @COL3, GeslaagdOp = @COL4, VerlopenOp = @COL5 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", OpleidingID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(GeslaagdOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(VerlopenOp));

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;
		}

		public void Load(SqliteConnection conn, string id)
		{
			Debug.WriteLine("GevolgdeOpleidingModel: " + id);

			bool shouldClose = false;

			// clear last connection to preventcirculair call to update
			_conn = null;

			// Is the database already open?
			if (conn.State != ConnectionState.Open)
			{
				shouldClose = true;
				conn.Open();
			}

			// Execute query
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "SELECT GevolgdeOpleiding.ID, PersoonID, OpleidingID, GeslaagdOp, VerlopenOp, OpleidingNaam " +
					"FROM GevolgdeOpleiding LEFT JOIN Opleiding ON Opleiding.ID = GevolgdeOpleiding.OpleidingID " +
					"WHERE GevolgdeOpleiding.ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					try
					{
						while (reader.Read())
						{
							// Pull values back into class
							ID = (string)reader[0];
							PersoonID = (string)reader[1];
							OpleidingID = (string)reader[2];
							GeslaagdOp = AppDelegate.DateTimeToNSDate((DateTime)reader[3]);
							VerlopenOp = AppDelegate.DateTimeToNSDate((DateTime)reader[4]);
							OpleidingNaam = (string)reader[5];
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine("ERROR: " + ex.Message);
					}
				}
			}

			if (shouldClose)
			{
				conn.Close();
			}

			// Save last connection
			_conn = conn;
		}

		public void Delete(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Execute query
			if (conn.State != ConnectionState.Open) { conn.Open(); }
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "DELETE FROM [GevolgdeOpleiding] WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			PersoonID = "";
			OpleidingID = "";
			GeslaagdOp = new NSDate();
			VerlopenOp = new NSDate();

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}