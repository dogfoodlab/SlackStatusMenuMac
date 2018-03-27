using AppKit;
using Foundation;
using CoreGraphics;
using System;

namespace SlackStatusMenuMac.Extensions
{
    public static class NSImageExtensions
    {
        public static NSImage TintColor(this NSImage image, NSColor color)
        {
            try
            {
                image.LockFocus();
                color.Set();
                var rect = new CGRect(0, 0, image.Size.Width, image.Size.Height);
                NSGraphics.RectFill(rect, NSCompositingOperation.SourceAtop);
            }
            finally
            {
                image.UnlockFocus();
            }
            return image;
        }

        public static NSImage SetTemplate(this NSImage image)
        {
            image.Template = true;
            return image;
        }


    }
}
