using System;

using AppKit;
using CoreGraphics;
using Foundation;

namespace FataAquana
{
	public partial class ViewController : NSViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Do any additional setup after loading the view.

			//AppDelegate.MainView = this;
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			// Populate Groepobjecten lijst
			GroepLijst.Initialize();

			var TableViews = new GroepLijstItem("Fata Aquana");
			TableViews.AddItem("Personen", "user1.png", () => {
				DisplaySubview(new PersonenController(), GroepObjecten.Personen);
			});
			GroepLijst.AddItem(TableViews);

			var StamObjecten = new GroepLijstItem("Stam Gegevens");
			StamObjecten.AddItem("Opleidingen", "blackboard.png", () => {
				DisplaySubview(new OpleidingenController(), GroepObjecten.Opleidingen);
			});

			StamObjecten.AddItem("Apparaten", "apparatuur.png", () => {
				DisplaySubview(new ApparatenController(), GroepObjecten.Apparaten);
			});

			StamObjecten.AddItem("Clubs", "users2.png", () => {
				DisplaySubview(new ClubsController(), GroepObjecten.Clubs);
			});
			GroepLijst.AddItem(StamObjecten);

			// Now Display the groeps
			GroepLijst.ReloadData();
			GroepLijst.ExpandItem(null, true);

			// initially show personen lijst
			//DisplaySubview(new PersonenController(), GroepObjecten.Personen);
		}

		public void SetSubView()
		{
			DisplaySubview(new PersonenController(), GroepObjecten.Personen);
		//	DisplaySubview(new OpleidingenController(), GroepObjecten.Opleidingen);
		//	DisplaySubview(new ApparatenController(), GroepObjecten.Apparaten);
		//	DisplaySubview(new ClubsController(), GroepObjecten.Clubs);
		}

		public void EnableGroeplijst()
		{
			GroepLijst.Enabled = true;
		}

		public void DisableGroeplijst()
		{
			GroepLijst.Enabled = false;
		}

		#region Private Methods
		private GroepObjecten GroepObject = GroepObjecten.None;

		private NSViewController GroepViewController = null;
		private NSView GroepView = null;

		private void DisplaySubview(NSViewController controller, GroepObjecten type)
		{

			// Is this view already displayed?
			if (GroepObject == type) return;

			// Is there a view already being displayed?
			if (GroepView != null)
			{
				// Yes, remove it from the view
				GroepView.RemoveFromSuperview();

				// Release memory
				GroepView = null;
				GroepViewController = null;
			}

			// Save values
			GroepObject = type;
			GroepViewController = controller;
			GroepView = GroepViewController.View;

			//Console.WriteLine("ViewContainer.Frame.Width: " + ViewContainer.Frame.Width);
			//Console.WriteLine("ViewContainer.Frame.Height: " + ViewContainer.Frame.Height);
			//Console.WriteLine("GroepView.Frame.Width:  " + GroepView.Frame.Width);
			//Console.WriteLine("GroepView.Frame.Height: " + GroepView.Frame.Height);

			// Define frame and display
			GroepView.Frame = new CGRect(0, 0, ViewContainer.Frame.Width, ViewContainer.Frame.Height);

			//Console.WriteLine(ViewContainer.Subviews.Length);
			//Console.WriteLine("ViewContainer.Frame.Width: " + ViewContainer.Frame.Width);
			//Console.WriteLine("ViewContainer.Frame.Height: " + ViewContainer.Frame.Height);
			//Console.WriteLine("GroepView.Frame.Width:  " + GroepView.Frame.Width);
			//Console.WriteLine("GroepView.Frame.Height: " + GroepView.Frame.Height);

			ViewContainer.AddSubview(GroepView);

			// Take action on type
			switch (type)
			{
				case GroepObjecten.Personen:
				//	AddButton.Active = true;
				//	EditButton.Active = true;
				//	DeleteButton.Active = true;
				//	Search.Enabled = true;
					break;
				case GroepObjecten.Opleidingen:
				//	AddButton.Active = true;
				//	EditButton.Active = true;
				//	DeleteButton.Active = true;
				//	Search.Enabled = true;
					break;
				case GroepObjecten.Apparaten:
					break;
				case GroepObjecten.Clubs:
					break;
				default:
				//	AddButton.Active = false;
				//	EditButton.Active = false;
				//	DeleteButton.Active = false;
				//	Search.Enabled = false;
					break;
			}
		}

		public override void PreferredContentSizeDidChange(NSViewController viewController)
		{
			base.PreferredContentSizeDidChange(viewController);
		}

		public override void WillChangeValue(string forKey)
		{
			base.WillChangeValue(forKey);
		}
	
		#endregion
	}
}
