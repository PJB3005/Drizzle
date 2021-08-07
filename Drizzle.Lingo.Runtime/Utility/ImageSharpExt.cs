using System;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Drizzle.Lingo.Runtime
{
    public static class ImageSharpExt
    {
        private const string KRITA = @"C:\Program Files\Krita (x64)\bin\krita.exe";

        public static void ShowImage(this Image img)
        {
            var tmp = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
            {
                using var file = File.OpenWrite(tmp);
                img.SaveAsPng(file);
            }

            Process.Start(new ProcessStartInfo(KRITA)
            {
                UseShellExecute = true,
                ArgumentList = { tmp }
            });
        }
    }
}
