using System;
using Foundation;
using AppKit;

namespace SlackStatusMenuMac.Forms
{
    public partial class SettingWindow : NSWindow
    {
        public SettingWindow(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public SettingWindow(NSCoder coder) : base(coder)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
        }
    }
}
