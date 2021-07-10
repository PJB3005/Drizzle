using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: soundEditorStart
//
public sealed class soundEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic c = null;
dynamic sav = null;
dynamic filelist = null;
dynamic i = null;
dynamic n = null;
dynamic projects = null;
dynamic l = null;
dynamic txt = null;
_movieScript.global_gseprops = new LingoPropertyList {[new LingoSymbol("sounds")] = new LingoPropertyList {},[new LingoSymbol("ambientsounds")] = new LingoPropertyList {},[new LingoSymbol("songs")] = new LingoPropertyList {},[new LingoSymbol("rects")] = new LingoPropertyList {},[new LingoSymbol("pickedupsound")] = @"NONE"};
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
_movieScript.global_gseprops.sounds.add(new LingoPropertyList {[new LingoSymbol("mem")] = @"None",[new LingoSymbol("vol")] = 0,[new LingoSymbol("pan")] = 0});
}
for (int tmp_q = 1; tmp_q <= _movieScript.global_glevel.ambientsounds.count; tmp_q++) {
q = tmp_q;
_movieScript.global_gseprops.sounds[q] = _movieScript.global_glevel.ambientsounds[q];
}
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
_global.sprite((38+q)).loc = LingoGlobal.point(100,(((q-1)*150)+100));
_global.sprite((38+q)).visibility = 1;
for (int tmp_c = 1; tmp_c <= 2; tmp_c++) {
c = tmp_c;
_global.sprite(((21+((q-1)*2))+c)).rect = LingoGlobal.rect(100,(((((q-1)*150)+c)*50)+100),600,(((((q-1)*150)+c)*50)+102));
_movieScript.global_gseprops.rects.add(new LingoList(new dynamic[] { (LingoGlobal.rect(100,(((((q-1)*150)+c)*50)+100),600,(((((q-1)*150)+c)*50)+100))+LingoGlobal.rect(-50,-20,50,20)),new LingoList(new dynamic[] { q,c }) }));
}
if ((_movieScript.global_gseprops.sounds[q].mem == @"None")) {
_global.member(LingoGlobal.concat(@"AmbienceSound",q)).text = @"No Ambience Sound";
}
else {
_global.member(LingoGlobal.concat(@"AmbienceSound",q)).text = _movieScript.global_gseprops.sounds[q].mem;
sav = _global.member(LingoGlobal.concat(@"amb",q));
_global.member(LingoGlobal.concat(@"amb",q)).importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"music\ambience\",_movieScript.global_gseprops.sounds[q].mem),@".mp3"));
sav.name = LingoGlobal.concat(@"amb",q);
_global.sound(q).pan = _movieScript.global_gseprops.sounds[q].pan;
_global.sound(q).volume = _movieScript.global_gseprops.sounds[q].vol;
_global.sound(q).play(new LingoPropertyList {[new LingoSymbol("member")] = sav,[new LingoSymbol("loopcount")] = 0});
}
}
filelist = new LingoPropertyList {};
for (int tmp_i = 1; tmp_i <= 100; tmp_i++) {
i = tmp_i;
n = _global.getnthfilenameinfolder(LingoGlobal.concat(_global.the_moviePath,@"\music\ambience"),i);
if ((n == LingoGlobal.EMPTY)) {
break;
}
filelist.append(n);
}
projects = new LingoList(new dynamic[] { @"QUIET" });
foreach (dynamic tmp_l in filelist) {
l = tmp_l;
projects.add(LingoGlobal.chars(l,1,(l.length-4)));
}
txt = @"Ambient Sounds:";
txt += txt.ToString();
txt += txt.ToString();
foreach (dynamic tmp_q in projects) {
q = tmp_q;
_movieScript.global_gseprops.ambientsounds.add(q);
txt += txt.ToString();
txt += txt.ToString();
}
txt += txt.ToString();
txt += txt.ToString();
filelist = new LingoPropertyList {};
for (int tmp_i = 1; tmp_i <= 100; tmp_i++) {
i = tmp_i;
n = _global.getnthfilenameinfolder(LingoGlobal.concat(_global.the_moviePath,@"\music"),i);
if ((n == LingoGlobal.EMPTY)) {
break;
}
filelist.append(n);
}
projects = new LingoList(new dynamic[] { @"NONE" });
foreach (dynamic tmp_l in filelist) {
l = tmp_l;
projects.add(LingoGlobal.chars(l,1,(l.length-4)));
}
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
foreach (dynamic tmp_q in projects) {
q = tmp_q;
if (((q != @"ambi") & (q != @"overwrite"))) {
_movieScript.global_gseprops.songs.add(q);
txt += txt.ToString();
txt += txt.ToString();
}
}
_global.member(@"AMBsoundL").text = txt;
_global.member(@"buttonText").text = @"";
foreach (dynamic tmp_q in _movieScript.global_gseprops.songs) {
q = tmp_q;
if ((q == _movieScript.global_glevel.music)) {
_global.sprite(12).loc = LingoGlobal.point(750,((6+(((_movieScript.global_gseprops.songs.getpos(q)+4)+_movieScript.global_gseprops.ambientsounds.count)+5))*12));
}
}
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

return null;
}
}
}
