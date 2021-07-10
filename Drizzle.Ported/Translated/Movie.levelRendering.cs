using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: levelRendering
//
public sealed partial class MovieScript {
public dynamic renderlevel() {
dynamic tm = null;
dynamic render = null;
dynamic cols = null;
dynamic rows = null;
tm = _global._system.milliseconds;
global_gskycolor = _global.color(0,0,0);
global_gtinysignsdrawn = 0;
global_grendertrashprops = new LingoPropertyList {};
render = 0;
cols = 100;
rows = 60;
_global.member(@"finalImage").image = _global.image((cols*20),(rows*20),32);
_global.the_randomSeed = global_gloprops.tileseed;
setuplayer(3);
setuplayer(2);
setuplayer(1);
global_glastimported = @"";
_global.put(LingoGlobal.concat_space(LingoGlobal.concat_space(global_gloadedname,@"rendered in"),(_global._system.milliseconds-tm)));

return null;
}
public dynamic setuplayer(dynamic layer) {
dynamic cols = null;
dynamic rows = null;
dynamic tlset = null;
dynamic dpt = null;
dynamic frntimg = null;
dynamic mdlfrntimg = null;
dynamic mdlbckimg = null;
dynamic polecol = null;
dynamic floorsl = null;
dynamic drawlatertiles = null;
dynamic drawlasttiles = null;
dynamic shortcutentrences = null;
dynamic shortcuts = null;
dynamic q = null;
dynamic c = null;
dynamic ps = null;
dynamic tp = null;
dynamic t = null;
dynamic rct = null;
dynamic dt = null;
dynamic drawmaterials = null;
dynamic indxer = null;
dynamic tl = null;
dynamic savseed = null;
dynamic mem = null;
dynamic d = null;
dynamic q2 = null;
dynamic c2 = null;
dynamic dir = null;
dynamic z = null;
dynamic r = null;
dynamic rnd = null;
cols = 100;
rows = 60;
tlset = _global.member(@"tileSet1").image.duplicate();
if ((layer == 1)) {
dpt = 0;
}
else if ((layer == 2)) {
dpt = 10;
}
else {
dpt = 20;
}
_global.member(@"vertImg").image = _global.image((cols*20),(rows*20),32);
_global.member(@"horiImg").image = _global.image((cols*20),(rows*20),32);
frntimg = _global.image((cols*20),(rows*20),32);
mdlfrntimg = _global.image((cols*20),(rows*20),32);
mdlbckimg = _global.image((cols*20),(rows*20),32);
polecol = _global.color(255,0,0);
floorsl = new LingoPropertyList {};
drawlatertiles = new LingoPropertyList {};
drawlasttiles = new LingoPropertyList {};
shortcutentrences = new LingoPropertyList {};
shortcuts = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= cols; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= rows; tmp_c++) {
c = tmp_c;
if ((((((q+global_grendercameratilepos.loch) > 0) & ((q+global_grendercameratilepos.loch) <= global_gloprops.size.loch)) & ((c+global_grendercameratilepos.locv) > 0)) & ((c+global_grendercameratilepos.locv) <= global_gloprops.size.locv))) {
ps = (LingoGlobal.point(q,c)+global_grendercameratilepos);
tp = global_gleprops.matrix[ps.loch][ps.locv][layer][1];
foreach (dynamic tmp_t in global_gleprops.matrix[ps.loch][ps.locv][layer][2]) {
t = tmp_t;
switch (t) {
case 1:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(0,8,0,-8));
mdlfrntimg.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = polecol});
break;
case 2:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(8,0,-8,0));
mdlfrntimg.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = polecol});
break;
case 3:
break;
case 4:
tp = 1;
break;
}
}
if (((global_gleprops.matrix[ps.loch][ps.locv][1][1] == 7) & (layer == 1))) {
shortcutentrences.add(new LingoList(new dynamic[] { _global.random(1000),ps.loch,ps.locv }));
}
else if ((global_gleprops.matrix[ps.loch][ps.locv][1][2].getpos(5) != 0)) {
if ((layer == 1)) {
if ((global_gleprops.matrix[ps.loch][ps.locv][1][1] == 1)) {
if ((new LingoList(new dynamic[] { @"material",@"default" }).getpos(global_gteprops.tlmatrix[ps.loch][ps.locv][layer].tp) != 0)) {
shortcuts.add(LingoGlobal.point(ps.loch,ps.locv));
}
}
}
else if ((layer == 2)) {
if ((global_gleprops.matrix[ps.loch][ps.locv][2][1] == 1)) {
if ((global_gleprops.matrix[ps.loch][ps.locv][1][1] != 1)) {
if ((new LingoList(new dynamic[] { @"material",@"default" }).getpos(global_gteprops.tlmatrix[ps.loch][ps.locv][layer].tp) != 0)) {
shortcuts.add(LingoGlobal.point(ps.loch,ps.locv));
}
}
}
}
}
if ((global_gteprops.tlmatrix[ps.loch][ps.locv][layer].tp == @"tileHead")) {
dt = global_gteprops.tlmatrix[ps.loch][ps.locv][layer].data;
if ((global_gtiles[dt[1].loch].tls[dt[1].locv].tags.getpos(@"drawLast") != 0)) {
drawlasttiles.add(new LingoList(new dynamic[] { _global.random(999),ps.loch,ps.locv }));
}
else {
drawlatertiles.add(new LingoList(new dynamic[] { _global.random(999),ps.loch,ps.locv }));
}
}
else if ((global_gteprops.tlmatrix[ps.loch][ps.locv][layer].tp != @"tileBody")) {
drawlatertiles.add(new LingoList(new dynamic[] { _global.random(999),ps.loch,ps.locv }));
}
}
}
}
drawlatertiles.sort();
drawmaterials = new LingoPropertyList {};
indxer = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= global_gtiles[1].tls.count; tmp_q++) {
q = tmp_q;
indxer.add(global_gtiles[1].tls[q].nm);
drawmaterials.add(new LingoList(new dynamic[] { global_gtiles[1].tls[q].nm,new LingoPropertyList {},global_gtiles[1].tls[q].rendertype }));
}
foreach (dynamic tmp_tl in drawlatertiles) {
tl = tmp_tl;
savseed = _global.the_randomSeed;
_global.the_randomSeed = seedfortile(LingoGlobal.point(tl[2],tl[3]),(global_gloprops.tileseed+layer));
switch (global_gteprops.tlmatrix[tl[2]][tl[3]][layer].tp) {
case @"material":
drawmaterials[indxer.getpos(global_gteprops.tlmatrix[tl[2]][tl[3]][layer].data)][2].add(tl);
break;
case @"default":
drawmaterials[indxer.getpos(global_gteprops.defaultmaterial)][2].add(tl);
break;
case @"tileHead":
dt = global_gteprops.tlmatrix[tl[2]][tl[3]][layer].data;
frntimg = drawatiletile(tl[2],tl[3],layer,global_gtiles[dt[1].loch].tls[dt[1].locv],frntimg,dt);
break;
}
_global.the_randomSeed = savseed;
}
for (int tmp_q = 1; tmp_q <= drawmaterials.count; tmp_q++) {
q = tmp_q;
savseed = _global.the_randomSeed;
_global.the_randomSeed = (global_gloprops.tileseed+layer);
if ((drawmaterials[q][2].count > 0)) {
switch (drawmaterials[q][3]) {
case @"unified":
foreach (dynamic tmp_tl in drawmaterials[q][2]) {
tl = tmp_tl;
frntimg = drawatilematerial(tl[2],tl[3],layer,drawmaterials[q][1],frntimg);
}
break;
case @"tiles":
frntimg = rendertilematerial(layer,drawmaterials[q][1],frntimg);
break;
case @"pipeType":
foreach (dynamic tmp_tl in drawmaterials[q][2]) {
tl = tmp_tl;
if ((afamvlvledit(LingoGlobal.point(tl[2],tl[3]),layer) != 0)) {
drawpipetypetile(drawmaterials[q][1],LingoGlobal.point(tl[2],tl[3]),layer);
}
}
break;
case @"largeTrashType":
foreach (dynamic tmp_tl in drawmaterials[q][2]) {
tl = tmp_tl;
if ((afamvlvledit(LingoGlobal.point(tl[2],tl[3]),layer) == 1)) {
drawlargetrashtypetile(drawmaterials[q][1],LingoGlobal.point(tl[2],tl[3]),layer,frntimg);
}
}
break;
case @"dirtType":
foreach (dynamic tmp_tl in drawmaterials[q][2]) {
tl = tmp_tl;
if ((afamvlvledit(LingoGlobal.point(tl[2],tl[3]),layer) == 1)) {
drawdirttypetile(drawmaterials[q][1],LingoGlobal.point(tl[2],tl[3]),layer,frntimg);
}
}
break;
case @"densePipeType":
foreach (dynamic tmp_tl in drawmaterials[q][2]) {
tl = tmp_tl;
if ((afamvlvledit(LingoGlobal.point(tl[2],tl[3]),layer) != 0)) {
drawdpttile(drawmaterials[q][1],LingoGlobal.point(tl[2],tl[3]),layer,frntimg);
}
}
break;
case @"ceramicType":
foreach (dynamic tmp_tl in drawmaterials[q][2]) {
tl = tmp_tl;
if ((afamvlvledit(LingoGlobal.point(tl[2],tl[3]),layer) == 1)) {
drawceramictypetile(drawmaterials[q][1],LingoGlobal.point(tl[2],tl[3]),layer,frntimg);
}
else if (LingoGlobal.ToBool(LingoGlobal.point(tl[2],tl[3]).inside(LingoGlobal.rect(global_grendercameratilepos,(global_grendercameratilepos+LingoGlobal.point(100,60)))))) {
frntimg = drawatilematerial(tl[2],tl[3],layer,@"Standard",frntimg);
}
}
break;
}
}
_global.the_randomSeed = savseed;
}
foreach (dynamic tmp_tl in shortcuts) {
tl = tmp_tl;
if (((shortcuts.getpos((tl+LingoGlobal.point(-1,0))) > 0) & (shortcuts.getpos((tl+LingoGlobal.point(1,0))) > 0))) {
drawatiletile(tl.loch,tl.locv,layer,new LingoPropertyList {[new LingoSymbol("nm")] = @"shortCutHorizontal",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoPropertyList {},[new LingoSymbol("specs2")] = LingoGlobal.VOID,[new LingoSymbol("tp")] = @"voxelStruct",[new LingoSymbol("repeatl")] = new LingoList(new dynamic[] { 1,9 }),[new LingoSymbol("bftiles")] = 0,[new LingoSymbol("rnd")] = 1,[new LingoSymbol("ptpos")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {}},frntimg);
}
else if (((shortcuts.getpos((tl+LingoGlobal.point(0,-1))) > 0) & (shortcuts.getpos((tl+LingoGlobal.point(0,1))) > 0))) {
drawatiletile(tl.loch,tl.locv,layer,new LingoPropertyList {[new LingoSymbol("nm")] = @"shortCutVertical",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoPropertyList {},[new LingoSymbol("specs2")] = LingoGlobal.VOID,[new LingoSymbol("tp")] = @"voxelStruct",[new LingoSymbol("repeatl")] = new LingoList(new dynamic[] { 1,9 }),[new LingoSymbol("bftiles")] = 0,[new LingoSymbol("rnd")] = 1,[new LingoSymbol("ptpos")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {}},frntimg);
}
else {
drawatiletile(tl.loch,tl.locv,layer,new LingoPropertyList {[new LingoSymbol("nm")] = @"shortCutTile",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoPropertyList {},[new LingoSymbol("specs2")] = LingoGlobal.VOID,[new LingoSymbol("tp")] = @"voxelStruct",[new LingoSymbol("repeatl")] = new LingoList(new dynamic[] { 1,9 }),[new LingoSymbol("bftiles")] = 0,[new LingoSymbol("rnd")] = 1,[new LingoSymbol("ptpos")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {}},frntimg);
}
}
foreach (dynamic tmp_tl in drawlasttiles) {
tl = tmp_tl;
dt = global_gteprops.tlmatrix[tl[2]][tl[3]][layer].data;
frntimg = drawatiletile(tl[2],tl[3],layer,global_gtiles[dt[1].loch].tls[dt[1].locv],frntimg);
}
shortcutentrences.sort();
foreach (dynamic tmp_tl in shortcutentrences) {
tl = tmp_tl;
tp = @"shortCut";
if ((global_gshortcuts.indexl.getpos((LingoGlobal.point(tl[2],tl[3])-global_grendercameratilepos)) > 0)) {
tp = global_gshortcuts.scs[global_gshortcuts.indexl.getpos((LingoGlobal.point(tl[2],tl[3])-global_grendercameratilepos))];
}
mem = @"shortCut";
if ((tp == @"shortCut")) {
mem = @"shortCutArrows";
}
else if ((tp == @"playerHole")) {
mem = @"shortCutDots";
}
drawatiletile(tl[2],tl[3],1,new LingoPropertyList {[new LingoSymbol("nm")] = mem,[new LingoSymbol("sz")] = LingoGlobal.point(3,3),[new LingoSymbol("specs")] = new LingoPropertyList {},[new LingoSymbol("specs2")] = new LingoPropertyList {},[new LingoSymbol("tp")] = @"voxelStruct",[new LingoSymbol("repeatl")] = new LingoList(new dynamic[] { 1,7,12 }),[new LingoSymbol("bftiles")] = 1,[new LingoSymbol("rnd")] = -1,[new LingoSymbol("ptpos")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {}},frntimg);
}
for (int tmp_q = 0; tmp_q <= cols; tmp_q++) {
q = tmp_q;
drawverticalsurface(q,dpt);
}
for (int tmp_q = 0; tmp_q <= rows; tmp_q++) {
q = tmp_q;
drawhorizontalsurface(q,dpt);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string((dpt+5)))).image.copypixels(mdlbckimg,mdlbckimg.rect,mdlbckimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(dpt))).image.copypixels(frntimg,frntimg.rect,frntimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
d = 0;
if ((layer == 2)) {
d = 10;
}
else if ((layer == 3)) {
d = 20;
}
for (int tmp_q = 1; tmp_q <= cols; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= rows; tmp_c++) {
c = tmp_c;
q2 = (q+global_grendercameratilepos.loch);
c2 = (c+global_grendercameratilepos.locv);
if (((((q2 > 1) & (q2 < global_gloprops.size.loch)) & (c2 > 1)) & (c2 < global_gloprops.size.locv))) {
if ((global_gleprops.matrix[q2][c2][layer][2].getpos(11) > 0)) {
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
if (((((global_gleprops.matrix[(q2-1)][c2][layer][2].getpos(11) > 0) | (global_gleprops.matrix[(q2-1)][c2][layer][1] == 0)) | (global_gleprops.matrix[(q2+1)][c2][layer][2].getpos(11) > 0)) | (global_gleprops.matrix[(q2+1)][c2][layer][1] == 0))) {
rct = (rct+LingoGlobal.rect(-10,0,10,0));
}
else {
rct = (rct+LingoGlobal.rect(5,0,-5,0));
}
if (((((global_gleprops.matrix[q2][(c2-1)][layer][2].getpos(11) > 0) | (global_gleprops.matrix[q2][(c2-1)][layer][1] == 0)) | (global_gleprops.matrix[q2][(c2+1)][layer][2].getpos(11) > 0)) | (global_gleprops.matrix[q2][(c2+1)][layer][1] == 0))) {
rct = (rct+LingoGlobal.rect(0,-10,0,10));
}
else {
rct = (rct+LingoGlobal.rect(0,5,0,-5));
}
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
if ((global_gleprops.matrix[(q2+dir.loch)][(c2+dir.locv)][layer][1] != 1)) {
for (int tmp_z = d; tmp_z <= (d+8); tmp_z++) {
z = tmp_z;
for (int tmp_r = 1; tmp_r <= 3; tmp_r++) {
r = tmp_r;
rnd = _global.random(4);
_global.member(LingoGlobal.concat(@"layer",_global.@string(z))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",_global.@string(rnd))).image,((LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect((dir*10),(dir*10)))+LingoGlobal.rect((_global.random(3)-(_global.random(8)+(z-d))),(_global.random(3)-(_global.random(8)+(z-d))),((_global.random(8)-_global.random(3))-(z-d)),((_global.random(8)-_global.random(3))-(z-d)))),_global.member(LingoGlobal.concat(@"rubbleGraf",_global.@string(rnd))).image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
}
}
}
}
for (int tmp_z = d; tmp_z <= (d+8); tmp_z++) {
z = tmp_z;
for (int tmp_r = 1; tmp_r <= 4; tmp_r++) {
r = tmp_r;
if ((((_global.random(8) > (z-d)) & (_global.random(3) > 1)) | (_global.random(5) == 1))) {
rnd = _global.random(4);
_global.member(LingoGlobal.concat(@"layer",_global.@string(z))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",_global.@string(rnd))).image,(rct+LingoGlobal.rect((_global.random(8)-(_global.random(8)+(z-d))),(_global.random(8)-(_global.random(8)+(z-d))),((_global.random(8)-_global.random(8))-(z-d)),((_global.random(8)-_global.random(8))-(z-d)))),_global.member(LingoGlobal.concat(@"rubbleGraf",_global.@string(rnd))).image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
}
}
}
}
}
}
}
_global.member(LingoGlobal.concat(@"layer",_global.@string((dpt+4)))).image.copypixels(mdlfrntimg,mdlfrntimg.rect,mdlfrntimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});

return null;
}
public dynamic checkifatileissolidandsamematerial(dynamic tl,dynamic lr,dynamic mat) {
dynamic rtrn = null;
tl = LingoGlobal.point(restrict(tl.loch,1,global_gloprops.size.loch),restrict(tl.locv,1,global_gloprops.size.locv));
rtrn = 0;
if ((global_gleprops.matrix[tl.loch][tl.locv][lr][1] == 1)) {
if (((global_gteprops.tlmatrix[tl.loch][tl.locv][lr].tp == @"material") & (global_gteprops.tlmatrix[tl.loch][tl.locv][lr].data == mat))) {
rtrn = 1;
}
else if (((global_gteprops.tlmatrix[tl.loch][tl.locv][lr].tp == @"default") & (global_gteprops.defaultmaterial == mat))) {
rtrn = 1;
}
}
return rtrn;

}
public dynamic drawatilematerial(dynamic q,dynamic c,dynamic l,dynamic mat,dynamic frntimg) {
dynamic dp = null;
dynamic rct = null;
dynamic mytileset = null;
dynamic f = null;
dynamic profl = null;
dynamic gtatv = null;
dynamic pstrect = null;
dynamic id = null;
dynamic dr = null;
dynamic gtath = null;
dynamic gtrect = null;
dynamic d = null;
dynamic slp = null;
dynamic askdirs = null;
dynamic myaskdirs = null;
dynamic ad = null;
dynamic matint = null;
dynamic modder = null;
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
mytileset = global_tilesetindex.getpos(mat);
switch (global_gleprops.matrix[q][c][l][1]) {
case 1:
for (int tmp_f = 1; tmp_f <= 4; tmp_f++) {
f = tmp_f;
switch (f) {
case 1:
profl = new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1) });
gtatv = 2;
pstrect = (rct+LingoGlobal.rect(0,0,-10,-10));
break;
case 2:
profl = new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,-1) });
gtatv = 4;
pstrect = (rct+LingoGlobal.rect(10,0,0,-10));
break;
case 3:
profl = new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,1) });
gtatv = 6;
pstrect = (rct+LingoGlobal.rect(10,10,0,0));
break;
default:
profl = new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,1) });
gtatv = 8;
pstrect = (rct+LingoGlobal.rect(0,10,-10,0));
break;
}
id = @"";
foreach (dynamic tmp_dr in profl) {
dr = tmp_dr;
id = LingoGlobal.concat(id,_global.@string(ismytilesetopentothistile(mat,(LingoGlobal.point(q,c)+dr),l)));
}
if ((id == @"11")) {
if ((new LingoList(new dynamic[] { 1,2,3,4,5 }).getpos(ismytilesetopentothistile(mat,((LingoGlobal.point(q,c)+profl[1])+profl[2]),l)) > 0)) {
gtath = 10;
gtatv = 2;
}
else {
gtath = 8;
}
}
else {
gtath = new LingoList(new dynamic[] { 0,@"00",0,@"01",0,@"10" }).getpos(id);
}
if ((gtath == 4)) {
if ((gtatv == 6)) {
gtatv = 4;
}
else if ((gtatv == 8)) {
gtatv = 2;
}
}
else if ((gtath == 6)) {
if (((gtatv == 4) | (gtatv == 8))) {
gtatv = (gtatv-2);
}
}
gtrect = (LingoGlobal.rect(((gtath-1)*10),((gtatv-1)*10),(gtath*10),(gtatv*10))+LingoGlobal.rect(-5,-5,5,5));
pstrect = (pstrect-(LingoGlobal.rect(global_grendercameratilepos,global_grendercameratilepos)*20));
frntimg.copypixels(_global.member(LingoGlobal.concat(@"tileSet",_global.@string(mytileset))).image,(pstrect+LingoGlobal.rect(-5,-5,5,5)),gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
for (int tmp_d = (dp+1); tmp_d <= (dp+9); tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(LingoGlobal.concat(@"tileSet",_global.@string(mytileset))).image,(pstrect+LingoGlobal.rect(-5,-5,5,5)),(gtrect+LingoGlobal.rect(120,0,120,0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
break;
case 2:
case 3:
case 4:
case 5:
slp = global_gleprops.matrix[q][c][l][1];
askdirs = new LingoList(new dynamic[] { 0,new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,1) }),new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,1) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1) }),new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,-1) }) });
myaskdirs = askdirs[slp];
pstrect = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))-(LingoGlobal.rect(global_grendercameratilepos,global_grendercameratilepos)*20));
for (int tmp_ad = 1; tmp_ad <= myaskdirs.count; tmp_ad++) {
ad = tmp_ad;
gtrect = (LingoGlobal.rect(10,90,30,110)+LingoGlobal.rect((60*LingoGlobal.op_eq(ad,2)),(30*(slp-2)),(60*LingoGlobal.op_eq(ad,2)),(30*(slp-2))));
if (LingoGlobal.ToBool(ismytilesetopentothistile(mat,(LingoGlobal.point(q,c)+myaskdirs[ad]),l))) {
gtrect = (gtrect+LingoGlobal.rect(30,0,30,0));
}
if ((mat == @"Scaffolding")) {
for (int tmp_d = (dp+5); tmp_d <= (dp+6); tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(LingoGlobal.concat(@"tileSet",_global.@string(mytileset))).image,(pstrect+LingoGlobal.rect(-5,-5,5,5)),((gtrect+LingoGlobal.rect(-5,-5,5,5))+LingoGlobal.rect(120,0,120,0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
for (int tmp_d = (dp+8); tmp_d <= (dp+9); tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(LingoGlobal.concat(@"tileSet",_global.@string(mytileset))).image,(pstrect+LingoGlobal.rect(-5,-5,5,5)),((gtrect+LingoGlobal.rect(-5,-5,5,5))+LingoGlobal.rect(120,0,120,0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
frntimg.copypixels(_global.member(LingoGlobal.concat(@"tileSet",_global.@string(mytileset))).image,(pstrect+LingoGlobal.rect(-5,-5,5,5)),(gtrect+LingoGlobal.rect(-5,-5,5,5)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
for (int tmp_d = (dp+1); tmp_d <= (dp+9); tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(LingoGlobal.concat(@"tileSet",_global.@string(mytileset))).image,(pstrect+LingoGlobal.rect(-5,-5,5,5)),((gtrect+LingoGlobal.rect(-5,-5,5,5))+LingoGlobal.rect(120,0,120,0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
}
break;
case 6:
drawatiletile(q,c,l,new LingoPropertyList {[new LingoSymbol("nm")] = LingoGlobal.concat(LingoGlobal.concat(@"tileSet",_global.@string(mat)),@"floor"),[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoPropertyList {},[new LingoSymbol("specs2")] = LingoGlobal.VOID,[new LingoSymbol("tp")] = @"voxelStruct",[new LingoSymbol("repeatl")] = new LingoList(new dynamic[] { 6,1,1,1,1 }),[new LingoSymbol("bftiles")] = 1,[new LingoSymbol("rnd")] = 1,[new LingoSymbol("ptpos")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {}},frntimg);
break;
}
if ((new LingoList(new dynamic[] { @"Concrete",@"RainStone",@"Bricks",@"Tiny Signs" }).getpos(mat) > 0)) {
matint = new LingoList(new dynamic[] { @"Concrete",@"RainStone",@"Bricks",@"Tiny Signs" }).getpos(mat);
modder = new LingoList(new dynamic[] { 45,6,1,10 })[matint];
gtrect = LingoGlobal.rect(((q%modder)*20),((c%modder)*20),(((q%modder)+1)*20),(((c%modder)+1)*20));
if ((mat == @"Bricks")) {
gtrect = LingoGlobal.rect(0,0,20,20);
}
if (((mat == @"Tiny Signs") & (global_gtinysignsdrawn == 0))) {
drawtinysigns();
global_gtinysignsdrawn = 1;
}
switch (global_gleprops.matrix[q][c][l][1]) {
case 1:
pstrect = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))-(LingoGlobal.rect(global_grendercameratilepos,global_grendercameratilepos)*20));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(LingoGlobal.concat(mat,@"Texture")).image,pstrect,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case 2:
case 3:
case 4:
case 5:
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(LingoGlobal.concat(mat,@"Texture")).image,pstrect,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
switch (global_gleprops.matrix[q][c][l][1]) {
case 5:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.bottom) });
break;
case 4:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.bottom) });
break;
case 3:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.top) });
break;
case 2:
rct = LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20));
rct = new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.right,rct.bottom),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.right,rct.top) });
break;
}
rct = (rct-(new LingoList(new dynamic[] { global_grendercameratilepos,global_grendercameratilepos,global_grendercameratilepos,global_grendercameratilepos })*20));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"pxl").image,rct,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
break;
}
}
return frntimg;

}
public dynamic ismytilesetopentothistile(dynamic mat,dynamic tl,dynamic l) {
dynamic rtrn = null;
rtrn = 0;
if (LingoGlobal.ToBool(tl.inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
if ((new LingoList(new dynamic[] { 1,2,3,4,5 }).getpos(global_gleprops.matrix[tl.loch][tl.locv][l][1]) > 0)) {
if (((global_gteprops.tlmatrix[tl.loch][tl.locv][l].tp == @"material") & (global_gteprops.tlmatrix[tl.loch][tl.locv][l].data == mat))) {
rtrn = 1;
}
else if (((global_gteprops.tlmatrix[tl.loch][tl.locv][l].tp == @"default") & (global_gteprops.defaultmaterial == mat))) {
rtrn = 1;
}
}
}
else if ((global_gteprops.defaultmaterial == mat)) {
rtrn = 1;
}
return rtrn;

}
public dynamic drawatiletile(dynamic q,dynamic c,dynamic l,dynamic tl,dynamic frntimg,dynamic dt) {
dynamic sav2 = null;
dynamic mdpnt = null;
dynamic strt = null;
dynamic nmoftiles = null;
dynamic n = null;
dynamic g = null;
dynamic h = null;
dynamic rct = null;
dynamic getrct = null;
dynamic getrect = null;
dynamic rnd = null;
dynamic dp = null;
dynamic gtrect = null;
dynamic dir = null;
dynamic d = null;
dynamic ps = null;
dynamic ps2 = null;
dynamic seed = null;
dynamic dsplcpoint = null;
dynamic gtrect1 = null;
dynamic gtrect2 = null;
dynamic rct1 = null;
dynamic rct2 = null;
dynamic tag = null;
dynamic ps1 = null;
dynamic steps = null;
dynamic dr = null;
dynamic ornt = null;
dynamic degdir = null;
dynamic stp = null;
dynamic pos = null;
dynamic dpsl = null;
dynamic dp1 = null;
dynamic pnt = null;
dynamic img = null;
dynamic r = null;
dynamic mdpoint = null;
dynamic lst = null;
dynamic tlt = null;
dynamic tilecat = null;
dynamic a = null;
dynamic actualtlps = null;
dynamic nextisfloor = null;
dynamic previsfloor = null;
dynamic b = null;
dynamic blnd = null;
sav2 = _global.member(@"previewImprt");
if ((global_glastimported != tl.nm)) {
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Graphics\",tl.nm),@".png"));
sav2.name = @"previewImprt";
global_glastimported = tl.nm;
}
q = (q-global_grendercameratilepos.loch);
c = (c-global_grendercameratilepos.locv);
mdpnt = LingoGlobal.point(((tl.sz.loch*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer,((tl.sz.locv*new LingoDecimal(0.5))+new LingoDecimal(0.4999)).integer);
strt = (LingoGlobal.point(q,c)-(mdpnt+LingoGlobal.point(1,1)));
switch (tl.tp) {
case @"box":
nmoftiles = (tl.sz.loch*tl.sz.locv);
n = 1;
for (int tmp_g = strt.loch; tmp_g <= ((strt.loch+tl.sz.loch)-1); tmp_g++) {
g = tmp_g;
for (int tmp_h = strt.locv; tmp_h <= ((strt.locv+tl.sz.locv)-1); tmp_h++) {
h = tmp_h;
rct = LingoGlobal.rect(((g-1)*20),((h-1)*20),(g*20),(h*20));
getrct = LingoGlobal.rect(20,((n-1)*20),40,(n*20));
_global.member(@"vertImg").image.copypixels(sav2.image,rct,getrct,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
getrct = LingoGlobal.rect(0,((n-1)*20),20,(n*20));
_global.member(@"horiImg").image.copypixels(sav2.image,rct,getrct,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
n = (n+1);
}
}
rct = ((LingoGlobal.rect((strt*20),((strt+tl.sz)*20))+LingoGlobal.rect((-20*tl.bftiles),(-20*tl.bftiles),(20*tl.bftiles),(20*tl.bftiles)))+LingoGlobal.rect(-20,-20,-20,-20));
getrect = ((LingoGlobal.rect(0,0,(tl.sz.loch*20),(tl.sz.locv*20))+LingoGlobal.rect(0,0,(40*tl.bftiles),(40*tl.bftiles)))+LingoGlobal.rect(0,(nmoftiles*20),0,(nmoftiles*20)));
rnd = _global.random(tl.rnd);
getrect = (getrect+LingoGlobal.rect((getrect.width*(rnd-1)),0,(getrect.width*(rnd-1)),0));
frntimg.copypixels(sav2.image,rct,getrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case @"voxelStruct":
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
rct = ((LingoGlobal.rect((strt*20),((strt+tl.sz)*20))+LingoGlobal.rect((-20*tl.bftiles),(-20*tl.bftiles),(20*tl.bftiles),(20*tl.bftiles)))+LingoGlobal.rect(-20,-20,-20,-20));
gtrect = LingoGlobal.rect(0,0,((tl.sz.loch*20)+(40*tl.bftiles)),((tl.sz.locv*20)+(40*tl.bftiles)));
if ((tl.rnd == -1)) {
rnd = 1;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
if ((new LingoList(new dynamic[] { 0,6 }).getpos(afamvlvledit(((LingoGlobal.point(q,c)+dir)+global_grendercameratilepos),1)) != 0)) {
break;
}
else {
rnd = (rnd+1);
}
}
}
else {
rnd = _global.random(tl.rnd);
}
if ((tl.tags.getpos(@"ramp") != 0)) {
rnd = 2;
if ((afamvlvledit((LingoGlobal.point(q,c)+global_grendercameratilepos),1) == 3)) {
rnd = 1;
}
}
frntimg.copypixels(sav2.image,rct,((gtrect+LingoGlobal.rect((gtrect.width*(rnd-1)),0,(gtrect.width*(rnd-1)),0))+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
d = -1;
for (int tmp_ps = 1; tmp_ps <= tl.repeatl.count; tmp_ps++) {
ps = tmp_ps;
for (int tmp_ps2 = 1; tmp_ps2 <= tl.repeatl[ps]; tmp_ps2++) {
ps2 = tmp_ps2;
d = (d+1);
if (((d+dp) > 29)) {
break;
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string((d+dp)))).image.copypixels(sav2.image,rct,((gtrect+LingoGlobal.rect((gtrect.width*(rnd-1)),(gtrect.height*(ps-1)),(gtrect.width*(rnd-1)),(gtrect.height*(ps-1))))+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
}
break;
case @"voxelStructRandomDisplaceHorizontal":
case @"voxelStructRandomDisplaceVertical":
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
rct = ((LingoGlobal.rect((strt*20),((strt+tl.sz)*20))+LingoGlobal.rect((-20*tl.bftiles),(-20*tl.bftiles),(20*tl.bftiles),(20*tl.bftiles)))+LingoGlobal.rect(-20,-20,-20,-20));
gtrect = LingoGlobal.rect(0,0,((tl.sz.loch*20)+(40*tl.bftiles)),((tl.sz.locv*20)+(40*tl.bftiles)));
seed = _global.the_randomSeed;
if ((tl.tp == @"voxelStructRandomDisplaceVertical")) {
_global.the_randomSeed = (global_gloprops.tileseed+q);
dsplcpoint = _global.random(gtrect.height);
gtrect1 = LingoGlobal.rect(gtrect.left,gtrect.top,gtrect.right,(gtrect.top+dsplcpoint));
gtrect2 = LingoGlobal.rect(gtrect.left,(gtrect.top+dsplcpoint),gtrect.right,gtrect.bottom);
rct1 = LingoGlobal.rect(rct.left,(rct.bottom-dsplcpoint),rct.right,rct.bottom);
rct2 = LingoGlobal.rect(rct.left,rct.top,rct.right,(rct.bottom-dsplcpoint));
}
else {
_global.the_randomSeed = (global_gloprops.tileseed+c);
dsplcpoint = _global.random(gtrect.width);
gtrect1 = LingoGlobal.rect(gtrect.left,gtrect.top,(gtrect.left+dsplcpoint),gtrect.bottom);
gtrect2 = LingoGlobal.rect((gtrect.left+dsplcpoint),gtrect.top,gtrect.right,gtrect.bottom);
rct1 = LingoGlobal.rect((rct.right-dsplcpoint),rct.top,rct.right,rct.bottom);
rct2 = LingoGlobal.rect(rct.left,rct.top,(rct.right-dsplcpoint),rct.bottom);
}
_global.the_randomSeed = seed;
frntimg.copypixels(sav2.image,rct1,(gtrect1+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
frntimg.copypixels(sav2.image,rct2,(gtrect2+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
d = -1;
for (int tmp_ps = 1; tmp_ps <= tl.repeatl.count; tmp_ps++) {
ps = tmp_ps;
for (int tmp_ps2 = 1; tmp_ps2 <= tl.repeatl[ps]; tmp_ps2++) {
ps2 = tmp_ps2;
d = (d+1);
if (((d+dp) > 29)) {
break;
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string((d+dp)))).image.copypixels(sav2.image,rct1,((gtrect1+LingoGlobal.rect(0,(gtrect.height*(ps-1)),0,(gtrect.height*(ps-1))))+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string((d+dp)))).image.copypixels(sav2.image,rct2,((gtrect2+LingoGlobal.rect(0,(gtrect.height*(ps-1)),0,(gtrect.height*(ps-1))))+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
}
break;
case @"voxelStructRockType":
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
rct = ((LingoGlobal.rect((strt*20),((strt+tl.sz)*20))+LingoGlobal.rect((-20*tl.bftiles),(-20*tl.bftiles),(20*tl.bftiles),(20*tl.bftiles)))+LingoGlobal.rect(-20,-20,-20,-20));
gtrect = LingoGlobal.rect(0,0,((tl.sz.loch*20)+(40*tl.bftiles)),((tl.sz.locv*20)+(40*tl.bftiles)));
rnd = _global.random(tl.rnd);
for (int tmp_d = dp; tmp_d <= restrict(((dp+9)+(10*LingoGlobal.op_ne(tl.specs2,LingoGlobal.VOID))),0,29); tmp_d++) {
d = tmp_d;
if (LingoGlobal.ToBool(new LingoList(new dynamic[] { 12,8,4 }).getpos(d))) {
rnd = _global.random(tl.rnd);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(sav2.image,rct,((gtrect+LingoGlobal.rect((gtrect.width*(rnd-1)),0,(gtrect.width*(rnd-1)),0))+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
break;
}
foreach (dynamic tmp_tag in tl.tags) {
tag = tmp_tag;
switch (tag) {
case @"Chain Holder":
if ((dt.count > 2)) {
if ((dt[3] != @"NONE")) {
ps1 = (givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(new LingoDecimal(10.1),new LingoDecimal(10.1)));
ps2 = (givemiddleoftile((dt[3]-global_grendercameratilepos))+LingoGlobal.point(new LingoDecimal(10.1),new LingoDecimal(10.1)));
if ((l == 1)) {
dp = 2;
}
else if ((l == 2)) {
dp = 12;
}
else {
dp = 22;
}
steps = ((diag(ps1,ps2)/new LingoDecimal(12))+new LingoDecimal(0.4999)).integer;
dr = movetopoint(ps1,ps2,new LingoDecimal(1));
ornt = (_global.random(2)-1);
degdir = lookatpoint(ps1,ps2);
stp = (_global.random(100)*new LingoDecimal(0.01));
for (int tmp_q = 1; tmp_q <= steps; tmp_q++) {
q = tmp_q;
pos = (ps1+((dr*12)*(q-stp)));
if (LingoGlobal.ToBool(ornt)) {
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-6,-10,6,10));
gtrect = LingoGlobal.rect(0,0,12,20);
ornt = 0;
}
else {
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-2,-10,2,10));
gtrect = LingoGlobal.rect(13,0,16,20);
ornt = 1;
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"bigChainSegment").image,rotatetoquad(rct,degdir),gtrect,new LingoPropertyList {[new LingoSymbol("color")] = global_gloprops.pals[global_gloprops.pal].detcol,[new LingoSymbol("ink")] = 36});
}
}
}
break;
case @"fanBlade":
if ((l == 1)) {
dp = 10;
}
else if ((l == 2)) {
dp = 20;
}
else {
dp = 25;
}
rct = (LingoGlobal.rect(-23,-23,23,23)+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c))));
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp-2)))).image.copypixels(_global.member(@"fanBlade").image,rotatetoquad(rct,_global.random(360)),_global.member(@"fanBlade").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"fanBlade").image,rotatetoquad(rct,_global.random(360)),_global.member(@"fanBlade").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
break;
case @"Big Wheel":
if ((l == 1)) {
dpsl = new LingoList(new dynamic[] { 0,7 });
}
else if ((l == 2)) {
dpsl = new LingoList(new dynamic[] { 9,17 });
}
else {
dpsl = new LingoList(new dynamic[] { 19,27 });
}
rct = (LingoGlobal.rect(-90,-90,90,90)+LingoGlobal.rect((givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(10,10)),(givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(10,10))));
foreach (dynamic tmp_dp1 in dpsl) {
dp1 = tmp_dp1;
rnd = _global.random(360);
foreach (dynamic tmp_dp in new LingoList(new dynamic[] { dp1,(dp1+1),(dp1+2) })) {
dp = tmp_dp;
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"Big Wheel Graf").image,rotatetoquad(rct,(rnd+new LingoDecimal(0.001))),_global.member(@"Big Wheel Graf").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
}
break;
case @"randomCords":
if ((l == 1)) {
dp = _global.random(9);
}
else if ((l == 2)) {
dp = (10+_global.random(9));
}
else {
dp = (20+_global.random(9));
}
pnt = givemiddleoftile(LingoGlobal.point(q,(c+(tl.sz.locv/2))));
rct = (LingoGlobal.rect(-50,-50,50,50)+LingoGlobal.rect(pnt,pnt));
rnd = _global.random(7);
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"randomCords").image,rotatetoquad(rct,(-30+_global.random(60))),(LingoGlobal.rect(((rnd-1)*100),0,(rnd*100),100)+LingoGlobal.rect(1,1,1,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case @"Big Sign":
img = _global.image(60,60,1);
rnd = _global.random(20);
rct = LingoGlobal.rect(3,3,29,33);
img.copypixels(_global.member(@"bigSigns1").image,rct,LingoGlobal.rect(((rnd-1)*26),0,(rnd*26),30),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
rnd = _global.random(20);
rct = LingoGlobal.rect((3+28),3,(29+28),33);
img.copypixels(_global.member(@"bigSigns1").image,rct,LingoGlobal.rect(((rnd-1)*26),0,(rnd*26),30),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
rnd = _global.random(14);
rct = LingoGlobal.rect(3,35,(3+55),(35+24));
img.copypixels(_global.member(@"bigSigns2").image,rct,LingoGlobal.rect(((rnd-1)*55),0,(rnd*55),24),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
foreach (dynamic tmp_r in new LingoList(new dynamic[] { new LingoList(new dynamic[] { LingoGlobal.point(-4,-4),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-3,-3),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(3,3),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(4,4),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(1,1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }) })) {
r = tmp_r;
frntimg.copypixels(img,((LingoGlobal.rect(-30,-30,30,30)+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c))))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,60,60),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
frntimg.copypixels(img,(LingoGlobal.rect(-30,-30,30,30)+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c)))),LingoGlobal.rect(0,0,60,60),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,255)});
mdpnt = givemiddleoftile(LingoGlobal.point(q,c));
copypixelstoeffectcolor(@"A",dp,LingoGlobal.rect((mdpnt+LingoGlobal.point(-30,-30)),(mdpnt+LingoGlobal.point(30,30))),@"bigSignGradient",LingoGlobal.rect(0,0,60,60),1,new LingoDecimal(1));
break;
case @"Big Western Sign":
case @"Big Western Sign Tilted":
img = _global.image(36,48,1);
rnd = _global.random(20);
img.copypixels(_global.member(@"bigWesternSigns").image,img.rect,LingoGlobal.rect(((rnd-1)*36),0,(rnd*36),48),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
mdpoint = (givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(10,0));
lst = new LingoList(new dynamic[] { new LingoList(new dynamic[] { LingoGlobal.point(-4,-4),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-3,-3),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(3,3),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(4,4),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(1,1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(255,0,255) }) });
if ((tag == @"Big Western Sign Tilted")) {
tlt = (-new LingoDecimal(45.1)+_global.random(90));
foreach (dynamic tmp_r in lst) {
r = tmp_r;
frntimg.copypixels(img,rotatetoquad(((LingoGlobal.rect(mdpoint,mdpoint)+LingoGlobal.rect(-18,-24,18,24))+LingoGlobal.rect(r[1],r[1])),tlt),LingoGlobal.rect(0,0,36,48),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
}
else {
foreach (dynamic tmp_r in lst) {
r = tmp_r;
frntimg.copypixels(img,((LingoGlobal.rect(mdpoint,mdpoint)+LingoGlobal.rect(-18,-24,18,24))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,36,48),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
}
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
copypixelstoeffectcolor(@"A",dp,LingoGlobal.rect((mdpoint+LingoGlobal.point(-25,-30)),(mdpoint+LingoGlobal.point(25,30))),@"bigSignGradient",LingoGlobal.rect(0,0,60,60),1,1);
break;
case @"Small Asian Sign":
case @"small asian sign on wall":
img = _global.image(20,20,1);
rnd = _global.random(14);
rct = LingoGlobal.rect(0,1,20,18);
img.copypixels(_global.member(@"smallAsianSigns").image,rct,LingoGlobal.rect(((rnd-1)*20),0,(rnd*20),17),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
if ((tag == @"Small Asian Sign")) {
foreach (dynamic tmp_r in new LingoList(new dynamic[] { new LingoList(new dynamic[] { LingoGlobal.point(-4,-4),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-3,-3),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(3,3),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(4,4),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(1,1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(255,0,255) }) })) {
r = tmp_r;
frntimg.copypixels(img,((LingoGlobal.rect(-10,-10,10,10)+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c))))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,20,20),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
mdpnt = givemiddleoftile(LingoGlobal.point(q,c));
copypixelstoeffectcolor(@"A",dp,LingoGlobal.rect((mdpnt+LingoGlobal.point(-13,-13)),(mdpnt+LingoGlobal.point(13,13))),@"bigSignGradient",LingoGlobal.rect(0,0,60,60),1);
}
else if ((l == 1)) {
dp = 8;
}
else if ((l == 2)) {
dp = 18;
}
else {
dp = 28;
}
foreach (dynamic tmp_r in new LingoList(new dynamic[] { new LingoList(new dynamic[] { LingoGlobal.point(-4,-4),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-3,-3),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(3,3),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(4,4),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(1,1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(255,0,255) }) })) {
r = tmp_r;
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(img,((LingoGlobal.rect(-10,-10,10,10)+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c))))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,20,20),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+1)))).image.copypixels(img,((LingoGlobal.rect(-10,-10,10,10)+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c))))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,20,20),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
mdpnt = givemiddleoftile(LingoGlobal.point(q,c));
copypixelstoeffectcolor(@"A",dp,LingoGlobal.rect((mdpnt+LingoGlobal.point(-13,-13)),(mdpnt+LingoGlobal.point(13,13))),@"bigSignGradient",LingoGlobal.rect(0,0,60,60),1,1);
break;
case @"glass":
if ((l == 1)) {
rct = (LingoGlobal.rect((-10*tl.sz.loch),(-10*tl.sz.locv),(10*tl.sz.loch),(10*tl.sz.locv))+LingoGlobal.rect(givemiddleoftile(LingoGlobal.point(q,c)),givemiddleoftile(LingoGlobal.point(q,c))));
_global.member(@"glassImage").image.copypixels(_global.member(@"pxl").image,rct,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
break;
case @"harvester":
renderharvesterdetails(q,c,l,tl,frntimg,dt);
break;
case @"Temple Floor":
tilecat = 0;
for (int tmp_a = 1; tmp_a <= global_gtiles.count; tmp_a++) {
a = tmp_a;
if ((global_gtiles[a].nm == @"Temple Stone")) {
tilecat = a;
break;
}
}
actualtlps = (LingoGlobal.point(q,c)+global_grendercameratilepos);
nextisfloor = 0;
if (((actualtlps.loch+8) <= global_gteprops.tlmatrix.count)) {
if ((global_gteprops.tlmatrix[(actualtlps.loch+8)][actualtlps.locv][l].tp == @"tileHead")) {
if ((global_gteprops.tlmatrix[(actualtlps.loch+8)][actualtlps.locv][l].data[2] == @"Temple Floor")) {
nextisfloor = 1;
}
}
}
previsfloor = 0;
if (((actualtlps.loch-8) > 0)) {
if ((global_gteprops.tlmatrix[(actualtlps.loch-8)][actualtlps.locv][l].tp == @"tileHead")) {
if ((global_gteprops.tlmatrix[(actualtlps.loch-8)][actualtlps.locv][l].data[2] == @"Temple Floor")) {
previsfloor = 1;
}
}
}
if (LingoGlobal.ToBool(previsfloor)) {
frntimg = drawatiletile(((q+global_grendercameratilepos.loch)-4),((c+global_grendercameratilepos.locv)-1),l,global_gtiles[tilecat].tls[13],frntimg);
}
else {
frntimg = drawatiletile(((q+global_grendercameratilepos.loch)-3),((c+global_grendercameratilepos.locv)-1),l,global_gtiles[tilecat].tls[7],frntimg);
}
if ((nextisfloor == 0)) {
frntimg = drawatiletile(((q+global_grendercameratilepos.loch)+4),((c+global_grendercameratilepos.locv)-1),l,global_gtiles[tilecat].tls[8],frntimg);
}
break;
case @"Larger Sign":
img = _global.image((80+6),(100+6),1);
rnd = _global.random(14);
rct = LingoGlobal.rect(3,3,83,103);
img.copypixels(_global.member(@"largerSigns").image,rct,LingoGlobal.rect(((rnd-1)*80),0,(rnd*80),100),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
if ((l == 1)) {
dp = 0;
}
else if ((l == 2)) {
dp = 10;
}
else {
dp = 20;
}
mdpnt = (givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(10,0));
foreach (dynamic tmp_r in new LingoList(new dynamic[] { new LingoList(new dynamic[] { LingoGlobal.point(-4,-4),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-3,-3),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(3,3),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(4,4),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(1,1),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(0,255,0) }) })) {
r = tmp_r;
for (int tmp_d = 0; tmp_d <= 1; tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+d)))).image.copypixels(img,((LingoGlobal.rect(-43,-53,43,53)+LingoGlobal.rect(mdpnt,mdpnt))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,86,106),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(img,(LingoGlobal.rect(-43,-53,43,53)+LingoGlobal.rect(mdpnt,mdpnt)),LingoGlobal.rect(0,0,86,106),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+1)))).image.copypixels(img,(LingoGlobal.rect(-43,-53,43,53)+LingoGlobal.rect(mdpnt,mdpnt)),LingoGlobal.rect(0,0,86,106),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,255)});
_global.member(@"largeSignGrad2").image.copypixels(_global.member(@"largeSignGrad").image,LingoGlobal.rect(0,0,80,100),LingoGlobal.rect(0,0,80,100));
for (int tmp_a = 0; tmp_a <= 6; tmp_a++) {
a = tmp_a;
for (int tmp_b = 0; tmp_b <= 13; tmp_b++) {
b = tmp_b;
rct = LingoGlobal.rect(((a*16)-6),((b*8)-1),(((a+1)*16)-6),(((b+1)*8)-1));
if ((_global.random(7) == 1)) {
blnd = _global.random(_global.random(100));
_global.member(@"largeSignGrad2").image.copypixels(_global.member(@"pxl").image,(rct+LingoGlobal.rect(0,0,1,1)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("blend")] = (blnd/2)});
_global.member(@"largeSignGrad2").image.copypixels(_global.member(@"pxl").image,(rct+LingoGlobal.rect(1,1,0,0)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("blend")] = (blnd/2)});
}
else if ((_global.random(7) == 1)) {
_global.member(@"largeSignGrad2").image.copypixels(_global.member(@"pxl").image,(rct+LingoGlobal.rect(1,1,0,0)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0),[new LingoSymbol("blend")] = _global.random(_global.random(60))});
}
_global.member(@"largeSignGrad2").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(rct.left,rct.top,rct.right,(rct.top+1)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("blend")] = 20});
_global.member(@"largeSignGrad2").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(rct.left,(rct.top+1),(rct.left+1),rct.bottom),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("blend")] = 20});
}
}
copypixelstoeffectcolor(@"A",(dp+1),LingoGlobal.rect((mdpnt+LingoGlobal.point(-43,-53)),(mdpnt+LingoGlobal.point(43,53))),@"largeSignGrad2",LingoGlobal.rect(0,0,86,106),1,new LingoDecimal(1));
break;
}
}
return frntimg;

}
public dynamic drawhorizontalsurface(dynamic row,dynamic dpt) {
dynamic pnt1 = null;
dynamic pnt2 = null;
dynamic q = null;
dynamic dp = null;
pnt1 = LingoGlobal.point(0,(row*20));
pnt2 = LingoGlobal.point((global_gloprops.size.loch*20),(row*20));
for (int tmp_q = 1; tmp_q <= 10; tmp_q++) {
q = tmp_q;
dp = ((dpt+10)-q);
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"horiImg").image,LingoGlobal.rect((pnt1+LingoGlobal.point(0,15)),(pnt2+LingoGlobal.point(0,20))),(LingoGlobal.rect(pnt1,pnt2)+LingoGlobal.rect(0,(20-q),0,(21-q))),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
pnt1 = LingoGlobal.point(0,((row-1)*20));
pnt2 = LingoGlobal.point((global_gloprops.size.loch*20),((row-1)*20));
for (int tmp_q = 1; tmp_q <= 10; tmp_q++) {
q = tmp_q;
dp = ((dpt+10)-q);
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"horiImg").image,LingoGlobal.rect((pnt1+LingoGlobal.point(0,0)),(pnt2+LingoGlobal.point(0,5))),(LingoGlobal.rect(pnt1,pnt2)+LingoGlobal.rect(0,q,0,(q+1))),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}

