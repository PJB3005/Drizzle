using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: soundEditor
//
public sealed class soundEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic c = null;
dynamic lstpos = null;
dynamic sav = null;
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= 2; tmp_c++) {
c = tmp_c;
if ((c == 1)) {
_global.sprite(((30+((q-1)*2))+c)).loc = LingoGlobal.point(((100+(_movieScript.global_gseprops.sounds[q].vol/new LingoDecimal(255)))*500),(((((q-1)*150)+c)*50)+100));
}
else if ((c == 2)) {
_global.sprite(((30+((q-1)*2))+c)).loc = LingoGlobal.point((((100+250)+(_movieScript.global_gseprops.sounds[q].pan/new LingoDecimal(100)))*250),(((((q-1)*150)+c)*50)+100));
}
}
}
_global.sprite(50).loc = (_global._mouse.mouseloc+LingoGlobal.point(20,20));
if (LingoGlobal.ToBool(_global._mouse.mousedown)) {
for (int tmp_q = 1; tmp_q <= _movieScript.global_gseprops.rects.count; tmp_q++) {
q = tmp_q;
if (LingoGlobal.ToBool(_global._mouse.mouseloc.inside(_movieScript.global_gseprops.rects[q][1]))) {
if ((_movieScript.global_gseprops.rects[q][2][2] == 1)) {
_movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].vol = (((_global._mouse.mouseloc.loch-100)/new LingoDecimal(500))*255);
_movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].vol = _movieScript.restrict(_movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].vol,0,255);
_global.sound(_movieScript.global_gseprops.rects[q][2][1]).volume = _movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].vol;
}
else {
_movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].pan = (((_global._mouse.mouseloc.loch-350)/new LingoDecimal(500))*255);
_movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].pan = _movieScript.restrict(_movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].pan,-100,100);
_global.sound(_movieScript.global_gseprops.rects[q][2][1]).pan = _movieScript.global_gseprops.sounds[_movieScript.global_gseprops.rects[q][2][1]].pan;
}
break;
}
}
}
if (LingoGlobal.ToBool(_global._mouse.mouseloc.inside(LingoGlobal.rect(760,50,860,1000)))) {
lstpos = (_global._mouse.mouseloc.locv/12);
lstpos = (lstpos-4);
if (((lstpos > 0) & (lstpos <= _movieScript.global_gseprops.ambientsounds.count))) {
_global.sprite(11).loc = LingoGlobal.point(750,((6+(lstpos+4))*12));
if (LingoGlobal.ToBool(_global._mouse.mousedown)) {
_movieScript.global_gseprops.pickedupsound = _movieScript.global_gseprops.ambientsounds[lstpos];
_global.member(@"buttonText").text = _movieScript.global_gseprops.ambientsounds[lstpos];
}
}
else {
_global.sprite(11).loc = LingoGlobal.point(-100,-100);
lstpos = ((lstpos-_movieScript.global_gseprops.ambientsounds.count)-5);
if (((lstpos > 0) & (lstpos <= _movieScript.global_gseprops.songs.count))) {
_global.sprite(11).loc = LingoGlobal.point(745,((6+(((lstpos+4)+_movieScript.global_gseprops.ambientsounds.count)+5))*12));
if (LingoGlobal.ToBool(_global._mouse.mousedown)) {
_movieScript.global_glevel.music = _movieScript.global_gseprops.songs[lstpos];
if ((_movieScript.global_glevel.music == @"none")) {
sav = _global.member(@"music");
_global.member(@"music").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"music\",@"overwrite"),@".mp3"));
sav.name = @"music";
_global.sound(5).pan = 0;
_global.sound(5).volume = 0;
_global.sound(5).stop();
}
else {
sav = _global.member(@"music");
_global.member(@"music").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"music\",_movieScript.global_glevel.music),@".mp3"));
sav.name = @"music";
_global.sound(5).pan = 0;
_global.sound(5).volume = 255;
_global.sound(5).play(new LingoPropertyList {[new LingoSymbol("member")] = sav,[new LingoSymbol("loopcount")] = 0});
}
_global.sprite(12).loc = LingoGlobal.point(750,((6+(((lstpos+4)+_movieScript.global_gseprops.ambientsounds.count)+5))*12));
}
}
}
}
else {
_global.sprite(11).loc = LingoGlobal.point(-100,-100);
}
_global.script(@"levelOverview").gotoeditor();
_global.go(_global.the_frame);

return null;
}
}
}
