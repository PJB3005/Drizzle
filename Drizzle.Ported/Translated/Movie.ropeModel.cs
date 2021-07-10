using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: ropeModel
//
public sealed partial class MovieScript {
public dynamic resetropemodel(dynamic me,dynamic pa,dynamic pb,dynamic prop,dynamic lengthfac,dynamic lr,dynamic rel) {
dynamic numberofsegments = null;
dynamic step = null;
dynamic i = null;
global_ropemodel = new LingoPropertyList {[new LingoSymbol("posa")] = pa,[new LingoSymbol("posb")] = pb,[new LingoSymbol("segmentlength")] = prop.segmentlength,[new LingoSymbol("grav")] = prop.grav,[new LingoSymbol("stiff")] = prop.stiff,[new LingoSymbol("release")] = rel,[new LingoSymbol("segments")] = new LingoPropertyList {},[new LingoSymbol("friction")] = prop.friction,[new LingoSymbol("airfric")] = prop.airfric,[new LingoSymbol("layer")] = lr,[new LingoSymbol("segrad")] = prop.segrad,[new LingoSymbol("rigid")] = prop.rigid,[new LingoSymbol("edgedirection")] = prop.edgedirection,[new LingoSymbol("selfpush")] = prop.selfpush,[new LingoSymbol("sourcePush")] = prop.sourcepush};
numberofsegments = ((diag(pa,pb)/prop.segmentlength)*lengthfac);
if ((numberofsegments < 3)) {
numberofsegments = 3;
}
step = (diag(pa,pb)/numberofsegments);
for (int tmp_i = 1; tmp_i <= numberofsegments; tmp_i++) {
i = tmp_i;
global_ropemodel.segments.add(new LingoPropertyList {[new LingoSymbol("pos")] = (pa+movetopoint(pa,pb,((i-new LingoDecimal(0.5))*step))),[new LingoSymbol("lastpos")] = (pa+movetopoint(pa,pb,((i-new LingoDecimal(0.5))*step))),[new LingoSymbol("vel")] = LingoGlobal.point(0,0)});
}

return null;
}
public dynamic modelropeupdate(dynamic me,dynamic preview,dynamic camerapos,dynamic previewscale) {
dynamic dir = null;
dynamic a = null;
dynamic fac = null;
dynamic idealfirstpos = null;
dynamic a1 = null;
dynamic i = null;
dynamic b = null;
dynamic dist = null;
dynamic mov = null;
dynamic adaptedpos = null;
if ((global_ropemodel.edgedirection > 0)) {
dir = movetopoint(global_ropemodel.posa,global_ropemodel.posb,new LingoDecimal(1));
if ((global_ropemodel.release > -1)) {
for (int tmp_A = 1; tmp_A <= (global_ropemodel.segments.count/2); tmp_A++) {
a = tmp_A;
fac = LingoGlobal.power((new LingoDecimal(1)-((LingoGlobal.floatmember_helper(a)-new LingoDecimal(1))/(global_ropemodel.segments.count/2))),2);
global_ropemodel.segments[a].vel = (((global_ropemodel.segments[a].vel+dir)*fac)*global_ropemodel.edgedirection);
}
idealfirstpos = ((global_ropemodel.posa+dir)*global_ropemodel.segmentlength);
global_ropemodel.segments[1].pos = LingoGlobal.point(lerp(global_ropemodel.segments[1].pos.loch,idealfirstpos.loch,global_ropemodel.edgedirection),lerp(global_ropemodel.segments[1].pos.locv,idealfirstpos.locv,global_ropemodel.edgedirection));
}
if ((global_ropemodel.release < 1)) {
for (int tmp_A1 = 1; tmp_A1 <= (global_ropemodel.segments.count/2); tmp_A1++) {
a1 = tmp_A1;
fac = LingoGlobal.power((new LingoDecimal(1)-((LingoGlobal.floatmember_helper(a1)-new LingoDecimal(1))/(global_ropemodel.segments.count/2))),2);
a = ((global_ropemodel.segments.count+1)-a1);
global_ropemodel.segments[a].vel = (global_ropemodel.segments[a].vel-((dir*fac)*global_ropemodel.edgedirection));
}
idealfirstpos = (global_ropemodel.posb-(dir*global_ropemodel.segmentlength));
global_ropemodel.segments[global_ropemodel.segments.count].pos = LingoGlobal.point(lerp(global_ropemodel.segments[global_ropemodel.segments.count].pos.loch,idealfirstpos.loch,global_ropemodel.edgedirection),lerp(global_ropemodel.segments[global_ropemodel.segments.count].pos.locv,idealfirstpos.locv,global_ropemodel.edgedirection));
}
}
if ((global_ropemodel.release > -1)) {
global_ropemodel.segments[1].pos = global_ropemodel.posa;
global_ropemodel.segments[1].vel = LingoGlobal.point(0,0);
}
if ((global_ropemodel.release < 1)) {
global_ropemodel.segments[global_ropemodel.segments.count].pos = global_ropemodel.posb;
global_ropemodel.segments[global_ropemodel.segments.count].vel = LingoGlobal.point(0,0);
}
for (int tmp_i = 1; tmp_i <= global_ropemodel.segments.count; tmp_i++) {
i = tmp_i;
global_ropemodel.segments[i].lastpos = global_ropemodel.segments[i].pos;
global_ropemodel.segments[i].pos = (global_ropemodel.segments[i].pos+global_ropemodel.segments[i].vel);
global_ropemodel.segments[i].vel = (global_ropemodel.segments[i].vel*global_ropemodel.airfric);
global_ropemodel.segments[i].vel.locv = (global_ropemodel.segments[i].vel.locv+global_ropemodel.grav);
}
for (int tmp_i = 2; tmp_i <= global_ropemodel.segments.count; tmp_i++) {
i = tmp_i;
connectropepoints(i,(i-1));
if ((global_ropemodel.rigid > 0)) {
applyrigidity(i);
}
}
for (int tmp_i = 2; tmp_i <= global_ropemodel.segments.count; tmp_i++) {
i = tmp_i;
a = (global_ropemodel.segments.count-(i+1));
connectropepoints(a,(a+1));
if ((global_ropemodel.rigid > 0)) {
applyrigidity(a);
}
}
if ((global_ropemodel.selfpush > 0)) {
for (int tmp_A = 1; tmp_A <= global_ropemodel.segments.count; tmp_A++) {
a = tmp_A;
for (int tmp_B = 1; tmp_B <= global_ropemodel.segments.count; tmp_B++) {
b = tmp_B;
if (((a != b) & LingoGlobal.ToBool(diagwi(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos,global_ropemodel.selfpush)))) {
dir = movetopoint(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos,new LingoDecimal(1));
dist = diag(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos);
mov = (dir*(dist-global_ropemodel.selfpush));
global_ropemodel.segments[a].pos = ((global_ropemodel.segments[a].pos+mov)*new LingoDecimal(0.5));
global_ropemodel.segments[a].vel = ((global_ropemodel.segments[a].vel+mov)*new LingoDecimal(0.5));
global_ropemodel.segments[b].pos = (global_ropemodel.segments[b].pos-(mov*new LingoDecimal(0.5)));
global_ropemodel.segments[b].vel = (global_ropemodel.segments[b].vel-(mov*new LingoDecimal(0.5)));
}
}
}
}
if ((global_ropemodel.sourcepush > 0)) {
for (int tmp_A = 1; tmp_A <= global_ropemodel.segments.count; tmp_A++) {
a = tmp_A;
global_ropemodel.segments[a].vel = ((global_ropemodel.segments[a].vel+movetopoint(global_ropemodel.posa,global_ropemodel.segments[a].pos,global_ropemodel.sourcepush))*restrict((((a-new LingoDecimal(1))/(global_ropemodel.segments.count-new LingoDecimal(1)))-new LingoDecimal(0.7)),0,1));
global_ropemodel.segments[a].vel = ((global_ropemodel.segments[a].vel+movetopoint(global_ropemodel.posb,global_ropemodel.segments[a].pos,global_ropemodel.sourcepush))*restrict(((new LingoDecimal(1)-((a-new LingoDecimal(1))/(global_ropemodel.segments.count-new LingoDecimal(1))))-new LingoDecimal(0.7)),0,1));
}
}
for (int tmp_i = (1+LingoGlobal.op_gt(global_ropemodel.release,-1)); tmp_i <= (global_ropemodel.segments.count-LingoGlobal.op_lt(global_ropemodel.release,1)); tmp_i++) {
i = tmp_i;
pushropepointoutofterrain(i);
}
if (LingoGlobal.ToBool(preview)) {
_global.member(@"ropePreview").image.copypixels(_global.member(@"pxl").image,_global.member(@"ropePreview").image.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255)});
for (int tmp_i = 1; tmp_i <= global_ropemodel.segments.count; tmp_i++) {
i = tmp_i;
adaptedpos = me.smoothedpos(i);
adaptedpos = (adaptedpos-(camerapos*new LingoDecimal(20)));
adaptedpos = (adaptedpos*previewscale);
_global.member(@"ropePreview").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((adaptedpos-LingoGlobal.point(1,1)),(adaptedpos+LingoGlobal.point(2,2))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(0,0,0)});
}
}

