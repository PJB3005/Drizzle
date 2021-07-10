using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderLightStart
//
public sealed class renderLightStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic layer = null;
dynamic q = null;
dynamic c = null;
dynamic tp = null;
dynamic grss = null;
dynamic lr = null;
dynamic cols = null;
dynamic rows = null;
dynamic marginpixels = null;
dynamic l = null;
dynamic inversedlightimage = null;
dynamic q2 = null;
_global.the_randomSeed = _movieScript.global_gloprops.tileseed;
_global.member(@"layer0dc").image.copypixels(_global.member(@"blackOutImg2").image,LingoGlobal.rect(0,0,(100*20),(60*20)),LingoGlobal.rect(0,0,(100*20),(60*20)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
for (int tmp_layer = 1; tmp_layer <= 3; tmp_layer++) {
layer = tmp_layer;
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if ((((_movieScript.global_gleprops.matrix[q][c][layer][2].getpos(3) > 0) & (_movieScript.afamvlvledit(LingoGlobal.point(q,c),layer) == 0)) & (_movieScript.afamvlvledit(LingoGlobal.point(q,(c+1)),layer) == 1))) {
for (int tmp_tp = 1; tmp_tp <= 2; tmp_tp++) {
tp = tmp_tp;
for (int tmp_grss = 1; tmp_grss <= 6; tmp_grss++) {
grss = tmp_grss;
lr = (((layer-1)*10)+_global.random(9));
_movieScript.global_pos = (_movieScript.givemiddleoftile((LingoGlobal.point(q,c)-_movieScript.global_grendercameratilepos))+LingoGlobal.point((-10+_global.random(20)),0));
if (((tp == 2) & (layer == 1))) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"hiveGrassGraf").image,(LingoGlobal.rect(_movieScript.global_pos,_movieScript.global_pos)+LingoGlobal.rect(-2,((_global.random(5)-_global.random(10))-_global.random(_global.random(14))),3,10)),LingoGlobal.rect(0,0,5,29),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"hiveGrassGraf2").image,(LingoGlobal.rect(_movieScript.global_pos,_movieScript.global_pos)+LingoGlobal.rect(-2,((_global.random(5)-_global.random(10))-_global.random(_global.random(14))),3,10)),LingoGlobal.rect(0,0,5,29),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
}
}
}
}
}
}
}
cols = 100;
rows = 60;
marginpixels = 150;
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
for (int tmp_l = 0; tmp_l <= 29; tmp_l++) {
l = tmp_l;
me.quadifymember(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(l)),@"dc"),((l-5)*new LingoDecimal(1.5)));
}
}
for (int tmp_l = 0; tmp_l <= 29; tmp_l++) {
l = tmp_l;
me.quadifymember(LingoGlobal.concat(@"layer",_global.@string(l)),((l-5)*new LingoDecimal(1.5)));
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",l),@"sh")).image = _global.image((((cols*20)+marginpixels)*2),(((rows*20)+marginpixels)*2),8);
me.quadifymember(LingoGlobal.concat(@"gradientA",_global.@string(l)),((l-5)*new LingoDecimal(1.5)));
me.quadifymember(LingoGlobal.concat(@"gradientB",_global.@string(l)),((l-5)*new LingoDecimal(1.5)));
}
_global.member(@"activeLightImage").image = _global.image((((cols*20)+marginpixels)*2),(((rows*20)+marginpixels)*2),1);
_global.member(@"activeLightImage").image.setpixel(0,0,_global.color(0,0,0));
_global.member(@"activeLightImage").image.setpixel((_global.member(@"activeLightImage").image.rect.right-1),(_global.member(@"activeLightImage").image.rect.bottom-1),_global.color(0,0,0));
inversedlightimage = _movieScript.makesilhouttefromimg(_global.member(@"lightImage").image,1);
_global.member(@"activeLightImage").image.copypixels(inversedlightimage,LingoGlobal.rect(0,0,(((cols*20)+marginpixels)*2),(((rows*20)+marginpixels)*2)),((LingoGlobal.rect(LingoGlobal.point(0,0),LingoGlobal.point((((cols*20)+marginpixels)*2),(((rows*20)+marginpixels)*2)))+LingoGlobal.rect((_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20)))+LingoGlobal.rect(150,150,150,150)));
_global.member(@"activeLightImage").image.copypixels(_global.member(@"blackOutImg2").image,(LingoGlobal.rect(0,0,(cols*20),(rows*20))+LingoGlobal.rect(marginpixels,marginpixels,marginpixels,marginpixels)),LingoGlobal.rect(0,0,(cols*20),(rows*20)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
c = 0;
_movieScript.global_keeplooping = 1;
_movieScript.global_pos = LingoGlobal.point(0,0);
_movieScript.global_tm = _global._system.milliseconds;
for (int tmp_q2 = 0; tmp_q2 <= 29; tmp_q2++) {
q2 = tmp_q2;
_global.sprite((50-q2)).loc = LingoGlobal.point(((1024/2)-q2),((768/2)-q2));
}
if ((_movieScript.global_gloprops.light == 0)) {
_global._movie.go(66);
}

return null;
}
public dynamic quadifymember(dynamic me,dynamic mem,dynamic fac) {
dynamic newimg = null;
dynamic qd = null;
dynamic q = null;
newimg = _global.member(mem).image.duplicate();
qd = new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(newimg.width,0),LingoGlobal.point(newimg.width,newimg.height),LingoGlobal.point(0,newimg.height) });
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
qd[q] = ((((qd[q]+_movieScript.degtovec(_movieScript.global_gcameraprops.quads[_movieScript.global_gcurrentrendercamera][q][1]))*_movieScript.global_gcameraprops.quads[_movieScript.global_gcurrentrendercamera][q][2])*fac)*new LingoDecimal(2.5));
}
_global.member(mem).image.copypixels(newimg,qd,newimg.rect);

return null;
}
}
}
