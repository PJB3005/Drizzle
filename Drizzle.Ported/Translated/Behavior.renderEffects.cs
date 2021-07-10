using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderEffects
//
public sealed class renderEffects : LingoBehaviorScript {
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
dynamic q = null;
dynamic q2 = null;
dynamic c2 = null;
_movieScript.global_vertrepeater = (_movieScript.global_vertrepeater+1);
if ((_movieScript.global_geeprops.effects.count == 0)) {
_movieScript.global_keeplooping = 0;
return null;
}
else if ((_movieScript.global_r == 0)) {
_movieScript.global_vertrepeater = 1;
_movieScript.global_r = 1;
me.initeffect();
}
if ((((_movieScript.global_vertrepeater > 60) & (_movieScript.global_geeprops.effects[_movieScript.global_r].crossscreen == 0)) | ((_movieScript.global_vertrepeater > _movieScript.global_gloprops.size.locv) & (_movieScript.global_geeprops.effects[_movieScript.global_r].crossscreen == 1)))) {
me.exiteffect();
_movieScript.global_r = (_movieScript.global_r+1);
if ((_movieScript.global_r > _movieScript.global_geeprops.effects.count)) {
_movieScript.global_keeplooping = 0;
return null;
}
else {
me.initeffect();
_movieScript.global_vertrepeater = 1;
}
}
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].crossscreen == 0)) {
_global.sprite(59).locv = (_movieScript.global_vertrepeater*20);
for (int tmp_q = 1; tmp_q <= 100; tmp_q++) {
q = tmp_q;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (_movieScript.global_vertrepeater+_movieScript.global_grendercameratilepos.locv);
if (((((q2 > 0) & (q2 <= _movieScript.global_gloprops.size.loch)) & (c2 > 0)) & (c2 <= _movieScript.global_gloprops.size.locv))) {
me.effectontile(q,_movieScript.global_vertrepeater,q2,c2);
}
}
}
else {
_global.sprite(59).locv = ((_movieScript.global_vertrepeater-_movieScript.global_grendercameratilepos.locv)*20);
for (int tmp_q2 = 1; tmp_q2 <= _movieScript.global_gloprops.size.loch; tmp_q2++) {
q2 = tmp_q2;
me.effectontile((q2-_movieScript.global_grendercameratilepos.loch),(_movieScript.global_vertrepeater-_movieScript.global_grendercameratilepos.locv),q2,_movieScript.global_vertrepeater);
}
}

return null;
}
public dynamic effectontile(dynamic me,dynamic q,dynamic c,dynamic q2,dynamic c2) {
dynamic savseed = null;
dynamic r2 = null;
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2] > 0)) {
savseed = _global.the_randomSeed;
_global.the_randomSeed = _movieScript.seedfortile(LingoGlobal.point(q2,c2),_movieScript.global_effectseed);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].nm) {
case @"Slime":
case @"rust":
case @"barnacles":
case @"erode":
case @"melt":
case @"Roughen":
case @"SlimeX3":
case @"Destructive Melt":
case @"Super Melt":
case @"Super Erode":
case @"DecalsOnlySlime":
me.applystandarderosion(q,c,0,_movieScript.global_geeprops.effects[_movieScript.global_r].nm);
break;
case @"Root Grass":
case @"Cacti":
case @"Rubble":
case @"Rain Moss":
case @"Seed Pods":
case @"Grass":
case @"Horse Tails":
case @"Circuit Plants":
case @"Feather Plants":
me.applystandardplant(q,c,0,_movieScript.global_geeprops.effects[_movieScript.global_r].nm);
break;
case @"Growers":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(2) == 1))) {
me.applyhugeflower(q,c,0);
}
break;
case @"Arm Growers":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(2) == 1))) {
me.applyarmgrower(q,c,0);
}
break;
case @"Fungi Flowers":
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2] > 0)) {
me.applyfungiflower(q,c);
}
break;
case @"Lighthouse Flowers":
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2] > 0)) {
me.applylhflower(q,c);
}
break;
case @"Fern":
case @"Giant Mushroom":
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2] > 0)) {
me.applybigplant(q,c);
}
break;
case @"sprawlbush":
case @"featherFern":
case @"Fungus Tree":
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2] > 0)) {
me.apply3dsprawler(q,c,_movieScript.global_geeprops.effects[_movieScript.global_r].nm);
}
break;
case @"hang roots":
for (int tmp_r2 = 1; tmp_r2 <= 3; tmp_r2++) {
r2 = tmp_r2;
if ((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2])) {
me.applyhangroots(q,c,0);
}
}
break;
case @"wires":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(2) == 1))) {
me.applywire(q,c,0);
}
break;
case @"chains":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(2) == 1))) {
me.applychain(q,c,0);
}
break;
case @"blackGoo":
me.applyblackgoo(q,c,0);
break;
case @"DarkSlime":
me.applydarkslime(q,c,_movieScript.global_geeprops.effects[_movieScript.global_r].nm);
break;
case @"Restore As Scaffolding":
case @"Restore as Pipes":
me.applyrestoreeffect(q,c,q2,c2,_movieScript.global_geeprops.effects[_movieScript.global_r].nm);
break;
case @"Rollers":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(5) == 1))) {
me.applyroller(q,c,0);
}
break;
case @"Thorn Growers":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(3) > 1))) {
me.applythorngrower(q,c,0);
}
break;
case @"Garbage Spirals":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(6) == 1))) {
me.applygarbagespiral(q,c,0);
}
break;
case @"Thick Roots":
if ((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2])) {
me.applythickroots(q,c,0);
}
break;
case @"Shadow Plants":
if (((_global.random(100) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]) & (_global.random(3) == 1))) {
me.applyshadowplants(q,c,0);
}
break;
case @"DaddyCorruption":
me.applydaddycorruption(q,c,_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]);
break;
}
_global.the_randomSeed = savseed;
}

return null;
}
public dynamic initeffect(dynamic me) {
dynamic a = null;
dynamic op = null;
dynamic cols = null;
dynamic rows = null;
dynamic q = null;
dynamic c = null;
dynamic q2 = null;
dynamic c2 = null;
dynamic rct = null;
dynamic tile = null;
dynamic spnt = null;
dynamic d = null;
dynamic e = null;
dynamic ps = null;
dynamic l = null;
dynamic l2 = null;
dynamic val = null;
dynamic txt = null;
dynamic ef = null;
_movieScript.global_effectseed = 0;
for (int tmp_a = 1; tmp_a <= _movieScript.global_geeprops.effects[_movieScript.global_r].options.count; tmp_a++) {
a = tmp_a;
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].options[a][1] == @"Seed")) {
_movieScript.global_effectseed = _movieScript.global_geeprops.effects[_movieScript.global_r].options[a][3];
break;
}
}
_movieScript.global_effectin3d = LingoGlobal.FALSE;
foreach (dynamic tmp_op in _movieScript.global_geeprops.effects[_movieScript.global_r].options) {
op = tmp_op;
switch (op[1]) {
case @"Color":
_movieScript.global_colr = new LingoList(new dynamic[] { _global.color(255,0,255),_global.color(0,255,255),_global.color(0,255,0) })[new LingoList(new dynamic[] { @"Color1",@"Color2",@"Dead" }).getpos(op[3])];
_movieScript.global_gdlayer = new LingoList(new dynamic[] { @"A",@"B",@"C" })[new LingoList(new dynamic[] { @"Color1",@"Color2",@"Dead" }).getpos(op[3])];
break;
case @"Seed":
_global.the_randomSeed = op[3];
break;
case @"3D":
_movieScript.global_effectin3d = LingoGlobal.op_eq(op[3],@"On");
break;
}
}
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].nm) {
case @"blackGoo":
cols = 100;
rows = 60;
_global.member(@"blackOutImg1").image = _global.image((cols*20),(rows*20),32);
_global.member(@"blackOutImg1").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,(cols*20),(rows*20)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
_global.member(@"blackOutImg2").image = _global.image((cols*20),(rows*20),32);
_global.member(@"blackOutImg2").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,(cols*20),(rows*20)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
_global.sprite(57).visibility = 1;
_global.sprite(58).visibility = 1;
for (int tmp_q = 1; tmp_q <= 100; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= 60; tmp_c++) {
c = tmp_c;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
if (((((q2 < 1) | (q2 > _movieScript.global_gloprops.size.loch)) | (c2 < 1)) | (c2 > _movieScript.global_gloprops.size.locv))) {
_global.member(@"blackOutImg1").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"blackOutImg2").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
}
}
}
rct = _global.member(@"blob").image.rect;
for (int tmp_q2 = 1; tmp_q2 <= cols; tmp_q2++) {
q2 = tmp_q2;
for (int tmp_c2 = 1; tmp_c2 <= rows; tmp_c2++) {
c2 = tmp_c2;
if ((((((q2+_movieScript.global_grendercameratilepos.loch) > 0) & ((q2+_movieScript.global_grendercameratilepos.loch) <= _movieScript.global_gloprops.size.loch)) & ((c2+_movieScript.global_grendercameratilepos.locv) > 0)) & ((c2+_movieScript.global_grendercameratilepos.locv) <= _movieScript.global_gloprops.size.locv))) {
tile = (LingoGlobal.point(q2,c2)+_movieScript.global_grendercameratilepos);
if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[tile.loch][tile.locv] == 0)) {
spnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q2,c2))+LingoGlobal.point(-10,-10));
for (int tmp_d = 1; tmp_d <= 10; tmp_d++) {
d = tmp_d;
for (int tmp_e = 1; tmp_e <= 10; tmp_e++) {
e = tmp_e;
ps = LingoGlobal.point(((spnt.loch+d)*2),((spnt.locv+e)*2));
_global.member(@"blackOutImg1").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-6)-_global.random(_global.random(11))),((ps.locv-6)-_global.random(_global.random(11))),((ps.loch+6)+_global.random(_global.random(11))),((ps.locv+6)+_global.random(_global.random(11)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
_global.member(@"blackOutImg2").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-7)-_global.random(_global.random(14))),((ps.locv-7)-_global.random(_global.random(14))),((ps.loch+7)+_global.random(_global.random(14))),((ps.locv+7)+_global.random(_global.random(14)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
}
}
}
else if ((((_movieScript.global_gleprops.matrix[tile.loch][tile.locv][1][2].getpos(5) > 0) | (_movieScript.global_gleprops.matrix[tile.loch][tile.locv][1][2].getpos(4) > 0)) & (_movieScript.global_gleprops.matrix[tile.loch][tile.locv][2][1] == 1))) {
ps = _movieScript.givemiddleoftile(LingoGlobal.point(q2,c2));
_global.member(@"blackOutImg1").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-4)-_global.random(_global.random(9))),((ps.locv-4)-_global.random(_global.random(9))),((ps.loch+4)+_global.random(_global.random(9))),((ps.locv+4)+_global.random(_global.random(9)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
_global.member(@"blackOutImg2").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-7)-_global.random(_global.random(9))),((ps.locv-7)-_global.random(_global.random(9))),((ps.loch+7)+_global.random(_global.random(9))),((ps.locv+7)+_global.random(_global.random(9)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
_global.member(@"blackOutImg1").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-4)-_global.random(_global.random(9))),((ps.locv-4)-_global.random(_global.random(9))),((ps.loch+4)+_global.random(_global.random(9))),((ps.locv+4)+_global.random(_global.random(9)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
_global.member(@"blackOutImg2").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-7)-_global.random(_global.random(9))),((ps.locv-7)-_global.random(_global.random(9))),((ps.loch+7)+_global.random(_global.random(9))),((ps.locv+7)+_global.random(_global.random(9)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
}
}
}
}
break;
case @"fungi flowers":
l = new LingoList(new dynamic[] { 2,3,4,5 });
l2 = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
val = l[_global.random(l.count)];
l2.add(val);
l.deleteone(val);
}
_movieScript.global_geffectprops = new LingoPropertyList {[new LingoSymbol("list")] = l2,[new LingoSymbol("listpos")] = 1};
break;
case @"lighthouse flowers":
l = new LingoList(new dynamic[] { 1,2,3,4,5,6,7,8 });
l2 = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= 8; tmp_a++) {
a = tmp_a;
val = l[_global.random(l.count)];
l2.add(val);
l.deleteone(val);
}
_movieScript.global_geffectprops = new LingoPropertyList {[new LingoSymbol("list")] = l2,[new LingoSymbol("listpos")] = 1};
break;
case @"Fern":
case @"Giant Mushroom":
l = new LingoList(new dynamic[] { 1,2,3,4,5,6,7 });
l2 = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= 7; tmp_a++) {
a = tmp_a;
val = l[_global.random(l.count)];
l2.add(val);
l.deleteone(val);
}
_movieScript.global_geffectprops = new LingoPropertyList {[new LingoSymbol("list")] = l2,[new LingoSymbol("listpos")] = 1};
break;
case @"DaddyCorruption":
_movieScript.global_daddycorruptionholes = new LingoPropertyList {};
break;
}
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_ef = 1; tmp_ef <= _movieScript.global_geeprops.effects.count; tmp_ef++) {
ef = tmp_ef;
if ((ef == _movieScript.global_r)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
txt += txt.ToString();
}
_global.member(@"effectsL").text = txt;

return null;
}
public dynamic exiteffect(dynamic me) {
dynamic i = null;
dynamic qd = null;
dynamic d = null;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].nm) {
case @"blackGoo":
_global.member(@"layer0").image.copypixels(_global.member(@"blackOutImg1").image,LingoGlobal.rect(0,0,(100*20),(60*20)),LingoGlobal.rect(0,0,(100*20),(60*20)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(@"layer0").image.copypixels(_global.member(@"blackOutImg2").image,LingoGlobal.rect(0,0,(100*20),(60*20)),LingoGlobal.rect(0,0,(100*20),(60*20)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
_global.member(@"blackOutImg1").image = _global.image(1,1,1);
_global.sprite(58).visibility = 0;
_global.sprite(57).visibility = 0;
break;
case @"daddyCorruption":
for (int tmp_i = 1; tmp_i <= _movieScript.global_daddycorruptionholes.count; tmp_i++) {
i = tmp_i;
qd = _movieScript.rotatetoquad((LingoGlobal.rect(_movieScript.global_daddycorruptionholes[i][1],_movieScript.global_daddycorruptionholes[i][1])+LingoGlobal.rect(-_movieScript.global_daddycorruptionholes[i][2],-_movieScript.global_daddycorruptionholes[i][2],_movieScript.global_daddycorruptionholes[i][2],_movieScript.global_daddycorruptionholes[i][2])),_movieScript.global_daddycorruptionholes[i][3]);
for (int tmp_d = 0; tmp_d <= 1; tmp_d++) {
d = tmp_d;
_global.member(LingoGlobal.concat(@"layer",_global.@string((_movieScript.global_daddycorruptionholes[i][4]+d)))).image.copypixels(_global.member(@"DaddyBulb").image,qd,LingoGlobal.rect(60,1,134,74),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
}
if (((_global.random(2) == 1) & (_global.random(100) > _movieScript.global_daddycorruptionholes[i][5]))) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((_movieScript.global_daddycorruptionholes[i][4]+2)))).image.copypixels(_global.member(@"DaddyBulb").image,qd,LingoGlobal.rect(60,1,134,74),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0),[new LingoSymbol("ink")] = 36});
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string((_movieScript.global_daddycorruptionholes[i][4]+2)))).image.copypixels(_global.member(@"DaddyBulb").image,qd,LingoGlobal.rect(60,1,134,74),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,255),[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(@"B",(_movieScript.global_daddycorruptionholes[i][4]+2),(LingoGlobal.rect(_movieScript.global_daddycorruptionholes[i][1],_movieScript.global_daddycorruptionholes[i][1])+LingoGlobal.rect((-_movieScript.global_daddycorruptionholes[i][2]*new LingoDecimal(1.5)),(-_movieScript.global_daddycorruptionholes[i][2]*new LingoDecimal(1.5)),(_movieScript.global_daddycorruptionholes[i][2]*new LingoDecimal(1.5)),(_movieScript.global_daddycorruptionholes[i][2]*new LingoDecimal(1.5)))),@"softBrush1",_global.member(@"softBrush1").rect,new LingoDecimal(0.5),_movieScript.lerp((_global.random(50)*new LingoDecimal(0.01)),new LingoDecimal(1),(_global.random(_movieScript.global_daddycorruptionholes[i][5])*new LingoDecimal(0.01))));
}
}
_movieScript.global_daddycorruptionholes = new LingoPropertyList {};
break;
}

