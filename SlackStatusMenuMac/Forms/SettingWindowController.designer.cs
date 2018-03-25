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
        AppKit.NSButton CancelButton { get; set; }

        [Outlet]
        AppKit.NSButton OkButton { get; set; }

        [Outlet]
        AppKit.NSTextView TokenTextView { get; set; }

        [Action ("CancelButton_Click:")]
        partial void CancelButton_Click (Foundation.NSObject sender);

        [Action ("OkButton_Click:")]
        partial void OkButton_Click (Foundation.NSObject sender);
        
        void ReleaseDesignerOutlets ()
        {
            if (CancelButton != null) {
                CancelButton.Dispose ();
                CancelButton = null;
            }

            if (OkButton != null) {
                OkButton.Dispose ();
                OkButton = null;
            }

            if (TokenTextView != null) {
                TokenTextView.Dispose ();
                TokenTextView = null;
            }
        }
    }
}
