using AppKit;
using Foundation;
using CoreGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlackStatusMenuMac.Extensions;
using SlackStatusMenuMac.Views;

namespace SlackStatusMenuMac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        private NSStatusItem statusItem = NSStatusBar.SystemStatusBar.CreateStatusItem(NSStatusItemLength.Variable);
        private NSImage slack0 = new NSImage("slack.png").SetTemplate();
        private NSImage slack1 = new NSImage("slack.png").TintColor(NSColor.FromRgb(0.3f, 0.6f, 0.5f));
        private NSPopover popover = new NSPopover();
        private PopoverViewController popoverViewController = new PopoverViewController();

        private const int LOOP_WAIT = 25 * 1000;
        private const int INFO_CALL_WAIT = 800;

        public AppDelegate()
        {
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            NSApplication.SharedApplication.ActivationPolicy = NSApplicationActivationPolicy.Accessory;

            this.statusItem.Title = "-";
            this.statusItem.HighlightMode = true;
            this.statusItem.Image = this.slack0;
            this.popover.ContentViewController = this.popoverViewController;

            //this.statusItem.Button.Action = new ObjCRuntime.Selector("popover:");

            NSEvent.AddLocalMonitorForEventsMatchingMask(
                NSEventMask.LeftMouseDown | NSEventMask.RightMouseDown, (evt) =>
            {
                if (this.statusItem.Button.Bounds.Contains(evt.LocationInWindow) == true)
                {
                    if (this.statusItem.Button.Highlighted == false)
                    {
                        this.statusItem.Button.Highlight(true);
                        this.popover.Show(CGRect.Empty, this.statusItem.Button, NSRectEdge.MinYEdge);
                    }
                    else
                    {
                        this.popover.Close();
                        this.statusItem.Button.Highlight(false);
                    }
                    return null;
                }
                return evt;
            });

            /*
            NSEvent.AddGlobalMonitorForEventsMatchingMask(
                NSEventMask.LeftMouseDown | NSEventMask.RightMouseDown, (evt) =>
            {
                this.popover.Close();
                this.statusItem.Button.Highlight(false);
            });
            */

            this.getSlackMessagesLoop();
        }

        public override void WillTerminate(NSNotification notification)
        {
        }

        /*
        [Export("popover:")]
        private void popoverAction(NSObject sender)
        {
            if (this.popover.Shown == false) {
                this.popover.Show(CGRect.Empty, this.statusItem.Button, NSRectEdge.MinYEdge);    
            } else {
                this.popover.Close();
            }
        }
        */

        private async void getSlackMessagesLoop()
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        this.getSlackMessages();
                    }
                    catch (Exception ex)
                    {
                        InvokeOnMainThread(() =>
                        {
                            this.statusItem.Title = ex.Message;
                        });
                    }
                    Task.Delay(LOOP_WAIT);
                });
            }
        }

        private void getSlackMessages()
        {
            var tokens = Slack.TokenUtil.LoadTokens();
            var messages = new List<Slack.Message>();
            var count = 0;

            foreach (var token in tokens)
            {
                var client = new Slack.Client(token);

                var channelsList = client.ChannelsList();
                if (channelsList.OK)
                {
                    foreach (var channel in channelsList.Channels)
                    {
                        Task.Delay(INFO_CALL_WAIT).Wait();
                        var history = client.ChannelHistory(channel.ID, count: 10, unreads: true);
                        if (history.OK == true)
                        {
                            count += history.UnreadCountDisplay;

                            foreach (var message in messages)
                            {
                                if (message.User == null) { continue; }
                                var info = client.UsersInfo(message.User);
                                if (info.OK == true)
                                {
                                    message.UserRef = info.User;
                                }
                                else
                                {
                                    throw new ApplicationException(info.Error);
                                }
                            }

                            //history.Messages = history.Messages.Take(history.UnreadCountDisplay).ToList();
                            messages.AddRange(history.Messages);
                        }
                        else
                        {
                            throw new ApplicationException(history.Error);
                        }
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
                        Task.Delay(INFO_CALL_WAIT).Wait();
                        var history = client.GroupsHistory(group.ID, count: 10, unreads: true);
                        if (history.OK == true)
                        {
                            count += history.UnreadCountDisplay;

                            foreach (var message in messages)
                            {
                                if (message.User == null) { continue; }
                                var info = client.UsersInfo(message.User);
                                if (info.OK == true)
                                {
                                    message.UserRef = info.User;
                                }
                                else
                                {
                                    throw new ApplicationException(info.Error);
                                }
                            }

                            //history.Messages = history.Messages.Take(history.UnreadCountDisplay).ToList();
                            messages.AddRange(history.Messages);
                        }
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
                if (0 < count) { this.statusItem.Image = this.slack1; }
                else { this.statusItem.Image = this.slack0; }

                messages = messages.OrderByDescending(x => x.Ts).ToList();
                this.popoverViewController.SetMessages(messages);
            });
        }


    }
}