return null;
}
public dynamic applystandarderosion(dynamic me,dynamic q,dynamic c,dynamic eftc,dynamic tp) {
dynamic q2 = null;
dynamic c2 = null;
dynamic fc = null;
dynamic d = null;
dynamic lr = null;
dynamic sld = null;
dynamic deepeffect = null;
dynamic cntr = null;
dynamic pnt = null;
dynamic cl = null;
dynamic ofst = null;
dynamic lgt = null;
dynamic nwlr = null;
dynamic dccol = null;
dynamic a = null;
dynamic cp = null;
dynamic rct = null;
dynamic var = null;
dynamic lch = null;
dynamic lcv = null;
dynamic gtcl = null;
dynamic maskimg = null;
dynamic cpimg = null;
dynamic mvdown = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
fc = ((_movieScript.global_geeprops.effects[_movieScript.global_r].affectopenareas+(new LingoDecimal(1)-_movieScript.global_geeprops.effects[_movieScript.global_r].affectopenareas))*_movieScript.solidafamv(LingoGlobal.point(q2,c2),3));
for (int tmp_d = 1; tmp_d <= 30; tmp_d++) {
d = tmp_d;
lr = (30-d);
if (((lr == 9) | (lr == 19))) {
sld = _movieScript.global_solidmtrx[q2][c2][((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19))];
fc = ((_movieScript.global_geeprops.effects[_movieScript.global_r].affectopenareas+(new LingoDecimal(1)-_movieScript.global_geeprops.effects[_movieScript.global_r].affectopenareas))*_movieScript.solidafamv(LingoGlobal.point(q2,c2),((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19))));
}
deepeffect = 0;
if (((((lr == 0) | (lr == 10)) | (lr == 20)) | (sld == 0))) {
deepeffect = 1;
}
for (int tmp_cntr = 1; tmp_cntr <= ((((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]*(new LingoDecimal(0.2)+(new LingoDecimal(0.8)*deepeffect)))*new LingoDecimal(0.01))*_movieScript.global_geeprops.effects[_movieScript.global_r].repeats)*fc); tmp_cntr++) {
cntr = tmp_cntr;
if (LingoGlobal.ToBool(deepeffect)) {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(_global.random(20),_global.random(20)));
}
else if ((_global.random(2) == 1)) {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(((1+19)*(_global.random(2)-1)),_global.random(20)));
}
else {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(_global.random(20),((1+19)*(_global.random(2)-1))));
}
switch (tp) {
case @"rust":
case @"barnacles":
pnt = ((pnt+_movieScript.degtovec(_global.random(360)))*4);
cl = _global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel(pnt);
break;
case @"erode":
case @"Super Erode":
pnt = ((pnt+_movieScript.degtovec(_global.random(360)))*2);
if (((_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel(pnt) == _global.color(255,255,255)) | ((_global.random(108) == 1) & (tp != @"Super Erode")))) {
cl = @"GOTHROUGH";
}
else {
cl = _global.color(255,255,255);
}
break;
case @"Destructive Melt":
cl = _global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel(pnt);
if ((cl == _global.color(255,255,255))) {
cl = @"WHITE";
}
break;
default:
cl = _global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel(pnt);
break;
}
if ((cl != _global.color(255,255,255))) {
switch (tp) {
case @"slime":
case @"slimeX3":
if ((cl != _global.color(255,255,255))) {
ofst = (_global.random(2)-1);
lgt = (3+_global.random(_global.random(_global.random(6))));
if (LingoGlobal.ToBool(_movieScript.global_effectin3d)) {
nwlr = get3dlr(lr);
}
else {
nwlr = _movieScript.restrict((lr-(1+_global.random(2))),0,29);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((0+ofst),0,(1+ofst),lgt)),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = cl});
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
dccol = _global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(nwlr)),@"dc")).image.getpixel(pnt);
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(nwlr)),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((0+ofst),0,(1+ofst),lgt)),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = dccol});
}
if ((_global.random(2) == 1)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(((0+ofst)+1),1,((1+ofst)+1),(lgt-1))),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = cl});
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(nwlr)),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(((0+ofst)+1),1,((1+ofst)+1),(lgt-1))),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = dccol});
}
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(((0+ofst)-1),1,((1+ofst)-1),(lgt-1))),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = cl});
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(nwlr)),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(((0+ofst)-1),1,((1+ofst)-1),(lgt-1))),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = dccol});
}
}
}
break;
case @"DecalsOnlySlime":
ofst = (_global.random(2)-1);
lgt = (3+_global.random(_global.random(_global.random(6))));
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
dccol = _global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(lr)),@"dc")).image.getpixel(pnt);
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(lr)),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((0+ofst),0,(1+ofst),lgt)),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = dccol});
if ((_global.random(2) == 1)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(lr)),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(((0+ofst)+1),1,((1+ofst)+1),(lgt-1))),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = dccol});
}
else {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(lr)),@"dc")).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(((0+ofst)-1),1,((1+ofst)-1),(lgt-1))),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = dccol});
}
}
break;
case @"rust":
ofst = (_global.random(2)-1);
if (LingoGlobal.ToBool(_movieScript.global_effectin3d)) {
nwlr = get3dlr(lr);
}
else {
nwlr = lr;
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"rustDot").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((-2+ofst),-2,(2+ofst),2)),_global.member(@"rustDot").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = cl,[new LingoSymbol("ink")] = 36});
break;
case @"barnacles":
if (LingoGlobal.ToBool(_movieScript.global_effectin3d)) {
nwlr = get3dlr(lr);
}
else {
nwlr = _movieScript.restrict((lr-(1+_global.random(2))),0,29);
}
if (LingoGlobal.ToBool((_global.random(2)-1))) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"barnacle1").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-3,-3,4,4)),_global.member(@"barnacle1").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = cl,[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"barnacle2").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-2,-2,3,3)),_global.member(@"barnacle2").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0),[new LingoSymbol("ink")] = 36});
}
else {
ofst = (_global.random(2)-1);
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"rustDot").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((-2+ofst),-2,(2+ofst),2)),_global.member(@"rustDot").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = new LingoList(new dynamic[] { _global.color(255,0,0),cl })[_global.random(2)],[new LingoSymbol("ink")] = 36});
}
break;
case @"erode":
if ((_global.random(6) > 1)) {
nwlr = lr;
}
else {
nwlr = _movieScript.restrict((lr+1),0,29);
}
for (int tmp_a = 1; tmp_a <= 6; tmp_a++) {
a = tmp_a;
pnt = (pnt+LingoGlobal.point((-3+_global.random(5)),(-3+_global.random(5))));
ofst = (_global.random(2)-1);
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"rustDot").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((-2+ofst),-2,(2+ofst),2)),_global.member(@"rustDot").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
}
break;
case @"Super Erode":
if ((_global.random((((40+4)*lr)*LingoGlobal.op_gt(lr,19))) > 1)) {
nwlr = lr;
}
else {
nwlr = _movieScript.restrict((lr-(2+_global.random(3))),0,29);
}
for (int tmp_a = 1; tmp_a <= 6; tmp_a++) {
a = tmp_a;
pnt = (pnt+LingoGlobal.point((-4+_global.random(7)),(-4+_global.random(7))));
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(_global.member(@"SuperErodeMask").image,(LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-4,-4,4,4)),_global.member(@"SuperErodeMask").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
}
break;
case @"melt":
cp = _global.image(4,4,32);
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-2,-2,2,2));
cp.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image,LingoGlobal.rect(0,0,4,4),rct);
cp.setpixel(LingoGlobal.point(0,0),_global.color(255,255,255));
cp.setpixel(LingoGlobal.point(3,0),_global.color(255,255,255));
cp.setpixel(LingoGlobal.point(0,3),_global.color(255,255,255));
cp.setpixel(LingoGlobal.point(3,3),_global.color(255,255,255));
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(cp,(rct+LingoGlobal.rect(0,1,0,1)),LingoGlobal.rect(0,0,4,4),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(@"tst").image = cp;
break;
case @"roughen":
if ((cl == _global.color(0,255,0))) {
var = _global.random(20);
for (int tmp_lch = 0; tmp_lch <= 6; tmp_lch++) {
lch = tmp_lch;
for (int tmp_lcv = 0; tmp_lcv <= 6; tmp_lcv++) {
lcv = tmp_lcv;
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel((pnt.loch-(3+lch)),(pnt.locv-(3+lcv))) == _global.color(0,255,0))) {
gtcl = _global.member(@"roughenTexture").image.getpixel(((lch+(var-1))*7),lcv);
if ((gtcl != _global.color(255,255,255))) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.setpixel((pnt.loch-(3+lch)),(pnt.locv-(3+lcv)),gtcl);
}
}
}
}
}
break;
case @"Super Melt":
maskimg = _global.member(@"destructiveMeltMask").image;
cpimg = _global.image(maskimg.width,maskimg.height,32);
rct = LingoGlobal.rect((pnt-(LingoGlobal.point(maskimg.width,maskimg.height)/new LingoDecimal(2))),((pnt+LingoGlobal.point(maskimg.width,maskimg.height))/new LingoDecimal(2)));
cpimg.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image,cpimg.rect,rct);
cpimg.copypixels(maskimg,cpimg.rect,maskimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
mvdown = (_global.random(7)*(_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]/new LingoDecimal(100)));
if (LingoGlobal.ToBool(_movieScript.global_effectin3d)) {
nwlr = get3dlr(lr);
}
else {
nwlr = _movieScript.restrict((lr-(1+_global.random(2))),0,29);
}
if ((((lr > 6) & (nwlr <= 6)) | ((nwlr > 6) & (lr <= 6)))) {
nwlr = lr;
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(cpimg,(rct+LingoGlobal.rect(0,0,0,mvdown)),cpimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case @"Destructive Melt":
maskimg = _global.member(@"destructiveMeltMask").image;
cpimg = _global.image(maskimg.width,maskimg.height,32);
rct = LingoGlobal.rect((pnt-(LingoGlobal.point(maskimg.width,maskimg.height)/new LingoDecimal(2))),((pnt+LingoGlobal.point(maskimg.width,maskimg.height))/new LingoDecimal(2)));
cpimg.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image,cpimg.rect,rct);
pnt = LingoGlobal.point((-2+_global.random(3)),(-2+_global.random(3)));
rct = (rct+LingoGlobal.rect(pnt,pnt));
mvdown = (_global.random(7)*(_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]/new LingoDecimal(100)));
if (LingoGlobal.ToBool(_movieScript.global_effectin3d)) {
nwlr = get3dlr(lr);
}
else {
nwlr = _movieScript.restrict((lr-(1+_global.random(2))),0,29);
}
if ((((lr > 6) & (nwlr <= 6)) | ((nwlr > 6) & (lr <= 6)))) {
nwlr = lr;
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(cpimg,(rct+LingoGlobal.rect(0,0,0,mvdown)),cpimg.rect,new LingoPropertyList {[new LingoSymbol("mask")] = _global.member(@"destructiveMeltDestroy").image.createmask()});
_global.member(LingoGlobal.concat(@"layer",_global.@string(nwlr))).image.copypixels(cpimg,(rct+LingoGlobal.rect(0,0,0,(mvdown*new LingoDecimal(0.5)))),cpimg.rect,new LingoPropertyList {[new LingoSymbol("mask")] = _global.member(@"destructiveMeltDestroy").image.createmask(),[new LingoSymbol("ink")] = 36});
if ((cl == @"WHITE")) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"destructiveMeltDestroy").image,LingoGlobal.rect(rct.left,rct.top,rct.right,(rct.bottom+mvdown)),_global.member(@"destructiveMeltDestroy").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
break;
}
}
}
}

return null;
}
public dynamic get3dlr(dynamic lr) {
dynamic nwlr = null;
nwlr = _movieScript.restrict((lr-(2+_global.random(3))),0,29);
if (((lr == 6) & (nwlr == 5))) {
nwlr = 6;
}
else if (((lr == 5) & (nwlr == 6))) {
nwlr = 5;
}
return nwlr;

}
public dynamic applydarkslime(dynamic me,dynamic q,dynamic c) {
dynamic q2 = null;
dynamic c2 = null;
dynamic cls = null;
dynamic fc = null;
dynamic d = null;
dynamic lr = null;
dynamic sld = null;
dynamic deepeffect = null;
dynamic cntr = null;
dynamic pnt = null;
dynamic lgt = null;
dynamic clr = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
cls = new LingoList(new dynamic[] { _global.color(255,0,0),_global.color(0,255,0),_global.color(0,0,255) });
fc = ((0+(new LingoDecimal(1)-0))*_movieScript.solidafamv(LingoGlobal.point(q2,c2),1));
for (int tmp_d = 0; tmp_d <= 29; tmp_d++) {
d = tmp_d;
lr = d;
if ((((lr == 0) | (lr == 10)) | (lr == 20))) {
sld = _movieScript.global_solidmtrx[q2][c2][((1+LingoGlobal.op_gt(lr,9))+LingoGlobal.op_gt(lr,19))];
fc = ((0+(new LingoDecimal(1)-0))*_movieScript.solidafamv((LingoGlobal.point(q2,c2)+_movieScript.global_grendercameratilepos),((1+LingoGlobal.op_gt(lr,9))+LingoGlobal.op_gt(lr,19))));
}
deepeffect = 0;
if (((((lr == 0) | (lr == 10)) | (lr == 20)) | (sld == 0))) {
deepeffect = 1;
}
for (int tmp_cntr = 1; tmp_cntr <= ((((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]*(new LingoDecimal(0.2)+(new LingoDecimal(0.8)*deepeffect)))*new LingoDecimal(0.01))*80)*fc); tmp_cntr++) {
cntr = tmp_cntr;
if (LingoGlobal.ToBool(deepeffect)) {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(_global.random(20),_global.random(20)));
}
else if ((_global.random(2) == 1)) {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(((1+19)*(_global.random(2)-1)),_global.random(20)));
}
else {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(_global.random(20),((1+19)*(_global.random(2)-1))));
}
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel(pnt) != _global.color(255,255,255))) {
lgt = _global.random(40);
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel((pnt+LingoGlobal.point(0,lgt))) != _global.color(255,255,255))) {
clr = cls[_global.random(3)];
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(pnt,(pnt+LingoGlobal.point(1,lgt))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = clr});
if ((_global.random(2) == 1)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,(pnt+LingoGlobal.point(1,lgt)))+LingoGlobal.rect(-1,1,-1,-1)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = clr});
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt,(pnt+LingoGlobal.point(1,lgt)))+LingoGlobal.rect(1,1,1,-1)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = clr});
}
}
}
}
}