return null;
}
public dynamic drawverticalsurface(dynamic col,dynamic dpt) {
dynamic pnt1 = null;
dynamic pnt2 = null;
dynamic q = null;
dynamic dp = null;
pnt1 = LingoGlobal.point((col*20),0);
pnt2 = LingoGlobal.point((col*20),(global_gloprops.size.locv*20));
for (int tmp_q = 1; tmp_q <= 10; tmp_q++) {
q = tmp_q;
dp = ((dpt+10)-q);
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"vertImg").image,LingoGlobal.rect((pnt1+LingoGlobal.point(15,0)),(pnt2+LingoGlobal.point(20,0))),(LingoGlobal.rect(pnt1,pnt2)+LingoGlobal.rect((20-q),0,(21-q),0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
pnt1 = LingoGlobal.point(((col-1)*20),0);
pnt2 = LingoGlobal.point(((col-1)*20),(global_gloprops.size.locv*20));
for (int tmp_q = 1; tmp_q <= 10; tmp_q++) {
q = tmp_q;
dp = ((dpt+10)-q);
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"vertImg").image,LingoGlobal.rect((pnt1+LingoGlobal.point(0,0)),(pnt2+LingoGlobal.point(5,0))),(LingoGlobal.rect(pnt1,pnt2)+LingoGlobal.rect(q,0,(q+1),0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}

return null;
}
public dynamic givedptfromcol(dynamic col) {
dynamic val = null;
dynamic q = null;
val = 255;
for (int tmp_q = 0; tmp_q <= 19; tmp_q++) {
q = tmp_q;
_global.put(val);
val = (val*new LingoDecimal(0.9)).integer;
}

return null;
}
public dynamic drawpipetypetile(dynamic mat,dynamic tl,dynamic layer) {
dynamic savseed = null;
dynamic gtpos = null;
dynamic nbrs = null;
dynamic dir = null;
dynamic mem = null;
dynamic startlayer = null;
dynamic rct = null;
dynamic d = null;
dynamic q = null;
dynamic gt = null;
savseed = _global.the_randomSeed;
_global.the_randomSeed = seedfortile(tl,(global_gloprops.tileseed+layer));
gtpos = LingoGlobal.point(0,0);
switch (global_gleprops.matrix[tl.loch][tl.locv][layer][1]) {
case 1:
nbrs = @"";
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
if (((_global.random(2) == 1) & (afamvlvledit((tl+dir),layer) == 1))) {
nbrs = LingoGlobal.concat(nbrs,@"1");
}
else {
nbrs = LingoGlobal.concat(nbrs,_global.@string(ismytilesetopentothistile(mat,(tl+dir),layer)));
}
}
switch (nbrs) {
case @"0101":
gtpos = LingoGlobal.point(2,2);
break;
case @"1010":
gtpos = LingoGlobal.point(4,2);
break;
case @"1111":
gtpos = LingoGlobal.point(6,2);
break;
case @"0111":
gtpos = LingoGlobal.point(8,2);
break;
case @"1101":
gtpos = LingoGlobal.point(10,2);
break;
case @"1110":
gtpos = LingoGlobal.point(12,2);
break;
case @"1011":
gtpos = LingoGlobal.point(14,2);
break;
case @"0011":
gtpos = LingoGlobal.point(16,2);
break;
case @"1001":
gtpos = LingoGlobal.point(18,2);
break;
case @"1100":
gtpos = LingoGlobal.point(20,2);
break;
case @"0110":
gtpos = LingoGlobal.point(22,2);
break;
case @"1000":
gtpos = LingoGlobal.point(24,2);
break;
case @"0010":
gtpos = LingoGlobal.point(26,2);
break;
case @"0100":
gtpos = LingoGlobal.point(28,2);
break;
case @"0001":
gtpos = LingoGlobal.point(30,2);
break;
}
if ((mat == @"small Pipes")) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+5)))).image.copypixels(_global.member(@"frameWork").image,LingoGlobal.rect((((tl.loch-1)-global_grendercameratilepos.loch)*20),(((tl.locv-1)-global_grendercameratilepos.locv)*20),((tl.loch-global_grendercameratilepos.loch)*20),((tl.locv-global_grendercameratilepos.locv)*20)),LingoGlobal.rect(0,0,20,20),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
break;
case 3:
gtpos = LingoGlobal.point(32,2);
break;
case 2:
gtpos = LingoGlobal.point(34,2);
break;
case 4:
gtpos = LingoGlobal.point(36,2);
break;
case 5:
gtpos = LingoGlobal.point(38,2);
break;
}
switch (mat) {
case @"small Pipes":
mem = @"pipeTiles";
break;
case @"trash":
mem = @"trashTiles2";
break;
}
foreach (dynamic tmp_startLayer in new LingoList(new dynamic[] { (((layer-1)*10)+2),(((layer-1)*10)+7) })) {
startlayer = tmp_startLayer;
gtpos.locv = new LingoList(new dynamic[] { 2,4,6,8 })[_global.random(4)];
rct = LingoGlobal.rect(((gtpos.loch-1)*20),((gtpos.locv-1)*20),(gtpos.loch*20),(gtpos.locv*20));
for (int tmp_d = startlayer; tmp_d <= (startlayer+1); tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(mem).image,(LingoGlobal.rect((((tl.loch-1)-global_grendercameratilepos.loch)*20),(((tl.locv-1)-global_grendercameratilepos.locv)*20),((tl.loch-global_grendercameratilepos.loch)*20),((tl.locv-global_grendercameratilepos.locv)*20))+LingoGlobal.rect(-10,-10,10,10)),((rct+LingoGlobal.rect(1,1,1,1))+LingoGlobal.rect(-10,-10,10,10)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
switch (mat) {
case @"trash":
for (int tmp_q = 1; tmp_q <= 3; tmp_q++) {
q = tmp_q;
d = ((new LingoList(new dynamic[] { 1,11,21 })[layer]+_global.random(9))-1);
gt = _global.random(48);
gt = (LingoGlobal.rect((50*(gt-1)),0,(50*gt),50)+LingoGlobal.rect(1,1,1,1));
rct = (givemiddleoftile((tl-global_grendercameratilepos))-(LingoGlobal.point(11,11)+LingoGlobal.point(_global.random(21),_global.random(21))));
rct = LingoGlobal.rect((rct-LingoGlobal.point(25,25)),(rct+LingoGlobal.point(25,25)));
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"assortedTrash").image,rotatetoquad(rct,_global.random(360)),gt,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = new LingoList(new dynamic[] { _global.color(255,0,0),_global.color(0,255,0),_global.color(0,0,255) })[_global.random(3)]});
}
break;
}
_global.the_randomSeed = savseed;

