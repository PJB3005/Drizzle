using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: effectsEditor
//
public sealed class effectsEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic l = null;
dynamic rct = null;
dynamic mstile = null;
dynamic actn = null;
dynamic actn2 = null;
dynamic sizeadd = null;
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((LingoGlobal.ToBool(_global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q])) & (_movieScript.global_gdirectionkeys[q] == 0))) {
_movieScript.global_gleprops.campos = ((_movieScript.global_gleprops.campos+new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })[q])*((1+9)*_global._key.keypressed(83)));
for (int tmp_l = 1; tmp_l <= 3; tmp_l++) {
l = tmp_l;
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),l);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),l);
}
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16);
me.drawefmtrx(_movieScript.global_geeprops.editeffect);
}
_movieScript.global_gdirectionkeys[q] = _global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q]);
}
if ((_movieScript.global_genveditorprops.waterlevel == -1)) {
_global.sprite(11).rect = LingoGlobal.rect(0,0,0,0);
}
else {
rct = (LingoGlobal.rect(0,((_movieScript.global_gloprops.size.locv-_movieScript.global_genveditorprops.waterlevel)-_movieScript.global_gloprops.extratiles[4]),_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv)-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
_global.sprite(11).rect = (((rct.intersect(LingoGlobal.rect(0,0,52,40))+LingoGlobal.rect(1,1,1,1))*LingoGlobal.rect(16,16,16,16))+LingoGlobal.rect(0,-8,0,0));
}
mstile = ((_global._mouse.mouseloc/LingoGlobal.point(new LingoDecimal(16),new LingoDecimal(16)))-LingoGlobal.point(new LingoDecimal(0.4999),new LingoDecimal(0.4999)));
mstile = LingoGlobal.point(mstile.loch.integer,mstile.locv.integer);
mstile = (mstile+_movieScript.global_gleprops.campos);
actn = 0;
actn2 = 0;
_movieScript.global_geeprops.keys.m1 = _global._mouse.mousedown;
if ((LingoGlobal.ToBool(_movieScript.global_geeprops.keys.m1) & (_movieScript.global_geeprops.lastkeys.m1 == 0))) {
actn = 1;
}
_movieScript.global_geeprops.lastkeys.m1 = _movieScript.global_geeprops.keys.m1;
_movieScript.global_geeprops.keys.m2 = _global._mouse.rightmousedown;
if ((LingoGlobal.ToBool(_movieScript.global_geeprops.keys.m2) & (_movieScript.global_geeprops.lastkeys.m2 == 0))) {
actn2 = 1;
}
_movieScript.global_geeprops.lastkeys.m2 = _movieScript.global_geeprops.keys.m2;
if ((mstile != _movieScript.global_geeprops.lstmsps)) {
actn = _movieScript.global_geeprops.keys.m1;
actn2 = _movieScript.global_geeprops.keys.m2;
}
_movieScript.global_geeprops.lstmsps = mstile;
if (LingoGlobal.ToBool(actn)) {
me.usebrush(mstile,1);
}
if (LingoGlobal.ToBool(actn2)) {
me.usebrush(mstile,-1);
}
if (LingoGlobal.ToBool(me.checkkey(@"N"))) {
me.initmode(@"createNew");
}
if (LingoGlobal.ToBool(me.checkkey(@"E"))) {
me.initmode(@"chooseEffect");
}
switch (_movieScript.global_geeprops.mode) {
case @"createNew":
if (LingoGlobal.ToBool(me.checkkey(@"W"))) {
me.updateeffectsmenu(LingoGlobal.point(0,-1));
}
if (LingoGlobal.ToBool(me.checkkey(@"S"))) {
me.updateeffectsmenu(LingoGlobal.point(0,1));
}
if (LingoGlobal.ToBool(me.checkkey(@"A"))) {
me.updateeffectsmenu(LingoGlobal.point(-1,0));
}
if (LingoGlobal.ToBool(me.checkkey(@"D"))) {
me.updateeffectsmenu(LingoGlobal.point(1,0));
}
if (LingoGlobal.ToBool(_global._key.keypressed(@" "))) {
me.neweffect();
_movieScript.global_geeprops.editeffect = _movieScript.global_geeprops.effects.count;
_movieScript.global_geeprops.selectediteffect = _movieScript.global_geeprops.effects.count;
me.initmode(@"editEffect");
}
_global.sprite(21).rect = LingoGlobal.rect(-100,-100,-100,-100);
break;
case @"chooseEffect":
if (LingoGlobal.ToBool(me.checkkey(@"W"))) {
me.updateeffectsl(-1);
}
if (LingoGlobal.ToBool(me.checkkey(@"S"))) {
me.updateeffectsl(1);
}
if ((LingoGlobal.ToBool(_global._key.keypressed(@" ")) & (_movieScript.global_lstspace == 0))) {
_movieScript.global_geeprops.editeffect = _movieScript.global_geeprops.selectediteffect;
me.initmode(@"editEffect");
me.updateeffectsl(0);
}
_global.sprite(21).rect = LingoGlobal.rect(-100,-100,-100,-100);
break;
case @"editEffect":
if (LingoGlobal.ToBool(me.checkkey(@"r"))) {
_movieScript.global_geeprops.brushsize = _movieScript.restrict((_movieScript.global_geeprops.brushsize+1),1,10);
}
if (LingoGlobal.ToBool(me.checkkey(@"f"))) {
_movieScript.global_geeprops.brushsize = _movieScript.restrict((_movieScript.global_geeprops.brushsize-1),1,10);
}
if (LingoGlobal.ToBool(me.checkkey(@"W"))) {
me.updateediteffect(LingoGlobal.point(0,-1));
}
if (LingoGlobal.ToBool(me.checkkey(@"S"))) {
me.updateediteffect(LingoGlobal.point(0,1));
}
if (LingoGlobal.ToBool(me.checkkey(@"A"))) {
me.updateediteffect(LingoGlobal.point(-1,0));
}
if (LingoGlobal.ToBool(me.checkkey(@"D"))) {
me.updateediteffect(LingoGlobal.point(1,0));
}
if ((_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][1] == @"seed")) {
me.changeoption();
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(@" ")) & (_movieScript.global_lstspace == 0))) {
me.changeoption();
}
sizeadd = (LingoGlobal.rect((-_movieScript.global_geeprops.brushsize+1),(-_movieScript.global_geeprops.brushsize+1),(_movieScript.global_geeprops.brushsize-1),(_movieScript.global_geeprops.brushsize-1))*LingoGlobal.rect(16,16,16,16));
_global.sprite(21).rect = (((LingoGlobal.rect((mstile-_movieScript.global_gleprops.campos),(mstile-_movieScript.global_gleprops.campos))*LingoGlobal.rect(16,16,16,16))+LingoGlobal.rect(0,0,16,16))+sizeadd);
break;
}
_global.sprite(15).rect = ((LingoGlobal.rect((mstile-_movieScript.global_gleprops.campos),(mstile-_movieScript.global_gleprops.campos))*LingoGlobal.rect(16,16,16,16))+LingoGlobal.rect(0,0,16,16));
_movieScript.global_lstspace = _global._key.keypressed(@" ");
_global.script(@"levelOverview").gotoeditor();
_global.go(_global.the_frame);