return null;
}
public dynamic giveaneffectpos(dynamic me,dynamic q,dynamic c,dynamic d,dynamic sld) {
dynamic pnt = null;
dynamic l = null;
pnt = LingoGlobal.point(0,0);
l = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if (((((d == 0) | (d == 9)) | (d == 19)) | LingoGlobal.ToBool(sld))) {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(_global.random(20),_global.random(20)));
}
else if ((_global.random(2) == 1)) {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(((1+19)*(_global.random(2)-1)),_global.random(20)));
}
else {
pnt = ((LingoGlobal.point((q-1),(c-1))*20)+LingoGlobal.point(_global.random(20),((1+19)*(_global.random(2)-1))));
}
return pnt;

}
public dynamic applystandardplant(dynamic me,dynamic q,dynamic c,dynamic eftc,dynamic tp) {
dynamic q2 = null;
dynamic c2 = null;
dynamic amount = null;
dynamic lsl = null;
dynamic layer = null;
dynamic cntr = null;
dynamic pnt = null;
dynamic lr = null;
dynamic freesides = null;
dynamic rct = null;
dynamic rnd = null;
dynamic flp = null;
dynamic gtrect = null;
dynamic sz = null;
dynamic leandir = null;
dynamic checkforsolid = null;
dynamic rep = null;
dynamic rotat = null;
dynamic tppnt = null;
dynamic rubbl = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
amount = 17;
switch (tp) {
case @"Root Grass":
amount = 12;
break;
case @"Grass":
amount = 10;
break;
case @"Seed Pods":
amount = _global.random(5);
break;
case @"Cacti":
amount = 3;
break;
case @"Rain Moss":
amount = 9;
break;
case @"rubble":
amount = 11;
break;
case @"Horse Tails":
amount = (1+_global.random(3));
break;
case @"Circuit Plants":
amount = 2;
break;
case @"Feather Plants":
amount = 4;
break;
}
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
lsl = new LingoList(new dynamic[] { 1,2,3 });
break;
case @"1":
lsl = new LingoList(new dynamic[] { 1 });
break;
case @"2":
lsl = new LingoList(new dynamic[] { 2 });
break;
case @"3":
lsl = new LingoList(new dynamic[] { 3 });
break;
case @"1:st and 2:nd":
lsl = new LingoList(new dynamic[] { 1,2 });
break;
case @"2:nd and 3:rd":
lsl = new LingoList(new dynamic[] { 2,3 });
break;
}
foreach (dynamic tmp_layer in lsl) {
layer = tmp_layer;
if (((_movieScript.global_solidmtrx[q2][c2][layer] != 1) & (_movieScript.solidafamv(LingoGlobal.point(q2,(c2+1)),layer) == 1))) {
for (int tmp_cntr = 1; tmp_cntr <= ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]*new LingoDecimal(0.01))*amount); tmp_cntr++) {
cntr = tmp_cntr;
pnt = me.givegroundpos(q,c,layer);
lr = ((_global.random(9)+(layer-1))*10);
switch (tp) {
case @"Grass":
freesides = 0;
if ((_movieScript.solidafamv(LingoGlobal.point((q2-1),(c2+1)),layer) == 0)) {
amount = (amount/2);
}
if ((_movieScript.solidafamv(LingoGlobal.point((q2+1),(c2+1)),layer) == 0)) {
amount = (amount/2);
}
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-10,-20,10,10));
rnd = _global.random(20);
flp = (_global.random(2)-1);
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
gtrect = (LingoGlobal.rect(((rnd-1)*20),0,(rnd*20),30)+LingoGlobal.rect(1,0,1,0));
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"GrassGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-10,-20,10,10));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"GrassGrad",gtrect,new LingoDecimal(0.5));
}
break;
case @"Root Grass":
freesides = 0;
if ((_movieScript.solidafamv(LingoGlobal.point((q2-1),(c2+1)),layer) == 0)) {
freesides = (freesides+1);
}
if ((_movieScript.solidafamv(LingoGlobal.point((q2+1),(c2+1)),layer) == 0)) {
freesides = (freesides+1);
}
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-5,-17,5,3));
if (((freesides > 0) | (amount < new LingoDecimal(0.5)))) {
rnd = (10+_global.random(5));
}
else {
rnd = _global.random(10);
}
flp = (_global.random(2)-1);
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
gtrect = (LingoGlobal.rect(((rnd-1)*10),0,(rnd*10),30)+LingoGlobal.rect(1,0,1,0));
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"RootGrassGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-5,-17,5,3));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"RootGrassGrad",gtrect,new LingoDecimal(0.5));
}
break;
case @"Seed Pods":
rnd = _global.random(7);
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-10,-77,10,3));
flp = (_global.random(2)-1);
gtrect = (LingoGlobal.rect(((rnd-1)*20),0,(rnd*20),80)+LingoGlobal.rect(1,0,1,0));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"SeedPodsGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-10,-77,10,3));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"SeedPodsGrad",gtrect,new LingoDecimal(0.5));
}
break;
case @"Circuit Plants":
if ((_global.random(300) > _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2])) {
rnd = _global.random(_movieScript.restrict(((20*(_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]-(11+_global.random(21))))*new LingoDecimal(0.01)).integer,1,16));
sz = ((new LingoDecimal(0.15)+new LingoDecimal(0.85))*LingoGlobal.power((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]*new LingoDecimal(0.01)),new LingoDecimal(0.85)));
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((-20*sz),(-95*sz),(20*sz),5));
flp = (_global.random(2)-1);
gtrect = (LingoGlobal.rect(((rnd-1)*40),0,(rnd*40),100)+LingoGlobal.rect(1,0,1,0));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"CircuitPlantGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((sz < new LingoDecimal(0.75))) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"CircuitPlantGraf").image,(rct+LingoGlobal.rect(1,0,1,0)),gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"CircuitPlantGraf").image,(rct+LingoGlobal.rect(0,1,0,1)),gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
}
if ((_movieScript.global_colr != _global.color(0,255,0))) {
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((-20*sz),(-95*sz),(20*sz),5));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"CircuitPlantGrad",gtrect,new LingoDecimal(0.5));
}
}
break;
case @"Feather Plants":
if ((_global.random(300) > _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2])) {
leandir = 0;
if ((q2 > 1)) {
if (((_movieScript.afamvlvledit(LingoGlobal.point((q2-1),c2),layer) == 0) & (_movieScript.afamvlvledit(LingoGlobal.point((q2-1),(c2+1)),layer) == 1))) {
leandir = (leandir+_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[(q2-1)][c2]);
}
else if ((_movieScript.afamvlvledit(LingoGlobal.point((q2-1),c2),layer) == 1)) {
leandir = (leandir-90);
}
}
if ((q2 < (_movieScript.global_gloprops.size.loch-1))) {
if (((_movieScript.afamvlvledit(LingoGlobal.point((q2+1),c2),layer) == 0) & (_movieScript.afamvlvledit(LingoGlobal.point((q2+1),(c2+1)),layer) == 1))) {
leandir = (leandir-_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[(q2+1)][c2]);
}
else if ((_movieScript.afamvlvledit(LingoGlobal.point((q2+1),c2),layer) == 1)) {
leandir = (leandir+90);
}
}
rnd = _global.random(_movieScript.restrict(((20*(_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]-(11+_global.random(21))))*new LingoDecimal(0.01)).integer,1,16));
sz = 1;
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect((-20*sz),(-90*sz),(20*sz),(100*sz)));
gtrect = (LingoGlobal.rect(((rnd-1)*40),0,(rnd*40),190)+LingoGlobal.rect(1,0,1,0));
rct = _movieScript.rotatetoquad(rct,((new LingoDecimal(65)*((leandir-(11+_global.random(21)))/new LingoDecimal(100)))+new LingoDecimal(0.1)));
checkforsolid = ((((rct[1]+rct[2])+rct[3])+rct[4])/new LingoDecimal(4));
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.getpixel(checkforsolid.loch,checkforsolid.locv) != _global.color(255,255,255))) {
if (((leandir-(11+_global.random(21))) > 0)) {
rct = _movieScript.flipquadh(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"FeatherPlantGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"FeatherPlantGrad",gtrect,new LingoDecimal(0.5));
}
}
}
break;
case @"Horse Tails":
rnd = _movieScript.restrict(_global.random((3+((20*_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2])*new LingoDecimal(0.01)).integer)),1,14);
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-10,-48,10,2));
flp = (_global.random(2)-1);
gtrect = (LingoGlobal.rect(((rnd-1)*20),0,(rnd*20),50)+LingoGlobal.rect(1,0,1,0));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"HorseTailGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-10,-48,10,2));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"HorseTailGrad",gtrect,new LingoDecimal(0.5));
}
break;
case @"Cacti":
for (int tmp_rep = 1; tmp_rep <= _global.random(_global.random(3)); tmp_rep++) {
rep = tmp_rep;
sz = (new LingoDecimal(0.5)+(_global.random((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]*new LingoDecimal(0.7)))*new LingoDecimal(0.01)));
rotat = (-45+_global.random(90));
if (((_movieScript.solidafamv(LingoGlobal.point((q2-1),(c2+1)),layer) == 0) | (_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 3))) {
rotat = ((rotat-10)-_global.random(30));
}
if (((_movieScript.solidafamv(LingoGlobal.point((q2+1),(c2+1)),layer) == 0) | (_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 2))) {
rotat = ((rotat+10)+_global.random(30));
}
tppnt = (((pnt+_movieScript.degtovec(rotat))*15)*sz);
rct = _movieScript.rotatetoquad((LingoGlobal.rect(((pnt+tppnt)*new LingoDecimal(0.5)),((pnt+tppnt)*new LingoDecimal(0.5)))+LingoGlobal.rect((-4*sz),(-7*sz),(4*sz),(8*sz))),_movieScript.lookatpoint(pnt,tppnt));
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"bigCircle").image,rct,_global.member(@"bigCircle").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
rct = ((LingoGlobal.rect(tppnt,tppnt)+LingoGlobal.rect((-9*sz),(-6*sz),(9*sz),(13*sz)))+LingoGlobal.rect(-3,-3,3,3));
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"softBrush1",_global.member(@"softBrush1").image.rect,new LingoDecimal(0.5));
}
}
break;
case @"Rubble":
rct = ((LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-3,-3,3,3))+LingoGlobal.rect(-_global.random(3),-_global.random(3),_global.random(3),_global.random(3)));
rct = _movieScript.rotatetoquad(rct,_global.random(360));
rubbl = _global.random(4);
for (int tmp_rep = 1; tmp_rep <= 4; tmp_rep++) {
rep = tmp_rep;
if ((((lr+rep)-1) > 29)) {
break;
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((lr+rep)-1)))).image.copypixels(_global.member(LingoGlobal.concat(@"rubbleGraf",_global.@string(rubbl))).image,rct,_global.member(LingoGlobal.concat(@"rubbleGraf",_global.@string(rubbl))).image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0),[new LingoSymbol("ink")] = 36});
}
}
break;
case @"Rain Moss":
pnt = (((pnt+_movieScript.degtovec(_global.random(360)))*_global.random(_global.random(100)))*new LingoDecimal(0.04));
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-12,-12,13,13));
rct = _movieScript.rotatetoquad(rct,(((_global.random(4)-1)*90)+1));
gtrect = _global.random(4);
gtrect = LingoGlobal.rect(((gtrect-1)*25),0,(gtrect*25),25);
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"rainMossGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
if ((_movieScript.global_colr != _global.color(0,255,0))) {
tppnt = ((_movieScript.depthpnt(pnt,(lr-5))+_movieScript.degtovec(_global.random(360)))*_global.random(6));
rct = ((LingoGlobal.rect(tppnt,tppnt)+LingoGlobal.rect(-20,-20,20,20))+LingoGlobal.rect(0,0,-15,-15));
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,@"softBrush1",_global.member(@"softBrush1").image.rect,new LingoDecimal(0.5));
}
break;
}
}
}
}

return null;
}
public dynamic givegroundpos(dynamic me,dynamic q,dynamic c,dynamic l) {
dynamic q2 = null;
dynamic c2 = null;
dynamic mdpnt = null;
dynamic pnt = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
pnt = (mdpnt+LingoGlobal.point((-11+_global.random(21)),10));
if ((_movieScript.global_gleprops.matrix[q2][c2][l][1] == 3)) {
pnt.locv = ((pnt.locv-(pnt.loch-mdpnt.loch))-5);
}
else if ((_movieScript.global_gleprops.matrix[q2][c2][l][1] == 2)) {
pnt.locv = ((pnt.locv-(mdpnt.loch-pnt.loch))-5);
}
return pnt;

}
public dynamic applyhugeflower(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic h = null;
dynamic tlpos = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = (_global.random(30)-1);
break;
case @"1":
d = (_global.random(10)-1);
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = (_global.random(20)-1);
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"flowerhead").image,LingoGlobal.rect((pnt.loch-3),(pnt.locv-3),(pnt.loch+3),(mdpnt.locv+3)),_global.member(@"flowerhead").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
h = pnt.locv;
while (LingoGlobal.ToBool(LingoGlobal.op_lt(h,30000))) {
h = (h+1);
pnt.loch = (pnt.loch-(2+_global.random(3)));
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((pnt.loch-1),h,(pnt.loch+2),(h+2)),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr});
tlpos = (_movieScript.givegridpos(LingoGlobal.point(pnt.loch,h))+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if ((_movieScript.solidafamv(tlpos,lr) == 1)) {
break;
}
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,d,LingoGlobal.rect((headpos.loch-37),(headpos.locv-37),(headpos.loch+37),(h+10)),@"hugeFlowerMaskMask",_global.member(@"hugeFlowerMask").image.rect,new LingoDecimal(0.8));
}

return null;
}
public dynamic applyarmgrower(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic lastdir = null;
dynamic points = null;
dynamic dir = null;
dynamic lastpnt = null;
dynamic rct = null;
dynamic qd = null;
dynamic var = null;
dynamic tlpos = null;
dynamic p = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = _global.random(29);
break;
case @"1":
d = _global.random(9);
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = _global.random(19);
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
lastdir = (180-(101+_global.random(201)));
points = new LingoList(new dynamic[] { pnt });
while (LingoGlobal.ToBool(LingoGlobal.op_lt(pnt.locv,30000))) {
dir = (180-(31+_global.random(61)));
dir = _movieScript.lerp(lastdir,dir,new LingoDecimal(0.75));
lastpnt = pnt;
pnt = ((pnt+_movieScript.degtovec(dir))*new LingoDecimal(30));
lastdir = dir;
rct = ((lastpnt+pnt)/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect(-10,-25,10,25));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt));
if ((_global.random(2) == 1)) {
qd = _movieScript.flipquadh(qd);
}
points.add(pnt);
var = _global.random(13);
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"ArmGrowerGraf").image,qd,LingoGlobal.rect(((var-1)*20),1,(var*20),(50+1)),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
tlpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if ((_movieScript.solidafamv(tlpos,lr) == 1)) {
break;
}
}
if ((points.count > 2)) {
for (int tmp_p = 1; tmp_p <= (points.count-1); tmp_p++) {
p = tmp_p;
rct = ((points[p]+points[(p+1)])/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect(-12,-36,12,36));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(points[p],points[(p+1)]));
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,d,qd,@"softBrush1",_global.member(@"softBrush1").image.rect,new LingoDecimal(0.5),LingoGlobal.power(((points.count-(LingoGlobal.floatmember_helper(p)+1))/LingoGlobal.floatmember_helper(points.count)),new LingoDecimal(1.5)));
}
}
}

