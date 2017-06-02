using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using AppKit;
using Foundation;
using Mono.Data.Sqlite;

namespace FataAquana
{
	[Register("AppDelegate")]
	public partial class AppDelegate : NSApplicationDelegate
	{
		public static SqliteConnection Conn = null;

		public static SplitViewController MainView = null;

		public AppDelegate()
		{
			//
		}

		public override void DidFinishLaunching(NSNotification notification)
		{
			// Insert code here to initialize your application
		}

		public override void WillTerminate(NSNotification notification)
		{
			// Insert code here to tear down your application
		}

		public static SqliteConnection GetDatabaseConnection()
		{
			var dlg = NSOpenPanel.OpenPanel;

			dlg.CanChooseFiles = true;
			dlg.CanChooseDirectories = false;

			if (dlg.RunModal() == 1)
			{
				// Nab the first file
				var url = dlg.Urls[0];

				bool exists = File.Exists(url.Path);
				if (!exists)
				{
					SqliteConnection.CreateFile(url.Path);

					Conn = new SqliteConnection("Data Source=" + url.Path);

					var commands = new[] { "CREATE TABLE Opleiding (ID TEXT, Naam TEXT, Omschrijving TEXT)" };
					Conn.Open();

					foreach (var cmd in commands)
					{
						using (var c = Conn.CreateCommand())
						{
							c.CommandText = cmd;
							c.CommandType = CommandType.Text;
							c.ExecuteNonQuery();
						}
					}
					Conn.Close();

				}
				else {
					// Database bestaat al
					Conn = new SqliteConnection("Data Source=" + url.Path);

					try
					{
						Conn.Open();

                        VoegColumnToe(Conn, "Apparaat", "Omschrijving", "Text");
                        VoegColumnToe(Conn, "Aankoop", "GekochtOpText", "Text");
						
                        VoegColumnToe(Conn, "Clublidmaatschap", "IngeschrevenOpText", "Text");
						VoegColumnToe(Conn, "Clublidmaatschap", "UitgeschrevenOpText", "Text");

                        VoegColumnToe(Conn, "GevolgdeOpleiding", "GeslaagdOpText", "Text");
                        VoegColumnToe(Conn, "GevolgdeOpleiding", "VerlopenOpText", "Text");
					
                        VoegColumnToe(Conn, "InOnderhoud", "OntvangenOpText", "Text");
                        VoegColumnToe(Conn, "InOnderhoud", "RetourOpText", "Text");

						VoegColumnToe(Conn, "Persoon", "GeboortedatumText", "Text");

						VoegColumnToe(Conn, "WerkPeriode", "GestartOpText", "Text");
						VoegColumnToe(Conn, "WerkPeriode", "GestoptOpText", "Text");
						

                        Conn.Close();
					}
					catch(Exception e)
					{
                        Debug.WriteLine(e.Message);
					}

					MainView.EnableGroepList();
				}
			}
			else {

			}

			return Conn;
		}

        internal static void CopyEmailadressen()
        {
            Debug.WriteLine("CopyEmailadressen clicked");

            var s = "Test string email adres jplatenb@dxc.com";

            NSPasteboard pasteboard = NSPasteboard.GeneralPasteboard;

            pasteboard.ClearContents();

            //pasteboard.WriteObjects(s as INSPasteboardWriting[]);
        }


        private static void VoegColumnToe(SqliteConnection conn, string tablename, string columnname, string columntype)
        {
            try
            {
                var cmd = string.Format("ALTER TABLE {0} ADD {1} {2}", tablename, columnname, columntype);

				using (var c = Conn.CreateCommand())
				{
					c.CommandText = cmd;
					c.CommandType = CommandType.Text;
					c.ExecuteNonQuery();
				}

			}
            catch(Exception e)
            {
				Debug.WriteLine(e.Message);
			}
        }

        public static DateTime NSDateToDateTime(NSDate date)
		{
			// NSDate has a wider range than DateTime, so clip
			// the converted date to DateTime.Min|MaxValue.
			double secs = date.SecondsSinceReferenceDate;
			if (secs < -63113904000)
				return DateTime.MinValue;
			if (secs > 252423993599)
				return DateTime.MaxValue;
			return (DateTime)date;
		}

		public static NSDate DateTimeToNSDate(DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind(date, DateTimeKind.Utc /* or DateTimeKind.Local, this depends on each app */);

			return (NSDate)date;
		}
	}
}