return null;
}
public dynamic checkkey(dynamic me,dynamic key) {
dynamic rtrn = null;
rtrn = 0;
_movieScript.global_geeprops.keys[LingoGlobal.symbol(key)] = _global._key.keypressed(key);
if ((LingoGlobal.ToBool(_movieScript.global_geeprops.keys[LingoGlobal.symbol(key)]) & (_movieScript.global_geeprops.lastkeys[LingoGlobal.symbol(key)] == 0))) {
rtrn = 1;
}
_movieScript.global_geeprops.lastkeys[LingoGlobal.symbol(key)] = _movieScript.global_geeprops.keys[LingoGlobal.symbol(key)];
return rtrn;

}
public dynamic updateeffectsmenu(dynamic me,dynamic mv) {
dynamic txt = null;
dynamic tl = null;
_movieScript.global_geeprops.empos = (_movieScript.global_geeprops.empos+mv);
if ((_movieScript.global_geeprops.empos.loch < 1)) {
_movieScript.global_geeprops.empos.loch = _movieScript.global_geffects.count;
}
else if ((_movieScript.global_geeprops.empos.loch > _movieScript.global_geffects.count)) {
_movieScript.global_geeprops.empos.loch = 1;
}
if ((_movieScript.global_geeprops.empos.locv < 1)) {
_movieScript.global_geeprops.empos.locv = _movieScript.global_geffects[_movieScript.global_geeprops.empos.loch].efs.count;
}
else if ((_movieScript.global_geeprops.empos.locv > _movieScript.global_geffects[_movieScript.global_geeprops.empos.loch].efs.count)) {
_movieScript.global_geeprops.empos.locv = 1;
}
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_tl = 1; tmp_tl <= _movieScript.global_geffects[_movieScript.global_geeprops.empos.loch].efs.count; tmp_tl++) {
tl = tmp_tl;
if ((tl == _movieScript.global_geeprops.empos.locv)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
}
_global.member(@"tileMenu").text = txt;

return null;
}
public dynamic updateeffectsl(dynamic me,dynamic mv) {
dynamic txt = null;
dynamic ef = null;
txt = @"";
if ((_movieScript.global_geeprops.effects.count != 0)) {
_movieScript.global_geeprops.selectediteffect = (_movieScript.global_geeprops.selectediteffect+mv);
if ((_movieScript.global_geeprops.selectediteffect < 1)) {
_movieScript.global_geeprops.selectediteffect = _movieScript.global_geeprops.effects.count;
}
else if ((_movieScript.global_geeprops.selectediteffect > _movieScript.global_geeprops.effects.count)) {
_movieScript.global_geeprops.selectediteffect = 1;
}
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_ef = 1; tmp_ef <= _movieScript.global_geeprops.effects.count; tmp_ef++) {
ef = tmp_ef;
if ((ef == _movieScript.global_geeprops.editeffect)) {
txt += txt.ToString();
_global.member(@"editEffectName").text = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat_space(_movieScript.global_geeprops.effects[ef].nm,@"("),_global.@string(ef)),@")");
}
else if ((ef == _movieScript.global_geeprops.selectediteffect)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
txt += txt.ToString();
}
me.drawefmtrx(_movieScript.global_geeprops.selectediteffect);
}
else {
_global.member(@"editEffectName").text = @"No effects added";
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
}
_global.member(@"effectsL").text = txt;

