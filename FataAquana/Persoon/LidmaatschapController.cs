// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using AppKit;
using System.Collections.Generic;
using System.Diagnostics;

namespace FataAquana
{
	public partial class LidmaatschapController : NSViewController
	{
		private PersoonController _parentController;
		private ClublidmaatschapModel _lidmaatschap;

		public List<ClublidmaatschapModel> Lidmaatschappen { get; set; } = new List<ClublidmaatschapModel>();

		public LidmaatschapController (IntPtr handle) : base (handle)
		{
		}

		#region Override Methods
		public override void AwakeFromNib()
		{
			Debug.WriteLine("Start: LidmaatschapController.AwakeFromNib");

			_parentController = this.PresentingViewController as PersoonController;
			if (_parentController != null)
			{
				Lidmaatschap = _parentController.SelectedLidmaatschap;

				if (Lidmaatschap == null) Lidmaatschap = new ClublidmaatschapModel();

				if (LidmaatschapCombobox != null)
				{
					LidmaatschapCombobox.UsesDataSource = true;
					LidmaatschapCombobox.Completes = true;
					LidmaatschapCombobox.DataSource = new ClubsComboDS();

					if (!Lidmaatschap.ClubNaam.Equals(string.Empty))
					{
						var index = LidmaatschapCombobox.DataSource.IndexOfItem(LidmaatschapCombobox, Lidmaatschap.ClubNaam);
						LidmaatschapCombobox.SelectItem(index);
					}
				}

				if (IngeschrevenOpButton != null)
				{
					IngeschrevenOpButton.State = NSCellStateValue.Off;
					IngeschrevenOpDate.Enabled = false;
				}

				if (UitgeschrevenOpButton != null)
				{
					UitgeschrevenOpButton.State = NSCellStateValue.Off;
					UitgeschrevenOpDate.Enabled = false;
				}
			}
			Debug.WriteLine("Start: LidmaatschapController.AwakeFromNib");
		}

		[Export("Lidmaatschap")]
		public ClublidmaatschapModel Lidmaatschap
		{
			get { return _lidmaatschap; }
			set
			{
				WillChangeValue("Lidmaatschap");
				_lidmaatschap = value;
				DidChangeValue("Lidmaatschap");
			}
		}
		#endregion

		partial void CancelButton(NSObject sender)
		{
			Debug.WriteLine("Start: LidmaatschapController.CloseButton");

			DismissController(this);

			Debug.WriteLine("Einde: LidmaatschapController.CloseButton");
		}

		partial void SaveButton(NSObject sender)
		{
			Debug.WriteLine("Start: LidmaatschapController.SaveButton");

			if (LidmaatschapCombobox.DataSource != null)
			{
				ClubsComboDS comboDS = LidmaatschapCombobox.DataSource as ClubsComboDS;

				var selectedClub = comboDS.Clubs[(int)LidmaatschapCombobox.SelectedIndex];

				Lidmaatschap.PersoonID = _parentController.Persoon.ID;
				Lidmaatschap.ClubID = selectedClub.ID;
				if (IngeschrevenOpButton.State.Equals(NSCellStateValue.On))
				{
					Lidmaatschap.IngeschrevenOp = IngeschrevenOpDate.DateValue;
				}
				if (UitgeschrevenOpButton.State.Equals(NSCellStateValue.On))
				{
					Lidmaatschap.UitgeschrevenOp = UitgeschrevenOpDate.DateValue;
				}

				Lidmaatschap.Create(AppDelegate.Conn);

				if (_parentController != null)
				{
					_parentController.LoadTables();
				}
			}

			DismissController(this);

			Debug.WriteLine("Einde: LidmaatschapController.SaveButton");
		}

		partial void IngeschrevenOpEnable(NSObject sender)
		{
			if (IngeschrevenOpButton.State.Equals(NSCellStateValue.On))
			{
				IngeschrevenOpDate.Enabled = true;
			}
			else
			{
				IngeschrevenOpDate.Enabled = false;
			}
		}

		partial void UitgeschrevenOpEnable(NSObject sender)
		{
			if (UitgeschrevenOpButton.State.Equals(NSCellStateValue.On))
			{
				UitgeschrevenOpDate.Enabled = true;
			}
			else
			{
				UitgeschrevenOpDate.Enabled = false;
			}
		}
	}
}
