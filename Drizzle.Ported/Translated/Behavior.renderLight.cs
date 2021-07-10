using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderLight
//
public sealed class renderLight : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
if (LingoGlobal.ToBool(_movieScript.global_gviewrender)) {
if (LingoGlobal.ToBool(_global._key.keypressed(48))) {
_global._movie.go(9);
}
me.newframe();
if (LingoGlobal.ToBool(_movieScript.global_keeplooping)) {
_global.go(_global.the_frame);
}
}
else {
while (LingoGlobal.ToBool(_movieScript.global_keeplooping)) {
me.newframe();
}
}

return null;
}
public dynamic newframe(dynamic me) {
dynamic cols = null;
dynamic rows = null;
dynamic marginpixels = null;
dynamic marginrect = null;
dynamic fullrect = null;
dynamic inv = null;
dynamic svpos = null;
dynamic q = null;
dynamic dir = null;
cols = 100;
rows = 60;
marginpixels = 150;
marginrect = LingoGlobal.rect(0,0,(((cols*20)+marginpixels)*2),(((rows*20)+marginpixels)*2));
fullrect = LingoGlobal.rect(0,0,(cols*20),(rows*20));
inv = _global.image(marginrect.right,marginrect.bottom,1);
svpos = _movieScript.global_pos;
for (int tmp_q = 1; tmp_q <= _movieScript.global_glighteprops.flatness; tmp_q++) {
q = tmp_q;
inv.copypixels(_movieScript.makesilhouttefromimg(_global.member(@"activeLightImage").image,1),(marginrect+LingoGlobal.rect(_movieScript.global_pos,_movieScript.global_pos)),marginrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
_movieScript.global_pos = (_movieScript.global_pos+_movieScript.degtovec(_movieScript.global_glighteprops.lightangle));
}
inv = _movieScript.makesilhouttefromimg(inv,1);
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_movieScript.global_c),@"sh")).image.copypixels(inv,(marginrect+LingoGlobal.rect(dir,dir)),marginrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
}
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_movieScript.global_c),@"sh")).image.copypixels(_movieScript.makesilhouttefromimg(_global.member(LingoGlobal.concat(@"layer",_movieScript.global_c)).image,1),(fullrect+LingoGlobal.rect(marginpixels,marginpixels,marginpixels,marginpixels)),fullrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
for (int tmp_q = 1; tmp_q <= _movieScript.global_glighteprops.flatness; tmp_q++) {
q = tmp_q;
_global.member(@"activeLightImage").image.copypixels(_movieScript.makesilhouttefromimg(_global.member(LingoGlobal.concat(@"layer",_movieScript.global_c)).image,0),(fullrect-(LingoGlobal.rect(svpos,svpos)+LingoGlobal.rect(marginpixels,marginpixels,marginpixels,marginpixels))),fullrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
svpos = (svpos+_movieScript.degtovec(_movieScript.global_glighteprops.lightangle));
}
_movieScript.global_c = (_movieScript.global_c+1);
if ((_movieScript.global_c > 29)) {
_movieScript.global_keeplooping = 0;
}

return null;
}
public dynamic newframe2(dynamic me) {
dynamic inv = null;
dynamic svpos = null;
dynamic q = null;
dynamic dir = null;
dynamic dp = null;
dynamic pstrct = null;
inv = _global.image((1040+200),(800+200),1);
svpos = _movieScript.global_pos;
for (int tmp_q = 1; tmp_q <= _movieScript.global_glighteprops.flatness; tmp_q++) {
q = tmp_q;
inv.copypixels(_movieScript.makesilhouttefromimg(_global.member(@"activeLightImage").image,1),(LingoGlobal.rect(0,0,(1040+200),(800+200))+LingoGlobal.rect(_movieScript.global_pos,_movieScript.global_pos)),LingoGlobal.rect(0,0,(1040+200),(800+200)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
_movieScript.global_pos = (_movieScript.global_pos+_movieScript.degtovec(_movieScript.global_glighteprops.lightangle));
}
inv = _movieScript.makesilhouttefromimg(inv,1);
inv.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,_movieScript.global_pos.loch,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
inv.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1040,_movieScript.global_pos.locv),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_movieScript.global_c),@"sh")).image.copypixels(inv,(((LingoGlobal.rect(0,0,1040,800)+LingoGlobal.rect(dir,dir))+LingoGlobal.rect(-100,-100,100,100))+LingoGlobal.rect(0,8,0,8)),LingoGlobal.rect(0,0,(1040+200),(800+200)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
}
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_movieScript.global_c),@"sh")).image.copypixels(_movieScript.makesilhouttefromimg(_global.member(LingoGlobal.concat(@"layer",_movieScript.global_c)).image,1),LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
for (int tmp_q = 1; tmp_q <= _movieScript.global_glighteprops.flatness; tmp_q++) {
q = tmp_q;
_global.member(@"activeLightImage").image.copypixels(_movieScript.makesilhouttefromimg(_global.member(LingoGlobal.concat(@"layer",_movieScript.global_c)).image,0),(LingoGlobal.rect(0,0,1040,800)-(LingoGlobal.rect(svpos,svpos)+LingoGlobal.rect(100,100,100,100))),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
svpos = (svpos+_movieScript.degtovec(_movieScript.global_glighteprops.lightangle));
}
_movieScript.global_c = (_movieScript.global_c+1);
if (((_movieScript.global_c > 29) | (_movieScript.global_glevel.lighttype == @"no Light"))) {
_global.member(@"shadowImage").image = _global.image((52*20),(40*20),32);
for (int tmp_q = 1; tmp_q <= 30; tmp_q++) {
q = tmp_q;
dp = ((30-q)-5);
pstrct = LingoGlobal.rect(_movieScript.depthpnt(LingoGlobal.point(0,0),dp),_movieScript.depthpnt(LingoGlobal.point(1040,800),dp));
_global.member(@"shadowImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image,pstrct,LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"shadowImage").image.copypixels(_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((30-q))),@"sh")).image,pstrct,LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
inv = _global.image((52*20),(40*20),1);
inv.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
inv.copypixels(_global.member(@"shadowImage").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"shadowImage").image.copypixels(inv,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800));
_movieScript.global_keeplooping = 0;
}

return null;
}
}
}
