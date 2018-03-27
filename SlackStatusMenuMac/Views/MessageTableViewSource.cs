using AppKit;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using SlackStatusMenuMac.Data;
using SlackStatusMenuMac.Extensions;

namespace SlackStatusMenuMac.Views
{
    public class MessageTableViewSource : AbstractTableViewSource<Slack.Message>
    {

        public override NSView GetViewForItem(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var message = this.source[(int)row];
            var viewController = new MessageTableCellViewController();
            viewController.LoadView();

            viewController.SetRowData(message);

            var view = viewController.View;
            //var size = view.FittingSize;

            return view;
        }

        public override nfloat GetRowHeight(NSTableView tableView, nint row)
        {
            var message = this.source[(int)row];
            var viewController = new MessageTableCellViewController();
            viewController.LoadView();

            viewController.SetRowData(message, sizeOnly: true);

            var view = viewController.View;
            //var size = view.FittingSize;

            return view.Frame.Height;
        }


    }
}
