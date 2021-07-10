using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: sliderBehav
//
public sealed class sliderBehav : LingoBehaviorScript {
public dynamic mousewithin(dynamic me) {
dynamic val = null;
_global.sprite(me.spritenum).color = _global.color(255,0,0);
if (LingoGlobal.ToBool(_global._mouse.mousedown)) {
val = (_movieScript.restrict(_global._mouse.mouseloc.loch,50,450)-50);
_global.sprite(me.spritenum).loch = (val+50);
switch (_global.sprite(me.spritenum).member.name) {
case @"tileSeedSlider":
_movieScript.global_gloprops.tileseed = val;
_global.put(@"seed changed!");
_global.the_randomSeed = _movieScript.global_gloprops.tileseed;
break;
}
}
switch (_global.sprite(me.spritenum).member.name) {
case @"tileSeedSlider":
_global.member(@"buttonText").text = LingoGlobal.concat_space(@"Tile random seed:",_global.@string(_movieScript.global_gloprops.tileseed));
break;
}

return null;
}
public dynamic mouseleave(dynamic me) {
_global.sprite(me.spritenum).color = _global.color(0,0,0);
_global.member(@"buttonText").text = @"";
_global.sprite(20).quad = new LingoList(new dynamic[] { LingoGlobal.point(-100,-100),LingoGlobal.point(-100,-100),LingoGlobal.point(-100,-100),LingoGlobal.point(-100,-100) });

return null;
}
}
}
