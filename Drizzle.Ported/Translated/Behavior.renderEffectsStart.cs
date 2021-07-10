using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderEffectsStart
//
public sealed class renderEffectsStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic tm = null;
dynamic q = null;
dynamic val = null;
tm = _global._system.milliseconds;
for (int tmp_q = 0; tmp_q <= 29; tmp_q++) {
q = tmp_q;
_global.sprite((50-q)).loc = LingoGlobal.point(((1024/2)-q),((768/2)-q));
val = ((LingoGlobal.floatmember_helper(q)+new LingoDecimal(1))/new LingoDecimal(30));
_global.sprite((50-q)).color = _global.color((val*255),(val*255),(val*255));
}
_global.sprite(57).visibility = 0;
_global.sprite(58).visibility = 0;
_movieScript.global_vertrepeater = 100000;
if ((_movieScript.global_geeprops.effects.count > 0)) {
_movieScript.global_r = 0;
_movieScript.global_keeplooping = 1;
}
else {
_global.go(56);
}

return null;
}
}
}
