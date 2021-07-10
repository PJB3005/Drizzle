using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: levelOverview
//
public sealed class levelOverview : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic lc = null;
_movieScript.global_gloprops.lastmouse = _movieScript.global_gloprops.mouse;
_movieScript.global_gloprops.mouse = _global._mouse.mousedown;
if (LingoGlobal.ToBool((_movieScript.global_gloprops.mouse*LingoGlobal.op_eq(_movieScript.global_gloprops.lastmouse,0)))) {
_movieScript.global_gloprops.mouseclick = 1;
}
if ((_movieScript.global_gloprops.mouse == 0)) {
_movieScript.global_gloprops.mouseclick = 0;
}
lc = (_global._mouse.mouseloc+LingoGlobal.point(30,30));
if ((lc.loch > (1024-300))) {
lc.loch = (1024-300);
}
_global.sprite(50).loc = lc;
me.gotoeditor();
_global.go(_global.the_frame);

return null;
}
public dynamic buttonclicked(dynamic me,dynamic bttn) {
dynamic l1 = null;
dynamic curr = null;
dynamic q = null;
dynamic l = null;
dynamic a = null;
dynamic sav = null;
dynamic cols = null;
dynamic rows = null;
dynamic maprect = null;
dynamic cpos = null;
switch (bttn) {
case @"button geometry editor":
_global._movie.go(15);
break;
case @"button tile editor":
_global._movie.go(25);
break;
case @"button effects editor":
_global._movie.go(34);
break;
case @"button light editor":
_global._movie.go(38);
break;
case @"button render level":
_movieScript.global_gviewrender = 1;
_global._movie.go(43);
break;
case @"button test render":
_movieScript.newmakelevel(_movieScript.global_gloadedname);
_global._movie.go(8);
break;
case @"button save project":
_movieScript.global_levelname = _movieScript.global_gloadedname;
_global.member(@"projectNameInput").text = _movieScript.global_gloadedname;
_global._movie.go(11);
break;
case @"button load project":
_global._movie.go(2);
break;
case @"button previous palette":
_movieScript.global_gloprops.pal = (_movieScript.global_gloprops.pal-1);
if ((_movieScript.global_gloprops.pal < 1)) {
_movieScript.global_gloprops.pal = _movieScript.global_gloprops.pals.count;
}
_global.sprite(21).member = _global.member(LingoGlobal.concat(@"libPal",_global.@string(_movieScript.global_gloprops.pal)));
_global.member(@"palName").text = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].name;
_movieScript.global_gbluroptions = new LingoPropertyList {[new LingoSymbol("blurlight")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].blurlight,[new LingoSymbol("blursky")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].blursky};
break;
case @"button next palette":
_movieScript.global_gloprops.pal = (_movieScript.global_gloprops.pal+1);
if ((_movieScript.global_gloprops.pal > _movieScript.global_gloprops.pals.count)) {
_movieScript.global_gloprops.pal = 1;
}
_global.sprite(21).member = _global.member(LingoGlobal.concat(@"libPal",_global.@string(_movieScript.global_gloprops.pal)));
_global.member(@"palName").text = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].name;
_movieScript.global_gbluroptions = new LingoPropertyList {[new LingoSymbol("blurlight")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].blurlight,[new LingoSymbol("blursky")] = _movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].blursky};
break;
case @"button previous EC1":
_movieScript.global_gloprops.ecol1 = (_movieScript.global_gloprops.ecol1-1);
if ((_movieScript.global_gloprops.ecol1 < 1)) {
_movieScript.global_gloprops.ecol1 = _movieScript.global_gloprops.totecols;
}
_global.sprite(22).member = _global.member(LingoGlobal.concat(@"ecol",_global.@string(_movieScript.global_gloprops.ecol1)));
break;
case @"button next EC1":
_movieScript.global_gloprops.ecol1 = (_movieScript.global_gloprops.ecol1+1);
if ((_movieScript.global_gloprops.ecol1 > _movieScript.global_gloprops.totecols)) {
_movieScript.global_gloprops.ecol1 = 1;
}
_global.sprite(22).member = _global.member(LingoGlobal.concat(@"ecol",_global.@string(_movieScript.global_gloprops.ecol1)));
break;
case @"button previous EC2":
_movieScript.global_gloprops.ecol2 = (_movieScript.global_gloprops.ecol2-1);
if ((_movieScript.global_gloprops.ecol2 < 1)) {
_movieScript.global_gloprops.ecol2 = _movieScript.global_gloprops.totecols;
}
_global.sprite(23).member = _global.member(LingoGlobal.concat(@"ecol",_global.@string(_movieScript.global_gloprops.ecol2)));
break;
case @"button next EC2":
_movieScript.global_gloprops.ecol2 = (_movieScript.global_gloprops.ecol2+1);
if ((_movieScript.global_gloprops.ecol2 > _movieScript.global_gloprops.totecols)) {
_movieScript.global_gloprops.ecol2 = 1;
}
_global.sprite(23).member = _global.member(LingoGlobal.concat(@"ecol",_global.@string(_movieScript.global_gloprops.ecol2)));
break;
case @"button more flies":
_movieScript.global_geditlizard[2] = _movieScript.restrict((_movieScript.global_geditlizard[2]+1),0,40);
_global.member(@"addLizardFlies").text = _global.@string(_movieScript.global_geditlizard[2]);
break;
case @"button less flies":
_movieScript.global_geditlizard[2] = _movieScript.restrict((_movieScript.global_geditlizard[2]-1),0,40);
_global.member(@"addLizardFlies").text = _global.@string(_movieScript.global_geditlizard[2]);
break;
case @"button more time":
_movieScript.global_geditlizard[3] = _movieScript.restrict((_movieScript.global_geditlizard[3]+1),0,4000);
_global.member(@"addLizardTime").text = _global.@string(_movieScript.global_geditlizard[3]);
break;
case @"button less time":
_movieScript.global_geditlizard[3] = _movieScript.restrict((_movieScript.global_geditlizard[3]-1),0,4000);
_global.member(@"addLizardTime").text = _global.@string(_movieScript.global_geditlizard[3]);
break;
case @"button super more time":
_movieScript.global_geditlizard[3] = _movieScript.restrict((_movieScript.global_geditlizard[3]+100),0,4000);
_global.member(@"addLizardTime").text = _global.@string(_movieScript.global_geditlizard[3]);
break;
case @"button super less time":
_movieScript.global_geditlizard[3] = _movieScript.restrict((_movieScript.global_geditlizard[3]-100),0,4000);
_global.member(@"addLizardTime").text = _global.@string(_movieScript.global_geditlizard[3]);
break;
case @"button lizard hole":
me.nexthole();
break;
case @"button delete lizard":
if ((_movieScript.global_glevel.lizards.count > 0)) {
_movieScript.global_glevel.lizards.deleteat(_movieScript.global_glevel.lizards.count);
me.updatelizardslist();
}
break;
case @"button add lizard":
if ((_movieScript.global_geditlizard[4] > 0)) {
if ((_movieScript.global_glevel.lizards.count < 4)) {
_movieScript.global_glevel.lizards.add(_movieScript.global_geditlizard.duplicate());
me.updatelizardslist();
}
}
if ((_movieScript.global_geditlizard[1] != @"yellow")) {
me.nexthole();
}
break;
case @"button lizard color":
l1 = new LingoList(new dynamic[] { @"pink",@"green",@"blue",@"white",@"red",@"yellow" });
curr = 1;
for (int tmp_q = 1; tmp_q <= l1.count; tmp_q++) {
q = tmp_q;
if ((_movieScript.global_geditlizard[1] == l1[q])) {
curr = q;
break;
}
}
curr = (curr+1);
if ((curr > l1.count)) {
curr = 1;
}
_movieScript.global_geditlizard[1] = l1[curr];
_global.sprite(43).color = new LingoList(new dynamic[] { _global.color(255,0,255),_global.color(0,255,0),_global.color(0,100,255),_global.color(255,255,255),_global.color(255,0,0),_global.color(255,200,0) })[curr];
break;
case @"button standard medium":
_movieScript.global_glevel.defaultterrain = (1-_movieScript.global_glevel.defaultterrain);
_global.sprite(2).loc = (LingoGlobal.point(312,312)+LingoGlobal.point(((-1000+1000)*_movieScript.global_glevel.defaultterrain),0));
break;
case @"button light type":
_movieScript.global_gloprops.light = (1-_movieScript.global_gloprops.light);
break;
case @"button next color glow 1":
_movieScript.global_gloprops.colglows[1] = (_movieScript.global_gloprops.colglows[1]+1);
if ((_movieScript.global_gloprops.colglows[1] > 2)) {
_movieScript.global_gloprops.colglows[1] = 0;
}
l = new LingoList(new dynamic[] { @"Dull",@"Reflective",@"Superflourescent" });
_global.member(@"color glow effects").text = LingoGlobal.concat_space(LingoGlobal.concat_space(l[(_movieScript.global_gloprops.colglows[1]+1)],LingoGlobal.RETURN),l[(_movieScript.global_gloprops.colglows[2]+1)]);
break;
case @"button next color glow 2":
_movieScript.global_gloprops.colglows[2] = (_movieScript.global_gloprops.colglows[2]+1);
if ((_movieScript.global_gloprops.colglows[2] > 2)) {
_movieScript.global_gloprops.colglows[2] = 0;
}
l = new LingoList(new dynamic[] { @"Dull",@"Reflective",@"Superflourescent" });
_global.member(@"color glow effects").text = LingoGlobal.concat_space(LingoGlobal.concat_space(l[(_movieScript.global_gloprops.colglows[1]+1)],LingoGlobal.RETURN),l[(_movieScript.global_gloprops.colglows[2]+1)]);
break;
case @"button sound editor":
_global._movie.go(18);
break;
case @"button mass render":
_movieScript.global_massrenderselectl = new LingoPropertyList {};
_global._movie.go(4);
break;
case @"button prop editor":
_global._movie.go(23);
break;
case @"button level size":
_global._movie.go(19);
_global.member(@"widthInput").text = _movieScript.global_gloprops.size.loch;
_global.member(@"heightInput").text = _movieScript.global_gloprops.size.locv;
_movieScript.global_newsize = new LingoList(new dynamic[] { _movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv,0,0 });
_movieScript.global_extrabuffertiles = _movieScript.global_gloprops.extratiles.duplicate();
_global.member(@"extraTilesLeft").text = _movieScript.global_extrabuffertiles[1];
_global.member(@"extraTilesTop").text = _movieScript.global_extrabuffertiles[2];
_global.member(@"extraTilesRight").text = _movieScript.global_extrabuffertiles[3];
_global.member(@"extraTilesBottom").text = _movieScript.global_extrabuffertiles[4];
_global.member(@"addTilesTop").text = @"0";
_global.member(@"addTilesLeft").text = @"0";
break;
case @"button cameras":
_global._movie.go(32);
break;
case @"button environment editor":
_global._movie.go(30);
break;
case @"button update preview":
for (int tmp_a = 1; tmp_a <= 3; tmp_a++) {
a = tmp_a;
_movieScript.minilvleditdraw(a);
}
sav = _movieScript.global_gleprops.campos;
_movieScript.global_gleprops.campos = LingoGlobal.point(0,0);
cols = _movieScript.global_gloprops.size.loch;
rows = _movieScript.global_gloprops.size.locv;
_global.member(@"levelEditImageShortCuts").image = _global.image((cols*5),(rows*5),1);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,cols,rows),5,1);
_movieScript.global_gleprops.campos = sav;
break;
case @"button prio cam":
_movieScript.global_gpriocam = (_movieScript.global_gpriocam+1);
if ((_movieScript.global_gpriocam > _movieScript.global_gcameraprops.cameras.count)) {
_movieScript.global_gpriocam = 0;
}
if ((_movieScript.global_gpriocam == 0)) {
_global.member(@"PrioCamText").text = @"NONE";
_global.sprite(22).rect = LingoGlobal.rect(-100,-100,-100,-100);
}
else {
_global.member(@"PrioCamText").text = LingoGlobal.concat(LingoGlobal.concat(@"Will render camera ",_movieScript.global_gpriocam),@" first");
maprect = _global.sprite(6).rect;
cpos = (_movieScript.global_gcameraprops.cameras[_movieScript.global_gpriocam]+LingoGlobal.point(new LingoDecimal(0.001),new LingoDecimal(0.001)));
_global.sprite(22).rect = LingoGlobal.rect(_movieScript.lerp(maprect.left,maprect.right,(cpos.loch/(_movieScript.global_gloprops.size.loch*20))),_movieScript.lerp(maprect.top,maprect.bottom,(cpos.locv/(_movieScript.global_gloprops.size.locv*20))),_movieScript.lerp(maprect.left,maprect.right,((cpos.loch+1024)/(_movieScript.global_gloprops.size.loch*20))),_movieScript.lerp(maprect.top,maprect.bottom,((cpos.locv+768)/(_movieScript.global_gloprops.size.locv*20))));
}
break;
}

