using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: fiffigt
//
public sealed partial class MovieScript {
public dynamic diag(dynamic point1,dynamic point2) {
dynamic rectheight = null;
dynamic rectwidth = null;
dynamic diagonal = null;
rectheight = LingoGlobal.abs((point1.locv-point2.locv));
rectwidth = LingoGlobal.abs((point1.loch-point2.loch));
diagonal = LingoGlobal.sqrt(((rectheight*rectheight)+(rectwidth*rectwidth)));
return diagonal;

}
public dynamic diagwi(dynamic point1,dynamic point2,dynamic dig) {
dynamic rectheight = null;
dynamic rectwidth = null;
rectheight = LingoGlobal.abs((point1.locv-point2.locv));
rectwidth = LingoGlobal.abs((point1.loch-point2.loch));
return LingoGlobal.op_lt(((rectheight*rectheight)+(rectwidth*rectwidth)),(dig*dig));

}
public dynamic diagnosqrt(dynamic point1,dynamic point2) {
dynamic rectheight = null;
dynamic rectwidth = null;
dynamic diagonal = null;
rectheight = LingoGlobal.abs((point1.locv-point2.locv));
rectwidth = LingoGlobal.abs((point1.loch-point2.loch));
diagonal = ((rectheight*rectheight)+(rectwidth*rectwidth));
return diagonal;

}
public dynamic vertfliprect(dynamic rct) {
return new LingoList(new dynamic[] { LingoGlobal.point(rct.right,rct.top),LingoGlobal.point(rct.left,rct.top),LingoGlobal.point(rct.left,rct.bottom),LingoGlobal.point(rct.right,rct.bottom) });

}
public dynamic movetopoint(dynamic pointa,dynamic pointb,dynamic themovement) {
dynamic diag = null;
dynamic dirvec = null;
pointb = (pointb-pointa);
diag = diag(LingoGlobal.point(0,0),pointb);
if ((diag > 0)) {
dirvec = (pointb/diag);
}
else {
dirvec = LingoGlobal.point(0,1);
}
return (dirvec*themovement);

}
public dynamic returnrelativepoint(dynamic p1,dynamic p2) {
dynamic newx = null;
dynamic newy = null;
newx = (-1*(p1.loch-p2.loch));
newy = (p1.locv-p2.locv);
return LingoGlobal.point(newx,newy);

}
public dynamic returnabsolutepoint(dynamic p1,dynamic p2) {
dynamic realx = null;
dynamic realy = null;
realx = (p1.loch+p2.loch);
realy = (p1.locv-p2.locv);
return LingoGlobal.point(realx,realy);

}
public dynamic lerp(dynamic a,dynamic b,dynamic val) {
dynamic sv = null;
val = restrict(val,0,1);
if ((b < a)) {
sv = a;
a = b;
b = sv;
val = (new LingoDecimal(1)-val);
}
return restrict(((a+(b-a))*val),a,b);

}
public dynamic givecrosspoint(dynamic line1pnta,dynamic line1pntb,dynamic line2pnta,dynamic line2pntb) {
dynamic x1 = null;
dynamic y1 = null;
dynamic x2 = null;
dynamic y2 = null;
dynamic x3 = null;
dynamic y3 = null;
dynamic x4 = null;
dynamic y4 = null;
dynamic crosspointx = null;
dynamic crosspointy = null;
x1 = LingoGlobal.floatmember_helper(line1pnta.loch);
y1 = LingoGlobal.floatmember_helper(line1pnta.locv);
x2 = LingoGlobal.floatmember_helper(line1pntb.loch);
y2 = LingoGlobal.floatmember_helper(line1pntb.locv);
x3 = LingoGlobal.floatmember_helper(line2pnta.loch);
y3 = LingoGlobal.floatmember_helper(line2pnta.locv);
x4 = LingoGlobal.floatmember_helper(line2pntb.loch);
y4 = LingoGlobal.floatmember_helper(line2pntb.locv);
if (((x2 != x1) & (x4 != x3))) {
if (((((y4-y3)/(x4-x3))-((y2-y1)/(x2-x1))) != 0)) {
crosspointx = (((y1-(((((y2-y1)/(x2-x1))*x1)+((y4-y3)/(x4-x3)))*x3))-y3)/(((y4-y3)/(x4-x3))-((y2-y1)/(x2-x1))));
crosspointy = ((((y2-y1)/(x2-x1))*crosspointx)+(y1-(((y2-y1)/(x2-x1))*x1)));
}
}
else if ((x4 != x3)) {
crosspointx = x1;
crosspointy = (((((y4-y3)/(x4-x3))*crosspointx)+y3)-(((y4-y3)/(x4-x3))*x3));
}
else {
crosspointx = x3;
crosspointy = y1;
}
return LingoGlobal.point(crosspointx,crosspointy);

}
public dynamic lookatpoint(dynamic pos,dynamic lookatpoint) {
dynamic y_diff = null;
dynamic x_diff = null;
dynamic rotationanglerad = null;
dynamic fuckedupanglefix_parameter = null;
y_diff = (LingoGlobal.floatmember_helper(lookatpoint.locv)-LingoGlobal.floatmember_helper(pos.locv));
x_diff = (LingoGlobal.floatmember_helper(pos.loch)-LingoGlobal.floatmember_helper(lookatpoint.loch));
if ((x_diff != 0)) {
rotationanglerad = LingoGlobal.atan((y_diff/x_diff));
}
else {
rotationanglerad = (new LingoDecimal(1.5)*LingoGlobal.PI);
}
if ((lookatpoint.loch > pos.loch)) {
fuckedupanglefix_parameter = 0;
}
else {
fuckedupanglefix_parameter = LingoGlobal.PI;
}
rotationanglerad = (fuckedupanglefix_parameter-rotationanglerad);
return (((rotationanglerad*180)/LingoGlobal.PI)+90);

}
public dynamic degtovec(dynamic deg) {
dynamic rad = null;
deg = (deg+90);
deg = -deg;
rad = ((LingoGlobal.floatmember_helper((deg/new LingoDecimal(360)))*LingoGlobal.PI)*2);
return LingoGlobal.point(-LingoGlobal.cos(rad),LingoGlobal.sin(rad));

}
public dynamic closestpointonline(dynamic pnt,dynamic a,dynamic b) {
return givecrosspoint(pnt,(pnt+givedirfor90degrtoline(a,b)),a,b);

}
public dynamic givedirfor90degrtoline(dynamic pnt1,dynamic pnt2) {
dynamic x1 = null;
dynamic y1 = null;
dynamic x2 = null;
dynamic y2 = null;
dynamic ydiff = null;
dynamic xdiff = null;
dynamic dir = null;
dynamic newdir = null;
dynamic newpnt = null;
dynamic fac = null;
x1 = pnt1.loch;
y1 = pnt1.locv;
x2 = pnt2.loch;
y2 = pnt2.locv;
ydiff = (y1-y2);
xdiff = (x1-x2);
if ((xdiff != 0)) {
dir = (ydiff/xdiff);
}
else {
dir = 1;
}
if ((dir != 0)) {
newdir = (-new LingoDecimal(1)/dir);
}
else {
newdir = 1;
}
newpnt = LingoGlobal.point(1,newdir);
fac = 1;
if ((x2 < x1)) {
if ((y2 < y1)) {
fac = 1;
}
else {
fac = -1;
}
}
else if ((y2 < y1)) {
fac = 1;
}
else {
fac = -1;
}
newpnt = (newpnt*fac);
newpnt = (newpnt/diag(LingoGlobal.point(0,0),newpnt));
return newpnt;

}
public dynamic lnpntdist(dynamic pnt,dynamic linea,dynamic lineb) {
return diag(pnt,givecrosspoint(pnt,(pnt+givedirfor90degrtoline(linea,lineb)),linea,lineb));

}
public dynamic givecirclecolltime(dynamic pos1,dynamic r1,dynamic vel1,dynamic pos2,dynamic r2,dynamic vel2) {
dynamic x1 = null;
dynamic y1 = null;
dynamic x2 = null;
dynamic y2 = null;
dynamic vx1 = null;
dynamic vy1 = null;
dynamic vx2 = null;
dynamic vy2 = null;
dynamic a = null;
dynamic b = null;
dynamic c = null;
dynamic d = null;
dynamic e = null;
dynamic t = null;
x1 = pos1.loch;
y1 = pos1.locv;
x2 = pos2.loch;
y2 = pos2.locv;
vx1 = vel1.loch;
vy1 = vel1.locv;
vx2 = vel2.loch;
vy2 = vel2.locv;
a = ((((-x1*vx1)-(((((((y1*vy1)+vx1)*x2)+vy1)*y2)+x1)*vx2))-(((x2*vx2)+y1)*vy2))-(y2*vy2));
b = ((((-x1*vx1)-(((((((y1*vy1)+vx1)*x2)+vy1)*y2)+x1)*vx2))-(((x2*vx2)+y1)*vy2))-(y2*vy2));
c = (((LingoGlobal.power(vx1,2)+LingoGlobal.power(vy1,2))-(((2*vx1)*vx2)+LingoGlobal.power(vx2,2)))-(((2*vy1)*vy2)+LingoGlobal.power(vy2,2)));
d = ((((((LingoGlobal.power(x1,2)+LingoGlobal.power(y1,2))-LingoGlobal.power(r1,2))-(((2*x1)*x2)+LingoGlobal.power(x2,2)))-(((2*y1)*y2)+LingoGlobal.power(y2,2)))-((2*r1)*r2))-LingoGlobal.power(r2,2));
e = (((LingoGlobal.power(vx1,2)+LingoGlobal.power(vy1,2))-(((2*vx1)*vx2)+LingoGlobal.power(vx2,2)))-(((2*vy1)*vy2)+LingoGlobal.power(vy2,2)));
t = (((new LingoDecimal(2)*a)-LingoGlobal.sqrt((LingoGlobal.power((-new LingoDecimal(2)*b),2)-((new LingoDecimal(4)*c)*d))))/(new LingoDecimal(2)*e));
return t;

}
public dynamic lnpntdistnonabs(dynamic pnt,dynamic lnpnt1,dynamic lnpnt2) {
dynamic k = null;
dynamic m = null;
dynamic y1 = null;
dynamic x1 = null;
dynamic k2 = null;
dynamic d = null;
dynamic e = null;
dynamic f = null;
if (((lnpnt1.loch-lnpnt2.loch) != 0)) {
k = ((lnpnt1.locv-lnpnt2.locv)/(lnpnt1.loch-lnpnt2.loch));
}
else {
k = 0;
}
m = (lnpnt1.locv-(k*lnpnt1.loch));
y1 = pnt.locv;
x1 = pnt.loch;
if ((x1 != 0)) {
k2 = ((y1-m)/x1);
d = LingoGlobal.sqrt((LingoGlobal.power(LingoGlobal.abs((y1-m)),2)+LingoGlobal.power(x1,2)));
e = LingoGlobal.sin(LingoGlobal.atan(((k2-k)/((1+k2)*k))));
f = 1;
if ((k < 0)) {
f = -1;
}
return ((d*e)*f);
}
else {
return LingoGlobal.point(0,0);
}

return null;
}
public dynamic closestpntinrect(dynamic rct,dynamic pnt) {
dynamic respnt = null;
respnt = LingoGlobal.point(0,0);
if ((pnt.loch < rct.left)) {
if ((pnt.locv < rct.top)) {
respnt = LingoGlobal.point(rct.left,rct.top);
}
else if ((pnt.locv > rct.bottom)) {
respnt = LingoGlobal.point(rct.left,rct.bottom);
}
else {
respnt = LingoGlobal.point(rct.left,pnt.locv);
}
}
else if ((pnt.loch > rct.right)) {
if ((pnt.locv < rct.top)) {
respnt = LingoGlobal.point(rct.right,rct.top);
}
else if ((pnt.locv > rct.bottom)) {
respnt = LingoGlobal.point(rct.right,rct.bottom);
}
else {
respnt = LingoGlobal.point(rct.right,pnt.locv);
}
}
else if ((pnt.locv < rct.top)) {
respnt = LingoGlobal.point(pnt.loch,rct.top);
}
else if ((pnt.locv > rct.bottom)) {
respnt = LingoGlobal.point(pnt.loch,rct.bottom);
}
else {
respnt = pnt;
}
return respnt;

}
public dynamic anglebetweenlines(dynamic pnt1,dynamic pnt2,dynamic pnt3,dynamic pnt4) {
return (lookatpoint(pnt1,pnt2)-lookatpoint(pnt3,pnt4));

}
public dynamic compareangles(dynamic origo,dynamic pnt1,dynamic pnt2) {
dynamic ang = null;
pnt1 = (pnt1-origo);
pnt2 = (pnt2-origo);
pnt2 = rotatepntfromorigo(pnt2,LingoGlobal.point(0,0),lookatpoint(LingoGlobal.point(0,0),pnt1));
ang = lookatpoint(LingoGlobal.point(0,0),pnt2);
if ((ang > 180)) {
ang = LingoGlobal.abs((ang-360));
}
return ang;

}
public dynamic rotatepntfromorigo(dynamic pnt,dynamic org,dynamic rotat) {
dynamic realdir = null;
dynamic diag = null;
dynamic newdir = null;
dynamic vec = null;
dynamic rotatedpnt = null;
realdir = lookatpoint(org,pnt);
diag = diag(org,pnt);
newdir = (realdir-rotat);
vec = degtovec(newdir);
rotatedpnt = (org+(vec*diag));
return rotatedpnt;

}
public dynamic customadd(dynamic l,dynamic val) {
l.add(val);
return l;

}
public dynamic customsort(dynamic l) {
l.sort();
return l;

}
public dynamic insideline(dynamic pnt,dynamic a,dynamic b,dynamic rad) {
dynamic retrn = null;
dynamic dist = null;
dynamic hyp1 = null;
dynamic hyp2 = null;
dynamic maxdiag = null;
retrn = LingoGlobal.FALSE;
if ((diag(pnt,a) < rad)) {
retrn = LingoGlobal.TRUE;
}
else if ((diag(pnt,b) < rad)) {
retrn = LingoGlobal.TRUE;
}
if ((retrn == LingoGlobal.FALSE)) {
dist = LingoGlobal.abs(lnpntdistnonabs(pnt,a,b));
if ((dist < rad)) {
hyp1 = diag(a,b);
hyp2 = diag(a,(a+(givedirfor90degrtoline(a,b)*rad)));
maxdiag = LingoGlobal.sqrt((LingoGlobal.power(hyp1,2)+LingoGlobal.power(hyp2,2)));
if (((diag(pnt,a) < maxdiag) & (diag(pnt,b) < maxdiag))) {
retrn = LingoGlobal.TRUE;
}
}
}
return retrn;

}
public dynamic newmakelevel(dynamic lvlname) {
dynamic sz = null;
dynamic pos = null;
dynamic lightangle = null;
dynamic txt = null;
dynamic q = null;
dynamic mtrx = null;
dynamic c = null;
dynamic e = null;
dynamic foundfile = null;
dynamic i = null;
dynamic n = null;
dynamic filedeleter = null;
dynamic objfileio = null;
dynamic fileopener = null;
_global.put(LingoGlobal.concat_space(LingoGlobal.concat_space(@"saving:",lvlname),@"..."));
sz = (global_gloprops.size*20);
pos = LingoGlobal.point(0,0);
lightangle = (degtovec(global_glighteprops.lightangle)*global_glighteprops.flatness);
txt = @"";
txt = LingoGlobal.concat(txt,lvlname);
txt += txt.ToString();
txt = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(txt,((global_gloprops.size.loch-global_gloprops.extratiles[1])-global_gloprops.extratiles[3])),@"*"),((global_gloprops.size.locv-global_gloprops.extratiles[2])-global_gloprops.extratiles[4]));
if ((global_genveditorprops.waterlevel > -1)) {
txt = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(txt,@"|"),global_genveditorprops.waterlevel),@"|"),global_genveditorprops.waterinfront);
}
txt += txt.ToString();
txt = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(txt,lightangle.loch),@"*"),lightangle.locv),@"|0|0");
txt += txt.ToString();
for (int tmp_q = 1; tmp_q <= global_gcameraprops.cameras.count; tmp_q++) {
q = tmp_q;
txt += txt.ToString();
if ((q < global_gcameraprops.cameras.count)) {
txt += txt.ToString();
}
}
mtrx = _global.script(@"saveFile").changetoplaymatrix();
txt += txt.ToString();
if ((global_glevel.defaultterrain == 1)) {
txt = LingoGlobal.concat(txt,@"Border: Solid");
}
else {
txt = LingoGlobal.concat(txt,@"Border: Passable");
}
txt += txt.ToString();
for (int tmp_q = (1+global_gloprops.extratiles[1]); tmp_q <= (global_gloprops.size.loch-global_gloprops.extratiles[3]); tmp_q++) {
q = tmp_q;
for (int tmp_c = (1+global_gloprops.extratiles[2]); tmp_c <= (global_gloprops.size.locv-global_gloprops.extratiles[4]); tmp_c++) {
c = tmp_c;
if ((mtrx[q][c][1][2].getpos(9) > 0)) {
txt = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(txt,@"0,"),(q-global_gloprops.extratiles[1])),@","),(c-global_gloprops.extratiles[2])),@"|");
}
if ((mtrx[q][c][1][2].getpos(10) > 0)) {
txt = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(txt,@"1,"),(q-global_gloprops.extratiles[1])),@","),(c-global_gloprops.extratiles[2])),@"|");
}
}
}
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_q = (1+global_gloprops.extratiles[1]); tmp_q <= (global_gloprops.size.loch-global_gloprops.extratiles[3]); tmp_q++) {
q = tmp_q;
for (int tmp_c = (1+global_gloprops.extratiles[2]); tmp_c <= (global_gloprops.size.locv-global_gloprops.extratiles[4]); tmp_c++) {
c = tmp_c;
switch (mtrx[q][c][1][1]) {
case 1:
txt = LingoGlobal.concat(txt,@"1");
break;
case 2:
case 3:
case 4:
case 5:
txt = LingoGlobal.concat(txt,@"2");
break;
case 6:
txt = LingoGlobal.concat(txt,@"3");
break;
case 7:
txt = LingoGlobal.concat(txt,@"4,3");
break;
default:
txt = LingoGlobal.concat(txt,@"0");
break;
}
for (int tmp_e = 1; tmp_e <= mtrx[q][c][1][2].count; tmp_e++) {
e = tmp_e;
switch (mtrx[q][c][1][2][e]) {
case 2:
if ((mtrx[q][c][1][1] != 1)) {
txt = LingoGlobal.concat(txt,@",1");
}
break;
case 1:
if ((mtrx[q][c][1][1] != 1)) {
txt = LingoGlobal.concat(txt,@",2");
}
break;
case 5:
txt = LingoGlobal.concat(txt,@",3");
break;
case 6:
txt = LingoGlobal.concat(txt,@",4");
break;
case 7:
txt = LingoGlobal.concat(txt,@",5");
break;
case 19:
txt = LingoGlobal.concat(txt,@",9");
break;
case 21:
txt = LingoGlobal.concat(txt,@",12");
break;
case 3:
if (((afamvlvledit(LingoGlobal.point(q,c),1) == 0) & (afamvlvledit(LingoGlobal.point(q,(c+1)),1) == 1))) {
txt = LingoGlobal.concat(txt,@",7");
}
break;
case 18:
txt = LingoGlobal.concat(txt,@",8");
break;
case 13:
txt = LingoGlobal.concat(txt,@",10");
break;
case 20:
txt = LingoGlobal.concat(txt,@",11");
break;
}
}
if (((mtrx[q][c][1][1] != 1) & (mtrx[q][c][2][1] == 1))) {
txt = LingoGlobal.concat(txt,@",6");
}
txt = LingoGlobal.concat(txt,@"|");
}
}
foundfile = 0;
_global.put(txt);
for (int tmp_i = 1; tmp_i <= 1000; tmp_i++) {
i = tmp_i;
n = _global.getnthfilenameinfolder(LingoGlobal.concat(_global.the_moviePath,@"Levels"),i);
if ((n == LingoGlobal.EMPTY)) {
break;
}
if ((n == LingoGlobal.concat(lvlname,@".txt"))) {
foundfile = 1;
break;
}
}
_global.put((@"Found file: "+foundfile));
if ((foundfile == 1)) {
filedeleter = _global.@new(_global.xtra(@"fileio"));
filedeleter.openfile(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(_global.the_moviePath,@"Levels/"),lvlname),@".txt"),0);
filedeleter.delete();
_global.put(@"FILE DELETED!");
}
objfileio = _global.@new(_global.xtra(@"fileio"));
objfileio.createfile(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(_global.the_moviePath,@"Levels/"),lvlname),@".txt"));
objfileio.closefile();
fileopener = _global.@new(_global.xtra(@"fileio"));
fileopener.openfile(LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(_global.the_moviePath,@"Levels/"),lvlname),@".txt"),0);
for (int tmp_q = 1; tmp_q <= LingoGlobal.thenumberoflines_helper(txt); tmp_q++) {
q = tmp_q;
fileopener.writestring(LingoGlobal.lineof_helper(q,txt));
fileopener.writereturn(new LingoSymbol("windows"));
}
fileopener.closefile();
fileopener = LingoGlobal.VOID;
_global.put(LingoGlobal.concat_space(@"saved22:",lvlname));

return null;
}
public dynamic lerpvector(dynamic a,dynamic b,dynamic l) {
return LingoGlobal.point(lerp(a.loch,b.loch,l),lerp(a.locv,b.locv,l));

}
public dynamic seedoftile(dynamic tile) {
return ((global_gloprops.tileseed+(tile.locv*global_gloprops.size.loch))+tile.loch);

}
public dynamic bezier(dynamic a,dynamic ca,dynamic b,dynamic cb,dynamic f) {
dynamic middlecontrol = null;
middlecontrol = lerpvector(ca,cb,f);
ca = lerpvector(a,ca,f);
cb = lerpvector(cb,b,f);
ca = lerpvector(ca,middlecontrol,f);
cb = lerpvector(middlecontrol,cb,f);
return lerpvector(ca,cb,f);

}
}
}
