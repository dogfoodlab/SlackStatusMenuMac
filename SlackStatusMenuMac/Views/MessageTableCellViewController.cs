using AppKit;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlackStatusMenuMac.Views
{
    public partial class MessageTableCellViewController : AppKit.NSViewController
    {
        #region Constructors

        public MessageTableCellViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public MessageTableCellViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public MessageTableCellViewController() : base("MessageTableCellView", NSBundle.MainBundle)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        #endregion

        public new MessageTableCellView View => (MessageTableCellView)base.View;

        public void SetRowData(Slack.Message message, bool sizeOnly = false)
        {
            this.dateTextField.StringValue = message.Ts.ToString("MM/dd HH:mm:ss");

            /*
            this.messageTextField.UsesSingleLineMode = false;
            this.messageTextField.LineBreakMode = NSLineBreakMode.CharWrapping;
            this.messageTextField.MaximumNumberOfLines = 0;
            */

            this.nameTextField.StringValue
                = message.Attachments?.First().AuthorName
                ?? message.UserRef?.Profile.DisplayNameNormalized
                ?? message.User
                ?? message.Username
                ?? string.Empty;

            this.messageTextField.StringValue
                = message.Attachments?.First().Text
                ?? message.Text
                ?? string.Empty;

            if (sizeOnly == false)
            {
                var url
                    = message.UserRef?.Profile.Image48
                    ?? message.Attachments?.First().AuthorIcon
                    ?? string.Empty;

                if (url != string.Empty)
                {
                    var avater = new NSImage(NSUrl.FromString(url));
                    this.avaterImageView.Image = avater;
                }
            }

            var text = this.messageTextField.StringValue;
            text = text.Trim() + "\n";
            text = new String(text.Take(150).ToArray());
            this.messageTextField.StringValue = text;

            var tv = new NSTextView();
            tv.SetFrameSize(new CGSize(this.messageTextField.Frame.Width, 9999f));
            tv.Value = this.messageTextField.StringValue;
            tv.SizeToFit();

            /*
            var text = new NSString(this.messageTextField.StringValue);
            var options = new NSStringDrawingOptions
            {
                
            };
            var attributes = new NSDictionary();
            var size = text.BoundingRectWithSize(
                new CGSize(this.messageTextField.Frame.Width, 999999f),
                options,
                attributes
            );
            */

            View.SetFrameSize(new CGSize(
                View.Frame.Width,
                tv.Frame.Height + this.nameTextField.Frame.Height));
        }


    }
}
