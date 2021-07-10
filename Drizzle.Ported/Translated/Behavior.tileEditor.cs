using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: tileEditor
//
public sealed class tileEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic mstile = null;
dynamic q = null;
dynamic l = null;
dynamic rct = null;
dynamic actn = null;
dynamic actn2 = null;
dynamic mdpnt = null;
mstile = ((_global._mouse.mouseloc/LingoGlobal.point(new LingoDecimal(16),new LingoDecimal(16)))+LingoGlobal.point(new LingoDecimal(0.4999),new LingoDecimal(0.4999)));
mstile = ((LingoGlobal.point(mstile.loch.integer,mstile.locv.integer)+LingoGlobal.point(-1,-1))+_movieScript.global_gleprops.campos);
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((LingoGlobal.ToBool(_global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q])) & (_movieScript.global_gdirectionkeys[q] == 0))) {
_movieScript.global_gleprops.campos = ((_movieScript.global_gleprops.campos+new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })[q])*((1+9)*_global._key.keypressed(83)));
for (int tmp_l = 1; tmp_l <= 3; tmp_l++) {
l = tmp_l;
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),l);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),l);
}
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16,1);
}
_movieScript.global_gdirectionkeys[q] = _global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q]);
}
rct = ((LingoGlobal.rect(0,0,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv)+LingoGlobal.rect(_movieScript.global_gloprops.extratiles[1],_movieScript.global_gloprops.extratiles[2],-_movieScript.global_gloprops.extratiles[3],-_movieScript.global_gloprops.extratiles[4]))-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
_global.sprite(71).rect = ((rct.intersect(LingoGlobal.rect(0,0,52,40))+LingoGlobal.rect(1,1,1,1))*LingoGlobal.rect(16,16,16,16));
if (LingoGlobal.ToBool(checkkey(@"Q"))) {
pickuptile(mstile);
}
if (LingoGlobal.ToBool(checkkey(@"L"))) {
_movieScript.global_gteprops.worklayer = (_movieScript.global_gteprops.worklayer+1);
if ((_movieScript.global_gteprops.worklayer > 3)) {
_movieScript.global_gteprops.worklayer = 1;
}
writematerial(mstile);
changelayer();
}
actn = 0;
actn2 = 0;
_movieScript.global_gteprops.keys.m1 = _global._mouse.mousedown;
if ((LingoGlobal.ToBool(_movieScript.global_gteprops.keys.m1) & (_movieScript.global_gteprops.lastkeys.m1 == 0))) {
actn = 1;
}
_movieScript.global_gteprops.lastkeys.m1 = _movieScript.global_gteprops.keys.m1;
_movieScript.global_gteprops.keys.m2 = _global._mouse.rightmousedown;
if ((LingoGlobal.ToBool(_movieScript.global_gteprops.keys.m2) & (_movieScript.global_gteprops.lastkeys.m2 == 0))) {
actn2 = 1;
}
_movieScript.global_gteprops.lastkeys.m2 = _movieScript.global_gteprops.keys.m2;
if ((mstile != _movieScript.global_gteprops.lstmsps)) {
writematerial(mstile);
actn = _movieScript.global_gteprops.keys.m1;
actn2 = _movieScript.global_gteprops.keys.m2;
istilepositionlegal(mstile);
}
_movieScript.global_gteprops.lstmsps = mstile;
if ((_movieScript.global_gteprops.specialedit != 0)) {
_global.sprite(19).visibility = 1;
_global.member(@"default material").text = LingoGlobal.concat_space(@"SPECIAL EDIT:",_global.@string(_movieScript.global_gteprops.specialedit));
if (LingoGlobal.ToBool(actn)) {
specialaction(mstile);
}
if (LingoGlobal.ToBool(actn2)) {
_movieScript.global_gteprops.specialedit = 0;
}
_global.sprite(19).visibility = LingoGlobal.op_ne(_movieScript.global_gteprops.specialedit,0);
}
else if (LingoGlobal.ToBool(actn)) {
action(mstile);
}
if (LingoGlobal.ToBool(actn2)) {
deletetile(mstile);
}
if (LingoGlobal.ToBool(checkkey(@"W"))) {
updatetilemenu(LingoGlobal.point(0,-1));
}
if (LingoGlobal.ToBool(checkkey(@"S"))) {
updatetilemenu(LingoGlobal.point(0,1));
}
if (LingoGlobal.ToBool(checkkey(@"A"))) {
updatetilemenu(LingoGlobal.point(-1,0));
}
if (LingoGlobal.ToBool(checkkey(@"D"))) {
updatetilemenu(LingoGlobal.point(1,0));
}
if (LingoGlobal.ToBool(checkkey(@"C"))) {
me.deletealltiles();
}
if ((_movieScript.global_gteprops.tooltype == @"material")) {
if (LingoGlobal.ToBool(_global._key.keypressed(@"F"))) {
_global.sprite(15).rect = ((LingoGlobal.rect((mstile*16),((mstile+LingoGlobal.point(1,1))*16))+LingoGlobal.rect(-16,-16,16,16))-LingoGlobal.rect((_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16)));
}
else {
_global.sprite(15).rect = (LingoGlobal.rect((mstile*16),((mstile+LingoGlobal.point(1,1))*16))-LingoGlobal.rect((_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16)));
}
_global.sprite(13).loc = LingoGlobal.point(-2000,-2000);
}
else if ((_movieScript.global_gteprops.tooltype == @"special")) {
_global.sprite(15).color = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].color;
switch (_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].placemethod) {
case @"rect":
if ((_movieScript.global_specialrectpoint == LingoGlobal.VOID)) {
_global.sprite(15).rect = (LingoGlobal.rect((mstile*16),((mstile+LingoGlobal.point(1,1))*16))-LingoGlobal.rect((_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16)));
if (LingoGlobal.ToBool(actn)) {
_movieScript.global_specialrectpoint = mstile;
}
}
else {
rct = LingoGlobal.rect(_movieScript.global_specialrectpoint,(_movieScript.global_specialrectpoint+LingoGlobal.point(1,1))).union(LingoGlobal.rect(mstile,(mstile+LingoGlobal.point(1,1))));
_global.sprite(15).rect = ((rct*16)-LingoGlobal.rect((_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16)));
if (LingoGlobal.ToBool(actn2)) {
_movieScript.global_specialrectpoint = LingoGlobal.VOID;
}
else if (LingoGlobal.ToBool(actn)) {
_movieScript.global_specialrectpoint = LingoGlobal.VOID;
specialrectplacement((rct+LingoGlobal.rect(0,0,-1,-1)));
}
}
break;
}
_global.sprite(13).loc = LingoGlobal.point(-2000,-2000);
}
else {
_global.sprite(15).rect = LingoGlobal.rect(-5,-5,-5,-5);
mdpnt = LingoGlobal.point(((_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
_global.sprite(13).loc = ((((mstile+LingoGlobal.point(1,1))-mdpnt)-_movieScript.global_gleprops.campos)*16);
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"E"))) {
updatetilemenu(LingoGlobal.point(0,0));
}
if ((_movieScript.global_genveditorprops.waterlevel == -1)) {
_global.sprite(9).rect = LingoGlobal.rect(0,0,0,0);
}
else {
rct = (LingoGlobal.rect(0,((_movieScript.global_gloprops.size.locv-_movieScript.global_genveditorprops.waterlevel)-_movieScript.global_gloprops.extratiles[4]),_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv)-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
_global.sprite(9).rect = (((rct.intersect(LingoGlobal.rect(0,0,52,40))+LingoGlobal.rect(1,1,1,1))*LingoGlobal.rect(16,16,16,16))+LingoGlobal.rect(0,-8,0,0));
}
_global.script(@"levelOverview").gotoeditor();
_global.go(_global.the_frame);

return null;
}
public dynamic deletealltiles() {
dynamic q = null;
dynamic l = null;
dynamic c = null;
_movieScript.global_gteprops.tlmatrix = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
l = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
l.add(new LingoList(new dynamic[] { new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0},new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0},new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0} }));
}
_movieScript.global_gteprops.tlmatrix.add(l);
}
for (int tmp_q = 1; tmp_q <= 3; tmp_q++) {
q = tmp_q;
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),q);
}

