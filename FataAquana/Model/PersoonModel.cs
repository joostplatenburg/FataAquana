using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using Foundation;
using AppKit;
using System.Collections.Generic;
using System.Diagnostics;

namespace FataAquana
{
	public class PersonenModel : List<PersoonModel>
	{
		
	}

	[Register("Persoon")]
	public class PersoonModel : NSObject
	{
		#region Private Variables
		private string _ID = "";
		private string _achternaam = "";
		private string _voornamen = "";
		private string _tussenvoegsel = "";
		private string _initialen = "";

		private string _adres = "";
		private string _postcode = "";
		private string _woonplaats = "";
		private string _telefoon = "";
		private string _mobiel = "";
		private string _email = "";
		private NSDate _geboortedatum = new NSDate();
		private string _imagepath = "";

		private NSMutableArray _gevolgdeopleidingen = new NSMutableArray();
		private string _gevolgdeopleidingenstring = "";
		private NSMutableArray _aankopen = new NSMutableArray();
		private string _aankopenstring = "";
		private NSMutableArray _inonderhoud = new NSMutableArray();
		private string _inonderhoudstring = "";
		private NSMutableArray _lidmaatschappen = new NSMutableArray();
		private string _lidmaatschappenstring = "";

		private SqliteConnection _conn = null;
		#endregion

		#region Computed Properties
		public SqliteConnection Conn {
			get { return _conn; }
			set { _conn = value; }
		}

		[Export("ID")]
		public string ID {
			get { return _ID; }
			set
			{
				WillChangeValue("ID");
				_ID = value;
				DidChangeValue("ID");
			}
		}

		[Export("Achternaam")]
		public string Achternaam { 
			get { return _achternaam; }
			set
			{
				WillChangeValue("Achternaam");
				_achternaam = value;
				DidChangeValue("Achternaam");

				// Save changes to database
				if (_conn != null) Update(_conn);
			} 
		}

