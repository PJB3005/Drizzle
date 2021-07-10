using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderProps
//
public sealed class renderProps : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
if (LingoGlobal.ToBool(_movieScript.global_gviewrender)) {
if (LingoGlobal.ToBool(_global._key.keypressed(48))) {
_global._movie.go(9);
}
me.newframe();
if (LingoGlobal.ToBool(_movieScript.global_keeplooping)) {
_global.go(_global.the_frame);
}
}
else {
while (LingoGlobal.ToBool(_movieScript.global_keeplooping)) {
me.newframe();
}
}

return null;
}
public dynamic newframe(dynamic me) {
dynamic propdata = null;
dynamic prop = null;
dynamic qd = null;
dynamic dp = null;
dynamic mdpoint = null;
dynamic savseed = null;
dynamic data = null;
if ((_movieScript.global_softprop != LingoGlobal.VOID)) {
rendersoftprop();
}
else if (LingoGlobal.ToBool(_movieScript.global_gcurrentlyrenderingtrash)) {
if ((_movieScript.global_c > _movieScript.global_grendertrashprops.count)) {
_movieScript.global_gcurrentlyrenderingtrash = LingoGlobal.FALSE;
if ((_movieScript.global_propstorender.count > 0)) {
_movieScript.global_c = 1;
propdata = _movieScript.global_propstorender[_movieScript.global_c];
}
else {
_movieScript.global_keeplooping = LingoGlobal.FALSE;
return null;
}
}
else {
propdata = _movieScript.global_grendertrashprops[_movieScript.global_c];
}
}
else if ((_movieScript.global_c > _movieScript.global_propstorender.count)) {
_movieScript.global_keeplooping = 0;
return null;
}
propdata = _movieScript.global_propstorender[_movieScript.global_c];
prop = _movieScript.global_gprops[propdata[3].loch].prps[propdata[3].locv];
if (LingoGlobal.ToBool(shouldthisproprender(prop,propdata[4],propdata[5].settings))) {
me.updatetext();
qd = propdata[4];
dp = -propdata[1];
if ((_movieScript.global_gcurrentlyrenderingtrash == LingoGlobal.FALSE)) {
qd = (qd*(new LingoDecimal(20)/new LingoDecimal(16)));
}
mdpoint = ((((qd[1]+qd[2])+qd[3])+qd[4])/new LingoDecimal(4));
savseed = _global.the_randomSeed;
_global.the_randomSeed = _movieScript.seedfortile(_movieScript.givegridpos(mdpoint),propdata[5].settings.seed);
if ((_movieScript.global_gcurrentlyrenderingtrash == LingoGlobal.FALSE)) {
qd = (qd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
}
if (LingoGlobal.ToBool(_movieScript.global_gcurrentlyrenderingtrash)) {
data = new LingoPropertyList {};
}
else {
data = _movieScript.global_propstorender[_movieScript.global_c][5];
}
renderprop(prop,dp,qd,mdpoint,data);
_global.the_randomSeed = savseed;
}
_movieScript.global_c = (_movieScript.global_c+1);

return null;
}
public dynamic shouldthisproprender(dynamic prop,dynamic qd,dynamic settings) {
dynamic mdpoint = null;
dynamic dig = null;
dynamic q = null;
if ((settings.rendertime != _movieScript.global_aftereffects)) {
return LingoGlobal.FALSE;
}
if ((_movieScript.global_gcurrentlyrenderingtrash == LingoGlobal.FALSE)) {
qd = (qd*(new LingoDecimal(20)/new LingoDecimal(16)));
qd = (qd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
}
mdpoint = ((((qd[1]+qd[2])+qd[3])+qd[4])/new LingoDecimal(4));
dig = 0;
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((_movieScript.diagwi(mdpoint,qd[q],dig) == LingoGlobal.FALSE)) {
dig = _movieScript.diag(mdpoint,qd[q]);
}
}
if ((_movieScript.diag(mdpoint,_movieScript.closestpntinrect(LingoGlobal.rect(-50,-100,2050,1100),mdpoint)) > dig)) {
return LingoGlobal.FALSE;
}
return LingoGlobal.TRUE;

}
public dynamic updatetext(dynamic me) {
dynamic txt = null;
dynamic viewprop = null;
dynamic prp = null;
dynamic propaddress = null;
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
viewprop = _movieScript.global_c;
if ((_movieScript.global_softprop != LingoGlobal.VOID)) {
viewprop = (_movieScript.global_c-1);
}
if ((_movieScript.global_gcurrentlyrenderingtrash == LingoGlobal.TRUE)) {
txt += txt.ToString();
txt += txt.ToString();
}
else {
for (int tmp_prp = 1; tmp_prp <= _movieScript.global_propstorender.count; tmp_prp++) {
prp = tmp_prp;
propaddress = _movieScript.global_propstorender[prp][3];
if (LingoGlobal.ToBool(shouldthisproprender(_movieScript.global_gprops[propaddress.loch].prps[propaddress.locv],_movieScript.global_propstorender[prp][4],_movieScript.global_propstorender[prp][5].settings))) {
if ((prp == viewprop)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
txt += txt.ToString();
}
}
}
_global.member(@"effectsL").text = txt;

return null;
}
public dynamic renderprop(dynamic prop,dynamic dp,dynamic qd,dynamic mdpoint,dynamic data) {
dynamic sav2 = null;
dynamic tileasprop = null;
dynamic q2 = null;
sav2 = _global.member(@"previewImprt");
if ((_movieScript.global_glastimported != prop.nm)) {
tileasprop = 0;
for (int tmp_q2 = 1; tmp_q2 <= prop.tags.count; tmp_q2++) {
q2 = tmp_q2;
if ((prop.tags[q2] == @"Tile")) {
tileasprop = 1;
break;
}
}
if (LingoGlobal.ToBool(tileasprop)) {
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Graphics\",prop.nm),@".png"));
}
else {
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Props\",prop.nm),@".png"));
}
sav2.name = @"previewImprt";
_movieScript.global_glastimported = prop.nm;
}
switch (prop.tp) {
case @"standard":
case @"variedStandard":
rendervoxelprop(prop,dp,qd,mdpoint,data);
break;
case @"simpleDecal":
case @"variedDecal":
_movieScript.global_ganydecals = 1;
renderdecal(prop,dp,qd,mdpoint,data);
break;
case @"rope":
renderrope(prop,_movieScript.global_propstorender[_movieScript.global_c][5],dp);
break;
case @"soft":
case @"variedSoft":
case @"antimatter":
initrendersoftprop(prop,qd,data,dp);
break;
case @"long":
renderlongprop(qd,prop,_movieScript.global_propstorender[_movieScript.global_c][5],dp);
break;
}
doproptags(prop,dp,qd);

return null;
}
public dynamic rendervoxelprop(dynamic prop,dynamic dp,dynamic qd,dynamic mdpoint,dynamic propdata) {
dynamic var = null;
dynamic ps = null;
dynamic sav2 = null;
dynamic colored = null;
dynamic q = null;
dynamic gtrect = null;
dynamic q2 = null;
dynamic dumpimg = null;
dynamic inverseimg = null;
dynamic b = null;
dynamic a = null;
var = 1;
if ((prop.tp == @"variedStandard")) {
var = propdata.settings.variation;
}
ps = 1;
sav2 = _global.member(@"previewImprt");
colored = LingoGlobal.op_gt(prop.tags.getpos(@"colored"),0);
if (LingoGlobal.ToBool(colored)) {
_movieScript.global_ganydecals = 1;
}
for (int tmp_q = 1; tmp_q <= prop.repeatl.count; tmp_q++) {
q = tmp_q;
gtrect = LingoGlobal.rect(0,0,(prop.sz.loch*20),(prop.sz.locv*20));
gtrect = ((gtrect+LingoGlobal.rect((gtrect.width*(var-1)),(gtrect.height*(ps-1)),(gtrect.width*(var-1)),(gtrect.height*(ps-1))))+LingoGlobal.rect(0,1,0,1));
for (int tmp_q2 = 1; tmp_q2 <= prop.repeatl[q]; tmp_q2++) {
q2 = tmp_q2;
switch (prop.colortreatment) {
case @"standard":
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(sav2.image,qd,gtrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case @"bevel":
dumpimg = _global.image(gtrect.width,gtrect.height,1);
dumpimg.copypixels(sav2.image,dumpimg.rect,gtrect);
inverseimg = _movieScript.makesilhouttefromimg(dumpimg,1);
dumpimg = _global.image(_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.width,_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.height,32);
dumpimg.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0)});
for (int tmp_b = 1; tmp_b <= prop.bevel; tmp_b++) {
b = tmp_b;
foreach (dynamic tmp_a in new LingoList(new dynamic[] { new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-1,-1) }),new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(0,-1) }),new LingoList(new dynamic[] { _global.color(255,0,0),LingoGlobal.point(-1,0) }),new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(1,1) }),new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(0,1) }),new LingoList(new dynamic[] { _global.color(0,0,255),LingoGlobal.point(1,0) }) })) {
a = tmp_a;
dumpimg.copypixels(inverseimg,(qd+new LingoList(new dynamic[] { (a[2]*b),(a[2]*b),(a[2]*b),(a[2]*b) })),inverseimg.rect,new LingoPropertyList {[new LingoSymbol("color")] = a[1],[new LingoSymbol("ink")] = 36});
}
}
dumpimg.copypixels(inverseimg,qd,inverseimg.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
inverseimg = _global.image(dumpimg.width,dumpimg.height,1);
inverseimg.copypixels(_global.member(@"pxl").image,inverseimg.rect,LingoGlobal.rect(0,0,1,1));
inverseimg.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
dumpimg.copypixels(inverseimg,dumpimg.rect,inverseimg.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(dumpimg,dumpimg.rect,dumpimg.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
}
if (LingoGlobal.ToBool(colored)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(dp)),@"dc")).image.copypixels(sav2.image,qd,(gtrect+LingoGlobal.rect((prop.sz.loch*20),0,(prop.sz.loch*20),0)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
dp = (dp+1);
if ((dp > 29)) {
break;
}
}
ps = (ps+1);
if ((dp > 29)) {
break;
}
}

