using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: tileEditorStart
//
public sealed class tileEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic l = null;
dynamic q = null;
_global.member(@"tileMenu").alignment = new LingoSymbol("left");
_global.member(@"TEimg1").image = _global.image((52*16),(40*16),16);
_global.member(@"TEimg2").image = _global.image((52*16),(40*16),16);
_global.member(@"TEimg3").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage1").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage2").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage3").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImageShortCuts").image = _global.image((52*16),(40*16),16);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),1);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),2);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),3);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),1);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),2);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),3);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16);
_movieScript.global_gdirectionkeys = new LingoList(new dynamic[] { 0,0,0,0 });
_global.sprite(1).blend = 10;
_global.sprite(2).blend = 10;
_global.sprite(3).blend = 60;
_global.sprite(4).blend = 10;
_global.sprite(5).blend = 70;
_global.sprite(6).blend = 100;
_global.sprite(8).blend = 80;
_global.sprite(8).visibility = 0;
_global.script(@"tileEditor").changelayer();
_global.member(@"default material").text = LingoGlobal.concat_space(LingoGlobal.concat_space(@"Default material:",_movieScript.global_gteprops.defaultmaterial),@"(Press 'E' to change)");
_global.sprite(19).visibility = 1;
l = new LingoPropertyList {[new LingoSymbol("l")] = 0,[new LingoSymbol("m1")] = 0,[new LingoSymbol("m2")] = 0,[new LingoSymbol("w")] = 0,[new LingoSymbol("a")] = 0,[new LingoSymbol("s")] = 0,[new LingoSymbol("d")] = 0,[new LingoSymbol("c")] = 0,[new LingoSymbol("q")] = 0};
_movieScript.global_gteprops.lastkeys = l.duplicate();
_movieScript.global_gteprops.keys = l.duplicate();
_global.script(@"tileEditor").updatetilemenu(LingoGlobal.point(0,0));
_movieScript.global_gteprops.tmsavposl = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gtiles.count; tmp_q++) {
q = tmp_q;
_movieScript.global_gteprops.tmsavposl.add(1);
}

return null;
}
}
}
