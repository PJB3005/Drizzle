using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: lvl
//
public sealed partial class MovieScript {
public dynamic drawshortcutsimg(dynamic drwrect,dynamic scl,dynamic drawall) {
dynamic q = null;
dynamic c = null;
dynamic drawq = null;
dynamic drawc = null;
dynamic rct = null;
dynamic l = null;
dynamic l2 = null;
dynamic dir = null;
dynamic shrtcut = null;
drwrect = (drwrect+LingoGlobal.rect(-1,-1,1,1));
for (int tmp_q = drwrect.left; tmp_q <= drwrect.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = drwrect.top; tmp_c <= drwrect.bottom; tmp_c++) {
c = tmp_c;
if ((drawall == 0)) {
drawq = (q-global_gleprops.campos.loch);
drawc = (c-global_gleprops.campos.locv);
}
else {
drawq = q;
drawc = c;
}
if ((LingoGlobal.ToBool(LingoGlobal.point(drawq,drawc).inside(LingoGlobal.rect(1,1,53,41))) | (drawall == 1))) {
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
if ((global_gleprops.matrix[q][c][1][2].getpos(4) > 0)) {
l = new LingoPropertyList {};
l2 = new LingoPropertyList {};
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
if (LingoGlobal.ToBool((LingoGlobal.point(q,c)+dir).inside(LingoGlobal.rect(1,1,global_gloprops.size.loch,global_gloprops.size.locv)))) {
if ((((((global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][2].getpos(5) > 0) | (global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][2].getpos(6) > 0)) | (global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][2].getpos(7) > 0)) | (global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][2].getpos(19) > 0)) | (global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][2].getpos(21) > 0))) {
l.add(dir);
}
if ((new LingoList(new dynamic[] { 0,6 }).getpos(global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][1]) != 0)) {
l2.add(dir);
}
}
}
shrtcut = 0;
if (((l.count == 1) & (l2.count == 1))) {
if ((l[1] == -l2[1])) {
shrtcut = 1;
}
}
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,1),LingoGlobal.point(-1,1) })) {
dir = tmp_dir;
if (LingoGlobal.ToBool((LingoGlobal.point(q,c)+dir).inside(LingoGlobal.rect(1,1,global_gloprops.size.loch,global_gloprops.size.locv)))) {
if ((global_gleprops.matrix[(q+dir.loch)][(c+dir.locv)][1][1] != 1)) {
shrtcut = 0;
break;
}
}
}
if (LingoGlobal.ToBool(shrtcut)) {
rct = (((LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*16))+LingoGlobal.rect(4,4,-4,-4))*scl)/16);
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(@"shortCutArrow",_global.@string(l[1].loch)),@"."),_global.@string(l[1].locv))).image,rct,_global.member(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(@"shortCutArrow",_global.@string(l[1].loch)),@"."),_global.@string(l[1].locv))).image.rect);
global_gleprops.matrix[q][c][1][1] = 7;
}
else {
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"shortCutArrow0.0").image,rct,_global.member(@"shortCutArrow0.0").image.rect);
global_gleprops.matrix[q][c][1][1] = 0;
}
}
if ((global_gleprops.matrix[q][c][1][2].getpos(5) > 0)) {
rct = (((LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl))+LingoGlobal.rect(7,7,-7,-7))*scl)/16);
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
}
if ((global_gleprops.matrix[q][c][1][2].getpos(6) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"p").image,(((rct+LingoGlobal.rect(3,3,-3,-3))*scl)/16),_global.member(@"p").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(7) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"e").image,(((rct+LingoGlobal.rect(3,3,-3,-3))*scl)/16),_global.member(@"e").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(9) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"rockIcon").image,(((rct+LingoGlobal.rect(3,3,-3,-3))*scl)/16),_global.member(@"rockIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(10) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"spearIcon").image,rct,_global.member(@"spearIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(12) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"iconforbidbats").image,rct,_global.member(@"iconforbidbats").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(13) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"g").image,(((rct+LingoGlobal.rect(3,3,-3,-3))*scl)/16),_global.member(@"g").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(14) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"bubble").image,rct,_global.member(@"bubble").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(15) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"eggIcon").image,rct,_global.member(@"eggIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(16) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"miniFlyGraf").image,(((rct+LingoGlobal.rect(10,0,0,-10))*scl)/16),_global.member(@"miniFlyGraf").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(17) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"wallBugGraf").image,(((rct+LingoGlobal.rect(5,5,-5,5))*scl)/16),_global.member(@"wallBugGraf").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(18) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"waterfallW").image,(((rct+LingoGlobal.rect(2,2,-2,2))*scl)/16),_global.member(@"waterfallW").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(19) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"W").image,(((rct+LingoGlobal.rect(3,3,-3,-3))*scl)/16),_global.member(@"W").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(20) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"iconWormGrass").image,rct,_global.member(@"iconWormGrass").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
if ((global_gleprops.matrix[q][c][1][2].getpos(21) > 0)) {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"s").image,(((rct+LingoGlobal.rect(3,3,-3,-3))*scl)/16),_global.member(@"s").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
rct = LingoGlobal.rect(((drawq-1)*scl),((drawc-1)*scl),(drawq*scl),(drawc*scl));
_global.member(@"levelEditImageShortCuts").image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
}
}
}
}

