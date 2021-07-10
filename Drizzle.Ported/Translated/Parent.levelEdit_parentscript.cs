using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Parent script: levelEdit_parentscript
//
public sealed class levelEdit_parentscript : LingoParentScript {
public dynamic p;
public dynamic newupdate(dynamic me) {
dynamic currtool = null;
dynamic mstile = null;
dynamic mv = null;
dynamic affectrect = null;
dynamic sav = null;
dynamic changeto = null;
dynamic wrkpos = null;
dynamic q = null;
dynamic c = null;
dynamic drwrect = null;
dynamic rct1 = null;
dynamic rct2 = null;
dynamic affectrect2 = null;
dynamic drwaffrct = null;
dynamic pnt1 = null;
dynamic pnt2 = null;
p.lastworkpos = p.workpos;
p.lastinput = p.input;
p.input = new LingoList(new dynamic[] { LingoGlobal.point(0,0),0,0 });
currtool = _movieScript.global_gleprops.toolmatrix[p.toolpos.locv][p.toolpos.loch];
switch (p.playernum) {
case 1:
p.input[1].loch = (p.input[1].loch-_global._key.keypressed(123));
p.input[1].loch = (p.input[1].loch+_global._key.keypressed(124));
p.input[1].locv = (p.input[1].locv-_global._key.keypressed(126));
p.input[1].locv = (p.input[1].locv+_global._key.keypressed(125));
p.input[2] = _global._key.keypressed(@"K");
p.input[3] = _global._key.keypressed(@"L");
mstile = ((_global._mouse.mouseloc/LingoGlobal.point(new LingoDecimal(16),new LingoDecimal(16)))+LingoGlobal.point(new LingoDecimal(0.4999),new LingoDecimal(0.4999)));
mstile = ((LingoGlobal.point(mstile.loch.integer,mstile.locv.integer)+LingoGlobal.point(-11,-1))+_movieScript.global_gleprops.campos);
if (LingoGlobal.ToBool(mstile.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
p.workpos = mstile;
p.input[3] = LingoGlobal.op_or(p.input[3],_global._mouse.mousedown);
}
break;
case 2:
mstile = LingoGlobal.point(-100,-100);
p.input[1].loch = (p.input[1].loch-_global._key.keypressed(@"S"));
p.input[1].loch = (p.input[1].loch+_global._key.keypressed(@"F"));
p.input[1].locv = (p.input[1].locv-_global._key.keypressed(@"E"));
p.input[1].locv = (p.input[1].locv+_global._key.keypressed(@"D"));
p.input[2] = _global._key.keypressed(@"Q");
p.input[3] = _global._key.keypressed(@"W");
break;
}
mv = LingoGlobal.point(0,0);
if ((p.lastinput[1].loch != p.input[1].loch)) {
mv.loch = p.input[1].loch;
}
if ((p.lastinput[1].locv != p.input[1].locv)) {
mv.locv = p.input[1].locv;
}
if ((mv != LingoGlobal.point(0,0))) {
if ((LingoGlobal.ToBool(p.input[2]) | LingoGlobal.ToBool(mstile.inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1)))))) {
p.toolpos = (p.toolpos+mv);
p.toolpos.loch = _movieScript.restrictwithflip(p.toolpos.loch,1,4);
p.toolpos.locv = _movieScript.restrictwithflip(p.toolpos.locv,1,_movieScript.global_gleprops.toolmatrix.count);
currtool = _movieScript.global_gleprops.toolmatrix[p.toolpos.locv][p.toolpos.loch];
me.updatetooltext();
}
else {
switch (currtool) {
case @"move":
me.movecanvas(mv);
break;
case @"setMirrorPoint":
p.mirrorpos = _movieScript.restrict((p.mirrorpos+mv.loch),1,_movieScript.global_gloprops.size.loch);
break;
default:
p.workpos = (p.workpos+mv);
p.workpos.loch = _movieScript.restrictwithflip(p.workpos.loch,1,_movieScript.global_gloprops.size.loch);
p.workpos.locv = _movieScript.restrictwithflip(p.workpos.locv,1,_movieScript.global_gloprops.size.locv);
break;
}
}
}
if ((new LingoList(new dynamic[] { @"squareWall",@"squareAir",@"copyBack",@"flip" }).getpos(currtool) > 0)) {
affectrect = LingoGlobal.rect(p.workpos,p.rectpos);
if ((affectrect.top > affectrect.bottom)) {
sav = affectrect.bottom;
affectrect.bottom = affectrect.top;
affectrect.top = sav;
}
if ((affectrect.left > affectrect.right)) {
sav = affectrect.right;
affectrect.right = affectrect.left;
affectrect.left = sav;
}
}
else {
p.rectfollow = 1;
affectrect = LingoGlobal.rect(p.workpos.loch,p.workpos.locv,p.workpos.loch,p.workpos.locv);
}
if (LingoGlobal.ToBool(p.rectfollow)) {
p.rectpos = p.workpos;
}
changeto = @"NOCHANGE";
if (LingoGlobal.ToBool(p.input[3])) {
switch (currtool) {
case @"inverse":
if ((p.lastinput[3] == 0)) {
if ((_movieScript.global_gleprops.matrix[p.workpos.loch][p.workpos.locv][p.worklayer][1] == 0)) {
changeto = 1;
}
else {
changeto = 0;
}
}
break;
case @"paintWall":
changeto = 1;
break;
case @"paintAir":
changeto = 0;
break;
case @"floor":
changeto = 6;
break;
case @"slope":
if ((p.lastinput[3] == 0)) {
me.slopetile(p.workpos);
if (LingoGlobal.ToBool(p.mirror)) {
wrkpos = me.mirrorrect(LingoGlobal.rect(p.workpos,p.workpos));
wrkpos = LingoGlobal.point(wrkpos.left,wrkpos.top);
me.slopetile(wrkpos);
_movieScript.lvleditdraw(LingoGlobal.rect(wrkpos,wrkpos),p.worklayer);
}
}
break;
case @"lizardHole":
me.addremovefeature(7);
break;
case @"playerSpawn":
me.addremovefeature(6);
break;
case @"squareWall":
case @"squareAir":
if ((p.lastinput[3] == 0)) {
if (LingoGlobal.ToBool(p.rectfollow)) {
p.rectfollow = 0;
}
else {
p.rectfollow = 1;
changeto = LingoGlobal.op_eq(currtool,@"squareWall");
}
}
break;
case @"copyBack":
if ((p.lastinput[3] == 0)) {
if (LingoGlobal.ToBool(p.rectfollow)) {
p.rectfollow = 0;
}
else {
p.rectfollow = 1;
changeto = @"copyBack";
}
}
break;
case @"flip":
if ((p.lastinput[3] == 0)) {
if (LingoGlobal.ToBool(p.rectfollow)) {
p.rectfollow = 0;
}
else {
p.rectfollow = 1;
changeto = @"flip";
}
}
break;
case @"horbeam":
me.addremovefeature(1);
break;
case @"verBeam":
me.addremovefeature(2);
break;
case @"glass":
if (((p.lastinput[3] == 0) | (p.lastworkpos != p.workpos))) {
if ((_movieScript.global_gleprops.matrix[p.workpos.loch][p.workpos.locv][p.worklayer][1] == 9)) {
changeto = 0;
}
else {
changeto = 9;
}
}
break;
case @"shortCutEntrance":
me.addremovefeature(4);
_movieScript.drawshortcutsimg((affectrect+LingoGlobal.rect(-1,-1,1,1)),16);
break;
case @"shortCut":
me.addremovefeature(5);
_movieScript.drawshortcutsimg((affectrect+LingoGlobal.rect(-1,-1,1,1)),16);
break;
case @"hive":
me.addremovefeature(3);
break;
case @"workLayer":
if ((p.lastinput[3] == 0)) {
p.worklayer = (p.worklayer+1);
if ((p.worklayer > 3)) {
p.worklayer = 1;
}
}
me.updatetooltext();
break;
case @"mirrorToggle":
if ((p.lastinput[3] == 0)) {
p.mirror = (1-p.mirror);
}
break;
case @"rock":
me.addremovefeature(9);
break;
case @"spear":
me.addremovefeature(10);
break;
case @"crack":
me.addremovefeature(11);
break;
case @"forbidbats":
me.addremovefeature(12);
break;
case @"garbageHole":
me.addremovefeature(13);
break;
case @"waterfall":
me.addremovefeature(18);
break;
case @"WHAMH":
me.addremovefeature(19);
break;
case @"wormGrass":
me.addremovefeature(20);
break;
case @"scavengerHole":
me.addremovefeature(21);
break;
}
if ((changeto != @"NOCHANGE")) {
for (int tmp_q = affectrect.left; tmp_q <= affectrect.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = affectrect.top; tmp_c <= affectrect.bottom; tmp_c++) {
c = tmp_c;
switch (changeto) {
case @"copyBack":
_movieScript.global_gleprops.matrix[q][c][((p.worklayer+1)-LingoGlobal.op_eq(p.worklayer,3))][1] = _movieScript.global_gleprops.matrix[q][c][p.worklayer][1];
_movieScript.lvleditdraw(LingoGlobal.rect(q,c,q,c),((p.worklayer+1)-LingoGlobal.op_eq(p.worklayer,3)));
break;
case @"flip":
_movieScript.global_gleprops.matrix[q][c] = new LingoList(new dynamic[] { new LingoList(new dynamic[] { 0,new LingoPropertyList {} }),new LingoList(new dynamic[] { 0,new LingoPropertyList {} }),new LingoList(new dynamic[] { 0,new LingoPropertyList {} }) });
break;
default:
_movieScript.global_gleprops.matrix[q][c][p.worklayer][1] = changeto;
break;
}
}
}
}
_movieScript.lvleditdraw(affectrect,p.worklayer);
_movieScript.drawshortcutsimg((affectrect+LingoGlobal.rect(-1,-1,1,1)),16);
}
if ((changeto == @"flip")) {
_movieScript.lvleditdraw(affectrect,1);
_movieScript.lvleditdraw(affectrect,2);
_movieScript.lvleditdraw(affectrect,3);
}
drwrect = (affectrect-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
rct1 = (LingoGlobal.rect(((drwrect.left-1)*16),((drwrect.top-1)*16),(drwrect.right*16),(drwrect.bottom*16))+LingoGlobal.rect(176,16,176,16));
rct2 = (LingoGlobal.rect(((p.toolpos.loch-1)*32),((p.toolpos.locv-1)*32),(p.toolpos.loch*32),(p.toolpos.locv*32))+LingoGlobal.rect(16,16,16,16));
if ((p.playernum == 1)) {
if (((affectrect.width == 0) & (affectrect.height == 0))) {
_global.member(@"rulerText").text = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(@"x:",affectrect.left),@" y:"),affectrect.top);
}
else {
_global.member(@"rulerText").text = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(@"w:",(affectrect.width+1)),@" h:"),(affectrect.height+1));
}
_global.sprite(10).loc = LingoGlobal.point((rct1.right+10),(rct1.bottom-10));
}
_global.sprite(p.sprl[1]).rect = rct1;
_global.sprite(p.sprl[2]).rect = rct2;
if (LingoGlobal.ToBool(p.mirror)) {
_global.sprite(p.sprl[3]).rect = (LingoGlobal.rect((((p.mirrorpos-_movieScript.global_gleprops.campos.loch)*16)-1),0,(((p.mirrorpos-_movieScript.global_gleprops.campos.loch)*16)+1),(40*16))+LingoGlobal.rect(176,16,176,16));
_global.sprite(p.sprl[3]).member = _global.member(@"pxl");
affectrect2 = me.mirrorrect(affectrect);
drwaffrct = (affectrect2-LingoGlobal.rect(_movieScript.global_gleprops.campos,_movieScript.global_gleprops.campos));
_global.sprite(p.sprl[4]).rect = (LingoGlobal.rect(((drwaffrct.left-1)*16),((drwaffrct.top-1)*16),(drwaffrct.right*16),(drwaffrct.bottom*16))+LingoGlobal.rect(176,16,176,16));
if ((changeto != @"NOCHANGE")) {
for (int tmp_q = affectrect2.left; tmp_q <= affectrect2.right; tmp_q++) {
q = tmp_q;
for (int tmp_c = affectrect2.top; tmp_c <= affectrect2.bottom; tmp_c++) {
c = tmp_c;
if (LingoGlobal.ToBool(LingoGlobal.point(q,c).inside(LingoGlobal.rect(1,1,(_movieScript.global_gloprops.size.loch+1),(_movieScript.global_gloprops.size.locv+1))))) {
if ((changeto == @"copyBack")) {
_movieScript.global_gleprops.matrix[q][c][((p.worklayer+1)-LingoGlobal.op_eq(p.worklayer,3))][1] = _movieScript.global_gleprops.matrix[q][c][p.worklayer][1];
_movieScript.lvleditdraw(LingoGlobal.rect(q,c,q,c),((p.worklayer+1)-LingoGlobal.op_eq(p.worklayer,3)));
}
else if ((changeto != @"flip")) {
_movieScript.global_gleprops.matrix[q][c][p.worklayer][1] = changeto;
}
}
}
}
_movieScript.lvleditdraw(affectrect2,p.worklayer);
_movieScript.drawshortcutsimg((affectrect2+LingoGlobal.rect(-1,-1,1,1)),16);
}
}
else {
_global.sprite(p.sprl[3]).rect = LingoGlobal.rect(-100,-100,-100,-100);
_global.sprite(p.sprl[4]).rect = LingoGlobal.rect(-100,-100,-100,-100);
}
pnt1 = _movieScript.closestpntinrect(rct1,LingoGlobal.point(((rct2.left+rct2.width)*new LingoDecimal(0.5)),((rct2.top+rct2.height)*new LingoDecimal(0.5))));
pnt2 = _movieScript.closestpntinrect(rct2,LingoGlobal.point(((rct1.left+rct1.width)*new LingoDecimal(0.5)),((rct1.top+rct1.height)*new LingoDecimal(0.5))));
_global.sprite(p.sprl[5]).member = _global.member(LingoGlobal.concat(@"line",(1+LingoGlobal.op_lt(pnt1.locv,pnt2.locv))));
_global.sprite(p.sprl[5]).rect = LingoGlobal.rect(pnt1,(pnt2+LingoGlobal.point(0,1)));
_global.sprite(p.sprl[6]).loc = ((LingoGlobal.point(rct1.right,rct1.bottom)+LingoGlobal.point(32,32))-LingoGlobal.point(8,8));

