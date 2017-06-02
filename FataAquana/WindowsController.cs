// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Diagnostics;

namespace FataAquana
{
	public partial class WindowsController : NSWindowController
	{
		public WindowsController (IntPtr handle) : base (handle)
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			// Wire up the toolbar controls
			OpenDB.Enabled = true;
			OpenDB.Activated += (sender, e) =>
			{
				AppDelegate.Conn = AppDelegate.GetDatabaseConnection();

				OpenDB.Enabled = false;

                CopyEmailadressen.Enabled = true;
				Search.Enabled = true;
				Print.Enabled = true;
			};

            CopyEmailadressen.Enabled = false;
            CopyEmailadressen.Activated += (sender, e) => 
            {
                AppDelegate.CopyEmailadressen();
            };
			Search.Enabled = false;
			Search.Activated += (sender, e) =>
			{
				if (AppDelegate.Conn != null)
				{
					// Als currentview = Personen zoek op Achternaam

					Debug.WriteLine("Zoek: " + Search.ToString());
					Debug.WriteLine("Zoekinhoud: " + Zoekveld.AccessibilityValue);

				}
			};
		}

		// Printing a file
/*		if (Path.GetExtension (filenames[0]).ToLower() == ".pdf") 
		{
		    var fileUrl = NSUrl.FromFilename(filePath);

			PdfDocument aPdfDocument = new PdfDocument(fileUrl);
			PdfView aPDFView = new PdfView();
			aPDFView.Document = aPdfDocument;
		    inv.Window.ContentView.AddSubview (aPDFView);
		    var pr = NSPrintInfo.SharedPrintInfo;
			pr.VerticallyCentered = false;
		    pr.TopMargin = 2.0f;
		    pr.BottomMargin = 2.0f;
		    pr.LeftMargin = 1.0f;
			bn 0pr.RightMargin = 1.0f;
			aPDFView.Print(this);
		}
*/
}
}
