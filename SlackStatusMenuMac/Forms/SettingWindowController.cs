using AppKit;
using Foundation;
using System;
using System.Collections.Generic;

namespace SlackStatusMenuMac.Forms
{
    public partial class SettingWindowController : NSWindowController
    {
        #region Constructors

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

        #endregion

        public new SettingWindow Window => (SettingWindow)base.Window;

		public override void WindowDidLoad()
		{
            base.WindowDidLoad();
            Window.StyleMask = Window.StyleMask & ~NSWindowStyle.Resizable;
            Window.Level = NSWindowLevel.ScreenSaver;
            //Window.Level = NSWindowLevel.Floating;

            this.okButton.Action = new ObjCRuntime.Selector("ok:");
            this.cancelButton.Action = new ObjCRuntime.Selector("cancel:");
		}

		public override void ShowWindow(NSObject sender)
        {
            base.ShowWindow(sender);

            var tokens = Slack.TokenUtil.LoadTokens();
            var text = string.Join("\n", tokens) + (0 < tokens.Count ? "\n" : string.Empty);

            this.slackTokensTextView.Value = text;
        }

        [Export("ok:")]
        private void okAction(NSObject sender)
        {
            var text = this.slackTokensTextView.Value;

            var list = text.Trim().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var tokens = new List<string>(list);

            Slack.TokenUtil.SaveTokens(tokens);

            this.slackTokensTextView.Value = string.Empty;

            this.Close();
        }

        [Export("cancel:")]
        private void cancelAction(NSObject sender)
        {
            this.slackTokensTextView.Value = string.Empty;

            this.Close();
        }


    }
}