return null;
}
public dynamic checkkey(dynamic key) {
dynamic rtrn = null;
rtrn = 0;
_movieScript.global_gteprops.keys[LingoGlobal.symbol(key)] = _global._key.keypressed(key);
if ((LingoGlobal.ToBool(_movieScript.global_gteprops.keys[LingoGlobal.symbol(key)]) & (_movieScript.global_gteprops.lastkeys[LingoGlobal.symbol(key)] == 0))) {
rtrn = 1;
}
_movieScript.global_gteprops.lastkeys[LingoGlobal.symbol(key)] = _movieScript.global_gteprops.keys[LingoGlobal.symbol(key)];
return rtrn;

}
public dynamic writematerial(dynamic mstile) {
dynamic txt = null;
dynamic dt = null;
_global.sprite(8).visibility = 0;
if (LingoGlobal.ToBool(mstile.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
txt = @"";
switch (_movieScript.global_gleprops.matrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer][1]) {
case 1:
txt = @"Wall";
break;
case 2:
txt = @"Eastward Slope";
break;
case 3:
txt = @"Westward Slope";
break;
case 4:
txt = @"Ceiling Slope";
break;
case 5:
txt = @"Ceiling Slope";
break;
case 6:
txt = @"Floor";
break;
case 7:
txt = @"Short Cut Entrance";
_global.sprite(8).visibility = 1;
break;
}
if ((txt != @"")) {
switch (_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].tp) {
case @"material":
txt += txt.ToString();
break;
case @"tileHead":
txt += txt.ToString();
if ((_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data.count > 2)) {
txt += txt.ToString();
}
break;
case @"tileBody":
dt = _movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data;
if ((_movieScript.global_gteprops.tlmatrix[dt[1].loch][dt[1].locv][dt[2]].tp == @"tileHead")) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
break;
}
}
_global.member(@"editor1tool").text = txt;
}
else {
_global.member(@"editor1tool").text = @"";
}

