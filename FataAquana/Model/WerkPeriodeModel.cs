using System;
using System.Data;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("WerkPeriode")]
	public class WerkPeriodeModel : NSObject
	{
		#region Private Variables
		private string _ID = string.Empty;
		private string _persoonID = string.Empty;
		private string _bedrijfID = string.Empty;
		private string _bedrijfNaam = string.Empty;
		private NSDate _gestartOp = new NSDate();
		private string _gestartOpText = string.Empty;
		private NSDate _gestoptOp = new NSDate();
		private string _gestoptOpText = string.Empty;

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

		[Export("BedrijfID")]
		public string BedrijfID
		{
			get { return _bedrijfID; }
			set
			{
				WillChangeValue("BedrijfID");
				_bedrijfID = value;
				DidChangeValue("BedrijfID");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("BedrijfNaam")]
		public string BedrijfNaam
		{
			get { return _bedrijfNaam; }
			set
			{
				WillChangeValue("BedrijfNaam");
				_bedrijfNaam = value;
				DidChangeValue("BedrijfNaam");
			}
		}

		[Export("GestartOp")]
		public NSDate GestartOp
		{
			get { return _gestartOp; }
			set
			{
				WillChangeValue("GestartOp");
				_gestartOp = value;
				DidChangeValue("GestartOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("GestartOpText")]
		public string GestartOpText
		{
			get { return _gestartOpText; }
			set
			{
				WillChangeValue("GestartOpText");
				_gestartOpText = value;
				DidChangeValue("GestartOpText");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("GestoptOp")]
		public NSDate GestoptOp
		{
			get { return _gestoptOp; }
			set
			{
				WillChangeValue("GestoptOp");
				_gestoptOp = value;
				DidChangeValue("GestoptOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("GestoptOpText")]
		public string GestoptOpText
		{
			get { return _gestoptOpText; }
			set
			{
				WillChangeValue("GestoptOpText");
				_gestoptOpText = value;
				DidChangeValue("GestoptOpText");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}
		#endregion

		#region Constructors
		public WerkPeriodeModel()
		{
		}

		public WerkPeriodeModel(NSDate gestartop, NSDate gestoptop, string persoonid, string bedrijfid)
		{
			GestartOp = gestartop;
			GestoptOp = gestoptop;
			PersoonID = persoonid;
			BedrijfID = bedrijfid;
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
				command.CommandText = "INSERT INTO [WerkPeriode] (ID, PersoonID, BedrijfID, GestartOp, GestoptOp) VALUES (@COL1, @COL2, @COL3, @COL4, @COL5)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", BedrijfID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(GestartOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(GestoptOp));

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
				command.CommandText = "UPDATE [WerkPeriode] SET PersoonID = @COL2, BedrijfID = @COL3, GestartOp = @COL4, GestoptOp = @COL5 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", BedrijfID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(GestartOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(GestoptOp));

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;
		}

		public void Load(SqliteConnection conn, string id)
		{
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
				command.CommandText = "SELECT WerkPeriode.ID, PersoonID, BedrijfID, GestartOp, GestoptOp, BedrijfNaam " +
					"FROM WerkPeriode LEFT JOIN Bedrijf ON Bedrijf.ID = WerkPeriode.BedrijfID " +
					"WHERE WerkPeriode.ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader[0];
						PersoonID = (string)reader[1];
						BedrijfID = (string)reader[2];
						GestartOp = AppDelegate.DateTimeToNSDate((DateTime)reader[3]);
						GestoptOp = AppDelegate.DateTimeToNSDate((DateTime)reader[4]);
						BedrijfNaam = (string)reader[5];
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
				command.CommandText = "DELETE FROM [WerkPeriode] WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			PersoonID = "";
			BedrijfID = "";
			GestartOp = new NSDate();
			GestoptOp = new NSDate();

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}