return null;
}
public dynamic applythorngrower(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic lastdir = null;
dynamic blnd = null;
dynamic blnd2 = null;
dynamic wdth = null;
dynamic searchbase = null;
dynamic dir = null;
dynamic lastpnt = null;
dynamic movedir = null;
dynamic tst = null;
dynamic tstpnt = null;
dynamic rct = null;
dynamic qd = null;
dynamic var = null;
dynamic tlpos = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = _global.random(29);
break;
case @"1":
d = _global.random(9);
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = _global.random(19);
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
lastdir = (180-(61+_global.random(121)));
blnd = 1;
blnd2 = 1;
wdth = new LingoDecimal(0.5);
searchbase = 50;
while (LingoGlobal.ToBool(LingoGlobal.op_lt(pnt.locv,30000))) {
dir = (180-(61+_global.random(121)));
dir = _movieScript.lerp(lastdir,dir,new LingoDecimal(0.35));
lastpnt = pnt;
pnt = ((pnt+_movieScript.degtovec(dir))*new LingoDecimal(30));
if ((searchbase > 0)) {
movedir = LingoGlobal.point(0,0);
foreach (dynamic tmp_tst in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
tst = tmp_tst;
tstpnt = ((_movieScript.givegridpos(lastpnt)+_movieScript.global_grendercameratilepos)+tst);
if (((((tstpnt.loch > 0) & (tstpnt.loch < (_movieScript.global_gloprops.size.loch-1))) & (tstpnt.locv > 0)) & (tstpnt.locv < (_movieScript.global_gloprops.size.locv-1)))) {
movedir = ((movedir+tst)*_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[tstpnt.loch][tstpnt.locv]);
}
}
pnt = ((pnt+(movedir/new LingoDecimal(100)))*searchbase);
searchbase = (searchbase-new LingoDecimal(1.5));
pnt = (lastpnt+_movieScript.movetopoint(lastpnt,pnt,new LingoDecimal(30)));
}
lastdir = dir;
rct = ((lastpnt+pnt)/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect((-10*wdth),-25,(10*wdth),25));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt));
if ((_global.random(2) == 1)) {
qd = _movieScript.flipquadh(qd);
}
wdth = ((wdth+(_global.random(1000)/new LingoDecimal(1000)))/new LingoDecimal(5));
if ((wdth > 1)) {
wdth = 1;
}
var = _global.random(13);
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"thornBushGraf").image,qd,LingoGlobal.rect(((var-1)*20),1,(var*20),(50+1)),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,d,qd,@"thornBushGrad",LingoGlobal.rect(((var-1)*20),1,(var*20),(50+1)),new LingoDecimal(0.5),blnd);
blnd = (blnd*new LingoDecimal(0.85));
if ((blnd2 > 0)) {
rct = ((lastpnt+pnt)/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect(-12,-36,12,36));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt));
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,d,qd,@"softBrush1",_global.member(@"softBrush1").image.rect,new LingoDecimal(0.5),blnd2);
blnd2 = (blnd2-new LingoDecimal(0.15));
}
tlpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if ((_movieScript.solidafamv(tlpos,lr) == 1)) {
break;
}
}
}

return null;
}
public dynamic applygarbagespiral(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic frontwall = null;
dynamic backwall = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic dir = null;
dynamic diradd = null;
dynamic grav = null;
dynamic spiralwait = null;
dynamic spiral = null;
dynamic searchbase = null;
dynamic losespiraltime = null;
dynamic spiralfac = null;
dynamic dpthspeed = null;
dynamic conpoints = null;
dynamic points = null;
dynamic cntr = null;
dynamic lastpnt = null;
dynamic movedir = null;
dynamic dst = null;
dynamic tst = null;
dynamic tstpnt = null;
dynamic tlpos = null;
dynamic a = null;
dynamic blnd = null;
dynamic used = null;
dynamic qd = null;
dynamic b = null;
dynamic perp = null;
dynamic lastused = null;
dynamic rct = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = _global.random(29);
frontwall = 1;
backwall = 29;
if ((d <= 5)) {
backwall = 5;
}
else if ((d >= 6)) {
frontwall = 6;
}
break;
case @"1":
d = _global.random(9);
if ((d <= 5)) {
backwall = 5;
}
else if ((d >= 6)) {
frontwall = 6;
}
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = _global.random(19);
if ((d <= 5)) {
backwall = 5;
}
else if ((d >= 6)) {
frontwall = 6;
}
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
dir = _global.random(360);
diradd = (40+_global.random(20));
if ((_global.random(2) == 1)) {
diradd = -diradd;
}
grav = -new LingoDecimal(0.7);
spiralwait = (15+_global.random(15));
spiral = new LingoDecimal(1);
searchbase = -8;
losespiraltime = (60+_global.random(300));
spiralfac = _movieScript.lerp(new LingoDecimal(0.95),new LingoDecimal(0.91),((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]/new LingoDecimal(100))*(_global.random(1000)/new LingoDecimal(1000))));
dpthspeed = (_movieScript.lerp(-new LingoDecimal(1),new LingoDecimal(1),(_global.random(1000)/new LingoDecimal(1000)))/new LingoDecimal(20));
conpoints = new LingoList(new dynamic[] { new LingoList(new dynamic[] { pnt,d,0 }) });
points = new LingoList(new dynamic[] { new LingoList(new dynamic[] { pnt,d,1 }) });
cntr = 0;
while (LingoGlobal.ToBool(LingoGlobal.op_lt(pnt.locv,30000))) {
cntr = (cntr+1);
dir = (dir+diradd);
diradd = (diradd*spiralfac);
spiralfac = (spiralfac+new LingoDecimal(0.0013));
if ((spiralfac > new LingoDecimal(0.993))) {
spiralfac = new LingoDecimal(0.993);
}
lastpnt = pnt;
pnt = (((pnt+_movieScript.degtovec(dir))*new LingoDecimal(3))*LingoGlobal.power(spiral,new LingoDecimal(0.5)));
spiralwait = (spiralwait-1);
if ((spiralwait < 0)) {
movedir = LingoGlobal.point(0,0);
for (int tmp_dst = 1; tmp_dst <= 7; tmp_dst++) {
dst = tmp_dst;
foreach (dynamic tmp_tst in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
tst = tmp_tst;
tstpnt = (((_movieScript.givegridpos(lastpnt)+_movieScript.global_grendercameratilepos)+tst)*dst);
if (((((tstpnt.loch > 0) & (tstpnt.loch < (_movieScript.global_gloprops.size.loch-1))) & (tstpnt.locv > 0)) & (tstpnt.locv < (_movieScript.global_gloprops.size.locv-1)))) {
movedir = (movedir+(tst*_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[tstpnt.loch][tstpnt.locv]));
}
}
}
pnt = (((pnt+(movedir/new LingoDecimal(4600)))*searchbase)*(new LingoDecimal(1)-LingoGlobal.power(spiral,new LingoDecimal(0.5))));
searchbase = (searchbase+new LingoDecimal(0.15));
if ((searchbase > 12)) {
searchbase = 12;
}
pnt.locv = ((pnt.locv+grav)*(new LingoDecimal(1)-LingoGlobal.power(spiral,new LingoDecimal(0.5))));
grav = ((grav+new LingoDecimal(0.2))*(new LingoDecimal(1)-LingoGlobal.power(spiral,new LingoDecimal(0.5))));
spiral = (spiral-(new LingoDecimal(1)/LingoGlobal.floatmember_helper(losespiraltime)));
if ((spiral < 0)) {
spiral = 0;
d = (d+dpthspeed);
if ((d < frontwall)) {
d = frontwall;
}
else if ((d > backwall)) {
d = backwall;
}
}
}
if ((_global.random(1000) < (LingoGlobal.power(spiral,new LingoDecimal(4))*1000))) {
conpoints.add(new LingoList(new dynamic[] { pnt,d,cntr }));
}
pnt = (lastpnt+_movieScript.movetopoint(lastpnt,pnt,new LingoDecimal(3)));
points.add(new LingoList(new dynamic[] { pnt,d,spiral }));
tlpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if ((_movieScript.solidafamv(tlpos,lr) == 1)) {
break;
}
}
for (int tmp_cntr = 1; tmp_cntr <= conpoints.count; tmp_cntr++) {
cntr = tmp_cntr;
a = conpoints[_global.random(conpoints.count)][1];
blnd = (new LingoDecimal(1)-LingoGlobal.power(_movieScript.restrict((LingoGlobal.floatmember_helper(conpoints[cntr][3])/points.count),0,1),new LingoDecimal(1.3)));
used = _movieScript.restrict(conpoints[cntr][2].integer,frontwall,backwall);
if ((_global.random(10) == 1)) {
qd = LingoGlobal.rect(a.loch,a.locv,(a.loch+1),(a.locv+_global.random(_global.random(100))));
_global.member(LingoGlobal.concat(@"layer",_global.@string(points[1][2]))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,used,qd,@"pxl",LingoGlobal.rect(0,0,1,1),new LingoDecimal(0.5),blnd);
}
else {
b = conpoints[_global.random(conpoints.count)][1];
dir = _movieScript.movetopoint(a,b,new LingoDecimal(1));
perp = (_movieScript.givedirfor90degrtoline(-dir,dir)*new LingoDecimal(0.5));
qd = new LingoList(new dynamic[] { (a-perp),(a+perp),(b+perp),(b-perp) });
_global.member(LingoGlobal.concat(@"layer",_global.@string(points[1][2]))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,used,qd,@"pxl",LingoGlobal.rect(0,0,1,1),new LingoDecimal(0.5),blnd);
}
}
lastpnt = points[1][1];
lastused = points[1][2];
for (int tmp_q = 1; tmp_q <= points.count; tmp_q++) {
q = tmp_q;
pnt = points[q][1];
rct = ((lastpnt+pnt)/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect(-1,-2,1,2));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt));
used = _movieScript.restrict(points[q][2].integer,frontwall,backwall);
blnd = (new LingoDecimal(1)-LingoGlobal.power(_movieScript.restrict((LingoGlobal.floatmember_helper(q)/points.count),0,1),new LingoDecimal(1.3)));
blnd = _movieScript.lerp(blnd,new LingoDecimal(0.5),points[q][3]);
_global.member(LingoGlobal.concat(@"layer",_global.@string(used))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,used,qd,@"pxl",LingoGlobal.rect(0,0,1,1),new LingoDecimal(0.5),blnd);
if ((lastused != used)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lastused))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lastused,qd,@"pxl",LingoGlobal.rect(0,0,1,1),new LingoDecimal(0.5),blnd);
}
lastused = used;
lastpnt = pnt;
}
}

return null;
}
public dynamic applyroller(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic frontwall = null;
dynamic backwall = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic dir = null;
dynamic diradd = null;
dynamic dspeed = null;
dynamic lastused = null;
dynamic grav = null;
dynamic points = null;
dynamic seedchance = null;
dynamic lastpnt = null;
dynamic rct = null;
dynamic qd = null;
dynamic used = null;
dynamic a = null;
dynamic seedpos = null;
dynamic seedlr = null;
dynamic tlpos = null;
dynamic p = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = _global.random(29);
frontwall = 1;
backwall = 29;
if ((d <= 5)) {
backwall = 5;
}
else if ((d >= 6)) {
frontwall = 6;
}
break;
case @"1":
d = _global.random(9);
if ((d <= 5)) {
backwall = 5;
}
else if ((d >= 6)) {
frontwall = 6;
}
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = _global.random(19);
if ((d <= 5)) {
backwall = 5;
}
else if ((d >= 6)) {
frontwall = 6;
}
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
dir = _global.random(360);
diradd = ((10+_global.random(30))*new LingoDecimal(0.3));
if ((_global.random(2) == 1)) {
diradd = -diradd;
}
dspeed = ((-11+_global.random(21))/new LingoDecimal(100));
lastused = d;
grav = new LingoDecimal(0.7);
points = new LingoList(new dynamic[] { new LingoList(new dynamic[] { pnt,d }) });
seedchance = new LingoDecimal(1);
while (LingoGlobal.ToBool(LingoGlobal.op_lt(pnt.locv,30000))) {
dir = (dir-((11+_global.random(21))+diradd));
dspeed = _movieScript.restrict(((dspeed+(-11+_global.random(21)))/new LingoDecimal(1000)),-new LingoDecimal(0.1),new LingoDecimal(0.1));
d = (d+dspeed);
if ((d < frontwall)) {
d = frontwall;
dspeed = (_global.random(10)/new LingoDecimal(100));
}
else if ((d > backwall)) {
d = backwall;
dspeed = (-_global.random(10)/new LingoDecimal(100));
}
lastpnt = pnt;
pnt = ((pnt+_movieScript.degtovec(dir))*new LingoDecimal(5));
pnt.locv = (pnt.locv+grav);
grav = (grav+new LingoDecimal(0.001));
rct = ((lastpnt+pnt)/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect(-new LingoDecimal(1.5),-new LingoDecimal(3.5),new LingoDecimal(1.5),new LingoDecimal(3.5)));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt));
used = _movieScript.restrict(d.integer,frontwall,backwall);
if ((seedchance > 0)) {
for (int tmp_a = 1; tmp_a <= 8; tmp_a++) {
a = tmp_a;
if ((_global.random(1000) < (LingoGlobal.power(seedchance,new LingoDecimal(1.5))*1000))) {
seedpos = (((pnt+_movieScript.movetopoint(pnt,lastpnt,(LingoGlobal.floatmember_helper((_movieScript.diag(pnt,lastpnt)*_global.random(1000)))/new LingoDecimal(1000))))+_movieScript.degtovec(_global.random(360)))*_global.random(3));
seedlr = _movieScript.restrict((used-(2+_global.random(3))),frontwall,backwall);
_global.member(LingoGlobal.concat(@"layer",_global.@string(seedlr))).image.copypixels(_global.member(@"rustDot").image,(LingoGlobal.rect(seedpos,seedpos)+LingoGlobal.rect(-2,-2,2,2)),_global.member(@"rustDot").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr,[new LingoSymbol("ink")] = 36});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,seedlr,(LingoGlobal.rect(seedpos,seedpos)+LingoGlobal.rect(-2,-2,2,2)),@"rustDot",_global.member(@"rustDot").image.rect,new LingoDecimal(0.8),1);
if ((_global.random(3) > 1)) {
seedlr = _movieScript.restrict((seedlr-1),frontwall,backwall);
_global.member(LingoGlobal.concat(@"layer",_global.@string(seedlr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(seedpos,seedpos)+LingoGlobal.rect(-1,-1,1,1)),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,seedlr,(LingoGlobal.rect(seedpos,seedpos)+LingoGlobal.rect(-1,-1,1,1)),@"pxl",_global.member(@"pxl").image.rect,new LingoDecimal(0.8),1);
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(seedlr))).image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(seedpos,seedpos)+LingoGlobal.rect(-1,-1,1,1)),_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
}
}
}
seedchance = (seedchance-(LingoGlobal.floatmember_helper(_global.random(100))/new LingoDecimal(2200)));
points.add(new LingoList(new dynamic[] { pnt,used }));
_global.member(LingoGlobal.concat(@"layer",_global.@string(used))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr});
if ((lastused != used)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(lastused))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_colr});
}
lastused = used;
tlpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if ((_movieScript.solidafamv(tlpos,((1+LingoGlobal.op_gt(used,9))+LingoGlobal.op_gt(used,19))) == 1)) {
break;
}
}
if ((points.count > 2)) {
for (int tmp_p = 1; tmp_p <= (points.count-1); tmp_p++) {
p = tmp_p;
rct = ((points[p][1]+points[(p+1)][1])/new LingoDecimal(2));
rct = LingoGlobal.rect(rct,rct);
rct = (rct+LingoGlobal.rect(-new LingoDecimal(1.5),-new LingoDecimal(3.5),new LingoDecimal(1.5),new LingoDecimal(3.5)));
qd = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(points[p][1],points[(p+1)][1]));
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,points[p][2],qd,@"pxl",LingoGlobal.rect(0,0,1,1),new LingoDecimal(0.8),LingoGlobal.power(((points.count-(LingoGlobal.floatmember_helper(p)+1))/LingoGlobal.floatmember_helper(points.count)),new LingoDecimal(1.5)));
}
}
}