return null;
}
public dynamic drawlargetrashtypetile(dynamic mat,dynamic tl,dynamic layer,dynamic frntimg) {
dynamic savseed = null;
dynamic distancetoair = null;
dynamic dist = null;
dynamic dir = null;
dynamic q = null;
dynamic dp = null;
dynamic pos = null;
dynamic propaddress = null;
dynamic prop = null;
dynamic rct = null;
dynamic var = null;
savseed = _global.the_randomSeed;
_global.the_randomSeed = seedfortile(tl,(global_gloprops.tileseed+layer));
distancetoair = -1;
for (int tmp_dist = 1; tmp_dist <= 5; tmp_dist++) {
dist = tmp_dist;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
if ((afamvlvledit(((tl+dir)*dist),layer) != 1)) {
distancetoair = dist;
break;
}
}
if ((distancetoair != -1)) {
break;
}
}
if ((distancetoair == -1)) {
distancetoair = 5;
}
if ((distancetoair < 5)) {
drawpipetypetile(@"trash",tl,layer);
}
if ((distancetoair < 3)) {
for (int tmp_q = 1; tmp_q <= ((distancetoair+_global.random(2))-1); tmp_q++) {
q = tmp_q;
dp = restrict(((((layer-1)*10)+_global.random(_global.random(10)))-(1+_global.random(3))),0,29);
pos = givemiddleoftile((tl-global_grendercameratilepos));
pos = (pos+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
propaddress = global_gtrashpropoptions[_global.random(global_gtrashpropoptions.count)];
prop = global_gprops[propaddress.loch].prps[propaddress.locv];
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect((-prop.sz.loch*10),(-prop.sz.locv*10),(prop.sz.loch*10),(prop.sz.locv*10)));
global_grendertrashprops.add(new LingoList(new dynamic[] { -dp,prop.nm,propaddress,rotatetoquad(rct,_global.random(360)),new LingoPropertyList {[new LingoSymbol("settings")] = new LingoPropertyList {[new LingoSymbol("rendertime")] = 0,[new LingoSymbol("seed")] = _global.random(1000)}} }));
}
}
if ((distancetoair > 2)) {
dp = ((layer-1)*10);
pos = givemiddleoftile((tl-global_grendercameratilepos));
if ((_global.random(5) <= distancetoair)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((pos.loch-10),(pos.locv-10),(pos.loch+10),(pos.locv+10)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
var = _global.random(14);
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-30,-30,30,30));
frntimg.copypixels(_global.member(@"bigJunk").image,rotatetoquad(rct,_global.random(360)),(LingoGlobal.rect(((var-1)*60),0,(var*60),60)+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
for (int tmp_q = 1; tmp_q <= distancetoair; tmp_q++) {
q = tmp_q;
dp = ((((layer-1)*10)+_global.random(10))-1);
pos = (givemiddleoftile((tl-global_grendercameratilepos))+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
var = _global.random(14);
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-30,-30,30,30));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"bigJunk").image,rotatetoquad(rct,_global.random(360)),(LingoGlobal.rect(((var-1)*60),0,(var*60),60)+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
_global.the_randomSeed = savseed;

return null;
}
public dynamic drawdirttypetile(dynamic mat,dynamic tl,dynamic layer,dynamic frntimg) {
dynamic savseed = null;
dynamic dp = null;
dynamic pos = null;
dynamic optout = null;
dynamic var = null;
dynamic rct = null;
dynamic distancetoair = null;
dynamic ext = null;
dynamic dist = null;
dynamic dir = null;
dynamic amnt = null;
dynamic q = null;
dynamic dpadd = null;
savseed = _global.the_randomSeed;
_global.the_randomSeed = seedfortile(tl,(global_gloprops.tileseed+layer));
dp = ((layer-1)*10);
pos = givemiddleoftile((tl-global_grendercameratilepos));
optout = LingoGlobal.FALSE;
if ((layer > 1)) {
optout = LingoGlobal.op_eq(afamvlvledit(tl,(layer-1)),1);
}
if (LingoGlobal.ToBool(optout)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((layer-1)*10)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-14,-14,14,14)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
var = _global.random(4);
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-18,-18,18,18));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image,rotatetoquad(rct,_global.random(360)),_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
else {
distancetoair = 6;
ext = 0;
for (int tmp_dist = 1; tmp_dist <= 5; tmp_dist++) {
dist = tmp_dist;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(-1,-1),LingoGlobal.point(0,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
dir = tmp_dir;
if ((afamvlvledit(((tl+dir)*dist),layer) != 1)) {
distancetoair = dist;
ext = 1;
break;
}
}
if (LingoGlobal.ToBool(ext)) {
break;
}
}
distancetoair = ((distancetoair+-2)+_global.random(3));
if ((distancetoair >= 5)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((layer-1)*10)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-14,-14,14,14)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
var = _global.random(4);
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-18,-18,18,18));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image,rotatetoquad(rct,_global.random(360)),_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
else {
amnt = (lerp(distancetoair,3,new LingoDecimal(0.5))*15);
if ((layer > 1)) {
amnt = (distancetoair*10);
}
for (int tmp_q = 1; tmp_q <= amnt; tmp_q++) {
q = tmp_q;
dp = ((((layer-1)*10)+_global.random(10))-1);
pos = (givemiddleoftile((tl-global_grendercameratilepos))+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
var = _global.random(4);
drawdirtclot(pos,dp,var,layer,distancetoair);
}
if ((layer < 3)) {
for (int tmp_dist = 1; tmp_dist <= 3; tmp_dist++) {
dist = tmp_dist;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(-1,-1),LingoGlobal.point(0,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
dir = tmp_dir;
if (((afamvlvledit(((tl+dir)*dist),(layer+1)) == 1) & (afamvlvledit(((tl+dir)*dist),layer) != 1))) {
for (int tmp_q = 1; tmp_q <= 10; tmp_q++) {
q = tmp_q;
if ((layer == 1)) {
dpadd = (6+_global.random(4));
}
else {
dpadd = (2+_global.random(8));
}
pos = (((((((givemiddleoftile((tl-global_grendercameratilepos))+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))))+dir)*dist)*dist)*dpadd)*_global.random(85))*new LingoDecimal(0.01));
var = _global.random(4);
drawdirtclot(pos,(((layer-1)*10)+dpadd),var,layer,distancetoair);
}
}
}
}
}
}
}
_global.the_randomSeed = savseed;

