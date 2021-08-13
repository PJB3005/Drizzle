using System;
using Drizzle.Lingo.Runtime;

namespace Drizzle.Ported {
    public sealed partial class applyBlur {
        public void exitframe() {
            // wrapped in an if(0)
        }

        public void newframe() {
            _global.sprite(59).locv = _movieScript.global_c - 8;
        }

        // doesn't appear to ever be called
        public void changelightrect(int lr, LingoPoint pnt) {
            if (pnt.loch < _movieScript.global_lightrects[lr].left) {
                _movieScript.global_lightrects[lr].left = pnt.loch;
            }
            if (pnt.loch > _movieScript.global_lightrects[lr].right) {
                _movieScript.global_lightrects[lr].right = pnt.loch;
            }
            if (pnt.locv < _movieScript.global_lightrects[lr].top) {
                _movieScript.global_lightrects[lr].top = pnt.locv;
            }
            if (pnt.locv > _movieScript.global_lightrects[lr].bottom) {
                _movieScript.global_lightrects[lr].bottom = pnt.locv;
            }
            _global.sprite((10 + lr)).rect = (_movieScript.global_lightrects[lr] + LingoGlobal.rect(-8, -16, -8, -16));
        }
    }
}