return null;
}
public dynamic updatetilemenu(dynamic mv) {
dynamic txt = null;
dynamic tl = null;
if (((mv == LingoGlobal.VOID) | (mv == _global.script(@"tileEditor")))) {
mv = LingoGlobal.point(0,0);
}
_movieScript.global_gteprops.tmpos = (_movieScript.global_gteprops.tmpos+mv);
if ((mv.loch != 0)) {
if ((_movieScript.global_gteprops.tmpos.loch < 1)) {
_movieScript.global_gteprops.tmpos.loch = _movieScript.global_gtiles.count;
}
else if ((_movieScript.global_gteprops.tmpos.loch > _movieScript.global_gtiles.count)) {
_movieScript.global_gteprops.tmpos.loch = 1;
}
_movieScript.global_gteprops.tmpos.locv = _movieScript.global_gteprops.tmsavposl[_movieScript.global_gteprops.tmpos.loch];
}
else if ((mv.locv != 0)) {
if ((_movieScript.global_gteprops.tmpos.locv < 1)) {
_movieScript.global_gteprops.tmpos.locv = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls.count;
}
else if ((_movieScript.global_gteprops.tmpos.locv > _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls.count)) {
_movieScript.global_gteprops.tmpos.locv = 1;
}
_movieScript.global_gteprops.tmsavposl[_movieScript.global_gteprops.tmpos.loch] = _movieScript.global_gteprops.tmpos.locv;
}
_movieScript.global_gteprops.tmpos.loch = _movieScript.restrict(_movieScript.global_gteprops.tmpos.loch,1,_movieScript.global_gtiles.count);
_movieScript.global_gteprops.tmpos.locv = _movieScript.restrict(_movieScript.global_gteprops.tmpos.locv,1,_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls.count);
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_tl = 1; tmp_tl <= _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls.count; tmp_tl++) {
tl = tmp_tl;
if ((tl == _movieScript.global_gteprops.tmpos.locv)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
}
_global.member(@"tileMenu").text = txt;
if ((_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].nm == @"materials")) {
_global.sprite(19).visibility = 1;
_movieScript.global_gteprops.tooltype = @"material";
_movieScript.global_gteprops.tooldata = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].nm;
_global.member(@"tilePreview").image = _global.image(1,1,1);
if (LingoGlobal.ToBool(_global._key.keypressed(@"E"))) {
if ((_movieScript.global_gteprops.defaultmaterial != _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].nm)) {
_movieScript.global_gteprops.defaultmaterial = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].nm;
_global.member(@"default material").text = LingoGlobal.concat_space(LingoGlobal.concat_space(@"Default material:",_movieScript.global_gteprops.defaultmaterial),@"(Press 'E' to change)");
}
}
}
else if ((_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].nm == @"special")) {
_movieScript.global_gteprops.tooltype = @"special";
_movieScript.global_gteprops.tooldata = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].nm;
_global.member(@"tilePreview").image = _global.image(1,1,1);
}
else if ((_movieScript.global_gteprops.specialedit == 0)) {
_global.sprite(19).visibility = 0;
}
_movieScript.global_gteprops.tooltype = @"tile";
_movieScript.global_gteprops.tooldata = @"TILE";
drawtilepreview();
istilepositionlegal(_movieScript.global_gteprops.lstmsps);

