using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: getExtraTile
//
public sealed class getExtraTile : LingoBehaviorScript {
public dynamic change(dynamic me) {
if ((me.spritenum == 48)) {
_movieScript.global_newsize[1] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 49)) {
_movieScript.global_newsize[2] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 50)) {
_movieScript.global_extrabuffertiles[1] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 51)) {
_movieScript.global_extrabuffertiles[2] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 52)) {
_movieScript.global_extrabuffertiles[3] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 53)) {
_movieScript.global_extrabuffertiles[4] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 54)) {
_movieScript.global_newsize[3] = _global.value(_global.sprite(me.spritenum).text);
}
else if ((me.spritenum == 55)) {
_movieScript.global_newsize[4] = _global.value(_global.sprite(me.spritenum).text);
}

return null;
}
}
}
