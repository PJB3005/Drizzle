using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoImage
    {
        public int Depth { get; }
        public byte[] ImageBuffer { get; }

        public LingoRect rect => new(0, 0, width, height);

        public int Width { get; }
        public int Height { get; }
        public LingoNumber width => Width;
        public LingoNumber height => Height;
        public LingoNumber depth => Depth;

        // So the editor does a *ton* of tiny copypixels() operations from the "pxl" cast member,
        // which basically amounts to line/rect drawings in a silly way.
        // Guess we're fast pathing this.
        public bool IsPxl { get; set; }

        public LingoImage(int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;

            ImageBuffer = NewImgBufferForDepth(width, height, depth);
        }

        public LingoImage(Image<Bgra32> image)
        {
            Width = image.Width;
            Height = image.Height;
            Depth = 32;

            ImageBuffer = NewImgBufferForDepth(image.Width, image.Height, 32);
            var span = image.GetSinglePixelSpan();
            span.CopyTo(MemoryMarshal.Cast<byte, Bgra32>(ImageBuffer));
        }

        private LingoImage(byte[] buffer, int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;

            ImageBuffer = buffer;
        }

        public void SaveAsPng(Stream stream)
        {
            using var img = GetImgSharpImage();
            img.SaveAsPng(stream);
        }

        public void copypixels(LingoImage source, LingoList destQuad, LingoRect sourceRect)
        {
            copypixels(source, destQuad, sourceRect, new LingoPropertyList());
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public LingoColor getpixel(LingoPoint point)
        {
            return getpixel(point.loch, point.locv);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public LingoColor getpixel(LingoNumber x, LingoNumber y)
        {
            return getpixel((int)x, (int)y);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public LingoColor getpixel(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return LingoColor.White;

            var idx = y * Width + x;
            switch (Depth)
            {
                case 32:
                {
                    var buf = MemoryMarshal.Cast<byte, Bgra32>(ImageBuffer);
                    ref var val = ref buf[idx];
                    return new LingoColor(val.R, val.G, val.B);
                }
                case 16:
                {
                    var buf = MemoryMarshal.Cast<byte, Bgra5551>(ImageBuffer);
                    ref var val = ref buf[idx];
                    var rgba = new Rgba32();
                    val.ToRgba32(ref rgba);
                    return new LingoColor(rgba.R, rgba.G, rgba.B);
                }
                case 1:
                {
                    var buf = MemoryMarshal.Cast<byte, int>(ImageBuffer);
                    return DoBitRead(buf, idx) ? new LingoColor(255, 255, 255) : default;
                }
                default:
                    Log.Warning("getpixel(): Unimplemented image depth: {Depth}", Depth);
                    return default;
            }

            // Log.Warning("getpixel unimplemented image depth: {ImageDepth}", Depth);
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void setpixel(LingoPoint point, LingoColor color)
        {
            setpixel(point.loch, point.locv, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void setpixel(LingoNumber x, LingoNumber y, LingoColor color)
        {
            setpixel((int)x, (int)y, color);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void setpixel(int x, int y, LingoColor color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;

            var idx = y * Width + x;
            switch (Depth)
            {
                case 32:
                {
                    var buf = MemoryMarshal.Cast<byte, Bgra32>(ImageBuffer);
                    ref var val = ref buf[idx];
                    val = new Bgra32((byte)color.red, (byte)color.green, (byte)color.blue, 255);
                    return;
                }
                case 16:
                {
                    var buf = MemoryMarshal.Cast<byte, Bgra5551>(ImageBuffer);
                    ref var val = ref buf[idx];
                    val.FromRgba32(new Rgba32((byte)color.red, (byte)color.green, (byte)color.blue, 255));
                    return;
                }
                case 1:
                {
                    var buf = MemoryMarshal.Cast<byte, int>(ImageBuffer);
                    var white = color.red != 0;

                    DoBitWrite(buf, idx, white);

                    return;
                }
                default:
                    Log.Warning("setpixel(): Unimplemented image depth: {Depth}", Depth);
                    break;
            }
        }

        public LingoImage duplicate()
        {
            var newBuf = GC.AllocateUninitializedArray<byte>(ImageBuffer.Length);
            ImageBuffer.AsSpan().CopyTo(newBuf);
            return new LingoImage(newBuf, Width, Height, Depth) { IsPxl = IsPxl };
        }

        public dynamic createmask()
        {
            Log.Warning("createmask(): Not implemented");
            return null;
        }

        public void ShowImage()
        {
            using var img = GetImgSharpImage();
            img.ShowImage();
        }

        public static LingoImage LoadFromPath(string path)
        {
            var img = Image.Load<Bgra32>(path);
            // TODO: loading of low-bit images, maybe.
            return new LingoImage(img);
        }

        public Image GetImgSharpImage()
        {
            var img = this;
            if (img.Depth is 8 or 1)
            {
                var copy = new LingoImage(Width, Height, 16);
                copy.copypixels(this, rect, rect);
                img = copy;
            }

            Image imgSharp;
            Span<byte> imgSharpSpan;
            if (img.Depth == 16)
            {
                var bgra5551 = new Image<Bgra5551>(img.Width, img.Height);
                imgSharp = bgra5551;
                imgSharpSpan = MemoryMarshal.Cast<Bgra5551, byte>(bgra5551.GetSinglePixelSpan());
            }
            else
            {
                Debug.Assert(img.Depth == 32);
                var bgra32 = new Image<Bgra32>(img.Width, img.Height);
                imgSharp = bgra32;
                imgSharpSpan = MemoryMarshal.Cast<Bgra32, byte>(bgra32.GetSinglePixelSpan());
            }

            img.ImageBuffer.CopyTo(imgSharpSpan);

            return imgSharp;
        }

        private static byte[] NewImgBufferForDepth(int width, int height, int depth)
        {
            if (depth is 2 or 4)
                throw new NotSupportedException();

            int size;
            if (depth == 1)
            {
                var bits = width * height;
                size = (bits + 7) >> 3;
                // Round up to 4 bytes so we can do 32-bit wide operations safely.
                var rem = size & 3;
                if (rem != 0)
                    size += 4 - rem;
            }
            else
            {
                size = width * height * (depth >> 3);
            }

            // 8-bit palettized needs to be initialized to 0 (0 == white).
            if (depth == 8)
                return new byte[size];

            // all other formats initialize to all 1s (white).
            // Use AllocateUninitializedArray
            var buf = GC.AllocateUninitializedArray<byte>(size);
            buf.AsSpan().Fill(255);
            return buf;
        }
    }
}
