using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: cameraEditorStart
//
public sealed class cameraEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic cols = null;
dynamic rows = null;
dynamic q = null;
cols = _movieScript.global_gloprops.size.loch;
rows = _movieScript.global_gloprops.size.locv;
_global.member(@"levelEditImageShortCuts").image = _global.image((cols*5),(rows*5),1);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,cols,rows),5,1);
for (int tmp_q = 1; tmp_q <= 3; tmp_q++) {
q = tmp_q;
_movieScript.minilvleditdraw(q);
}
_global.script(@"cameraEditor").drawall();

return null;
}
}
}
