using AppKit;
using Foundation;
using System;
using System.Collections.Generic;

namespace SlackStatusMenuMac.Forms
{
    public partial class SettingWindowController : NSWindowController
    {
        public SettingWindowController(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public SettingWindowController(NSCoder coder) : base(coder)
        {
        }

        public SettingWindowController() : base("SettingWindow")
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Window.Level = NSWindowLevel.Floating;

            var tokens = Slack.TokenUtil.LoadTokens();

            var text = string.Join("\n", tokens) + "\n";

            this.TokenTextView.InsertText(new NSString(text));
        }

        public new SettingWindow Window
        {
            get { return (SettingWindow)base.Window; }
        }

        partial void OkButton_Click(Foundation.NSObject sender)
        {
            var text = this.TokenTextView.String;

            var list = text.Trim().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var tokens = new List<string>(list);

            Slack.TokenUtil.SaveTokens(tokens);

            this.Close();
        }

        partial void CancelButton_Click(NSObject sender)
        {
            this.Close();
        }


    }
}