return null;
}
public dynamic action(dynamic mstile) {
dynamic l = null;
dynamic q = null;
if (LingoGlobal.ToBool(mstile.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
switch (_movieScript.global_gteprops.tooltype) {
case @"material":
l = new LingoList(new dynamic[] { mstile });
if (LingoGlobal.ToBool(_global._key.keypressed(@"F"))) {
l = new LingoList(new dynamic[] { mstile,(mstile+LingoGlobal.point(1,0)),(mstile+LingoGlobal.point(-1,0)),(mstile+LingoGlobal.point(0,1)),(mstile+LingoGlobal.point(0,-1)),(mstile+LingoGlobal.point(-1,-1)),(mstile+LingoGlobal.point(-1,1)),(mstile+LingoGlobal.point(1,1)),(mstile+LingoGlobal.point(1,-1)) });
}
foreach (dynamic tmp_q in l) {
q = tmp_q;
if (LingoGlobal.ToBool(q.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
if ((new LingoList(new dynamic[] { @"tileHead",@"tileBody" }).getpos(_movieScript.global_gteprops.tlmatrix[q.loch][q.locv][_movieScript.global_gteprops.worklayer].tp) == 0)) {
_movieScript.global_gteprops.tlmatrix[q.loch][q.locv][_movieScript.global_gteprops.worklayer].tp = @"material";
_movieScript.global_gteprops.tlmatrix[q.loch][q.locv][_movieScript.global_gteprops.worklayer].data = _movieScript.global_gteprops.tooldata;
}
_movieScript.tedraw(LingoGlobal.rect(q,q),_movieScript.global_gteprops.worklayer);
}
}
break;
case @"tile":
if (((LingoGlobal.ToBool(istilepositionlegal(mstile)) | LingoGlobal.ToBool(_global._key.keypressed(@"F"))) | LingoGlobal.ToBool(_global._key.keypressed(@"G")))) {
placetile(mstile,_movieScript.global_gteprops.tmpos);
}
break;
}
}

return null;
}
public dynamic deletetile(dynamic mstile) {
dynamic dt = null;
if (LingoGlobal.ToBool(mstile.inside(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv)))) {
switch (_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].tp) {
case @"material":
_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].tp = @"default";
_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data = 0;
_movieScript.tedraw(LingoGlobal.rect(mstile,mstile),_movieScript.global_gteprops.worklayer);
break;
case @"tileHead":
deletetiletile(mstile,_movieScript.global_gteprops.worklayer);
break;
case @"tileBody":
dt = _movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data;
if ((_movieScript.global_gteprops.tlmatrix[dt[1].loch][dt[1].locv][dt[2]].tp == @"tileHead")) {
deletetiletile(dt[1],dt[2]);
}
else {
_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].tp = @"default";
_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data = 0;
_movieScript.tedraw(LingoGlobal.rect(mstile,mstile),_movieScript.global_gteprops.worklayer);
}
break;
}
}

