using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: cameraEditor
//
public sealed class cameraEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic rct = null;
dynamic q = null;
dynamic mouseovercamera = null;
dynamic smallstdist = null;
dynamic pos = null;
dynamic closestcorner = null;
dynamic cornerpos = null;
dynamic linepos = null;
if ((_movieScript.global_gloprops.size.loch > _movieScript.global_gloprops.size.locv)) {
_movieScript.global_fac = (new LingoDecimal(1024)/_movieScript.global_gloprops.size.loch);
}
else {
_movieScript.global_fac = (new LingoDecimal(768)/_movieScript.global_gloprops.size.locv);
}
if ((_movieScript.global_fac > 16)) {
_movieScript.global_fac = 16;
}
rct = (LingoGlobal.rect((1024/2),(768/2),(1024/2),(768/2))+LingoGlobal.rect(((-_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*_movieScript.global_fac),((-_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*_movieScript.global_fac),((_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*_movieScript.global_fac),((_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*_movieScript.global_fac)));
_global.sprite(90).rect = (rct+LingoGlobal.rect((_movieScript.global_gloprops.extratiles[1]*_movieScript.global_fac),(_movieScript.global_gloprops.extratiles[2]*_movieScript.global_fac),(-_movieScript.global_gloprops.extratiles[3]*_movieScript.global_fac),(-_movieScript.global_gloprops.extratiles[4]*_movieScript.global_fac)));
for (int tmp_q = 2; tmp_q <= 8; tmp_q++) {
q = tmp_q;
_global.sprite(q).rect = rct;
}
_global.sprite(13).rect = rct;
if ((LingoGlobal.ToBool(checkkey(@"n")) & (_movieScript.global_gcameraprops.cameras.count < 20))) {
_movieScript.global_gcameraprops.cameras.add(LingoGlobal.point((_movieScript.global_gloprops.size.loch*10),(_movieScript.global_gloprops.size.locv*10)));
_movieScript.global_gcameraprops.quads.add(new LingoList(new dynamic[] { new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }) }));
_movieScript.global_gcameraprops.selectedcamera = _movieScript.global_gcameraprops.cameras.count;
}
if ((_movieScript.global_gcameraprops.selectedcamera > 0)) {
mouseovercamera = _movieScript.global_gcameraprops.selectedcamera;
}
else {
mouseovercamera = 0;
smallstdist = 10000;
for (int tmp_q = 1; tmp_q <= _movieScript.global_gcameraprops.cameras.count; tmp_q++) {
q = tmp_q;
pos = ((((LingoGlobal.point((1024/2),(768/2))+LingoGlobal.point(((-_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*_movieScript.global_fac),((-_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*_movieScript.global_fac)))+((_movieScript.global_gcameraprops.cameras[q]/20)*_movieScript.global_fac))+LingoGlobal.point(35,20))*_movieScript.global_fac);
if ((_movieScript.diag(pos,_global._mouse.mouseloc) < smallstdist)) {
mouseovercamera = q;
smallstdist = _movieScript.diag(pos,_global._mouse.mouseloc);
}
}
}
if ((mouseovercamera > 0)) {
pos = (((LingoGlobal.point((1024/2),(768/2))+LingoGlobal.point(((-_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*_movieScript.global_fac),((-_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*_movieScript.global_fac)))+(_movieScript.global_gcameraprops.cameras[mouseovercamera]/20))*_movieScript.global_fac);
smallstdist = 10000;
closestcorner = 0;
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((_movieScript.diag(_global._mouse.mouseloc,(pos+new LingoList(new dynamic[] { LingoGlobal.point(0,0),(LingoGlobal.point(70,0)*_movieScript.global_fac),(LingoGlobal.point(70,40)*_movieScript.global_fac),(LingoGlobal.point(0,40)*_movieScript.global_fac) })[q])) < smallstdist)) {
smallstdist = _movieScript.diag(_global._mouse.mouseloc,(pos+new LingoList(new dynamic[] { LingoGlobal.point(0,0),(LingoGlobal.point(70,0)*_movieScript.global_fac),(LingoGlobal.point(70,40)*_movieScript.global_fac),(LingoGlobal.point(0,40)*_movieScript.global_fac) })[q]));
closestcorner = q;
}
}
if ((closestcorner > 0)) {
cornerpos = (pos+new LingoList(new dynamic[] { LingoGlobal.point(0,0),(LingoGlobal.point(70,0)*_movieScript.global_fac),(LingoGlobal.point(70,40)*_movieScript.global_fac),(LingoGlobal.point(0,40)*_movieScript.global_fac) })[closestcorner]);
linepos = ((pos+LingoGlobal.point(35,20))*_movieScript.global_fac);
if ((_movieScript.global_showquads > 0)) {
_movieScript.global_showquads = (_movieScript.global_showquads-1);
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"J"))) {
_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][1] = (_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][1]-2);
linepos = cornerpos;
_movieScript.global_showquads = 20;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"L"))) {
_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][1] = (_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][1]+2);
linepos = cornerpos;
_movieScript.global_showquads = 20;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"I"))) {
_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2] = (_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2]+(new LingoDecimal(1)/new LingoDecimal(20)));
if ((_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2] > 1)) {
_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2] = 1;
}
_movieScript.global_showquads = 20;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"K"))) {
_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2] = (_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2]-(new LingoDecimal(1)/new LingoDecimal(20)));
if ((_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2] < 0)) {
_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2] = 0;
}
_movieScript.global_showquads = 20;
}
cornerpos = ((((cornerpos+_movieScript.degtovec(_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][1]))*4)*_movieScript.global_fac)*_movieScript.global_gcameraprops.quads[mouseovercamera][closestcorner][2]);
_global.sprite(89).rect = LingoGlobal.rect(linepos,cornerpos);
_global.sprite(89).member.linedirection = LingoGlobal.op_and(LingoGlobal.op_or(LingoGlobal.op_gt(linepos.loch,cornerpos.loch),LingoGlobal.op_gt(linepos.locv,cornerpos.locv)),LingoGlobal.op_or(LingoGlobal.op_lt(linepos.loch,cornerpos.loch),LingoGlobal.op_lt(linepos.locv,cornerpos.locv)));
}
}
if ((_movieScript.global_gcameraprops.selectedcamera != 0)) {
_movieScript.global_gcameraprops.cameras[_movieScript.global_gcameraprops.selectedcamera] = (((_global._mouse.mouseloc/LingoGlobal.point(new LingoDecimal(1024),new LingoDecimal(768)))*LingoGlobal.point((_movieScript.global_gloprops.size.loch*20),(_movieScript.global_gloprops.size.locv*20)))-LingoGlobal.point((new LingoDecimal(35)*20),(new LingoDecimal(20)*20)));
if ((LingoGlobal.ToBool(checkkey(@"d")) & (_movieScript.global_gcameraprops.cameras.count > 1))) {
_movieScript.global_gcameraprops.cameras.deleteat(_movieScript.global_gcameraprops.selectedcamera);
_movieScript.global_gcameraprops.quads.deleteat(_movieScript.global_gcameraprops.selectedcamera);
_movieScript.global_gcameraprops.selectedcamera = 0;
}
if (LingoGlobal.ToBool(checkkey(@"p"))) {
_movieScript.global_gcameraprops.selectedcamera = 0;
}
}
else if ((LingoGlobal.ToBool(checkkey(@"e")) & (mouseovercamera > 0))) {
_movieScript.global_gcameraprops.selectedcamera = mouseovercamera;
}
me.drawall();
_global.script(@"levelOverview").gotoeditor();
_global.go(_global.the_frame);