return null;
}
public dynamic neweffect(dynamic me) {
dynamic ef = null;
dynamic fillwith = null;
dynamic crossscreen = null;
dynamic q = null;
dynamic ql = null;
dynamic c = null;
ef = new LingoPropertyList {[new LingoSymbol("nm")] = _movieScript.global_geffects[_movieScript.global_geeprops.empos.loch].efs[_movieScript.global_geeprops.empos.locv].nm,[new LingoSymbol("tp")] = @"nn",[new LingoSymbol("crossscreen")] = 0,[new LingoSymbol("mtrx")] = new LingoPropertyList {},[new LingoSymbol("options")] = new LingoList(new dynamic[] { new LingoList(new dynamic[] { @"Delete/Move",new LingoList(new dynamic[] { @"Delete",@"Move Back",@"Move Forth" }),@"" }) })};
fillwith = 0;
switch (ef.nm) {
case @"slime":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),130);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
ef.options.add(new LingoList(new dynamic[] { @"3D",new LingoList(new dynamic[] { @"Off",@"On" }),@"Off" }));
break;
case @"slimeX3":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),(130*3));
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
ef.options.add(new LingoList(new dynamic[] { @"3D",new LingoList(new dynamic[] { @"Off",@"On" }),@"Off" }));
break;
case @"DecalsOnlySlime":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),130);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
break;
case @"melt":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),60);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
break;
case @"rust":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),60);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.2));
ef.options.add(new LingoList(new dynamic[] { @"3D",new LingoList(new dynamic[] { @"Off",@"On" }),@"Off" }));
break;
case @"barnacles":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),60);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.3));
ef.options.add(new LingoList(new dynamic[] { @"3D",new LingoList(new dynamic[] { @"Off",@"On" }),@"Off" }));
break;
case @"erode":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),80);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
break;
case @"Super Erode":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),60);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
break;
case @"Roughen":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),30);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.05));
break;
case @"Super Melt":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),50);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
ef.options.add(new LingoList(new dynamic[] { @"3D",new LingoList(new dynamic[] { @"Off",@"On" }),@"Off" }));
break;
case @"Destructive Melt":
ef.tp = @"standardErosion";
ef.addprop(new LingoSymbol("repeats"),50);
ef.addprop(new LingoSymbol("affectopenareas"),new LingoDecimal(0.5));
ef.options.add(new LingoList(new dynamic[] { @"3D",new LingoList(new dynamic[] { @"Off",@"On" }),@"Off" }));
break;
case @"Rubble":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
break;
case @"Fungi Flowers":
case @"Lighthouse Flowers":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"1",@"2",@"3" }),@"1" }));
break;
case @"Fern":
case @"Giant Mushroom":
case @"sprawlBush":
case @"featherFern":
case @"Fungus Tree":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"1",@"2",@"3" }),@"1" }));
ef.options.add(new LingoList(new dynamic[] { @"Color",new LingoList(new dynamic[] { @"Color1",@"Color2",@"Dead" }),@"Color2" }));
break;
case @"Root Grass":
case @"Growers":
case @"Cacti":
case @"Rain Moss":
case @"Seed Pods":
case @"Grass":
case @"Arm Growers":
case @"Horse Tails":
case @"Circuit Plants":
case @"Feather Plants":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
ef.options.add(new LingoList(new dynamic[] { @"Color",new LingoList(new dynamic[] { @"Color1",@"Color2",@"Dead" }),@"Color2" }));
if ((new LingoList(new dynamic[] { @"Arm Growers",@"Growers" }).getpos(ef.nm) > 0)) {
ef.crossscreen = 1;
}
break;
case @"Rollers":
case @"Thorn Growers":
case @"Garbage Spirals":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
ef.options.add(new LingoList(new dynamic[] { @"Color",new LingoList(new dynamic[] { @"Color1",@"Color2",@"Dead" }),@"Color2" }));
ef.crossscreen = 1;
break;
case @"wires":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
ef.options.add(new LingoList(new dynamic[] { @"Fatness",new LingoList(new dynamic[] { @"1px",@"2px",@"3px",@"random" }),@"2px" }));
crossscreen = 1;
break;
case @"chains":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
ef.options.add(new LingoList(new dynamic[] { @"Size",new LingoList(new dynamic[] { @"Small",@"FAT" }),@"Small" }));
ef.crossscreen = 1;
break;
case @"blackGoo":
fillwith = 100;
break;
case @"Hang Roots":
case @"Thick Roots":
case @"Shadow Plants":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
ef.crossscreen = 1;
break;
case @"Restore As Scaffolding":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
break;
case @"Restore As Pipes":
ef.options.add(new LingoList(new dynamic[] { @"Layers",new LingoList(new dynamic[] { @"All",@"1",@"2",@"3",@"1:st and 2:nd",@"2:nd and 3:rd" }),@"All" }));
break;
case @"Ceramic Chaos":
ef.options.add(new LingoList(new dynamic[] { @"Colored",new LingoList(new dynamic[] { @"None",@"White" }),@"White" }));
break;
case @"DaddyCorruption":
break;
}
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
ql = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
ql.add(fillwith);
}
ef.mtrx.add(ql);
}
ef.options.add(new LingoList(new dynamic[] { @"Seed",new LingoPropertyList {},_global.random(500) }));
_movieScript.global_geeprops.effects.add(ef);
me.updateeffectsl(0);

