using System;
using System.Data;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("InOnderhoud")]
	public class InOnderhoudModel : NSObject
	{
		#region Private Variables
		private string _ID = string.Empty;
		private string _apparaatNaam = string.Empty;
		private string _persoonID = string.Empty;
		private string _apparaatID = string.Empty;
		private NSDate _ontvangenOp = new NSDate();
		private NSDate _retourOp = new NSDate();

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

		[Export("OntvangenOp")]
		public NSDate OntvangenOp
		{
			get { return _ontvangenOp; }
			set
			{
				if (_ontvangenOp == value) return;

				WillChangeValue("OntvangenOp");
				_ontvangenOp = value;
				DidChangeValue("OntvangenOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("RetourOp")]
		public NSDate RetourOp
		{
			get { return _retourOp; }
			set
			{
				if (_retourOp == value) return;

				WillChangeValue("RetourOp");
				_retourOp = value;
				DidChangeValue("RetourOp");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}
		#endregion

		#region Constructors
		public InOnderhoudModel()
		{
		}

		public InOnderhoudModel(NSDate ontvangenop, NSDate retourop, string persoonid, string apparaatid)
		{
			OntvangenOp = ontvangenop;
			RetourOp = retourop;
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
				command.CommandText = "INSERT INTO InOnderhoud (ID, PersoonID, ApparaatID, OntvangenOp, RetourOp) VALUES (@COL1, @COL2, @COL3, @COL4, @COL5)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", ApparaatID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(OntvangenOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(RetourOp));

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
				command.CommandText = "UPDATE InOnderhoud SET PersoonID = @COL2, ApparaatID = @COL3, OntvangenOp = @COL4, RetourOp = @COL5 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", PersoonID);
				command.Parameters.AddWithValue("@COL3", ApparaatID);
				command.Parameters.AddWithValue("@COL4", AppDelegate.NSDateToDateTime(OntvangenOp));
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(RetourOp));

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
				command.CommandText = "SELECT InOnderhoud.ID, PersoonID, ApparaatID, OntvangenOp, RetourOp, ApparaatNaam " +
					"FROM InOnderhoud LEFT JOIN Apparaat ON Apparaat.ID = InOnderhoud.ApparaatID " +
					"WHERE InOnderhoud.ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader[0];
						PersoonID = (string)reader[1];
						ApparaatID = (string)reader[2];
                        try { OntvangenOp = AppDelegate.DateTimeToNSDate((DateTime)reader[3]); }
                        catch { }
                        try { RetourOp = AppDelegate.DateTimeToNSDate((DateTime)reader[4]); }
                        catch { }
						ApparaatNaam = (string)reader[5];
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
				command.CommandText = "DELETE FROM InOnderhoud WHERE ID = @COL1";

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
			OntvangenOp = new NSDate();
			RetourOp = new NSDate();

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}