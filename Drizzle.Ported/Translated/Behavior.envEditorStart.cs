using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: envEditorStart
//
public sealed class envEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic cols = null;
dynamic rows = null;
dynamic l = null;
cols = _movieScript.global_gloprops.size.loch;
rows = _movieScript.global_gloprops.size.locv;
for (int tmp_l = 1; tmp_l <= 3; tmp_l++) {
l = tmp_l;
_movieScript.minilvleditdraw(l);
}
_movieScript.global_genveditbuttons = new LingoPropertyList {[new LingoSymbol("w")] = 0,[new LingoSymbol("f")] = 0};
_movieScript.global_glastenveditbuttons = _movieScript.global_genveditbuttons.duplicate();

return null;
}
public dynamic checkkey(dynamic me,dynamic key) {
dynamic rtrn = null;
rtrn = 0;
_movieScript.global_genveditbuttons[LingoGlobal.symbol(key)] = _global._key.keypressed(key);
if ((LingoGlobal.ToBool(_movieScript.global_genveditbuttons[LingoGlobal.symbol(key)]) & (_movieScript.global_glastenveditbuttons[LingoGlobal.symbol(key)] == 0))) {
rtrn = 1;
}
_movieScript.global_glastenveditbuttons[LingoGlobal.symbol(key)] = _movieScript.global_genveditbuttons[LingoGlobal.symbol(key)];
return rtrn;

}
}
}
