using System;
using AppKit;
using Foundation;

namespace FataAquana
{
	[Register("GroepLijstView")]
	public class GroepLijstView : NSOutlineView
	{
		#region Computed Properties
		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>The data.</value>
		public GroepLijstDataSource Data {
			get {return (GroepLijstDataSource)this.DataSource; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Rotation.OutlineViewController"/> class.
		/// </summary>
		public GroepLijstView ()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rotation.OutlineViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public GroepLijstView (IntPtr handle) : base(handle)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rotation.OutlineViewController"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public GroepLijstView (NSCoder coder) : base(coder)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Rotation.OutlineViewController"/> class.
		/// </summary>
		/// <param name="t">T.</param>
		public GroepLijstView (NSObjectFlag t) : base(t)
		{

		}

		#endregion

		#region Override Methods
		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize() {

			// Initialize this instance
			this.DataSource = new GroepLijstDataSource (this);
			this.Delegate = new GroepLijstDelegate (this);
			//this.Enabled = false;

		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void AddItem(GroepLijstItem item) {
			if (Data != null) {
				Data.Items.Add (item);
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Item selected delegate.
		/// </summary>
		public delegate void ItemSelectedDelegate(GroepLijstItem item);
		/// <summary>
		/// Occurs when item selected.
		/// </summary>
		public event ItemSelectedDelegate ItemSelected;

		/// <summary>
		/// Raises the item selected.
		/// </summary>
		/// <param name="item">Item.</param>
		internal void RaiseItemSelected(GroepLijstItem item) {
			// Inform caller
			if (this.ItemSelected != null) {
				this.ItemSelected (item);
			}
		}
		#endregion
	}
}

