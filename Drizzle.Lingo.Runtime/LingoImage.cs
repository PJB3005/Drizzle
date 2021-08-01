using System;
using Serilog;
using Serilog.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace Drizzle.Lingo.Runtime
{
    public sealed class LingoImage
    {
        public int Depth { get; }
        public Image Image { get; }

        public LingoRect rect => new LingoRect(0, 0, Image.Width, Image.Height);

        public int width => Image.Width;
        public int height => Image.Width;

        public LingoImage(int width, int height, int depth)
        {
            Image = NewImgForDepth(depth, width, height);

            Depth = depth;
        }

        public LingoImage(Image image, int depth)
        {
            Image = image;
            Depth = depth;
        }

        public void copypixels(LingoImage source, LingoRect destRect, LingoRect sourceRect)
        {
            copypixels(source, destRect, sourceRect, new LingoPropertyList());
        }

        public void copypixels(LingoImage source, LingoRect destRect, LingoRect sourceRect, LingoPropertyList paramList)
        {
            if (paramList.length != 0)
                throw new NotImplementedException("Advanced copy operations not implemented.");

            if (destRect.width != sourceRect.width || destRect.height != sourceRect.height)
            {
                Log.Debug(
                    "copyPixels() stretching: {SrcW}x{SrcH} -> {DstW}x{DstH}",
                    sourceRect.width, sourceRect.height, destRect.width, destRect.height);
            }

            var srcImg = source.Image;

            var srcRect = Rectangle.FromLTRB(
                Math.Clamp((int) sourceRect.left, 0, srcImg.Width),
                Math.Clamp((int) sourceRect.top, 0, srcImg.Height),
                Math.Min(srcImg.Width, (int) sourceRect.right),
                Math.Min(srcImg.Height, (int) sourceRect.bottom));

            if (srcRect.Width == 0 || srcRect.Height == 0)
            {
                Log.Debug("copyPixels() cropped rect empty, drawing nothing.");
                return;
            }

            var srcCropped = srcImg.Clone(ctx => ctx.Crop(srcRect));

            if (sourceRect.right > srcImg.Width
                || sourceRect.bottom > srcImg.Height
                || sourceRect.left < 0
                || sourceRect.top < 0)
            {
                Log.Debug("copyPixels() doing out-of bounds read");

                var padImage = NewImgForDepth(Depth, (int) sourceRect.width, (int) sourceRect.height);

                padImage.Mutate(
                    ctx =>
                    {
                        var point = new Point(Math.Max(0, (int) -sourceRect.left), Math.Max(0, (int) -sourceRect.top));
                        ctx.DrawImage(srcCropped, point, opacity: 1f);
                    });

                srcCropped = padImage;
            }

            Image.Mutate(c =>
            {
                var srcScaled = srcCropped.Clone(s =>
                    s.Resize((int) destRect.width, (int) destRect.height, new NearestNeighborResampler()));

                c.DrawImage(srcScaled, new Point((int) destRect.left, (int) destRect.top), PixelColorBlendingMode.Overlay, 1);
            });
        }

        public dynamic getpixel(int x, int y)
        {
            throw new NotImplementedException();
        }

        public dynamic getpixel(LingoPoint point)
        {
            throw new NotImplementedException();
        }

        public void setpixel(LingoDecimal x, LingoDecimal y, LingoColor color)
        {
            setpixel((int)x, (int)y, color);
        }

        public void setpixel(int x, int y, LingoColor color)
        {
            throw new NotImplementedException();
        }

        public void setpixel(LingoPoint point, LingoColor color)
        {
            setpixel(point.loch, point.locv, color);
        }

        public LingoImage duplicate()
        {
            return new LingoImage(Image.Clone(_ => { }), Depth);
        }

        public dynamic createmask()
        {
            throw new NotImplementedException();
        }

        public static LingoImage LoadFromPath(string path)
        {
            var img = Image.Load<Bgra32>(path);
            // TODO: loading of low-bit images.
            return new LingoImage(img, 32);
        }

        private static Image NewImgForDepth(int depth, int width, int height)
        {
            return depth switch
            {
                1 or 2 or 4 or 8 => new Image<A8>(width, height),
                16 => new Image<Bgra5551>(width, height),
                32 => new Image<Bgra32>(width, height),
                _ => throw new ArgumentException("Invalid image depth", nameof(depth))
            };
        }
    }
}