return null;
}
public dynamic applyhangroots(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic lftborder = null;
dynamic rgthborder = null;
dynamic lstpos = null;
dynamic dir = null;
dynamic crossdir = null;
dynamic qd = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = (_global.random(30)-1);
break;
case @"1":
d = (_global.random(10)-1);
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = (_global.random(20)-1);
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
lftborder = (mdpnt.loch-10);
rgthborder = (mdpnt.loch+10);
while (LingoGlobal.ToBool(LingoGlobal.op_gt(((pnt.locv+_movieScript.global_grendercameratilepos.locv)*20),-100))) {
lstpos = pnt;
pnt = ((pnt+_movieScript.degtovec((-45+_global.random(90))))*(2+_global.random(6)));
pnt.loch = _movieScript.restrict(pnt.loch,lftborder,rgthborder);
dir = _movieScript.movetopoint(pnt,lstpos,new LingoDecimal(1));
crossdir = _movieScript.givedirfor90degrtoline(-dir,dir);
qd = new LingoList(new dynamic[] { (pnt-crossdir),(pnt+crossdir),(lstpos+crossdir),(lstpos-crossdir) });
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"pxl").image,qd,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].detcol});
if ((_movieScript.solidafamv((_movieScript.givegridpos(lstpos)+_movieScript.global_grendercameratilepos),lr) == 1)) {
break;
}
}
}

return null;
}
public dynamic applythickroots(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic frontwall = null;
dynamic backwall = null;
dynamic d = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic health = null;
dynamic points = null;
dynamic dir = null;
dynamic floatdpth = null;
dynamic thickness = null;
dynamic lstpos = null;
dynamic lstgridpos = null;
dynamic gridpos = null;
dynamic tlt = null;
dynamic lastrad = null;
dynamic lastperp = null;
dynamic f = null;
dynamic perp = null;
dynamic rad = null;
dynamic qd = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
frontwall = 0;
backwall = 29;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = (_global.random(30)-1);
break;
case @"1":
d = (_global.random(10)-1);
backwall = 9;
break;
case @"2":
d = (_global.random(10)-(1+10));
frontwall = 10;
backwall = 19;
break;
case @"3":
d = (_global.random(10)-(1+20));
frontwall = 20;
break;
case @"1:st and 2:nd":
d = (_global.random(20)-1);
backwall = 19;
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
frontwall = 10;
break;
}
if ((d > 5)) {
frontwall = (5+3);
d = _movieScript.restrict(d,frontwall,29);
}
else {
backwall = 5;
}
if ((_movieScript.global_gleprops.matrix[q2][c2][((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19))][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
health = 6;
points = new LingoList(new dynamic[] { new LingoList(new dynamic[] { pnt,d,health }) });
dir = 0;
floatdpth = d;
thickness = ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]/new LingoDecimal(100))*LingoGlobal.power((_global.random(10000)/new LingoDecimal(10000)),new LingoDecimal(0.3)));
while (LingoGlobal.ToBool(LingoGlobal.op_gt(((pnt.locv+_movieScript.global_grendercameratilepos.locv)*20),-100))) {
floatdpth = (floatdpth+_movieScript.lerp(-new LingoDecimal(0.3),new LingoDecimal(0.3),(_global.random(1000)/new LingoDecimal(1000))));
if ((floatdpth < frontwall)) {
floatdpth = frontwall;
}
else if ((floatdpth > backwall)) {
floatdpth = backwall;
}
d = _movieScript.restrict(floatdpth.integer,frontwall,backwall);
lstpos = pnt;
dir = _movieScript.lerp(dir,(-45+_global.random(90)),new LingoDecimal(0.5));
pnt = ((pnt+_movieScript.degtovec(dir))*(2+_global.random(6)));
lstgridpos = (_movieScript.givegridpos(lstpos)+_movieScript.global_grendercameratilepos);
gridpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
tlt = 0;
for (int tmp_q = -1; tmp_q <= 1; tmp_q++) {
q = tmp_q;
if ((((((q != 0) & ((gridpos.loch+q) > 0)) & ((gridpos.loch+q) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx.count)) & ((gridpos.locv-1) > 0)) & ((gridpos.locv-1) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[1].count))) {
tlt = ((tlt+_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[(lstgridpos.loch+q)][(lstgridpos.locv-1)])*q);
}
}
pnt.loch = ((pnt.loch+(tlt/new LingoDecimal(100)))*new LingoDecimal(2));
gridpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
if ((lstgridpos.loch != gridpos.loch)) {
if (((((gridpos.loch > 0) & (gridpos.loch < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx.count)) & (gridpos.locv > 0)) & (gridpos.locv < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[1].count))) {
if (((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[gridpos.loch][gridpos.locv] == 0) & (_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[lstgridpos.loch][lstgridpos.locv] > 0))) {
pnt.loch = _movieScript.restrict(pnt.loch,(_movieScript.givemiddleoftile(_movieScript.givegridpos(lstpos)).loch-9),(_movieScript.givemiddleoftile(_movieScript.givegridpos(lstpos)).loch+9));
}
}
}
points.add(new LingoList(new dynamic[] { pnt,d,health }));
if ((_movieScript.solidafamv(lstgridpos,((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19))) == 1)) {
health = (health-1);
if ((health < 1)) {
break;
}
}
else {
health = _movieScript.restrict((health+1),0,6);
}
}
lstpos = (points[1][1]+LingoGlobal.point(0,1));
lastrad = 0;
lastperp = LingoGlobal.point(0,0);
for (int tmp_q = 1; tmp_q <= points.count; tmp_q++) {
q = tmp_q;
f = (LingoGlobal.floatmember_helper(q)/LingoGlobal.floatmember_helper(points.count));
pnt = points[q][1];
d = points[q][2];
dir = _movieScript.movetopoint(pnt,lstpos,new LingoDecimal(1));
perp = _movieScript.givedirfor90degrtoline(-dir,dir);
rad = (((((new LingoDecimal(0.6)+f)*new LingoDecimal(8))*(LingoGlobal.floatmember_helper(points[q][3])/new LingoDecimal(6)))*_movieScript.lerp(new LingoDecimal(0.8),new LingoDecimal(1.2),(_global.random(1000)/new LingoDecimal(1000))))*_movieScript.lerp(thickness,new LingoDecimal(0.5),new LingoDecimal(0.2)));
foreach (dynamic tmp_c in new LingoList(new dynamic[] { new LingoList(new dynamic[] { 0,new LingoDecimal(1) }),new LingoList(new dynamic[] { 1,new LingoDecimal(0.7) }),new LingoList(new dynamic[] { 2,new LingoDecimal(0.3) }) })) {
c = tmp_c;
if ((((d-c[1]) >= 0) & (((rad*c[2]) > new LingoDecimal(0.8)) | (c[1] == 0)))) {
qd = new LingoList(new dynamic[] { (pnt-((perp*rad)*c[2])),(((pnt+perp)*rad)*c[2]),((((lstpos+dir)+lastperp)*lastrad)*c[2]),((lstpos+dir)-((lastperp*lastrad)*c[2])) });
_global.member(LingoGlobal.concat(@"layer",_global.@string((d-c[1])))).image.copypixels(_global.member(@"pxl").image,qd,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0)});
}
}
lstpos = pnt;
lastperp = perp;
lastrad = rad;
}
}

return null;
}
public dynamic applyshadowplants(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic frontwall = null;
dynamic backwall = null;
dynamic d = null;
dynamic mdpnt = null;
dynamic headpos = null;
dynamic pnt = null;
dynamic health = null;
dynamic points = null;
dynamic dir = null;
dynamic cycle = null;
dynamic cntr = null;
dynamic tltfac = null;
dynamic lstpos = null;
dynamic lstgridpos = null;
dynamic gridpos = null;
dynamic tlt = null;
dynamic fuzzlength = null;
dynamic thickness = null;
dynamic lastrad = null;
dynamic lastperp = null;
dynamic f = null;
dynamic perp = null;
dynamic rad = null;
dynamic qd = null;
dynamic f2 = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
frontwall = 0;
backwall = 29;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = (_global.random(30)-1);
break;
case @"1":
d = (_global.random(10)-1);
backwall = 9;
break;
case @"2":
d = (_global.random(10)-(1+10));
frontwall = 10;
backwall = 19;
break;
case @"3":
d = (_global.random(10)-(1+20));
frontwall = 20;
break;
case @"1:st and 2:nd":
d = (_global.random(20)-1);
backwall = 19;
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
frontwall = 10;
break;
}
if ((d > 5)) {
frontwall = (5+3);
d = _movieScript.restrict(d,frontwall,29);
}
else {
backwall = 5;
}
if ((_movieScript.global_gleprops.matrix[q2][c2][((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19))][1] == 0)) {
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
headpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
pnt = LingoGlobal.point(headpos.loch,headpos.locv);
health = 6;
points = new LingoList(new dynamic[] { new LingoList(new dynamic[] { pnt,d,health }) });
dir = 180;
cycle = _movieScript.lerp(new LingoDecimal(6),new LingoDecimal(12),(_global.random(10000)/new LingoDecimal(10000)));
cntr = _global.random(50);
tltfac = new LingoDecimal(0);
while (LingoGlobal.ToBool(LingoGlobal.op_gt(((pnt.locv+_movieScript.global_grendercameratilepos.locv)*20),-100))) {
cntr = (cntr+1);
lstpos = pnt;
dir = _movieScript.lerp(dir,(180-(45+_global.random(90))),new LingoDecimal(0.1));
dir = ((dir+LingoGlobal.sin((((cntr/cycle)*LingoGlobal.PI)*new LingoDecimal(2))))*8);
cycle = (cycle+new LingoDecimal(0.1));
if ((cycle > 35)) {
cycle = 35;
}
pnt = ((pnt+_movieScript.degtovec(dir))*3);
lstgridpos = (_movieScript.givegridpos(lstpos)+_movieScript.global_grendercameratilepos);
gridpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
tlt = 0;
for (int tmp_q = -1; tmp_q <= 1; tmp_q++) {
q = tmp_q;
if ((((((q != 0) & ((lstgridpos.loch+q) > 0)) & ((lstgridpos.loch+q) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx.count)) & ((lstgridpos.locv+1) > 0)) & ((lstgridpos.locv-1) < _movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[1].count))) {
tlt = ((tlt+_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[(lstgridpos.loch+q)][(lstgridpos.locv+1)])*q);
}
}
pnt.loch = ((pnt.loch+(tlt/new LingoDecimal(100)))*_movieScript.lerp(-new LingoDecimal(2),new LingoDecimal(1),LingoGlobal.power(tltfac,new LingoDecimal(0.85))));
gridpos = (_movieScript.givegridpos(pnt)+_movieScript.global_grendercameratilepos);
tltfac = (tltfac+new LingoDecimal(0.002));
if ((tltfac > new LingoDecimal(1))) {
tltfac = new LingoDecimal(1);
}
points.add(new LingoList(new dynamic[] { pnt,d,health }));
if ((_movieScript.solidafamv(lstgridpos,((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19))) == 1)) {
health = (health-1);
if ((health < 1)) {
break;
}
}
else {
health = _movieScript.restrict((health+1),0,6);
}
}
fuzzlength = (20+_global.random(50));
thickness = ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][c2]/new LingoDecimal(100))*LingoGlobal.power((_global.random(10000)/new LingoDecimal(10000)),new LingoDecimal(0.3)));
thickness = _movieScript.lerp(thickness,(_movieScript.restrict(LingoGlobal.floatmember_helper(points.count),new LingoDecimal(20),new LingoDecimal(180))/new LingoDecimal(180)),new LingoDecimal(0.5));
lstpos = (points[1][1]+LingoGlobal.point(0,1));
lastrad = 0;
lastperp = LingoGlobal.point(0,0);
for (int tmp_q = 1; tmp_q <= points.count; tmp_q++) {
q = tmp_q;
f = (LingoGlobal.floatmember_helper(q)/LingoGlobal.floatmember_helper(points.count));
pnt = points[q][1];
d = points[q][2];
dir = _movieScript.movetopoint(pnt,lstpos,new LingoDecimal(1));
perp = _movieScript.givedirfor90degrtoline(-dir,dir);
f = LingoGlobal.sin(((f*LingoGlobal.PI)*new LingoDecimal(0.5)));
rad = ((((new LingoDecimal(1.1)+f)*new LingoDecimal(7))*(LingoGlobal.floatmember_helper(points[q][3])/new LingoDecimal(6)))*_movieScript.lerp(thickness,new LingoDecimal(0.5),new LingoDecimal(0.2)));
foreach (dynamic tmp_c in new LingoList(new dynamic[] { new LingoList(new dynamic[] { 0,new LingoDecimal(1) }),new LingoList(new dynamic[] { 1,new LingoDecimal(0.7) }),new LingoList(new dynamic[] { 2,new LingoDecimal(0.3) }) })) {
c = tmp_c;
if ((((d-c[1]) >= 0) & (((rad*c[2]) > new LingoDecimal(0.8)) | (c[1] == 0)))) {
qd = new LingoList(new dynamic[] { (pnt-((perp*rad)*c[2])),(((pnt+perp)*rad)*c[2]),((((lstpos+dir)+lastperp)*lastrad)*c[2]),((lstpos+dir)-((lastperp*lastrad)*c[2])) });
_global.member(LingoGlobal.concat(@"layer",_global.@string((d-c[1])))).image.copypixels(_global.member(@"pxl").image,qd,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,255)});
if ((_global.random(30) == 1)) {
me.sporegrower((pnt+_movieScript.movetopoint(pnt,lstpos,((_movieScript.diag(pnt,lstpos)*_global.random(10000))/new LingoDecimal(10000)))),((15+_global.random(50))*(new LingoDecimal(1)-f)),(d-c[1]),_global.color(0,0,255));
}
if ((((q < fuzzlength) & (_global.random(fuzzlength) > q)) & (_global.random(6) == 1))) {
f2 = (LingoGlobal.floatmember_helper(q)/LingoGlobal.floatmember_helper(fuzzlength));
me.sporegrower((pnt+_movieScript.movetopoint(pnt,lstpos,((_movieScript.diag(pnt,lstpos)*_global.random(10000))/new LingoDecimal(10000)))),((65+_global.random(50))*(new LingoDecimal(1)-f2)),(d-c[1]),_global.color(0,0,255));
}
}
}
lstpos = pnt;
lastperp = perp;
lastrad = rad;
}
}