return null;
}
public dynamic gotoeditor(dynamic me) {
dynamic gofrm = null;
dynamic q = null;
gofrm = 0;
if (LingoGlobal.ToBool(_global._key.keypressed(@"1"))) {
gofrm = 9;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"2"))) {
gofrm = 15;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"3"))) {
gofrm = 25;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"4"))) {
gofrm = 33;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"5"))) {
gofrm = 38;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"6"))) {
gofrm = 18;
}
if ((gofrm != 0)) {
for (int tmp_q = 1; tmp_q <= 5; tmp_q++) {
q = tmp_q;
_global.sound(q).stop();
}
if ((_movieScript.global_gseprops.sounds != LingoGlobal.VOID)) {
_movieScript.global_glevel.ambientsounds = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if (((_movieScript.global_gseprops.sounds[q].mem != @"none") & (_movieScript.global_gseprops.sounds[q].vol > 0))) {
_movieScript.global_glevel.ambientsounds.add(_movieScript.global_gseprops.sounds[q].duplicate());
_movieScript.global_gseprops.sounds[q].mem = @"None";
}
}
_movieScript.global_gseprops.sounds = LingoGlobal.VOID;
}
for (int tmp_q = 1; tmp_q <= 22; tmp_q++) {
q = tmp_q;
_global.sprite(q).visibility = 1;
}
for (int tmp_q = 800; tmp_q <= 820; tmp_q++) {
q = tmp_q;
_global.sprite(q).visibility = LingoGlobal.op_eq(gofrm,15);
}
_global._movie.go(gofrm);
}