return null;
}
public dynamic usebrush(dynamic me,dynamic pnt,dynamic fac) {
dynamic strength = null;
dynamic rct = null;
dynamic q = null;
dynamic c = null;
dynamic val = null;
dynamic digfac = null;
if ((_movieScript.global_geeprops.mode == @"editEffect")) {
strength = (10+(90*_global._key.keypressed(@"T")));
if ((new LingoList(new dynamic[] { @"BlackGoo",@"Fungi Flowers",@"Lighthouse Flowers",@"Fern",@"Giant Mushroom",@"Sprawlbush",@"featherFern",@"Fungus Tree",@"Restore As Scaffolding",@"Restore As Pipes" }).getpos(_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].nm) > 0)) {
strength = 10000;
if ((_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].nm != @"BlackGoo")) {
_movieScript.global_geeprops.brushsize = 1;
}
}
rct = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-_movieScript.global_geeprops.brushsize,-_movieScript.global_geeprops.brushsize,_movieScript.global_geeprops.brushsize,_movieScript.global_geeprops.brushsize));
for (int tmp_q = rct.left; tmp_q <= rct.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = rct.top; tmp_c <= rct.bottom; tmp_c++) {
c = tmp_c;
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
val = _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].mtrx[q][c];
digfac = (new LingoDecimal(1)-(LingoGlobal.floatmember_helper(_movieScript.diag(LingoGlobal.point(q,c),pnt))/_movieScript.global_geeprops.brushsize));
if ((digfac > 0)) {
val = _movieScript.restrict((((val+strength)*digfac)*fac),0,100);
_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].mtrx[q][c] = val;
me.updateeffectgraph(LingoGlobal.point(q,c),val);
}
}
}
}
}