return null;
}
public dynamic sporegrower(dynamic me,dynamic pos,dynamic lngth,dynamic layer,dynamic col) {
dynamic dir = null;
dynamic q = null;
dynamic othercol = null;
dir = LingoGlobal.point(0,-1);
for (int tmp_q = 1; tmp_q <= lngth; tmp_q++) {
q = tmp_q;
othercol = _global.member(LingoGlobal.concat(@"layer",layer)).image.getpixel((pos.loch-1),(pos.locv-1));
if (((othercol != col) & (othercol != _global.color(255,255,255)))) {
break;
}
else {
_global.member(LingoGlobal.concat(@"layer",layer)).image.setpixel((pos.loch-1),(pos.locv-1),col);
pos = (pos+dir);
if (((dir.locv == -1) & (_global.random(2) == 1))) {
if ((_global.random(2) == 1)) {
dir = LingoGlobal.point(-1,0);
}
else {
dir = LingoGlobal.point(1,0);
}
}
else {
dir = LingoGlobal.point(0,-1);
}
}
}

return null;
}
public dynamic applydaddycorruption(dynamic me,dynamic q,dynamic c,dynamic amount) {
dynamic q2 = null;
dynamic c2 = null;
dynamic mdpnt = null;
dynamic extraholechance = null;
dynamic a = null;
dynamic dp = null;
dynamic lr = null;
dynamic rad = null;
dynamic startpos = null;
dynamic solid = null;
dynamic dr = null;
dynamic d = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
extraholechance = 1;
for (int tmp_a = 1; tmp_a <= (amount/2); tmp_a++) {
a = tmp_a;
dp = (_global.random(28)-1);
if ((dp > 3)) {
dp = (dp+2);
}
lr = 3;
rad = ((_global.random(100)*new LingoDecimal(0.2))*_movieScript.lerp(new LingoDecimal(0.2),new LingoDecimal(1),(amount/100)));
if ((dp < 10)) {
lr = 1;
}
else if ((dp < 20)) {
lr = 2;
}
startpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
solid = 0;
if ((_movieScript.solidafamv(LingoGlobal.point(q2,c2),lr) == 1)) {
solid = 1;
}
if ((((solid == 0) & (lr < 3)) & ((dp-((lr-1)*10)) > 6))) {
if ((_movieScript.solidafamv(LingoGlobal.point(q2,c2),(lr+1)) == 1)) {
solid = 1;
}
}
if ((solid == 0)) {
foreach (dynamic tmp_dr in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(0,1),LingoGlobal.point(1,0) })) {
dr = tmp_dr;
if ((_movieScript.solidafamv((_movieScript.givegridpos(((startpos+dr)*rad))+_movieScript.global_grendercameratilepos),lr) == 1)) {
solid = 1;
break;
}
}
}
if ((((solid == 0) & (dp < 27)) & (rad > new LingoDecimal(1.2)))) {
foreach (dynamic tmp_dr in new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(0,1),LingoGlobal.point(1,0) })) {
dr = tmp_dr;
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+2)))).getpixel((((startpos.loch+dr.loch)*rad)*new LingoDecimal(0.5)),(((startpos.locv+dr.locv)*rad)*new LingoDecimal(0.5))) != -1)) {
rad = (rad/2);
solid = 1;
break;
}
}
}
if ((solid == 1)) {
for (int tmp_d = 0; tmp_d <= 2; tmp_d++) {
d = tmp_d;
if (((dp+d) < 30)) {
if ((rad <= 10)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+d)))).image.copypixels(_global.member(@"DaddyBulb").image,(LingoGlobal.rect(startpos,startpos)+LingoGlobal.rect(-rad,-rad,rad,rad)),LingoGlobal.rect(0,((1+d)*20),20,((1+(d+1))*20)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+d)))).image.copypixels(_global.member(@"DaddyBulb").image,(LingoGlobal.rect(startpos,startpos)+LingoGlobal.rect(-rad,-rad,rad,rad)),LingoGlobal.rect(20,((1+d)*40),60,((1+(d+1))*40)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
break;
}
}
if ((((_global.random(3) == 1) | (extraholechance == 1)) & (dp < 27))) {
_movieScript.global_daddycorruptionholes.add(new LingoList(new dynamic[] { startpos,((rad*(50+_global.random(50)))*new LingoDecimal(0.01)),_global.random(360),dp,amount }));
extraholechance = 0;
}
}
}

return null;
}
public dynamic applywire(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic lr = null;
dynamic mdpnt = null;
dynamic startpos = null;
dynamic mycamera = null;
dynamic fatness = null;
dynamic a = null;
dynamic keepitfromtoforty = null;
dynamic goodstops = null;
dynamic dir = null;
dynamic pnt = null;
dynamic lastpnt = null;
dynamic rep = null;
dynamic dr = null;
dynamic tlpos = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = (_global.random(30)-1);
break;
case @"1":
d = (_global.random(10)-1);
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = (_global.random(20)-1);
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
_global.member(@"wireImage").image = _global.image(_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.width,_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.height,1);
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
startpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
mycamera = me.closestcamera(((startpos+_movieScript.global_grendercameratilepos)*20));
if ((mycamera == 0)) {
return null;
}
fatness = 1;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[3][3]) {
case @"2px":
fatness = 2;
break;
case @"3px":
fatness = 3;
break;
case @"random":
fatness = _global.random(3);
break;
}
a = ((new LingoDecimal(1)+_global.random(100))+_global.random(_global.random(_global.random(900))));
keepitfromtoforty = _global.random(30);
a = (((a*keepitfromtoforty)+new LingoDecimal(40))/(keepitfromtoforty+new LingoDecimal(1)));
_global.member(@"wireImage").image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(startpos.loch,(startpos.locv-1),(startpos.loch+1),(startpos.locv+1))+LingoGlobal.rect(-LingoGlobal.op_gt(fatness,1),-LingoGlobal.op_gt(fatness,1),LingoGlobal.op_eq(fatness,3),LingoGlobal.op_eq(fatness,3))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0)});
goodstops = 0;
for (int tmp_dir = 0; tmp_dir <= 1; tmp_dir++) {
dir = tmp_dir;
pnt = LingoGlobal.point(startpos.loch,startpos.locv);
lastpnt = LingoGlobal.point(startpos.loch,startpos.locv);
for (int tmp_rep = 1; tmp_rep <= 1000; tmp_rep++) {
rep = tmp_rep;
pnt.loch = ((startpos.loch+((-1+2)*dir))*rep);
pnt.locv = ((startpos.locv+a)-((LingoGlobal.power(new LingoDecimal(2.71828183),(rep/a))+LingoGlobal.power(new LingoDecimal(2.71828183),(-rep/a)))*(a/new LingoDecimal(2))));
dr = _movieScript.movetopoint(lastpnt,pnt,LingoGlobal.floatmember_helper(fatness));
_global.member(@"wireImage").image.copypixels(_global.member(@"pxl").image,(LingoGlobal.rect(pnt.loch,pnt.locv,(pnt.loch+1),(lastpnt.locv+1))+LingoGlobal.rect(-LingoGlobal.op_gt(fatness,1),-LingoGlobal.op_gt(fatness,1),LingoGlobal.op_eq(fatness,3),LingoGlobal.op_eq(fatness,3))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0)});
lastpnt = LingoGlobal.point(pnt.loch,pnt.locv);
tlpos = (_movieScript.givegridpos(LingoGlobal.point(pnt.loch,pnt.locv))+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if (((mycamera == _movieScript.global_gcurrentrendercamera) & (me.seenbycamera(mycamera,(pnt+_movieScript.global_grendercameratilepos)) == 1))) {
if ((_movieScript.global_gleprops.matrix[tlpos.loch][tlpos.locv][lr][1] == 1)) {
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.getpixel(pnt) != _global.color(255,255,255))) {
goodstops = (goodstops+1);
break;
}
}
}
else if (LingoGlobal.ToBool(_movieScript.solidafamv(tlpos,lr))) {
goodstops = (goodstops+1);
break;
}
}
}
if ((goodstops == 2)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"wireImage").image,_global.member(@"wireImage").image.rect,_global.member(@"wireImage").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].detcol,[new LingoSymbol("ink")] = 36});
}
}

return null;
}
public dynamic applychain(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic d = null;
dynamic lr = null;
dynamic big = null;
dynamic mdpnt = null;
dynamic startpos = null;
dynamic mycamera = null;
dynamic a = null;
dynamic keepitfromtoforty = null;
dynamic origornt = null;
dynamic goodstops = null;
dynamic dir = null;
dynamic pnt = null;
dynamic lastpnt = null;
dynamic ornt = null;
dynamic rep = null;
dynamic checkterrain = null;
dynamic pos = null;
dynamic rct = null;
dynamic gtrect = null;
dynamic tlpos = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
d = (_global.random(30)-1);
break;
case @"1":
d = (_global.random(10)-1);
break;
case @"2":
d = (_global.random(10)-(1+10));
break;
case @"3":
d = (_global.random(10)-(1+20));
break;
case @"1:st and 2:nd":
d = (_global.random(20)-1);
break;
case @"2:nd and 3:rd":
d = (_global.random(20)-(1+10));
break;
}
lr = ((1+LingoGlobal.op_gt(d,9))+LingoGlobal.op_gt(d,19));
big = 0;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[3][3]) {
case @"FAT":
big = 1;
break;
}
if ((_movieScript.global_gleprops.matrix[q2][c2][lr][1] == 0)) {
_global.member(@"wireImage").image = _global.image(_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.width,_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.height,1);
mdpnt = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
startpos = (mdpnt+LingoGlobal.point((-11+_global.random(21)),(-11+_global.random(21))));
mycamera = me.closestcamera(((startpos+_movieScript.global_grendercameratilepos)*20));
if ((mycamera == 0)) {
return null;
}
a = ((new LingoDecimal(1)+_global.random(100))+_global.random(_global.random(_global.random(900))));
keepitfromtoforty = _global.random(30);
a = (((a*keepitfromtoforty)+new LingoDecimal(40))/(keepitfromtoforty+new LingoDecimal(1)));
if (LingoGlobal.ToBool(big)) {
a = (a+10);
}
origornt = (_global.random(2)-1);
goodstops = 0;
for (int tmp_dir = 0; tmp_dir <= 1; tmp_dir++) {
dir = tmp_dir;
pnt = LingoGlobal.point(startpos.loch,startpos.locv);
lastpnt = LingoGlobal.point(startpos.loch,startpos.locv);
if ((dir == 0)) {
ornt = origornt;
}
else {
ornt = (1-origornt);
}
for (int tmp_rep = 1; tmp_rep <= 4000; tmp_rep++) {
rep = tmp_rep;
checkterrain = 0;
pnt.loch = (((startpos.loch+((-1+2)*dir))*rep)*new LingoDecimal(0.25));
pnt.locv = ((startpos.locv+a)-((LingoGlobal.power(new LingoDecimal(2.71828183),((rep*new LingoDecimal(0.25))/a))+LingoGlobal.power(new LingoDecimal(2.71828183),(-(rep*new LingoDecimal(0.25))/a)))*(a/new LingoDecimal(2))));
if ((big == 0)) {
if ((_movieScript.diag(pnt,lastpnt) >= 7)) {
if (LingoGlobal.ToBool(ornt)) {
pos = ((pnt+lastpnt)*new LingoDecimal(0.5));
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-3,-5,3,5));
gtrect = LingoGlobal.rect(0,0,6,10);
ornt = 0;
}
else {
pos = ((pnt+lastpnt)*new LingoDecimal(0.5));
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-1,-5,1,5));
gtrect = LingoGlobal.rect(7,0,8,10);
ornt = 1;
}
_global.member(@"wireImage").image.copypixels(_global.member(@"chainSegment").image,_movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt)),gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0),[new LingoSymbol("ink")] = 36});
lastpnt = LingoGlobal.point(pnt.loch,pnt.locv);
checkterrain = 1;
}
}
else if ((_movieScript.diag(pnt,lastpnt) >= 12)) {
if (LingoGlobal.ToBool(ornt)) {
pos = ((pnt+lastpnt)*new LingoDecimal(0.5));
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-6,-10,6,10));
gtrect = LingoGlobal.rect(0,0,12,20);
ornt = 0;
}
else {
pos = ((pnt+lastpnt)*new LingoDecimal(0.5));
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-2,-10,2,10));
gtrect = LingoGlobal.rect(13,0,16,20);
ornt = 1;
}
_global.member(@"wireImage").image.copypixels(_global.member(@"bigChainSegment").image,_movieScript.rotatetoquad(rct,_movieScript.lookatpoint(lastpnt,pnt)),gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0),[new LingoSymbol("ink")] = 36});
lastpnt = LingoGlobal.point(pnt.loch,pnt.locv);
checkterrain = 1;
}
if (LingoGlobal.ToBool(checkterrain)) {
tlpos = (_movieScript.givegridpos(LingoGlobal.point(pnt.loch,pnt.locv))+_movieScript.global_grendercameratilepos);
if ((tlpos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))) == 0)) {
break;
}
else if (((mycamera == _movieScript.global_gcurrentrendercamera) & (me.seenbycamera(mycamera,(pnt+_movieScript.global_grendercameratilepos)) == 1))) {
if ((_movieScript.global_gleprops.matrix[tlpos.loch][tlpos.locv][lr][1] == 1)) {
if ((_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.getpixel(pnt) != _global.color(255,255,255))) {
goodstops = (goodstops+1);
break;
}
}
}
else if (LingoGlobal.ToBool(_movieScript.solidafamv(tlpos,lr))) {
goodstops = (goodstops+1);
break;
}
}
}
}
if ((goodstops == 2)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(d))).image.copypixels(_global.member(@"wireImage").image,_global.member(@"wireImage").image.rect,_global.member(@"wireImage").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].detcol,[new LingoSymbol("ink")] = 36});
}
}

return null;
}
public dynamic applyfungiflower(dynamic me,dynamic q,dynamic c) {
dynamic q2 = null;
dynamic c2 = null;
dynamic lr = null;
dynamic layer = null;
dynamic rnd = null;
dynamic flp = null;
dynamic closestedge = null;
dynamic a = null;
dynamic pnt = null;
dynamic rct = null;
dynamic gtrect = null;
dynamic l = null;
dynamic l2 = null;
dynamic val = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
lr = 1;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"1":
layer = 1;
break;
case @"2":
layer = 2;
break;
case @"3":
layer = 3;
break;
}
lr = ((((layer-1)*10)+_global.random(9))-1);
if ((_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 0)) {
rnd = 0;
if ((_movieScript.afamvlvledit(LingoGlobal.point(q2,(c2+1)),layer) == 1)) {
rnd = _movieScript.global_geffectprops.list[_movieScript.global_geffectprops.listpos];
flp = (_global.random(2)-1);
closestedge = 1000;
for (int tmp_a = -5; tmp_a <= 5; tmp_a++) {
a = tmp_a;
if ((_movieScript.afamvlvledit(LingoGlobal.point((q2+a),(c2+1)),layer) != 1)) {
if ((LingoGlobal.abs(a) <= LingoGlobal.abs(closestedge))) {
flp = LingoGlobal.op_gt(a,0);
closestedge = a;
if ((a == 0)) {
flp = (_global.random(2)-1);
}
}
}
}
pnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point((-10+_global.random(20)),10));
}
else if ((_movieScript.afamvlvledit(LingoGlobal.point((q2+1),c2),layer) == 1)) {
rnd = 1;
flp = 0;
pnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(10,-_global.random(10)));
}
else if ((_movieScript.afamvlvledit(LingoGlobal.point((q2-1),c2),layer) == 1)) {
rnd = 1;
flp = 1;
pnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q2,c2))+LingoGlobal.point(-10,-_global.random(10)));
}
if ((rnd != 0)) {
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-80,-80,80,80));
gtrect = (LingoGlobal.rect(((rnd-1)*160),0,(rnd*160),160)+LingoGlobal.rect(1,0,1,0));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"fungiFlowersGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
_movieScript.global_geffectprops.listpos = (_movieScript.global_geffectprops.listpos+1);
if ((_movieScript.global_geffectprops.listpos > _movieScript.global_geffectprops.list.count)) {
l = new LingoList(new dynamic[] { 2,3,4,5 });
l2 = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
val = l[_global.random(l.count)];
l2.add(val);
l.deleteone(val);
}
_movieScript.global_geffectprops = new LingoPropertyList {[new LingoSymbol("list")] = l2,[new LingoSymbol("listpos")] = 1};
}

