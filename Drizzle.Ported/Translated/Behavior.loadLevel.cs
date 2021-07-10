using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: loadLevel
//
public sealed class loadLevel : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic txt = null;
dynamic f = null;
dynamic q = null;
dynamic up = null;
dynamic dwn = null;
dynamic lft = null;
dynamic rgth = null;
txt = @"Use the up and down keys to select a project. Use enter to open it.";
txt += txt.ToString();
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
txt += txt.ToString();
}
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_q = _movieScript.global_ldprps.listscrollpos; tmp_q <= (_movieScript.global_ldprps.listscrollpos+_movieScript.global_ldprps.listshowtotal); tmp_q++) {
q = tmp_q;
if ((q > _movieScript.global_projects.count)) {
break;
}
else if ((q != _movieScript.global_ldprps.currproject)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
txt += txt.ToString();
}
_global.member(@"ProjectsL").text = txt;
up = _global._key.keypressed(126);
dwn = _global._key.keypressed(125);
lft = _global._key.keypressed(123);
rgth = _global._key.keypressed(124);
if ((LingoGlobal.ToBool(up) & (_movieScript.global_ldprps.lstup == 0))) {
_movieScript.global_ldprps.currproject = (_movieScript.global_ldprps.currproject-1);
if ((_movieScript.global_ldprps.currproject < 1)) {
_movieScript.global_ldprps.currproject = _movieScript.global_projects.count;
}
}
if ((LingoGlobal.ToBool(dwn) & (_movieScript.global_ldprps.lstdwn == 0))) {
_movieScript.global_ldprps.currproject = (_movieScript.global_ldprps.currproject+1);
if ((_movieScript.global_ldprps.currproject > _movieScript.global_projects.count)) {
_movieScript.global_ldprps.currproject = 1;
}
}
if ((_movieScript.global_ldprps.currproject < _movieScript.global_ldprps.listscrollpos)) {
_movieScript.global_ldprps.listscrollpos = _movieScript.global_ldprps.currproject;
}
else if ((_movieScript.global_ldprps.currproject > (_movieScript.global_ldprps.listscrollpos+_movieScript.global_ldprps.listshowtotal))) {
_movieScript.global_ldprps.listscrollpos = (_movieScript.global_ldprps.currproject-_movieScript.global_ldprps.listshowtotal);
}
if (((LingoGlobal.ToBool(rgth) & (_movieScript.global_ldprps.rgth == 0)) & (_movieScript.global_projects.count > 0))) {
if ((LingoGlobal.chars(_movieScript.global_projects[_movieScript.global_ldprps.currproject],1,1) == @"#")) {
me.loadsubfolder(_movieScript.global_projects[_movieScript.global_ldprps.currproject]);
}
}
else if ((LingoGlobal.ToBool(lft) & (_movieScript.global_ldprps.lft == 0))) {
if ((_movieScript.global_gloadpath.count > 0)) {
_movieScript.global_gloadpath.deleteat(_movieScript.global_gloadpath.count);
_global._movie.go(2);
}
}
_movieScript.global_ldprps.lstup = up;
_movieScript.global_ldprps.lstdwn = dwn;
_movieScript.global_ldprps.lft = lft;
_movieScript.global_ldprps.rgth = rgth;
if (LingoGlobal.ToBool(_global._key.keypressed(@"N"))) {
_movieScript.global_gloadedname = @"New Project";
_global.member(@"level Name").text = @"New Project";
_global._movie.go(7);
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(36)) & (_movieScript.global_projects.count > 0))) {
if ((LingoGlobal.chars(_movieScript.global_projects[_movieScript.global_ldprps.currproject],1,1) != @"#")) {
me.loadlevel(_movieScript.global_projects[_movieScript.global_ldprps.currproject]);
_global._movie.go(7);
}
}
_global.go(_global.the_frame);

