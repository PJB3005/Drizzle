using Drizzle.Lingo.Runtime;
using NUnit.Framework;

namespace Drizzle.Lingo.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [TestOf(typeof(LingoImage))]
    public sealed class ImageTest
    {
        [Test]
        public void TestCopyPixelsMove()
        {
            // Set up source image to have a blue-bordered red/green checkerboard in the center.
            // Then copy the shape to another image.
            var src = new LingoImage(128, 128, 32);
            var dst = new LingoImage(128, 128, 32);

            for (var y = 32; y < 96; y++)
            {
                for (var x = 32; x < 96; x++)
                {
                    if (x == 32 || x == 95 || y == 32 || y == 95)
                    {
                        src.setpixel(x, y, new LingoColor(0, 0, 255));
                    }
                    else
                    {
                        var color = x % 2 == 0 ^ y % 2 == 0 ? new LingoColor(255, 0, 0) : new LingoColor(0, 255, 0);
                        src.setpixel(x, y, color);
                    }
                }
            }

            // copy
            dst.copypixels(src, new LingoRect(16, 16, 80, 80), new LingoRect(32, 32, 96, 96));

            // assert
            for (var y = 0; y < 64; y++)
            {
                for (var x = 0; x < 64; x++)
                {
                    var srcPx = src.getpixel(x + 32, y + 32);
                    var dstPx = dst.getpixel(x + 16, y + 16);

                    Assert.That(srcPx, Is.EqualTo(dstPx));
                }
            }
        }

        [Test]
        public void TestCopyPixelsMove1Bit()
        {
            // Like before, but with a 1 bit image. No border since that isn't possible on one bit.
            var src = new LingoImage(128, 128, 1);
            var dst = new LingoImage(128, 128, 1);

            for (var y = 32; y < 96; y++)
            {
                for (var x = 32; x < 96; x++)
                {
                    var color = x % 2 == 0 ^ y % 2 == 0 ? 255 : 0;
                    src.setpixel(x, y, color);
                }
            }

            // copy
            dst.copypixels(src, new LingoRect(16, 16, 80, 80), new LingoRect(32, 32, 96, 96));

            // assert
            for (var y = 0; y < 64; y++)
            {
                for (var x = 0; x < 64; x++)
                {
                    var srcPx = src.getpixel(x + 32, y + 32);
                    var dstPx = dst.getpixel(x + 16, y + 16);

                    Assert.That(srcPx, Is.EqualTo(dstPx));
                }
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestCopyPixelsPxl(bool pxlFlag)
        {
            var pxl = new LingoImage(1, 1, 32);
            pxl.setpixel(0, 0, 255);
            pxl.IsPxl = pxlFlag;
            var dst = new LingoImage(128, 128, 1);

            dst.copypixels(pxl, new LingoRect(32, 32, 96, 96), pxl.rect);

            // assert
            for (var y = 32; y < 96; y++)
            {
                for (var x = 32; x < 96; x++)
                {
                    var dstPx = dst.getpixel(x, y);

                    Assert.That(dstPx, Is.EqualTo(new LingoColor(0, 0, 0)));
                }
            }
        }

        [Test]
        public void TestMakeSilhouette()
        {
            var src = new LingoImage(32, 32, 32);
            for (var y = 8; y < 24; y++)
            {
                for (var x = 8; x < 24; x++)
                {
                    src.setpixel(x, y, 255);
                }
            }

            var silhouette = src.makesilhouette(1);

            for (var y = 0; y < 32; y++)
            {
                for (var x = 0; x < 32; x++)
                {
                    var px = silhouette.getpixel(x, y);
                    var shouldBeInSilhouette = x is >= 8 and < 24 && y is >= 8 and < 24;
                    LingoColor expectedPixel = shouldBeInSilhouette ? 0 : 255;

                    Assert.That(px, Is.EqualTo(expectedPixel));
                }
            }
        }
    }
}
