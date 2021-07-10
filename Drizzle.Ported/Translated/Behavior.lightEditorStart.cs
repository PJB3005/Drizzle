using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: lightEditorStart
//
public sealed class lightEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic l = null;
_movieScript.global_firstframe = 1;
l = new LingoPropertyList {[new LingoSymbol("m1")] = 1,[new LingoSymbol("m2")] = 0,[new LingoSymbol("w")] = 0,[new LingoSymbol("a")] = 0,[new LingoSymbol("s")] = 0,[new LingoSymbol("d")] = 0,[new LingoSymbol("r")] = 0,[new LingoSymbol("f")] = 0};
_movieScript.global_glighteprops.lastkeys = l.duplicate();
_movieScript.global_glighteprops.keys = l.duplicate();
for (int tmp_l = 1; tmp_l <= 3; tmp_l++) {
l = tmp_l;
_movieScript.minilvleditdraw(l);
}
_movieScript.global_geverysecond = 0;
_movieScript.global_gdirectionkeys = new LingoList(new dynamic[] { 0,0,0,0 });
_movieScript.global_glgtimgquad = new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(_global.member(@"lightImage").image.width,0),LingoGlobal.point(_global.member(@"lightImage").image.width,_global.member(@"lightImage").image.height),LingoGlobal.point(0,_global.member(@"lightImage").image.height) });
_movieScript.global_glighteprops.lasttm = _global._system.milliseconds;
_global.sprite(11).member = _global.member(@"pxl");
_global.sprite(12).member = _global.member(@"pxl");
_movieScript.global_glighteprops.paintshape = @"pxl";
_global.sprite(5).rect = LingoGlobal.rect(0,0,(_movieScript.global_gloprops.size.loch*20),(_movieScript.global_gloprops.size.locv*20));
_global.sprite(8).rect = LingoGlobal.rect(0,0,(_movieScript.global_gloprops.size.loch*20),(_movieScript.global_gloprops.size.locv*20));
_global.sprite(9).member = _global.member(@"lightImage");
_global.sprite(10).member = _global.member(@"lightImage");
_global.sprite(6).member = _global.member(@"lightImage");

return null;
}
}
}
