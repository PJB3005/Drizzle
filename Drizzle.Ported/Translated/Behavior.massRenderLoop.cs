using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: massRenderLoop
//
public sealed class massRenderLoop : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
_movieScript.global_gmassrenderl.deleteat(1);
if ((_movieScript.global_gmassrenderl.count == 0)) {
_global.alert(@"Mass Render Finished");
_global._movie.go(1);
}
else {
_global.put(LingoGlobal.concat_space(@"started rendering:",_movieScript.global_gmassrenderl[1]));
_global.script(@"loadLevel").loadlevel(_movieScript.global_gmassrenderl[1],1);
_movieScript.global_gviewrender = 0;
_global._movie.go(42);
}

return null;
}
}
}
