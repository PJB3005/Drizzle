using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: levelEditor
//
public sealed class levelEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic rct = null;
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((LingoGlobal.ToBool(_global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q])) & (_movieScript.global_gdirectionkeys[q] == 0))) {
_movieScript.global_gleprops.campos = ((_movieScript.global_gleprops.campos+new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })[q])*((1+9)*_global._key.keypressed(83)));
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),1);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),2);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),3);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16);
}
_movieScript.global_gdirectionkeys[q] = _global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q]);
}
_global.call(new LingoSymbol("newupdate"),_movieScript.global_gleprops.leveleditors);
rct = ((LingoGlobal.rect(0,0,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv)+LingoGlobal.rect(_movieScript.global_gloprops.extratiles[1],_movieScript.global_gloprops.extratiles[2],-_movieScript.global_gloprops.extratiles[3],-_movieScript.global_gloprops.extratiles[4]))-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
_global.sprite(71).rect = ((rct.intersect(LingoGlobal.rect(0,0,52,40))+LingoGlobal.rect(11,1,11,1))*LingoGlobal.rect(16,16,16,16));
if ((_movieScript.global_genveditorprops.waterlevel == -1)) {
_global.sprite(9).rect = LingoGlobal.rect(0,0,0,0);
}
else {
rct = (LingoGlobal.rect(0,((_movieScript.global_gloprops.size.locv-_movieScript.global_genveditorprops.waterlevel)-_movieScript.global_gloprops.extratiles[4]),_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv)-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
_global.sprite(9).rect = (((rct.intersect(LingoGlobal.rect(0,0,52,40))+LingoGlobal.rect(11,1,11,1))*LingoGlobal.rect(16,16,16,16))+LingoGlobal.rect(0,-8,0,0));
}
_global.script(@"levelOverview").gotoeditor();
_global.go(_global.the_frame);

return null;
}
}
}
