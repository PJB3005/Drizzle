using System;
using System.Dynamic;

namespace Drizzle.Lingo.Runtime
{
    public sealed partial class LingoGlobal
    {
        public Movie _movie { get; private set; } = default!;

        public void go(int frame) => _movie.go(frame);
        public int the_frame => _movie.frame;

        public sealed class Movie
        {
            private readonly LingoGlobal _global;

            public Window window { get; }

            public Movie(LingoGlobal global)
            {
                _global = global;
                window = new Window(global);
            }

            public int frame => _global.LingoRuntime.CurrentFrame;

            public string path => throw new NotImplementedException();

            public dynamic stage => throw new NotImplementedException();

            public void go(int newFrame)
            {
                _global.LingoRuntime.ScoreGo(newFrame);
            }
        }

        public sealed class Window
        {
            public Window(LingoGlobal lingoGlobal)
            {

            }

            // Literally unused except for one set, just return it.
            public dynamic appearanceoptions => new ExpandoObject();
            public int resizable { get; set; }
            public LingoRect rect { get; set; }
            public LingoSymbol sizestate => new ("normal");
        }
    }
}
