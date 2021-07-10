using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderLightStart2
//
public sealed class renderLightStart2 : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic smpl = null;
dynamic smplps = null;
dynamic dp = null;
dynamic pstrct = null;
dynamic ang = null;
dynamic flatness = null;
_movieScript.global_q = 1;
_movieScript.global_c = 1;
_movieScript.global_tm = _global._system.milliseconds;
for (int tmp_q = 0; tmp_q <= 19; tmp_q++) {
q = tmp_q;
_global.sprite((40-q)).loc = (_global.sprite((40-q)).loc+LingoGlobal.point(-q,-q));
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(q)),@"sh")).image = _global.image(1040,800,32);
}
_global.member(@"dpImage").image = _global.image(1040,800,32);
_global.member(@"dpImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
smpl = _global.image(4,1,32);
smplps = 0;
_movieScript.global_dptsl = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= 20; tmp_q++) {
q = tmp_q;
dp = ((20-q)-5);
pstrct = LingoGlobal.rect(_movieScript.depthpnt(LingoGlobal.point(0,0),dp),_movieScript.depthpnt(LingoGlobal.point(1040,800),dp));
_global.member(@"dpImage").image.copypixels(_global.member(LingoGlobal.concat(@"layer",_global.@string((20-q)))).image,pstrct,LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
smpl.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(smplps,0,4,1),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 0});
if (((((dp+5) == 12) | ((dp+5) == 8)) | ((dp+5) == 4))) {
smpl.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,4,1),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 10,[new LingoSymbol("color")] = 255});
smplps = (smplps+1);
_global.member(@"dpImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 10,[new LingoSymbol("color")] = 255});
}
}
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
_movieScript.global_dptsl.add(smpl.getpixel((4-q),0));
}
ang = _movieScript.global_glighteprops.lightangle;
ang = (_movieScript.degtovec(ang)*new LingoDecimal(2.8));
flatness = 1;
_movieScript.global_mvl = new LingoList(new dynamic[] { new LingoList(new dynamic[] { ang.loch,ang.locv,1 }) });
for (int tmp_q = 1; tmp_q <= _movieScript.global_glighteprops.flatness; tmp_q++) {
q = tmp_q;
_movieScript.global_mvl.add(new LingoList(new dynamic[] { ang.loch,ang.locv,0 }));
}

return null;
}
}
}
