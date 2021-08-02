using System;
using System.Diagnostics;
using System.IO;
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

        public void copypixels(LingoImage source, LingoList destQuad, LingoRect sourceRect)
        {
            copypixels(source, destQuad, sourceRect, new LingoPropertyList());
        }

        public void copypixels(LingoImage source, LingoList destQuad, LingoRect sourceRect, LingoPropertyList paramList)
        {
            Log.Warning("copyPixels() destquad: not implemented");
        }

        public void copypixels(LingoImage source, LingoRect destRect, LingoRect sourceRect)
        {
            copypixels(source, destRect, sourceRect, new LingoPropertyList());
        }

        public void copypixels(LingoImage source, LingoRect destRect, LingoRect sourceRect, LingoPropertyList paramList)
        {
            if (paramList.length != 0)
            {
                //    Log.Warning("Advanced copypixels() not implemented");
                //    throw new NotImplementedException("Advanced copy operations not implemented.");
            }

            if (destRect.width != sourceRect.width || destRect.height != sourceRect.height)
            {
                /*Log.Debug(
                    "copyPixels() stretching: {SrcW}x{SrcH} -> {DstW}x{DstH}",
                    sourceRect.width, sourceRect.height, destRect.width, destRect.height);*/
            }

            var srcImg = source.Image;

            var srcRect = Rectangle.FromLTRB(
                Math.Clamp((int) sourceRect.left, 0, srcImg.Width),
                Math.Clamp((int) sourceRect.top, 0, srcImg.Height),
                Math.Clamp((int) sourceRect.right, 0, srcImg.Width),
                Math.Clamp((int) sourceRect.bottom, 0, srcImg.Height));

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

        public LingoColor getpixel(LingoPoint point)
        {
            return getpixel(point.loch, point.locv);
        }

        public LingoColor getpixel(LingoDecimal x, LingoDecimal y)
        {
            return getpixel((int) x, (int) y);
        }

        public LingoColor getpixel(int x, int y)
        {
            switch (Image)
            {
                case Image<Bgra32> bgra32:
                {
                    var bgra = bgra32[x, y];
                    return new LingoColor(bgra.R, bgra.G, bgra.B);
                }
            }

            // Log.Warning("getpixel unimplemented image depth: {ImageDepth}", Depth);
            return default;
        }

        public void setpixel(LingoPoint point, LingoColor color)
        {
            setpixel(point.loch, point.locv, color);
        }

        public void setpixel(LingoDecimal x, LingoDecimal y, LingoColor color)
        {
            setpixel((int)x, (int)y, color);
        }

        public void setpixel(int x, int y, LingoColor color)
        {
            switch (Image)
            {
                case Image<Bgra32> bgra32:
                {
                    bgra32[x, y] = new Bgra32((byte)color.red, (byte)color.green, (byte)color.red, 255);
                    return;
                }
            }

            // Log.Warning("setpixel unimplemented image depth: {ImageDepth}", Depth);
            return;
        }

        public LingoImage duplicate()
        {
            return new LingoImage(Image.Clone(_ => { }), Depth);
        }

        public dynamic createmask()
        {
            Log.Warning("createmask(): Not implemented");
            return null;
        }

        public void ShowImage()
        {
            var tmp = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            {
                using var file = File.OpenWrite(tmp);
                Image.SaveAsPng(file);
            }

            Process.Start(new ProcessStartInfo(tmp) { UseShellExecute = true });
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
