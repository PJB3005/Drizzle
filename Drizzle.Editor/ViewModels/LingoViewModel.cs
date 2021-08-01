using System;
using Avalonia.Threading;
using Drizzle.Lingo.Runtime;
using Drizzle.Ported;
using Serilog;

namespace Drizzle.Editor.ViewModels
{
    public class LingoViewModel
    {
        public LingoRuntime Runtime { get; } = new(typeof(MovieScript).Assembly);
        private readonly DispatcherTimer _timer = new();

        public void Init()
        {
            // Run lingo at 60 FPS for now.
            _timer.Interval = TimeSpan.FromSeconds(1 / 60f);
            _timer.Tick += TimerTickLingo;
            _timer.Start();

            Runtime.Init();
        }

        private void TimerTickLingo(object? sender, EventArgs e)
        {
            Runtime.Tick();
        }
    }
}
