using System;
using System.Collections;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace FataAquana
{
	public class GroepLijstDataSource : NSOutlineViewDataSource
	{
		#region Private Variables
		private GroepLijstView _controller;
		#endregion

		#region Public Variables
		/// <summary>
		/// The collection of <see cref="GroepLijstItem"/> that are being displayed.
		/// </summary>
		public List<GroepLijstItem> Items = new List<GroepLijstItem>();
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstDataSource"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public GroepLijstDataSource (GroepLijstView controller)
		{
			// Initialize
			_controller = controller;
		}
		#endregion

		#region Override Properties
		/// <summary>
		/// Gets the children count.
		/// </summary>
		/// <returns>The children count.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="item">Item.</param>
		public override nint GetChildrenCount (NSOutlineView outlineView, NSObject item)
		{
			if (item == null) {
				return Items.Count;
			} else {
				return ((GroepLijstItem)item).Count;
			}
		}

		/// <summary>
		/// Items the expandable.
		/// </summary>
		/// <returns><c>true</c>, if expandable was itemed, <c>false</c> otherwise.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="item">Item.</param>
		public override bool ItemExpandable (NSOutlineView outlineView, NSObject item)
		{
			return ((GroepLijstItem)item).HasChildren;
		}

		/// <summary>
		/// Gets the child.
		/// </summary>
		/// <returns>The child.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="childIndex">Child index.</param>
		/// <param name="item">Item.</param>
		public override NSObject GetChild (NSOutlineView outlineView, nint childIndex, NSObject item)
		{
			if (item == null) {
				return Items [(int)childIndex];
			} else {
				return ((GroepLijstItem)item) [(int)childIndex]; 
			}
		}

		/// <summary>
		/// Gets the object value.
		/// </summary>
		/// <returns>The object value.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="tableColumn">Table column.</param>
		/// <param name="item">Item.</param>
		public override NSObject GetObjectValue (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
		{
			return new NSString (((GroepLijstItem)item).Title);
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Items for row.
		/// </summary>
		/// <returns>The for row.</returns>
		/// <param name="row">Row.</param>
		internal GroepLijstItem ItemForRow(int row) {
			int index = 0;

			// Look at each group
			foreach (GroepLijstItem item in Items) {
				// Is the row inside this group?
				if (row >= index && row <= (index + item.Count)) {
					return item [row - index - 1];
				}

				// Move index
				index += item.Count + 1;
			}

			// Not found 
			return null;
		}
		#endregion
	}
}

