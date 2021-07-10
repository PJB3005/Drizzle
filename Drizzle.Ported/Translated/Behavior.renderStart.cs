using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderStart
//
public sealed class renderStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic sav = null;
dynamic nonsolidtilesets = null;
dynamic l = null;
dynamic c = null;
dynamic cell = null;
dynamic d = null;
dynamic ad = null;
dynamic tl = null;
dynamic testmat = null;
dynamic tlps = null;
_global.put(@"Start render");
_movieScript.global_firstcamrepeat = LingoGlobal.TRUE;
_movieScript.global_gcurrentrendercamera = 0;
_movieScript.global_ganydecals = 0;
me.createshortcuts();
_movieScript.global_tilesetindex = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gtiles[1].tls.count; tmp_q++) {
q = tmp_q;
if ((_movieScript.global_gtiles[1].tls[q].rendertype == @"unified")) {
_global.member(LingoGlobal.concat(@"tileSet",_global.@string(q))).image = _global.image(1,1,32);
sav = _global.member(LingoGlobal.concat(@"tileSet",_global.@string(q)));
_global.member(LingoGlobal.concat(@"tileSet",_global.@string(q))).importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Graphics\tileSet",_movieScript.global_gtiles[1].tls[q].nm),@".png"));
sav.name = LingoGlobal.concat(@"tileSet",_global.@string(q));
_movieScript.global_tilesetindex.add(_movieScript.global_gtiles[1].tls[q].nm);
}
}
_movieScript.global_solidmtrx = new LingoPropertyList {};
nonsolidtilesets = new LingoList(new dynamic[] { @"Small Pipes",@"Invisible",@"Trash" });
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
l = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
cell = new LingoPropertyList {};
for (int tmp_d = 1; tmp_d <= 3; tmp_d++) {
d = tmp_d;
ad = 0;
if (((_movieScript.global_gleprops.matrix[q][c][d][1] == 1) & (_movieScript.global_gleprops.matrix[q][c][d][2].getpos(11) == 0))) {
tl = _movieScript.global_gteprops.tlmatrix[q][c][d];
if (((tl.tp == @"default") | (tl.tp == @"material"))) {
testmat = tl.data;
if ((tl.tp == @"default")) {
testmat = _movieScript.global_gteprops.defaultmaterial;
}
ad = LingoGlobal.op_eq(nonsolidtilesets.getpos(testmat),0);
}
else if (((tl.tp == @"tileHead") | (tl.tp == @"tileBody"))) {
tlps = tl.data[1];
if ((tl.tp == @"tileBody")) {
tlps = LingoGlobal.VOID;
if ((_global.ilk(_movieScript.global_gteprops.tlmatrix[tl.data[1].loch][tl.data[1].locv][tl.data[2]].data) == new LingoSymbol("list"))) {
tlps = _movieScript.global_gteprops.tlmatrix[tl.data[1].loch][tl.data[1].locv][tl.data[2]].data[1];
}
}
ad = 1;
if ((tlps != LingoGlobal.VOID)) {
if (((tlps.loch > 2) & (tlps.loch <= _movieScript.global_gtiles.count))) {
if ((tlps.locv <= _movieScript.global_gtiles[tlps.loch].tls.count)) {
ad = LingoGlobal.op_eq(_movieScript.global_gtiles[tlps.loch].tls[tlps.locv].tags.getpos(@"nonSolid"),0);
}
}
}
}
}
cell.add(ad);
}
l.add(cell);
}
_movieScript.global_solidmtrx.add(l);
}

return null;
}
public dynamic createshortcuts(dynamic me) {
dynamic q = null;
dynamic c = null;
dynamic diditwork = null;
dynamic tp = null;
dynamic holedir = null;
dynamic stps = null;
dynamic pos = null;
dynamic stp = null;
dynamic lastdir = null;
dynamic rpt = null;
dynamic dirsl = null;
dynamic dir = null;
_movieScript.global_gshortcuts = new LingoPropertyList {[new LingoSymbol("scs")] = new LingoPropertyList {},[new LingoSymbol("indexl")] = new LingoPropertyList {}};
for (int tmp_q = 2; tmp_q <= (_movieScript.global_gleprops.matrix.count-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = 2; tmp_c <= (_movieScript.global_gleprops.matrix[1].count-1); tmp_c++) {
c = tmp_c;
if ((_movieScript.global_gleprops.matrix[q][c][1][2].getpos(4) > 0)) {
diditwork = 1;
tp = @"shortCut";
holedir = LingoGlobal.point(0,0);
stps = 0;
pos = LingoGlobal.point(q,c);
stp = 0;
lastdir = LingoGlobal.point(0,0);
rpt = 0;
while (LingoGlobal.ToBool(LingoGlobal.op_eq(stp,0))) {
rpt = (rpt+1);
if ((rpt > 1000)) {
diditwork = 0;
stp = 1;
}
dirsl = new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) });
dirsl.deleteone(lastdir);
dirsl.addat(1,lastdir);
dirsl.deleteone(-lastdir);
foreach (dynamic tmp_dir in dirsl) {
dir = tmp_dir;
if (LingoGlobal.ToBool((pos+dir).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
if ((_movieScript.global_gleprops.matrix[(pos.loch+dir.loch)][(pos.locv+dir.locv)][1][2].getpos(6) > 0)) {
stp = 1;
tp = @"playerHole";
pos = LingoGlobal.point(q,c);
lastdir = dir;
break;
}
else if ((_movieScript.global_gleprops.matrix[(pos.loch+dir.loch)][(pos.locv+dir.locv)][1][2].getpos(7) > 0)) {
stp = 1;
tp = @"lizardHole";
pos = LingoGlobal.point(q,c);
lastdir = dir;
break;
}
else if ((_movieScript.global_gleprops.matrix[(pos.loch+dir.loch)][(pos.locv+dir.locv)][1][2].getpos(19) > 0)) {
stp = 1;
tp = @"WHAMH";
pos = LingoGlobal.point(q,c);
lastdir = dir;
break;
}
else if ((_movieScript.global_gleprops.matrix[(pos.loch+dir.loch)][(pos.locv+dir.locv)][1][2].getpos(21) > 0)) {
stp = 1;
tp = @"scavengerHole";
pos = LingoGlobal.point(q,c);
lastdir = dir;
break;
}
else if ((_movieScript.global_gleprops.matrix[(pos.loch+dir.loch)][(pos.locv+dir.locv)][1][2].getpos(4) > 0)) {
stp = 1;
pos = (pos+dir);
lastdir = dir;
break;
}
else if ((_movieScript.global_gleprops.matrix[(pos.loch+dir.loch)][(pos.locv+dir.locv)][1][2].getpos(5) > 0)) {
stps = (stps+1);
pos = (pos+dir);
lastdir = dir;
break;
}
}
}
if ((holedir == LingoGlobal.point(0,0))) {
holedir = lastdir;
}
}
if (LingoGlobal.ToBool(diditwork)) {
_movieScript.global_gshortcuts.indexl.add(LingoGlobal.point(q,c));
_movieScript.global_gshortcuts.scs.add(tp);
}
}
}
}

return null;
}
}
}