return null;
}
public dynamic drawdirtclot(dynamic pos,dynamic dp,dynamic var,dynamic layer,dynamic distancetoair) {
dynamic szadd = null;
dynamic d = null;
dynamic sz = null;
dynamic pstdp = null;
dynamic rct = null;
szadd = (_global.random((distancetoair+1))-1);
for (int tmp_d = 0; tmp_d <= 2; tmp_d++) {
d = tmp_d;
sz = (((5+szadd)+d)*2);
pstdp = restrict((dp-(1+d)),0,29);
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-sz,-sz,sz,sz));
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image,rotatetoquad(rct,_global.random(360)),_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
if ((((_global.random(6) > distancetoair) & (_global.random(3) == 1)) | (((afamvlvledit((givegridpos((pos+LingoGlobal.point(-10,-10)))+global_grendercameratilepos),layer) != 1) & (afamvlvledit((givegridpos((pos+LingoGlobal.point(10,10)))+global_grendercameratilepos),layer) == 1)) | (layer == 2)))) {
for (int tmp_d = 0; tmp_d <= 2; tmp_d++) {
d = tmp_d;
sz = (((2+(szadd*new LingoDecimal(0.5)))+d)*2);
pstdp = restrict((dp-(1+d)),0,29);
rct = ((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-sz,-sz,sz,sz))+LingoGlobal.rect((LingoGlobal.point(-4,-4)+LingoGlobal.point((-2*d),(-2*d))),(LingoGlobal.point(-4,-4)+LingoGlobal.point((-2*d),(-2*d)))));
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image,rotatetoquad(rct,_global.random(360)),_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
}
}
if ((((_global.random(6) > distancetoair) & (_global.random(3) == 1)) | (((afamvlvledit((givegridpos((pos+LingoGlobal.point(10,10)))+global_grendercameratilepos),layer) != 1) & (afamvlvledit((givegridpos((pos+LingoGlobal.point(-10,-10)))+global_grendercameratilepos),layer) == 1)) | (layer == 2)))) {
for (int tmp_d = 0; tmp_d <= 2; tmp_d++) {
d = tmp_d;
sz = (((2+(szadd*new LingoDecimal(0.5)))+d)*2);
pstdp = restrict((dp-(1+d)),0,29);
rct = ((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-sz,-sz,sz,sz))+LingoGlobal.rect((LingoGlobal.point(4,4)+LingoGlobal.point((2*d),(2*d))),(LingoGlobal.point(4,4)+LingoGlobal.point((2*d),(2*d)))));
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image,rotatetoquad(rct,_global.random(360)),_global.member(LingoGlobal.concat(@"rubbleGraf",var)).image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
}
}

