using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: finalize
//
public sealed class finalize : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic cols = null;
dynamic rows = null;
dynamic extrarect = null;
dynamic extrapoint = null;
dynamic lightmargin = null;
dynamic q = null;
dynamic dp = null;
dynamic pstrct = null;
dynamic inv = null;
dynamic smpl = null;
dynamic smpl2 = null;
dynamic smplps = null;
dynamic q2 = null;
dynamic c = null;
dynamic l = null;
dynamic bd = null;
dynamic lr = null;
dynamic getrect = null;
cols = 100;
rows = 60;
_global.member(@"finalImage").image = _global.image(1400,800,32);
_global.member(@"shadowImage").image = _global.image(1400,800,32);
_global.member(@"finalDecalImage").image = _global.image(1400,800,32);
_movieScript.global_gdecalcolors = new LingoPropertyList {};
extrarect = LingoGlobal.rect(-50,-50,50,50);
extrapoint = LingoGlobal.point(extrarect.right,extrarect.bottom);
lightmargin = 150;
_movieScript.global_grendercamerapixelpos = (_movieScript.global_grendercamerapixelpos+LingoGlobal.point((15*20),(10*20)));
_global.member(@"dumpImage").image = _global.image((((cols*20)+lightmargin)*2),(((rows*20)+lightmargin)*2),32);
for (int tmp_q = 1; tmp_q <= 30; tmp_q++) {
q = tmp_q;
dp = ((30-q)-5);
_global.member(@"dumpImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image,(LingoGlobal.rect(0,0,(cols*20),(rows*20))+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin)),LingoGlobal.rect(0,0,(cols*20),(rows*20)));
_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image.copypixels(_global.member(@"dumpImage").image,_global.member(@"dumpImage").image.rect,_global.member(@"dumpImage").image.rect);
pstrct = LingoGlobal.rect(_movieScript.depthpnt((LingoGlobal.point(0,0)-extrapoint),dp),_movieScript.depthpnt((LingoGlobal.point(1400,800)+extrapoint),dp));
_global.member(@"shadowImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image,pstrct,(((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+extrarect),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"shadowImage").image.copypixels(_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((30-q))),@"sh")).image,pstrct,(((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+extrarect),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
inv = _global.image(1400,800,1);
inv.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
inv.copypixels(_global.member(@"shadowImage").image,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1400,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"shadowImage").image.copypixels(inv,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1400,800));
_global.member(@"fogImage").image = _global.image(1400,800,32);
_global.member(@"dpImage").image = _global.image(1400,800,32);
_global.member(@"dpImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
smpl = _global.image(4,1,32);
smpl2 = _global.image(30,1,32);
smplps = 0;
_movieScript.global_dptsl = new LingoPropertyList {};
_movieScript.global_fogdptsl = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= 30; tmp_q++) {
q = tmp_q;
dp = ((30-q)-5);
pstrct = LingoGlobal.rect(_movieScript.depthpnt((LingoGlobal.point(0,0)-extrapoint),dp),_movieScript.depthpnt((LingoGlobal.point(1400,800)+extrapoint),dp));
_global.member(@"dpImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image,pstrct,(((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+extrarect),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
smpl.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(smplps,0,4,1),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 0});
if (((((dp+5) == 12) | ((dp+5) == 8)) | ((dp+5) == 4))) {
smpl.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,4,1),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 10,[new LingoSymbol("color")] = 255});
smplps = (smplps+1);
_global.member(@"dpImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 10,[new LingoSymbol("color")] = 255});
}
_global.member(@"fogImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image,pstrct,(((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+extrarect),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"fogImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 5,[new LingoSymbol("color")] = 255});
smpl2.setpixel((q-1),0,_global.color(255,255,255));
smpl2.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,30,1),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 5,[new LingoSymbol("color")] = 255});
}
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
_movieScript.global_dptsl.add(smpl.getpixel((4-q),0));
}
for (int tmp_q = 1; tmp_q <= 30; tmp_q++) {
q = tmp_q;
_movieScript.global_fogdptsl.add(smpl2.getpixel((30-q),0));
}
for (int tmp_q2 = 1; tmp_q2 <= 25; tmp_q2++) {
q2 = tmp_q2;
dp = ((30-q2)-5);
pstrct = LingoGlobal.rect(_movieScript.depthpnt((LingoGlobal.point(0,0)-extrapoint),dp),_movieScript.depthpnt((LingoGlobal.point(1400,800)+extrapoint),dp));
_global.member(@"finalImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q2)))).image,pstrct,(((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+extrarect),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
if (((30-q2) == 10)) {
inv = _movieScript.makesilhouttefromimg(_global.member(@"finalImage").image,1);
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if ((((_movieScript.global_gleprops.matrix[q][c][1][2].getpos(5) > 0) & (_movieScript.global_gleprops.matrix[q][c][1][1] == 0)) & (_movieScript.global_gleprops.matrix[q][c][2][1] == 1))) {
_movieScript.pasteshortcuthole(@"finalImage",LingoGlobal.point(q,c),5,@"BORDER");
_movieScript.pasteshortcuthole(@"finalImage",LingoGlobal.point(q,c),5,_global.color(51,10,0));
}
}
}
_global.member(@"finalImage").image.copypixels(inv,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1400,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
else if (((30-q2) == 20)) {
inv = _movieScript.makesilhouttefromimg(_global.member(@"finalImage").image,1);
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if (((((_movieScript.global_gleprops.matrix[q][c][1][2].getpos(5) > 0) & (_movieScript.global_gleprops.matrix[q][c][1][1] == 0)) & (_movieScript.global_gleprops.matrix[q][c][2][1] == 0)) & (_movieScript.global_gleprops.matrix[q][c][3][1] == 1))) {
_movieScript.pasteshortcuthole(@"finalImage",LingoGlobal.point(q,c),15,@"BORDER");
_movieScript.pasteshortcuthole(@"finalImage",LingoGlobal.point(q,c),15,_global.color(41,9,0));
}
}
}
_global.member(@"finalImage").image.copypixels(inv,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1400,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
}
for (int tmp_q = 25; tmp_q <= 30; tmp_q++) {
q = tmp_q;
dp = ((30-q)-5);
pstrct = LingoGlobal.rect(_movieScript.depthpnt((LingoGlobal.point(0,0)-extrapoint),dp),_movieScript.depthpnt((LingoGlobal.point(1400,800)+extrapoint),dp));
_global.member(@"finalImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((30-q)))).image,pstrct,(((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+extrarect),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
inv = _movieScript.makesilhouttefromimg(_global.member(@"finalImage").image,1);
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if (((_movieScript.global_gleprops.matrix[q][c][1][2].getpos(5) > 0) & (_movieScript.global_gleprops.matrix[q][c][1][1] == 1))) {
_movieScript.pasteshortcuthole(@"finalImage",LingoGlobal.point(q,c),-5,@"BORDER");
_movieScript.pasteshortcuthole(@"finalImage",LingoGlobal.point(q,c),-5,_global.color(31,8,0));
}
}
}
_global.member(@"finalImage").image.copypixels(inv,LingoGlobal.rect(0,0,1400,800),LingoGlobal.rect(0,0,1400,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"rainBowMask").image = _global.image(1400,800,1);
foreach (dynamic tmp_L in new LingoList(new dynamic[] { @"A",@"B" })) {
l = tmp_L;
_global.member(LingoGlobal.concat(@"flattenedGradient",l)).image = _global.image(1400,800,16);
for (int tmp_bd = 0; tmp_bd <= 29; tmp_bd++) {
bd = tmp_bd;
lr = (29-bd);
dp = (lr-5);
_global.member(@"dumpImage").image.copypixels(_global.member(LingoGlobal.concat(LingoGlobal.concat(@"gradient",l),_global.@string(lr))).image,(LingoGlobal.rect(0,0,(cols*20),(rows*20))+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin)),LingoGlobal.rect(0,0,(cols*20),(rows*20)));
pstrct = LingoGlobal.rect(_movieScript.depthpnt((LingoGlobal.point(0,0)-extrapoint),dp),_movieScript.depthpnt((LingoGlobal.point(1400,800)+extrapoint),dp));
getrect = (((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+extrarect);
_global.member(LingoGlobal.concat(@"flattenedGradient",l)).image.copypixels(_global.member(@"dumpImage").image,pstrct,getrect,new LingoPropertyList {[new LingoSymbol("maskimage")] = _movieScript.makesilhouttefromimg(_global.member(LingoGlobal.concat(@"layer",lr)).image,0).createmask()});
_global.member(LingoGlobal.concat(@"flattenedGradient",l)).image.setpixel(0,0,_global.color(0,0,0));
_global.member(LingoGlobal.concat(@"flattenedGradient",l)).image.setpixel((1400-1),(800-1),_global.color(0,0,0));
}
}
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
for (int tmp_bd = 0; tmp_bd <= 29; tmp_bd++) {
bd = tmp_bd;
lr = (29-bd);
dp = (lr-5);
_global.member(@"dumpImage").image.copypixels(_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(lr)),@"dc")).image,(LingoGlobal.rect(0,0,(cols*20),(rows*20))+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin)),LingoGlobal.rect(0,0,(cols*20),(rows*20)));
pstrct = LingoGlobal.rect(_movieScript.depthpnt((LingoGlobal.point(0,0)-extrapoint),dp),_movieScript.depthpnt((LingoGlobal.point(1400,800)+extrapoint),dp));
getrect = (((LingoGlobal.rect(0,0,1400,800)+LingoGlobal.rect(_movieScript.global_grendercamerapixelpos,_movieScript.global_grendercamerapixelpos))+LingoGlobal.rect(lightmargin,lightmargin,lightmargin,lightmargin))+extrarect);
_global.member(@"finalDecalImage").image.copypixels(_global.member(@"dumpImage").image,pstrct,getrect,new LingoPropertyList {[new LingoSymbol("maskimage")] = _movieScript.makesilhouttefromimg(_global.member(LingoGlobal.concat(@"layer",lr)).image,0).createmask()});
_global.member(@"finalDecalImage").image.setpixel(0,0,_global.color(0,0,0));
_global.member(@"finalDecalImage").image.setpixel((1400-1),(800-1),_global.color(0,0,0));
}
}
c = 1;
if ((_movieScript.global_glevel.lighttype == @"No Light")) {
_global.member(@"shadowImage").image.copypixels(_global.member(@"pxl").image,_global.member(@"shadowImage").image.rect,_global.member(@"pxl").image.rect);
}
_movieScript.global_keeplooping = 1;

return null;
}
}
}
