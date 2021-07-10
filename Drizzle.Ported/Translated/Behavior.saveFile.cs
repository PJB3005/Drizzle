using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: saveFile
//
public sealed class saveFile : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic props = null;
dynamic ok = null;
props = new LingoPropertyList {[@"image"] = _global.member(@"finalImage").image,[@"filename"] = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(_global._movie.path,@"Levels/"),_movieScript.global_gloadedname),@"_"),_movieScript.global_gcurrentrendercamera),@".png")};
ok = _movieScript.global_gimgxtra.ix_saveimage(props);
if ((_movieScript.global_gcurrentrendercamera < _movieScript.global_gcameraprops.cameras.count)) {
_global.put(LingoGlobal.concat_space(@"sendback",_movieScript.global_gcurrentrendercamera));
_global._movie.go(44);
}
else {
_movieScript.newmakelevel(_movieScript.global_gloadedname);
}

return null;
}
public dynamic changetoplaymatrix() {
dynamic nmtrx = null;
dynamic q = null;
dynamic l = null;
dynamic c = null;
dynamic cell = null;
nmtrx = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gloprops.size.loch; tmp_q++) {
q = tmp_q;
l = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= _movieScript.global_gloprops.size.locv; tmp_c++) {
c = tmp_c;
cell = new LingoList(new dynamic[] { _movieScript.global_gleprops.matrix[q][c][1].duplicate(),new LingoList(new dynamic[] { (LingoGlobal.op_gt(new LingoList(new dynamic[] { 1,9 }).getpos(_movieScript.global_gleprops.matrix[q][c][2][1]),0)*LingoGlobal.op_eq(_movieScript.global_gleprops.matrix[q][c][2][2].getpos(11),0)),new LingoPropertyList {} }) });
if ((cell[1][1] == 9)) {
cell[1][1] = 1;
cell[1][2].add(8);
}
if (((((cell[1][2].getpos(6) > 0) | (cell[1][2].getpos(7) > 0)) | (cell[1][2].getpos(19) > 0)) | (cell[1][2].getpos(21) > 0))) {
if ((cell[1][2].getpos(5) == 0)) {
cell[1][2].add(5);
}
}
if ((cell[1][2].getpos(11) > 0)) {
cell[1][1] = 0;
if ((c > 1)) {
if ((((((_movieScript.global_gleprops.matrix[q][(c-1)][1][1] == 0) & (_movieScript.global_gleprops.matrix[(q-1)][c][1][1] == 1)) & (_movieScript.global_gleprops.matrix[(q-1)][c][1][2].getpos(11) == 0)) & (_movieScript.global_gleprops.matrix[(q+1)][c][1][1] == 1)) & (_movieScript.global_gleprops.matrix[(q+1)][c][1][2].getpos(11) == 0))) {
cell[1][1] = 6;
}
}
}
l.add(cell);
}
nmtrx.add(l);
}
return nmtrx;

}
}
}