return null;
}
public dynamic mirrorrect(dynamic me,dynamic rct) {
dynamic lft = null;
dynamic rght = null;
lft = (p.mirrorpos-(rct.left-p.mirrorpos));
rght = (p.mirrorpos-(rct.right-p.mirrorpos));
if ((lft < rght)) {
rct = LingoGlobal.rect(lft,rct.top,rght,rct.bottom);
}
else {
rct = LingoGlobal.rect(rght,rct.top,lft,rct.bottom);
}
return (rct+LingoGlobal.rect(1,0,1,0));

}
public dynamic slopetile(dynamic me,dynamic ps) {
dynamic nbs = null;
dynamic dir = null;
dynamic rslt = null;
nbs = @"";
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dir = tmp_dir;
nbs = LingoGlobal.concat(nbs,_movieScript.afamvlvledit((ps+dir),p.worklayer));
}
switch (nbs) {
case @"1001":
rslt = 2;
break;
case @"0011":
rslt = 3;
break;
case @"1100":
rslt = 4;
break;
case @"0110":
rslt = 5;
break;
default:
rslt = 0;
break;
}
if ((rslt != 0)) {
_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][1] = rslt;
}

return null;
}
public dynamic addremovefeature(dynamic me,dynamic ft) {
dynamic ps = null;
ps = p.workpos;
if (((p.lastinput[3] == 0) | (p.lastworkpos != p.workpos))) {
if ((_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][2].getpos(ft) == 0)) {
_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][2].add(ft);
}
else {
_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][2].deleteone(ft);
}
if (LingoGlobal.ToBool(p.mirror)) {
ps = me.mirrorrect(LingoGlobal.rect(p.workpos,p.workpos));
ps = LingoGlobal.point(ps.left,ps.top);
if ((_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][2].getpos(ft) == 0)) {
_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][2].add(ft);
}
else {
_movieScript.global_gleprops.matrix[ps.loch][ps.locv][p.worklayer][2].deleteone(ft);
}
}
_movieScript.lvleditdraw(LingoGlobal.rect(ps,ps),p.worklayer);
_movieScript.drawshortcutsimg((LingoGlobal.rect(ps,ps)+LingoGlobal.rect(-1,-1,1,1)),16);
}