return null;
}
public dynamic drawtilepreview() {
dynamic tl = null;
dynamic mdpnt = null;
dynamic offst = null;
dynamic n = null;
dynamic q = null;
dynamic c = null;
dynamic cl = null;
_global.member(@"tilePreview").image = _global.image((85*5),(85*5),16);
tl = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv];
mdpnt = LingoGlobal.point(((tl.sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((tl.sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
offst = (LingoGlobal.point((3*5),(3*5))-mdpnt);
if ((tl.specs2 != LingoGlobal.VOID)) {
n = 1;
for (int tmp_q = 1; tmp_q <= tl.sz.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= tl.sz.locv; tmp_c++) {
c = tmp_c;
if ((tl.specs2[n] != -1)) {
_global.member(@"tilePreview").image.copypixels(_global.member(LingoGlobal.concat(@"prvw",_global.@string(tl.specs2[n]))).image,(LingoGlobal.rect(((q-(1+offst.loch))*16),((c-(1+offst.locv))*16),((q+offst.loch)*16),((c+offst.locv)*16))+LingoGlobal.rect(5,0,5,0)),_global.member(LingoGlobal.concat(@"prvw",_global.@string(tl.specs2[n]))).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(50,50,50)});
}
n = (n+1);
}
}
}
n = 1;
for (int tmp_q = 1; tmp_q <= tl.sz.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= tl.sz.locv; tmp_c++) {
c = tmp_c;
if ((tl.specs[n] != -1)) {
cl = _global.color(150,150,150);
_global.member(@"tilePreview").image.copypixels(_global.member(LingoGlobal.concat(@"prvw",_global.@string(tl.specs[n]))).image,(LingoGlobal.rect(((q-(1+offst.loch))*16),((c-(1+offst.locv))*16),((q+offst.loch)*16),((c+offst.locv)*16))+LingoGlobal.rect(0,5,0,5)),_global.member(LingoGlobal.concat(@"prvw",_global.@string(tl.specs[n]))).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = cl});
}
n = (n+1);
}
}
_global.member(@"tileMouse").image = _global.image((tl.sz.loch*16),(tl.sz.locv*16),16);
_global.member(@"tileMouse").image.copypixels(_global.member(@"previewTiles").image,_global.member(@"tileMouse").image.rect,LingoGlobal.rect(tl.ptpos,0,(tl.ptpos+(tl.sz.loch*16)),(tl.sz.locv*16)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(150,150,150)});
_global.member(@"tileMouse").regpoint = LingoGlobal.point(0,0);

return null;
}
public dynamic istilepositionlegal(dynamic mstile) {
dynamic rtrn = null;
dynamic tl = null;
dynamic mdpnt = null;
dynamic strt = null;
dynamic n = null;
dynamic q = null;
dynamic c = null;
rtrn = 1;
if ((_movieScript.global_gteprops.tooltype == @"tile")) {
tl = _movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv];
mdpnt = LingoGlobal.point(((tl.sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((tl.sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
strt = (mstile-(mdpnt+LingoGlobal.point(1,1)));
if (((tl.specs2 != LingoGlobal.VOID) & (_movieScript.global_gteprops.worklayer < 3))) {
n = 1;
for (int tmp_q = strt.loch; tmp_q <= ((strt.loch+tl.sz.loch)-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = strt.locv; tmp_c <= ((strt.locv+tl.sz.locv)-1); tmp_c++) {
c = tmp_c;
if ((((tl.specs2[n] != -1) & LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) & (_movieScript.global_gteprops.worklayer < 3))) {
if (((_movieScript.afamvlvledit(LingoGlobal.point(q,c),(_movieScript.global_gteprops.worklayer+1)) != tl.specs2[n]) | (new LingoList(new dynamic[] { @"tileHead",@"tileBody" }).getpos(_movieScript.global_gteprops.tlmatrix[q][c][(_movieScript.global_gteprops.worklayer+1)].tp) > 0))) {
rtrn = 0;
break;
}
}
n = (n+1);
}
if ((rtrn == 0)) {
break;
}
}
}
if ((rtrn == 1)) {
n = 1;
for (int tmp_q = strt.loch; tmp_q <= ((strt.loch+tl.sz.loch)-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = strt.locv; tmp_c <= ((strt.locv+tl.sz.locv)-1); tmp_c++) {
c = tmp_c;
if (((tl.specs[n] != -1) & LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1)))))) {
if (((_movieScript.afamvlvledit(LingoGlobal.point(q,c),_movieScript.global_gteprops.worklayer) != tl.specs[n]) | (new LingoList(new dynamic[] { @"tileHead",@"tileBody" }).getpos(_movieScript.global_gteprops.tlmatrix[q][c][_movieScript.global_gteprops.worklayer].tp) > 0))) {
rtrn = 0;
break;
}
}
n = (n+1);
}
if ((rtrn == 0)) {
break;
}
}
}
}
_global.sprite(15).color = _global.color(255,(255*rtrn),(255*rtrn));
_global.sprite(13).color = _global.color(255,(255*rtrn),(255*rtrn));
return rtrn;

}
public dynamic pickuptile(dynamic mstile) {
dynamic q = null;
dynamic dt = null;
switch (_movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].tp) {
case @"material":
for (int tmp_q = 1; tmp_q <= _movieScript.global_gtiles[1].tls.count; tmp_q++) {
q = tmp_q;
if ((_movieScript.global_gtiles[1].tls[q].nm == _movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data)) {
_movieScript.global_gteprops.tmpos = LingoGlobal.point(1,q);
updatetilemenu(LingoGlobal.point(0,0));
break;
}
}
break;
case @"tileHead":
_movieScript.global_gteprops.tmpos = _movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data[1];
updatetilemenu(LingoGlobal.point(0,0));
break;
case @"tileBody":
dt = _movieScript.global_gteprops.tlmatrix[mstile.loch][mstile.locv][_movieScript.global_gteprops.worklayer].data;
if ((_movieScript.global_gteprops.tlmatrix[dt[1].loch][dt[1].locv][dt[2]].tp == @"tileHead")) {
_movieScript.global_gteprops.tmpos = _movieScript.global_gteprops.tlmatrix[dt[1].loch][dt[1].locv][dt[2]].data[1];
updatetilemenu(LingoGlobal.point(0,0));
}
break;
}

