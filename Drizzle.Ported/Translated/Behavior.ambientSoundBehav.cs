using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: ambientSoundBehav
//
public sealed class ambientSoundBehav : LingoBehaviorScript {
public dynamic mousewithin(dynamic me) {
dynamic nm = null;
dynamic sav = null;
if ((_movieScript.global_gseprops.pickedupsound != @"NONE")) {
_global.sprite(me.spritenum).visibility = (_global.random(2)-1);
nm = (me.spritenum-38);
if (LingoGlobal.ToBool(_global._mouse.mousedown)) {
if ((_movieScript.global_gseprops.pickedupsound == @"QUIET")) {
_movieScript.global_gseprops.sounds[nm].mem = @"None";
sav = _global.member(LingoGlobal.concat(@"amb",nm));
_global.member(LingoGlobal.concat(@"AmbienceSound",nm)).text = @"No Ambience Sound";
_global.member(LingoGlobal.concat(@"amb",nm)).importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"music\",@"overwrite"),@".mp3"));
sav.name = LingoGlobal.concat(@"amb",nm);
_global.sound(nm).pan = _movieScript.global_gseprops.sounds[nm].pan;
_global.sound(nm).volume = _movieScript.global_gseprops.sounds[nm].vol;
_global.sound(nm).stop();
}
else {
_movieScript.global_gseprops.sounds[nm].mem = _movieScript.global_gseprops.pickedupsound;
_global.member(LingoGlobal.concat(@"AmbienceSound",nm)).text = _movieScript.global_gseprops.sounds[nm].mem;
sav = _global.member(LingoGlobal.concat(@"amb",nm));
_global.member(LingoGlobal.concat(@"amb",nm)).importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"music\ambience\",_movieScript.global_gseprops.sounds[nm].mem),@".mp3"));
sav.name = LingoGlobal.concat(@"amb",nm);
_global.sound(nm).pan = _movieScript.global_gseprops.sounds[nm].pan;
_global.sound(nm).volume = _movieScript.global_gseprops.sounds[nm].vol;
_global.sound(nm).play(new LingoPropertyList {[new LingoSymbol("member")] = sav,[new LingoSymbol("loopcount")] = 0});
}
_movieScript.global_gseprops.pickedupsound = @"NONE";
_global.member(@"ButtonText").text = @"";
}
}

return null;
}
public dynamic mouseleave(dynamic me) {
dynamic nm = null;
_global.sprite(me.spritenum).visibility = 1;
nm = (me.spritenum-38);
if ((_movieScript.global_gseprops.sounds[nm].mem == @"None")) {
_global.member(LingoGlobal.concat(@"AmbienceSound",nm)).text = @"No Ambience Sound";
}
else {
_global.member(LingoGlobal.concat(@"AmbienceSound",nm)).text = _movieScript.global_gseprops.sounds[nm].mem;
}

return null;
}
}
}
