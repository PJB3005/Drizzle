using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: envEditorLoop
//
public sealed class envEditorLoop : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic fac = null;
dynamic rct = null;
dynamic q = null;
dynamic h = null;
dynamic waterlevel = null;
_global.script(@"levelOverview").gotoeditor();
if ((_movieScript.global_gloprops.size.loch > _movieScript.global_gloprops.size.locv)) {
fac = (new LingoDecimal(1024)/_movieScript.global_gloprops.size.loch);
}
else {
fac = (new LingoDecimal(768)/_movieScript.global_gloprops.size.locv);
}
rct = (LingoGlobal.rect((1024/2),(768/2),(1024/2),(768/2))+LingoGlobal.rect(((-_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*fac),((-_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*fac),((_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*fac),((_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*fac)));
for (int tmp_q = 1; tmp_q <= 2; tmp_q++) {
q = tmp_q;
_global.sprite(q).rect = rct;
}
_global.sprite(4).rect = rct;
if ((_movieScript.global_genveditorprops.waterlevel >= 0)) {
_global.sprite(3).visibility = LingoGlobal.TRUE;
_global.sprite(5).visibility = LingoGlobal.TRUE;
h = (rct.bottom-(((_movieScript.global_genveditorprops.waterlevel+_movieScript.global_gloprops.extratiles[4])+new LingoDecimal(0.5))*fac));
_global.sprite(3).rect = LingoGlobal.rect((rct.left-2),(h-1),(rct.right+2),768);
_global.sprite(5).rect = LingoGlobal.rect((rct.left-2),(h-1),(rct.right+2),768);
if (LingoGlobal.ToBool(_movieScript.global_genveditorprops.waterinfront)) {
_global.sprite(3).blend = 5;
_global.sprite(5).blend = 50;
}
else {
_global.sprite(3).blend = 50;
_global.sprite(5).blend = 5;
}
}
else {
_global.sprite(3).visibility = LingoGlobal.FALSE;
_global.sprite(5).visibility = LingoGlobal.FALSE;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"l"))) {
waterlevel = (((_movieScript.global_gloprops.size.locv-_movieScript.global_gloprops.extratiles[2])-_movieScript.global_gloprops.extratiles[4])-(_global._mouse.mouseloc.locv/fac).integer);
_movieScript.global_genveditorprops.waterlevel = waterlevel;
}
if (LingoGlobal.ToBool(_global.script(@"envEditorStart").checkkey(@"w"))) {
if ((_movieScript.global_genveditorprops.waterlevel == -1)) {
_movieScript.global_genveditorprops.waterlevel = (_movieScript.global_gloprops.size.locv/2);
}
else {
_movieScript.global_genveditorprops.waterlevel = -1;
}
}
if (LingoGlobal.ToBool(_global.script(@"envEditorStart").checkkey(@"f"))) {
_movieScript.global_genveditorprops.waterinfront = (1-_movieScript.global_genveditorprops.waterinfront);
}
_global.go(_global.the_frame);

return null;
}
}
}
