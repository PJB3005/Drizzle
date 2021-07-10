using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: spelrelarat
//
public sealed partial class MovieScript {
public dynamic givegridpos(dynamic pos) {
return LingoGlobal.point(((LingoGlobal.floatmember_helper(pos.loch)/new LingoDecimal(20))+new LingoDecimal(0.4999)).integer,((LingoGlobal.floatmember_helper(pos.locv)/new LingoDecimal(20))+new LingoDecimal(0.4999)).integer);

}
public dynamic givemiddleoftile(dynamic pos) {
return LingoGlobal.point(((pos.loch*20)-10),((pos.locv*20)-10));

}
public dynamic restrict(dynamic val,dynamic low,dynamic high) {
if ((val < low)) {
return low;
}
else if ((val > high)) {
return high;
}
else {
return val;
}

return null;
}
public dynamic restrictwithflip(dynamic val,dynamic low,dynamic high) {
if ((val < low)) {
return ((val+(high-low))+1);
}
else if ((val > high)) {
return ((val-(high-low))-1);
}
else {
return val;
}

return null;
}
public dynamic afamvlvledit(dynamic pos,dynamic layer) {
if (LingoGlobal.ToBool(pos.inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
return global_gleprops.matrix[pos.loch][pos.locv][layer][1];
}
else {
return 1;
}

return null;
}
public dynamic solidafamv(dynamic pos,dynamic layer) {
if (LingoGlobal.ToBool(pos.inside(LingoGlobal.rect(1,1,(global_gloprops.size.loch+1),(global_gloprops.size.locv+1))))) {
return global_solidmtrx[pos.loch][pos.locv][layer];
}
else {
return 1;
}

return null;
}
public dynamic drawgraph() {
dynamic q = null;
dynamic fc = null;
dynamic val = null;
_global.member(@"grafImg").image = _global.image(300,100,1);
for (int tmp_q = 1; tmp_q <= 300; tmp_q++) {
q = tmp_q;
fc = (q/new LingoDecimal(300));
fc = (new LingoDecimal(1)-fc);
fc = (fc*fc);
fc = (new LingoDecimal(1)-fc);
val = LingoGlobal.sin((fc*LingoGlobal.PI));
_global.member(@"grafImg").image.setpixel(q,(100-(val*100)),_global.color(0,0,0));
}

return null;
}
public dynamic depthpnt(dynamic pnt,dynamic dpt) {
dpt = (dpt*-new LingoDecimal(0.025));
pnt = (pnt-LingoGlobal.point((1400/2),(800/3)));
dpt = ((10-dpt)*new LingoDecimal(0.1));
pnt = (pnt/dpt);
pnt = (pnt+LingoGlobal.point((1400/2),(800/3)));
return pnt;

}
public dynamic antidepthpnt(dynamic pnt,dynamic dpt) {
dpt = (dpt*-new LingoDecimal(0.025));
pnt = (pnt-LingoGlobal.point((1400/2),(800/3)));
dpt = ((10-dpt)*new LingoDecimal(0.1));
pnt = (pnt*dpt);
pnt = (pnt+LingoGlobal.point((1400/2),(800/3)));
return pnt;

}
public dynamic seedfortile(dynamic tile,dynamic effectseed) {
return (((effectseed+tile.loch)+tile.locv)*global_gleprops.matrix.count);

}
public dynamic copypixelstoeffectcolor(dynamic gdlayer,dynamic lr,dynamic rct,dynamic getmember,dynamic gtrect,dynamic zbleed,dynamic blnd) {
dynamic gtimg = null;
dynamic dmpimg = null;
dynamic nxt = null;
if ((blnd == LingoGlobal.VOID)) {
blnd = new LingoDecimal(1);
}
if (((gdlayer != @"C") & (blnd > 0))) {
lr = lr.integer;
if ((lr < 0)) {
lr = 0;
}
else if ((lr > 29)) {
lr = 29;
}
gtimg = _global.member(getmember).image;
if (((blnd != 0) & (blnd != LingoGlobal.VOID))) {
dmpimg = gtimg.duplicate();
dmpimg.copypixels(_global.member(@"pxl").image,dmpimg.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = (new LingoDecimal(100)*(new LingoDecimal(1)-blnd)),[new LingoSymbol("color")] = _global.color(255,255,255)});
gtimg = dmpimg;
}
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"gradient",gdlayer),_global.@string(lr))).image.copypixels(gtimg,rct,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 39});
if ((zbleed > 0)) {
if ((zbleed < 1)) {
dmpimg = gtimg.duplicate();
dmpimg.copypixels(_global.member(@"pxl").image,dmpimg.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = (new LingoDecimal(100)*(new LingoDecimal(1)-zbleed)),[new LingoSymbol("color")] = _global.color(255,255,255)});
gtimg = dmpimg;
}
nxt = (lr+1);
if ((nxt > 29)) {
nxt = 29;
}
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"gradient",gdlayer),_global.@string(nxt))).image.copypixels(gtimg,rct,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 39});
nxt = (lr-1);
if ((nxt < 0)) {
nxt = 0;
}
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"gradient",gdlayer),_global.@string(nxt))).image.copypixels(gtimg,rct,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 39});
}
}

