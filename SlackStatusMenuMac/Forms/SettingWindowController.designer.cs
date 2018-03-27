// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SlackStatusMenuMac.Forms
{
	[Register ("SettingWindowController")]
	partial class SettingWindowController
	{
		[Outlet]
		AppKit.NSButton cancelButton { get; set; }

		[Outlet]
		AppKit.NSButton okButton { get; set; }

		[Outlet]
		AppKit.NSTextView slackTokensTextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (slackTokensTextView != null) {
				slackTokensTextView.Dispose ();
				slackTokensTextView = null;
			}

			if (okButton != null) {
				okButton.Dispose ();
				okButton = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}
		}
	}
}