return null;
}
public dynamic renderdecal(dynamic prop,dynamic dp,dynamic qd,dynamic mdpoint,dynamic data) {
dynamic rnd = null;
dynamic ps = null;
dynamic sav2 = null;
dynamic depthzero = null;
dynamic testdp = null;
dynamic dirq = null;
dynamic actualdepth = null;
dynamic averagesz = null;
dynamic getrect = null;
dynamic clr = null;
dynamic q = null;
rnd = 1;
ps = 1;
sav2 = _global.member(@"previewImprt");
depthzero = dp;
foreach (dynamic tmp_testDp in new LingoList(new dynamic[] { 0,10,20 })) {
testdp = tmp_testDp;
if (((dp <= testdp) & ((dp+prop.depth) > testdp))) {
depthzero = testdp;
break;
}
}
dirq = directionsquad();
actualdepth = prop.depth;
if (((dp+prop.depth) > 29)) {
actualdepth = (29-dp);
}
averagesz = ((((_movieScript.diag(qd[1],qd[2])+_movieScript.diag(qd[2],qd[3]))+_movieScript.diag(qd[3],qd[4]))+_movieScript.diag(qd[4],qd[1]))/new LingoDecimal(4));
averagesz = ((averagesz+new LingoDecimal(80))/new LingoDecimal(2));
averagesz = (averagesz/new LingoDecimal(12));
averagesz = (averagesz/((new LingoDecimal(4)+actualdepth)/new LingoDecimal(5)));
dirq = (dirq*averagesz);
getrect = sav2.image.rect;
if ((prop.tp == @"variedDecal")) {
getrect = (LingoGlobal.rect((prop.pxlsize.loch*(data.settings.variation-1)),0,(prop.pxlsize.loch*data.settings.variation),prop.pxlsize.locv)+LingoGlobal.rect(0,1,0,1));
}
clr = _global.color(0,0,0);
if ((data.settings.findpos(new LingoSymbol("color")) != LingoGlobal.VOID)) {
if ((data.settings.color > 0)) {
clr = _movieScript.global_gpecolors[data.settings.color][2];
}
}
for (int tmp_q = 1; tmp_q <= data.settings.customdepth; tmp_q++) {
q = tmp_q;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(dp)),@"dc")).image.copypixels(sav2.image,(qd+(dirq*(dp-depthzero))),getrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = clr});
dp = (dp+1);
if ((dp > 29)) {
break;
}
}