return null;
}
public dynamic drawall(dynamic me) {
dynamic q = null;
dynamic pos = null;
dynamic qd = null;
dynamic c = null;
for (int tmp_q = 1; tmp_q <= _movieScript.global_gcameraprops.cameras.count; tmp_q++) {
q = tmp_q;
pos = (((LingoGlobal.point((1024/2),(768/2))+LingoGlobal.point(((-_movieScript.global_gloprops.size.loch*new LingoDecimal(0.5))*_movieScript.global_fac),((-_movieScript.global_gloprops.size.locv*new LingoDecimal(0.5))*_movieScript.global_fac)))+(_movieScript.global_gcameraprops.cameras[q]/20))*_movieScript.global_fac);
_global.sprite((23+q)).rect = ((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(0,0,70,40))*_movieScript.global_fac);
_global.sprite((44+q)).rect = ((LingoGlobal.rect(pos,pos)+(LingoGlobal.rect(0,0,70,40)+LingoGlobal.rect(new LingoDecimal(9.3),new LingoDecimal(0.8),-new LingoDecimal(9.3),-new LingoDecimal(0.8))))*_movieScript.global_fac);
qd = new LingoList(new dynamic[] { pos,((pos+LingoGlobal.point(70,0))*_movieScript.global_fac),((pos+LingoGlobal.point(70,40))*_movieScript.global_fac),((pos+LingoGlobal.point(0,40))*_movieScript.global_fac) });
for (int tmp_c = 1; tmp_c <= 4; tmp_c++) {
c = tmp_c;
qd[c] = ((((qd[c]+_movieScript.degtovec(_movieScript.global_gcameraprops.quads[q][c][1]))*4)*_movieScript.global_fac)*_movieScript.global_gcameraprops.quads[q][c][2]);
}
_global.sprite((67+q)).quad = qd;
_global.sprite((67+q)).blend = ((15+(_movieScript.global_showquads/new LingoDecimal(20)))*40);
}
for (int tmp_q = (_movieScript.global_gcameraprops.cameras.count+1); tmp_q <= 10; tmp_q++) {
q = tmp_q;
_global.sprite((23+q)).rect = LingoGlobal.rect(-100,-100,-100,-100);
_global.sprite((44+q)).rect = LingoGlobal.rect(-100,-100,-100,-100);
_global.sprite((67+q)).rect = LingoGlobal.rect(-100,-100,-100,-100);
}

return null;
}
public dynamic checkkey(dynamic key) {
dynamic rtrn = null;
rtrn = 0;
_movieScript.global_gcameraprops.keys[LingoGlobal.symbol(key)] = _global._key.keypressed(key);
if ((LingoGlobal.ToBool(_movieScript.global_gcameraprops.keys[LingoGlobal.symbol(key)]) & (_movieScript.global_gcameraprops.lastkeys[LingoGlobal.symbol(key)] == 0))) {
rtrn = 1;
}
_movieScript.global_gcameraprops.lastkeys[LingoGlobal.symbol(key)] = _movieScript.global_gcameraprops.keys[LingoGlobal.symbol(key)];
return rtrn;

}
}
}
