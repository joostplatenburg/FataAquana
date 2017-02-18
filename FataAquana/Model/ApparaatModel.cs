using System;
using System.Data;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("Apparaat")]
	public class ApparaatModel : NSObject
	{
		#region private Properties
		private string _ID = "";
		private string _apparaatnaam = "";
		private string _omschrijving = "";

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

		[Export("ApparaatNaam")]
		public string ApparaatNaam
		{
			get { return _apparaatnaam; }
			set
			{
				WillChangeValue("ApparaatNaam");
				_apparaatnaam = value;
				DidChangeValue("ApparaatNaam");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Omschrijving")]
		public string Omschrijving
		{
			get { return _omschrijving; }
			set
			{
				WillChangeValue("Omschrijving");
				_omschrijving = value;
				DidChangeValue("Omschrijving");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		#endregion

		#region Constructors
		public ApparaatModel()
		{
		}

		public ApparaatModel(string apparaatnaam)
		{
			_apparaatnaam = apparaatnaam;
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
				command.CommandText = "INSERT INTO Apparaat (ID, ApparaatNaam) VALUES (@COL1, @COL2)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", ApparaatNaam);

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
			conn.Open();
			using (var command = conn.CreateCommand())
			{
				// Create new command
				command.CommandText = "UPDATE Apparaat SET ApparaatNaam = @COL2 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", ApparaatNaam);

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
				command.CommandText = "SELECT * FROM Apparaat WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader[0];
						ApparaatNaam = (string)reader[1];
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
				command.CommandText = "DELETE FROM Apparaat WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			ApparaatNaam = "";

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}

