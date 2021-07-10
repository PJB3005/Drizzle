using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderLight2
//
public sealed class renderLight2 : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic pnt = null;
dynamic d = null;
dynamic stp = null;
dynamic mvlps = null;
dynamic dir = null;
dynamic dp = null;
dynamic pstrct = null;
dynamic inv = null;
for (int tmp_q = 1; tmp_q <= 1040; tmp_q++) {
q = tmp_q;
pnt = LingoGlobal.point(q,_movieScript.global_c);
d = 0;
stp = 0;
mvlps = 1;
if ((_global.member(@"lightImage").getpixel((q-1),(_movieScript.global_c-1)) == 0)) {
while (LingoGlobal.ToBool(LingoGlobal.op_eq(stp,0))) {
if ((d > 19)) {
break;
}
else if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.getpixel((pnt+LingoGlobal.point(-1,-1))) != _global.color(255,255,255))) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(d)),@"sh")).image.setpixel((pnt+LingoGlobal.point(-1,-1)),_global.color(255,0,0));
if ((d > 0)) {
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.getpixel(((pnt+LingoGlobal.point(-1,-1))+dir)) != _global.color(255,255,255))) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(d)),@"sh")).image.setpixel(((pnt+LingoGlobal.point(-1,-1))+dir),_global.color(255,0,0));
}
}
}
break;
}
mvlps = (mvlps+1);
if ((mvlps > _movieScript.global_mvl.count)) {
mvlps = 1;
}
pnt = (pnt+LingoGlobal.point(_movieScript.global_mvl[mvlps][1],_movieScript.global_mvl[mvlps][2]));
d = (d+_movieScript.global_mvl[mvlps][3]);
}
}
}
_movieScript.global_c = (_movieScript.global_c+1);
_global.member(@"timeLeft").text = LingoGlobal.concat_space(LingoGlobal.concat_space(LingoGlobal.concat_space(_global.@string(((LingoGlobal.floatmember_helper(_movieScript.global_c)/new LingoDecimal(800))*new LingoDecimal(100)).integer),@"% Rendered, Approx. "),_global.@string((((LingoGlobal.floatmember_helper((_global._system.milliseconds-_movieScript.global_tm))/LingoGlobal.floatmember_helper(_movieScript.global_c))*(800-_movieScript.global_c))/1000).integer)),@"seconds left");
_global.sprite(42).loc = LingoGlobal.point(10,_movieScript.restrict(_movieScript.global_c,30,700));
if ((_movieScript.global_c > 800)) {
_global.member(@"shadowImage").image = _global.image((52*20),(40*20),32);
for (int tmp_q = 1; tmp_q <= 20; tmp_q++) {
q = tmp_q;
dp = ((20-q)-5);
pstrct = LingoGlobal.rect(_movieScript.depthpnt(LingoGlobal.point(0,0),dp),_movieScript.depthpnt(LingoGlobal.point(1040,800),dp));
_global.member(@"shadowImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((20-q)))).image,pstrct,LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"shadowImage").image.copypixels(_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((20-q))),@"sh")).image,pstrct,LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
inv = _global.image((52*20),(40*20),1);
inv.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
inv.copypixels(_global.member(@"shadowImage").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"shadowImage").image.copypixels(inv,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800));
}
else {
_global.go(_global.the_frame);
}

return null;
}
}
}
