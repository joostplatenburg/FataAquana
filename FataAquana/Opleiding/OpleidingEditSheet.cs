using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace FataAquana
{
	public partial class OpleidingEditSheet : NSPanel
	{
		#region Constructors
		// Called when created from unmanaged code
		public OpleidingEditSheet (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public OpleidingEditSheet (NSCoder coder) : base (coder)
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
