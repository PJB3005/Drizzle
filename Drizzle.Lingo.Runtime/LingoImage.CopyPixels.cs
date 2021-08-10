using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Serilog;
using SixLabors.ImageSharp.PixelFormats;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoImage
    {
        /*
             private static int CopiesPixelFast;
   private static int CopiesEqualNonStretch;
        private static int CopiesNonStretch;
        private static int CopiesEqual;
        private static int CopiesEqualTall;
        private static int CopiesOther;

        private static Dictionary<(int w, int h), int> Sizes = new();
        */

        public void copypixels(LingoImage source, LingoList destQuad, LingoRect sourceRect, LingoPropertyList paramList)
        {
            Log.Warning("copyPixels() destquad: not implemented");
        }

        public void copypixels(LingoImage source, LingoRect destRect, LingoRect sourceRect)
        {
            var parameters = new CopyPixelsParameters
            {
                Blend = 1,
                Ink = CopyPixelsInk.Copy
            };

            CopyPixelsImpl(source, this, destRect, sourceRect, parameters);
        }

        public void copypixels(LingoImage source, LingoRect destRect, LingoRect sourceRect, LingoPropertyList paramList)
        {
            var parameters = new CopyPixelsParameters();
            if (paramList.Dict.TryGetValue(new LingoSymbol("blend"), out var blendValObj))
            {
                var dec = (LingoDecimal)blendValObj!;
                parameters.Blend = (float)(dec.Value / 100f);
            }
            else
            {
                parameters.Blend = 1;
            }

            if (paramList.Dict.TryGetValue(new LingoSymbol("color"), out var colorObj))
                parameters.ForeColor = (LingoColor)colorObj!;

            if (paramList.Dict.TryGetValue(new LingoSymbol("ink"), out var inkVal))
                parameters.Ink = (CopyPixelsInk)(int)inkVal!;

            if (paramList.Dict.ContainsKey(new LingoSymbol("mask")))
                Log.Warning("copypixels(): Mask rendering not implemented");

            CopyPixelsImpl(source, this, destRect, sourceRect, parameters);
        }

        private static void CopyPixelsImpl(
            LingoImage source,
            LingoImage dest,
            LingoRect destRect,
            LingoRect sourceRect,
            in CopyPixelsParameters parameters)
        {
            Debug.Assert(!dest.IsPxl);

            /*
            if (destRect.width == sourceRect.width && destRect.height == sourceRect.height)
            {
                if (source.Depth == dest.Depth)
                    CopiesEqualNonStretch += 1;
                else
                    CopiesNonStretch += 1;
            }
            else
            {
                if (source.Depth == dest.Depth)
                {
                    CopiesEqual += 1;
                }
                else
                    CopiesOther += 1;
            }

            if (source.width == 1 && source.height == 1 && parameters.Ink != CopyPixelsInk.Copy)
            {
                var tup = ((int)destRect.width, (int)destRect.height);
                if (!Sizes.ContainsKey(tup))
                    Sizes[tup] = 0;

                CollectionsMarshal.GetValueRefOrNullRef(Sizes, tup) += 1;
            }
            */

            // Integer coordinates for the purpose of rasterization.
            var dstL = destRect.left.integer;
            var dstT = destRect.top.integer;
            var dstR = destRect.right.integer;
            var dstB = destRect.bottom.integer;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            // todo: make sure to not apply this when mask is set.
            if (source.IsPxl && parameters.Blend == 1 && parameters.Ink == CopyPixelsInk.Copy)
            {
                // CopiesPixelFast += 1;
                CopyPixelsPxlRectGenWriter(dest, (dstL, dstT, dstR, dstB), parameters);
                return;
            }

            if (parameters.Ink == CopyPixelsInk.Darkest)
                Log.Warning("copypixels(): Darkest ink not implemented");

            // Float coordinates for the purposes of sampling.
            var srcL = (float)(sourceRect.left / source.width);
            var srcT = (float)(sourceRect.top / source.height);
            var srcR = (float)(sourceRect.right / source.width);
            var srcB = (float)(sourceRect.bottom / source.height);

            // LTRB
            var srcBox = new Vector4(srcL, srcT, srcR, srcB);

            CopyPixelsRectGenWriter(source, dest, srcBox, (dstL, dstT, dstR, dstB), parameters);
        }

        private static void CopyPixelsRectGenWriter(
            LingoImage src, LingoImage dst,
            Vector4 srcBox,
            (int l, int t, int r, int b) dstBox,
            in CopyPixelsParameters parameters)
        {
            switch (dst.Depth)
            {
                case 32:
                    CopyPixelsRectGenSampler<Bgra32, PixelWriterRgb<Bgra32>>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                case 16:
                    CopyPixelsRectGenSampler<Bgra5551, PixelWriterRgb<Bgra5551>>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                case 8:
                    CopyPixelsRectGenSampler<L8, PixelWriterPalette8>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                case 1:
                    CopyPixelsRectGenSampler<int, PixelWriterBit>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                default:
                    // Not implemented.
                    break;
            }
        }

        private static void CopyPixelsRectGenSampler<TDstData, TWriter>(
            LingoImage src, LingoImage dst,
            Vector4 srcBox,
            (int l, int t, int r, int b) dstBox,
            in CopyPixelsParameters parameters)
            where TWriter : struct, IPixelWriter<TDstData>
            where TDstData : unmanaged
        {
            switch (src.Depth)
            {
                case 32:
                    CopyPixelsRectCoreCopy<Bgra32, PixelSamplerRgb<Bgra32>, TDstData, TWriter>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                case 16:
                    CopyPixelsRectCoreCopy<Bgra5551, PixelSamplerRgb<Bgra5551>, TDstData, TWriter>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                case 8:
                    CopyPixelsRectCoreCopy<L8, PixelSamplerPalette8, TDstData, TWriter>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                case 1:
                    CopyPixelsRectCoreCopy<int, PixelSamplerBit, TDstData, TWriter>(
                        src,
                        dst,
                        srcBox,
                        dstBox,
                        parameters);
                    break;
                default:
                    // Not implemented.
                    break;
            }
        }

        // Struct generics for static dispatch.
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static void CopyPixelsRectCoreCopy<TSrcData, TSampler, TDstData, TWriter>(
            LingoImage src, LingoImage dst,
            Vector4 srcBox,
            (int l, int t, int r, int b) dstBox,
            in CopyPixelsParameters parameters)
            where TSampler : struct, IPixelSampler<TSrcData>
            where TSrcData : unmanaged
            where TWriter : struct, IPixelWriter<TDstData>
            where TDstData : unmanaged
        {
            var (dstL, dstT, dstR, dstB) = dstBox;

            var srcW = srcBox.Z - srcBox.X;
            var srcH = srcBox.W - srcBox.Y;
            var dstW = dstR - dstL;
            var dstH = dstB - dstT;

            // Horizontal increment for sampling coordinates when the rasterizer iterates.
            var incSrcS = srcW / dstW;
            var incSrcT = srcH / dstH;

            // Half-texel offset so we sample the *center* of the pixels, not the edges.
            var initS = srcW / (dstW * 2) + srcBox.X;
            var initT = srcH / (dstH * 2) + srcBox.Y;

            var sampler = new TSampler();
            var writer = new TWriter();

            var srcSpan = MemoryMarshal.Cast<byte, TSrcData>(src.ImageBuffer);
            var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);

            var srcImgW = src.width;
            var srcImgH = src.height;
            var dstImgW = dst.width;
            var dstImgH = dst.height;

            var doBackgroundTransparent = parameters.Ink == CopyPixelsInk.BackgroundTransparent;
            var fgc = parameters.ForeColor;
            var fg = new Vector4(fgc.red / 255f, fgc.green / 255f, fgc.blue / 255f, 0f);

            var t = initT;
            for (var y = dstT; y < dstB; y++, t += incSrcT)
            {
                if (y < 0 || y >= dstImgH)
                    continue;

                var s = initS;
                for (var x = dstL; x < dstR; x++, s += incSrcS)
                {
                    if (x < 0 || x >= dstImgW)
                        continue;

                    Vector4 color;
                    if (s < 0 || s >= 1 || t < 0 || t >= 1)
                        color = Vector4.One;
                    else
                        color = sampler.Sample(srcSpan, srcImgW, srcImgH, new Vector2(s, t));

                    if (!doBackgroundTransparent || color != Vector4.One)
                    {
                        color += fg;
                        writer.Write(dstSpan, dstImgW * y + x, color);
                    }
                }
            }
        }

        private static void CopyPixelsPxlRectGenWriter(
            LingoImage dst,
            (int l, int t, int r, int b) dstBox,
            in CopyPixelsParameters parameters)
        {
            switch (dst.Depth)
            {
                case 32:
                    CopyPixelsPxlRectCore<Bgra32, PixelWriterRgb<Bgra32>>(
                        dst,
                        dstBox,
                        parameters);
                    break;
                case 16:
                    CopyPixelsPxlRectCore<Bgra5551, PixelWriterRgb<Bgra5551>>(
                        dst,
                        dstBox,
                        parameters);
                    break;
                case 1:
                    CopyPixelsPxlRectCore<int, PixelWriterBit>(
                        dst,
                        dstBox,
                        parameters);
                    break;
                default:
                    // Not implemented.
                    break;
            }
        }

        private static void CopyPixelsPxlRectCore<TDstData, TWriter>(
            LingoImage dst,
            (int l, int t, int r, int b) dstBox,
            in CopyPixelsParameters parameters)
            where TWriter : struct, IPixelWriter<TDstData>
            where TDstData : unmanaged
        {
            var writer = new TWriter();
            var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);
            var (dstL, dstT, dstR, dstB) = dstBox;

            dstL = Math.Clamp(dstL, 0, dst.width);
            dstT = Math.Clamp(dstT, 0, dst.height);
            dstR = Math.Clamp(dstR, 0, dst.width);
            dstB = Math.Clamp(dstB, 0, dst.height);

            // todo: remove round trip to Vector4 here please.
            var fgc = parameters.ForeColor;
            var fg = new Vector4(fgc.red / 255f, fgc.green / 255f, fgc.blue / 255f, 1f);

            if (dstL == 0 && dstT == 0 && dstR == dst.width && dstB == dst.height)
            {
                // Writing to the whole image with pxl is commonly used as a fill operation.
                writer.Fill(dstSpan, fg);
                return;
            }

            var dstWidth = dst.width;

            for (var y = dstT; y < dstB; y++)
            {
                for (var x = dstL; x < dstR; x++)
                {
                    writer.Write(dstSpan, y * dstWidth + x, fg);
                }
            }
        }

        private struct CopyPixelsParameters
        {
            public CopyPixelsInk Ink;
            public float Blend;

            public LingoColor ForeColor;
            // todo: mask
        }

        private enum CopyPixelsInk
        {
            Copy = 0,
            BackgroundTransparent = 36,
            Darkest = 39
        }

        private interface IPixelSampler<TPixel>
        {
            Vector4 Sample(ReadOnlySpan<TPixel> srcDat, int srcWidth, int srcHeight, Vector2 pos);
        }

        private interface IPixelWriter<TPixel>
        {
            void Write(Span<TPixel> dstDat, int rowMajorPos, Vector4 value);
            void Fill(Span<TPixel> dstDat, Vector4 value);
        }

        // Bgra32 and Bgra5551 image sampler/writer.
        // Can use common generic code thanks to ImageSharp pixel helpers.
        private struct PixelSamplerRgb<TPixel> : IPixelSampler<TPixel>
            where TPixel : unmanaged, IPixel<TPixel>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public Vector4 Sample(ReadOnlySpan<TPixel> srcDat, int srcWidth, int srcHeight, Vector2 pos)
            {
                var (x, y) = DoStandardSample(pos, srcWidth, srcHeight);

                var rowMajor = x + y * srcWidth;
                var px = srcDat[rowMajor];
                return px.ToVector4();
            }
        }

        private struct PixelWriterRgb<TPixel> : IPixelWriter<TPixel>
            where TPixel : unmanaged, IPixel<TPixel>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public void Write(Span<TPixel> dstDat, int rowMajorPos, Vector4 value)
            {
                dstDat[rowMajorPos].FromVector4(value);
            }

            public void Fill(Span<TPixel> dstDat, Vector4 value)
            {
                var px = new TPixel();
                px.FromVector4(value);
                dstDat.Fill(px);
            }
        }

        private struct PixelSamplerPalette8 : IPixelSampler<L8>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public Vector4 Sample(ReadOnlySpan<L8> srcDat, int srcWidth, int srcHeight, Vector2 pos)
            {
                var (x, y) = DoStandardSample(pos, srcWidth, srcHeight);

                var rowMajor = x + y * srcWidth;
                var px = srcDat[rowMajor].PackedValue;
                var lingoColor = (LingoColor)px;
                return new Vector4(lingoColor.red / 255f, lingoColor.green / 255f, lingoColor.blue / 255f, 1);
            }
        }

        private struct PixelWriterPalette8 : IPixelWriter<L8>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public void Write(Span<L8> dstDat, int rowMajorPos, Vector4 value)
            {
                dstDat[rowMajorPos] = ToPalettized(value);
            }

            public void Fill(Span<L8> dstDat, Vector4 value)
            {
                dstDat.Fill(ToPalettized(value));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            private static L8 ToPalettized(Vector4 value)
            {
                var r = (int)(value.X * 255);
                var g = (int)(value.Y * 255);
                var b = (int)(value.Z * 255);

                // Red.
                if (r == 255 && g == 0 && b == 0)
                    return new L8(6);

                // Black.
                if (r == 0 && g == 0 && b == 0)
                    return new L8(255);

                // White.
                return new L8(0);
            }
        }

        private struct PixelSamplerBit : IPixelSampler<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public Vector4 Sample(ReadOnlySpan<int> srcDat, int srcWidth, int srcHeight, Vector2 pos)
            {
                var (x, y) = DoStandardSample(pos, srcWidth, srcHeight);
                var rowMajor = x + y * srcWidth;
                return DoBitRead(srcDat, rowMajor) ? Vector4.One : new Vector4(0, 0, 0, 1);
            }
        }

        private struct PixelWriterBit : IPixelWriter<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public void Write(Span<int> dstDat, int rowMajorPos, Vector4 value)
            {
                DoBitWrite(dstDat, rowMajorPos, value.X != 0);
            }

            public void Fill(Span<int> dstDat, Vector4 value)
            {
                dstDat.Fill(value.X != 0 ? -1 : 0);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static (int x, int y) DoStandardSample(Vector2 pos, int srcWidth, int srcHeight)
        {
            var x = Math.Clamp((int)(pos.X * srcWidth), 0, srcWidth - 1);
            var y = Math.Clamp((int)(pos.Y * srcHeight), 0, srcHeight - 1);

            return (x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static void DoBitWrite(Span<int> buf, int rowMajorPos, bool white)
        {
            var bytePos = rowMajorPos >> 5;
            var mask = 1 << rowMajorPos;
            ref var val = ref buf[bytePos];

            if (white)
                val |= mask;
            else
                val &= ~mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static bool DoBitRead(ReadOnlySpan<int> buf, int rowMajorPos)
        {
            var bytePos = rowMajorPos >> 5;
            var mask = 1 << rowMajorPos;

            ref readonly var val = ref buf[bytePos];

            return (val & mask) != 0;
        }
    }
}
