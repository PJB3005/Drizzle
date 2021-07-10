using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: TEdraw
//
public sealed partial class MovieScript {
public dynamic tedraw(dynamic drwrect,dynamic layer,dynamic drawall) {
dynamic q = null;
dynamic c = null;
dynamic drawq = null;
dynamic drawc = null;
dynamic rct = null;
dynamic cl = null;
dynamic t = null;
dynamic tl = null;
dynamic clr = null;
dynamic mdpnt = null;
dynamic strt = null;
dynamic g = null;
dynamic h = null;
dynamic drw = null;
dynamic rct2 = null;
for (int tmp_q = drwrect.left; tmp_q <= drwrect.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = drwrect.top; tmp_c <= drwrect.bottom; tmp_c++) {
c = tmp_c;
drawq = (q-global_gleprops.campos.loch);
drawc = (c-global_gleprops.campos.locv);
if ((LingoGlobal.ToBool(LingoGlobal.point(drawq,drawc).inside(LingoGlobal.rect(1,1,53,41))) | LingoGlobal.ToBool(drawall))) {
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
if ((global_gteprops.tlmatrix[q][c][layer].tp != @"tileBody")) {
_global.member(LingoGlobal.concat(@"TEimg",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
}
switch (global_gteprops.tlmatrix[q][c][layer].tp) {
case @"material":
rct = (LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16))+LingoGlobal.rect(5,5,-5,-5));
switch (global_gleprops.matrix[q][c][layer][1]) {
case 0:
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
case 1:
break;
case 2:
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.bottom) });
break;
case 3:
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.bottom) });
break;
case 4:
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.top) });
break;
case 5:
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.top) });
break;
case 6:
rct = (LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16))+LingoGlobal.rect(5,5,-5,-8));
break;
case 7:
break;
case 8:
break;
}
cl = _global.color(0,0,0);
foreach (dynamic tmp_t in global_gtiles[1].tls) {
t = tmp_t;
if ((t.nm == global_gteprops.tlmatrix[q][c][layer].data)) {
cl = t.color;
break;
}
}
_global.member(LingoGlobal.concat(@"TEimg",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = cl});
break;
case @"tileHead":
tl = global_gtiles[global_gteprops.tlmatrix[q][c][layer].data[1].loch].tls[global_gteprops.tlmatrix[q][c][layer].data[1].locv];
clr = global_gtiles[global_gteprops.tlmatrix[q][c][layer].data[1].loch].clr;
mdpnt = LingoGlobal.point(((tl.sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((tl.sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
strt = ((LingoGlobal.point(q,c)-(mdpnt+LingoGlobal.point(1,1)))-global_gleprops.campos);
if (((tl.specs2 != LingoGlobal.VOID) & (layer < 3))) {
for (int tmp_g = strt.loch; tmp_g <= ((strt.loch+tl.sz.loch)-1); tmp_g++) {
g = tmp_g;
for (int tmp_h = strt.locv; tmp_h <= ((strt.locv+tl.sz.locv)-1); tmp_h++) {
h = tmp_h;
if ((((((g+global_gleprops.campos.loch) > 0) & ((h+global_gleprops.campos.locv) > 0)) & ((g+global_gleprops.campos.loch) < global_gteprops.tlmatrix.count)) & ((h+global_gleprops.campos.locv) < global_gteprops.tlmatrix[1].count))) {
drw = LingoGlobal.TRUE;
if ((tl.specs2[((((h-strt.locv)+(g-strt.loch))*tl.sz.locv)+1)] == -1)) {
drw = LingoGlobal.FALSE;
}
else if (((global_gteprops.tlmatrix[(g+global_gleprops.campos.loch)][(h+global_gleprops.campos.locv)][(layer+1)].tp == @"tileHead") & (global_gteprops.tlmatrix[q][c][layer].data != global_gteprops.tlmatrix[(g+global_gleprops.campos.loch)][(h+global_gleprops.campos.locv)][(layer+1)].data))) {
drw = LingoGlobal.FALSE;
}
if (LingoGlobal.ToBool(drw)) {
rct2 = LingoGlobal.rect(((g-1)*16),((h-1)*16),(g*16),(h*16));
_global.member(LingoGlobal.concat(@"TEimg",_global.@string((layer+1)))).image.copypixels(_global.member(@"previewTiles").image,rct2,((rct2+LingoGlobal.rect((tl.ptpos+16),(0+16),(tl.ptpos+16),(0+16)))-LingoGlobal.rect((strt.loch*16),(strt.locv*16),(strt.loch*16),(strt.locv*16))),new LingoPropertyList {[new LingoSymbol("color")] = (clr*new LingoDecimal(0.5))});
}
}
}
}
}
for (int tmp_g = strt.loch; tmp_g <= ((strt.loch+tl.sz.loch)-1); tmp_g++) {
g = tmp_g;
for (int tmp_h = strt.locv; tmp_h <= ((strt.locv+tl.sz.locv)-1); tmp_h++) {
h = tmp_h;
if ((((((g+global_gleprops.campos.loch) > 0) & ((h+global_gleprops.campos.locv) > 0)) & ((g+global_gleprops.campos.loch) < global_gteprops.tlmatrix.count)) & ((h+global_gleprops.campos.locv) < global_gteprops.tlmatrix[1].count))) {
drw = LingoGlobal.TRUE;
if ((tl.specs[((((h-strt.locv)+(g-strt.loch))*tl.sz.locv)+1)] == -1)) {
drw = LingoGlobal.FALSE;
}
else if (((global_gteprops.tlmatrix[(g+global_gleprops.campos.loch)][(h+global_gleprops.campos.locv)][layer].tp == @"tileHead") & (global_gteprops.tlmatrix[q][c][layer].data != global_gteprops.tlmatrix[(g+global_gleprops.campos.loch)][(h+global_gleprops.campos.locv)][layer].data))) {
drw = LingoGlobal.FALSE;
}
if (LingoGlobal.ToBool(drw)) {
rct2 = LingoGlobal.rect(((g-1)*16),((h-1)*16),(g*16),(h*16));
_global.member(LingoGlobal.concat(@"TEimg",_global.@string(layer))).image.copypixels(_global.member(@"previewTiles").image,rct2,((rct2+LingoGlobal.rect((tl.ptpos+16),(0+16),(tl.ptpos+16),(0+16)))-LingoGlobal.rect((strt.loch*16),(strt.locv*16),(strt.loch*16),(strt.locv*16))),new LingoPropertyList {[new LingoSymbol("color")] = clr});
}
}
}
}
break;
}
}
else {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"TEimg",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
}
}
}
}

return null;
}
public dynamic tedraw(dynamic p1, dynamic p2) {
return tedraw(p1, p2, null);
}
}
}