return null;
}
public dynamic connectropepoints(dynamic a,dynamic b) {
dynamic dir = null;
dynamic dist = null;
dynamic mov = null;
dir = movetopoint(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos,new LingoDecimal(1));
dist = diag(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos);
if (((global_ropemodel.stiff == 1) | (dist > global_ropemodel.segmentlength))) {
mov = (dir*(dist-global_ropemodel.segmentlength));
global_ropemodel.segments[a].pos = ((global_ropemodel.segments[a].pos+mov)*new LingoDecimal(0.5));
global_ropemodel.segments[a].vel = ((global_ropemodel.segments[a].vel+mov)*new LingoDecimal(0.5));
global_ropemodel.segments[b].pos = (global_ropemodel.segments[b].pos-(mov*new LingoDecimal(0.5)));
global_ropemodel.segments[b].vel = (global_ropemodel.segments[b].vel-(mov*new LingoDecimal(0.5)));
}

return null;
}
public dynamic applyrigidity(dynamic a) {
dynamic b2 = null;
dynamic b = null;
dynamic dir = null;
foreach (dynamic tmp_B2 in new LingoList(new dynamic[] { -2,2,-3,3,-4,4 })) {
b2 = tmp_B2;
b = (a+b2);
if (((b > 0) & (b <= global_ropemodel.segments.count))) {
dir = movetopoint(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos,new LingoDecimal(1));
global_ropemodel.segments[a].vel = (global_ropemodel.segments[a].vel-(((dir*global_ropemodel.rigid)*global_ropemodel.segmentlength)/((diag(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos)+new LingoDecimal(0.1))+LingoGlobal.abs(b2))));
global_ropemodel.segments[b].vel = ((global_ropemodel.segments[b].vel+((dir*global_ropemodel.rigid)*global_ropemodel.segmentlength))/((diag(global_ropemodel.segments[a].pos,global_ropemodel.segments[b].pos)+new LingoDecimal(0.1))+LingoGlobal.abs(b2)));
}
}

return null;
}
public dynamic smoothedpos(dynamic me,dynamic a) {
dynamic smoothpos = null;
if ((a == 1)) {
if ((global_ropemodel.release > -1)) {
return global_ropemodel.posa;
}
else {
return global_ropemodel.segments[a].pos;
}
}
else if ((a == global_ropemodel.segments.count)) {
if ((global_ropemodel.release < 1)) {
return global_ropemodel.posb;
}
else {
return global_ropemodel.segments[a].pos;
}
}
else {
smoothpos = ((global_ropemodel.segments[(a-1)].pos+global_ropemodel.segments[(a+1)].pos)/new LingoDecimal(2));
return ((global_ropemodel.segments[a].pos+smoothpos)/new LingoDecimal(2));
}

return null;
}
public dynamic pushropepointoutofterrain(dynamic a) {
dynamic p = null;
dynamic gridpos = null;
dynamic dir = null;
dynamic midpos = null;
dynamic terrainpos = null;
dynamic dist = null;
dynamic mov = null;
p = new LingoPropertyList {[new LingoSymbol("loc")] = global_ropemodel.segments[a].pos,[new LingoSymbol("lastloc")] = global_ropemodel.segments[a].lastpos,[new LingoSymbol("frc")] = global_ropemodel.segments[a].vel,[new LingoSymbol("sizepnt")] = LingoGlobal.point(global_ropemodel.segrad,global_ropemodel.segrad)};
p = sharedcheckvcollision(p,global_ropemodel.friction,global_ropemodel.layer);
global_ropemodel.segments[a].pos = p.loc;
global_ropemodel.segments[a].vel = p.frc;
gridpos = givegridpos(global_ropemodel.segments[a].pos);
foreach (dynamic tmp_dir in new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(-1,0),LingoGlobal.point(-1,-1),LingoGlobal.point(0,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,0),LingoGlobal.point(1,1),LingoGlobal.point(0,1),LingoGlobal.point(-1,1) })) {
dir = tmp_dir;
if ((afamvlvledit((gridpos+dir),global_ropemodel.layer) == 1)) {
midpos = givemiddleoftile((gridpos+dir));
terrainpos = LingoGlobal.point(restrict(global_ropemodel.segments[a].pos.loch,(midpos.loch-10),(midpos.loch+10)),restrict(global_ropemodel.segments[a].pos.locv,(midpos.locv-10),(midpos.locv+10)));
terrainpos = (((terrainpos*new LingoDecimal(10))+midpos)/new LingoDecimal(11));
dir = movetopoint(global_ropemodel.segments[a].pos,terrainpos,new LingoDecimal(1));
dist = diag(global_ropemodel.segments[a].pos,terrainpos);
if ((dist < global_ropemodel.segrad)) {
mov = (dir*(dist-global_ropemodel.segrad));
global_ropemodel.segments[a].pos = (global_ropemodel.segments[a].pos+mov);
global_ropemodel.segments[a].vel = (global_ropemodel.segments[a].vel+mov);
}
}
}

