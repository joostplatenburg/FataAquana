using System.Diagnostics;
using AppKit;

namespace FataAquana
{
	static class MainClass
	{
		static void Main(string[] args)
		{
			try
			{
				NSApplication.Init();
				NSApplication.Main(args);
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
	}
}
