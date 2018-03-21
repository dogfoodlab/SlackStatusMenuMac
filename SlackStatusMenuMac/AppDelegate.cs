using AppKit;
using Foundation;
using Security;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        NSStatusItem statusItem = NSStatusBar.SystemStatusBar.CreateStatusItem(NSStatusItemLength.Variable);
        NSImage slack0 = new NSImage("slack_0.png");
        NSImage slack1 = new NSImage("slack_1.png");

        long count = 0;
        System.Timers.Timer timer = new System.Timers.Timer();

        List<string> tokens = new List<string>();

        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            var menu = new NSMenu();
            this.statusItem.Title = count.ToString();
            this.statusItem.HighlightMode = true;
            this.statusItem.Image = this.slack0;
            this.statusItem.Menu = menu;

            var menuItem1 = new NSMenuItem
            {
                Title = "Token",
                Action = new ObjCRuntime.Selector("token:"),
            };

            var menuItem2 = new NSMenuItem
            {
                Title = "Start",
                Action = new ObjCRuntime.Selector("start:"),
            };

            var menuItem3 = new NSMenuItem
            {
                Title = "Slack",
                Action = new ObjCRuntime.Selector("slack:"),
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


        [Export("quit:")]
        private void quitExec(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(this);
        }

        [Export("token:")]
        private void tokenExec(NSObject sender)
        {
            this.getTokens();
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


        private void getUnreadCount()
        {
            count = 0;

            foreach (var token in this.tokens)
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
                    }
                }
            }

            InvokeOnMainThread(() =>
            {
                this.statusItem.Title = this.count.ToString();
                if (0 < this.count)
                {
                    this.statusItem.Image = this.slack1;
                }
                else
                {
                    this.statusItem.Image = this.slack0;
                }
            });
        }

        private void getTokens()
        {
            
            var query = new SecRecord(SecKind.GenericPassword)
            {
                Account = "tokens",
                Service = "Slack Status Menu"
            };

            SecStatusCode status;
            SecRecord record;

            // Check item exists
            record = SecKeyChain.QueryAsRecord(query, out status);
            if (status == SecStatusCode.ItemNotFound)
            {
                record = new SecRecord(SecKind.GenericPassword)
                {
                    Account = "tokens",
                    Service = "Slack Status Menu",
                    ValueData = "[]"
                };
                var createdStatus = SecKeyChain.Add(record);
                if (createdStatus != SecStatusCode.Success)
                {
                    throw new Exception(createdStatus.ToString());
                }
            }

            // Get item value
            record = SecKeyChain.QueryAsRecord(query, out status);
            if (status == SecStatusCode.Success)
            {
                var data = record.ValueData;
                Debug.WriteLine(data);

                this.tokens = JsonConvert.DeserializeObject<List<string>>(data.ToString());
            } else {
                throw new Exception(status.ToString());
            }
        }


    }
}