return null;
}
public dynamic placetile(dynamic plctile,dynamic tmpos) {
dynamic forceadaptterrain = null;
dynamic tl = null;
dynamic mdpnt = null;
dynamic strt = null;
dynamic n = null;
dynamic q = null;
dynamic c = null;
if (((((plctile.loch < 1) | (plctile.locv < 1)) | (plctile.loch > _movieScript.global_gteprops.tlmatrix.count)) | (plctile.locv > _movieScript.global_gteprops.tlmatrix[1].count))) {
return LingoGlobal.VOID;
}
forceadaptterrain = _global._key.keypressed(@"G");
tl = _movieScript.global_gtiles[tmpos.loch].tls[tmpos.locv];
mdpnt = LingoGlobal.point(((tl.sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((tl.sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
strt = (plctile-(mdpnt+LingoGlobal.point(1,1)));
_movieScript.global_gteprops.tlmatrix[plctile.loch][plctile.locv][_movieScript.global_gteprops.worklayer].tp = @"tileHead";
_movieScript.global_gteprops.tlmatrix[plctile.loch][plctile.locv][_movieScript.global_gteprops.worklayer].data = new LingoList(new dynamic[] { tmpos,tl.nm });
if ((tl.nm == @"Chain Holder")) {
_movieScript.global_gteprops.tlmatrix[plctile.loch][plctile.locv][_movieScript.global_gteprops.worklayer].data.add(@"NONE");
_movieScript.global_gteprops.specialedit = new LingoList(new dynamic[] { @"Attatch Chain",plctile,_movieScript.global_gteprops.worklayer });
}
_movieScript.tedraw(LingoGlobal.rect(plctile.loch,plctile.locv,plctile.loch,plctile.locv),_movieScript.global_gteprops.worklayer);
if (((tl.specs2 != LingoGlobal.VOID) & (_movieScript.global_gteprops.worklayer < 3))) {
n = 1;
for (int tmp_q = strt.loch; tmp_q <= ((strt.loch+tl.sz.loch)-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = strt.locv; tmp_c <= ((strt.locv+tl.sz.locv)-1); tmp_c++) {
c = tmp_c;
if (((tl.specs2[n] != -1) & LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1)))))) {
_movieScript.global_gteprops.tlmatrix[q][c][(_movieScript.global_gteprops.worklayer+1)].tp = @"tileBody";
_movieScript.global_gteprops.tlmatrix[q][c][(_movieScript.global_gteprops.worklayer+1)].data = new LingoList(new dynamic[] { plctile,_movieScript.global_gteprops.worklayer });
_movieScript.tedraw(LingoGlobal.rect(q,c,q,c),(_movieScript.global_gteprops.worklayer+1));
if (LingoGlobal.ToBool(forceadaptterrain)) {
_movieScript.global_gleprops.matrix[q][c][(_movieScript.global_gteprops.worklayer+1)][1] = tl.specs2[n];
}
}
n = (n+1);
}
}
}
n = 1;
for (int tmp_q = strt.loch; tmp_q <= ((strt.loch+tl.sz.loch)-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = strt.locv; tmp_c <= ((strt.locv+tl.sz.locv)-1); tmp_c++) {
c = tmp_c;
if (((tl.specs[n] != -1) & LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1)))))) {
if ((LingoGlobal.point(q,c) != plctile)) {
_movieScript.global_gteprops.tlmatrix[q][c][_movieScript.global_gteprops.worklayer].tp = @"tileBody";
_movieScript.global_gteprops.tlmatrix[q][c][_movieScript.global_gteprops.worklayer].data = new LingoList(new dynamic[] { plctile,_movieScript.global_gteprops.worklayer });
_movieScript.tedraw(LingoGlobal.rect(q,c,q,c),_movieScript.global_gteprops.worklayer);
}
if (LingoGlobal.ToBool(forceadaptterrain)) {
_movieScript.global_gleprops.matrix[q][c][_movieScript.global_gteprops.worklayer][1] = tl.specs[n];
}
}
n = (n+1);
}
}
if (LingoGlobal.ToBool(forceadaptterrain)) {
_movieScript.lvleditdraw(LingoGlobal.rect(strt,(strt+tl.sz)),1);
_movieScript.lvleditdraw(LingoGlobal.rect(strt,(strt+tl.sz)),2);
_movieScript.lvleditdraw(LingoGlobal.rect(strt,(strt+tl.sz)),3);
}

