using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: goback
//
public sealed class goback : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
if ((_movieScript.global_gmassrenderl == new LingoPropertyList {})) {
_global._movie.go(9);
}
else {
_global._movie.go(90);
}

return null;
}
}
}
