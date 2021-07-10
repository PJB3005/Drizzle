using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: LOstart
//
public sealed class LOstart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic cols = null;
dynamic rows = null;
dynamic q = null;
dynamic l = null;
cols = _movieScript.global_gloprops.size.loch;
rows = _movieScript.global_gloprops.size.locv;
_movieScript.global_geditlizard = new LingoList(new dynamic[] { @"pink",0,0,0 });
_global.script(@"levelOverview").nexthole();
_global.member(@"addLizardTime").text = @"0";
_global.member(@"addLizardFlies").text = @"0";
_global.sprite(43).color = _global.color(255,0,255);
_global.sprite(2).loc = (LingoGlobal.point(312,312)+LingoGlobal.point(((-1000+1000)*_movieScript.global_glevel.defaultterrain),0));
_global.sprite(56).visibility = 1;
_global.sprite(57).visibility = 1;
_global.sprite(58).visibility = 1;
_global.sprite(59).visibility = 1;
_global.sprite(67).loch = ((_movieScript.global_glevel.waterdrips*8)+50);
_global.sprite(68).loch = ((_movieScript.global_glevel.maxflies*10)+50);
_global.sprite(69).loch = ((_movieScript.global_glevel.flyspawnrate*4)+50);
_global.member(@"lightTypeText").text = _movieScript.global_glevel.lighttype;
_global.sprite(70).loch = (_movieScript.global_gloprops.tileseed+50);
_global.script(@"levelOverview").updatelizardslist();
for (int tmp_q = 0; tmp_q <= 29; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(@"layer",q)).image = _global.image(1,1,1);
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",q),@"sh")).image = _global.image(1,1,1);
}
l = new LingoList(new dynamic[] { @"Dull",@"Reflective",@"Superflourescent" });
_global.member(@"color glow effects").text = LingoGlobal.concat_space(LingoGlobal.concat_space(l[(_movieScript.global_gloprops.colglows[1]+1)],LingoGlobal.RETURN),l[(_movieScript.global_gloprops.colglows[2]+1)]);
_global.sprite(22).rect = LingoGlobal.rect(-100,-100,-100,-100);
if ((_movieScript.global_gpriocam == 0)) {
_global.member(@"PrioCamText").text = @"";
}
else {
_global.member(@"PrioCamText").text = LingoGlobal.concat(LingoGlobal.concat(@"Will render camera ",_movieScript.global_gpriocam),@" first");
}
_global.the_randomSeed = _global._system.milliseconds;

return null;
}
}
}