return null;
}
public dynamic deletetiletile(dynamic ps,dynamic lr) {
dynamic tl = null;
dynamic mdpnt = null;
dynamic strt = null;
dynamic n = null;
dynamic q = null;
dynamic c = null;
dynamic rct = null;
tl = _movieScript.global_gteprops.tlmatrix[ps.loch][ps.locv][lr].data[1];
tl = _movieScript.global_gtiles[tl.loch].tls[tl.locv];
mdpnt = LingoGlobal.point(((tl.sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((tl.sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
strt = (ps-(mdpnt+LingoGlobal.point(1,1)));
if (((tl.specs2 != 0) & (lr < 3))) {
n = 1;
for (int tmp_q = strt.loch; tmp_q <= ((strt.loch+tl.sz.loch)-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = strt.locv; tmp_c <= ((strt.locv+tl.sz.locv)-1); tmp_c++) {
c = tmp_c;
if (((tl.specs2[n] != -1) & LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1)))))) {
_movieScript.global_gteprops.tlmatrix[q][c][(lr+1)].tp = @"default";
_movieScript.global_gteprops.tlmatrix[q][c][(lr+1)].data = 0;
rct = (LingoGlobal.rect(((q-1)*16),((c-1)*16),(q*16),(c*16))-LingoGlobal.rect((_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16)));
_global.member(LingoGlobal.concat(@"TEimg",_global.@string(2))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
}
n = (n+1);
}
}
}
n = 1;
for (int tmp_q = strt.loch; tmp_q <= ((strt.loch+tl.sz.loch)-1); tmp_q++) {
q = tmp_q;
for (int tmp_c = strt.locv; tmp_c <= ((strt.locv+tl.sz.locv)-1); tmp_c++) {
c = tmp_c;
if (((tl.specs[n] != -1) & LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1)))))) {
_movieScript.global_gteprops.tlmatrix[q][c][lr].tp = @"default";
_movieScript.global_gteprops.tlmatrix[q][c][lr].data = 0;
rct = (LingoGlobal.rect(((q-1)*16),((c-1)*16),(q*16),(c*16))-LingoGlobal.rect((_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16)));
_global.member(LingoGlobal.concat(@"TEimg",_global.@string(lr))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
}
n = (n+1);
}
}

