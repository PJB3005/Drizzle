using System;
using System.Diagnostics;
using System.IO;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoImage
    {
        public int Depth { get; }
        public Image Image { get; }

        public LingoRect rect => new LingoRect(0, 0, Image.Width, Image.Height);

        public int width => Image.Width;
        public int height => Image.Height;

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
