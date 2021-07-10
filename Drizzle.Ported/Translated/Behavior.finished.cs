using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: finished
//
public sealed class finished : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
if ((LingoGlobal.ToBool(_global._key.keypressed(48)) & LingoGlobal.ToBool(_movieScript.global_gviewrender))) {
_global._movie.go(9);
}
if ((_movieScript.global_gviewrender == 0)) {
_movieScript.global_levelname = _movieScript.global_gloadedname;
}
for (int tmp_q = 0; tmp_q <= (_movieScript.global_gdecalcolors.count-1); tmp_q++) {
q = tmp_q;
_global.member(@"finalImage").image.setpixel(q,0,_movieScript.global_gdecalcolors[(q+1)]);
}

return null;
}
}
}