return null;
}
public dynamic loadsubfolder(dynamic me,dynamic fldrname) {
_movieScript.global_gloadpath.add(LingoGlobal.chars(fldrname,2,fldrname.length));
_global._movie.go(2);

return null;
}
public dynamic loadlevel(dynamic me,dynamic lvlname,dynamic fullpath) {
dynamic pth = null;
dynamic f = null;
dynamic objfileio = null;
dynamic lastbackslash = null;
dynamic q = null;
dynamic l2 = null;
dynamic sv2 = null;
dynamic l1 = null;
dynamic sav = null;
dynamic wantedrect = null;
dynamic img = null;
if (LingoGlobal.ToBool(fullpath)) {
pth = @"";
}
else {
pth = LingoGlobal.concat(_global.the_moviePath,@"LevelEditorProjects\");
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
pth = LingoGlobal.concat(LingoGlobal.concat(pth,f),@"\");
}
}
objfileio = _global.@new(_global.xtra(@"fileio"));
objfileio.openfile(LingoGlobal.concat(LingoGlobal.concat(pth,lvlname),@".txt"),0);
if ((fullpath == 1)) {
_movieScript.global_gloadedname = @"";
lastbackslash = 0;
for (int tmp_q = 1; tmp_q <= lvlname.length; tmp_q++) {
q = tmp_q;
if ((LingoGlobal.chars(lvlname,q,q) == @"\")) {
lastbackslash = q;
}
}
_movieScript.global_gloadedname = LingoGlobal.chars(lvlname,(lastbackslash+1),lvlname.length);
_global.put(_movieScript.global_gloadedname);
}
else {
_movieScript.global_gloadedname = lvlname;
}
_global.member(@"level Name").text = lvlname;
l2 = objfileio.readfile();
objfileio.closefile();
sv2 = _movieScript.global_gloprops.duplicate();
l1 = _global.value(l2.line[1]);
_movieScript.global_gleprops.matrix = l1;
l1 = _global.value(l2.line[2]);
_movieScript.global_gteprops = l1;
l1 = _global.value(l2.line[3]);
_movieScript.global_geeprops = l1;
l1 = _global.value(l2.line[4]);
_movieScript.global_glighteprops = l1;
l1 = _global.value(l2.line[5]);
_movieScript.global_glevel = l1;
l1 = _global.value(l2.line[6]);
_movieScript.global_gloprops = l1;
if ((_movieScript.global_gloprops.findpos(new LingoSymbol("light")) == LingoGlobal.VOID)) {
_movieScript.global_gloprops.addprop(new LingoSymbol("light"),1);
}
if ((_movieScript.global_gteprops.findpos(new LingoSymbol("specialedit")) == LingoGlobal.VOID)) {
_movieScript.global_gteprops.addprop(new LingoSymbol("specialedit"),0);
}
if ((_movieScript.global_gloprops.findpos(new LingoSymbol("size")) == LingoGlobal.VOID)) {
_movieScript.global_gloprops.addprop(new LingoSymbol("size"),LingoGlobal.point(52,40));
}
if ((_movieScript.global_gloprops.findpos(new LingoSymbol("extratiles")) == LingoGlobal.VOID)) {
_movieScript.global_gloprops.addprop(new LingoSymbol("extratiles"),new LingoList(new dynamic[] { 1,1,1,3 }));
}
_movieScript.global_gloprops.pals = new LingoList(new dynamic[] { new LingoPropertyList {[new LingoSymbol("detcol")] = _global.color(255,0,0)} });
if ((_global.value(l2.line[7]) == LingoGlobal.VOID)) {
_movieScript.global_gcameraprops.cameras = new LingoList(new dynamic[] { (LingoGlobal.point((_movieScript.global_gloprops.size.loch*10),(_movieScript.global_gloprops.size.locv*10))-LingoGlobal.point((35*20),(20*20))) });
}
else {
_movieScript.global_gcameraprops = _global.value(l2.line[7]);
}
if (((_global.value(l2.line[8]) == LingoGlobal.VOID) | (_global.value(l2.line[8]).findpos(new LingoSymbol("waterlevel")) == LingoGlobal.VOID))) {
_movieScript.resetgenveditorprops();
}
else {
_movieScript.global_genveditorprops = _global.value(l2.line[8]);
}
if (((_global.value(l2.line[9]) == LingoGlobal.VOID) | (LingoGlobal.chars(l2.line[9],1,6) != @"[#prop"))) {
_movieScript.resetpropeditorprops();
}
else {
_movieScript.global_gpeprops = _global.value(l2.line[9]);
}
if ((_movieScript.global_gpeprops.findpos(new LingoSymbol("color")) == LingoGlobal.VOID)) {
_movieScript.global_gpeprops.addprop(new LingoSymbol("color"),0);
}
if ((_movieScript.global_gpeprops.findpos(new LingoSymbol("props")) == LingoGlobal.VOID)) {
_movieScript.global_gpeprops.addprop(new LingoSymbol("props"),new LingoPropertyList {});
}
_movieScript.global_gteprops.tmpos = LingoGlobal.point(2,1);
me.versionfix();
_global.member(@"lightImage").image = _global.image(((_movieScript.global_gloprops.size.loch*20)+300),((_movieScript.global_gloprops.size.locv*20)+300),1);
sav = _global.member(@"lightImage");
_global.member(@"lightImage").importfileinto(LingoGlobal.concat(LingoGlobal.concat(pth,lvlname),@".png"));
sav.name = @"lightImage";
if ((_global.member(@"lightImage").image.rect != LingoGlobal.rect(0,0,((_movieScript.global_gloprops.size.loch*20)+300),((_movieScript.global_gloprops.size.locv*20)+300)))) {
wantedrect = LingoGlobal.rect(0,0,((_movieScript.global_gloprops.size.loch*20)+300),((_movieScript.global_gloprops.size.locv*20)+300));
img = _global.image(wantedrect.width,wantedrect.height,1);
img.copypixels(_global.member(@"lightImage").image,(LingoGlobal.rect((wantedrect.width/2),(wantedrect.height/2),(wantedrect.width/2),(wantedrect.height/2))+LingoGlobal.rect((-_global.member(@"lightImage").rect.width/2),(-_global.member(@"lightImage").image.rect.height/2),(_global.member(@"lightImage").image.rect.width/2),(_global.member(@"lightImage").image.rect.height/2))),_global.member(@"lightImage").image.rect);
_global.member(@"lightImage").image = img;
_global.put(@"Adapted light rect");
}
_movieScript.global_glastdrawwasfullandmini = 0;
_global.put(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(pth,@"\"),lvlname),@".png"));

return null;
}
public dynamic versionfix(dynamic me) {
dynamic q = null;
dynamic c = null;
dynamic d = null;
dynamic huntnew = null;
dynamic pnt = null;
dynamic cat = null;
dynamic tl = null;
dynamic correctreference = null;
dynamic a = null;
dynamic b = null;
dynamic lz = null;
dynamic ef = null;
dynamic sd = null;
dynamic op = null;
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
for (int tmp_d = 1; tmp_d <= 3; tmp_d++) {
d = tmp_d;
if ((_movieScript.global_gteprops.tlmatrix[q][c][d].tp == @"tileHead")) {
huntnew = @"";
if ((_movieScript.global_gteprops.tlmatrix[q][c][d].data.count < 2)) {
huntnew = _movieScript.global_gteprops.tlmatrix[q][c][d].data.nm;
}
else {
pnt = _movieScript.global_gteprops.tlmatrix[q][c][d].data[1];
if ((_movieScript.global_gtiles.count >= pnt.loch)) {
if ((_movieScript.global_gtiles[pnt.loch].tls.count >= pnt.locv)) {
if ((_movieScript.global_gtiles[pnt.loch].tls[pnt.locv].nm != _movieScript.global_gteprops.tlmatrix[q][c][d].data[2])) {
huntnew = _movieScript.global_gteprops.tlmatrix[q][c][d].data[2];
}
}
else {
huntnew = _movieScript.global_gteprops.tlmatrix[q][c][d].data[2];
}
}
else {
huntnew = _movieScript.global_gteprops.tlmatrix[q][c][d].data[2];
}
}
if ((huntnew != @"")) {
_movieScript.global_gteprops.tlmatrix[q][c][d].data = new LingoList(new dynamic[] { LingoGlobal.point(2,1),@"NOT FOUND" });
for (int tmp_cat = 1; tmp_cat <= _movieScript.global_gtiles.count; tmp_cat++) {
cat = tmp_cat;
for (int tmp_tl = 1; tmp_tl <= _movieScript.global_gtiles[cat].tls.count; tmp_tl++) {
tl = tmp_tl;
if ((_movieScript.global_gtiles[cat].tls[tl].nm == huntnew)) {
_movieScript.global_gteprops.tlmatrix[q][c][d].data = new LingoList(new dynamic[] { LingoGlobal.point(cat,tl),huntnew });
}
}
}
}
}
}
}
}
for (int tmp_q = 1; tmp_q <= _movieScript.global_gleprops.toolmatrix.count; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gleprops.toolmatrix[1].count; tmp_c++) {
c = tmp_c;
if ((_movieScript.global_gleprops.toolmatrix[q][c] == @"save")) {
_movieScript.global_gleprops.toolmatrix[q][c] = @"";
}
}
}
for (int tmp_q = 1; tmp_q <= _movieScript.global_gpeprops.props.count; tmp_q++) {
q = tmp_q;
correctreference = LingoGlobal.TRUE;
if ((_movieScript.global_gpeprops.props[q][3].loch > _movieScript.global_gprops.count)) {
correctreference = LingoGlobal.FALSE;
}
else if ((_movieScript.global_gpeprops.props[q][3].locv > _movieScript.global_gprops[_movieScript.global_gpeprops.props[q][3].loch].prps.count)) {
correctreference = LingoGlobal.FALSE;
}
else if ((_movieScript.global_gprops[_movieScript.global_gpeprops.props[q][3].loch].prps[_movieScript.global_gpeprops.props[q][3].locv].nm != _movieScript.global_gpeprops.props[q][2])) {
correctreference = LingoGlobal.FALSE;
}
if ((correctreference == LingoGlobal.FALSE)) {
for (int tmp_a = 1; tmp_a <= _movieScript.global_gprops.count; tmp_a++) {
a = tmp_a;
for (int tmp_b = 1; tmp_b <= _movieScript.global_gprops[a].prps.count; tmp_b++) {
b = tmp_b;
if ((_movieScript.global_gprops[a].prps[b].nm == _movieScript.global_gpeprops.props[q][2])) {
correctreference = LingoGlobal.TRUE;
_movieScript.global_gpeprops.props[q][3] = LingoGlobal.point(a,b);
break;
}
}
if ((correctreference == LingoGlobal.TRUE)) {
break;
}
}
}
if ((_movieScript.global_gpeprops.props[q].count == 4)) {
_movieScript.global_gpeprops.props[q].add(new LingoPropertyList {[new LingoSymbol("settings")] = _movieScript.global_gprops[_movieScript.global_gpeprops.props[q][3].loch].prps[_movieScript.global_gpeprops.props[q][3].locv].settings.duplicate()});
}
if ((correctreference == LingoGlobal.FALSE)) {
_movieScript.global_gpeprops.props[q][3] = LingoGlobal.point(1,1);
}
}
foreach (dynamic tmp_lz in _movieScript.global_glevel.lizards) {
lz = tmp_lz;
if ((lz.count == 3)) {
lz.add(1);
}
}
if ((_movieScript.global_glevel.findpos(new LingoSymbol("waterdrips")) == LingoGlobal.VOID)) {
_movieScript.global_glevel.addprop(new LingoSymbol("waterdrips"),1);
}
if ((_movieScript.global_glevel.findpos(new LingoSymbol("tags")) == LingoGlobal.VOID)) {
_movieScript.global_glevel.addprop(new LingoSymbol("tags"),new LingoPropertyList {});
}
if ((_movieScript.global_glevel.findpos(new LingoSymbol("lightdynamic")) != LingoGlobal.VOID)) {
_movieScript.global_glevel.deleteprop(new LingoSymbol("lightdynamic"));
_movieScript.global_glevel.addprop(new LingoSymbol("lighttype"),@"Static");
}
if ((_movieScript.global_glevel.findpos(new LingoSymbol("lightblend")) != LingoGlobal.VOID)) {
_movieScript.global_glevel.deleteprop(new LingoSymbol("lightblend"));
}
if ((_movieScript.global_gloprops.findpos(new LingoSymbol("tileseed")) == LingoGlobal.VOID)) {
_movieScript.global_gloprops.addprop(new LingoSymbol("tileseed"),_global.random(400));
}
if ((_movieScript.global_gloprops.findpos(new LingoSymbol("colglows")) == LingoGlobal.VOID)) {
_movieScript.global_gloprops.addprop(new LingoSymbol("colglows"),new LingoList(new dynamic[] { 0,0 }));
}
if ((_movieScript.global_gleprops.findpos(new LingoSymbol("campos")) == LingoGlobal.VOID)) {
_movieScript.global_gleprops.addprop(new LingoSymbol("campos"),LingoGlobal.point(0,0));
}
if ((_movieScript.global_gcameraprops.findpos(new LingoSymbol("quads")) == LingoGlobal.VOID)) {
_movieScript.global_gcameraprops.addprop(new LingoSymbol("quads"),new LingoPropertyList {});
for (int tmp_q = 1; tmp_q <= _movieScript.global_gcameraprops.cameras.count; tmp_q++) {
q = tmp_q;
_movieScript.global_gcameraprops.quads.add(new LingoList(new dynamic[] { new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }) }));
}
}
if ((_movieScript.global_glevel.findpos(new LingoSymbol("music")) == LingoGlobal.VOID)) {
_movieScript.global_glevel.addprop(new LingoSymbol("music"),@"NONE");
}
foreach (dynamic tmp_ef in _movieScript.global_geeprops.effects) {
ef = tmp_ef;
sd = 0;
foreach (dynamic tmp_op in ef.options) {
op = tmp_op;
if ((op[1] == @"seed")) {
sd = 1;
break;
}
}
if ((sd == 0)) {
ef.options.add(new LingoList(new dynamic[] { @"Seed",new LingoPropertyList {},_global.random(500) }));
}
if ((ef.findpos(new LingoSymbol("crossscreen")) == LingoGlobal.VOID)) {
ef.addprop(new LingoSymbol("crossscreen"),0);
if ((new LingoList(new dynamic[] { @"Hang Roots",@"Growers",@"Wires",@"Chains" }).getpos(ef.nm) > 0)) {
ef.crossscreen = 1;
}
}
}

return null;
}
}
}