return null;
}
public dynamic drawceramictypetile(dynamic mat,dynamic tl,dynamic layer,dynamic frntimg) {
dynamic savseed = null;
dynamic chaos = null;
dynamic docolor = null;
dynamic q = null;
dynamic dp = null;
dynamic pos = null;
dynamic clr = null;
dynamic lft = null;
dynamic rght = null;
dynamic tp = null;
dynamic bttm = null;
dynamic f = null;
dynamic a = null;
savseed = _global.the_randomSeed;
_global.the_randomSeed = seedfortile(tl,(global_gloprops.tileseed+layer));
chaos = 0;
docolor = 0;
for (int tmp_q = 1; tmp_q <= global_geeprops.effects.count; tmp_q++) {
q = tmp_q;
if ((global_geeprops.effects[q].nm == @"Ceramic Chaos")) {
if ((global_geeprops.effects[q].mtrx[tl.loch][tl.locv] > chaos)) {
chaos = global_geeprops.effects[q].mtrx[tl.loch][tl.locv];
}
if ((global_geeprops.effects[q].options[2][3] == @"White")) {
docolor = LingoGlobal.TRUE;
}
}
}
if (LingoGlobal.ToBool(docolor)) {
global_ganydecals = LingoGlobal.TRUE;
}
chaos = (chaos*new LingoDecimal(0.01));
dp = ((layer-1)*10);
pos = givemiddleoftile((tl-global_grendercameratilepos));
clr = _global.color(239,234,224);
lft = 0;
rght = 0;
tp = 0;
bttm = 0;
if ((afamvlvledit((tl+LingoGlobal.point(-1,0)),layer) != 1)) {
lft = 1;
}
if ((afamvlvledit((tl+LingoGlobal.point(1,0)),layer) != 1)) {
rght = 1;
}
if ((afamvlvledit((tl+LingoGlobal.point(0,-1)),layer) != 1)) {
tp = 1;
}
if ((afamvlvledit((tl+LingoGlobal.point(0,1)),layer) != 1)) {
bttm = 1;
}
for (int tmp_q = 1; tmp_q <= 9; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect((-10+lft),(-10+tp),(10-rght),(10-bttm))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color((255*(1-docolor)),(255*docolor),0)});
}
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+1)))).image.copypixels(_global.member(@"ceramicTileSocket").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-8,-8,8,8)),_global.member(@"ceramicTileSocket").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color((255*docolor),(255*(1-docolor)),0)});
if ((LingoGlobal.ToBool(lft) & (_global.random(120) > (((chaos*chaos)*chaos)*100)))) {
for (int tmp_q = 2; tmp_q <= 8; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-11,-9,-9,9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-11,-9,-9,-8)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
if (LingoGlobal.ToBool(docolor)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q))),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-11,-9,-9,9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
}
}
}
if ((LingoGlobal.ToBool(rght) & (_global.random(120) > (((chaos*chaos)*chaos)*100)))) {
for (int tmp_q = 2; tmp_q <= 8; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(9,-9,11,9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(9,-9,11,-8)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
if (LingoGlobal.ToBool(docolor)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q))),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(9,-9,11,9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
}
}
}
if ((LingoGlobal.ToBool(tp) & (_global.random(120) > (((chaos*chaos)*chaos)*100)))) {
for (int tmp_q = 2; tmp_q <= 8; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-11,9,-9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-11,-8,-9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
if (LingoGlobal.ToBool(docolor)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q))),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-11,9,-9)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
}
}
}
if ((LingoGlobal.ToBool(bttm) & (_global.random(120) > (((chaos*chaos)*chaos)*100)))) {
for (int tmp_q = 2; tmp_q <= 8; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,9,9,11)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q)))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,9,-8,11)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
if (LingoGlobal.ToBool(docolor)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string((((layer-1)*10)+q))),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,9,9,11)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
}
}
}
pos = ((((((pos+LingoGlobal.point((-7+_global.random(13)),(-7+_global.random(13))))*chaos)*chaos)*chaos)*_global.random(100))*new LingoDecimal(0.01));
if (((chaos == 0) | (_global.random((300-(((298*chaos)*chaos)*chaos))) > 1))) {
if ((_global.random(100) < (chaos*100))) {
f = (_global.random(((1000+4000)*chaos))*chaos).integer;
for (int tmp_a = 1; tmp_a <= ((new LingoDecimal(1)-chaos)*4); tmp_a++) {
a = tmp_a;
f = _global.random(f);
if ((f == 1)) {
break;
}
}
}
else {
f = 1;
}
if ((LingoGlobal.abs(f) > 1)) {
f = (f-1);
if ((_global.random(2) == 1)) {
f = (f*-1);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(((layer-1)*10)))).image.copypixels(_global.member(@"ceramicTile").image,rotatetoquad((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-9,9,9)),((-new LingoDecimal(90.05122)+f)*new LingoDecimal(0.01))),_global.member(@"ceramicTile").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
if (LingoGlobal.ToBool(docolor)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(((layer-1)*10))),@"dc")).image.copypixels(_global.member(@"ceramicTileSilh").image,rotatetoquad((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-9,9,9)),((-new LingoDecimal(90.05122)+f)*new LingoDecimal(0.01))),_global.member(@"ceramicTileSilh").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
}
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((layer-1)*10)))).image.copypixels(_global.member(@"ceramicTile").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-9,9,9)),_global.member(@"ceramicTile").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
if (LingoGlobal.ToBool(docolor)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(((layer-1)*10))),@"dc")).image.copypixels(_global.member(@"ceramicTileSilh").image,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-9,-9,9,9)),_global.member(@"ceramicTileSilh").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
}
}
}
_global.the_randomSeed = savseed;

