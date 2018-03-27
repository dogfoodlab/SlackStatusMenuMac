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
	[Register ("MessageTableCellViewController")]
	partial class MessageTableCellViewController
	{
		[Outlet]
		AppKit.NSImageView avaterImageView { get; set; }

		[Outlet]
		AppKit.NSTextField dateTextField { get; set; }

		[Outlet]
		AppKit.NSTextField messageTextField { get; set; }

		[Outlet]
		AppKit.NSTextField nameTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (avaterImageView != null) {
				avaterImageView.Dispose ();
				avaterImageView = null;
			}

			if (dateTextField != null) {
				dateTextField.Dispose ();
				dateTextField = null;
			}

			if (messageTextField != null) {
				messageTextField.Dispose ();
				messageTextField = null;
			}

			if (nameTextField != null) {
				nameTextField.Dispose ();
				nameTextField = null;
			}
		}
	}
}
