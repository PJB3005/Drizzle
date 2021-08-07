using System;
using System.Numerics;
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

        // So the editor does a *ton* of tiny copypixels() operations from the "pxl" cast member,
        // which basically amounts to line/rect drawings in a silly way.
        // Guess we're fast pathing this.
        public bool IsPxl { get; set; }

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
            return getpixel((int)x, (int)y);
        }

        public LingoColor getpixel(int x, int y)
        {
            switch (Depth)
            {
                case 32:
                {
                    var bgra32 = (Image<Bgra32>)Image;
                    var bgra = bgra32[x, y];
                    return new LingoColor(bgra.R, bgra.G, bgra.B);
                }
                case 16:
                {
                    var bgra5551 = (Image<Bgra5551>)Image;
                    var rgba = new Rgba32();
                    bgra5551[x, y].ToRgba32(ref rgba);
                    return new LingoColor(rgba.R, rgba.G, rgba.B);
                }
                case 1:
                {
                    var l8 = (Image<L8>)Image;
                    var val = l8[x, y];
                    return val.PackedValue == 255 ? new LingoColor(255, 255, 255) : new LingoColor(0, 0, 0);
                }
                default:
                    Log.Warning("getpixel(): Unimplemented image depth: {Depth}", Depth);
                    return default;
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
            switch (Depth)
            {
                case 32:
                {
                    var bgra32 = (Image<Bgra32>)Image;
                    bgra32[x, y] = new Bgra32((byte)color.red, (byte)color.green, (byte)color.blue, 255);
                    return;
                }
                case 16:
                {
                    var bgra5551 = (Image<Bgra5551>)Image;
                    var val = new Bgra5551();
                    val.FromRgba32(new Rgba32((byte)color.red, (byte)color.green, (byte)color.blue, 255));
                    bgra5551[x, y] = val;
                    return;
                }
                case 1:
                {
                    var L8 = (Image<L8>)Image;
                    var white = color.red != 0;
                    L8[x, y] = white ? new L8(255) : new L8(0);
                    return;
                }
                default:
                    Log.Warning("setpixel(): Unimplemented image depth: {Depth}", Depth);
                    break;
            }
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
            if (Depth == 8)
            {
                var copy = new LingoImage(width, height, 32);
                copy.copypixels(this, rect, rect);
                copy.ShowImage();
                return;
            }

            Image.ShowImage();
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
                1 or 2 or 4 or 8 => new Image<L8>(width, height, depth == 1 ? new L8(255) : new L8(0)),
                16 => new Image<Bgra5551>(width, height, new Bgra5551(Vector4.One)),
                32 => new Image<Bgra32>(width, height, new Bgra32(255, 255, 255, 255)),
                _ => throw new ArgumentException("Invalid image depth", nameof(depth))
            };
        }
    }
}
