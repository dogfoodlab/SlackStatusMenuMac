using AppKit;
using Foundation;
using System;
using System.Threading.Tasks;

namespace SlackStatusMenuMac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        NSStatusItem statusItem = NSStatusBar.SystemStatusBar.CreateStatusItem(NSStatusItemLength.Variable);
        NSImage slack0 = new NSImage("slack_0.png");
        NSImage slack1 = new NSImage("slack_1.png");

        Forms.SettingWindowController settingWindow = new Forms.SettingWindowController();

        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            var menu = new NSMenu();
            this.statusItem.Title = "-";
            this.statusItem.HighlightMode = true;
            this.statusItem.Image = this.slack0;
            this.statusItem.Menu = menu;

            menu.AddItem(new NSMenuItem { Title = "Slack", Action = new ObjCRuntime.Selector("slack:") });
            menu.AddItem(new NSMenuItem { Title = "Setting", Action = new ObjCRuntime.Selector("setting:") });
            menu.AddItem(new NSMenuItem { Title = "Quit", Action = new ObjCRuntime.Selector("quit:") });

            this.getUnreadCountLoop();
        }

        public override void WillTerminate(NSNotification notification)
        {
        }

        [Export("slack:")]
        private void slackMenu(NSObject sender)
        {
            NSWorkspace.SharedWorkspace.LaunchApplication("Slack");
        }

        [Export("setting:")]
        private void settingMenu(NSObject sender)
        {
            this.settingWindow.ShowWindow(sender);
        }

        [Export("quit:")]
        private void quitMenu(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(this);
        }

        private async void getUnreadCountLoop()
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        this.getUnreadCount();
                    }
                    catch (Exception ex)
                    {
                        InvokeOnMainThread(() =>
                        {
                            this.statusItem.Title = ex.Message;
                        });
                    }
                    Task.Delay(20 * 1000);
                });
            }
        }

        private void getUnreadCount()
        {
            var count = 0L;
            var tokens = Slack.TokenUtil.LoadTokens();

            foreach (var token in tokens)
            {
                var client = new Slack.Client(token);

                var channelsList = client.ChannelsList();
                if (channelsList.OK)
                {
                    foreach (var channel in channelsList.Channels)
                    {
                        Task.Delay(500).Wait();
                        var info = client.ChannelsInfo(channel.ID);
                        if (info.OK) { count += info.Channel.UnreadCountDisplay; }
                        else { throw new ApplicationException(info.Error); }
                    }
                }
                else
                {
                    throw new ApplicationException(channelsList.Error);
                }

                var groupsList = client.GroupsList();
                if (groupsList.OK)
                {
                    foreach (var group in groupsList.Groups)
                    {
                        Task.Delay(500).Wait();
                        var info = client.GroupsInfo(group.ID);
                        if (info.OK) { count += info.Group.UnreadCountDisplay; }
                        else { throw new ApplicationException(info.Error); }
                    }
                }
                else
                {
                    throw new ApplicationException(groupsList.Error);
                }
            }

            InvokeOnMainThread(() =>
            {
                this.statusItem.Title = count.ToString();
                if (0 < count)
                {
                    this.statusItem.Image = this.slack1;
                }
                else
                {
                    this.statusItem.Image = this.slack0;
                }
            });
        }


    }
}
