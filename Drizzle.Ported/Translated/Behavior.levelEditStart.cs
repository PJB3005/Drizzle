using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: levelEditStart
//
public sealed class levelEditStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic cols = null;
dynamic rows = null;
dynamic q = null;
dynamic c = null;
dynamic rct = null;
dynamic nm = null;
cols = _movieScript.global_gloprops.size.loch;
rows = _movieScript.global_gloprops.size.locv;
_global.member(@"levelEditImage1").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage2").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage3").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImageShortCuts").image = _global.image((52*16),(40*16),16);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,cols,rows),1);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,cols,rows),2);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,cols,rows),3);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,cols,rows),16);
_movieScript.global_gdirectionkeys = new LingoList(new dynamic[] { 0,0,0,0 });
for (int tmp_q = 800; tmp_q <= 820; tmp_q++) {
q = tmp_q;
_global.sprite(q).visibility = 1;
}
_global.sprite(2).visibility = 1;
_global.sprite(8).visibility = 1;
_global.member(@"toolsImage").image = _global.image((_movieScript.global_gleprops.toolmatrix[1].count*32),(_movieScript.global_gleprops.toolmatrix.count*32),16);
for (int tmp_q = 1; tmp_q <= _movieScript.global_gleprops.toolmatrix.count; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gleprops.toolmatrix[1].count; tmp_c++) {
c = tmp_c;
rct = LingoGlobal.rect(((c-1)*32),((q-1)*32),(c*32),(q*32));
nm = LingoGlobal.concat(@"icon",_movieScript.global_gleprops.toolmatrix[q][c]);
_global.member(@"toolsImage").image.copypixels(_global.member(LingoGlobal.concat(@"icon",_movieScript.global_gleprops.toolmatrix[q][c])).image,rct,LingoGlobal.rect(0,0,32,32));
}
}
_movieScript.global_gleprops.leveleditors[1].p.mirrorpos = (_movieScript.global_gloprops.size.loch/2);

return null;
}
}
}
