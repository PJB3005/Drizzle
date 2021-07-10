using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: changeSize
//
public sealed class changeSize : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
if (LingoGlobal.ToBool(_global._key.keypressed(@"A"))) {
if ((((_movieScript.global_gloprops.size != LingoGlobal.point(_movieScript.global_newsize[1],_movieScript.global_newsize[2])) | (_movieScript.global_newsize[3] > 0)) | (_movieScript.global_newsize[4] > 0))) {
_movieScript.resizelevel(LingoGlobal.point(_movieScript.global_newsize[1],_movieScript.global_newsize[2]),_movieScript.global_newsize[3],_movieScript.global_newsize[4]);
}
_movieScript.global_gloprops.extratiles = _movieScript.global_extrabuffertiles.duplicate();
_global._movie.go(9);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"C"))) {
_global._movie.go(9);
}
else {
_global.go(_global.the_frame);
}

return null;
}
}
}
