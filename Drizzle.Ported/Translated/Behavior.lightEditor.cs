using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: lightEditor
//
public sealed class lightEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic dupl = null;
dynamic l = null;
dynamic curr = null;
dynamic s = null;
dynamic mv = null;
dynamic dir1 = null;
dynamic dir2 = null;
dynamic angleadd = null;
dynamic dsppos = null;
dynamic rad = null;
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((LingoGlobal.ToBool(_global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q])) & (_movieScript.global_gdirectionkeys[q] == 0))) {
_movieScript.global_gleprops.campos = ((_movieScript.global_gleprops.campos+new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })[q])*((1+9)*_global._key.keypressed(83)));
}
_movieScript.global_gdirectionkeys[q] = _global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q]);
}
if (LingoGlobal.ToBool(me.checkkey(@"Z"))) {
_movieScript.global_glighteprops.col = (1-_movieScript.global_glighteprops.col);
}
if (LingoGlobal.ToBool(_global._mouse.rightmousedown)) {
_movieScript.global_glighteprops.rot = _movieScript.lookatpoint(_movieScript.global_glighteprops.pos,_global._mouse.mouseloc);
}
else {
_movieScript.global_glighteprops.pos = _global._mouse.mouseloc;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"C"))) {
_movieScript.global_glgtimgquad[1] = ((_global._mouse.mouseloc+_movieScript.global_gleprops.campos)*20);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"V"))) {
_movieScript.global_glgtimgquad[2] = ((_global._mouse.mouseloc+_movieScript.global_gleprops.campos)*20);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"B"))) {
_movieScript.global_glgtimgquad[3] = ((_global._mouse.mouseloc+_movieScript.global_gleprops.campos)*20);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"N"))) {
_movieScript.global_glgtimgquad[4] = ((_global._mouse.mouseloc+_movieScript.global_gleprops.campos)*20);
}
if (LingoGlobal.ToBool(me.checkkey(@"M"))) {
dupl = _global.image(_global.member(@"lightImage").image.width,_global.member(@"lightImage").image.height,1);
dupl.copypixels(_global.member(@"lightImage").image,dupl.rect,dupl.rect);
_global.member(@"lightImage").image.copypixels(dupl,_movieScript.global_glgtimgquad,dupl.rect);
_movieScript.global_glgtimgquad = new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(_global.member(@"lightImage").image.width,0),LingoGlobal.point(_global.member(@"lightImage").image.width,_global.member(@"lightImage").image.height),LingoGlobal.point(0,_global.member(@"lightImage").image.height) });
}
if (((_global._system.milliseconds-_movieScript.global_glighteprops.lasttm) > 10)) {
if (LingoGlobal.ToBool(_global._key.keypressed(@"W"))) {
_movieScript.global_glighteprops.sz.locv = (_movieScript.global_glighteprops.sz.locv+1);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"S"))) {
_movieScript.global_glighteprops.sz.locv = (_movieScript.global_glighteprops.sz.locv-1);
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"D"))) {
_movieScript.global_glighteprops.sz.loch = (_movieScript.global_glighteprops.sz.loch+1);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"A"))) {
_movieScript.global_glighteprops.sz.loch = (_movieScript.global_glighteprops.sz.loch-1);
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"Q"))) {
_movieScript.global_glighteprops.rot = (_movieScript.global_glighteprops.rot-1);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"E"))) {
_movieScript.global_glighteprops.rot = (_movieScript.global_glighteprops.rot+1);
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"J"))) {
_movieScript.global_glighteprops.lightangle = _movieScript.restrict((_movieScript.global_glighteprops.lightangle-1),90,180);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"L"))) {
_movieScript.global_glighteprops.lightangle = _movieScript.restrict((_movieScript.global_glighteprops.lightangle+1),90,180);
}
if (LingoGlobal.ToBool(_movieScript.global_geverysecond)) {
if (LingoGlobal.ToBool(_global._key.keypressed(@"I"))) {
_movieScript.global_glighteprops.flatness = _movieScript.restrict((_movieScript.global_glighteprops.flatness-1),1,10);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"K"))) {
_movieScript.global_glighteprops.flatness = _movieScript.restrict((_movieScript.global_glighteprops.flatness+1),1,10);
}
_movieScript.global_geverysecond = 0;
}
else {
_movieScript.global_geverysecond = 1;
}
_movieScript.global_glighteprops.lasttm = _global._system.milliseconds;
}
l = new LingoList(new dynamic[] { @"pxl",@"bigCircle",@"leaves",@"oilyLight",@"directionalLight",@"blobLight1",@"blobLight2",@"wormsLight",@"crackLight",@"squareishLight",@"holeLight",@"roundedRectLight" });
curr = 1;
for (int tmp_s = 1; tmp_s <= l.count; tmp_s++) {
s = tmp_s;
if ((l[s] == _movieScript.global_glighteprops.paintshape)) {
curr = s;
break;
}
}
mv = 0;
if (LingoGlobal.ToBool(me.checkkey(@"r"))) {
mv = -1;
}
else if (LingoGlobal.ToBool(me.checkkey(@"f"))) {
mv = 1;
}
if ((mv != 0)) {
curr = _movieScript.restrict((curr+mv),1,l.count);
_global.sprite(11).member = _global.member(l[curr]);
_global.sprite(12).member = _global.member(l[curr]);
_movieScript.global_glighteprops.paintshape = l[curr];
}
dir1 = _movieScript.degtovec(_movieScript.global_glighteprops.rot);
dir2 = _movieScript.degtovec((_movieScript.global_glighteprops.rot+90));
angleadd = (_movieScript.degtovec(_movieScript.global_glighteprops.lightangle)*(_movieScript.global_glighteprops.flatness*10));
dsppos = LingoGlobal.point(((1024/2)-(_global.member(@"lightImage").image.width/2)),((768/2)-(_global.member(@"lightImage").image.height/2)));
dsppos = (dsppos-LingoGlobal.point(150,150));
q = (new LingoList(new dynamic[] { _movieScript.global_glighteprops.pos,_movieScript.global_glighteprops.pos,_movieScript.global_glighteprops.pos,_movieScript.global_glighteprops.pos })+new LingoList(new dynamic[] { (_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20) }));
q = (q+new LingoList(new dynamic[] { ((-dir2*_movieScript.global_glighteprops.sz.loch)-(dir1*_movieScript.global_glighteprops.sz.locv)),((dir2*_movieScript.global_glighteprops.sz.loch)-(dir1*_movieScript.global_glighteprops.sz.locv)),(((dir2*_movieScript.global_glighteprops.sz.loch)+dir1)*_movieScript.global_glighteprops.sz.locv),(((-dir2*_movieScript.global_glighteprops.sz.loch)+dir1)*_movieScript.global_glighteprops.sz.locv) }));
_movieScript.global_glighteprops.keys.m1 = _global._mouse.mousedown;
if ((LingoGlobal.ToBool(_movieScript.global_glighteprops.keys.m1) & (_movieScript.global_firstframe != 1))) {
_global.member(@"lightImage").image.copypixels(_global.member(_movieScript.global_glighteprops.paintshape).image,(q-new LingoList(new dynamic[] { dsppos,dsppos,dsppos,dsppos })),_global.member(_movieScript.global_glighteprops.paintshape).image.rect,new LingoPropertyList {[new LingoSymbol("color")] = (_movieScript.global_glighteprops.col*255),[new LingoSymbol("ink")] = 36});
}
if ((_movieScript.global_glighteprops.keys.m1 == 0)) {
_movieScript.global_firstframe = 0;
}
_movieScript.global_glighteprops.lastkeys.m1 = _movieScript.global_glighteprops.keys.m1;
_global.sprite(11).quad = (q-new LingoList(new dynamic[] { (_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20) }));
_global.sprite(12).quad = (q-new LingoList(new dynamic[] { (_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20) }));
_global.sprite(11).color = _global.color(((1-_movieScript.global_glighteprops.col)*255),((1-_movieScript.global_glighteprops.col)*255),((1-_movieScript.global_glighteprops.col)*255));
_global.sprite(10).quad = (_movieScript.global_glgtimgquad-(new LingoList(new dynamic[] { (_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20),(_movieScript.global_gleprops.campos*20) })+new LingoList(new dynamic[] { dsppos,dsppos,dsppos,dsppos })));
_global.sprite(15).rect = (LingoGlobal.rect(LingoGlobal.point(850,700),LingoGlobal.point(850,700))+LingoGlobal.rect(-50,-50,50,50));
rad = (_movieScript.global_glighteprops.flatness*10);
_global.sprite(16).rect = (LingoGlobal.rect(LingoGlobal.point(850,700),LingoGlobal.point(850,700))+LingoGlobal.rect(-rad,-rad,rad,rad));
_global.sprite(17).loc = (LingoGlobal.point(850,700)-(_movieScript.degtovec(_movieScript.global_glighteprops.lightangle)*rad));
_global.sprite(6).loc = ((LingoGlobal.point((1024/2),(768/2))-(LingoGlobal.point(150,150)+(angleadd*2)))-(_movieScript.global_gleprops.campos*20));
_global.sprite(9).loc = ((LingoGlobal.point((1024/2),(768/2))-LingoGlobal.point(150,150))-(_movieScript.global_gleprops.campos*20));
_global.sprite(5).loc = (LingoGlobal.point((1024/2),(768/2))-(_movieScript.global_gleprops.campos*20));
_global.sprite(8).loc = (LingoGlobal.point((1024/2),(768/2))-(_movieScript.global_gleprops.campos*20));
_global.script(@"levelOverview").gotoeditor();
_global.go(_global.the_frame);

return null;
}
public dynamic checkkey(dynamic me,dynamic key) {
dynamic rtrn = null;
rtrn = 0;
_movieScript.global_glighteprops.keys[LingoGlobal.symbol(key)] = _global._key.keypressed(key);
if ((LingoGlobal.ToBool(_movieScript.global_glighteprops.keys[LingoGlobal.symbol(key)]) & (_movieScript.global_glighteprops.lastkeys[LingoGlobal.symbol(key)] == 0))) {
rtrn = 1;
}
_movieScript.global_glighteprops.lastkeys[LingoGlobal.symbol(key)] = _movieScript.global_glighteprops.keys[LingoGlobal.symbol(key)];
return rtrn;

}
}
}