return null;
}
public dynamic @new(dynamic me,dynamic playnm) {
dynamic offst = null;
dynamic cl = null;
dynamic q = null;
p = new LingoPropertyList {};
offst = 800;
if ((playnm == 1)) {
offst = 810;
}
p.addprop(new LingoSymbol("sprl"),new LingoList(new dynamic[] { 6,7,4,5,8,9 }));
p.sprl = (p.sprl+offst);
p.addprop(new LingoSymbol("playernum"),playnm);
_global.sprite(p.sprl[1]).linesize = 2;
_global.sprite(p.sprl[2]).linesize = 2;
_global.sprite(p.sprl[4]).linesize = 2;
if ((playnm == 1)) {
cl = _global.color(100,100,100);
}
else {
cl = _global.color(255,0,0);
}
for (int tmp_q = 1; tmp_q <= 5; tmp_q++) {
q = tmp_q;
_global.sprite(p.sprl[q]).color = cl;
}
p.addprop(new LingoSymbol("input"),new LingoList(new dynamic[] { LingoGlobal.point(0,0),0,0 }));
p.addprop(new LingoSymbol("lastinput"),new LingoList(new dynamic[] { LingoGlobal.point(0,0),0,0 }));
p.addprop(new LingoSymbol("toolpos"),LingoGlobal.point(1,1));
p.addprop(new LingoSymbol("workpos"),LingoGlobal.point(1,1));
p.addprop(new LingoSymbol("rectpos"),LingoGlobal.point(1,1));
p.addprop(new LingoSymbol("rectfollow"),0);
p.addprop(new LingoSymbol("toolprops"),@"");
p.addprop(new LingoSymbol("worklayer"),1);
p.addprop(new LingoSymbol("lastworkpos"),LingoGlobal.point(1,1));
p.addprop(new LingoSymbol("mirror"),0);
p.addprop(new LingoSymbol("mirrorpos"),(_movieScript.global_gloprops.size.loch/2));
me.updatetooltext();
_movieScript.global_gleprops.leveleditors.add(me);

return null;
}
public dynamic movecanvas(dynamic me,dynamic mv) {
dynamic sav = null;
dynamic eff = null;
dynamic q = null;
if ((mv.loch < 0)) {
sav = _movieScript.global_gleprops.matrix[1];
_movieScript.global_gleprops.matrix.deleteat(1);
_movieScript.global_gleprops.matrix.add(sav);
sav = _movieScript.global_gteprops.tlmatrix[1];
_movieScript.global_gteprops.tlmatrix.deleteat(1);
_movieScript.global_gteprops.tlmatrix.add(sav);
foreach (dynamic tmp_eff in _movieScript.global_geeprops.effects) {
eff = tmp_eff;
sav = eff.mtrx[1];
eff.mtrx.deleteat(1);
eff.mtrx.add(sav);
}
}
else if ((mv.loch > 0)) {
sav = _movieScript.global_gleprops.matrix[_movieScript.global_gloprops.size.loch];
_movieScript.global_gleprops.matrix.deleteat(_movieScript.global_gloprops.size.loch);
_movieScript.global_gleprops.matrix.addat(1,sav);
sav = _movieScript.global_gteprops.tlmatrix[_movieScript.global_gloprops.size.loch];
_movieScript.global_gteprops.tlmatrix.deleteat(_movieScript.global_gloprops.size.loch);
_movieScript.global_gteprops.tlmatrix.addat(1,sav);
foreach (dynamic tmp_eff in _movieScript.global_geeprops.effects) {
eff = tmp_eff;
sav = eff.mtrx[_movieScript.global_gloprops.size.loch];
eff.mtrx.deleteat(_movieScript.global_gloprops.size.loch);
eff.mtrx.addat(1,sav);
}
}
if ((mv.locv < 0)) {
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
sav = _movieScript.global_gleprops.matrix[q][1];
_movieScript.global_gleprops.matrix[q].deleteat(1);
_movieScript.global_gleprops.matrix[q].add(sav);
sav = _movieScript.global_gteprops.tlmatrix[q][1];
_movieScript.global_gteprops.tlmatrix[q].deleteat(1);
_movieScript.global_gteprops.tlmatrix[q].add(sav);
foreach (dynamic tmp_eff in _movieScript.global_geeprops.effects) {
eff = tmp_eff;
sav = eff.mtrx[q][1];
eff.mtrx[q].deleteat(1);
eff.mtrx[q].add(sav);
}
}
}
else if ((mv.locv > 0)) {
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
sav = _movieScript.global_gleprops.matrix[q][_movieScript.global_gloprops.size.locv];
_movieScript.global_gleprops.matrix[q].deleteat(_movieScript.global_gloprops.size.locv);
_movieScript.global_gleprops.matrix[q].addat(1,sav);
sav = _movieScript.global_gteprops.tlmatrix[q][_movieScript.global_gloprops.size.locv];
_movieScript.global_gteprops.tlmatrix[q].deleteat(_movieScript.global_gloprops.size.locv);
_movieScript.global_gteprops.tlmatrix[q].addat(1,sav);
foreach (dynamic tmp_eff in _movieScript.global_geeprops.effects) {
eff = tmp_eff;
sav = eff.mtrx[q][_movieScript.global_gloprops.size.locv];
eff.mtrx[q].deleteat(_movieScript.global_gloprops.size.locv);
eff.mtrx[q].addat(1,sav);
}
}
}
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),1);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),2);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),3);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16);

