using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: testDrawLevel
//
public sealed class testDrawLevel : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
_movieScript.global_gfullrender = 0;
_movieScript.global_lightrects = new LingoList(new dynamic[] { LingoGlobal.rect(0,0,0,0),LingoGlobal.rect(0,0,0,0) });
_movieScript.drawtestlevel();
_global.member(@"finalfg").image.setpixel(0,0,_movieScript.global_gskycolor);
_global.member(@"finalfg").image.setpixel(1,0,_movieScript.global_gskycolor);
_global.member(@"finalfg").image.setpixel(2,0,_global.color(10,10,10));
_global.member(@"finalfg").image.setpixel(3,0,_global.color(10,10,10));
_global.member(@"finalfg").image.setpixel(0,1,_global.color(10,10,10));
_global.member(@"finalfg").image.setpixel(1,1,_global.color(10,10,10));
_movieScript.global_levelname = _movieScript.global_gloadedname;
_global.member(@"TextInput").text = _movieScript.global_gloadedname;
_global.put(@"I'M DOING A TEST RENDER!");
_global.alert(@"I'M DOING A TEST RENDER!");
_global.go(76);

return null;
}
}
}
