using System;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Drizzle.Lingo.Runtime;
using SixLabors.ImageSharp.PixelFormats;

namespace Drizzle.Editor.Helpers;

public static class LingoImageAvaloniaHelper
{
    public static unsafe Bitmap LingoImageToBitmap(LingoImage img, bool thumbnail)
    {
        var finalImg = img;

        if (thumbnail)
        {
            var copyImg = new LingoImage(50, 50, 32);
            copyImg.copypixels(img, copyImg.rect, img.rect);
            finalImg = copyImg;
        }
        else if (img.Depth != 32)
        {
            var copyImg = new LingoImage(img.Width, img.Height, 32);
            copyImg.copypixels(img, img.rect, img.rect);
            finalImg = copyImg;
        }

        var bgra = finalImg;

        fixed (byte* data = finalImg.ImageBuffer)
        {
            return new Bitmap(
                PixelFormat.Bgra8888,
                AlphaFormat.Unpremul,
                (nint)data,
                new PixelSize(bgra.Width, bgra.Height),
                new Vector(96, 96),
                sizeof(Bgra32) * bgra.Width);
        }
    }

    public static unsafe void CopyToBitmap(LingoImage image, WriteableBitmap bitmap)
    {
        using var locked = bitmap.Lock();
        if (locked.Format != PixelFormat.Bgra8888 || image.Depth != 32)
            throw new InvalidOperationException();

        if (locked.Size.Width != image.Width || locked.Size.Height != image.Height)
            throw new InvalidOperationException();

        // Wow I can't believe that worked.
        var dstSpan = new Span<byte>((void*)locked.Address, locked.RowBytes * locked.Size.Height);
        image.ImageBufferNoPadding.CopyTo(dstSpan);
    }

}
