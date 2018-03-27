// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SlackStatusMenuMac.Views
{
	[Register ("PopoverViewController")]
	partial class PopoverViewController
	{
		[Outlet]
		AppKit.NSButton menuButton { get; set; }

		[Outlet]
		AppKit.NSTableView messageTableView { get; set; }

		[Outlet]
		AppKit.NSButton slackButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (menuButton != null) {
				menuButton.Dispose ();
				menuButton = null;
			}

			if (messageTableView != null) {
				messageTableView.Dispose ();
				messageTableView = null;
			}

			if (slackButton != null) {
				slackButton.Dispose ();
				slackButton = null;
			}
		}
	}
}
