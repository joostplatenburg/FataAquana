using System;
using AppKit;
using Foundation;

namespace FataAquana
{
	public class GroepLijstDelegate : NSOutlineViewDelegate
	{
		#region Private variables
		private GroepLijstView _controller;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstDelegate"/> class.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public GroepLijstDelegate (GroepLijstView controller)
		{
			// Initialize
			_controller = controller;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Shoulds the edit table column.
		/// </summary>
		/// <returns><c>true</c>, if edit table column was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="tableColumn">Table column.</param>
		/// <param name="item">Item.</param>
		public override bool ShouldEditTableColumn (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
		{
			return false;
		}

		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="tableColumn">Table column.</param>
		/// <param name="item">Item.</param>
		public override NSCell GetCell (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
		{
			nint row = outlineView.RowForItem (item);
			return tableColumn.DataCellForRow (row);
		}

		/// <summary>
		/// Determines whether this instance is group item the specified outlineView item.
		/// </summary>
		/// <returns><c>true</c> if this instance is group item the specified outlineView item; otherwise, <c>false</c>.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="item">Item.</param>
		public override bool IsGroupItem (NSOutlineView outlineView, NSObject item)
		{
			return ((GroepLijstItem)item).HasChildren;
		}

		/// <summary>
		/// Views for table column.
		/// </summary>
		/// <returns>The for table column.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="tableColumn">Table column.</param>
		/// <param name="item">Item.</param>
		public override NSView GetView (NSOutlineView outlineView, NSTableColumn tableColumn, NSObject item)
		{
			NSTableCellView view = null;

			// Is this a group item?
			if (((GroepLijstItem)item).HasChildren) {
				view = (NSTableCellView)outlineView.MakeView ("HeaderCell", this);
			} else {
				view = (NSTableCellView)outlineView.MakeView ("DataCell", this);
				view.ImageView.Image = ((GroepLijstItem)item).Icon;
			}

			// Initialize view
			view.TextField.StringValue = ((GroepLijstItem)item).Title;

			// Return new view
			return view;
		}

		/// <summary>
		/// Shoulds the select item.
		/// </summary>
		/// <returns><c>true</c>, if select item was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="outlineView">Outline view.</param>
		/// <param name="item">Item.</param>
		public override bool ShouldSelectItem (NSOutlineView outlineView, NSObject item)
		{
			return (outlineView.GetParent (item) != null);
		}

		/// <summary>
		/// Selections the did change.
		/// </summary>
		/// <param name="notification">Notification.</param>
		public override void SelectionDidChange (NSNotification notification)
		{
			NSIndexSet selectedIndexes = _controller.SelectedRows;

			// More than one item selected?
			if (selectedIndexes.Count > 1) {
				// Not handling this case
			} else {
				// Grab the item
				var item = _controller.Data.ItemForRow ((int)selectedIndexes.FirstIndex);

				// Was an item found?
				if (item != null) {
					// Fire the clicked event for the item
					item.RaiseClickedEvent ();

					// Inform caller of selection
					_controller.RaiseItemSelected (item);
				}
			}
		}
		#endregion
	}
}