return null;
}
public dynamic applylhflower(dynamic me,dynamic q,dynamic c) {
dynamic q2 = null;
dynamic c2 = null;
dynamic lr = null;
dynamic layer = null;
dynamic rnd = null;
dynamic flp = null;
dynamic pnt = null;
dynamic rct = null;
dynamic gtrect = null;
dynamic l = null;
dynamic l2 = null;
dynamic a = null;
dynamic val = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
lr = 1;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"1":
layer = 1;
break;
case @"2":
layer = 2;
break;
case @"3":
layer = 3;
break;
}
lr = ((((layer-1)*10)+_global.random(9))-1);
if ((_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 0)) {
rnd = _movieScript.global_geffectprops.list[_movieScript.global_geffectprops.listpos];
flp = (_global.random(2)-1);
pnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point((-10+_global.random(20)),10));
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-40,-160,40,20));
gtrect = (LingoGlobal.rect(((rnd-1)*80),0,(rnd*80),180)+LingoGlobal.rect(1,0,1,0));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(@"lightHouseFlowersGraf").image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
_movieScript.global_geffectprops.listpos = (_movieScript.global_geffectprops.listpos+1);
if ((_movieScript.global_geffectprops.listpos > _movieScript.global_geffectprops.list.count)) {
l = new LingoList(new dynamic[] { 1,2,3,4,5,6,7,8 });
l2 = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= 8; tmp_a++) {
a = tmp_a;
val = l[_global.random(l.count)];
l2.add(val);
l.deleteone(val);
}
_movieScript.global_geffectprops = new LingoPropertyList {[new LingoSymbol("list")] = l2,[new LingoSymbol("listpos")] = 1};
}

return null;
}
public dynamic applybigplant(dynamic me,dynamic q,dynamic c) {
dynamic q2 = null;
dynamic c2 = null;
dynamic lr = null;
dynamic layer = null;
dynamic mem = null;
dynamic rnd = null;
dynamic flp = null;
dynamic pnt = null;
dynamic rct = null;
dynamic gtrect = null;
dynamic l = null;
dynamic l2 = null;
dynamic a = null;
dynamic val = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
lr = 1;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"1":
layer = 1;
break;
case @"2":
layer = 2;
break;
case @"3":
layer = 3;
break;
}
mem = @"fern";
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].nm) {
case @"Fern":
break;
case @"Giant Mushroom":
mem = @"giantMushroom";
break;
}
lr = ((((layer-1)*10)+_global.random(9))-1);
if ((_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 0)) {
rnd = _movieScript.global_geffectprops.list[_movieScript.global_geffectprops.listpos];
flp = (_global.random(2)-1);
pnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point((-10+_global.random(20)),10));
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-50,-80,50,20));
gtrect = (LingoGlobal.rect(((rnd-1)*100),0,(rnd*100),100)+LingoGlobal.rect(1,1,1,1));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(lr))).image.copypixels(_global.member(LingoGlobal.concat(mem,@"Graf")).image,rct,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _movieScript.global_colr});
pnt = _movieScript.depthpnt(pnt,(lr-5));
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-50,-80,50,20));
if (LingoGlobal.ToBool(flp)) {
rct = _movieScript.vertfliprect(rct);
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,lr,rct,LingoGlobal.concat(mem,@"Grad"),(LingoGlobal.rect(((rnd-1)*100),0,(rnd*100),100)+LingoGlobal.rect(1,1,1,1)),new LingoDecimal(0.5));
}
_movieScript.global_geffectprops.listpos = (_movieScript.global_geffectprops.listpos+1);
if ((_movieScript.global_geffectprops.listpos > _movieScript.global_geffectprops.list.count)) {
l = new LingoList(new dynamic[] { 1,2,3,4,5,6,7,8 });
l2 = new LingoPropertyList {};
for (int tmp_a = 1; tmp_a <= 8; tmp_a++) {
a = tmp_a;
val = l[_global.random(l.count)];
l2.add(val);
l.deleteone(val);
}
_movieScript.global_geffectprops = new LingoPropertyList {[new LingoSymbol("list")] = l2,[new LingoSymbol("listpos")] = 1};
}

return null;
}
public dynamic apply3dsprawler(dynamic me,dynamic q,dynamic c,dynamic effc) {
dynamic q2 = null;
dynamic c2 = null;
dynamic big = null;
dynamic lr = null;
dynamic layer = null;
dynamic lrrange = null;
dynamic sts = null;
dynamic pnt = null;
dynamic expectedlife = null;
dynamic branches = null;
dynamic pos = null;
dynamic lstpos = null;
dynamic generaldir = null;
dynamic lstaimpnt = null;
dynamic brlr = null;
dynamic brlrdir = null;
dynamic avoidwalls = null;
dynamic tiredness = null;
dynamic branch = null;
dynamic basethickness = null;
dynamic startlifetime = null;
dynamic step = null;
dynamic aimpnt = null;
dynamic dir = null;
dynamic lstlayer = null;
dynamic smllst = null;
dynamic wall = null;
dynamic bggst = null;
dynamic pstcolor = null;
dynamic fc = null;
dynamic lngth = null;
dynamic cntr = null;
dynamic rct = null;
dynamic ftness = null;
dynamic thickness = null;
dynamic rnd = null;
dynamic blnd = null;
q2 = (q+_movieScript.global_grendercameratilepos.loch);
c2 = (c+_movieScript.global_grendercameratilepos.locv);
big = 0;
if ((c > 1)) {
big = LingoGlobal.op_gt(_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[q2][(c2-1)],0);
}
lr = 1;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"1":
layer = 1;
break;
case @"2":
layer = 2;
lrrange = new LingoList(new dynamic[] { 6,29 });
break;
case @"3":
layer = 3;
lrrange = new LingoList(new dynamic[] { 6,29 });
break;
}
lr = ((((layer-1)*10)+_global.random(9))-1);
if ((layer == 1)) {
if ((lr < 5)) {
lrrange = new LingoList(new dynamic[] { 0,5 });
}
else {
lrrange = new LingoList(new dynamic[] { 6,29 });
}
}
switch (effc) {
case @"sprawlBush":
sts = new LingoPropertyList {[new LingoSymbol("branches")] = (((10+_global.random(10))+15)*big),[new LingoSymbol("expectedbranchlife")] = new LingoPropertyList {[new LingoSymbol("small")] = 20,[new LingoSymbol("big")] = 35,[new LingoSymbol("smallrandom")] = 30,[new LingoSymbol("bigrandom")] = 70},[new LingoSymbol("starttired")] = 0,[new LingoSymbol("avoidwalls")] = new LingoDecimal(1),[new LingoSymbol("generaldir")] = new LingoDecimal(0.6),[new LingoSymbol("randomdir")] = new LingoDecimal(1.2),[new LingoSymbol("step")] = new LingoDecimal(6)};
break;
case @"featherFern":
sts = new LingoPropertyList {[new LingoSymbol("branches")] = (((3+_global.random(3))+3)*big),[new LingoSymbol("expectedbranchlife")] = new LingoPropertyList {[new LingoSymbol("small")] = 130,[new LingoSymbol("big")] = 200,[new LingoSymbol("smallrandom")] = 50,[new LingoSymbol("bigrandom")] = 100},[new LingoSymbol("starttired")] = (-77-(77*big)),[new LingoSymbol("avoidwalls")] = new LingoDecimal(0.6),[new LingoSymbol("generaldir")] = new LingoDecimal(1.2),[new LingoSymbol("randomdir")] = new LingoDecimal(0.6),[new LingoSymbol("step")] = new LingoDecimal(2),[new LingoSymbol("feathercounter")] = 0,[new LingoSymbol("airroots")] = 0};
break;
case @"Fungus Tree":
sts = new LingoPropertyList {[new LingoSymbol("branches")] = (((10+_global.random(10))+15)*big),[new LingoSymbol("expectedbranchlife")] = new LingoPropertyList {[new LingoSymbol("small")] = 30,[new LingoSymbol("big")] = 60,[new LingoSymbol("smallrandom")] = 15,[new LingoSymbol("bigrandom")] = 30},[new LingoSymbol("starttired")] = 0,[new LingoSymbol("avoidwalls")] = new LingoDecimal(0.8),[new LingoSymbol("generaldir")] = new LingoDecimal(0.8),[new LingoSymbol("randomdir")] = new LingoDecimal(1),[new LingoSymbol("step")] = new LingoDecimal(3),[new LingoSymbol("thickness")] = ((6+_global.random(3))*((1+big)*new LingoDecimal(0.4))),[new LingoSymbol("branchpoints")] = new LingoPropertyList {}};
break;
}
if (((_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 0) & (_movieScript.afamvlvledit(LingoGlobal.point(q2,(c2+1)),layer) == 1))) {
pnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point((-10+_global.random(20)),10));
switch (effc) {
case @"Fungus Tree":
if (LingoGlobal.ToBool(big)) {
expectedlife = (sts.expectedbranchlife.big+_global.random(sts.expectedbranchlife.bigrandom));
}
else {
expectedlife = (sts.expectedbranchlife.small+_global.random(sts.expectedbranchlife.smallrandom));
}
sts.branchpoints = new LingoList(new dynamic[] { new LingoPropertyList {[new LingoSymbol("pos")] = pnt,[new LingoSymbol("dir")] = LingoGlobal.point(0,-1),[new LingoSymbol("thickness")] = sts.thickness,[new LingoSymbol("layer")] = lr,[new LingoSymbol("lifeleft")] = expectedlife,[new LingoSymbol("tired")] = sts.starttired} });
break;
}
for (int tmp_branches = 1; tmp_branches <= sts.branches; tmp_branches++) {
branches = tmp_branches;
pos = LingoGlobal.point(pnt.loch,pnt.locv);
lstpos = LingoGlobal.point(pnt.loch,pnt.locv);
generaldir = _movieScript.degtovec((-60+_global.random(120)));
lstaimpnt = generaldir;
brlr = lr;
brlrdir = (101+_global.random(201));
avoidwalls = new LingoDecimal(2);
tiredness = sts.starttired;
if (LingoGlobal.ToBool(big)) {
expectedlife = (sts.expectedbranchlife.big+_global.random(sts.expectedbranchlife.bigrandom));
}
else {
expectedlife = (sts.expectedbranchlife.small+_global.random(sts.expectedbranchlife.smallrandom));
}
switch (effc) {
case @"featherFern":
sts.airroots = ((25+15)*big);
break;
case @"Fungus Tree":
branch = sts.branchpoints[_global.random(sts.branchpoints.count)];
sts.branchpoints.deleteone(branch);
basethickness = branch.thickness;
pos = branch.pos;
lstpos = branch.pos;
brlr = branch.layer;
generaldir = branch.dir;
lstaimpnt = branch.dir;
tiredness = branch.tired;
expectedlife = _movieScript.restrict((branch.lifeleft-(11+_global.random(21))),5,200);
startlifetime = expectedlife;
break;
}
for (int tmp_step = 1; tmp_step <= expectedlife; tmp_step++) {
step = tmp_step;
lstpos = pos;
switch (effc) {
case @"featherFern":
tiredness = (((tiredness+new LingoDecimal(0.5))+LingoGlobal.abs((tiredness*new LingoDecimal(0.05))))-(new LingoDecimal(0.3)*big));
break;
case @"Fungus Tree":
tiredness = (-90*(new LingoDecimal(1)-((startlifetime-step)/LingoGlobal.floatmember_helper(startlifetime))));
break;
}
aimpnt = ((((generaldir*sts.generaldir)+_movieScript.degtovec(_global.random(360)))*sts.randomdir)+LingoGlobal.point(0,(tiredness*new LingoDecimal(0.01))));
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(-1,-1),LingoGlobal.point(0,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
dir = tmp_dir;
if ((_movieScript.afamvlvledit(((_movieScript.givegridpos(lstpos)+dir)+_movieScript.global_grendercameratilepos),(((brlr/new LingoDecimal(10))-new LingoDecimal(0.4999)).integer+1)) == 1)) {
aimpnt = (aimpnt-(dir*avoidwalls));
avoidwalls = _movieScript.restrict((avoidwalls-new LingoDecimal(0.06)),new LingoDecimal(0.2),2);
step = (step+LingoGlobal.op_ne(effc,@"Fungus Tree"));
}
else {
aimpnt = ((aimpnt+dir)*new LingoDecimal(0.1));
}
}
avoidwalls = _movieScript.restrict((avoidwalls+new LingoDecimal(0.03)),new LingoDecimal(0.2),2);
lstlayer = brlr;
brlr = ((brlr+brlrdir)*new LingoDecimal(0.01));
smllst = lrrange[1];
if (((((lstlayer/new LingoDecimal(10))-new LingoDecimal(0.4999)).integer+1) > 1)) {
if ((_movieScript.afamvlvledit((_movieScript.givegridpos(pos)+_movieScript.global_grendercameratilepos),((((lstlayer/new LingoDecimal(10))-new LingoDecimal(0.4999)).integer+1)-1)) == 1)) {
wall = (((lstlayer/new LingoDecimal(10))-new LingoDecimal(0.4999)).integer*10);
if ((wall > 0)) {
wall = (wall-1);
}
smllst = _movieScript.restrict(smllst,wall,0);
}
}
bggst = lrrange[2];
if (((((lstlayer/new LingoDecimal(10))-new LingoDecimal(0.4999)).integer+1) < 3)) {
if ((_movieScript.afamvlvledit((_movieScript.givegridpos(pos)+_movieScript.global_grendercameratilepos),((((lstlayer/new LingoDecimal(10))-new LingoDecimal(0.4999)).integer+1)+1)) == 1)) {
wall = ((((_movieScript.restrict(lstlayer,1,29)/new LingoDecimal(10))+new LingoDecimal(0.4999)).integer*10)-1);
bggst = _movieScript.restrict(bggst,0,wall);
}
}
if ((brlr < smllst)) {
brlr = smllst;
brlrdir = _global.random(41);
}
if ((brlr > bggst)) {
brlr = bggst;
brlrdir = -_global.random(41);
}
aimpnt = (((aimpnt+lstaimpnt)+lstaimpnt)/new LingoDecimal(3));
lstaimpnt = aimpnt;
pos = (pos+_movieScript.movetopoint(LingoGlobal.point(0,0),aimpnt,sts.step));
pstcolor = 0;
switch (effc) {
case @"featherFern":
if ((sts.airroots > 0)) {
sts.feathercounter = 20;
sts.airroots = (sts.airroots-1);
}
sts.feathercounter = ((((sts.feathercounter+_movieScript.diag(pos,lstpos))*new LingoDecimal(0.5))+LingoGlobal.abs((pos.loch-lstpos.loch)))+LingoGlobal.abs((lstlayer-brlr)));
if ((sts.feathercounter > ((8+((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife)))*12))) {
sts.feathercounter = (sts.feathercounter-((8+((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife)))*12));
fc = ((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife));
fc = (new LingoDecimal(1)-fc);
fc = (fc*fc);
fc = (new LingoDecimal(1)-fc);
lngth = ((LingoGlobal.sin((fc*LingoGlobal.PI))*(LingoGlobal.abs((pos.locv-pnt.locv))+120))/new LingoDecimal(3));
for (int tmp_cntr = 1; tmp_cntr <= sts.airroots; tmp_cntr++) {
cntr = tmp_cntr;
lngth = (((lngth*new LingoDecimal(6))+(LingoGlobal.abs((pos.locv-pnt.locv))+4))/new LingoDecimal(7));
}
foreach (dynamic tmp_rct in new LingoList(new dynamic[] { (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(0,0,1,lngth)),(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(1,0,2,(lngth-_global.random(_global.random(_global.random((lngth.integer+1))))))) })) {
rct = tmp_rct;
_global.member(LingoGlobal.concat(@"layer",_global.@string(brlr.integer))).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _movieScript.global_colr});
}
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,brlr,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-6,0,6,(lngth+2))),@"featherFernGradient",_global.member(@"featherFernGradient").rect,new LingoDecimal(0.5));
pstcolor = 1;
}
fc = ((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife));
fc = (fc*fc);
ftness = (LingoGlobal.sin((fc*LingoGlobal.PI))*((4+1)*big));
rct = ((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-1,-3,1,3))+LingoGlobal.rect(-ftness,-ftness,ftness,ftness));
rct = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(pos,lstpos));
brlrdir = (brlrdir-(4+_global.random(7)));
break;
case @"sprawlBush":
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-2,-5,2,5));
rct = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(pos,lstpos));
brlrdir = (brlrdir-(11+_global.random(21)));
pstcolor = 1;
break;
case @"Fungus Tree":
thickness = (((startlifetime-step)/LingoGlobal.floatmember_helper(startlifetime))*basethickness);
sts.branchpoints.add(new LingoPropertyList {[new LingoSymbol("pos")] = pos,[new LingoSymbol("dir")] = _movieScript.movetopoint(LingoGlobal.point(0,0),aimpnt,new LingoDecimal(1)),[new LingoSymbol("thickness")] = thickness,[new LingoSymbol("layer")] = brlr,[new LingoSymbol("lifeleft")] = (startlifetime-step),[new LingoSymbol("tired")] = tiredness});
if ((step == expectedlife)) {
rnd = _global.random(5);
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-5,-19,5,1));
if ((_global.random(2) == 1)) {
rct = _movieScript.vertfliprect(rct);
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(brlr.integer))).image.copypixels(_global.member(@"fungusTreeTops").image,rct,LingoGlobal.rect(((rnd-1)*10),1,(rnd*10),21),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _movieScript.global_colr});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,brlr,(LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-7,-11,7,3)),@"softBrush1",_global.member(@"softBrush1").rect,new LingoDecimal(0.5));
}
rct = ((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-1,-3,1,3))+LingoGlobal.rect(-thickness,-thickness,thickness,thickness));
rct = _movieScript.rotatetoquad(rct,_movieScript.lookatpoint(pos,lstpos));
brlrdir = (brlrdir-(11+_global.random(21)));
pstcolor = 1;
break;
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(brlr.integer))).image.copypixels(_global.member(@"blob").image,rct,_global.member(@"blob").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _movieScript.global_colr});
_global.member(LingoGlobal.concat(@"layer",_global.@string(lstlayer.integer))).image.copypixels(_global.member(@"blob").image,rct,_global.member(@"blob").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _movieScript.global_colr});
if (LingoGlobal.ToBool(pstcolor)) {
blnd = (((new LingoDecimal(1)-((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife)))*25)+_global.random(((new LingoDecimal(1)-((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife)))*75)));
if ((effc == @"Fungus Tree")) {
blnd = ((new LingoDecimal(1)-((expectedlife-step)/LingoGlobal.floatmember_helper(expectedlife)))*100);
}
_global.member(@"softbrush2").image.copypixels(_global.member(@"pxl").image,_global.member(@"softbrush2").image.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"softbrush2").image.copypixels(_global.member(@"softbrush1").image,_global.member(@"softbrush2").image.rect,_global.member(@"softbrush1").image.rect,new LingoPropertyList {[new LingoSymbol("blend")] = blnd});
_movieScript.copypixelstoeffectcolor(_movieScript.global_gdlayer,brlr,_movieScript.rotatetoquad((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-17,-25,17,25)),_movieScript.lookatpoint(pos,lstpos)),@"softBrush2",_global.member(@"softBrush1").rect,new LingoDecimal(0.5));
}
}
}
}

