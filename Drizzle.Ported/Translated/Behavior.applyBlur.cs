using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: applyBlur
//
public sealed class applyBlur : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
if (LingoGlobal.ToBool(0)) {
if (LingoGlobal.ToBool(_movieScript.global_gviewrender)) {
if (LingoGlobal.ToBool(_global._key.keypressed(48))) {
_global._movie.go(9);
}
me.newframe();
if (LingoGlobal.ToBool(_movieScript.global_keeplooping)) {
_global.go(_global.the_frame);
}
}
else {
while (LingoGlobal.ToBool(_movieScript.global_keeplooping)) {
me.newframe();
}
}
}

return null;
}
public dynamic newframe(dynamic me) {
_global.sprite(59).locv = (_movieScript.global_c-8);

return null;
}
public dynamic changelightrect(dynamic me,dynamic lr,dynamic pnt) {
if ((pnt.loch < _movieScript.global_lightrects[lr].left)) {
_movieScript.global_lightrects[lr].left = pnt.loch;
}
if ((pnt.loch > _movieScript.global_lightrects[lr].right)) {
_movieScript.global_lightrects[lr].right = pnt.loch;
}
if ((pnt.locv < _movieScript.global_lightrects[lr].top)) {
_movieScript.global_lightrects[lr].top = pnt.locv;
}
if ((pnt.locv > _movieScript.global_lightrects[lr].bottom)) {
_movieScript.global_lightrects[lr].bottom = pnt.locv;
}
_global.sprite((10+lr)).rect = (_movieScript.global_lightrects[lr]+LingoGlobal.rect(-8,-16,-8,-16));

return null;
}
}
}
