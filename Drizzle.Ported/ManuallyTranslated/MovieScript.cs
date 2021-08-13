using System;
using Drizzle.Lingo.Runtime;

namespace Drizzle.Ported {
    public sealed partial class MovieScript {
        public int diag(LingoPoint a, LingoPoint b) {
            LingoDecimal h = a.locv - b.locv;
            LingoDecimal w = a.loch - b.loch;
            return (int)LingoGlobal.sqrt(w * w + h * h);
        }
    }
}
