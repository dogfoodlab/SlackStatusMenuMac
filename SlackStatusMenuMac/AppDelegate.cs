using AppKit;
using Foundation;
using System;

namespace SlackStatusMenuMac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        NSStatusItem statusItem = NSStatusBar.SystemStatusBar.CreateStatusItem(NSStatusItemLength.Variable);
        NSImage slack0 = new NSImage("slack_0.png");
        NSImage slack1 = new NSImage("slack_1.png");

        System.Timers.Timer timer = new System.Timers.Timer();

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

            var menuItem1 = new NSMenuItem
            {
                Title = "Start",
                Action = new ObjCRuntime.Selector("start:"),
            };

            var menuItem2 = new NSMenuItem
            {
                Title = "Slack",
                Action = new ObjCRuntime.Selector("slack:"),
            };

            var menuItem3 = new NSMenuItem
            {
                Title = "Setting",
                Action = new ObjCRuntime.Selector("setting:"),
            };

            var menuItem4 = new NSMenuItem
            {
                Title = "Quit",
                Action = new ObjCRuntime.Selector("quit:"),
            };

            menu.AddItem(menuItem1);
            menu.AddItem(menuItem2);
            menu.AddItem(menuItem3);
            menu.AddItem(menuItem4);

            this.timer.Interval = 20 * 1000;
            this.timer.AutoReset = true;
            this.timer.Elapsed += (s, e) =>
            {
                this.getUnreadCount();
            };
        }

        public override void WillTerminate(NSNotification notification)
        {
        }

        [Export("start:")]
        private void startExec(NSObject sender)
        {
            this.timer.Start();
        }

        [Export("slack:")]
        private void slackExec(NSObject sender)
        {
            NSWorkspace.SharedWorkspace.LaunchApplication("Slack");
        }

        [Export("setting:")]
        private void settingExec(NSObject sender)
        {
            var window = new Forms.SettingWindowController();
            window.ShowWindow(sender);
        }

        [Export("quit:")]
        private void quitExec(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(this);
        }


        private void getUnreadCount()
        {
            var count = 0L;
            var tokens = Slack.TokenUtil.LoadTokens();


            try
            {

                foreach (var token in tokens)
                {
                    var client = new Slack.Client(token);

                    var list = client.GroupsList();
                    if (list.OK)
                    {
                        foreach (var group in list.Groups)
                        {
                            var info = client.GroupsInfo(group.ID);
                            if (info.OK)
                            {
                                count += info.Group.UnreadCountDisplay;
                            }
                            else
                            {
                                throw new ApplicationException(info.Error);
                            }
                        }
                    }
                    else
                    {
                        throw new ApplicationException(list.Error);
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
            catch (Exception ex)
            {
                InvokeOnMainThread(() =>
                {
                    this.statusItem.Title = ex.Message;
                });
            }
        }


    }
}
