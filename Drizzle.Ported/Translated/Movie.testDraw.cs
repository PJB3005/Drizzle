using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: testDraw
//
public sealed partial class MovieScript {
public dynamic drawtestlevel() {
dynamic img = null;
dynamic q = null;
dynamic c = null;
dynamic rct = null;
img = _global.image(1040,800,16);
_global.member(@"finalbg").image = drawtestlevellayer(img,3,_global.color(150,150,150));
_global.member(@"finalbg").image.copypixels(drawtestlevellayer(img,2,_global.color(100,100,100)),LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
img = _global.image(1040,800,16);
_global.member(@"finalfg").image = drawtestlevellayer(img,1,_global.color(50,50,50));
_global.member(@"finalbgLight").image = _global.image(1040,800,16);
_global.member(@"finalfgLight").image = _global.image(1040,800,16);
for (int tmp_q = 1; tmp_q <= global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if ((global_gleprops.matrix[q][c][1][2].getpos(5) > 0)) {
rct = givemiddleoftile(LingoGlobal.point(q,c));
rct = (LingoGlobal.rect(rct,rct)+LingoGlobal.rect(-1,-1,2,2));
if ((global_gleprops.matrix[q][c][1][1] == 1)) {
_global.member(@"finalfg").image.copypixels(_global.member(@"pxl").image,rct,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0)});
}
else if ((global_gleprops.matrix[q][c][2][1] == 1)) {
_global.member(@"finalbg").image.copypixels(_global.member(@"pxl").image,rct,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(70,70,70)});
}
}
}
}
global_gskycolor = _global.color(170,170,170);

return null;
}
public dynamic drawtestlevellayer(dynamic img,dynamic layer,dynamic col) {
dynamic drwrect = null;
dynamic q = null;
dynamic c = null;
dynamic rct = null;
dynamic t = null;
dynamic any = null;
drwrect = LingoGlobal.rect(1,1,global_gloprops.size.loch,global_gloprops.size.locv);
for (int tmp_q = drwrect.left; tmp_q <= drwrect.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = drwrect.top; tmp_c <= drwrect.bottom; tmp_c++) {
c = tmp_c;
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
img.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
foreach (dynamic tmp_t in global_gleprops.matrix[q][c][layer][2]) {
t = tmp_t;
switch (t) {
case 1:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(0,8,0,-8));
img.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = col});
break;
case 2:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(8,0,-8,0));
img.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = col});
break;
case 3:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(0,6,0,-6));
img.copypixels(_global.member(@"hiveGrass").image,rct,_global.member(@"hiveGrass").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(100,100,100)});
break;
}
}
switch (global_gleprops.matrix[q][c][layer][1]) {
case 0:
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
case 1:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
break;
case 2:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.bottom) });
break;
case 3:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.bottom) });
break;
case 4:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.top) });
break;
case 5:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.top) });
break;
case 6:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),((c*20)-10));
break;
case 7:
case 9:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
img.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = col});
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
}
img.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = col});
if (((global_gleprops.matrix[q][c][layer][1] == 1) & LingoGlobal.ToBool(global_gleprops.matrix[q][c][layer][2].getpos(11)))) {
any = 0;
if ((LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(-1,0)),layer)) | LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(1,0)),layer)))) {
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(0,4,0,-4));
img.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
any = 1;
}
if ((LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(0,-1)),layer)) | LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(0,1)),layer)))) {
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(4,0,-4,0));
img.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
any = 1;
}
if ((any == 0)) {
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
img.copypixels(_global.member(@"spearIcon").image,rct,_global.member(@"spearIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
else {
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
img.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
}
}
}
return img;

}
}
}
