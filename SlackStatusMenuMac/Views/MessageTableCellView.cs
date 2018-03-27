using AppKit;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlackStatusMenuMac.Views
{
    public partial class MessageTableCellView : AppKit.NSTableCellView
    {
        #region Constructors

        public MessageTableCellView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public MessageTableCellView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        #endregion


    }
}