return null;
}
public dynamic drawefmtrx(dynamic me,dynamic l) {
dynamic a = null;
dynamic b = null;
if (((_movieScript.global_geeprops.effects.count > 0) & (l > 0))) {
for (int tmp_a = _movieScript.global_gleprops.campos.loch; tmp_a <= (_movieScript.global_gleprops.campos.loch+52); tmp_a++) {
a = tmp_a;
for (int tmp_b = _movieScript.global_gleprops.campos.locv; tmp_b <= (_movieScript.global_gleprops.campos.locv+40); tmp_b++) {
b = tmp_b;
if (((((a > 0) & (a <= _movieScript.global_gloprops.size.loch)) & (b > 0)) & (b <= _movieScript.global_gloprops.size.locv))) {
me.updateeffectgraph(LingoGlobal.point(a,b),_movieScript.global_geeprops.effects[l].mtrx[a][b]);
}
else {
me.updateeffectgraph(LingoGlobal.point(a,b),-1);
}
}
}
}
else {
_global.member(@"effectsMatrix").image.copypixels(_global.member(@"pxl").image,_global.member(@"effectsMatrix").image.rect,_global.member(@"pxl").image.rect);
}

return null;
}
public dynamic updateeffectgraph(dynamic me,dynamic tile,dynamic strength) {
if ((strength == -1)) {
_global.member(@"effectsMatrix").image.setpixel(((tile.loch-_movieScript.global_gleprops.campos.loch)-1),((tile.locv-_movieScript.global_gleprops.campos.locv)-1),_global.color(0,0,0));
}
else {
strength = (strength/new LingoDecimal(100));
_global.member(@"effectsMatrix").image.setpixel(((tile.loch-_movieScript.global_gleprops.campos.loch)-1),((tile.locv-_movieScript.global_gleprops.campos.locv)-1),_global.color((255-(strength*255)),(255*strength),(255-(strength*255))));
}

return null;
}
public dynamic initmode(dynamic me,dynamic md) {
switch (md) {
case @"chooseEffect":
if ((_movieScript.global_geeprops.effects.count > 0)) {
_movieScript.global_geeprops.selectediteffect = _movieScript.global_geeprops.editeffect;
_global.sprite(20).rect = LingoGlobal.rect(((53*16)+8),(10*16),(1024-8),(41*16));
_global.member(@"EEhelp").text = @"Use W, S and the spacebar to select an effect to edit";
}
break;
case @"editEffect":
_global.sprite(20).rect = LingoGlobal.rect(8,(42*16),(1024-8),(768-8));
_global.member(@"EEhelp").text = @"Edit effect: Use WASD to change the settings of the effect. Use the left and right mouse buttons to change the effect matrix";
break;
case @"createNew":
_global.sprite(20).rect = LingoGlobal.rect(((53*16)+8),8,(1024-8),(10*16));
_global.member(@"EEhelp").text = @"Create new: Use the WASD keys and the spacebar to select an effect to add";
break;
default:
_global.sprite(20).rect = LingoGlobal.rect(-1,-1,-1,-1);
_global.member(@"EEhelp").text = @"---";
break;
}
_movieScript.global_geeprops.mode = md;
me.updatealltext();

return null;
}
public dynamic updateediteffect(dynamic me,dynamic mv) {
dynamic txt = null;
dynamic ef = null;
if ((_movieScript.global_geeprops.effects != new LingoPropertyList {})) {
if ((_movieScript.global_geeprops.editeffect > _movieScript.global_geeprops.effects.count)) {
_movieScript.global_geeprops.editeffect = _movieScript.global_geeprops.effects.count;
}
_movieScript.global_geeprops.empos = (_movieScript.global_geeprops.empos+mv);
if ((_movieScript.global_geeprops.empos.locv > _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options.count)) {
_movieScript.global_geeprops.empos.locv = 1;
}
else if ((_movieScript.global_geeprops.empos.locv < 1)) {
_movieScript.global_geeprops.empos.locv = _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options.count;
}
if ((_movieScript.global_geeprops.empos.loch > _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][2].count)) {
_movieScript.global_geeprops.empos.loch = 1;
}
else if ((_movieScript.global_geeprops.empos.loch < 1)) {
_movieScript.global_geeprops.empos.loch = _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][2].count;
}
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_ef = 1; tmp_ef <= _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][2].count; tmp_ef++) {
ef = tmp_ef;
if ((ef == _movieScript.global_geeprops.empos.loch)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
}
_global.member(@"effectOptions").text = txt;
}
else {
_global.member(@"effectOptions").text = @"";
}

