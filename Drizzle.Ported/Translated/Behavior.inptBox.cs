using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: inptBox
//
public sealed class inptBox : LingoBehaviorScript {
public dynamic change(dynamic me) {
_movieScript.global_levelname = _global.sprite(me.spritenum).text;

return null;
}
}
}