return null;
}
public dynamic updatetooltext(dynamic me) {
dynamic txt = null;
txt = @"";
switch (_movieScript.global_gleprops.toolmatrix[p.toolpos.locv][p.toolpos.loch]) {
case @"copyBack":
txt = @"Copy Backwards";
break;
case @"inverse":
txt = @"Inverse";
break;
case @"paintWall":
txt = @"Paint Walls";
break;
case @"paintAir":
txt = @"Paint Air";
break;
case @"slope":
txt = @"Make Slope";
break;
case @"floor":
txt = @"Make Floor";
break;
case @"squareWall":
txt = @"Rect Wall";
break;
case @"squareAir":
txt = @"Rect Air";
break;
case @"move":
txt = @"Move Level";
break;
case @"horBeam":
txt = @"Horizontal Beam";
break;
case @"verBeam":
txt = @"Vertical Beam";
break;
case @"glass":
txt = @"Glass Wall";
break;
case @"hive":
txt = @"Hive";
break;
case @"shortCutEntrance":
txt = @"Short Cut Entrance";
break;
case @"shortCut":
txt = @"Short Cut";
break;
case @"playerSpawn":
txt = @"Entrance";
break;
case @"lizardHole":
txt = @"Dragon Den";
break;
case @"rock":
txt = @"Place Rock";
break;
case @"spear":
txt = @"Place Spear";
break;
case @"mirrorToggle":
txt = @"Toggle Mirror";
break;
case @"setMirrorPoint":
txt = @"Move Mirror";
break;
case @"workLayer":
txt = LingoGlobal.concat(@"Work Layer: ",_global.@string(p.worklayer));
break;
case @"Crack":
txt = @"Crack Terrain";
break;
case @"flip":
txt = @"Clear All";
break;
case @"forbidbats":
txt = @"Forbid Fly Chains";
break;
case @"spawnfly":
txt = @"Spawn Fly";
break;
case @"bubble":
txt = @"Bubble";
break;
case @"Egg":
txt = @"Dragon Egg";
break;
case @"Waterfall":
txt = @"Waterfall";
break;
case @"WHAMH":
txt = @"Whack A Mole Hole";
break;
case @"wormGrass":
txt = @"Worm Grass";
break;
case @"scavengerHole":
txt = @"Scavenger Hole";
break;
}
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"editor",_global.@string(p.playernum)),@"tool")).text = txt;
_global.sprite(p.sprl[6]).member = _global.member(LingoGlobal.concat(@"icon",_movieScript.global_gleprops.toolmatrix[p.toolpos.locv][p.toolpos.loch]));

return null;
}
}
}
