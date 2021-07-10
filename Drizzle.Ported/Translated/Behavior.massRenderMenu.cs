using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: massRenderMenu
//
public sealed class massRenderMenu : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic pth = null;
dynamic f = null;
dynamic txt = null;
dynamic q = null;
dynamic up = null;
dynamic dwn = null;
dynamic lft = null;
dynamic rgth = null;
dynamic entr = null;
pth = LingoGlobal.concat(_global.the_moviePath,@"LevelEditorProjects\");
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
pth = LingoGlobal.concat(LingoGlobal.concat(pth,f),@"\");
}
txt = @"Use arrow keys and space to select projects for rendering.";
txt += txt.ToString();
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
txt += txt.ToString();
}
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_q = _movieScript.global_ldprps.listscrollpos; tmp_q <= (_movieScript.global_ldprps.listscrollpos+_movieScript.global_ldprps.listshowtotal); tmp_q++) {
q = tmp_q;
if ((q > _movieScript.global_projects.count)) {
break;
}
else if ((q != _movieScript.global_ldprps.currproject)) {
if ((_movieScript.global_massrenderselectl.getpos(LingoGlobal.concat(pth,_movieScript.global_projects[q])) == 0)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
}
else if ((_movieScript.global_massrenderselectl.getpos(LingoGlobal.concat(pth,_movieScript.global_projects[q])) == 0)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
txt += txt.ToString();
}
_global.member(@"ProjectsL").text = txt;
txt = @"MASS RENDER";
txt += txt.ToString();
txt += txt.ToString();
foreach (dynamic tmp_q in _movieScript.global_massrenderselectl) {
q = tmp_q;
txt += txt.ToString();
txt += txt.ToString();
}
_global.member(@"massRenderL").text = txt;
up = _global._key.keypressed(126);
dwn = _global._key.keypressed(125);
lft = _global._key.keypressed(123);
rgth = _global._key.keypressed(124);
if ((LingoGlobal.ToBool(up) & (_movieScript.global_ldprps.lstup == 0))) {
_movieScript.global_ldprps.currproject = (_movieScript.global_ldprps.currproject-1);
if ((_movieScript.global_ldprps.currproject < 1)) {
_movieScript.global_ldprps.currproject = _movieScript.global_projects.count;
}
}
if ((LingoGlobal.ToBool(dwn) & (_movieScript.global_ldprps.lstdwn == 0))) {
_movieScript.global_ldprps.currproject = (_movieScript.global_ldprps.currproject+1);
if ((_movieScript.global_ldprps.currproject > _movieScript.global_projects.count)) {
_movieScript.global_ldprps.currproject = 1;
}
}
if ((_movieScript.global_ldprps.currproject < _movieScript.global_ldprps.listscrollpos)) {
_movieScript.global_ldprps.listscrollpos = _movieScript.global_ldprps.currproject;
}
else if ((_movieScript.global_ldprps.currproject > (_movieScript.global_ldprps.listscrollpos+_movieScript.global_ldprps.listshowtotal))) {
_movieScript.global_ldprps.listscrollpos = (_movieScript.global_ldprps.currproject-_movieScript.global_ldprps.listshowtotal);
}
if (((LingoGlobal.ToBool(rgth) & (_movieScript.global_ldprps.rgth == 0)) & (_movieScript.global_projects.count > 0))) {
if ((LingoGlobal.chars(_movieScript.global_projects[_movieScript.global_ldprps.currproject],1,1) == @"#")) {
me.loadsubfolder(_movieScript.global_projects[_movieScript.global_ldprps.currproject]);
}
}
else if ((LingoGlobal.ToBool(lft) & (_movieScript.global_ldprps.lft == 0))) {
if ((_movieScript.global_gloadpath.count > 0)) {
_movieScript.global_gloadpath.deleteat(_movieScript.global_gloadpath.count);
_global._movie.go(4);
}
}
_movieScript.global_ldprps.lstup = up;
_movieScript.global_ldprps.lstdwn = dwn;
_movieScript.global_ldprps.lft = lft;
_movieScript.global_ldprps.rgth = rgth;
if (LingoGlobal.ToBool(_global._key.keypressed(@"A"))) {
foreach (dynamic tmp_q in _movieScript.global_projects) {
q = tmp_q;
if (((_movieScript.global_massrenderselectl.getpos(LingoGlobal.concat(pth,q)) == 0) & (LingoGlobal.chars(q,1,1) != @"#"))) {
_movieScript.global_massrenderselectl.add(LingoGlobal.concat(pth,q));
}
}
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"C"))) {
_movieScript.global_massrenderselectl = new LingoPropertyList {};
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(48)) | LingoGlobal.ToBool(_global._key.keypressed(@"1")))) {
_global._movie.go(9);
}
entr = _global._key.keypressed(@" ");
if ((LingoGlobal.ToBool(entr) & (_movieScript.global_ldprps.lstenter == 0))) {
if ((_movieScript.global_massrenderselectl.getpos(LingoGlobal.concat(pth,_movieScript.global_projects[_movieScript.global_ldprps.currproject])) == 0)) {
_movieScript.global_massrenderselectl.add(LingoGlobal.concat(pth,_movieScript.global_projects[_movieScript.global_ldprps.currproject]));
}
else {
_movieScript.global_massrenderselectl.deleteone(LingoGlobal.concat(pth,_movieScript.global_projects[_movieScript.global_ldprps.currproject]));
}
}
_movieScript.global_ldprps.lstenter = entr;
if (LingoGlobal.ToBool(_global._key.keypressed(36))) {
_movieScript.global_gviewrender = 1;
_movieScript.global_gmassrenderl = _movieScript.global_massrenderselectl.duplicate();
_movieScript.global_gmassrenderl.addat(1,@"DUMMY");
_global._movie.go(90);
}
_global.go(_global.the_frame);

return null;
}
public dynamic loadsubfolder(dynamic me,dynamic fldrname) {
_movieScript.global_gloadpath.add(LingoGlobal.chars(fldrname,2,fldrname.length));
_global._movie.go(4);

return null;
}
}
}
