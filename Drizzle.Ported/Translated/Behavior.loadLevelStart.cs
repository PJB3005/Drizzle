using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: loadLevelStart
//
public sealed class loadLevelStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic pth = null;
dynamic f = null;
dynamic filelist = null;
dynamic i = null;
dynamic n = null;
dynamic l = null;
dynamic txt = null;
dynamic q = null;
_movieScript.global_projects = new LingoPropertyList {};
pth = LingoGlobal.concat(_global.the_moviePath,@"LevelEditorProjects\");
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
pth = LingoGlobal.concat(LingoGlobal.concat(pth,@"\"),f);
}
filelist = new LingoPropertyList {};
for (int tmp_i = 1; tmp_i <= 300; tmp_i++) {
i = tmp_i;
n = _global.getnthfilenameinfolder(pth,i);
if ((n == LingoGlobal.EMPTY)) {
break;
}
if (LingoGlobal.ToBool(LingoGlobal.charof_helper((n.length-3),LingoGlobal.op_ne(n,@".")))) {
_movieScript.global_projects.add(LingoGlobal.concat(@"#",n));
}
else {
filelist.append(n);
}
}
foreach (dynamic tmp_l in filelist) {
l = tmp_l;
if ((LingoGlobal.chars(l,(l.length-3),l.length) == @".txt")) {
_movieScript.global_projects.add(LingoGlobal.chars(l,1,(l.length-4)));
}
}
txt = @"Use the arrow keys to select a project. Use enter to open it.";
txt += txt.ToString();
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
txt += txt.ToString();
}
txt += txt.ToString();
txt += txt.ToString();
foreach (dynamic tmp_q in _movieScript.global_projects) {
q = tmp_q;
txt += txt.ToString();
txt += txt.ToString();
}
_movieScript.global_ldprps = new LingoPropertyList {[new LingoSymbol("lstup")] = 1,[new LingoSymbol("lstDwn")] = 1,[new LingoSymbol("lft")] = 1,[new LingoSymbol("rgth")] = 1,[new LingoSymbol("currproject")] = 1,[new LingoSymbol("listscrollpos")] = 1,[new LingoSymbol("listshowtotal")] = 30};
_global.member(@"ProjectsL").text = txt;
_global.member(@"PalName").text = @"Press 'N' to create a new level. Use left and right arrows to step in and out of subfolders";

return null;
}
}
}
