using System;
using System.Collections;
using System.Collections.Generic;
using AppKit;
using Foundation;

namespace FataAquana
{
	public class GroepLijstItem: NSObject, IEnumerator, IEnumerable
	{
		#region Private Properties
		private string _title;
		private NSImage _icon;
		private string _tag;
		private List<GroepLijstItem> _items = new List<GroepLijstItem> ();
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public NSImage Icon {
			get { return _icon; }
			set { _icon = value; }
		}

		/// <summary>
		/// Gets or sets the tag.
		/// </summary>
		/// <value>The tag.</value>
		public string Tag {
			get { return _tag; }
			set { _tag=value; }
		}
		#endregion

		#region Indexer
		/// <summary>
		/// Gets or sets the <see cref="GroepLijstItem"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public GroepLijstItem this[int index]
		{
			get
			{
				return _items[index];
			}

			set
			{
				_items[index] = value;
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count {
			get { return _items.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance has children.
		/// </summary>
		/// <value><c>true</c> if this instance has children; otherwise, <c>false</c>.</value>
		public bool HasChildren {
			get { return (Count > 0); }
		}
		#endregion

		#region Enumerable Routines
		private int _position = -1;

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator GetEnumerator()
		{
			_position = -1;
			return this;
		}

		/// <summary>
		/// Moves the next.
		/// </summary>
		/// <returns><c>true</c>, if next was moved, <c>false</c> otherwise.</returns>
		public bool MoveNext()
		{
			_position++;
			return (_position < _items.Count);
		}

		/// <Docs>The collection was modified after the enumerator was instantiated.</Docs>
		/// <attribution license="cc4" from="Microsoft" modified="false"></attribution>
		/// <see cref="M:System.Collections.IEnumerator.MoveNext"></see>
		/// <see cref="M:System.Collections.IEnumerator.Reset"></see>
		/// <see cref="T:System.InvalidOperationException"></see>
		/// <summary>
		/// Reset this instance.
		/// </summary>
		public void Reset()
		{_position = -1;}

		/// <summary>
		/// Gets the current.
		/// </summary>
		/// <value>The current.</value>
		public object Current
		{
			get 
			{ 
				try 
				{
					return _items[_position];
				}

				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		public GroepLijstItem ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		public GroepLijstItem (string title)
		{
			// Initialize
			_title = title;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		public GroepLijstItem (string title, string icon)
		{
			// Initialize
			_title = title;
			_icon = NSImage.ImageNamed (icon);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="clicked">Clicked.</param>
		public GroepLijstItem (string title, string icon, ClickedDelegate clicked)
		{
			// Initialize
			_title = title;
			_icon = NSImage.ImageNamed (icon);
			Clicked = clicked;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		public GroepLijstItem (string title, NSImage icon)
		{
			// Initialize
			_title = title;
			_icon = icon;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="clicked">Clicked.</param>
		public GroepLijstItem (string title, NSImage icon, ClickedDelegate clicked)
		{
			// Initialize
			_title = title;
			_icon = icon;
			Clicked = clicked;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		public GroepLijstItem (string title, NSImage icon, string tag)
		{
			// Initialize
			_title = title;
			_icon = icon;
			_tag = tag;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GroepLijstItem"/> class.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="clicked">Clicked.</param>
		public GroepLijstItem (string title, NSImage icon, string tag, ClickedDelegate clicked)
		{
			// Initialize
			_title = title;
			_icon = icon;
			_tag = tag;
			Clicked = clicked;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void AddItem(GroepLijstItem item) {
			_items.Add (item);
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		public void AddItem(string title) {
			_items.Add (new GroepLijstItem (title));
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		public void AddItem(string title, string icon) {
			_items.Add (new GroepLijstItem (title, icon));
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="clicked">Clicked.</param>
		public void AddItem(string title, string icon, ClickedDelegate clicked) {
			_items.Add (new GroepLijstItem (title, icon, clicked));
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		public void AddItem(string title, NSImage icon) {
			_items.Add (new GroepLijstItem (title, icon));
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="clicked">Clicked.</param>
		public void AddItem(string title, NSImage icon, ClickedDelegate clicked) {
			_items.Add (new GroepLijstItem (title, icon, clicked));
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="tag">Tag.</param>
		public void AddItem(string title, NSImage icon, string tag) {
			_items.Add (new GroepLijstItem (title, icon, tag));
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="icon">Icon.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="clicked">Clicked.</param>
		public void AddItem(string title, NSImage icon, string tag, ClickedDelegate clicked) {
			_items.Add (new GroepLijstItem (title, icon, tag, clicked));
		}

		/// <summary>
		/// Insert the specified n and item.
		/// </summary>
		/// <param name="n">N.</param>
		/// <param name="item">Item.</param>
		public void Insert(int n, GroepLijstItem item) {
			_items.Insert (n, item);
		}

		/// <summary>
		/// Removes the item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void RemoveItem(GroepLijstItem item) {
			_items.Remove (item);
		}

		/// <summary>
		/// Removes the item.
		/// </summary>
		/// <param name="n">N.</param>
		public void RemoveItem(int n) {
			_items.RemoveAt (n);
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear() {
			_items.Clear ();
		}
		#endregion

		#region Events
		/// <summary>
		/// Clicked delegate.
		/// </summary>
		public delegate void ClickedDelegate();
		/// <summary>
		/// Occurs when clicked.
		/// </summary>
		public event ClickedDelegate Clicked;

		/// <summary>
		/// Raises the clicked event.
		/// </summary>
		internal void RaiseClickedEvent() {
			// Inform caller
			if (Clicked != null)
				Clicked();
		}
		#endregion
	}
}