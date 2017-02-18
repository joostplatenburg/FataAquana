using System;
using AppKit;
using Foundation;

namespace FataAquana
{
	public partial class GevolgdeOpleidingEditSheet : NSPanel
	{
		#region Constructors
		// Called when created from unmanaged code
		public GevolgdeOpleidingEditSheet (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public GevolgdeOpleidingEditSheet (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		// Shared initialization code
		void Initialize ()
		{
		}
		#endregion
	}
}
