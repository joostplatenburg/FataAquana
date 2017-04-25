using System;
using System.Data;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("Bedrijf")]
	public class BedrijfModel : NSObject
	{
		#region Private Variables
		private string _id = "";
		private string _bedrijfnaam = "";
		private string _adres = "";
		private string _postcode = "";
		private string _plaats = "";

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
			get { return _id; }
			set
			{
				WillChangeValue("ID");
				_id = value;
				DidChangeValue("ID");
			}
		}

		[Export("BedrijfNaam")]
		public string BedrijfNaam { 
			get { return _bedrijfnaam; } 
			set { 
				WillChangeValue("BedrijfNaam");
				_bedrijfnaam = value;
				DidChangeValue("BedrijfNaam");
			} 
		}

		[Export("Adres")]
		public string Adres
		{
			get { return _adres; }
			set
			{
				WillChangeValue("Adres");
				_adres = value;
				DidChangeValue("Adres");
			}
		}

		[Export("Postcode")]
		public string Postcode
		{
			get { return _postcode; }
			set
			{
				WillChangeValue("Postcode");
				_postcode = value;
				DidChangeValue("Postcode");
			}
		}

		[Export("Plaats")]
		public string Plaats
		{
			get { return _plaats; }
			set
			{
				WillChangeValue("Plaats");
				_plaats = value;
				DidChangeValue("Plaats");
			}
		}
		#endregion

		#region Constructors
		public BedrijfModel()
		{
		}

		public BedrijfModel(string bedrijfnaam, string adres, string postcode, string plaats)
		{
			BedrijfNaam = bedrijfnaam;
			Adres = adres;
			Postcode = postcode;
			Plaats = plaats;
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
				command.CommandText = "INSERT INTO Bedrijf (ID, BedrijfNaam, Adres, Postcode, Plaats) VALUES (@COL1, @COL2, @COL3, @COL4, @COL5)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", BedrijfNaam);
				command.Parameters.AddWithValue("@COL3", Adres);
				command.Parameters.AddWithValue("@COL4", Postcode);
				command.Parameters.AddWithValue("@COL5", Plaats);

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
				command.CommandText = "UPDATE Bedrijf SET BedrijfNaam = @COL2, Adres = @COL3, Postcode = @COL4, Plaats = @COL5 WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", BedrijfNaam);
				command.Parameters.AddWithValue("@COL3", Adres);
				command.Parameters.AddWithValue("@COL4", Postcode);
				command.Parameters.AddWithValue("@COL5", Plaats);

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
				command.CommandText = "SELECT ID, BedrijfNaam, Adres, Postcode, Plaats FROM Bedrijf WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader[0];
						BedrijfNaam = (string)reader[1];
						Adres = (string)reader[2];
						Postcode = (string)reader[3];
						Plaats = (string)reader[4];
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
				command.CommandText = "DELETE FROM Bedrijf WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			BedrijfNaam = "";
			Adres = "";
			Postcode = "";
			Plaats = "";

			// Save last connection
			_conn = conn;
		}
		#endregion
	}
}