return null;
}
public dynamic sharedcheckvcollision(dynamic p,dynamic friction,dynamic layer) {
dynamic bounce = null;
dynamic lastgridpos = null;
dynamic feetpos = null;
dynamic lastfeetpos = null;
dynamic leftpos = null;
dynamic rightpos = null;
dynamic q = null;
dynamic c = null;
dynamic headpos = null;
dynamic lastheadpos = null;
dynamic d = null;
bounce = 0;
if ((p.frc.locv > 0)) {
lastgridpos = givegridpos(p.lastloc);
feetpos = givegridpos((p.loc+LingoGlobal.point(0,(p.sizepnt.locv+new LingoDecimal(0.01)))));
lastfeetpos = givegridpos((p.lastloc+LingoGlobal.point(0,p.sizepnt.locv)));
leftpos = givegridpos((p.loc+LingoGlobal.point((-p.sizepnt.loch+1),(p.sizepnt.locv+new LingoDecimal(0.01)))));
rightpos = givegridpos((p.loc+LingoGlobal.point((p.sizepnt.loch-1),(p.sizepnt.locv+new LingoDecimal(0.01)))));
for (int tmp_q = lastfeetpos.locv; tmp_q <= feetpos.locv; tmp_q++) {
q = tmp_q;
for (int tmp_c = leftpos.loch; tmp_c <= rightpos.loch; tmp_c++) {
c = tmp_c;
if (((afamvlvledit(LingoGlobal.point(c,q),layer) == 1) & (afamvlvledit(LingoGlobal.point(c,(q-1)),layer) != 1))) {
if (((lastgridpos.locv >= q) & (afamvlvledit(lastgridpos,layer) == 1))) {
}
else {
p.loc.locv = (((q-1)*new LingoDecimal(20))-p.sizepnt.locv);
p.frc.loch = (p.frc.loch*friction);
p.frc.locv = (-p.frc.locv*bounce);
return p;
return null;
}
}
}
}
}
else if ((p.frc.locv < 0)) {
lastgridpos = givegridpos(p.lastloc);
headpos = givegridpos((p.loc-LingoGlobal.point(0,(p.sizepnt.locv+new LingoDecimal(0.01)))));
lastheadpos = givegridpos((p.lastloc-LingoGlobal.point(0,p.sizepnt.locv)));
leftpos = givegridpos((p.loc+LingoGlobal.point((-p.sizepnt.loch+1),(p.sizepnt.locv+new LingoDecimal(0.01)))));
rightpos = givegridpos((p.loc+LingoGlobal.point((p.sizepnt.loch-1),(p.sizepnt.locv+new LingoDecimal(0.01)))));
for (int tmp_d = headpos.locv; tmp_d <= lastheadpos.locv; tmp_d++) {
d = tmp_d;
q = (lastheadpos.locv-(d-headpos.locv));
for (int tmp_c = leftpos.loch; tmp_c <= rightpos.loch; tmp_c++) {
c = tmp_c;
if (((afamvlvledit(LingoGlobal.point(c,q),layer) == 1) & (afamvlvledit(LingoGlobal.point(c,(q+1)),layer) != 1))) {
if (((lastgridpos.locv <= q) & (afamvlvledit(lastgridpos,layer) != 1))) {
}
else {
p.loc.locv = ((q*new LingoDecimal(20))+p.sizepnt.locv);
p.frc.loch = (p.frc.loch*friction);
p.frc.locv = (-p.frc.locv*bounce);
return p;
return null;
}
}
}
}
}
return p;

}
}
}