return null;
}
public dynamic changeoption(dynamic me) {
dynamic sv = null;
switch (_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][1]) {
case @"Delete/Move":
switch (_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][2][_movieScript.global_geeprops.empos.loch]) {
case @"Delete":
_movieScript.global_geeprops.effects.deleteat(_movieScript.global_geeprops.editeffect);
me.initmode(@"chooseEffect");
_movieScript.global_geeprops.editeffect = _movieScript.global_geeprops.effects.count;
me.updatealltext();
break;
case @"Move Back":
sv = _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].duplicate();
_movieScript.global_geeprops.effects.deleteat(_movieScript.global_geeprops.editeffect);
_movieScript.global_geeprops.editeffect = _movieScript.restrict((_movieScript.global_geeprops.editeffect-1),1,_movieScript.global_geeprops.effects.count);
_movieScript.global_geeprops.effects.addat(_movieScript.global_geeprops.editeffect,sv);
_movieScript.global_geeprops.selectediteffect = _movieScript.global_geeprops.editeffect;
me.updateeffectsl(0);
break;
case @"Move Forth":
sv = _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].duplicate();
_movieScript.global_geeprops.effects.deleteat(_movieScript.global_geeprops.editeffect);
_movieScript.global_geeprops.editeffect = _movieScript.restrict((_movieScript.global_geeprops.editeffect+1),1,(_movieScript.global_geeprops.effects.count+1));
_movieScript.global_geeprops.effects.addat(_movieScript.global_geeprops.editeffect,sv);
_movieScript.global_geeprops.selectediteffect = _movieScript.global_geeprops.editeffect;
me.updateeffectsl(0);
break;
}
break;
case @"Seed":
if (LingoGlobal.ToBool(_global._key.keypressed(@"A"))) {
_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3] = (_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3]-1);
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"D"))) {
_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3] = (_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3]+1);
}
_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3] = _movieScript.restrict(_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3],1,500);
break;
case @"Color":
case @"Fatness":
case @"Size":
case @"Layers":
case @"3D":
case @"Colored":
_movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][3] = _movieScript.global_geeprops.effects[_movieScript.global_geeprops.editeffect].options[_movieScript.global_geeprops.empos.locv][2][_movieScript.global_geeprops.empos.loch];
break;
}
me.updateediteffect(LingoGlobal.point(0,0));

return null;
}
public dynamic updatealltext(dynamic me) {
me.updateeffectsl(0);
me.updateediteffect(LingoGlobal.point(0,0));
me.updateeffectsmenu(LingoGlobal.point(0,0));

return null;
}
}
}
