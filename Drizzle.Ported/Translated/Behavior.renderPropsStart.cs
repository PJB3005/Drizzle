using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderPropsStart
//
public sealed class renderPropsStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic val = null;
dynamic a = null;
_movieScript.global_c = 1;
_movieScript.global_keeplooping = 1;
_movieScript.global_aftereffects = LingoGlobal.op_gt(_global._movie.frame,51);
_movieScript.global_glastimported = @"";
_movieScript.global_gcurrentlyrenderingtrash = LingoGlobal.FALSE;
if (((_movieScript.global_grendertrashprops.count > 0) & (_movieScript.global_aftereffects == 0))) {
_movieScript.global_gcurrentlyrenderingtrash = LingoGlobal.TRUE;
}
for (int tmp_q = 0; tmp_q <= 29; tmp_q++) {
q = tmp_q;
_global.sprite((50-q)).loc = LingoGlobal.point(((1024/2)-q),((768/2)-q));
val = ((LingoGlobal.floatmember_helper(q)+new LingoDecimal(1))/new LingoDecimal(30));
_global.sprite((50-q)).color = _global.color((val*255),(val*255),(val*255));
}
_movieScript.global_propstorender = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= _movieScript.global_gpeprops.props.count; tmp_a++) {
a = tmp_a;
_movieScript.global_propstorender.add(_movieScript.global_gpeprops.props[a]);
_movieScript.global_propstorender[_movieScript.global_propstorender.count].addat(1,_movieScript.global_propstorender[_movieScript.global_propstorender.count][5].settings.renderorder);
}
_movieScript.global_propstorender.sort();
for (int tmp_a = 1; tmp_a <= _movieScript.global_propstorender.count; tmp_a++) {
a = tmp_a;
_movieScript.global_propstorender[a].deleteat(1);
}
_movieScript.global_softprop = LingoGlobal.VOID;

return null;
}
}
}