return null;
}
public dynamic directionsquad() {
dynamic qdirs = null;
dynamic frst = null;
dynamic l1 = null;
dynamic q = null;
qdirs = new LingoPropertyList {};
frst = _movieScript.degtovec(_global.random(360));
l1 = new LingoList(new dynamic[] { new LingoList(new dynamic[] { _global.random(100),frst }),new LingoList(new dynamic[] { _global.random(100),-frst }),new LingoList(new dynamic[] { _global.random(100),_movieScript.degtovec(_global.random(360)) }),new LingoList(new dynamic[] { _global.random(100),_movieScript.degtovec(_global.random(360)) }) });
l1.sort();
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
qdirs.add(l1[q][2]);
}
return qdirs;

}
public dynamic renderrope(dynamic prop,dynamic data,dynamic dp) {
dynamic lastpos = null;
dynamic lastdir = null;
dynamic lastperp = null;
dynamic q = null;
dynamic pos = null;
dynamic dir = null;
dynamic perp = null;
lastpos = data.points[1];
lastdir = _movieScript.movetopoint(data.points[1],data.points[2],new LingoDecimal(1));
lastperp = correctperp(lastdir);
for (int tmp_q = 1; tmp_q <= data.points.count; tmp_q++) {
q = tmp_q;
pos = data.points[q];
if ((q < data.points.count)) {
dir = _movieScript.movetopoint(data.points[q],data.points[(q+1)],new LingoDecimal(1));
}
else {
dir = lastdir;
}
perp = correctperp(dir);
renderropesegment(q,prop,data,dp,pos,dir,perp,lastpos,lastdir,lastperp);
lastpos = pos;
lastdir = dir;
lastperp = perp;
}

return null;
}
public dynamic correctperp(dynamic dir) {
return _movieScript.givedirfor90degrtoline((-dir+LingoGlobal.point(new LingoDecimal(0.001),-new LingoDecimal(0.001))),dir);

}
public dynamic renderropesegment(dynamic num,dynamic prop,dynamic data,dynamic dp,dynamic pos,dynamic dir,dynamic perp,dynamic lastpos,dynamic lastdir,dynamic lastperp) {
dynamic wdth = null;
dynamic pastqd = null;
dynamic a = null;
dynamic jointsize = null;
dynamic col = null;
dynamic myperp = null;
dynamic dr = null;
dynamic dst = null;
dynamic b = null;
dynamic pnta = null;
dynamic pntb = null;
dynamic aprp = null;
dynamic bprp = null;
dynamic pstdp = null;
dynamic mdpnt = null;
dynamic i = null;
dynamic possiblepositions = null;
dynamic uselastpos = null;
dynamic uselastdir = null;
dynamic uselastperp = null;
dynamic indx = null;
dynamic apos = null;
dynamic adp = null;
dynamic bpos = null;
dynamic bdp = null;
dynamic ahandle = null;
dynamic bhandle = null;
dynamic c2 = null;
dynamic cpos = null;
dynamic mnclamp = null;
dynamic a2 = null;
switch (prop.nm) {
case @"wire":
case @"Zero-G Wire":
wdth = (data.settings.thickness/new LingoDecimal(2));
pastqd = new LingoList(new dynamic[] { (pos-(perp*wdth)),((pos+perp)*wdth),((lastpos+lastperp)*wdth),(lastpos-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"pxl").image,pastqd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,0)});
break;
case @"tube":
wdth = new LingoDecimal(5);
pastqd = new LingoList(new dynamic[] { (pos-(perp*wdth)),((pos+perp)*wdth),((lastpos+lastperp)*wdth),(lastpos-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 30)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((dp+a)-1)))).image.copypixels(_global.member(@"tubeGraf").image,pastqd,LingoGlobal.rect(0,((a-1)*10),10,(a*10)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
break;
case @"ThickWire":
wdth = 2;
pastqd = new LingoList(new dynamic[] { (pos-(perp*wdth)),((pos+perp)*wdth),((lastpos+lastperp)*wdth),(lastpos-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 1; tmp_a <= 3; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 30)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((dp+a)-1)))).image.copypixels(_global.member(@"thickWireGraf").image,pastqd,LingoGlobal.rect(0,((a-1)*4),4,(a*4)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
break;
case @"RidgedTube":
wdth = 5;
pastqd = new LingoList(new dynamic[] { (pos-(perp*wdth)),((pos+perp)*wdth),((lastpos+lastperp)*wdth),(lastpos-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 30)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((dp+a)-1)))).image.copypixels(_global.member(@"ridgedTubeGraf").image,pastqd,LingoGlobal.rect(0,((a-1)*10),5,(a*10)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
break;
case @"Fuel Hose":
case @"Zero-G Tube":
wdth = 7;
jointsize = 6;
col = 0;
if ((prop.nm == @"Zero-G Tube")) {
wdth = 6;
jointsize = 4;
if ((data.settings.applycolor == 1)) {
col = 1;
_movieScript.global_ganydecals = 1;
}
}
myperp = lastperp;
pastqd = new LingoList(new dynamic[] { (pos-(myperp*wdth)),((pos+myperp)*wdth),((lastpos+myperp)*wdth),(lastpos-(myperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 1; tmp_a <= 5; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 30)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((dp+a)-1)))).image.copypixels(_global.member(@"fuelHoseGraf").image,pastqd,LingoGlobal.rect(0,((1+(a-1))*16),14,((1+a)*16)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
if ((col == 1)) {
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",_global.@string(((dp+a)-1))),@"dc")).image.copypixels(_global.member(@"fuelHoseCol").image,pastqd,LingoGlobal.rect(0,((1+(a-1))*16),14,((1+a)*16)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
break;
}
}
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 29)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+a)))).image.copypixels(_global.member(@"fuelHoseJoint").image,((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-jointsize,-jointsize,jointsize,jointsize))-LingoGlobal.rect((_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20))),LingoGlobal.rect(0,((1+(a-1))*12),12,((1+a)*12)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
break;
case @"Broken Fuel Hose":
dr = _movieScript.movetopoint(pos,lastpos,new LingoDecimal(1));
dst = _movieScript.diag(pos,lastpos);
for (int tmp_b = 0; tmp_b <= 2; tmp_b++) {
b = tmp_b;
wdth = 5;
pnta = (((pos+dr)*(dst/new LingoDecimal(3)))*b);
pntb = (((pos+dr)*(dst/new LingoDecimal(3)))*(b+1));
aprp = _movieScript.movetopoint(LingoGlobal.point(0,0),LingoGlobal.point(_movieScript.lerp(lastperp.loch,perp.loch,(b/new LingoDecimal(3))),_movieScript.lerp(lastperp.locv,perp.locv,(b/new LingoDecimal(3)))),new LingoDecimal(1));
bprp = _movieScript.movetopoint(LingoGlobal.point(0,0),LingoGlobal.point(_movieScript.lerp(lastperp.loch,perp.loch,((b+1)/new LingoDecimal(3))),_movieScript.lerp(lastperp.locv,perp.locv,((b+1)/new LingoDecimal(3)))),new LingoDecimal(1));
pastqd = new LingoList(new dynamic[] { (pnta-(aprp*wdth)),((pnta+aprp)*wdth),((pntb+bprp)*wdth),(pntb-(bprp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 2; tmp_a <= 5; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 29)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+a)))).image.copypixels(_global.member(@"ridgedTubeGraf").image,pastqd,LingoGlobal.rect(0,((a-1)*10),5,(a*10)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
}
if ((_global.random(5) < 4)) {
wdth = 7;
myperp = lastperp;
pastqd = new LingoList(new dynamic[] { (pos-(myperp*wdth)),((pos+myperp)*wdth),((lastpos+myperp)*wdth),(lastpos-(myperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 1; tmp_a <= 5; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 30)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((dp+a)-1)))).image.copypixels(_global.member(@"fuelHoseGraf").image,pastqd,LingoGlobal.rect(0,((1+(a-1))*16),14,((1+a)*16)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
for (int tmp_a = 1; tmp_a <= 4; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 29)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+a)))).image.copypixels(_global.member(@"fuelHoseJoint").image,((LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-6,-6,6,6))-LingoGlobal.rect((_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20))),LingoGlobal.rect(0,((1+(a-1))*12),12,((1+a)*12)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
}
break;
case @"Large Chain":
case @"Large Chain 2":
dr = _movieScript.movetopoint(pos,lastpos,new LingoDecimal(1));
dst = _movieScript.diag(pos,lastpos);
if (((num%2) == 0)) {
wdth = 10;
}
else {
wdth = new LingoDecimal(3.5);
}
pnta = ((lastpos+dr)*11);
pntb = (pos-(dr*11));
pastqd = new LingoList(new dynamic[] { (pnta-(lastperp*wdth)),((pnta+lastperp)*wdth),((pntb+lastperp)*wdth),(pntb-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
if ((prop.nm == @"Large Chain")) {
for (int tmp_a = 0; tmp_a <= 5; tmp_a++) {
a = tmp_a;
pstdp = _movieScript.restrict((dp+a),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGraf").image,pastqd,LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+a)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(a+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGrafHighLight").image,(pastqd+new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2) })),LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+a)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(a+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
pstdp = _movieScript.restrict(((dp+4)+a),0,29);
b = (5-a);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGraf").image,pastqd,LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+b)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(b+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGrafHighLight").image,(pastqd+new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2) })),LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+b)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(b+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
for (int tmp_a = 0; tmp_a <= 5; tmp_a++) {
a = tmp_a;
pstdp = _movieScript.restrict((dp+a),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGraf2").image,pastqd,LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+a)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(a+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGraf2HighLight").image,(pastqd+new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2) })),LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+a)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(a+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
pstdp = _movieScript.restrict(((dp+4)+a),0,29);
b = (5-a);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGraf2").image,pastqd,LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+b)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(b+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"largeChainGraf2HighLight").image,(pastqd+new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2) })),LingoGlobal.rect((LingoGlobal.op_eq((num%2),1)*20),((1+b)*50),((20+LingoGlobal.op_eq((num%2),1))*7),((1+(b+1))*50)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
break;
case @"Bike Chain":
dr = _movieScript.movetopoint(pos,lastpos,new LingoDecimal(1));
dst = _movieScript.diag(pos,lastpos);
wdth = 17;
pnta = ((lastpos+dr)*17);
pntb = (pos-(dr*17));
pastqd = new LingoList(new dynamic[] { (pnta-(lastperp*wdth)),((pnta+lastperp)*wdth),((pntb+lastperp)*wdth),(pntb-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
_movieScript.renderbeveledimage(_global.member(@"BikeChainBolt").image,dp,(new LingoList(new dynamic[] { (lastpos+LingoGlobal.point(-8,-8)),(lastpos+LingoGlobal.point(8,-8)),(lastpos+LingoGlobal.point(8,8)),(lastpos+LingoGlobal.point(-8,8)) })-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) })),2);
for (int tmp_a = 1; tmp_a <= 9; tmp_a++) {
a = tmp_a;
pstdp = _movieScript.restrict((dp+a),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"BikeChainBolt").image,((LingoGlobal.rect(lastpos,lastpos)+LingoGlobal.rect(-8,-8,8,8))-LingoGlobal.rect((_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20))),_global.member(@"BikeChainBolt").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
if (((num%2) == 0)) {
pstdp = _movieScript.restrict((dp+1),0,29);
_movieScript.renderbeveledimage(_global.member(@"BikeChainSegment").image,pstdp,pastqd,1);
pstdp = _movieScript.restrict((dp+2),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"BikeChainSegment").image,pastqd,_global.member(@"BikeChainSegment").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
pstdp = _movieScript.restrict((dp+8),0,29);
_movieScript.renderbeveledimage(_global.member(@"BikeChainSegment").image,pstdp,pastqd,1);
pstdp = _movieScript.restrict((dp+9),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"BikeChainSegment").image,pastqd,_global.member(@"BikeChainSegment").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
else {
pstdp = _movieScript.restrict((dp+3),0,29);
_movieScript.renderbeveledimage(_global.member(@"BikeChainSegment").image,pstdp,pastqd,1);
pstdp = _movieScript.restrict((dp+4),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"BikeChainSegment").image,pastqd,_global.member(@"BikeChainSegment").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
pstdp = _movieScript.restrict((dp+6),0,29);
_movieScript.renderbeveledimage(_global.member(@"BikeChainSegment").image,pstdp,pastqd,1);
pstdp = _movieScript.restrict((dp+7),0,29);
_global.member(LingoGlobal.concat(@"layer",_global.@string(pstdp))).image.copypixels(_global.member(@"BikeChainSegment").image,pastqd,_global.member(@"BikeChainSegment").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
break;
case @"Fat Hose":
wdth = 20;
pastqd = new LingoList(new dynamic[] { (pos-(perp*wdth)),((pos+perp)*wdth),((lastpos+lastperp)*wdth),(lastpos-(lastperp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 0; tmp_a <= 4; tmp_a++) {
a = tmp_a;
if ((((dp+a)+1) <= 29)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((dp+a)+1)))).image.copypixels(_global.member(@"fatHoseGraf").image,pastqd,LingoGlobal.rect(40,(a*40),80,((a+1)*40)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
pastqd = new LingoList(new dynamic[] { ((pos-(perp*wdth))-(dir*5)),(((pos+perp)*wdth)-(dir*5)),((((pos+perp)*wdth)+dir)*5),(pos-(((perp*wdth)+dir)*5)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
for (int tmp_a = 0; tmp_a <= 5; tmp_a++) {
a = tmp_a;
if (((dp+a) <= 29)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string((dp+a)))).image.copypixels(_global.member(@"fatHoseGraf").image,pastqd,LingoGlobal.rect(0,(a*10),40,((a+1)*10)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
mdpnt = ((pos+lastpos)/2);
mdpnt = (mdpnt-(_movieScript.global_grendercameratilepos*20));
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"fatHoseGraf").image,(LingoGlobal.rect(mdpnt,mdpnt)+LingoGlobal.rect(-5,-5,5,5)),LingoGlobal.rect(80,0,90,10),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case @"Wire Bunch":
case @"Wire Bunch 2":
if ((((num%2) == 0) | (num == data.points.count))) {
dr = _movieScript.movetopoint(pos,lastpos,new LingoDecimal(1));
if ((_movieScript.global_wirebunchsav == LingoGlobal.VOID)) {
_movieScript.global_wirebunchsav = new LingoPropertyList {};
_movieScript.global_wirebunchsav.add(new LingoList(new dynamic[] { lastpos,lastdir }));
for (int tmp_i = 1; tmp_i <= 19; tmp_i++) {
i = tmp_i;
_movieScript.global_wirebunchsav.add(_movieScript.degtovec(_global.random(360)));
}
}
possiblepositions = new LingoPropertyList {};
for (int tmp_i = 1; tmp_i <= 10; tmp_i++) {
i = tmp_i;
possiblepositions.add(_movieScript.degtovec(((LingoGlobal.floatmember_helper(i)/new LingoDecimal(10))*360)));
}
for (int tmp_i = 1; tmp_i <= 6; tmp_i++) {
i = tmp_i;
possiblepositions.add((_movieScript.degtovec(((LingoGlobal.floatmember_helper(i)/new LingoDecimal(6))*360))*new LingoDecimal(0.75)));
}
for (int tmp_i = 1; tmp_i <= 3; tmp_i++) {
i = tmp_i;
possiblepositions.add((_movieScript.degtovec(((LingoGlobal.floatmember_helper(i)/new LingoDecimal(3))*360))*new LingoDecimal(0.5)));
}
uselastpos = _movieScript.global_wirebunchsav[1][1];
uselastdir = _movieScript.global_wirebunchsav[1][2];
uselastperp = _movieScript.givedirfor90degrtoline(-uselastdir,uselastdir);
for (int tmp_i = 1; tmp_i <= 19; tmp_i++) {
i = tmp_i;
a = _movieScript.global_wirebunchsav[(i+1)];
indx = _global.random(possiblepositions.count);
b = possiblepositions[indx];
possiblepositions.deleteat(indx);
apos = (((uselastpos+uselastperp)*a.loch)*18);
adp = ((((dp+new LingoDecimal(2.5))+a.locv)*new LingoDecimal(2.5)).integer+1);
bpos = (((pos+perp)*b.loch)*18);
bdp = ((((dp+new LingoDecimal(2.5))+b.locv)*new LingoDecimal(2.5)).integer+1);
ahandle = ((apos+uselastdir)*_movieScript.lerp((_movieScript.diag(apos,bpos)/new LingoDecimal(2)),LingoGlobal.floatmember_helper((40+_global.random(40))),new LingoDecimal(0.5)));
bhandle = (bpos-(dir*_movieScript.lerp((_movieScript.diag(apos,bpos)/new LingoDecimal(2)),LingoGlobal.floatmember_helper((40+_global.random(40))),new LingoDecimal(0.5))));
c2 = _movieScript.lerpvector(a,b,new LingoDecimal(0.5));
cpos = (((lastpos+lastperp)*c2.loch)*18);
ahandle = _movieScript.lerpvector(ahandle,cpos,new LingoDecimal(0.5));
bhandle = _movieScript.lerpvector(bhandle,cpos,new LingoDecimal(0.5));
if ((_global.random(35) == 1)) {
bpos = ((((apos+uselastdir)*new LingoDecimal(60))+_movieScript.degtovec(_global.random(360)))*_global.random(60));
bhandle = _movieScript.lerpvector(bhandle,((bpos+_movieScript.degtovec(_global.random(360)))*_global.random(30)),new LingoDecimal(0.5));
}
else if ((_global.random(35) == 1)) {
apos = (bpos-(((dir*new LingoDecimal(60))+_movieScript.degtovec(_global.random(360)))*_global.random(60)));
ahandle = _movieScript.lerpvector(ahandle,((apos+_movieScript.degtovec(_global.random(360)))*_global.random(30)),new LingoDecimal(0.5));
}
drawbezierwire(lastdir,apos,ahandle,bpos,bhandle,adp,bdp);
_movieScript.global_wirebunchsav[(i+1)] = b;
}
_movieScript.global_wirebunchsav[1][1] = pos;
_movieScript.global_wirebunchsav[1][2] = dir;
}
wdth = 20;
pastqd = new LingoList(new dynamic[] { ((pos-(dr*new LingoDecimal(3.5)))-(perp*wdth)),(pos-(((dr*new LingoDecimal(3.5))+perp)*wdth)),((((pos+dr)*new LingoDecimal(3.5))+perp)*wdth),(((pos+dr)*new LingoDecimal(3.5))-(perp*wdth)) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
mnclamp = 0;
if ((dp >= 6)) {
mnclamp = 6;
}
for (int tmp_a2 = 0; tmp_a2 <= 10; tmp_a2++) {
a2 = tmp_a2;
a = (10-a2);
if ((prop.nm == @"Wire Bunch")) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(_movieScript.restrict(((dp+a)-1),mnclamp,29)))).image.copypixels(_global.member(@"wireBunchGraf").image,pastqd,LingoGlobal.rect(0,((1+a)*7),42,((1+(a+1))*7)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
_global.member(LingoGlobal.concat(@"layer",_global.@string(_movieScript.restrict(((dp+a)-1),mnclamp,29)))).image.copypixels(_global.member(@"wireBunchGraf2").image,pastqd,LingoGlobal.rect(0,(a*7),42,((a+1)*7)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
if ((num == data.points.count)) {
_movieScript.global_wirebunchsav = LingoGlobal.VOID;
}
break;
}

return null;
}
public dynamic drawbezierwire(dynamic startdir,dynamic a,dynamic ahandle,dynamic b,dynamic bhandle,dynamic adp,dynamic bdp) {
dynamic repeats = null;
dynamic lastdir = null;
dynamic lastpos = null;
dynamic lastperp = null;
dynamic i = null;
dynamic pos = null;
dynamic dir = null;
dynamic perp = null;
dynamic wdth = null;
dynamic pastqd = null;
dynamic mydp = null;
dynamic i2 = null;
repeats = (_movieScript.diag(a,b)/new LingoDecimal(5)).integer;
lastdir = startdir;
lastpos = (a-startdir);
lastperp = _movieScript.givedirfor90degrtoline(lastpos,a);
for (int tmp_i = 1; tmp_i <= repeats; tmp_i++) {
i = tmp_i;
pos = _movieScript.bezier(a,ahandle,b,bhandle,(LingoGlobal.floatmember_helper(i)/LingoGlobal.floatmember_helper(repeats)));
dir = _movieScript.movetopoint(lastpos,pos,new LingoDecimal(1));
perp = _movieScript.givedirfor90degrtoline(lastpos,pos);
wdth = 2;
pastqd = new LingoList(new dynamic[] { (pos-((perp*wdth)+dir)),(((pos+perp)*wdth)+dir),(((lastpos+lastperp)*wdth)-lastdir),((lastpos-(lastperp*wdth))-lastdir) });
pastqd = (pastqd-new LingoList(new dynamic[] { (_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20),(_movieScript.global_grendercameratilepos*20) }));
mydp = _movieScript.lerp(adp,bdp,(LingoGlobal.floatmember_helper(i)/LingoGlobal.floatmember_helper(repeats))).integer;
for (int tmp_i2 = 1; tmp_i2 <= 3; tmp_i2++) {
i2 = tmp_i2;
if (((mydp+i2) <= 30)) {
_global.member(LingoGlobal.concat(@"layer",_global.@string(((mydp+i2)-1)))).image.copypixels(_global.member(@"thickWireGraf").image,pastqd,LingoGlobal.rect(0,((i2-1)*4),4,(i2*4)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
else {
break;
}
}
lastpos = pos;
lastdir = dir;
lastperp = perp;
}

return null;
}
public dynamic initrendersoftprop(dynamic prop,dynamic qd,dynamic propdata,dynamic dp) {
dynamic lft = null;
dynamic tp = null;
dynamic rght = null;
dynamic bttm = null;
dynamic p = null;
dynamic pasterect = null;
dynamic offsetpnt = null;
dynamic getrect = null;
dynamic clr = null;
dynamic q = null;
lft = qd[1].loch;
tp = qd[1].locv;
rght = qd[1].loch;
bttm = qd[1].locv;
foreach (dynamic tmp_p in qd) {
p = tmp_p;
if ((p.loch < lft)) {
lft = p.loch;
}
if ((p.loch > rght)) {
rght = p.loch;
}
if ((p.locv < tp)) {
tp = p.locv;
}
if ((p.locv > bttm)) {
bttm = p.locv;
}
}
pasterect = LingoGlobal.rect(lft,tp,rght,bttm);
offsetpnt = LingoGlobal.point(lft,tp);
_global.member(@"softPropRender").image = _global.image(pasterect.width,pasterect.height,32);
getrect = _global.member(@"previewImprt").image.rect;
if ((prop.tp == @"variedSoft")) {
getrect = (LingoGlobal.rect(((propdata.settings.variation-1)*prop.pxlsize.loch),0,(propdata.settings.variation*prop.pxlsize.loch),prop.pxlsize.locv)+LingoGlobal.rect(0,1,0,1));
}
_global.member(@"softPropRender").image.copypixels(_global.member(@"previewImprt").image,(qd-new LingoList(new dynamic[] { offsetpnt,offsetpnt,offsetpnt,offsetpnt })),getrect);
if ((prop.tp == @"variedSoft")) {
if ((prop.colorize == 1)) {
if (LingoGlobal.ToBool(propdata.settings.applycolor)) {
_movieScript.global_ganydecals = LingoGlobal.TRUE;
_global.member(@"softPropColor").image = _global.image(pasterect.width,pasterect.height,32);
_global.member(@"softPropColor").image.copypixels(_global.member(@"previewImprt").image,(qd-new LingoList(new dynamic[] { offsetpnt,offsetpnt,offsetpnt,offsetpnt })),(getrect+LingoGlobal.rect(0,getrect.height,0,getrect.height)));
}
}
}
clr = 0;
if ((propdata.settings.findpos(new LingoSymbol("color")) != LingoGlobal.VOID)) {
if ((propdata.settings.color > 0)) {
clr = _movieScript.global_gpecolors[propdata.settings.color][2];
_movieScript.global_ganydecals = 1;
}
}
_movieScript.global_softprop = new LingoPropertyList {[new LingoSymbol("c")] = 0,[new LingoSymbol("pasterect")] = pasterect,[new LingoSymbol("prop")] = prop,[new LingoSymbol("propdata")] = propdata,[new LingoSymbol("dp")] = dp,[new LingoSymbol("clr")] = clr};
for (int tmp_q = 0; tmp_q <= 29; tmp_q++) {
q = tmp_q;
_global.sprite((50-q)).color = _global.color(0,0,0);
}

return null;
}
public dynamic rendersoftprop() {
dynamic q2 = null;
dynamic clr = null;
dynamic dpth = null;
dynamic renderfrom = null;
dynamic renderto = null;
dynamic painted = null;
dynamic d = null;
dynamic dp = null;
dynamic dir = null;
dynamic palcol = null;
dynamic ang = null;
dynamic a = null;
dynamic pnt = null;
dynamic dpthremove = null;
dynamic clrzclr = null;
dynamic q = null;
dynamic val = null;
for (int tmp_q2 = 0; tmp_q2 <= (_movieScript.global_softprop.pasterect.width-1); tmp_q2++) {
q2 = tmp_q2;
clr = _global.member(@"softPropRender").image.getpixel(q2,_movieScript.global_softprop.c);
if (((clr != _global.color(255,255,255)) & ((clr.green > 0) | (_movieScript.global_softprop.prop.tp == @"antimatter")))) {
dpth = (clr.green/new LingoDecimal(255));
if ((_movieScript.global_softprop.prop.tp == @"antimatter")) {
renderfrom = _movieScript.global_softprop.dp;
renderto = _movieScript.restrict(((_movieScript.global_softprop.dp+_movieScript.global_softprop.propdata.settings.customdepth)*(new LingoDecimal(1)-dpth)).integer,0,29);
painted = LingoGlobal.FALSE;
for (int tmp_d = renderfrom; tmp_d <= renderto; tmp_d++) {
d = tmp_d;
dp = _movieScript.restrict((renderto-(d+renderfrom)),0,29);
if ((_global.member(LingoGlobal.concat(@"layer",dp)).image.getpixel((q2+_movieScript.global_softprop.pasterect.left),(_movieScript.global_softprop.c+_movieScript.global_softprop.pasterect.top)) != _global.color(255,255,255))) {
_global.member(LingoGlobal.concat(@"layer",dp)).image.setpixel((q2+_movieScript.global_softprop.pasterect.left),(_movieScript.global_softprop.c+_movieScript.global_softprop.pasterect.top),_global.color(255,255,255));
if ((painted == LingoGlobal.FALSE)) {
foreach (dynamic tmp_clr in new LingoList(new dynamic[] { new LingoList(new dynamic[] { _global.color(255,0,0),-1 }),new LingoList(new dynamic[] { _global.color(0,0,255),1 }) })) {
clr = tmp_clr;
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(1,-1),LingoGlobal.point(0,1),LingoGlobal.point(2,0),LingoGlobal.point(2,-2),LingoGlobal.point(0,2) })) {
dir = tmp_dir;
if ((_global.member(LingoGlobal.concat(@"layer",dp)).image.getpixel((((q2+_movieScript.global_softprop.pasterect.left)+dir.loch)*clr[2]),(((_movieScript.global_softprop.c+_movieScript.global_softprop.pasterect.top)+dir.locv)*clr[2])) != _global.color(255,255,255))) {
_global.member(LingoGlobal.concat(@"layer",dp)).image.setpixel((((q2+_movieScript.global_softprop.pasterect.left)+dir.loch)*clr[2]),(((_movieScript.global_softprop.c+_movieScript.global_softprop.pasterect.top)+dir.locv)*clr[2]),clr[1]);
}
}
}
painted = LingoGlobal.TRUE;
}
}
}
}
else {
palcol = _global.color(0,255,0);
if ((_movieScript.global_softprop.prop.selfshade == 0)) {
if ((clr.blue > ((new LingoDecimal(255)/new LingoDecimal(3))*new LingoDecimal(2)))) {
palcol = _global.color(0,0,255);
}
else if ((clr.blue < (new LingoDecimal(255)/new LingoDecimal(3)))) {
palcol = _global.color(255,0,0);
}
}
else {
ang = new LingoDecimal(0);
for (int tmp_a = 1; tmp_a <= _movieScript.global_softprop.prop.smoothshading; tmp_a++) {
a = tmp_a;
foreach (dynamic tmp_pnt in new LingoList(new dynamic[] { LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1) })) {
pnt = tmp_pnt;
ang = ((ang+(dpth-softpropdepth((LingoGlobal.point(q2,_movieScript.global_softprop.c)-(pnt*a)))))+(softpropdepth(((LingoGlobal.point(q2,_movieScript.global_softprop.c)+pnt)*a))-dpth));
}
}
ang = (ang/(LingoGlobal.floatmember_helper(_movieScript.global_softprop.prop.smoothshading)*new LingoDecimal(3)));
ang = (ang*(new LingoDecimal(1)-(clr.red/new LingoDecimal(255))));
if ((((ang*new LingoDecimal(10))*LingoGlobal.power(dpth,_movieScript.global_softprop.prop.depthaffecthilites)) > _movieScript.global_softprop.prop.highlightborder)) {
palcol = _global.color(0,0,255);
}
else if (((-ang*new LingoDecimal(10)) > _movieScript.global_softprop.prop.shadowborder)) {
palcol = _global.color(255,0,0);
}
}
dpth = (new LingoDecimal(1)-dpth);
dpth = LingoGlobal.power(dpth,_movieScript.global_softprop.prop.contourexp);
dpthremove = (dpth*_movieScript.global_softprop.propdata.settings.customdepth);
renderfrom = 0;
renderto = 0;
if (LingoGlobal.ToBool(_movieScript.global_softprop.prop.round)) {
renderfrom = (_movieScript.global_softprop.dp+(dpthremove/new LingoDecimal(2)));
renderto = ((_movieScript.global_softprop.dp+_movieScript.global_softprop.propdata.settings.customdepth)-(dpthremove/new LingoDecimal(2)));
}
else {
renderfrom = (_movieScript.global_softprop.dp+dpthremove);
renderto = (_movieScript.global_softprop.dp+_movieScript.global_softprop.propdata.settings.customdepth);
}
renderfrom = _movieScript.lerp(renderfrom,(_movieScript.global_softprop.dp+dpthremove),(clr.red/new LingoDecimal(255)));
renderto = _movieScript.lerp(renderto,(_movieScript.global_softprop.dp+dpthremove),(clr.red/new LingoDecimal(255)));
for (int tmp_dp = _movieScript.restrict(renderfrom.integer,0,29); tmp_dp <= _movieScript.restrict(renderto.integer,0,29); tmp_dp++) {
dp = tmp_dp;
_global.member(LingoGlobal.concat(@"layer",dp)).image.setpixel((q2+_movieScript.global_softprop.pasterect.left),(_movieScript.global_softprop.c+_movieScript.global_softprop.pasterect.top),palcol);
}
clrzclr = _global.color(255,255,255);
if ((_movieScript.global_softprop.clr != 0)) {
clrzclr = _movieScript.global_softprop.clr;
}
else if ((_movieScript.global_softprop.prop.tp == @"variedSoft")) {
if ((_movieScript.global_softprop.prop.colorize == 1)) {
if (LingoGlobal.ToBool(_movieScript.global_softprop.propdata.settings.applycolor)) {
clrzclr = _global.member(@"softPropColor").image.getpixel(q2,_movieScript.global_softprop.c);
}
}
}
if ((clrzclr != _global.color(255,255,255))) {
for (int tmp_dp = _movieScript.restrict(renderfrom.integer,0,29); tmp_dp <= _movieScript.restrict(renderto.integer,0,29); tmp_dp++) {
dp = tmp_dp;
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",dp),@"dc")).image.setpixel((q2+_movieScript.global_softprop.pasterect.left),(_movieScript.global_softprop.c+_movieScript.global_softprop.pasterect.top),clrzclr);
}
}
}
}
}
_movieScript.global_softprop.c = (_movieScript.global_softprop.c+1);
if ((_movieScript.global_softprop.c >= _movieScript.global_softprop.pasterect.height)) {
for (int tmp_q = 0; tmp_q <= 29; tmp_q++) {
q = tmp_q;
val = ((LingoGlobal.floatmember_helper(q)+new LingoDecimal(1))/new LingoDecimal(30));
_global.sprite((50-q)).color = _global.color((val*255),(val*255),(val*255));
}
_movieScript.global_softprop = LingoGlobal.VOID;
}

return null;
}
public dynamic softpropdepth(dynamic pxl) {
dynamic clr = null;
clr = _global.member(@"softPropRender").image.getpixel(pxl.loch,pxl.locv);
if (((clr == _global.color(255,255,255)) | (clr == 0))) {
return new LingoDecimal(0);
}
return (clr.green/new LingoDecimal(255));

}
public dynamic renderlongprop(dynamic qd,dynamic prop,dynamic data,dynamic dp) {
dynamic a = null;
dynamic b = null;
dynamic dir = null;
dynamic perp = null;
dynamic dist = null;
dynamic mem = null;
dynamic totalsegments = null;
dynamic buffer = null;
dynamic qd2 = null;
dynamic d = null;
dynamic q = null;
dynamic pnt = null;
dynamic steps = null;
dynamic ornt = null;
dynamic degdir = null;
dynamic stp = null;
dynamic pos = null;
dynamic rct = null;
dynamic gtrect = null;
dynamic thirddist = null;
dynamic ps = null;
dynamic dr = null;
dynamic rodwidth = null;
dynamic e = null;
dynamic perpoffset = null;
dynamic sz = null;
dynamic wdth = null;
dynamic a2 = null;
a = ((qd[1]+qd[4])/new LingoDecimal(2));
b = ((qd[2]+qd[3])/new LingoDecimal(2));
dir = _movieScript.movetopoint(a,b,new LingoDecimal(1));
perp = correctperp(dir);
dist = _movieScript.diag(a,b);
switch (prop.nm) {
case @"Cabinet Clamp":
mem = _global.member(@"clampSegmentGraf");
totalsegments = ((dist/mem.image.height)-new LingoDecimal(0.5)).integer;
buffer = (dist-(totalsegments*mem.image.height));
qd2 = new LingoList(new dynamic[] { (a-(((perp*mem.image.width)*new LingoDecimal(0.5))+((dir*buffer)*new LingoDecimal(0.5)))),((a+((perp*mem.image.width)*new LingoDecimal(0.5)))+((dir*buffer)*new LingoDecimal(0.5))),(a+((perp*mem.image.width)*new LingoDecimal(0.5))),(a-((perp*mem.image.width)*new LingoDecimal(0.5))) });
_global.member(LingoGlobal.concat(@"layer",dp)).image.copypixels(_global.member(@"pxl").image,qd2,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0)});
qd2 = new LingoList(new dynamic[] { ((b-((perp*mem.image.width)*new LingoDecimal(0.5)))-((dir*buffer)*new LingoDecimal(0.5))),((b+((perp*mem.image.width)*new LingoDecimal(0.5)))-((dir*buffer)*new LingoDecimal(0.5))),(b+((perp*mem.image.width)*new LingoDecimal(0.5))),(b-((perp*mem.image.width)*new LingoDecimal(0.5))) });
_global.member(LingoGlobal.concat(@"layer",dp)).image.copypixels(_global.member(@"pxl").image,qd2,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0)});
d = (buffer/new LingoDecimal(2));
for (int tmp_q = 1; tmp_q <= totalsegments; tmp_q++) {
q = tmp_q;
pnt = ((a+d)*dir);
qd2 = new LingoList(new dynamic[] { (pnt-(((perp*mem.image.width)*new LingoDecimal(0.5))+(dir*mem.image.height))),((pnt+((perp*mem.image.width)*new LingoDecimal(0.5)))+(dir*mem.image.height)),(pnt+((perp*mem.image.width)*new LingoDecimal(0.5))),(pnt-((perp*mem.image.width)*new LingoDecimal(0.5))) });
_global.member(LingoGlobal.concat(@"layer",dp)).image.copypixels(mem.image,qd2,mem.image.rect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,255,0),[new LingoSymbol("ink")] = 36});
d = (d+mem.image.height);
}
mem = _global.member(@"clampBoltGraf");
_global.member(LingoGlobal.concat(@"layer",dp)).image.copypixels(mem.image,(LingoGlobal.rect(a,a)+LingoGlobal.rect((-mem.image.width/2),(-mem.image.height/2),(mem.image.width/2),(mem.image.height/2))),mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
_global.member(LingoGlobal.concat(@"layer",dp)).image.copypixels(mem.image,(LingoGlobal.rect(b,b)+LingoGlobal.rect((-mem.image.width/2),(-mem.image.height/2),(mem.image.width/2),(mem.image.height/2))),mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
break;
case @"Thick Chain":
steps = ((_movieScript.diag(a,b)/new LingoDecimal(12))+new LingoDecimal(0.4999)).integer;
ornt = (_global.random(2)-1);
degdir = _movieScript.lookatpoint(a,b);
stp = (_global.random(100)*new LingoDecimal(0.01));
for (int tmp_q = 1; tmp_q <= steps; tmp_q++) {
q = tmp_q;
pos = (a+((dir*12)*(q-stp)));
if (LingoGlobal.ToBool(ornt)) {
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-6,-10,6,10));
gtrect = LingoGlobal.rect(0,0,12,20);
ornt = 0;
}
else {
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-2,-10,2,10));
gtrect = LingoGlobal.rect(13,0,16,20);
ornt = 1;
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"bigChainSegment").image,_movieScript.rotatetoquad(rct,degdir),gtrect,new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,0,5),[new LingoSymbol("ink")] = 36});
}
break;
case @"Drill Suspender":
thirddist = (dist/new LingoDecimal(4));
for (int tmp_q = 1; tmp_q <= 2; tmp_q++) {
q = tmp_q;
ps = a;
dr = dir;
if ((q == 2)) {
ps = b;
dr = -dir;
}
qd = new LingoList(new dynamic[] { (ps-perp),(ps+perp),(((ps+dr)*thirddist)+perp),(((ps+dr)*thirddist)-perp) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+3),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
qd = new LingoList(new dynamic[] { (ps-(perp*2)),((ps+perp)*2),((((ps+dr)*new LingoDecimal(10))+perp)*2),(((ps+dr)*new LingoDecimal(10))-(perp*2)) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+3),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
rodwidth = new LingoDecimal(18);
qd = new LingoList(new dynamic[] { (((ps+dr)*thirddist)-(perp*rodwidth)),((((ps+dr)*thirddist)+perp)*rodwidth),((((ps+dr)*(thirddist-new LingoDecimal(2.5)))+perp)*rodwidth),(((ps+dr)*(thirddist-new LingoDecimal(2.5)))-(perp*rodwidth)) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+3),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
qd = new LingoList(new dynamic[] { (((ps+dr)*thirddist)-(perp*3)),((((ps+dr)*thirddist)+perp)*3),((((ps+dr)*thirddist)-(perp*3))-(dr*28)),(((((ps+dr)*thirddist)+perp)*3)-(dr*28)) });
qd = (qd+new LingoList(new dynamic[] { (dr*2),(dr*2),(dr*2),(dr*2) }));
for (int tmp_e = 0; tmp_e <= 2; tmp_e++) {
e = tmp_e;
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict(((dp+2)+e),0,29))).image.copypixels(_global.member(@"DrillSuspenderClamp").image,qd,LingoGlobal.rect(0,(_movieScript.restrict(e,0,1)*28),6,((_movieScript.restrict(e,0,1)+1)*28)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+2),0,29))).image.copypixels(_global.member(@"DrillSuspenderBolt").image,(LingoGlobal.rect(ps,ps)+LingoGlobal.rect(-3,-3,3,3)),LingoGlobal.rect(0,0,6,6),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
for (int tmp_e = 3; tmp_e <= 4; tmp_e++) {
e = tmp_e;
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+e),0,29))).image.copypixels(_global.member(@"DrillSuspenderBolt").image,(LingoGlobal.rect(ps,ps)+LingoGlobal.rect(-4,-4,4,4)),LingoGlobal.rect(0,6,8,14),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
for (int tmp_q = 1; tmp_q <= 2; tmp_q++) {
q = tmp_q;
perpoffset = -new LingoDecimal(10);
if ((q == 2)) {
perpoffset = new LingoDecimal(10);
}
rodwidth = new LingoDecimal(0.65);
qd = new LingoList(new dynamic[] { (((a+dir)*thirddist)-(perp*(-rodwidth+perpoffset))),(((a+dir)*thirddist)-(perp*(rodwidth+perpoffset))),((b-(dir*thirddist))-(perp*(rodwidth+perpoffset))),((b-(dir*thirddist))-(perp*(-rodwidth+perpoffset))) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+3),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
for (int tmp_e = 1; tmp_e <= 2; tmp_e++) {
e = tmp_e;
pos = (((a+dir)*thirddist)-(perp*perpoffset));
if ((e == 2)) {
pos = ((b-(dir*thirddist))-(perp*perpoffset));
}
for (int tmp_d = 0; tmp_d <= 5; tmp_d++) {
d = tmp_d;
sz = ((new LingoDecimal(3)+new LingoDecimal(7))*LingoGlobal.sin(((d/new LingoDecimal(5))*LingoGlobal.PI)));
qd = new LingoList(new dynamic[] { (((pos+dir)*sz)-perp),(((pos+dir)*sz)+perp),(pos-((dir*sz)+perp)),((pos-(dir*sz))-perp) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+d),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
}
}
}
break;
case @"Drill":
steps = ((_movieScript.diag(a,b)/new LingoDecimal(20))+new LingoDecimal(0.4999)).integer;
degdir = _movieScript.lookatpoint(a,b);
stp = (_global.random(100)*new LingoDecimal(0.01));
for (int tmp_q = 1; tmp_q <= steps; tmp_q++) {
q = tmp_q;
pos = (a+((dir*new LingoDecimal(20))*(q-stp)));
rct = (LingoGlobal.rect(pos,pos)+LingoGlobal.rect(-10,-10,10,10));
for (int tmp_e = 0; tmp_e <= 9; tmp_e++) {
e = tmp_e;
_global.member(LingoGlobal.concat(@"layer",_global.@string(_movieScript.restrict((dp+e),0,29)))).image.copypixels(_global.member(@"DrillGraf").image,_movieScript.rotatetoquad(rct,degdir),LingoGlobal.rect(0,(e*20),20,((e+1)*20)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
break;
case @"Piston":
dr = dir;
for (int tmp_d = 0; tmp_d <= 2; tmp_d++) {
d = tmp_d;
wdth = (3+d);
qd = new LingoList(new dynamic[] { (a-(perp*wdth)),((a+perp)*wdth),((b+perp)*wdth),(b-(perp*wdth)) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict(((dp+d)+1),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict(((dp+d)+1),0,29))).image.copypixels(_global.member(@"pistonHead").image,LingoGlobal.rect((a.loch-5),(a.locv-5),(a.loch+5),(a.locv+5)),_global.member(@"pistonHead").image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
wdth = 1;
qd = (new LingoList(new dynamic[] { ((a+dir)-(perp*wdth)),(((a+dir)+perp)*wdth),(b-((dir+perp)*wdth)),((b-dir)-(perp*wdth)) })+new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),LingoGlobal.point(-1,-1),LingoGlobal.point(-1,-1),LingoGlobal.point(-1,-1) }));
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+1),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
a2 = a;
if ((_movieScript.diag(a,b) > 200)) {
a2 = (b+_movieScript.movetopoint(b,a,new LingoDecimal(200)));
}
for (int tmp_d = 0; tmp_d <= 4; tmp_d++) {
d = tmp_d;
wdth = ((5+d)+LingoGlobal.op_gt(d,0));
qd = new LingoList(new dynamic[] { (a2-(perp*wdth)),((a2+perp)*wdth),((b+perp)*wdth),(b-(perp*wdth)) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+d),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
if ((d == 0)) {
wdth = 3;
qd = (new LingoList(new dynamic[] { (((a2+dir)*2)-(perp*wdth)),((((a2+dir)*2)+perp)*wdth),(b-(((dir*2)+perp)*wdth)),((b-(dir*2))-(perp*wdth)) })+new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2),LingoGlobal.point(-2,-2) }));
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict(dp,0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,255)});
}
qd = new LingoList(new dynamic[] { (a2-(perp*wdth)),((a2+perp)*wdth),((((a2+dir)*2)+perp)*wdth),(((a2+dir)*2)-(perp*wdth)) });
_global.member(LingoGlobal.concat(@"layer",_movieScript.restrict((dp+d),0,29))).image.copypixels(_global.member(@"pxl").image,qd,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,0)});
}
break;
}

return null;
}
public dynamic doproptags(dynamic prop,dynamic dp,dynamic qd) {
dynamic i = null;
dynamic img = null;
dynamic rnd = null;
dynamic mdpnt = null;
dynamic r = null;
for (int tmp_i = 1; tmp_i <= prop.tags.count; tmp_i++) {
i = tmp_i;
switch (prop.tags[i]) {
case @"Circular Sign":
img = _global.image(120,120,1);
rnd = _global.random(14);
img.copypixels(_global.member(@"circularSigns").image,LingoGlobal.rect(0,0,120,120),LingoGlobal.rect(((rnd-1)*120),(1+240),(rnd*120),((1+240)+120)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,0,0)});
mdpnt = ((((qd[1]+qd[2])+qd[3])+qd[4])/new LingoDecimal(4));
foreach (dynamic tmp_r in new LingoList(new dynamic[] { new LingoList(new dynamic[] { LingoGlobal.point(-1,-1),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-0,-1),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-1,-0),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(-2,-2),_global.color(0,0,255) }),new LingoList(new dynamic[] { LingoGlobal.point(1,1),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,1),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(1,0),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(2,2),_global.color(255,0,0) }),new LingoList(new dynamic[] { LingoGlobal.point(0,0),_global.color(0,255,0) }) })) {
r = tmp_r;
_global.member(LingoGlobal.concat(@"layer",_global.@string(_movieScript.restrict(dp,0,29)))).image.copypixels(img,((LingoGlobal.rect(-60,-60,60,60)+LingoGlobal.rect(mdpnt,mdpnt))+LingoGlobal.rect(r[1],r[1])),LingoGlobal.rect(0,0,120,120),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = r[2]});
}
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"circularSigns").image,(LingoGlobal.rect(-60,-60,60,60)+LingoGlobal.rect(mdpnt,mdpnt)),LingoGlobal.rect(((rnd-1)*120),(1+120),(rnd*120),(1+240)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(0,255,0)});
_global.member(LingoGlobal.concat(@"layer",_global.@string(dp))).image.copypixels(_global.member(@"circularSigns").image,(LingoGlobal.rect(-60,-60,60,60)+LingoGlobal.rect(mdpnt,mdpnt)),LingoGlobal.rect(((rnd-1)*120),1,(rnd*120),(1+120)),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,0,255)});
_movieScript.copypixelstoeffectcolor(@"A",dp,LingoGlobal.rect((mdpnt+LingoGlobal.point(-60,-60)),(mdpnt+LingoGlobal.point(60,60))),@"circleSignGrad",LingoGlobal.rect(0,1,120,121),new LingoDecimal(0.5),1);
break;
}
}

return null;
}
}
}