return null;
}
public dynamic recolor(dynamic img,dynamic paloffset) {
dynamic col1 = null;
dynamic col2 = null;
dynamic col3 = null;
dynamic col4 = null;
dynamic col5 = null;
dynamic c = null;
dynamic r = null;
col1 = _global.member(@"currentPalette").image.getpixel((0+paloffset),0);
col2 = _global.member(@"currentPalette").image.getpixel((1+paloffset),0);
col3 = _global.member(@"currentPalette").image.getpixel((2+paloffset),0);
col4 = _global.member(@"currentPalette").image.getpixel((3+paloffset),0);
col5 = _global.member(@"currentPalette").image.getpixel((4+paloffset),0);
for (int tmp_c = 1; tmp_c <= img.width; tmp_c++) {
c = tmp_c;
for (int tmp_r = 1; tmp_r <= img.height; tmp_r++) {
r = tmp_r;
switch (_global.@string(img.getpixel((c-1),(r-1)))) {
case @"color( 248, 0, 0 )":
img.setpixel((c-1),(r-1),col1);
break;
case @"color( 0, 248, 0 )":
img.setpixel((c-1),(r-1),col2);
break;
case @"color( 0, 0, 248 )":
img.setpixel((c-1),(r-1),col3);
break;
case @"color( 248, 248, 0 )":
img.setpixel((c-1),(r-1),col4);
break;
case @"color( 0, 248, 248 )":
img.setpixel((c-1),(r-1),col5);
break;
}
}
}
return img;

}
public dynamic txttoimg(dynamic txt) {
dynamic totchars = null;
dynamic rws = null;
dynamic img = null;
dynamic pos = null;
dynamic q = null;
totchars = txt.length;
rws = ((totchars/new LingoDecimal(1040))+new LingoDecimal(0.5)).integer;
img = _global.image(1040,rws,32);
pos = LingoGlobal.point(0,1);
for (int tmp_q = 1; tmp_q <= txt.length; tmp_q++) {
q = tmp_q;
pos.loch = (pos.loch+1);
if ((pos.loch > 1040)) {
pos.loch = 1;
pos.locv = (pos.locv+1);
}
img.setpixel((pos.loch-1),(pos.locv-1),_global.color(_global.slice_helper(LingoGlobal.charmember_helper(txt),q,q).chartonum,0,0));
}
return img;

}
public dynamic imgtotxt(dynamic img) {
dynamic txt = null;
dynamic c = null;
dynamic q = null;
dynamic col = null;
txt = @"";
for (int tmp_c = 1; tmp_c <= img.rect.height; tmp_c++) {
c = tmp_c;
for (int tmp_q = 1; tmp_q <= 1040; tmp_q++) {
q = tmp_q;
col = img.getpixel((q-1),(c-1));
if ((col == _global.color(255,255,255))) {
break;
}
else {
txt += txt.ToString();
}
}
}
return txt;

}
public dynamic givedpfromlr(dynamic lr) {
dynamic rtrn = null;
rtrn = 1;
if ((lr >= 12)) {
rtrn = 4;
}
else if ((lr >= 8)) {
rtrn = 3;
}
else if ((lr >= 4)) {
rtrn = 2;
}
return rtrn;

}
public dynamic makesilhouttefromimg(dynamic img,dynamic inverted) {
dynamic inv = null;
inv = _global.image(img.width,img.height,1);
inv.copypixels(_global.member(@"pxl").image,img.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = 255});
inv.copypixels(img,img.rect,img.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
if ((inverted == 0)) {
inv = makesilhouttefromimg(inv,1);
}
return inv;

}
public dynamic blurimage(dynamic img,dynamic blurblend) {
dynamic opimg = null;
dynamic pnt = null;
dynamic l = null;
dynamic h = null;
dynamic v = null;
opimg = _global.image(img.width,img.height,32);
pnt = LingoGlobal.point(-2,-2);
l = new LingoPropertyList {};
for (int tmp_h = -3; tmp_h <= 3; tmp_h++) {
h = tmp_h;
for (int tmp_v = -3; tmp_v <= 3; tmp_v++) {
v = tmp_v;
l.add(new LingoList(new dynamic[] { (100-((diag(LingoGlobal.point(h,v),LingoGlobal.point(0,0))*10)+_global.random(9))),LingoGlobal.point(h,v) }));
}
}
l.sort();
foreach (dynamic tmp_pnt in l) {
pnt = tmp_pnt;
opimg.copypixels(img,img.rect,(img.rect+LingoGlobal.rect(pnt[2],pnt[2])),new LingoPropertyList {[new LingoSymbol("blend")] = ((_global.member(@"blurShape").image.getpixel((pnt[2].loch+3),(pnt[2].locv+3)).red/new LingoDecimal(255))*blurblend)});
}
return opimg;

}
public dynamic bluronblack(dynamic img,dynamic blurblend) {
dynamic opimg = null;
dynamic pnt = null;
dynamic l = null;
dynamic h = null;
dynamic v = null;
opimg = _global.image(img.width,img.height,32);
opimg.copypixels(_global.member(@"pxl").image,opimg.rect,LingoGlobal.rect(0,0,1,1));
pnt = LingoGlobal.point(-2,-2);
l = new LingoPropertyList {};
for (int tmp_h = -3; tmp_h <= 3; tmp_h++) {
h = tmp_h;
for (int tmp_v = -3; tmp_v <= 3; tmp_v++) {
v = tmp_v;
l.add(new LingoList(new dynamic[] { (100-((diag(LingoGlobal.point(h,v),LingoGlobal.point(0,0))*10)+_global.random(9))),LingoGlobal.point(h,v) }));
}
}
l.sort();
foreach (dynamic tmp_pnt in l) {
pnt = tmp_pnt;
opimg.copypixels(img,img.rect,(img.rect+LingoGlobal.rect(pnt[2],pnt[2])),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("blend")] = ((_global.member(@"blurShape").image.getpixel((pnt[2].loch+3),(pnt[2].locv+3)).red/new LingoDecimal(255))*blurblend)});
}
return opimg;

}
public dynamic rotatetoquad(dynamic rct,dynamic deg) {
dynamic dir = null;
dynamic midpnt = null;
dynamic toppnt = null;
dynamic bottompnt = null;
dynamic crossdir = null;
dynamic pnt1 = null;
dynamic pnt2 = null;
dynamic pnt3 = null;
dynamic pnt4 = null;
dir = degtovec(LingoGlobal.floatmember_helper(deg));
midpnt = LingoGlobal.point(((rct.left+rct.right)*new LingoDecimal(0.5)),((rct.top+rct.bottom)*new LingoDecimal(0.5)));
toppnt = (midpnt+((dir*rct.height)*new LingoDecimal(0.5)));
bottompnt = (midpnt-((dir*rct.height)*new LingoDecimal(0.5)));
crossdir = givedirfor90degrtoline(-dir,dir);
pnt1 = (toppnt+((crossdir*rct.width)*new LingoDecimal(0.5)));
pnt2 = (toppnt-((crossdir*rct.width)*new LingoDecimal(0.5)));
pnt3 = (bottompnt-((crossdir*rct.width)*new LingoDecimal(0.5)));
pnt4 = (bottompnt+((crossdir*rct.width)*new LingoDecimal(0.5)));
return new LingoList(new dynamic[] { pnt1,pnt2,pnt3,pnt4 });

}
public dynamic flipquadh(dynamic qd) {
return new LingoList(new dynamic[] { qd[2],qd[1],qd[4],qd[3] });

}
public dynamic inversekinematic(dynamic va,dynamic vc,dynamic a,dynamic b,dynamic flip) {
dynamic r = null;
dynamic alph = null;
r = diag(va,vc);
alph = (acos(restrict(((((r*r)+(a*a))-(b*b))/((new LingoDecimal(2)*r)*a)),new LingoDecimal(0.1),new LingoDecimal(0.99)))*((flip*new LingoDecimal(180))/LingoGlobal.PI));
return ((va+degtovec((lookatpoint(va,vc)+alph)))*a);

}
public dynamic acos(dynamic a) {
return (2*LingoGlobal.atan((LingoGlobal.sqrt((1-(a*a)))/(1+a))));

}
public dynamic depthchangeimage(dynamic img,dynamic dp) {
dynamic newimg = null;
newimg = _global.image(img.rect.width,img.rect.height,dp);
newimg.copypixels(img,img.rect,img.rect);
return newimg;

}
public dynamic pasteshortcuthole(dynamic mem,dynamic pnt,dynamic dp,dynamic cl) {
dynamic rct = null;
dynamic idstring = null;
dynamic dr = null;
dynamic ps = null;
dynamic cll = null;
dynamic c = null;
rct = ((givemiddleoftile(pnt)-(global_grendercameratilepos*20))-global_grendercamerapixelpos);
rct = depthpnt(rct,dp);
rct = (LingoGlobal.rect(rct,rct)+LingoGlobal.rect(-10,-10,10,10));
idstring = @"";
foreach (dynamic tmp_dr in new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dr = tmp_dr;
if (LingoGlobal.ToBool((pnt+dr).inside(LingoGlobal.rect(1,1,global_gloprops.size.loch,global_gloprops.size.locv)))) {
if (((global_gleprops.matrix[(pnt.loch+dr.loch)][(pnt.locv+dr.locv)][1][2].getpos(5) > 0) | (global_gleprops.matrix[(pnt.loch+dr.loch)][(pnt.locv+dr.locv)][1][2].getpos(4) > 0))) {
idstring = LingoGlobal.concat(idstring,@"1");
}
else {
idstring = LingoGlobal.concat(idstring,@"0");
}
}
else {
idstring = LingoGlobal.concat(idstring,@"0");
}
}
ps = new LingoList(new dynamic[] { @"0101",@"1010",@"1111",@"1100",@"0110",@"0011",@"1001",@"1110",@"0111",@"1011",@"1101",@"0000" }).getpos(idstring);
if ((cl == @"BORDER")) {
cll = new LingoPropertyList {};
cll.add(new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-1,0) }));
cll.add(new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(0,-1) }));
cll.add(new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-1,-1) }));
cll.add(new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-2,0) }));
cll.add(new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(0,-2) }));
cll.add(new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-2,-2) }));
cll.add(new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(1,0) }));
cll.add(new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(0,1) }));
cll.add(new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(1,1) }));
cll.add(new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(2,0) }));
cll.add(new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(0,2) }));
cll.add(new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(2,2) }));
}
else {
cll = new LingoList(new dynamic[] { new LingoList(new dynamic[] { cl,LingoGlobal.point(0,0) }) });
}
foreach (dynamic tmp_c in cll) {
c = tmp_c;
_global.member(mem).image.copypixels(_global.member(@"shortCutsGraf").image,(rct+LingoGlobal.rect(c[2],c[2])),LingoGlobal.rect((20*(ps-1)),1,(20*ps),21),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = c[1]});
}

