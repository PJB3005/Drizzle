using System;
using Drizzle.Lingo.Runtime;

namespace Drizzle.Ported {
    public sealed partial class cameraEditor {
        public void exitframe() {
            LingoDecimal fac = _movieScript.global_fac;
            LingoPoint size = _movieScript.global_gloprops.size;

            LingoRect rct;
            int q;
            int mouseovercamera;
            LingoPoint pos;
            int closestcorner;
            LingoPoint cornerpos;
            LingoPoint linepos;

            if (size.loch > size.locv) {
                fac = 1024 / size.loch;
            } else {
                fac = 768 / size.locv;
            }

            if (fac > 16) {
                fac = 16;
            }

            rct = new LingoRect(512, 384, 512, 384)
                + new LingoRect(-size.loch * (LingoDecimal)0.5 * fac,
                                -size.locv * (LingoDecimal)0.5 * fac,
                                 size.loch * (LingoDecimal)0.5 * fac,
                                 size.locv * (LingoDecimal)0.5 * fac);

            dynamic et = _movieScript.global_gloprops.extratiles;
            _global.sprite(90).rect = rct
                + new LingoRect(et[1] * fac, et[2] * fac, et[3] * fac, et[4] * fac);

            for (int tmp_q = 2; tmp_q <= 8; tmp_q++) {
                q = tmp_q;
                _global.sprite(q).rect = rct;
            }

            if (checkkey("n") & _movieScript.global_gcameraprops.cameras.count < 20) {
                _movieScript.global_gcameraprops.cameras.add(
                    new LingoPoint(size.loch * 10, size.locv * 10));
                _movieScript.global_gcameraprops.quads.add(
                    new LingoList {
                        new LingoList { 0, 0 },
                        new LingoList { 0, 0 },
                        new LingoList { 0, 0 },
                        new LingoList { 0, 0 }
                    });
                _movieScript.global_gcameraprops.selectedcamera = _movieScript.global_gcameraprops.cameras.count;
            }

            if(_movieScript.global_gcameraprops.selectedcamera > 0) {
                mouseovercamera = _movieScript.global_gcameraprops.selectedcamera;
            } else {
                mouseovercamera = 0;
                int smallestdist = 10000;

                for(int tmp_q = 1; tmp_q <= _movieScript.global_gcameraprops.cameras.count; tmp_q++) {
                    q = tmp_q;
                    pos = new LingoPoint(512, 384)
                        + new LingoPoint(size.loch * (LingoDecimal)0.5 * fac,
                                         size.locv * (LingoDecimal)0.5 * fac)
                        + _movieScript.global_gcameraprops.cameras[q] / 20 * fac
                        + new LingoPoint(35, 20) * fac;

                    int distance = _movieScript.diag(pos, _global._mouse.mouseloc);
                    if (distance < smallestdist) {
                        mouseovercamera = q;
                        smallestdist = distance;
                    }
                }
            }

            if(mouseovercamera > 0) {
                // point to continue from later
            }

            // make sure global_fac is global
            _movieScript.global_fac = fac;
        }

        public bool checkkey(string key) {
            // key pressed = keypressed(key) && !minimized 
            _movieScript.global_gcameraprops.keys[LingoGlobal.symbol(key)] =
                LingoGlobal.op_and(_global._key.keypressed(key),
                LingoGlobal.op_ne(_global._movie.window.sizestate, new LingoSymbol("minimized")));

            if (LingoGlobal.ToBool(
                _movieScript.global_gcameraprops.keys[LingoGlobal.symbol(key)] &
                LingoGlobal.op_eq_b(_movieScript.global_gcameraprops.lastkeys[LingoGlobal.symbol(key)],
                0))) {
                return true;
            }

            _movieScript.global_gcameraprops.lastkeys[LingoGlobal.symbol(key)] = _movieScript.global_gcameraprops.keys[LingoGlobal.symbol(key)];

            return false;
        }
    }
}
