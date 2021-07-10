using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: cameraRepeatPoint
//
public sealed class cameraRepeatPoint : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
_global.put(@"camera repeat point");
if (LingoGlobal.ToBool(_movieScript.global_firstcamrepeat)) {
if ((_movieScript.global_gpriocam == 0)) {
_movieScript.global_gcurrentrendercamera = 1;
}
else {
_movieScript.global_gcurrentrendercamera = _movieScript.global_gpriocam;
}
_movieScript.global_firstcamrepeat = LingoGlobal.FALSE;
}
else if ((_movieScript.global_gcurrentrendercamera == _movieScript.global_gpriocam)) {
_movieScript.global_gcurrentrendercamera = 0;
}
_movieScript.global_gcurrentrendercamera = (_movieScript.global_gcurrentrendercamera+1);
if ((_movieScript.global_gcurrentrendercamera == _movieScript.global_gpriocam)) {
_movieScript.global_gcurrentrendercamera = (_movieScript.global_gcurrentrendercamera+1);
}
_movieScript.global_grendercameratilepos = LingoGlobal.point(((_movieScript.global_gcameraprops.cameras[_movieScript.global_gcurrentrendercamera].loch/new LingoDecimal(20))-new LingoDecimal(0.49999)).integer,((_movieScript.global_gcameraprops.cameras[_movieScript.global_gcurrentrendercamera].locv/new LingoDecimal(20))-new LingoDecimal(0.49999)).integer);
_movieScript.global_grendercamerapixelpos = (_movieScript.global_gcameraprops.cameras[_movieScript.global_gcurrentrendercamera]-(_movieScript.global_grendercameratilepos*20));
_movieScript.global_grendercamerapixelpos.loch = _movieScript.global_grendercamerapixelpos.loch.integer;
_movieScript.global_grendercamerapixelpos.locv = _movieScript.global_grendercamerapixelpos.locv.integer;
_movieScript.global_grendercameratilepos = (_movieScript.global_grendercameratilepos+LingoGlobal.point(-15,-10));

return null;
}
}
}
