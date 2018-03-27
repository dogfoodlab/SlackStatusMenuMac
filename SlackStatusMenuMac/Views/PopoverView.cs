using AppKit;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlackStatusMenuMac.Views
{
    public partial class PopoverView : AppKit.NSView
    {
        #region Constructors

        public PopoverView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        [Export("initWithCoder:")]
        public PopoverView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        #endregion


    }
}
