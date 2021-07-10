using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: effectsEditorStart
//
public sealed class effectsEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic spr = null;
dynamic q = null;
dynamic c = null;
dynamic gdirectionkeys = null;
dynamic l = null;
_global.sprite(15).color = _global.color(255,255,255);
_global.member(@"effectsMatrix").image = _global.image(52,40,32);
_global.sprite(3).blend = 30;
_global.sprite(4).blend = 50;
_global.sprite(5).blend = 70;
_global.sprite(6).blend = 100;
for (int tmp_spr = 3; tmp_spr <= 9; tmp_spr++) {
spr = tmp_spr;
_global.sprite(spr).rect = LingoGlobal.rect(16,16,(53*16),(41*16));
}
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
_global.member(@"effectsMatrix").image.setpixel((q-1),(c-1),_global.color(0,0,0));
}
}
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
gdirectionkeys = new LingoList(new dynamic[] { 0,0,0,0 });
_global.sprite(3).blend = 10;
_global.sprite(4).blend = 10;
_global.sprite(5).blend = 60;
_global.sprite(6).blend = 10;
_global.sprite(7).blend = 70;
_global.sprite(8).blend = 100;
l = new LingoPropertyList {[new LingoSymbol("n")] = 0,[new LingoSymbol("m1")] = 0,[new LingoSymbol("m2")] = 0,[new LingoSymbol("w")] = 0,[new LingoSymbol("a")] = 0,[new LingoSymbol("s")] = 0,[new LingoSymbol("d")] = 0,[new LingoSymbol("e")] = 0,[new LingoSymbol("r")] = 0,[new LingoSymbol("f")] = 0};
_movieScript.global_geeprops.lastkeys = l.duplicate();
_movieScript.global_geeprops.keys = l.duplicate();
_global.script(@"effectsEditor").updateeffectsmenu(LingoGlobal.point(0,0));
_global.script(@"effectsEditor").updateeffectsl(0);
_global.script(@"effectsEditor").initmode(@"createNew");
_global.script(@"effectsEditor").drawefmtrx(_movieScript.global_geeprops.editeffect);

return null;
}
}
}
