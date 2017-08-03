using System;
using System.Data;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("Aankoop")]
	public class AankoopModel : NSObject
	{
		#region Private Variables
		private string _ID = string.Empty;
		private string _apparaatNaam = string.Empty;
		private string _persoonID = string.Empty;
		private string _apparaatID = string.Empty;
		private NSDate _gekochtOp = new NSDate();

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
                if (_ID == value) return;

				WillChangeValue("ID");
				_ID = value;
				DidChangeValue("ID");
			}
		}

		[Export("ApparaatNaam")]
		public string ApparaatNaam
		{
			get { return _apparaatNaam; }
			set
			{
                if (_apparaatNaam == value) return;

				WillChangeValue("ApparaatNaam");
				_apparaatNaam = value;
				DidChangeValue("ApparaatNaam");
			}
		}

		[Export("PersoonID")]
		public string PersoonID
		{
			get { return _persoonID; }
			set
			{
                if (_persoonID == value) return;

				WillChangeValue("PersoonID");
				_persoonID = value;
				DidChangeValue("PersoonID");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("ApparaatID")]
		public string ApparaatID
		{
			get { return _apparaatID; }
			set
			{
                if (_apparaatID == value) return;

				WillChangeValue("ApparaatID");
				_apparaatID = value;
				DidChangeValue("ApparaatID");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("GekochtOp")]
		public NSDate GekochtOp
		{
			get { return _gekochtOp; }
			set
			{
                if (_gekochtOp == value) return;

				WillChangeValue("GekochtOp");
				_gekochtOp = value;
				DidChangeValue("GekochtOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}
		#endregion

		#region Constructors
		public AankoopModel()
		{
		}

		public AankoopModel(NSDate datum, string persoonid, string apparaatid)
		{
			GekochtOp = datum;
			PersoonID = persoonid;
			ApparaatID = apparaatid;
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
				command.CommandText = "INSERT INTO [Aankoop] (ID, PersoonID, ApparaatID, GekochtOp) VALUES (@COL1, @COL2, @COL3, @COL4)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", ApparaatID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(GekochtOp));

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
				command.CommandText = "UPDATE [Aankoop] SET PersoonID = @COL2, ApparaatID = @COL3, GekochtOp = @COL4 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", ApparaatID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(GekochtOp));

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
				command.CommandText = "SELECT Aankoop.ID, PersoonID, ApparaatID, GekochtOp, ApparaatNaam " +
					"FROM Aankoop LEFT JOIN Apparaat ON Apparaat.ID = Aankoop.ApparaatID " +
					"WHERE Aankoop.ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					while(reader.Read())
					{
						// Pull values back into class
						ID = reader[0] as string;
						PersoonID = reader[1] as string;
						ApparaatID = reader[2] as string;
                        try { GekochtOp = AppDelegate.DateTimeToNSDate((DateTime)reader[3]); }
                        catch { }
						ApparaatNaam = reader[4] as string;
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
				command.CommandText = "DELETE FROM [Aankoop] WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			PersoonID = "";
			ApparaatID = "";
			GekochtOp = new NSDate();

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}