return null;
}
public dynamic lvleditdraw(dynamic drwrect,dynamic layer,dynamic drawall) {
dynamic q = null;
dynamic c = null;
dynamic drawq = null;
dynamic drawc = null;
dynamic rct = null;
dynamic t = null;
dynamic any = null;
global_glastdrawwasfullandmini = 0;
drwrect = (drwrect+LingoGlobal.rect(-1,-1,1,1));
for (int tmp_q = drwrect.left; tmp_q <= drwrect.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = drwrect.top; tmp_c <= drwrect.bottom; tmp_c++) {
c = tmp_c;
drawq = (q-global_gleprops.campos.loch);
drawc = (c-global_gleprops.campos.locv);
if ((LingoGlobal.ToBool(LingoGlobal.point(drawq,drawc).inside(LingoGlobal.rect(1,1,53,41))) | LingoGlobal.ToBool(drawall))) {
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
foreach (dynamic tmp_t in global_gleprops.matrix[q][c][layer][2]) {
t = tmp_t;
switch (t) {
case 1:
rct = (LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16))+LingoGlobal.rect(0,6,0,-6));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
break;
case 2:
rct = (LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16))+LingoGlobal.rect(6,0,-6,0));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
break;
case 3:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"hiveGrass").image,rct,_global.member(@"hiveGrass").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(100,100,100),[new LingoSymbol("ink")] = 36});
break;
}
}
switch (global_gleprops.matrix[q][c][layer][1]) {
case 0:
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
case 1:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
break;
case 2:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.bottom) });
break;
case 3:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.bottom) });
break;
case 4:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.top) });
break;
case 5:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.top) });
break;
case 6:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),((drawc*16)-8));
break;
case 7:
case 9:
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect);
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
}
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
if (LingoGlobal.ToBool(global_gleprops.matrix[q][c][layer][2].getpos(11))) {
if ((global_gleprops.matrix[q][c][layer][1] == 1)) {
any = 0;
if ((LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(-1,0)),layer)) | LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(1,0)),layer)))) {
rct = (LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16))+LingoGlobal.rect(0,4,0,-4));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
any = 1;
}
if ((LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(0,-1)),layer)) | LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(0,1)),layer)))) {
rct = (LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16))+LingoGlobal.rect(4,0,-4,0));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
any = 1;
}
if ((any == 0)) {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"spearIcon").image,rct,_global.member(@"spearIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
else {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"spearIcon").image,rct,_global.member(@"spearIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
}
}
}
else {
rct = LingoGlobal.rect(((drawq-1)*16),((drawc-1)*16),(drawq*16),(drawc*16));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect);
}
}
}
}

return null;
}
public dynamic minilvleditdraw(dynamic layer) {
dynamic q = null;
dynamic c = null;
dynamic drawq = null;
dynamic drawc = null;
dynamic rct = null;
dynamic t = null;
dynamic any = null;
if (LingoGlobal.ToBool(global_glastdrawwasfullandmini)) {
return null;
}
else if ((layer == 3)) {
global_glastdrawwasfullandmini = 1;
}
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image = _global.image((global_gloprops.size.loch*5),(global_gloprops.size.locv*5),1);
for (int tmp_q = 1; tmp_q <= global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
drawq = q;
drawc = c;
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
foreach (dynamic tmp_t in global_gleprops.matrix[q][c][layer][2]) {
t = tmp_t;
switch (t) {
case 1:
rct = (LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5))+LingoGlobal.rect(0,2,0,-2));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
break;
case 2:
rct = (LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5))+LingoGlobal.rect(2,0,-2,0));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
break;
case 3:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"hiveGrass").image,rct,_global.member(@"hiveGrass").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(100,100,100),[new LingoSymbol("ink")] = 36});
break;
}
}
switch (global_gleprops.matrix[q][c][layer][1]) {
case 0:
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
case 1:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
break;
case 2:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.bottom) });
break;
case 3:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.bottom) });
break;
case 4:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.top) });
break;
case 5:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.top) });
break;
case 6:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),((drawc*5)-3));
break;
case 7:
case 9:
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect);
rct = LingoGlobal.rect(-1,-1,-1,-1);
break;
}
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect);
if (LingoGlobal.ToBool(global_gleprops.matrix[q][c][layer][2].getpos(11))) {
if ((global_gleprops.matrix[q][c][layer][1] == 1)) {
any = 0;
if ((LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(-1,0)),layer)) | LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(1,0)),layer)))) {
rct = (LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5))+LingoGlobal.rect(0,1,0,-1));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
any = 1;
}
if ((LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(0,-1)),layer)) | LingoGlobal.ToBool(checktileifcrackopen((LingoGlobal.point(q,c)+LingoGlobal.point(0,1)),layer)))) {
rct = (LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5))+LingoGlobal.rect(1,0,-1,0));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
any = 1;
}
if ((any == 0)) {
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"spearIcon").image,rct,_global.member(@"spearIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
else {
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"spearIcon").image,rct,_global.member(@"spearIcon").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
}
}
}
else {
rct = LingoGlobal.rect(((drawq-1)*5),((drawc-1)*5),(drawq*5),(drawc*5));
_global.member(LingoGlobal.concat(@"levelEditImage",_global.@string(layer))).image.copypixels(_global.member(@"semiTransperent").image,rct,_global.member(@"semiTransperent").image.rect);
}
}
}

return null;
}
public dynamic checktileifcrackopen(dynamic tl,dynamic lr) {
if (LingoGlobal.ToBool(tl.inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
if (((global_gleprops.matrix[tl.loch][tl.locv][lr][1] != 1) | (global_gleprops.matrix[tl.loch][tl.locv][lr][2].getpos(11) > 0))) {
return 1;
}
else {
return 0;
}
}
else {
return 0;
}

return null;
}
public dynamic savelvl() {

return null;
}
public dynamic lvleditdraw(dynamic p1, dynamic p2) {
return lvleditdraw(p1, p2, null);
}
public dynamic drawshortcutsimg(dynamic p1, dynamic p2) {
return drawshortcutsimg(p1, p2, null);
}
}
}