return null;
}
public dynamic drawdpttile(dynamic mat,dynamic tl,dynamic layer,dynamic frntimg) {
dynamic savseed = null;
dynamic pos = null;
dynamic pstlr = null;
dynamic a = null;
dynamic var = null;
dynamic q = null;
dynamic lst = null;
dynamic lftdp = null;
dynamic rghtdp = null;
dynamic tpdp = null;
dynamic bttmdp = null;
dynamic lft = null;
dynamic rght = null;
dynamic tp = null;
dynamic bttm = null;
dynamic rand = null;
savseed = _global.the_randomSeed;
_global.the_randomSeed = seedfortile(tl,(global_gloprops.tileseed+layer));
pos = givemiddleoftile((tl-global_grendercameratilepos));
pstlr = dpstartlayeroftile(tl,layer);
if ((afamvlvledit(tl,layer) > 1)) {
a = afamvlvledit(tl,layer);
var = 16;
switch (a) {
case 2:
var = 20;
break;
case 3:
var = 19;
break;
case 4:
var = 17;
break;
case 5:
var = 18;
break;
}
for (int tmp_q = pstlr; tmp_q <= ((layer*10)-1); tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",q)).image.copypixels(_global.member(LingoGlobal.concat(mat,@"image")).image,LingoGlobal.rect((pos-LingoGlobal.point(20,20)),(pos+LingoGlobal.point(20,20))),LingoGlobal.rect(((var-1)*40),1,(var*40),41),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
lst = new LingoList(new dynamic[] { @"0000",@"1111",@"0101",@"1010",@"0001",@"1000",@"0100",@"0010",@"1001",@"1100",@"0110",@"0011",@"1011",@"1101",@"1110",@"0111" });
lftdp = dpstartlayeroftile((tl+LingoGlobal.point(-1,0)),layer);
rghtdp = dpstartlayeroftile((tl+LingoGlobal.point(1,0)),layer);
tpdp = dpstartlayeroftile((tl+LingoGlobal.point(0,-1)),layer);
bttmdp = dpstartlayeroftile((tl+LingoGlobal.point(0,1)),layer);
for (int tmp_q = pstlr; tmp_q <= ((layer*10)-1); tmp_q++) {
q = tmp_q;
lft = ((solidafamv((tl+LingoGlobal.point(-1,0)),layer)*dpcircuitconnection((tl+LingoGlobal.point(-1,0)),q).loch)*LingoGlobal.op_le(lftdp,q));
rght = ((solidafamv((tl+LingoGlobal.point(1,0)),layer)*dpcircuitconnection(tl,q).loch)*LingoGlobal.op_le(rghtdp,q));
tp = ((solidafamv((tl+LingoGlobal.point(0,-1)),layer)*dpcircuitconnection((tl+LingoGlobal.point(0,-1)),q).locv)*LingoGlobal.op_le(tpdp,q));
bttm = ((solidafamv((tl+LingoGlobal.point(0,1)),layer)*dpcircuitconnection(tl,q).locv)*LingoGlobal.op_le(bttmdp,q));
if ((afamvlvledit((tl+LingoGlobal.point(-1,0)),layer) > 1)) {
lft = 1;
}
if ((afamvlvledit((tl+LingoGlobal.point(1,0)),layer) > 1)) {
rght = 1;
}
if ((afamvlvledit((tl+LingoGlobal.point(0,-1)),layer) > 1)) {
tp = 1;
}
if ((afamvlvledit((tl+LingoGlobal.point(0,1)),layer) > 1)) {
bttm = 1;
}
var = lst.getpos(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(_global.@string(lft),_global.@string(tp)),_global.@string(rght)),_global.@string(bttm)));
rand = 1;
if ((mat == @"circuits")) {
rand = _global.random(5);
}
_global.member(LingoGlobal.concat(@"layer",q)).image.copypixels(_global.member(LingoGlobal.concat(mat,@"image")).image,LingoGlobal.rect((pos-LingoGlobal.point(20,20)),(pos+LingoGlobal.point(20,20))),LingoGlobal.rect(((var-1)*40),((1+(rand-1))*40),(var*40),((1+rand)*40)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
_global.the_randomSeed = savseed;

return null;
}
public dynamic dpcircuitconnection(dynamic tl,dynamic dpadd) {
dynamic savseed = null;
dynamic pnt = null;
savseed = _global.the_randomSeed;
_global.the_randomSeed = (((seedfortile(tl)+(dpadd/2).integer)*(tl.loch/3).integer)-(tl.locv/2).integer);
if ((_global.random(2) == 1)) {
pnt = LingoGlobal.point((_global.random(2)-1),(_global.random(2)-1));
}
else if ((_global.random(2) == 1)) {
pnt = LingoGlobal.point(1,0);
}
else {
pnt = LingoGlobal.point(0,1);
}
_global.the_randomSeed = savseed;
return pnt;

}
public dynamic dpstartlayeroftile(dynamic tl,dynamic layer) {
dynamic distancetoair = null;
dynamic pushin = null;
if ((layer > 1)) {
if ((afamvlvledit(tl,(layer-1)) == 1)) {
}
}
distancetoair = distancetoair(tl,layer);
if (((distancetoair >= 7) & (layer == 1))) {
}
pushin = (6-distancetoair);
pushin = ((pushin-LingoGlobal.op_eq(layer,1))-(3*LingoGlobal.op_eq(layer,3)));
pushin = restrict(pushin,((-4*LingoGlobal.op_gt(layer,1))-(5*LingoGlobal.op_eq(layer,3))),(9-(5*LingoGlobal.op_eq(layer,1))));
return (((layer-1)*10)+pushin);

}
public dynamic distancetoair(dynamic tl,dynamic layer) {
dynamic distancetoair = null;
dynamic ext = null;
dynamic dist = null;
dynamic dir = null;
distancetoair = 8;
ext = 0;
for (int tmp_dist = 1; tmp_dist <= 7; tmp_dist++) {
dist = tmp_dist;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(-1,-1),LingoGlobal.point(0,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
dir = tmp_dir;
if (((afamvlvledit(((tl+dir)*dist),layer) != 1) & (afamvlvledit(((tl+dir)*dist),restrict((layer-1),1,3)) != 1))) {
distancetoair = dist;
ext = 1;
break;
}
}
if (LingoGlobal.ToBool(ext)) {
break;
}
}
return distancetoair;

}
public dynamic drawtinysigns() {
dynamic language = null;
dynamic bluelist = null;
dynamic redlist = null;
dynamic tlsize = null;
dynamic c = null;
dynamic q = null;
dynamic mdpnt = null;
dynamic gtpos = null;
dynamic p = null;
_global.member(@"Tiny SignsTexture").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1080,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0)});
language = 2;
bluelist = new LingoList(new dynamic[] { LingoGlobal.point(1,1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) });
redlist = new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),LingoGlobal.point(-1,0),LingoGlobal.point(0,-1) });
tlsize = 8;
for (int tmp_c = 0; tmp_c <= 100; tmp_c++) {
c = tmp_c;
for (int tmp_q = 0; tmp_q <= 135; tmp_q++) {
q = tmp_q;
mdpnt = LingoGlobal.point(((q+new LingoDecimal(0.5))*tlsize),((c+new LingoDecimal(0.5))*tlsize));
gtpos = LingoGlobal.point(_global.random(new LingoList(new dynamic[] { 20,14,1 })[language]),language);
if ((_global.random(50) == 1)) {
language = 2;
}
else if ((_global.random(80) == 1)) {
language = 1;
}
if ((_global.random(7) == 1)) {
if ((_global.random(3) == 1)) {
gtpos = LingoGlobal.point(1,3);
}
else {
gtpos = LingoGlobal.point(_global.random(_global.random(7)),3);
if ((_global.random(5) == 1)) {
language = 2;
}
else if ((_global.random(10) == 1)) {
language = 1;
}
}
}
foreach (dynamic tmp_p in redlist) {
p = tmp_p;
_global.member(@"Tiny SignsTexture").image.copypixels(_global.member(@"tinySigns").image,((LingoGlobal.rect(mdpnt,mdpnt)+LingoGlobal.rect(-3,-3,3,3))+LingoGlobal.rect(p,p)),LingoGlobal.rect(((gtpos.loch-1)*6),((gtpos.locv-1)*6),(gtpos.loch*6),(gtpos.locv*6)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
}
foreach (dynamic tmp_p in bluelist) {
p = tmp_p;
_global.member(@"Tiny SignsTexture").image.copypixels(_global.member(@"tinySigns").image,((LingoGlobal.rect(mdpnt,mdpnt)+LingoGlobal.rect(-3,-3,3,3))+LingoGlobal.rect(p,p)),LingoGlobal.rect(((gtpos.loch-1)*6),((gtpos.locv-1)*6),(gtpos.loch*6),(gtpos.locv*6)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
}
_global.member(@"Tiny SignsTexture").image.copypixels(_global.member(@"tinySigns").image,(LingoGlobal.rect(mdpnt,mdpnt)+LingoGlobal.rect(-3,-3,3,3)),LingoGlobal.rect(((gtpos.loch-1)*6),((gtpos.locv-1)*6),(gtpos.loch*6),(gtpos.locv*6)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
}

return null;
}
public dynamic rendertilematerial(dynamic layer,dynamic material,dynamic frntimg) {
dynamic tlsordered = null;
dynamic q = null;
dynamic c = null;
dynamic addme = null;
dynamic tls = null;
dynamic dell = null;
dynamic tl = null;
dynamic hts = null;
dynamic dir = null;
dynamic savseed = null;
dynamic randommachines = null;
dynamic w = null;
dynamic lst = null;
dynamic h = null;
dynamic grabtiles = null;
dynamic forbidden = null;
dynamic a = null;
dynamic t = null;
dynamic randomorderlist = null;
dynamic testtile = null;
dynamic legaltoplace = null;
dynamic b = null;
dynamic testpoint = null;
dynamic spec = null;
dynamic rootpos = null;
dynamic tilecat = null;
dynamic tls2 = null;
dynamic cnt = null;
dynamic ind = null;
dynamic drawn = null;
dynamic occupy = null;
tlsordered = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if ((global_gleprops.matrix[q][c][layer][1] != 0)) {
addme = 0;
if ((global_gteprops.tlmatrix[q][c][layer].tp == @"material")) {
if ((global_gteprops.tlmatrix[q][c][layer].data == material)) {
addme = 1;
}
}
else if ((global_gteprops.defaultmaterial == material)) {
if ((global_gteprops.tlmatrix[q][c][layer].tp == @"default")) {
addme = 1;
}
}
if (LingoGlobal.ToBool(addme)) {
if ((global_gleprops.matrix[q][c][layer][1] == 1)) {
tlsordered.add(new LingoList(new dynamic[] { _global.random((global_gloprops.size.loch+global_gloprops.size.locv)),LingoGlobal.point(q,c) }));
}
else if ((material == @"Temple Stone")) {
tlsordered.add(new LingoList(new dynamic[] { _global.random((global_gloprops.size.loch+global_gloprops.size.locv)),LingoGlobal.point(q,c) }));
}
else if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(global_grendercameratilepos,(global_grendercameratilepos+LingoGlobal.point(100,60)))))) {
frntimg = drawatilematerial(q,c,layer,@"Standard",frntimg);
}
}
}
}
}
tlsordered.sort();
tls = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= tlsordered.count; tmp_q++) {
q = tmp_q;
tls.add(tlsordered[q][2]);
}
switch (material) {
case @"Chaotic Stone":
dell = new LingoPropertyList {};
foreach (dynamic tmp_tl in tls) {
tl = tmp_tl;
if ((dell.getpos(tl) == 0)) {
hts = 0;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,1),LingoGlobal.point(1,1) })) {
dir = tmp_dir;
hts = ((hts+LingoGlobal.op_gt(tls.getpos((tl+dir)),0))*LingoGlobal.op_eq(dell.getpos((tl+dir)),0));
}
if ((hts == 3)) {
if (LingoGlobal.ToBool(tl.inside(LingoGlobal.rect(global_grendercameratilepos,(global_grendercameratilepos+LingoGlobal.point(100,60)))))) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[3].tls[2],frntimg);
}
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,1),LingoGlobal.point(1,1) })) {
dir = tmp_dir;
dell.add((tl+dir));
}
dell.add(tl);
}
}
}
foreach (dynamic tmp_c in dell) {
c = tmp_c;
tls.deleteone(c);
}
savseed = _global.the_randomSeed;
while (LingoGlobal.ToBool(LingoGlobal.op_gt(tls.count,0))) {
_global.the_randomSeed = (global_gloprops.tileseed+tls.count);
tl = tls[_global.random(tls.count)];
if (LingoGlobal.ToBool(tl.inside(LingoGlobal.rect(global_grendercameratilepos,(global_grendercameratilepos+LingoGlobal.point(100,60)))))) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[3].tls[1],frntimg);
}
tls.deleteone(tl);
}
_global.the_randomSeed = savseed;
break;
case @"Tiled Stone":
dell = new LingoPropertyList {};
foreach (dynamic tmp_tl in tls) {
tl = tmp_tl;
if ((dell.getpos(tl) == 0)) {
if ((LingoGlobal.ToBool((tl.locv%2)) & LingoGlobal.ToBool(((tl.loch+LingoGlobal.op_eq((tl.locv%4),1))%2)))) {
hts = 0;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,1),LingoGlobal.point(1,1) })) {
dir = tmp_dir;
hts = ((hts+LingoGlobal.op_gt(tls.getpos((tl+dir)),0))*LingoGlobal.op_eq(dell.getpos((tl+dir)),0));
}
if ((hts == 3)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[3].tls[2],frntimg);
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(0,1),LingoGlobal.point(1,1) })) {
dir = tmp_dir;
dell.add((tl+dir));
}
dell.add(tl);
}
}
}
}
foreach (dynamic tmp_c in dell) {
c = tmp_c;
tls.deleteone(c);
}
while (LingoGlobal.ToBool(LingoGlobal.op_gt(tls.count,0))) {
tl = tls[_global.random(tls.count)];
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[3].tls[1],frntimg);
tls.deleteone(tl);
}
break;
case @"3DBricks":
while (LingoGlobal.ToBool(LingoGlobal.op_gt(tls.count,0))) {
tl = tls[_global.random(tls.count)];
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[3].tls[8],frntimg);
tls.deleteone(tl);
}
break;
case @"Random Machines":
savseed = _global.the_randomSeed;
_global.the_randomSeed = (global_gloprops.tileseed+layer);
randommachines = new LingoPropertyList {};
for (int tmp_w = 1; tmp_w <= 8; tmp_w++) {
w = tmp_w;
lst = new LingoPropertyList {};
for (int tmp_h = 1; tmp_h <= 8; tmp_h++) {
h = tmp_h;
lst.add(new LingoPropertyList {});
}
randommachines.add(lst);
}
grabtiles = new LingoList(new dynamic[] { @"Machinery",@"Machinery2",@"Small machine" });
forbidden = new LingoList(new dynamic[] { @"Feather Box - W",@"Feather Box - E",@"Piston Arm",@"Vertical Conveyor Belt A" });
for (int tmp_a = 1; tmp_a <= grabtiles.count; tmp_a++) {
a = tmp_a;
for (int tmp_q = 1; tmp_q <= global_gtiles.count; tmp_q++) {
q = tmp_q;
if ((global_gtiles[q].nm == grabtiles[a])) {
for (int tmp_t = 1; tmp_t <= global_gtiles[q].tls.count; tmp_t++) {
t = tmp_t;
if (((((global_gtiles[q].tls[t].sz.loch <= 8) & (global_gtiles[q].tls[t].sz.locv <= 8)) & (global_gtiles[q].tls[t].specs2 == 0)) & (forbidden.getpos(global_gtiles[q].tls[t].nm) == 0))) {
randommachines[global_gtiles[q].tls[t].sz.loch][global_gtiles[q].tls[t].sz.locv].add(LingoGlobal.point(q,t));
}
}
}
}
}
dell = new LingoPropertyList {};
foreach (dynamic tmp_tl in tls) {
tl = tmp_tl;
_global.the_randomSeed = seedfortile(tl,(global_gloprops.tileseed+layer));
if ((dell.getpos(tl) == 0)) {
randomorderlist = new LingoPropertyList {};
for (int tmp_w = 1; tmp_w <= randommachines.count; tmp_w++) {
w = tmp_w;
for (int tmp_h = 1; tmp_h <= randommachines[w].count; tmp_h++) {
h = tmp_h;
for (int tmp_t = 1; tmp_t <= randommachines[w][h].count; tmp_t++) {
t = tmp_t;
randomorderlist.add(new LingoList(new dynamic[] { _global.random(1000),randommachines[w][h][t] }));
}
}
}
randomorderlist.sort();
for (int tmp_q = 1; tmp_q <= randomorderlist.count; tmp_q++) {
q = tmp_q;
testtile = global_gtiles[randomorderlist[q][2].loch].tls[randomorderlist[q][2].locv];
legaltoplace = LingoGlobal.TRUE;
for (int tmp_a = 0; tmp_a <= (testtile.sz.loch-1); tmp_a++) {
a = tmp_a;
for (int tmp_b = 0; tmp_b <= (testtile.sz.locv-1); tmp_b++) {
b = tmp_b;
testpoint = (tl+LingoGlobal.point(a,b));
spec = testtile.specs[((b+1)+(a*testtile.sz.locv))];
if ((tls.getpos(testpoint) == 0)) {
legaltoplace = LingoGlobal.FALSE;
break;
}
if ((spec > -1)) {
if ((dell.getpos(testpoint) > 0)) {
legaltoplace = LingoGlobal.FALSE;
break;
}
if ((afamvlvledit(testpoint,layer) != spec)) {
legaltoplace = LingoGlobal.FALSE;
break;
}
}
}
if ((legaltoplace == LingoGlobal.FALSE)) {
break;
}
}
if (LingoGlobal.ToBool(legaltoplace)) {
rootpos = (tl+LingoGlobal.point((((LingoGlobal.floatmember_helper(testtile.sz.loch)/new LingoDecimal(2))+new LingoDecimal(0.4999)).integer-1),(((LingoGlobal.floatmember_helper(testtile.sz.locv)/new LingoDecimal(2))+new LingoDecimal(0.4999)).integer-1)));
if (LingoGlobal.ToBool(rootpos.inside(LingoGlobal.rect(global_grendercameratilepos,(global_grendercameratilepos+LingoGlobal.point(100,60)))))) {
frntimg = drawatiletile(rootpos.loch,rootpos.locv,layer,testtile,frntimg);
}
for (int tmp_a = 0; tmp_a <= (testtile.sz.loch-1); tmp_a++) {
a = tmp_a;
for (int tmp_b = 0; tmp_b <= (testtile.sz.locv-1); tmp_b++) {
b = tmp_b;
spec = testtile.specs[((b+1)+(a*testtile.sz.locv))];
if ((spec > -1)) {
dell.add((tl+LingoGlobal.point(a,b)));
}
}
}
break;
}
}
}
}
_global.the_randomSeed = savseed;
break;
case @"Temple Stone":
tilecat = 0;
for (int tmp_q = 1; tmp_q <= global_gtiles.count; tmp_q++) {
q = tmp_q;
if ((global_gtiles[q].nm == @"Temple Stone")) {
tilecat = q;
break;
}
}
global_templestonecorners = new LingoList(new dynamic[] { new LingoPropertyList {},new LingoPropertyList {},new LingoPropertyList {},new LingoPropertyList {} });
tls2 = tls.duplicate();
cnt = tls.count;
for (int tmp_q = 1; tmp_q <= cnt; tmp_q++) {
q = tmp_q;
tl = tls[((cnt+1)-q)];
if ((afamvlvledit(LingoGlobal.point(tl.loch,tl.locv),layer) == 2)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[6],frntimg);
tls.deleteat(((cnt+1)-q));
}
else if ((afamvlvledit(LingoGlobal.point(tl.loch,tl.locv),layer) == 3)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[5],frntimg);
tls.deleteat(((cnt+1)-q));
}
else if ((afamvlvledit(LingoGlobal.point(tl.loch,tl.locv),layer) == 4)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[7],frntimg);
tls.deleteat(((cnt+1)-q));
}
else if ((afamvlvledit(LingoGlobal.point(tl.loch,tl.locv),layer) == 5)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[8],frntimg);
tls.deleteat(((cnt+1)-q));
}
}
for (int tmp_q = 1; tmp_q <= tls2.count; tmp_q++) {
q = tmp_q;
tl = tls2[q];
if (((tl.locv%4) == 0)) {
if (((tl.loch%6) == 0)) {
attemptdrawtemplestone(tl,tls,2,layer,frntimg,tilecat);
}
}
if (((tl.locv%4) == 2)) {
if (((tl.loch%6) == 3)) {
attemptdrawtemplestone(tl,tls,2,layer,frntimg,tilecat);
}
}
}
for (int tmp_q = 1; tmp_q <= global_templestonecorners[1].count; tmp_q++) {
q = tmp_q;
ind = ((global_templestonecorners[1].count+1)-q);
if ((global_templestonecorners[3].getpos(global_templestonecorners[1][ind]) > 0)) {
tls.deleteone(global_templestonecorners[1][ind]);
}
}
for (int tmp_q = 1; tmp_q <= global_templestonecorners[2].count; tmp_q++) {
q = tmp_q;
ind = ((global_templestonecorners[2].count+1)-q);
if ((global_templestonecorners[4].getpos(global_templestonecorners[2][ind]) > 0)) {
tls.deleteone(global_templestonecorners[2][ind]);
}
}
while (LingoGlobal.ToBool(LingoGlobal.op_gt(tls.count,0))) {
tl = tls[_global.random(tls.count)];
drawn = LingoGlobal.FALSE;
if ((global_templestonecorners[1].getpos(tl) > 0)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[7],frntimg);
drawn = LingoGlobal.TRUE;
}
else if ((global_templestonecorners[2].getpos(tl) > 0)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[8],frntimg);
drawn = LingoGlobal.TRUE;
}
else if ((global_templestonecorners[3].getpos(tl) > 0)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[5],frntimg);
drawn = LingoGlobal.TRUE;
}
else if ((global_templestonecorners[4].getpos(tl) > 0)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[6],frntimg);
drawn = LingoGlobal.TRUE;
}
if ((drawn == LingoGlobal.FALSE)) {
occupy = new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(-1,1),LingoGlobal.point(0,0),LingoGlobal.point(0,1),LingoGlobal.point(1,0),LingoGlobal.point(1,1) });
drawn = LingoGlobal.TRUE;
for (int tmp_q = 1; tmp_q <= occupy.count; tmp_q++) {
q = tmp_q;
if ((checkifatileissolidandsamematerial((tl+occupy[q]),layer,@"Temple Stone") == LingoGlobal.FALSE)) {
drawn = LingoGlobal.FALSE;
break;
}
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
if ((global_templestonecorners[a].getpos((tl+occupy[q])) > 0)) {
drawn = LingoGlobal.FALSE;
break;
}
}
if ((drawn == LingoGlobal.FALSE)) {
break;
}
if ((tls.getpos((tl+occupy[q])) == 0)) {
drawn = LingoGlobal.FALSE;
break;
}
}
if (LingoGlobal.ToBool(drawn)) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[9],frntimg);
for (int tmp_q = 1; tmp_q <= occupy.count; tmp_q++) {
q = tmp_q;
tls.deleteone((tl+occupy[q]));
}
}
}
if ((drawn == LingoGlobal.FALSE)) {
if ((((((LingoGlobal.op_and(checkifatileissolidandsamematerial((tl+LingoGlobal.point(-1,0)),layer,@"Temple Stone"),tls.getpos((tl+LingoGlobal.point(-1,0)))) > 0) & (global_templestonecorners[1].getpos((tl+LingoGlobal.point(-1,0))) == 0)) & (global_templestonecorners[2].getpos((tl+LingoGlobal.point(-1,0))) == 0)) & (global_templestonecorners[3].getpos((tl+LingoGlobal.point(-1,0))) == 0)) & (global_templestonecorners[4].getpos((tl+LingoGlobal.point(-1,0))) == 0))) {
frntimg = drawatiletile((tl.loch-1),tl.locv,layer,global_gtiles[tilecat].tls[3],frntimg);
tls.deleteone((tl+LingoGlobal.point(-1,0)));
}
else if ((((((LingoGlobal.op_and(checkifatileissolidandsamematerial((tl+LingoGlobal.point(1,0)),layer,@"Temple Stone"),tls.getpos((tl+LingoGlobal.point(1,0)))) > 0) & (global_templestonecorners[1].getpos((tl+LingoGlobal.point(1,0))) == 0)) & (global_templestonecorners[2].getpos((tl+LingoGlobal.point(1,0))) == 0)) & (global_templestonecorners[3].getpos((tl+LingoGlobal.point(1,0))) == 0)) & (global_templestonecorners[4].getpos((tl+LingoGlobal.point(1,0))) == 0))) {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[3],frntimg);
tls.deleteone((tl+LingoGlobal.point(1,0)));
}
else {
frntimg = drawatiletile(tl.loch,tl.locv,layer,global_gtiles[tilecat].tls[4],frntimg);
}
}
tls.deleteone(tl);
}
global_templestonecorners = new LingoPropertyList {};
break;
}
return frntimg;

}
public dynamic attemptdrawtemplestone(dynamic tlpos,dynamic tileslist,dynamic templestonetype,dynamic layer,dynamic frntimg,dynamic tilecat) {
dynamic occupy = null;
dynamic q = null;
occupy = new LingoPropertyList {};
switch (templestonetype) {
case 2:
occupy = new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(0,0),LingoGlobal.point(0,1),LingoGlobal.point(1,-1),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(2,0) });
break;
case 3:
occupy = new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(1,0) });
break;
}
for (int tmp_q = 1; tmp_q <= occupy.count; tmp_q++) {
q = tmp_q;
if ((checkifatileissolidandsamematerial((tlpos+occupy[q]),layer,@"Temple Stone") == 0)) {
return tileslist;
}
}
frntimg = drawatiletile(tlpos.loch,tlpos.locv,layer,global_gtiles[tilecat].tls[templestonetype],frntimg);
if ((templestonetype == 2)) {
global_templestonecorners[1].add((tlpos+LingoGlobal.point(-1,-1)));
global_templestonecorners[2].add((tlpos+LingoGlobal.point(2,-1)));
global_templestonecorners[3].add((tlpos+LingoGlobal.point(2,1)));
global_templestonecorners[4].add((tlpos+LingoGlobal.point(-1,1)));
}
for (int tmp_q = 1; tmp_q <= occupy.count; tmp_q++) {
q = tmp_q;
tileslist.deleteone((tlpos+occupy[q]));
}
return tileslist;

}
public dynamic checkiftilehasmaterialrendertypetiles(dynamic tl,dynamic lr) {
dynamic retrn = null;
retrn = 0;
if ((LingoGlobal.ToBool(checkifatileissolidandsamematerial(tl,lr,@"Chaotic Stone")) | LingoGlobal.ToBool(checkifatileissolidandsamematerial(tl,lr,@"Tiled Stone")))) {
retrn = 1;
}
return retrn;

}
public dynamic renderharvesterdetails(dynamic q,dynamic c,dynamic l,dynamic tl,dynamic frntimg,dynamic dt) {
dynamic mdpnt = null;
dynamic big = null;
dynamic letter = null;
dynamic eyepoint = null;
dynamic armpoint = null;
dynamic actualq = null;
dynamic actualc = null;
dynamic lowerpart = null;
dynamic h = null;
dynamic lowerpartpos = null;
dynamic side = null;
dynamic dr = null;
dynamic eyepastepos = null;
dynamic mem = null;
dynamic qd = null;
dynamic dpth = null;
mdpnt = givemiddleoftile(LingoGlobal.point(q,c));
big = LingoGlobal.op_eq(dt[2],@"Harvester B");
_global.put(LingoGlobal.concat_space(dt[2],big));
if (LingoGlobal.ToBool(big)) {
letter = @"B";
mdpnt.loch = (mdpnt.loch+10);
eyepoint = LingoGlobal.point(75,-126);
armpoint = LingoGlobal.point(105,108);
}
else {
letter = @"A";
eyepoint = LingoGlobal.point(37,-85);
armpoint = LingoGlobal.point(58,60);
}
actualq = (q+global_grendercameratilepos.loch);
actualc = (c+global_grendercameratilepos.locv);
lowerpart = LingoGlobal.point(0,0);
for (int tmp_h = actualc; tmp_h <= global_gteprops.tlmatrix[actualq].count; tmp_h++) {
h = tmp_h;
if ((global_gteprops.tlmatrix[actualq][h][l].tp == @"tileHead")) {
if ((global_gteprops.tlmatrix[actualq][h][l].data[2] == LingoGlobal.concat(@"Harvester Arm ",letter))) {
lowerpart = LingoGlobal.point(q,(h-global_grendercameratilepos.locv));
}
}
}
if ((lowerpart != LingoGlobal.point(0,0))) {
lowerpartpos = givemiddleoftile(lowerpart);
if (LingoGlobal.ToBool(big)) {
lowerpartpos.loch = (lowerpartpos.loch+10);
}
}
for (int tmp_side = 1; tmp_side <= 2; tmp_side++) {
side = tmp_side;
dr = new LingoList(new dynamic[] { -1,1 })[side];
eyepastepos = (mdpnt+LingoGlobal.point((eyepoint.loch*dr),eyepoint.locv));
mem = _global.member(LingoGlobal.concat(LingoGlobal.concat(@"Harvester",letter),@"Eye"));
qd = rotatetoquad((LingoGlobal.rect(eyepastepos,eyepastepos)+LingoGlobal.rect((-mem.width/2),(-mem.height/2),(mem.width/2),(mem.height/2))),_global.random(360));
for (int tmp_dpth = (((l-1)*10)+3); tmp_dpth <= (((l-1)*10)+6); tmp_dpth++) {
dpth = tmp_dpth;
_global.member(LingoGlobal.concat(@"layer",dpth)).image.copypixels(mem.image,qd,mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
}

return null;
}
public dynamic renderbeveledimage(dynamic img,dynamic dp,dynamic qd,dynamic bevel) {
dynamic boundrect = null;
dynamic mrgn = null;
dynamic pnt = null;
dynamic qdoffset = null;
dynamic dumpimg = null;
dynamic inverseimg = null;
dynamic b = null;
dynamic a = null;
boundrect = LingoGlobal.rect(10000,10000,-10000,-10000);
mrgn = 10;
foreach (dynamic tmp_pnt in qd) {
pnt = tmp_pnt;
if (((pnt.loch-mrgn) < boundrect.left)) {
boundrect.left = (pnt.loch-mrgn);
}
if (((pnt.loch+mrgn) > boundrect.right)) {
boundrect.right = (pnt.loch+mrgn);
}
if (((pnt.locv-mrgn) < boundrect.top)) {
boundrect.top = (pnt.locv-mrgn);
}
if (((pnt.locv+mrgn) > boundrect.bottom)) {
boundrect.bottom = (pnt.locv+mrgn);
}
}
qdoffset = new LingoList(new dynamic[] { LingoGlobal.point(boundrect.left,boundrect.top),LingoGlobal.point(boundrect.left,boundrect.top),LingoGlobal.point(boundrect.left,boundrect.top),LingoGlobal.point(boundrect.left,boundrect.top) });
dumpimg = _global.image(boundrect.width,boundrect.height,1);
dumpimg.copypixels(img,(qd-qdoffset),img.rect);
inverseimg = makesilhouttefromimg(dumpimg,1);
dumpimg = _global.image(boundrect.width,boundrect.width,32);
dumpimg.copypixels(_global.member(@"pxl").image,dumpimg.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0)});
for (int tmp_b = 1; tmp_b <= bevel; tmp_b++) {
b = tmp_b;
foreach (dynamic tmp_a in new LingoList(new dynamic[] { new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-1,-1) }),new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(0,-1) }),new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-1,0) }),new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(1,1) }),new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(0,1) }),new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(1,0) }) })) {
a = tmp_a;
dumpimg.copypixels(inverseimg,(dumpimg.rect+LingoGlobal.rect((a[2]*b),(a[2]*b))),inverseimg.rect,new LingoPropertyList {[new LingoSymbol("color")] = a[1],[new LingoSymbol("ink")] = 36});
}
}
dumpimg.copypixels(inverseimg,dumpimg.rect,inverseimg.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(dumpimg,boundrect,dumpimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});

return null;
}
public dynamic drawatiletile(dynamic p1, dynamic p2, dynamic p3, dynamic p4) {
return drawatiletile(p1, p2, p3, p4, null, null);
}
public dynamic drawatiletile(dynamic p1, dynamic p2, dynamic p3, dynamic p4, dynamic p5) {
return drawatiletile(p1, p2, p3, p4, p5, null);
}
}
}