return null;
}
public dynamic resizelevel(dynamic sze,dynamic addtilesleft,dynamic addtilestop) {
dynamic newmatrix = null;
dynamic newtematrix = null;
dynamic q = null;
dynamic ql = null;
dynamic c = null;
dynamic adder = null;
dynamic effect = null;
dynamic neweffmtrx = null;
dynamic oldimg = null;
newmatrix = new LingoPropertyList {};
newtematrix = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= (sze.loch+addtilesleft); tmp_q++) {
q = tmp_q;
ql = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= (sze.locv+addtilestop); tmp_c++) {
c = tmp_c;
if ((((((q-addtilesleft) <= global_gleprops.matrix.count) & ((c-addtilestop) <= global_gleprops.matrix[1].count)) & ((q-addtilesleft) > 0)) & ((c-addtilestop) > 0))) {
adder = global_gleprops.matrix[(q-addtilesleft)][(c-addtilestop)];
}
else {
adder = new LingoList(new dynamic[] { new LingoList(new dynamic[] { 1,new LingoPropertyList {} }),new LingoList(new dynamic[] { 1,new LingoPropertyList {} }),new LingoList(new dynamic[] { 1,new LingoPropertyList {} }) });
}
ql.add(adder);
}
newmatrix.add(ql);
}
for (int tmp_q = 1; tmp_q <= (sze.loch+addtilesleft); tmp_q++) {
q = tmp_q;
ql = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= (sze.locv+addtilestop); tmp_c++) {
c = tmp_c;
if ((((((q+addtilesleft) <= global_gteprops.tlmatrix.count) & ((c+addtilestop) <= global_gteprops.tlmatrix[1].count)) & ((q-addtilesleft) > 0)) & ((c-addtilestop) > 0))) {
adder = global_gteprops.tlmatrix[(q-addtilesleft)][(c-addtilestop)];
}
else {
adder = new LingoList(new dynamic[] { new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0},new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0},new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0} });
}
ql.add(adder);
}
newtematrix.add(ql);
}
foreach (dynamic tmp_effect in global_geeprops.effects) {
effect = tmp_effect;
neweffmtrx = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= (sze.loch+addtilesleft); tmp_q++) {
q = tmp_q;
ql = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= (sze.locv+addtilestop); tmp_c++) {
c = tmp_c;
if ((((((q+addtilesleft) <= effect.mtrx.count) & ((c+addtilestop) <= effect.mtrx[1].count)) & ((q-addtilesleft) > 0)) & ((c-addtilestop) > 0))) {
adder = effect.mtrx[(q-addtilesleft)][(c-addtilestop)];
}
else {
adder = 0;
}
ql.add(adder);
}
neweffmtrx.add(ql);
}
effect.mtrx = neweffmtrx;
}
global_gleprops.matrix = newmatrix;
global_gteprops.tlmatrix = newtematrix;
global_gloprops.size = (sze+LingoGlobal.point(addtilesleft,addtilestop));
global_glastdrawwasfullandmini = 0;
oldimg = _global.member(@"lightImage").image.duplicate();
_global.member(@"lightImage").image = _global.image(((global_gloprops.size.loch*20)+300),((global_gloprops.size.locv*20)+300),1);
_global.member(@"lightImage").image.copypixels(oldimg,oldimg.rect,oldimg.rect);

