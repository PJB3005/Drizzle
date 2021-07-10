using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderLayers
//
public sealed class renderLayers : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic lightangle = null;
dynamic q = null;
lightangle = (_movieScript.degtovec(_movieScript.global_glighteprops.lightangle)*_movieScript.global_glighteprops.flatness);
for (int tmp_q = 0; tmp_q <= 29; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",q)).image = _global.image((100*20),(60*20),32);
_global.member(LingoGlobal.concat(@"gradientA",_global.@string(q))).image = _global.image((100*20),(60*20),16);
_global.member(LingoGlobal.concat(@"gradientB",_global.@string(q))).image = _global.image((100*20),(60*20),16);
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",q),@"dc")).image = _global.image((100*20),(60*20),32);
}
_global.member(@"rainBowMask").image = _global.image((100*20),(60*20),32);
_movieScript.renderlevel();
_movieScript.global_c = 1;

return null;
}
}
}
