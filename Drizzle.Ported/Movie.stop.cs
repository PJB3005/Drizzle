using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: stop
//
public sealed partial class Movie {
public dynamic stopmovie(dynamic me) {
dynamic changeBack = null;
changeBack = _global.basetdisplay(_movieScript.global_gSaveProps[Integer { Value = 1 }],_movieScript.global_gSaveProps[Integer { Value = 2 }],_movieScript.global_gSaveProps[Integer { Value = 3 }],@"perm",LingoGlobal.FALSE);

}
}
}
