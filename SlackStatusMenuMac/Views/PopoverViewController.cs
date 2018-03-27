using AppKit;
using Foundation;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlackStatusMenuMac.Views
{
    public partial class PopoverViewController : AppKit.NSViewController
    {
        private MessageTableViewSource source = new MessageTableViewSource();
        private NSWindowController settingWindow = new Forms.SettingWindowController();
        private NSMenu menu = new NSMenu();

        #region Constructors

        public PopoverViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public PopoverViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public PopoverViewController() : base("PopoverView", NSBundle.MainBundle)
        {
            Initialize();
        }

        private void Initialize()
        {
            View.SetAppearance(NSAppearance.GetAppearance(NSAppearance.NameAqua));
            this.messageTableView.Source = this.source;

            this.menu.AddItem(new NSMenuItem { Title = "Setting", Action = new ObjCRuntime.Selector("setting:") });
            this.menu.AddItem(new NSMenuItem { Title = "Quit", Action = new ObjCRuntime.Selector("quit:") });

            this.slackButton.Action = new ObjCRuntime.Selector("slack:");
            this.menuButton.Action = new ObjCRuntime.Selector("popupMenu:");
        }

        #endregion

        public new PopoverView View => (PopoverView)base.View;

        [Export("slack:")]
        private void slackAction(NSObject sender)
        {
            NSWorkspace.SharedWorkspace.LaunchApplication("Slack");
        }

        [Export("popupMenu:")]
        private void popupAction(NSObject sender)
        {
            this.menu.PopUpMenu(null, CGPoint.Empty, this.menuButton);
        }

        [Export("setting:")]
        private void settingAction(NSObject sender)
        {
            this.settingWindow.ShowWindow(this);
        }

        [Export("quit:")]
        private void quitAction(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(this);
        }

        public void SetMessages(List<Slack.Message> messages)
        {
            this.source.Clear();
            this.source.AddRange(messages);
            this.messageTableView.ReloadData();
        }


    }
}