return null;
}
public dynamic resetgenveditorprops() {
global_genveditorprops = new LingoPropertyList {[new LingoSymbol("waterlevel")] = -1,[new LingoSymbol("waterinfront")] = 1,[new LingoSymbol("wavelength")] = 60,[new LingoSymbol("waveamplitude")] = 5,[new LingoSymbol("wavespeed")] = 10};

return null;
}
public dynamic resetpropeditorprops() {
global_gpeprops = new LingoPropertyList {[new LingoSymbol("props")] = new LingoPropertyList {},[new LingoSymbol("lastkeys")] = new LingoPropertyList {},[new LingoSymbol("keys")] = new LingoPropertyList {},[new LingoSymbol("worklayer")] = 1,[new LingoSymbol("lstmsps")] = LingoGlobal.point(0,0),[new LingoSymbol("pmPos")] = LingoGlobal.point(1,1),[new LingoSymbol("pmsavposl")] = new LingoPropertyList {},[new LingoSymbol("proprotation")] = 0,[new LingoSymbol("propstretchx")] = 1,[new LingoSymbol("propstretchy")] = 1,[new LingoSymbol("propflipx")] = 1,[new LingoSymbol("propflipy")] = 1,[new LingoSymbol("depth")] = 0,[new LingoSymbol("color")] = 0};

return null;
}
public dynamic copypixelstoeffectcolor(dynamic p1, dynamic p2, dynamic p3, dynamic p4, dynamic p5, dynamic p6) {
return copypixelstoeffectcolor(p1, p2, p3, p4, p5, p6, null);
}
public dynamic seedfortile(dynamic p1) {
return seedfortile(p1, null);
}
}
}