return null;
}
public dynamic specialaction(dynamic tl) {
switch (_movieScript.global_gteprops.specialedit[1]) {
case @"Attatch Chain":
_movieScript.global_gteprops.tlmatrix[_movieScript.global_gteprops.specialedit[2].loch][_movieScript.global_gteprops.specialedit[2].locv][_movieScript.global_gteprops.specialedit[3]].data[3] = tl;
_movieScript.global_gteprops.specialedit = 0;
break;
}

return null;
}
public dynamic changelayer() {
dynamic pos = null;
if ((_movieScript.global_gteprops.worklayer == 2)) {
_global.sprite(1).blend = 10;
_global.sprite(2).blend = 10;
_global.sprite(3).blend = 90;
_global.sprite(4).blend = 100;
_global.sprite(5).blend = 70;
_global.sprite(6).blend = 0;
}
else if ((_movieScript.global_gteprops.worklayer == 1)) {
_global.sprite(1).blend = 10;
_global.sprite(2).blend = 10;
_global.sprite(3).blend = 60;
_global.sprite(4).blend = 10;
_global.sprite(5).blend = 70;
_global.sprite(6).blend = 100;
}
else {
_global.sprite(1).blend = 100;
_global.sprite(2).blend = 100;
_global.sprite(3).blend = 60;
_global.sprite(4).blend = 10;
_global.sprite(5).blend = 60;
_global.sprite(6).blend = 10;
}
_global.member(@"layerText").text = LingoGlobal.concat_space(@"Work Layer:",_global.@string(_movieScript.global_gteprops.worklayer));
pos = (2-_movieScript.global_gteprops.worklayer);
_global.sprite(1).loc = ((LingoGlobal.point(432,336)+LingoGlobal.point((pos+1),(-pos-1)))*3);
_global.sprite(2).loc = ((LingoGlobal.point(432,336)+LingoGlobal.point((pos+1),(-pos-1)))*3);
_global.sprite(3).loc = ((LingoGlobal.point(432,336)+LingoGlobal.point(pos,-pos))*3);
_global.sprite(4).loc = ((LingoGlobal.point(432,336)+LingoGlobal.point(pos,-pos))*3);
_global.sprite(5).loc = ((LingoGlobal.point(432,336)+LingoGlobal.point((pos-1),(-pos+1)))*3);
_global.sprite(6).loc = ((LingoGlobal.point(432,336)+LingoGlobal.point((pos-1),(-pos+1)))*3);

return null;
}
public dynamic specialrectplacement(dynamic rct) {
dynamic px = null;
dynamic py = null;
dynamic lookfortilecat = null;
dynamic stringlength = null;
dynamic q = null;
dynamic patterns = null;
dynamic a = null;
dynamic b = null;
dynamic currentpattern = null;
dynamic possiblepatterns = null;
dynamic tl = null;
switch (_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].nm) {
case @"Rect Clear":
for (int tmp_px = rct.left; tmp_px <= rct.right; tmp_px++) {
px = tmp_px;
for (int tmp_py = rct.top; tmp_py <= rct.bottom; tmp_py++) {
py = tmp_py;
deletetile(LingoGlobal.point(px,py));
}
}
break;
case @"SH pattern box":
case @"SH grate box":
placetile(LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(4,5));
placetile(LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(4,6));
placetile(LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(4,7));
placetile(LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(4,8));
for (int tmp_px = (rct.left+1); tmp_px <= (rct.right-1); tmp_px++) {
px = tmp_px;
placetile(LingoGlobal.point(px,rct.top),LingoGlobal.point(4,1));
placetile(LingoGlobal.point(px,rct.bottom),LingoGlobal.point(4,3));
}
for (int tmp_py = (rct.top+1); tmp_py <= (rct.bottom-1); tmp_py++) {
py = tmp_py;
placetile(LingoGlobal.point(rct.left,py),LingoGlobal.point(4,4));
placetile(LingoGlobal.point(rct.right,py),LingoGlobal.point(4,2));
}
lookfortilecat = @"SU patterns";
stringlength = 10;
if ((_movieScript.global_gtiles[_movieScript.global_gteprops.tmpos.loch].tls[_movieScript.global_gteprops.tmpos.locv].nm == @"SH grate box")) {
lookfortilecat = @"SU grates";
stringlength = 8;
}
for (int tmp_q = 3; tmp_q <= _movieScript.global_gtiles.count; tmp_q++) {
q = tmp_q;
if ((_movieScript.global_gtiles[q].nm == lookfortilecat)) {
lookfortilecat = q;
break;
}
}
patterns = new LingoPropertyList {};
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"A" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"B1" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"B2" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"espaced",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"B3" }),[new LingoSymbol("upper")] = @"ospaced",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"B4" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"C1" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"espaced",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"C2" }),[new LingoSymbol("upper")] = @"ospaced",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"E1" }),[new LingoSymbol("upper")] = @"ospaced",[new LingoSymbol("lower")] = @"espaced",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"E2" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"F1" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 1});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"F2" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 1});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"F1",@"F2" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"F3" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"F4" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"G1",@"G2" }),[new LingoSymbol("upper")] = @"dense",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 5});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"I" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 4});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"J1" }),[new LingoSymbol("upper")] = @"ospaced",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 1});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"J2" }),[new LingoSymbol("upper")] = @"ospaced",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 1});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"J1",@"J2" }),[new LingoSymbol("upper")] = @"ospaced",[new LingoSymbol("lower")] = @"ospaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 2});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"J3" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"espaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 1});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"J4" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"espaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 1});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"J3",@"J4" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"espaced",[new LingoSymbol("tall")] = 2,[new LingoSymbol("freq")] = 2});
patterns.add(new LingoPropertyList {[new LingoSymbol("tiles")] = new LingoList(new dynamic[] { @"B1",@"I" }),[new LingoSymbol("upper")] = @"espaced",[new LingoSymbol("lower")] = @"dense",[new LingoSymbol("tall")] = 1,[new LingoSymbol("freq")] = 2});
for (int tmp_q = 1; tmp_q <= patterns.count; tmp_q++) {
q = tmp_q;
for (int tmp_a = 1; tmp_a <= patterns[q].tiles.count; tmp_a++) {
a = tmp_a;
for (int tmp_b = 1; tmp_b <= _movieScript.global_gtiles[lookfortilecat].tls.count; tmp_b++) {
b = tmp_b;
if ((patterns[q].tiles[a] == LingoGlobal.chars(_movieScript.global_gtiles[lookfortilecat].tls[b].nm,stringlength,_movieScript.global_gtiles[lookfortilecat].tls[b].nm.length))) {
patterns[q].tiles[a] = b;
}
}
}
}
py = (rct.top+1);
currentpattern = patterns[_global.random(patterns.count)];
while (LingoGlobal.ToBool(LingoGlobal.op_lt(py,rct.bottom))) {
possiblepatterns = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= patterns.count; tmp_q++) {
q = tmp_q;
if (((patterns[q].upper == currentpattern.lower) & ((py+patterns[q].tall) < (rct.bottom+1)))) {
for (int tmp_a = 1; tmp_a <= patterns[q].freq; tmp_a++) {
a = tmp_a;
possiblepatterns.add(q);
}
}
}
currentpattern = patterns[possiblepatterns[_global.random(possiblepatterns.count)]];
tl = _global.random(currentpattern.tiles.count);
for (int tmp_px = (rct.left+1); tmp_px <= (rct.right-1); tmp_px++) {
px = tmp_px;
tl = (tl+1);
if ((tl > currentpattern.tiles.count)) {
tl = 1;
}
placetile(LingoGlobal.point(px,py),LingoGlobal.point(lookfortilecat,currentpattern.tiles[tl]));
}
py = (py+currentpattern.tall);
}
break;
}

return null;
}
}
}
