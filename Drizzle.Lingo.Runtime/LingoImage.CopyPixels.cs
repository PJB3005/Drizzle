using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using SixLabors.ImageSharp.PixelFormats;

namespace Drizzle.Lingo.Runtime;

public sealed unsafe partial class LingoImage
{
    // Not used by the editor (but there is a similar lingo API)
    // Mostly just for unit tests right now.
    // Maybe optimizations for the editor later.
    public void fill(LingoColor color)
    {
        CopyIfShared();

        switch (Depth)
        {
            case 32:
                FillCore<PixelOpsBgra32, Bgra32>(this, color);
                break;
            case 16:
                FillCore<PixelOpsBgra5551, Bgra5551>(this, color);
                break;
            case 8:
                FillCore<PixelOpsPalette8, L8>(this, color);
                break;
            case 1:
                FillCore<PixelOpsBit, int>(this, color);
                break;
        }
    }

    private static void FillCore<TWriter, TDstData>(LingoImage dst, LingoColor color)
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : struct
    {
        var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);
        TWriter.Fill(dstSpan, color.BitPack);
    }


    public void copypixels(LingoImage source, LingoList destQuad, LingoRect sourceRect, LingoPropertyList paramList)
    {
        ParseCommonCopyPixelsParameters(paramList, out var parameters);

        var quad = new DestQuad
        {
            TopLeft = ((LingoPoint)destQuad.List[0]!).AsVector2,
            TopRight = ((LingoPoint)destQuad.List[1]!).AsVector2,
            BottomRight = ((LingoPoint)destQuad.List[2]!).AsVector2,
            BottomLeft = ((LingoPoint)destQuad.List[3]!).AsVector2,
        };

        CopyPixelsQuadImpl(source, this, quad, sourceRect, parameters);
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
        ParseCommonCopyPixelsParameters(paramList, out var parameters);

        CopyPixelsImpl(source, this, destRect, sourceRect, parameters);
    }

    private static void ParseCommonCopyPixelsParameters(
        LingoPropertyList paramList,
        out CopyPixelsParameters parameters)
    {
        parameters = default;
        if (paramList.Dict.TryGetValue(new LingoSymbol("blend"), out var blendValObj))
        {
            var dec = (LingoNumber)blendValObj!;
            parameters.Blend = (float)(dec.DecimalValue / 100f);
        }
        else
        {
            parameters.Blend = 1;
        }

        if (paramList.Dict.TryGetValue(new LingoSymbol("color"), out var colorObj))
            parameters.ForeColor = (LingoColor)colorObj!;

        if (paramList.Dict.TryGetValue(new LingoSymbol("ink"), out var inkVal))
            parameters.Ink = (CopyPixelsInk)(int)inkVal!;

        if (paramList.Dict.TryGetValue(new LingoSymbol("mask"), out var mask)
            || paramList.Dict.TryGetValue(new LingoSymbol("maskImage"), out mask)
            || paramList.Dict.TryGetValue(new LingoSymbol("maskimage"), out mask))
            parameters.Mask = (LingoMask?)mask;
    }

    private static void CopyPixelsQuadImpl(
        LingoImage source,
        LingoImage dest,
        in DestQuad destQuad,
        LingoRect sourceRect,
        in CopyPixelsParameters parameters)
    {
        dest.CopyIfShared();
        var srcBox = CalcSrcBox(source, sourceRect);

        CopyPixelsQuadGenWriter(source, dest, destQuad, srcBox, parameters);
    }

    private static void CopyPixelsQuadGenWriter(
        LingoImage src, LingoImage dst,
        in DestQuad destQuad,
        Vector4 srcBox,
        in CopyPixelsParameters parameters)
    {
        switch (dst.Depth)
        {
            case 32:
                CopyPixelsQuadGenSampler<Bgra32, PixelOpsBgra32>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            case 16:
                CopyPixelsQuadGenSampler<Bgra5551, PixelOpsBgra5551>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            case 8:
                CopyPixelsQuadGenSampler<L8, PixelOpsPalette8>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            case 1:
                CopyPixelsQuadGenSampler<int, PixelOpsBit>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            default:
                // Not implemented.
                break;
        }
    }

    private static void CopyPixelsQuadGenSampler<TDstData, TWriter>(
        LingoImage src, LingoImage dst,
        in DestQuad destQuad,
        Vector4 srcBox,
        in CopyPixelsParameters parameters)
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        switch (src.Depth)
        {
            case 32:
                CopyPixelsQuadCoreScalar<Bgra32, PixelOpsBgra32, TDstData, TWriter>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            case 16:
                CopyPixelsQuadCoreScalar<Bgra5551, PixelOpsBgra5551, TDstData, TWriter>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            case 8:
                CopyPixelsQuadCoreScalar<L8, PixelOpsPalette8, TDstData, TWriter>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            case 1:
                CopyPixelsQuadCoreScalar<int, PixelOpsBit, TDstData, TWriter>(
                    src,
                    dst,
                    destQuad,
                    srcBox,
                    parameters);
                break;
            default:
                // Not implemented.
                break;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static void CopyPixelsQuadCoreScalar<TSrcData, TSampler, TDstData, TWriter>(
        LingoImage src, LingoImage dst,
        in DestQuad destQuad,
        Vector4 srcBox,
        in CopyPixelsParameters parameters)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        var srcImgW = src.Width;
        var srcImgH = src.Height;
        var dstImgW = dst.Width;
        var dstImgH = dst.Height;

        var boundsTL = Vector2.Min(
            destQuad.TopLeft,
            Vector2.Min(
                destQuad.TopRight,
                Vector2.Min(destQuad.BottomLeft, destQuad.BottomRight)));

        var boundsBR = Vector2.Max(
            destQuad.TopLeft,
            Vector2.Max(
                destQuad.TopRight,
                Vector2.Max(destQuad.BottomLeft, destQuad.BottomRight)));

        var boundL = Math.Clamp((int)boundsTL.X, 0, dstImgW);
        var boundT = Math.Clamp((int)boundsTL.Y, 0, dstImgH);
        var boundR = Math.Clamp((int)MathF.Ceiling(boundsBR.X), 0, dstImgW);
        var boundB = Math.Clamp((int)MathF.Ceiling(boundsBR.Y), 0, dstImgH);

        var doBackgroundTransparent = parameters.Ink == CopyPixelsInk.BackgroundTransparent;

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        var doBlend = parameters.Blend != 1;
        var fgc = parameters.ForeColor;

        ReadOnlySpan<TSrcData> srcSpan = MemoryMarshal.Cast<byte, TSrcData>(src.ImageBuffer);
        var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);

        // TODO: guard against degenerate shape?

        for (var y = boundT; y < boundB; y++)
        {
            for (var x = boundL; x < boundR; x++)
            {
                var p = new Vector2(x + 0.5f, y + 0.5f);

                var st = InvBilinear(p, destQuad.TopLeft, destQuad.TopRight, destQuad.BottomRight, destQuad.BottomLeft);

                if (MathF.Max(MathF.Abs(st.X - 0.5f), MathF.Abs(st.Y - 0.5f)) < 0.5f)
                {
                    st.X = Lerp(srcBox.X, srcBox.Z, st.X);
                    st.Y = Lerp(srcBox.Y, srcBox.W, st.Y);

                    var dstPos = dstImgW * y + x;

                    var imgRow = (int)(st.Y * srcImgH) * srcImgW;
                    var color = DoSample<TSrcData, TSampler>(st.X, st.Y, srcImgW, srcSpan, imgRow);

                    if (doBackgroundTransparent && color == LingoColor.PackWhite)
                        continue;

                    CopyPixelsCoreDoOutputScalar<TSrcData, TSampler, TDstData, TWriter>(
                        parameters, color, fgc, doBlend, dstSpan, dstPos);
                }
            }
        }

        static float Lerp(float x, float y, float a) => x + a * (y - x);

        static float Cross2d(Vector2 a, Vector2 b) => a.X * b.Y - a.Y * b.X;

        // https://iquilezles.org/www/articles/ibilinear/ibilinear.htm
        static Vector2 InvBilinear(Vector2 p, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            Vector2 res;

            var e = b - a;
            var f = d - a;
            var g = a - b + c - d;
            var h = p - a;

            var k2 = Cross2d(g, f);
            var k1 = Cross2d(e, f) + Cross2d(h, g);
            var k0 = Cross2d(h, e);

            // if edges are parallel, this is a linear equation
            if (MathF.Abs(k2) < 0.001)
            {
                res = new Vector2((h.X * k1 + f.X * k0) / (e.X * k1 - g.X * k0), -k0 / k1);
            }
            // otherwise, it's a quadratic
            else
            {
                var w = k1 * k1 - 4.0f * k0 * k2;
                if (w < 0.0) return new Vector2(-1.0f);
                w = MathF.Sqrt(w);

                var ik2 = 0.5f / k2;
                var v = (-k1 - w) * ik2;
                var u = (h.X - f.X * v) / (e.X + g.X * v);

                if (u < 0.0 || u > 1.0 || v < 0.0 || v > 1.0)
                {
                    v = (-k1 + w) * ik2;
                    u = (h.X - f.X * v) / (e.X + g.X * v);
                }

                res = new Vector2(u, v);
            }

            return res;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private static void CopyPixelsQuadCoreScalarRasterize<TSrcData, TSampler, TDstData, TWriter>(
        LingoImage src,
        LingoImage dst,
        Vector2 v0, Vector2 v0st,
        Vector2 v1, Vector2 v1st,
        Vector2 v2, Vector2 v2st,
        in CopyPixelsParameters parameters)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        var srcImgW = src.Width;
        var srcImgH = src.Height;
        var dstImgW = dst.Width;
        var dstImgH = dst.Height;

        var boundsTL = Vector2.Min(
            v0,
            Vector2.Min(v1, v2));

        var boundsBR = Vector2.Max(
            v0,
            Vector2.Max(v1, v2));

        var boundL = Math.Clamp((int)boundsTL.X, 0, dstImgW);
        var boundT = Math.Clamp((int)boundsTL.Y, 0, dstImgH);
        var boundR = Math.Clamp((int)MathF.Ceiling(boundsBR.X), 0, dstImgW);
        var boundB = Math.Clamp((int)MathF.Ceiling(boundsBR.Y), 0, dstImgH);

        var doBackgroundTransparent = parameters.Ink == CopyPixelsInk.BackgroundTransparent;
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        var doBlend = parameters.Blend != 1;
        var fgc = parameters.ForeColor;

        ReadOnlySpan<TSrcData> srcSpan = MemoryMarshal.Cast<byte, TSrcData>(src.ImageBuffer);
        var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);

        var area = Math.Abs(EdgeFunction(v2, v1, v0));

        if (area == 0)
            // Degenerate triangle, do not render.
            return;

        for (var y = boundT; y < boundB; y++)
        {
            for (var x = boundL; x < boundR; x++)
            {
                var p = new Vector2(x + 0.5f, y + 0.5f);

                var w0 = EdgeFunction(p, v2, v1);
                var w1 = EdgeFunction(p, v0, v2);
                var w2 = EdgeFunction(p, v1, v0);

                if (w0 >= 0 && w1 >= 0 && w2 >= 0 || w0 <= 0 && w1 <= 0 && w2 <= 0)
                {
                    w0 /= area;
                    w1 /= area;
                    w2 /= area;

                    w0 = Math.Abs(w0);
                    w1 = Math.Abs(w1);
                    w2 = Math.Abs(w2);

                    var st = w0 * v0st + w1 * v1st + w2 * v2st;

                    var dstPos = dstImgW * y + x;

                    var imgRow = (int)(st.Y * srcImgH) * srcImgW;
                    var color = DoSample<TSrcData, TSampler>(st.X, st.Y, srcImgW, srcSpan, imgRow);

                    if (doBackgroundTransparent && color == LingoColor.PackWhite)
                        continue;

                    CopyPixelsCoreDoOutputScalar<TSrcData, TSampler, TDstData, TWriter>(
                        parameters, color, fgc, doBlend, dstSpan, dstPos);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        static float EdgeFunction(Vector2 a, Vector2 b, Vector2 c)
        {
            return (c.X - a.X) * (b.Y - a.Y) - (c.Y - a.Y) * (b.X - a.X);
        }
    }

    private static void CopyPixelsImpl(
        LingoImage source,
        LingoImage dest,
        LingoRect destRect,
        LingoRect sourceRect,
        in CopyPixelsParameters parameters)
    {
        dest.CopyIfShared();
        Debug.Assert(!dest.IsPxl);

        // Integer coordinates for the purpose of rasterization.
        var dstL = (int)destRect.left;
        var dstT = (int)destRect.top;
        var dstR = (int)destRect.right;
        var dstB = (int)destRect.bottom;

        if (dstL > dest.width || dstT > dest.height || dstR < 0 || dstB < 0)
        {
            //Log.Debug("copyPixels(): ignoring complete out-of-bounds write.");
            return;
        }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        // todo: make sure to not apply this when mask is set.
        if (source.IsPxl && parameters.Blend == 1 &&
            parameters.Ink is CopyPixelsInk.Copy or CopyPixelsInk.BackgroundTransparent)
        {
            // CopiesPixelFast += 1;
            CopyPixelsPxlRectGenWriter(dest, (dstL, dstT, dstR, dstB), parameters);
            return;
        }

        var srcBox = CalcSrcBox(source, sourceRect);

        CopyPixelsRectGenWriter(source, dest, srcBox, (dstL, dstT, dstR, dstB), parameters);
    }

    private static Vector4 CalcSrcBox(LingoImage source, LingoRect sourceRect)
    {
        // Float coordinates for the purposes of sampling.
        var srcL = (float)(sourceRect.left / source.width.DecimalValue);
        var srcT = (float)(sourceRect.top / source.height.DecimalValue);
        var srcR = (float)(sourceRect.right / source.width.DecimalValue);
        var srcB = (float)(sourceRect.bottom / source.height.DecimalValue);

        // LTRB
        var srcBox = new Vector4(srcL, srcT, srcR, srcB);
        return srcBox;
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
                CopyPixelsRectGenSampler<Bgra32, PixelOpsBgra32>(
                    src,
                    dst,
                    srcBox,
                    dstBox,
                    parameters);
                break;
            case 16:
                CopyPixelsRectGenSampler<Bgra5551, PixelOpsBgra5551>(
                    src,
                    dst,
                    srcBox,
                    dstBox,
                    parameters);
                break;
            case 8:
                CopyPixelsRectGenSampler<L8, PixelOpsPalette8>(
                    src,
                    dst,
                    srcBox,
                    dstBox,
                    parameters);
                break;
            case 1:
                CopyPixelsRectGenSampler<int, PixelOpsBit>(
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
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        switch (src.Depth)
        {
            case 32:
                CopyPixelsRectCoreCopy<Bgra32, PixelOpsBgra32, TDstData, TWriter>(
                    src,
                    dst,
                    srcBox,
                    dstBox,
                    parameters);
                break;
            case 16:
                CopyPixelsRectCoreCopy<Bgra5551, PixelOpsBgra5551, TDstData, TWriter>(
                    src,
                    dst,
                    srcBox,
                    dstBox,
                    parameters);
                break;
            case 8:
                CopyPixelsRectCoreCopy<L8, PixelOpsPalette8, TDstData, TWriter>(
                    src,
                    dst,
                    srcBox,
                    dstBox,
                    parameters);
                break;
            case 1:
                CopyPixelsRectCoreCopy<int, PixelOpsBit, TDstData, TWriter>(
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
    private static void CopyPixelsRectCoreCopy<TSrcData, TSampler, TDstData, TWriter>(
        LingoImage src, LingoImage dst,
        Vector4 srcBox,
        (int l, int t, int r, int b) dstBox,
        in CopyPixelsParameters parameters)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        // Advanced copy features not implemented on AVX code path, they're rare so it's fine probably.
        var mustScalar = parameters.Ink == CopyPixelsInk.Darkest || parameters.Mask != null;
        if (Avx2.IsSupported && !mustScalar)
            CopyPixelsRectCoreCopyAvx2<TSrcData, TSampler, TDstData, TWriter>(
                src, dst,
                srcBox, dstBox,
                parameters);
        else
            CopyPixelsRectCoreCopyScalar<TSrcData, TSampler, TDstData, TWriter>(
                src, dst,
                srcBox, dstBox,
                parameters);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static void CopyPixelsRectCoreCopyScalar<TSrcData, TSampler, TDstData, TWriter>(
        LingoImage src, LingoImage dst,
        Vector4 srcBox,
        (int l, int t, int r, int b) dstBox,
        in CopyPixelsParameters parameters)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        var (dstL, dstT, dstR, dstB) = dstBox;

        var (initS, initT, incSrcS, incSrcT) =
            CopyPixelsRectCoreCopyCalcSampleCoords(srcBox, dstL, dstT, dstR, dstB);

        var srcImgW = src.Width;
        var srcImgH = src.Height;
        var dstImgW = dst.Width;
        var dstImgH = dst.Height;

        var doBackgroundTransparent = parameters.Ink == CopyPixelsInk.BackgroundTransparent;
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        var doBlend = parameters.Blend != 1;
        var fgc = parameters.ForeColor;

        CopyPixelsRectCoreCopyClampDst(ref dstL, ref dstR, ref initS, incSrcS, dstImgW);
        CopyPixelsRectCoreCopyClampDst(ref dstT, ref dstB, ref initT, incSrcT, dstImgH);

        ReadOnlySpan<TSrcData> srcSpan = MemoryMarshal.Cast<byte, TSrcData>(src.ImageBuffer);
        var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);

        var t = initT;
        for (var y = dstT; y < dstB; y++, t += incSrcT)
        {
            var imgRow = (int)(t * srcImgH) * srcImgW;
            var s = initS;
            for (var x = dstL; x < dstR; x++, s += incSrcS)
            {
                var dstPos = dstImgW * y + x;

                if (parameters.Mask is { } mask)
                {
                    var maskX = (int)(s * srcImgW);
                    var maskY = (int)(t * srcImgH);

                    if (maskX < 0 || maskY < 0 || maskX >= mask.Width || maskY >= mask.Height)
                        continue;

                    if (DoBitRead(MemoryMarshal.Cast<byte, int>(mask.Data), mask.Width * maskY + maskX))
                        continue;
                }

                var color = DoSample<TSrcData, TSampler>(s, t, srcImgW, srcSpan, imgRow);

                if (doBackgroundTransparent && color == LingoColor.PackWhite)
                    continue;

                CopyPixelsCoreDoOutputScalar<TSrcData, TSampler, TDstData, TWriter>(
                    parameters, color, fgc, doBlend, dstSpan, dstPos);
            }
        }
    }

    private static void CopyPixelsCoreDoOutputScalar<TSrcData, TSampler, TDstData, TWriter>(
        in CopyPixelsParameters parameters,
        int color,
        LingoColor fgc,
        bool doBlend,
        Span<TDstData> dstSpan,
        int dstPos)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        var unpacked = LingoColor.BitUnpack(color);
        int r = unpacked.RedByte;
        int g = unpacked.GreenByte;
        int b = unpacked.BlueByte;

        r = Math.Min(0xFF, r + fgc.RedByte);
        g = Math.Min(0xFF, g + fgc.GreenByte);
        b = Math.Min(0xFF, b + fgc.BlueByte);

        if (doBlend)
        {
            var unpackedDst = LingoColor.BitUnpack(TWriter.Sample(dstSpan, dstPos));

            var blendSrc = new Vector4(r, g, b, 0);
            var blendDst = new Vector4(unpackedDst.RedByte, unpackedDst.GreenByte, unpackedDst.BlueByte, 0);

            var final = blendSrc * parameters.Blend + blendDst * (1 - parameters.Blend);

            r = (int)final.X;
            g = (int)final.Y;
            b = (int)final.Z;
        }
        else if (parameters.Ink == CopyPixelsInk.Darkest)
        {
            var existing = LingoColor.BitUnpack(TWriter.Sample(dstSpan, dstPos));

            r = Math.Min(r, existing.RedByte);
            g = Math.Min(g, existing.GreenByte);
            b = Math.Min(b, existing.BlueByte);
        }

        TWriter.Write(dstSpan, dstPos, new LingoColor(r, g, b).BitPack);
    }

    private static int DoSample<TSrcData, TSampler>(
        float s,
        float t,
        int srcImgW,
        ReadOnlySpan<TSrcData> srcSpan,
        int imgRow)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
    {
        if (s < 0 || s >= 1 || t < 0 || t >= 1)
            return LingoColor.PackWhite;

        var imgX = (int)(s * srcImgW);

        return TSampler.Sample(srcSpan, imgRow + imgX);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static void CopyPixelsRectCoreCopyAvx2<TSrcData, TSampler, TDstData, TWriter>(
        LingoImage src, LingoImage dst,
        Vector4 srcBox,
        (int l, int t, int r, int b) dstBox,
        in CopyPixelsParameters parameters)
        where TSampler : struct, IPixelOps<TSrcData>
        where TSrcData : unmanaged
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        var (dstL, dstT, dstR, dstB) = dstBox;

        var (initS, initT, incSrcS, incSrcT) =
            CopyPixelsRectCoreCopyCalcSampleCoords(srcBox, dstL, dstT, dstR, dstB);

        ReadOnlySpan<TSrcData> srcSpan = MemoryMarshal.Cast<byte, TSrcData>(src.ImageBuffer);
        var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);

        var srcImgW = src.Width;
        var srcImgH = src.Height;
        var dstImgW = dst.Width;
        var dstImgH = dst.Height;

        CopyPixelsRectCoreCopyClampDst(ref dstL, ref dstR, ref initS, incSrcS, dstImgW);
        CopyPixelsRectCoreCopyClampDst(ref dstT, ref dstB, ref initT, incSrcT, dstImgH);

        var doBackgroundTransparent = parameters.Ink == CopyPixelsInk.BackgroundTransparent;
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        var doBlend = parameters.Blend != 1;
        var fgc = parameters.ForeColor;
        var fgVec = Vector256.Create(fgc.BitPack).AsByte();

        var vecIncS = Avx.Multiply(Vector256.Create(0f, 1f, 2f, 3f, 4f, 5f, 6f, 7f), Vector256.Create(incSrcS));

        var posMask = Vector256.Create(0, 1, 2, 3, 4, 5, 6, 7);
        var widthVec = Vector256.Create(dstR);

        var initVecS = Vector256.Create(initS);
        var incVecS = Vector256.Create(incSrcS * 8);

        var t = initT;
        for (var y = dstT; y < dstB; y++, t += incSrcT)
        {
            var imgRow = (int)(t * srcImgH) * srcImgW;
            var s = initVecS;
            for (var x = dstL; x < dstR; x += 8, s = Avx.Add(s, incVecS))
            {
                var pos = Avx2.Add(posMask, Vector256.Create(x));
                var writeMask = Avx2.CompareGreaterThan(widthVec, pos);

                // Color vectors on AVX contain BGRA32 data per lane.

                Vector256<int> color;
                if (t is < 0 or >= 1)
                    color = Vector256<int>.AllBitsSet;
                else
                {
                    var vecS = Avx.Add(s, vecIncS);
                    var coord = Avx.ConvertToVector256Int32WithTruncation(
                        Avx.Multiply(vecS, Vector256.Create((float)srcImgW)));

                    var lowerCoord = coord.GetLower();
                    var l0 = DoSample(srcSpan, srcImgW, imgRow, lowerCoord.GetElement(0));
                    var l1 = DoSample(srcSpan, srcImgW, imgRow, lowerCoord.GetElement(1));
                    var l2 = DoSample(srcSpan, srcImgW, imgRow, lowerCoord.GetElement(2));
                    var l3 = DoSample(srcSpan, srcImgW, imgRow, lowerCoord.GetElement(3));
                    var lowerSample = Vector128.Create(l0, l1, l2, l3);

                    var upperCoord = coord.GetUpper();
                    var u0 = DoSample(srcSpan, srcImgW, imgRow, upperCoord.GetElement(0));
                    var u1 = DoSample(srcSpan, srcImgW, imgRow, upperCoord.GetElement(1));
                    var u2 = DoSample(srcSpan, srcImgW, imgRow, upperCoord.GetElement(2));
                    var u3 = DoSample(srcSpan, srcImgW, imgRow, upperCoord.GetElement(3));
                    var upperSample = Vector128.Create(u0, u1, u2, u3);

                    color = Vector256.Create(lowerSample, upperSample);
                }

                // Don't write if sampled color is white (== transparent)
                if (doBackgroundTransparent)
                    writeMask = Avx2.AndNot(Avx2.CompareEqual(color, Vector256<int>.AllBitsSet), writeMask);

                // Add foreground color.
                color = Avx2.AddSaturate(color.AsByte(), fgVec).AsInt32();

                var dstPos = dstImgW * y + x;

                if (doBlend)
                {
                    var blendVec = Vector256.Create(parameters.Blend);
                    var dstColor = TWriter.Read8(dstSpan, dstPos, writeMask);

                    var res = DoBlend8Avx2(color.AsByte(), dstColor.AsByte(), blendVec);

                    color = res.AsInt32();
                }

                TWriter.Write8(dstSpan, dstPos, color, writeMask);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        static int DoSample(ReadOnlySpan<TSrcData> srcSpan, int srcImgW, int imgRow, int imgX)
        {
            if (imgX < 0 || imgX >= srcImgW)
                return LingoColor.PackWhite;

            return TSampler.Sample(srcSpan, imgRow + imgX);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static Vector256<byte> DoBlend8Avx2(Vector256<byte> src, Vector256<byte> dst, Vector256<float> blend)
    {
        var res = Vector256.Create(0xFF_00_00_00).AsByte();

        var blendInv = Avx.Subtract(Vector256.Create(1f), blend);

        var bMask = Vector256.Create(
            0, 255, 255, 255,
            4, 255, 255, 255,
            8, 255, 255, 255,
            12, 255, 255, 255,
            0, 255, 255, 255,
            4, 255, 255, 255,
            8, 255, 255, 255,
            12, 255, 255, 255);

        var srcBlue = Avx2.Shuffle(src, bMask);
        var dstBlue = Avx2.Shuffle(dst, bMask);

        var resBlue = DoSingleBlend(srcBlue, dstBlue, blend, blendInv);

        var bInvMask = Vector256.Create(
            0, 255, 255, 255,
            4, 255, 255, 255,
            8, 255, 255, 255,
            12, 255, 255, 255,
            0, 255, 255, 255,
            4, 255, 255, 255,
            8, 255, 255, 255,
            12, 255, 255, 255);

        res = Avx2.Or(res, Avx2.Shuffle(resBlue, bInvMask));

        var gMask = Vector256.Create(
            1, 255, 255, 255,
            5, 255, 255, 255,
            9, 255, 255, 255,
            13, 255, 255, 255,
            1, 255, 255, 255,
            5, 255, 255, 255,
            9, 255, 255, 255,
            13, 255, 255, 255);

        var srcGreen = Avx2.Shuffle(src, gMask);
        var dstGreen = Avx2.Shuffle(dst, gMask);

        var resGreen = DoSingleBlend(srcGreen, dstGreen, blend, blendInv);

        var gInvMask = Vector256.Create(
            255, 0, 255, 255,
            255, 4, 255, 255,
            255, 8, 255, 255,
            255, 12, 255, 255,
            255, 0, 255, 255,
            255, 4, 255, 255,
            255, 8, 255, 255,
            255, 12, 255, 255);

        res = Avx2.Or(res, Avx2.Shuffle(resGreen, gInvMask));

        var rMask = Vector256.Create(
            2, 255, 255, 255,
            6, 255, 255, 255,
            10, 255, 255, 255,
            14, 255, 255, 255,
            2, 255, 255, 255,
            6, 255, 255, 255,
            10, 255, 255, 255,
            14, 255, 255, 255);

        var scrRed = Avx2.Shuffle(src, rMask);
        var dstRed = Avx2.Shuffle(dst, rMask);

        var resRed = DoSingleBlend(scrRed, dstRed, blend, blendInv);

        var rInvMask = Vector256.Create(
            255, 255, 0, 255,
            255, 255, 4, 255,
            255, 255, 8, 255,
            255, 255, 12, 255,
            255, 255, 0, 255,
            255, 255, 4, 255,
            255, 255, 8, 255,
            255, 255, 12, 255);

        return Avx2.Or(res, Avx2.Shuffle(resRed, rInvMask));

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        static Vector256<byte> DoSingleBlend(
            Vector256<byte> srcColor, Vector256<byte> dstColor,
            Vector256<float> blend, Vector256<float> blendInv)
        {
            var srcFloat = Avx.ConvertToVector256Single(srcColor.AsInt32());
            var dstFloat = Avx.ConvertToVector256Single(dstColor.AsInt32());

            var resFloat = Avx.Add(
                Avx.Multiply(srcFloat, blend),
                Avx.Multiply(dstFloat, blendInv)
            );

            var res = Avx.ConvertToVector256Int32WithTruncation(resFloat);
            return res.AsByte();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static void CopyPixelsRectCoreCopyClampDst(
        ref int dst0,
        ref int dst1,
        ref float initTex,
        float incSrcTex,
        int dstImg)
    {
        if (dst0 < 0)
        {
            initTex += -dst0 * incSrcTex;
            dst0 = 0;
        }

        dst1 = Math.Min(dst1, dstImg);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static (float initS, float initT, float incSrcS, float incSrcT) CopyPixelsRectCoreCopyCalcSampleCoords(
        Vector4 srcBox, int dstL, int dstT, int dstR, int dstB)
    {
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

        return (initS, initT, incSrcS, incSrcT);
    }

    private static void CopyPixelsPxlRectGenWriter(
        LingoImage dst,
        (int l, int t, int r, int b) dstBox,
        in CopyPixelsParameters parameters)
    {
        switch (dst.Depth)
        {
            case 32:
                CopyPixelsPxlRectCore<Bgra32, PixelOpsBgra32>(
                    dst,
                    dstBox,
                    parameters);
                break;
            case 16:
                CopyPixelsPxlRectCore<Bgra5551, PixelOpsBgra5551>(
                    dst,
                    dstBox,
                    parameters);
                break;
            case 1:
                CopyPixelsPxlRectCore<int, PixelOpsBit>(
                    dst,
                    dstBox,
                    parameters);
                break;
            default:
                // Not implemented.
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private static void CopyPixelsPxlRectCore<TDstData, TWriter>(
        LingoImage dst,
        (int l, int t, int r, int b) dstBox,
        in CopyPixelsParameters parameters)
        where TWriter : struct, IPixelOps<TDstData>
        where TDstData : unmanaged
    {
        var dstSpan = MemoryMarshal.Cast<byte, TDstData>(dst.ImageBuffer);
        var (dstL, dstT, dstR, dstB) = dstBox;

        dstL = Math.Clamp(dstL, 0, dst.Width);
        dstT = Math.Clamp(dstT, 0, dst.Height);
        dstR = Math.Clamp(dstR, 0, dst.Width);
        dstB = Math.Clamp(dstB, 0, dst.Height);

        // todo: remove round trip to Vector4 here please.
        var fgc = parameters.ForeColor;
        var packed = fgc.BitPack;

        if (dstL == 0 && dstT == 0 && dstR == dst.width && dstB == dst.height)
        {
            // Writing to the whole image with pxl is commonly used as a fill operation.
            TWriter.Fill(dstSpan, packed);
            return;
        }

        var dstWidth = dst.Width;

        for (var y = dstT; y < dstB; y++)
        {
            for (var x = dstL; x < dstR; x++)
            {
                TWriter.Write(dstSpan, y * dstWidth + x, packed);
            }
        }
    }

    private struct CopyPixelsParameters
    {
        public CopyPixelsInk Ink;
        public float Blend;

        public LingoColor ForeColor;
        public LingoMask? Mask;
    }

    private enum CopyPixelsInk
    {
        Copy = 0,
        BackgroundTransparent = 36,
        Darkest = 39
    }

    private interface IPixelOps<TPixel>
    {
        static abstract int Sample(ReadOnlySpan<TPixel> srcDat, int rowMajorPos);
        static abstract Vector256<int> Read8(ReadOnlySpan<TPixel> dstDat, int rowMajorPos0, Vector256<int> readMask);
        static abstract void Write(Span<TPixel> dstDat, int rowMajorPos, int value);

        static abstract void Write8(Span<TPixel> dstDat, int rowMajorPos0, Vector256<int> pixelData,
            Vector256<int> writeMask);

        static abstract void Fill(Span<TPixel> dstDat, int value);
    }

    private struct PixelOpsBgra32 : IPixelOps<Bgra32>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static int Sample(ReadOnlySpan<Bgra32> srcDat, int rowMajorPos)
        {
            ref readonly var px = ref srcDat[rowMajorPos];
            return Unsafe.As<Bgra32, int>(ref Unsafe.AsRef(px));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Vector256<int> Read8(ReadOnlySpan<Bgra32> dstDat, int rowMajorPos0, Vector256<int> readMask)
        {
            fixed (Bgra32* px = &dstDat[rowMajorPos0])
            {
                return Avx2.MaskLoad((int*)px, readMask);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Span<Bgra32> dstDat, int rowMajorPos, int value)
        {
            dstDat[rowMajorPos] = Unsafe.As<int, Bgra32>(ref value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write8(Span<Bgra32> dstDat, int rowMajorPos0, Vector256<int> pixelData,
            Vector256<int> writeMask)
        {
            fixed (Bgra32* ptr = &dstDat[rowMajorPos0])
            {
                var iPtr = (int*)ptr;
                Avx2.MaskStore(iPtr, writeMask, pixelData);
            }
        }

        public static void Fill(Span<Bgra32> dstDat, int value)
        {
            dstDat.Fill(Unsafe.As<int, Bgra32>(ref value));
        }
    }

    private struct PixelOpsBgra5551 : IPixelOps<Bgra5551>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static int Sample(ReadOnlySpan<Bgra5551> srcDat, int rowMajorPos)
        {
            // TODO: Make this fast.
            var bgra5551 = srcDat[rowMajorPos];
            var bgra = new Bgra32();
            bgra.FromBgra5551(bgra5551);
            return (int)bgra.PackedValue;
        }

        public static Vector256<int> Read8(ReadOnlySpan<Bgra5551> dstDat, int rowMajorPos0, Vector256<int> readMask)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Span<Bgra5551> dstDat, int rowMajorPos, int value)
        {
            dstDat[rowMajorPos].FromBgra32(Unsafe.As<int, Bgra32>(ref value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write8(
            Span<Bgra5551> dstDat,
            int rowMajorPos0,
            Vector256<int> pixelData,
            Vector256<int> writeMask)
        {
            // TODO: Make this fast.
            dstDat = dstDat[rowMajorPos0..];
            dstDat = dstDat[..Math.Min(8, dstDat.Length)];
            for (var i = 0; i < dstDat.Length; i++)
            {
                var elem = pixelData.GetElement(i);
                dstDat[i].FromBgra32(Unsafe.As<int, Bgra32>(ref elem));
            }
        }

        public static void Fill(Span<Bgra5551> dstDat, int value)
        {
            var px = new Bgra5551();
            px.FromBgra32(Unsafe.As<int, Bgra32>(ref value));
            dstDat.Fill(px);
        }
    }

    private struct PixelOpsPalette8 : IPixelOps<L8>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static int Sample(ReadOnlySpan<L8> srcDat, int rowMajorPos)
        {
            var px = srcDat[rowMajorPos].PackedValue;
            var lingoColor = (LingoColor)px;
            return lingoColor.BitPack;
        }

        public static Vector256<int> Read8(ReadOnlySpan<L8> dstDat, int rowMajorPos0, Vector256<int> readMask)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Span<L8> dstDat, int rowMajorPos, int value)
        {
            dstDat[rowMajorPos] = ToPalettized(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write8(Span<L8> dstDat, int rowMajorPos0, Vector256<int> pixelData, Vector256<int> writeMask)
        {
            // todo: make this fast.

            dstDat = dstDat[rowMajorPos0..];
            dstDat = dstDat[..Math.Min(8, dstDat.Length)];

            for (var i = 0; i < dstDat.Length; i++)
            {
                if (writeMask.GetElement(i) == 0)
                    continue;

                dstDat[i] = ToPalettized(pixelData.GetElement(i));
            }
        }

        public static void Fill(Span<L8> dstDat, int value)
        {
            dstDat.Fill(ToPalettized(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static L8 ToPalettized(int color)
        {
            // Red.
            if (color == LingoColor.PackRed)
                return new L8(6);

            // Black.
            if (color == LingoColor.PackBlack)
                return new L8(255);

            // White.
            return new L8(0);
        }
    }

    private struct PixelOpsBit : IPixelOps<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static int Sample(ReadOnlySpan<int> srcDat, int rowMajorPos)
        {
            return DoBitRead(srcDat, rowMajorPos) ? LingoColor.PackWhite : LingoColor.PackBlack;
        }

        public static Vector256<int> Read8(ReadOnlySpan<int> dstDat, int rowMajorPos0, Vector256<int> readMask)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write(Span<int> dstDat, int rowMajorPos, int value)
        {
            DoBitWrite(dstDat, rowMajorPos, value == LingoColor.PackWhite);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void Write8(Span<int> dstDat, int rowMajorPos0, Vector256<int> pixelData,
            Vector256<int> writeMask)
        {
            // todo: do these writes on int instead idk.
            var bytes = MemoryMarshal.Cast<int, byte>(dstDat);

            var isBlack = Avx2.CompareEqual(pixelData, Vector256.Create(LingoColor.PackWhite));
            var bits = Avx.MoveMask(isBlack.AsSingle());
            var writeMaskBit = Avx.MoveMask(writeMask.AsSingle());
            bits &= writeMaskBit;

            var pos = rowMajorPos0 >> 3;
            var posRem = rowMajorPos0 & 7;

            if (posRem == 0)
            {
                // Aligned yay.
                ref var dst = ref bytes[pos];

                dst &= (byte)~writeMaskBit;
                dst |= (byte)bits;
            }
            else
            {
                // 1010_1010
                //
                // 32       40
                // 4    <   5   >
                // 0000_0000 0000_0000
                ref var dstA = ref bytes[pos];

                dstA &= (byte)(~(writeMaskBit << posRem));
                dstA |= (byte)(bits << posRem);

                var secondMask = (byte)(~(writeMaskBit >> (8 - posRem)));
                // if secondMask is 0xFF no bits would be written, skip write to avoid OOB.
                if (secondMask == 0xFF)
                    return;

                ref var dstB = ref bytes[pos + 1];

                dstB &= secondMask;
                dstB |= (byte)(bits >> (8 - posRem));
            }
        }

        public static void Fill(Span<int> dstDat, int value)
        {
            dstDat.Fill(value != LingoColor.PackBlack ? -1 : 0);
        }
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

    private struct DestQuad
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomRight;
        public Vector2 BottomLeft;
    }
}