return null;
}
public dynamic applyblackgoo(dynamic me,dynamic q,dynamic c,dynamic eftc) {
dynamic spnt = null;
dynamic rct = null;
dynamic d = null;
dynamic e = null;
dynamic ps = null;
spnt = (_movieScript.givemiddleoftile(LingoGlobal.point(q,c))+LingoGlobal.point(-10,-10));
rct = _global.member(@"blob").image.rect;
for (int tmp_d = 1; tmp_d <= 10; tmp_d++) {
d = tmp_d;
for (int tmp_e = 1; tmp_e <= 10; tmp_e++) {
e = tmp_e;
ps = LingoGlobal.point(((spnt.loch+d)*2),((spnt.locv+e)*2));
if ((_global.member(@"layer0").image.getpixel(ps) == _global.color(255,255,255))) {
_global.member(@"blackOutImg1").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-6)-_global.random(_global.random(11))),((ps.locv-6)-_global.random(_global.random(11))),((ps.loch+6)+_global.random(_global.random(11))),((ps.locv+6)+_global.random(_global.random(11)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
_global.member(@"blackOutImg2").image.copypixels(_global.member(@"blob").image,LingoGlobal.rect(((ps.loch-7)-_global.random(_global.random(14))),((ps.locv-7)-_global.random(_global.random(14))),((ps.loch+7)+_global.random(_global.random(14))),((ps.locv+7)+_global.random(_global.random(14)))),rct,new LingoPropertyList {[new LingoSymbol("color")] = 0,[new LingoSymbol("ink")] = 36});
}
}
}

return null;
}
public dynamic applyrestoreeffect(dynamic me,dynamic q,dynamic c,dynamic q2,dynamic c2,dynamic eftc) {
dynamic layers = null;
dynamic layer = null;
dynamic mdpoint = null;
dynamic tlrct = null;
dynamic a = null;
dynamic b = null;
dynamic u = null;
dynamic lr = null;
switch (_movieScript.global_geeprops.effects[_movieScript.global_r].options[2][3]) {
case @"All":
layers = new LingoList(new dynamic[] { 1,2,3 });
break;
case @"1":
layers = new LingoList(new dynamic[] { 1 });
break;
case @"2":
layers = new LingoList(new dynamic[] { 2 });
break;
case @"3":
layers = new LingoList(new dynamic[] { 3 });
break;
case @"1:st and 2:nd":
layers = new LingoList(new dynamic[] { 1,2 });
break;
case @"2:nd and 3:rd":
layers = new LingoList(new dynamic[] { 2,3 });
break;
}
foreach (dynamic tmp_layer in layers) {
layer = tmp_layer;
if ((_movieScript.afamvlvledit(LingoGlobal.point(q2,c2),layer) == 1)) {
mdpoint = _movieScript.givemiddleoftile(LingoGlobal.point(q,c));
tlrct = LingoGlobal.rect((mdpoint+LingoGlobal.point(-10,-10)),(mdpoint+LingoGlobal.point(10,10)));
a = 2;
b = 1;
u = a;
if ((me.istilesolidandaffected(LingoGlobal.point((q2-1),c2),layer) == 1)) {
u = b;
}
for (int tmp_lr = (((layer-1)*10)+4); tmp_lr <= (((layer-1)*10)+6); tmp_lr++) {
lr = tmp_lr;
_global.member(LingoGlobal.concat(@"layer",lr)).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((mdpoint+LingoGlobal.point(-10,-10)),(mdpoint+LingoGlobal.point((-10+u),10))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
me.draw3dbeams(q2,c2,layer,tlrct,new LingoList(new dynamic[] { 1,4 }),u);
u = a;
if ((me.istilesolidandaffected(LingoGlobal.point((q2+1),c2),layer) == 1)) {
u = b;
}
for (int tmp_lr = (((layer-1)*10)+4); tmp_lr <= (((layer-1)*10)+6); tmp_lr++) {
lr = tmp_lr;
_global.member(LingoGlobal.concat(@"layer",lr)).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((mdpoint+LingoGlobal.point((10-u),-10)),(mdpoint+LingoGlobal.point(10,10))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
me.draw3dbeams(q2,c2,layer,tlrct,new LingoList(new dynamic[] { 2,3 }),u);
u = a;
if ((me.istilesolidandaffected(LingoGlobal.point(q2,(c2-1)),layer) == 1)) {
u = b;
}
for (int tmp_lr = (((layer-1)*10)+4); tmp_lr <= (((layer-1)*10)+6); tmp_lr++) {
lr = tmp_lr;
_global.member(LingoGlobal.concat(@"layer",lr)).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((mdpoint+LingoGlobal.point(-10,-10)),(mdpoint+LingoGlobal.point(10,(-10+u)))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
me.draw3dbeams(q2,c2,layer,tlrct,new LingoList(new dynamic[] { 1,2 }),u);
u = a;
if ((me.istilesolidandaffected(LingoGlobal.point(q2,(c2+1)),layer) == 1)) {
u = b;
}
for (int tmp_lr = (((layer-1)*10)+4); tmp_lr <= (((layer-1)*10)+6); tmp_lr++) {
lr = tmp_lr;
_global.member(LingoGlobal.concat(@"layer",lr)).image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((mdpoint+LingoGlobal.point(-10,(10-u))),(mdpoint+LingoGlobal.point(10,10))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
me.draw3dbeams(q2,c2,layer,tlrct,new LingoList(new dynamic[] { 3,4 }),u);
}
redrawpoles(LingoGlobal.point(q2,c2),layer,q,c,(((layer-1)*10)+4));
}

return null;
}
public dynamic draw3dbeams(dynamic me,dynamic q2,dynamic c2,dynamic layer,dynamic tlrct,dynamic crnrs,dynamic u) {
dynamic crnr = null;
dynamic rct = null;
dynamic lr = null;
if ((layer > 1)) {
if ((me.istilesolidandaffected(LingoGlobal.point(q2,c2),(layer-1)) == 1)) {
foreach (dynamic tmp_crnr in crnrs) {
crnr = tmp_crnr;
rct = cornerrect(tlrct,crnr,u);
for (int tmp_lr = (((layer-1)*10)-5); tmp_lr <= (((layer-1)*10)+5); tmp_lr++) {
lr = tmp_lr;
_global.member(LingoGlobal.concat(@"layer",lr)).image.copypixels(_global.member(@"pxl").image,rct,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
}
}
}
if ((layer < 3)) {
if ((me.istilesolidandaffected(LingoGlobal.point(q2,c2),(layer+1)) == 1)) {
rct = cornerrect(tlrct,crnr,u);
foreach (dynamic tmp_crnr in crnrs) {
crnr = tmp_crnr;
for (int tmp_lr = (((layer-1)*10)+5); tmp_lr <= (((layer-1)*10)+15); tmp_lr++) {
lr = tmp_lr;
_global.member(LingoGlobal.concat(@"layer",lr)).image.copypixels(_global.member(@"pxl").image,rct,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
}
}
}
}

return null;
}
public dynamic cornerrect(dynamic tlrct,dynamic crnr,dynamic u) {
switch (crnr) {
case 1:
return LingoGlobal.rect(tlrct.left,tlrct.top,(tlrct.left+u),(tlrct.top+u));
break;
case 2:
return LingoGlobal.rect((tlrct.right-u),tlrct.top,tlrct.right,(tlrct.top+u));
break;
case 3:
return LingoGlobal.rect((tlrct.right-u),(tlrct.bottom-u),tlrct.right,tlrct.bottom);
break;
case 4:
return LingoGlobal.rect(tlrct.left,(tlrct.bottom-u),(tlrct.left+u),tlrct.bottom);
break;
}

return null;
}
public dynamic istilesolidandaffected(dynamic me,dynamic tl,dynamic layer) {
if ((((((_movieScript.afamvlvledit(LingoGlobal.point(tl.loch,tl.locv),layer) != 1) | (tl.loch < 1)) | (tl.locv < 1)) | (tl.loch > _movieScript.global_gloprops.size.loch)) | (tl.locv > _movieScript.global_gloprops.size.locv))) {
return 0;
}
else if ((_movieScript.global_geeprops.effects[_movieScript.global_r].mtrx[tl.loch][tl.locv] > 0)) {
return 1;
}
else {
return 0;
}

return null;
}
public dynamic redrawpoles(dynamic pos,dynamic layer,dynamic q,dynamic c,dynamic drawlayer) {
dynamic t = null;
dynamic rct = null;
if (LingoGlobal.ToBool(pos.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
foreach (dynamic tmp_t in _movieScript.global_gleprops.matrix[pos.loch][pos.locv][layer][2]) {
t = tmp_t;
switch (t) {
case 1:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(0,8,0,-8));
_global.member(LingoGlobal.concat(@"layer",drawlayer)).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
break;
case 2:
rct = (LingoGlobal.rect(((q-1)*20),((c-1)*20),(q*20),(c*20))+LingoGlobal.rect(8,0,-8,0));
_global.member(LingoGlobal.concat(@"layer",drawlayer)).image.copypixels(_global.member(@"pxl").image,rct,_global.member(@"pxl").image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
break;
}
}
}

return null;
}
public dynamic closestcamera(dynamic me,dynamic pos) {
dynamic closest = null;
dynamic bestcam = null;
dynamic camnum = null;
closest = 1000;
bestcam = 0;
for (int tmp_camNum = 1; tmp_camNum <= _movieScript.global_gcameraprops.cameras.count; tmp_camNum++) {
camnum = tmp_camNum;
if (((me.seenbycamera(camnum,pos) == 1) & (_movieScript.diag(pos,(_movieScript.global_gcameraprops.cameras[camnum]+LingoGlobal.point((1400/2),(800/2)))) < closest))) {
closest = _movieScript.diag(pos,(_movieScript.global_gcameraprops.cameras[camnum]+LingoGlobal.point((1400/2),(800/2))));
bestcam = camnum;
}
}
return bestcam;

}
public dynamic seenbycamera(dynamic me,dynamic camnum,dynamic pos) {
dynamic camerapos = null;
camerapos = _movieScript.global_gcameraprops.cameras[camnum];
if (LingoGlobal.ToBool(pos.inside((LingoGlobal.rect(camerapos.loch,camerapos.locv,(camerapos.loch+1400),(camerapos.locv+800))+(LingoGlobal.rect(-15,-10,15,10)*20))))) {
return 1;
}
else {
return 0;
}

return null;
}
}
}
