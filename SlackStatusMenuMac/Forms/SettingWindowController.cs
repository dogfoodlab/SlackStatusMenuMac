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

            Window.StyleMask = Window.StyleMask & ~NSWindowStyle.Resizable; 
            Window.Level = NSWindowLevel.Floating;
        }

        public new SettingWindow Window
        {
            get { return (SettingWindow)base.Window; }
        }

		public override void ShowWindow(NSObject sender)
		{
            base.ShowWindow(sender);

            var tokens = Slack.TokenUtil.LoadTokens();
            var text = string.Join("\n", tokens) + (0 < tokens.Count ? "\n" : string.Empty);

            this.TokenTextView.Value = text;
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
