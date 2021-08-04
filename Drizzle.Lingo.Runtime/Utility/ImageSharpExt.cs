using System;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Drizzle.Lingo.Runtime
{
    public static class ImageSharpExt
    {
        public static void ShowImage(this Image img)
        {
            var tmp = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            {
                using var file = File.OpenWrite(tmp);
                img.SaveAsPng(file);
            }

            Process.Start(new ProcessStartInfo(tmp) { UseShellExecute = true });

        }
    }
}
