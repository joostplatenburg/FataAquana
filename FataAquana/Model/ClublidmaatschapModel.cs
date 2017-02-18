using System;
using System.Data;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("Clublidmaatschap")]
	public class ClublidmaatschapModel : NSObject
	{
		#region Private Variables
		private string _ID = "";
		private string _persoonID = "";
		private string _clubID = "";
		private string _clubNaam = "";
		private NSDate _ingeschrevenOp = new NSDate();
		private NSDate _uitgeschrevenOp = new NSDate();

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

		[Export("ClubID")]
		public string ClubID
		{
			get { return _clubID; }
			set
			{
				WillChangeValue("ClubID");
				_clubID = value;
				DidChangeValue("ClubID");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("ClubNaam")]
		public string ClubNaam
		{
			get { return _clubNaam; }
			set
			{
				WillChangeValue("ClubNaam");
				_clubNaam = value;
				DidChangeValue("ClubNaam");
			}
		}

		[Export("IngeschrevenOp")]
		public NSDate IngeschrevenOp
		{
			get { return _ingeschrevenOp; }
			set
			{
				WillChangeValue("IngeschrevenOp");
				_ingeschrevenOp = value;
				DidChangeValue("IngeschrevenOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("UitgeschrevenOp")]
		public NSDate UitgeschrevenOp
		{
			get { return _uitgeschrevenOp; }
			set
			{
				WillChangeValue("UitgeschrevenOp");
				_uitgeschrevenOp = value;
				DidChangeValue("UitgeschrevenOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}
		#endregion

		#region Constructors
		public ClublidmaatschapModel()
		{
		}

		public ClublidmaatschapModel(NSDate ingeschrevenop, NSDate uitgeschrevenop, string persoonid, string clubid)
		{
			IngeschrevenOp = ingeschrevenop;
			UitgeschrevenOp = uitgeschrevenop;
			PersoonID = persoonid;
			ClubID = clubid;
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
			conn.Open();
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "INSERT INTO [ClubLidmaatschap] (ID, PersoonID, ClubID, IngeschrevenOp, UitgeschrevenOp) VALUES (@COL1, @COL2, @COL3, @COL4, @COL5)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", ClubID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(IngeschrevenOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(UitgeschrevenOp));

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;

			Load(_conn, ID);
		}

		public void Update(SqliteConnection conn)
		{
			// clear last connection to preventcirculair call to update
			_conn = null;

			// Execute query
			conn.Open();
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "UPDATE [ClubLidmaatschap] SET PersoonID = @COL2, ClubID = @COL3, IngeschrevenOp = @COL4, UitgeschrevenOp = @COL5 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", ClubID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(IngeschrevenOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(UitgeschrevenOp));

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
				command.CommandText = "SELECT ClubLidmaatschap.ID, PersoonID, ClubID, IngeschrevenOp, UitgeschrevenOp, ClubNaam " +
					"FROM ClubLidmaatschap LEFT JOIN Club ON Club.ID = ClubLidmaatschap.ClubID " +
					"WHERE ClubLidmaatschap.ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader[0];
						PersoonID = (string)reader[1];
						ClubID = (string)reader[2];
						IngeschrevenOp = AppDelegate.DateTimeToNSDate((DateTime)reader[3]);
						UitgeschrevenOp = AppDelegate.DateTimeToNSDate((DateTime)reader[4]);
						ClubNaam = (string)reader[5];
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
			conn.Open();
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "DELETE FROM [ClubLidmaatschap] WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			PersoonID = "";
			ClubID = "";
			IngeschrevenOp = new NSDate();
			UitgeschrevenOp = new NSDate();

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}