return null;
}
public dynamic updatelizardslist(dynamic me) {
dynamic l1 = null;
dynamic l2 = null;
dynamic lz = null;
dynamic q = null;
dynamic c = null;
dynamic pnt1 = null;
dynamic pnt2 = null;
l1 = new LingoList(new dynamic[] { @"pink",@"green",@"blue",@"white",@"red",@"yellow" });
l2 = new LingoList(new dynamic[] { _global.color(255,0,255),_global.color(0,255,0),_global.color(0,100,255),_global.color(255,255,255),_global.color(255,0,0),_global.color(255,200,0) });
lz = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if ((_movieScript.global_gleprops.matrix[q][c][1][2].getpos(7) > 0)) {
lz.add(LingoGlobal.point(q,c));
}
}
}
if ((lz != new LingoPropertyList {})) {
for (int tmp_q = 1; tmp_q <= _movieScript.global_glevel.lizards.count; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"lizard",q),@"text")).text = LingoGlobal.concat_space(LingoGlobal.concat_space(LingoGlobal.concat_space(LingoGlobal.concat_space(_movieScript.global_glevel.lizards[q][1],@"- Flies:"),_movieScript.global_glevel.lizards[q][2]),@"- Time:"),_movieScript.global_glevel.lizards[q][3]);
_global.sprite((51+q)).color = l2[l1.getpos(_movieScript.global_glevel.lizards[q][1])];
_global.sprite((55+q)).color = l2[l1.getpos(_movieScript.global_glevel.lizards[q][1])];
if ((_movieScript.global_glevel.lizards[q][4] > lz.count)) {
_movieScript.global_glevel.lizards[q][4] = lz.count;
}
pnt1 = (((lz[_movieScript.global_glevel.lizards[q][4]]*10)+LingoGlobal.point(52,112))+LingoGlobal.point(-5,-5));
pnt2 = LingoGlobal.point(_global.sprite((51+q)).rect.left,((_global.sprite((51+q)).rect.top+_global.sprite((51+q)).rect.height)*new LingoDecimal(0.5)));
_global.sprite((55+q)).rect = LingoGlobal.rect(pnt1,pnt2);
_global.sprite((55+q)).member = _global.member(LingoGlobal.concat(@"line",(1+LingoGlobal.op_gt(pnt1.locv,pnt2.locv))));
}
for (int tmp_q = (_movieScript.global_glevel.lizards.count+1); tmp_q <= 4; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"lizard",q),@"text")).text = @"";
_global.sprite((55+q)).rect = LingoGlobal.rect(-100,-100,-100,-100);
}
}
else {
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"lizard",q),@"text")).text = @"";
_global.sprite((55+q)).rect = LingoGlobal.rect(-100,-100,-100,-100);
}
}

return null;
}
public dynamic nexthole(dynamic me) {
dynamic lz = null;
dynamic q = null;
dynamic c = null;
dynamic pnt = null;
lz = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
if ((_movieScript.global_gleprops.matrix[q][c][1][2].getpos(7) > 0)) {
lz.add(LingoGlobal.point(q,c));
}
}
}
if ((lz != new LingoPropertyList {})) {
_movieScript.global_geditlizard[4] = (_movieScript.global_geditlizard[4]+1);
if ((_movieScript.global_geditlizard[4] > lz.count)) {
_movieScript.global_geditlizard[4] = 1;
}
pnt = (((lz[_movieScript.global_geditlizard[4]]*10)+LingoGlobal.point(52,112))+LingoGlobal.point(-5,-5));
_global.sprite(60).rect = (LingoGlobal.rect(pnt,pnt)+LingoGlobal.rect(-5,-5,5,5));
}
else {
_global.sprite(60).rect = LingoGlobal.rect(-5,-5,-5,-5);
}

return null;
}
}
}
