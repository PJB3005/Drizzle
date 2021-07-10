using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: buttonBehav
//
public sealed class buttonBehav : LingoBehaviorScript {
public dynamic mousewithin(dynamic me) {
_global.sprite(me.spritenum).color = _global.color(255,0,0);
if (LingoGlobal.ToBool(_movieScript.global_gloprops.mouseclick)) {
_global.script(@"levelOverview").buttonclicked(_global.sprite(me.spritenum).member.name);
_movieScript.global_gloprops.mouseclick = 0;
}
switch (_global.sprite(me.spritenum).member.name) {
case @"button geometry editor":
_global.member(@"buttonText").text = @"(2) Edit the geometry of the level. After this step is finished you can do a TEST RENDER";
break;
case @"button tile editor":
_global.member(@"buttonText").text = @"(3) Edit the tiles. Decide what materials and objects the level will be made of";
break;
case @"button effects editor":
_global.member(@"buttonText").text = @"(4) Add effects and additional objects such as rust, plants, and chains";
break;
case @"button light editor":
_global.member(@"buttonText").text = @"(5) Edit the light source and shadows";
break;
case @"button render level":
_global.member(@"buttonText").text = @"Render and save as a Rain World playable level";
break;
case @"button test render":
_global.member(@"buttonText").text = @"Outputs the level geometry without re-rendering the images";
break;
case @"button save project":
_global.member(@"buttonText").text = @"Save as a Level Editor Project, that can be openened and edited further";
break;
case @"button load project":
_global.member(@"buttonText").text = @"Load a project";
break;
case @"button super more time":
_movieScript.global_geditlizard[3] = _movieScript.restrict((_movieScript.global_geditlizard[3]+1),0,4000);
_global.member(@"addLizardTime").text = _global.@string(_movieScript.global_geditlizard[3]);
break;
case @"button super less time":
_movieScript.global_geditlizard[3] = _movieScript.restrict((_movieScript.global_geditlizard[3]-1),0,4000);
_global.member(@"addLizardTime").text = _global.@string(_movieScript.global_geditlizard[3]);
break;
case @"button lizard hole":
_global.sprite(60).visibility = (_global.random(2)-1);
break;
case @"button standard medium":
if (LingoGlobal.ToBool(_movieScript.global_glevel.defaultterrain)) {
_global.member(@"buttonText").text = @"The level is surrounded by a solid wall";
}
else {
_global.member(@"buttonText").text = @"It's possible to fall out of the level";
}
break;
case @"button add lizard":
_global.member(@"buttonText").text = @"Add a dragon";
break;
case @"button delete Lizard":
_global.member(@"buttonText").text = @"Delete the last dragon";
break;
case @"button light type":
if (LingoGlobal.ToBool(_movieScript.global_gloprops.light)) {
_global.member(@"buttonText").text = @"Sunlight: ON";
}
else {
_global.member(@"buttonText").text = @"Sunlight: OFF";
}
break;
case @"button sound editor":
_global.member(@"buttonText").text = @"(6) Edit music and ambient sounds.";
break;
case @"button mass render":
_global.member(@"buttonText").text = @"Render multiple levels in one go.";
break;
case @"button level size":
_global.member(@"buttonText").text = @"Change the proportions of the level";
break;
case @"button cameras":
_global.member(@"buttonText").text = @"Set camera positions viewing this level";
break;
case @"button environment editor":
_global.member(@"buttonText").text = @"Edit water and environmental elements";
break;
case @"button prop editor":
_global.member(@"buttonText").text = @"Add props";
break;
case @"button update preview":
if ((_movieScript.global_glastdrawwasfullandmini == 1)) {
_global.member(@"buttonText").text = @"Update the room preview (Updated)";
}
else {
_global.member(@"buttonText").text = @"Update the room preview (Needs to be refreshed)";
}
break;
case @"button prio cam":
_global.member(@"buttonText").text = @"Select a specific camera to render first";
break;
}

return null;
}
public dynamic mouseleave(dynamic me) {
_global.sprite(me.spritenum).color = _global.color(0,0,0);
_global.member(@"buttonText").text = @"";

return null;
}
}
}