		[Export("Adres")]
		public string Adres {
			get { return _adres; }
			set
			{
				WillChangeValue("Adres");
				_adres = value;
				DidChangeValue("Adres");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Geboortedatum")]
		public NSDate Geboortedatum {
			get { return _geboortedatum; }
			set
			{
				WillChangeValue("Geboortedatum");
				_geboortedatum = value;
				DidChangeValue("Geboortedatum");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Imagepath")]
		public string Imagepath {
			get { return _imagepath; }
			set
			{
				WillChangeValue("Imagepath");
				_imagepath = value;
				DidChangeValue("Imagepath");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Initialen")]
		public string Initialen {
			get { return _initialen; }
			set
			{
				WillChangeValue("Initialen");
				_initialen = value;
				DidChangeValue("Initialen");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Postcode")]
		public string Postcode {
			get { return _postcode; }
			set
			{
				WillChangeValue("Postcode");
				_postcode = value;
				DidChangeValue("Postcode");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Tussenvoegsel")]
		public string Tussenvoegsel {
			get { return _tussenvoegsel; }
			set
			{
				WillChangeValue("Tussenvoegsel");
				_tussenvoegsel = value;
				DidChangeValue("Tussenvoegsel");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Voornamen")]
		public string Voornamen { 
			get { return _voornamen; }
			set
			{
				WillChangeValue("Voornamen");
				_voornamen = value;
				DidChangeValue("Voornamen");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Woonplaats")]
		public string Woonplaats {
			get { return _woonplaats; }
			set
			{
				WillChangeValue("Woonplaats");
				_woonplaats = value;
				DidChangeValue("Woonplaats");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Telefoon")]
		public string Telefoon {
			get { return _telefoon; }
			set
			{
				WillChangeValue("Telefoon");
				_telefoon = value;
				DidChangeValue("Telefoon");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Mobiel")]
		public string Mobiel {
			get { return _mobiel; }
			set
			{
				WillChangeValue("Mobiel");
				_mobiel = value;
				DidChangeValue("Mobiel");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("Email")]
		public string Email {
			get { return _email; }
			set
			{
				WillChangeValue("Email");
				_email = value;
				DidChangeValue("Email");

				// Save changes to database
				if (_conn != null) Update(_conn);
			}
		}

		[Export("GevolgdeOpleidingen")]
		public NSMutableArray GevolgdeOpleidingen
		{
			get { return _gevolgdeopleidingen; }
			set
			{
				WillChangeValue("GevolgdeOpleidingen");
				_gevolgdeopleidingen = value;
				DidChangeValue("GevolgdeOpleidingen");
			}
		}

		[Export("GevolgdeOpleidingenString")]
		public string GevolgdeOpleidingenString
		{
			get { return _gevolgdeopleidingenstring; }
			set {
				WillChangeValue("GevolgdeOpleidingenString");
				_gevolgdeopleidingenstring = value;
				DidChangeValue("GevolgdeOpleidingenString");
			}
		}

		[Export("Aankopen")]
		public NSMutableArray Aankopen
		{
			get { return _aankopen; }
			set
			{
				WillChangeValue("Aankopen");
				_aankopen = value;
				DidChangeValue("Aankopen");
			}
		}

		[Export("AankopenString")]
		public string AankopenString
		{
			get { return _aankopenstring; }
			set {
				WillChangeValue("AankopenString");
				_aankopenstring = value;
				DidChangeValue("AankopenString");
			}
		}

		[Export("InOnderhoud")]
		public NSMutableArray InOnderhoud
		{
			get { return _inonderhoud; }
			set
			{
				WillChangeValue("InOnderhoud");
				_inonderhoud = value;
				DidChangeValue("InOnderhoud");
			}
		}

		[Export("InOnderhoudString")]
		public string InOnderhoudString
		{
			get { return _inonderhoudstring; }
			set
			{
				WillChangeValue("InOnderhoudString");
				_inonderhoudstring = value;
				DidChangeValue("InOnderhoudString");
			}
		}

		[Export("Lidmaatschappen")]
		public NSMutableArray Lidmaatschappen
		{
			get { return _lidmaatschappen; }
			set
			{
				WillChangeValue("Lidmaatschappen");
				_lidmaatschappen = value;
				DidChangeValue("Lidmaatschappen");
			}
		}

		[Export("LidmaatschappenString")]
		public string LidmaatschappenString
		{
			get { return _lidmaatschappenstring; }
			set
			{
				WillChangeValue("LidmaatschappenString");
				_lidmaatschappenstring = value;
				DidChangeValue("LidmaatschappenString");
			}
		}
		#endregion

		#region Constructors
		public PersoonModel()
		{
		}

		public PersoonModel(string achternaam, string voornamen, string tussenvoegsel)
		{
			this.Achternaam = achternaam;
			this.Voornamen = voornamen;
			this.Tussenvoegsel = tussenvoegsel;
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
				command.CommandText = "INSERT INTO [Persoon] (ID, Achternaam, Adres, Email, " +
										"Geboortedatum, Imagepath, Initialen, Mobiel, Postcode, " +
										"Telefoon, Tussenvoegsel, Voornamen, Woonplaats) " +
									"VALUES (@COL1, @COL2, @COL3, @COL4, @COL5, @COL6, @COL7, @COL8, @COL9, @COL10, @COL11, @COL12, @COL13)";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", Achternaam);
				command.Parameters.AddWithValue("@COL3", Adres);
				command.Parameters.AddWithValue("@COL4", Email);
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(Geboortedatum));
				command.Parameters.AddWithValue("@COL6", Imagepath);
				command.Parameters.AddWithValue("@COL7", Initialen);
				command.Parameters.AddWithValue("@COL8", Mobiel);
				command.Parameters.AddWithValue("@COL9", Postcode);
				command.Parameters.AddWithValue("@COL10", Telefoon);
				command.Parameters.AddWithValue("@COL11", Tussenvoegsel);
				command.Parameters.AddWithValue("@COL12", Voornamen);
				command.Parameters.AddWithValue("@COL13", Woonplaats);

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
				command.CommandText = "UPDATE [Persoon] SET Achternaam = @COL2, Adres = @COL3, Email  = @COL4, " +
					"Geboortedatum = @COL5, Imagepath = @COL6, Initialen = @COL7, Mobiel = @COL8, Postcode = @COL9, " +
					"Telefoon = @COL10, Tussenvoegsel = @COL11, Voornamen = @COL12, Woonplaats = @COL13 WHERE ID =  @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);
				command.Parameters.AddWithValue("@COL2", Achternaam);
				command.Parameters.AddWithValue("@COL3", Adres);
				command.Parameters.AddWithValue("@COL4", Email);
				command.Parameters.AddWithValue("@COL5", AppDelegate.NSDateToDateTime(Geboortedatum));
				command.Parameters.AddWithValue("@COL6", Imagepath);
				command.Parameters.AddWithValue("@COL7", Initialen);
				command.Parameters.AddWithValue("@COL8", Mobiel);
				command.Parameters.AddWithValue("@COL9", Postcode);
				command.Parameters.AddWithValue("@COL10", Telefoon);
				command.Parameters.AddWithValue("@COL11", Tussenvoegsel);
				command.Parameters.AddWithValue("@COL12", Voornamen);
				command.Parameters.AddWithValue("@COL13", Woonplaats);

				// write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Save last connection
			_conn = conn;
		}

		public void Load(SqliteConnection conn, string id)
		{
			//Debug.WriteLine("PersoonModel.Load(" + id + ")");

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
				command.CommandText = "SELECT ID, Achternaam, Adres, Email, " +
					"Geboortedatum, Imagepath, Initialen, Mobiel, Postcode, " +
					"Telefoon, Tussenvoegsel, Voornamen, Woonplaats FROM Persoon WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", id);

				using (var reader = command.ExecuteReader())
				{
					var r = reader.HasRows;

					while (reader.Read())
					{
						// Pull values back into class
						ID = (string)reader["ID"];
						Achternaam = (string)reader["Achternaam"];
						Adres = (string)reader["Adres"];
						Email = (string)reader["Email"];
						Geboortedatum = AppDelegate.DateTimeToNSDate((DateTime)reader["Geboortedatum"]);
						Imagepath = (string)reader["Imagepath"];
						Initialen = (string)reader["Initialen"];
						Mobiel = (string)reader["Mobiel"];
						Postcode = (string)reader["Postcode"];
						Telefoon = (string)reader["Telefoon"];
						Tussenvoegsel = (string)reader["Tussenvoegsel"];
						Voornamen = (string)reader["Voornamen"];
						Woonplaats = (string)reader["Woonplaats"];

						// ook de 'gevolgde opleidingen' laden
						using (var commandGO = conn.CreateCommand())
						{
							try
							{
								// Create new command
								commandGO.CommandText = "SELECT DISTINCT ID FROM [GevolgdeOpleiding] WHERE PersoonID = '" + ID + "'";
								using (var readerGO = commandGO.ExecuteReader())
								{
									GevolgdeOpleidingenString = string.Empty;

									while (readerGO.Read())
									{
										var gevopl = new GevolgdeOpleidingModel();
										var idGO = (string)readerGO["ID"];

										gevopl.Load(conn, idGO);

										GevolgdeOpleidingenString = gevopl.OpleidingNaam + ", " + GevolgdeOpleidingenString;
										GevolgdeOpleidingen.Add(gevopl);
									}
									GevolgdeOpleidingenString = GevolgdeOpleidingenString.Substring(0, (GevolgdeOpleidingenString.Length - 2));
								}
							}
							catch (Exception Exception)
							{
								Debug.WriteLine(Exception.Message);
							}
						}

						// ook de 'aankopen' laden
						using (var commandAK = conn.CreateCommand())
						{
							try
							{
								// Create new command
								commandAK.CommandText = "SELECT DISTINCT ID FROM [Aankoop] WHERE PersoonID = '" + ID + "'";
								using (var readerAK = commandAK.ExecuteReader())
								{
									AankopenString = string.Empty;

									while (readerAK.Read())
									{
										var aankoop = new AankoopModel();
										var idAK = (string)readerAK["ID"];

										aankoop.Load(conn, idAK);

										AankopenString = aankoop.ApparaatNaam + ", " + AankopenString;
										Aankopen.Add(aankoop);
									}
									AankopenString = AankopenString.Substring(0, (AankopenString.Length - 2));
								}
							}
							catch (Exception Exception)
							{
								Debug.WriteLine(Exception.Message);
							}
						}

						// ook de 'onderhoud' laden
						//						var onderhoudArray = new OnderhoudArrayController();
						//						onderhoudArray.LoadOnderhoud(conn, ID);
						//						InOnderhoud = onderhoudArray.Onderhoud;

						// ook de 'clubs' laden
						//						var lidmaatschappenArray = new LidmaatschappenArrayController();
						//						lidmaatschappenArray.LoadLidmaatschappen(conn, ID);
						//						Lidmaatschappen = lidmaatschappenArray.Lidmaatschappen;
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
				command.CommandText = "DELETE FROM [Persoon] WHERE ID = @COL1";

				// Populate with data from the record
				command.Parameters.AddWithValue("@COL1", ID);

				// Write to database
				command.ExecuteNonQuery();
			}

			conn.Close();

			// Empty class
			ID = "";
			Achternaam = "";
			Adres = "";
			Email = "";
			Geboortedatum = new NSDate();
			Imagepath = "";
			Initialen = "";
			Mobiel = "";
			Postcode = "";
			Telefoon = "";
			Tussenvoegsel = "";
			Voornamen = "";
			Woonplaats = "";

			// Save last connection
			_conn = conn;
		}
		#endregion

	}
}

