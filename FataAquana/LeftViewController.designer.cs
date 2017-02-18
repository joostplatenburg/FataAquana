// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FataAquana
{
	[Register ("LeftViewController")]
	partial class LeftViewController
	{
		[Outlet]
		GroepLijstView GroepLijst { get; set; }

		void ReleaseDesignerOutlets()
		{
			if (GroepLijst != null)
			{
				GroepLijst.Dispose();
				GroepLijst = null;
			}
		}
	}
}
