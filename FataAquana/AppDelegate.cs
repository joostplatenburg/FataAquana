using System;
using System.Data;
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

		public static PersoonModel SelectedPersoon = null;

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

					MainView.EnableGroepList();
				}
			}
			else {

			}

			return Conn;
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
