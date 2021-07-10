using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: OldRenderColors
//
public sealed class OldRenderColors : LingoBehaviorScript {
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
_movieScript.global_keeplooping = 1;

return null;
}
public dynamic newframe(dynamic me) {
dynamic l = null;
dynamic q = null;
dynamic cl = null;
dynamic dp = null;
dynamic fgdp = null;
dynamic whiteness = null;
dynamic usenextcolorfac = null;
dynamic lr = null;
dynamic shdw = null;
dynamic gtps = null;
dynamic colour = null;
dynamic onestepfurthercolor = null;
dynamic clr1fac = null;
dynamic clr2fac = null;
dynamic darkdown = null;
dynamic fogfac = null;
dynamic rainbowfac = null;
dynamic dsplc = null;
dynamic otherfogfac = null;
dynamic rainbowdisplace = null;
dynamic rainbowcolor = null;
dynamic recolored = null;
dynamic mem = null;
dynamic inv = null;
l = new LingoList(new dynamic[] { _global.color(255,0,0),_global.color(0,255,0),_global.color(0,0,255),_global.color(255,0,255),_global.color(0,255,255),_global.color(255,255,0) });
_global.sprite(59).locv = (_movieScript.global_c-8);
for (int tmp_q = 1; tmp_q <= 1040; tmp_q++) {
q = tmp_q;
cl = _global.member(@"finalfg").image.getpixel((q-1),(_movieScript.global_c-1));
dp = _movieScript.global_dptsl.getpos(_global.member(@"dpImage").image.getpixel((q-1),(_movieScript.global_c-1)));
fgdp = _movieScript.global_fogdptsl.getpos(_global.member(@"fogImage").image.getpixel((q-1),(_movieScript.global_c-1)));
whiteness = 0;
usenextcolorfac = 0;
if (LingoGlobal.ToBool(new LingoList(new dynamic[] { 4,8,12 }).getpos(fgdp))) {
usenextcolorfac = new LingoDecimal(0.66);
}
else if (LingoGlobal.ToBool(new LingoList(new dynamic[] { 3,7,11 }).getpos(fgdp))) {
usenextcolorfac = new LingoDecimal(0.33);
}
lr = 0;
if ((cl != _global.color(255,255,255))) {
if ((cl.blue == 150)) {
whiteness = (cl.red/new LingoDecimal(255));
cl = 1;
}
else {
cl = l.getpos(cl);
}
lr = 1;
}
else {
cl = _global.member(@"finalbg").image.getpixel((q-1),(_movieScript.global_c-1));
if ((cl != _global.color(255,255,255))) {
if ((cl == _global.color(5,5,5))) {
if (((_movieScript.global_gbluroptions.blurlight == 0) & (_movieScript.global_gbluroptions.blursky == 0))) {
_global.member(@"finalbg").image.setpixel((q-1),(_movieScript.global_c-1),_global.color(255,255,255));
_global.member(@"finalbgLight").image.setpixel((q-1),(_movieScript.global_c-1),_global.color(255,255,255));
}
}
if ((cl.blue == 150)) {
whiteness = (cl.red/new LingoDecimal(255));
cl = 1;
}
else {
cl = l.getpos(cl);
}
lr = 2;
}
else if (LingoGlobal.ToBool(_movieScript.global_gbluroptions.blursky)) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),_movieScript.global_gskycolor);
}
}
if ((cl != _global.color(255,255,255))) {
shdw = LingoGlobal.op_eq(_global.member(@"shadowImage").image.getpixel((q-1),(_movieScript.global_c-1)),_global.color(0,0,0));
gtps = LingoGlobal.point(dp,cl);
}
else {
gtps = LingoGlobal.point(1,1);
cl = 1;
}
if (((cl != 0) & (dp != 0))) {
colour = _movieScript.global_gpalette[gtps.loch][gtps.locv];
onestepfurthercolor = _movieScript.global_gpalette[(gtps.loch+LingoGlobal.op_lt(gtps.loch,4))][gtps.locv];
if ((cl == 4)) {
clr1fac = (_global.member(LingoGlobal.concat(@"gradientA",_global.@string(dp))).image.getpixel((q-1),(_movieScript.global_c-1)).red/new LingoDecimal(255));
colour = ((colour*clr1fac)+(_movieScript.global_geffectpalettea[gtps.loch]*(new LingoDecimal(1)-clr1fac)));
onestepfurthercolor = ((onestepfurthercolor*clr1fac)+(_movieScript.global_geffectpalettea[(gtps.loch+LingoGlobal.op_lt(gtps.loch,4))]*(new LingoDecimal(1)-clr1fac)));
}
else if ((cl == 5)) {
clr2fac = (_global.member(LingoGlobal.concat(@"gradientB",_global.@string(dp))).image.getpixel((q-1),(_movieScript.global_c-1)).red/new LingoDecimal(255));
colour = ((colour*clr2fac)+(_movieScript.global_geffectpaletteb[gtps.loch]*(new LingoDecimal(1)-clr2fac)));
onestepfurthercolor = ((onestepfurthercolor*clr2fac)+(_movieScript.global_geffectpaletteb[(gtps.loch+LingoGlobal.op_lt(gtps.loch,4))]*(new LingoDecimal(1)-clr2fac)));
}
colour = ((colour*(new LingoDecimal(1)-usenextcolorfac))+(onestepfurthercolor*usenextcolorfac));
darkdown = 0;
fogfac = ((255-_global.member(@"fogImage").image.getpixel((q-1),(_movieScript.global_c-1)).red)/new LingoDecimal(255));
fogfac = ((fogfac-new LingoDecimal(0.0275))*(new LingoDecimal(1)/new LingoDecimal(0.9411)));
rainbowfac = 0;
if ((_global.member(@"blackOutImg2").image.getpixel((q-1),(_movieScript.global_c-1)) == _global.color(255,255,255))) {
if ((fogfac <= new LingoDecimal(0.2))) {
foreach (dynamic tmp_dsplc in new LingoList(new dynamic[] { LingoGlobal.point(-2,0),LingoGlobal.point(0,-2),LingoGlobal.point(2,0),LingoGlobal.point(0,2),LingoGlobal.point(-1,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,1),LingoGlobal.point(-1,1) })) {
dsplc = tmp_dsplc;
otherfogfac = ((255-_global.member(@"fogImage").image.getpixel(_movieScript.restrict((q-(1+dsplc.loch)),0,1039),_movieScript.restrict((_movieScript.global_c-(1+dsplc.locv)),0,799)).red)/new LingoDecimal(255));
otherfogfac = ((otherfogfac-new LingoDecimal(0.0275))*(new LingoDecimal(1)/new LingoDecimal(0.9411)));
rainbowfac = ((rainbowfac+LingoGlobal.op_gt(LingoGlobal.abs((fogfac-otherfogfac)),new LingoDecimal(0.0333)))*(_movieScript.restrict((fogfac-otherfogfac),0,1)+1));
if ((rainbowfac > 5)) {
break;
}
}
rainbowfac = LingoGlobal.op_gt(rainbowfac,5);
if (LingoGlobal.ToBool(rainbowfac)) {
foreach (dynamic tmp_dsplc in new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })) {
dsplc = tmp_dsplc;
_global.member(@"rainBowMask").image.setpixel((q-(1+dsplc.loch)),(_movieScript.global_c-(1+dsplc.locv)),_global.color(255,255,255));
}
}
}
rainbowdisplace = (_global.member(@"noiseGraf").image.getpixel((q-1),(_movieScript.global_c-1)).red/new LingoDecimal(255));
rainbowcolor = _global.member(@"rainBow").image.getpixel(((90*((fogfac*new LingoDecimal(0.4))+(rainbowdisplace*new LingoDecimal(1))))%10),0);
_global.member(@"rainBowImage").image.setpixel((q-1),(_movieScript.global_c-1),rainbowcolor);
}
fogfac = (fogfac*new LingoDecimal(0.7));
colour = _global.color(((colour.red*(new LingoDecimal(1)-whiteness))+(250*whiteness)),((colour.green*(new LingoDecimal(1)-whiteness))+(250*whiteness)),((colour.blue*(new LingoDecimal(1)-whiteness))+(250*whiteness)));
colour = _global.color(_movieScript.restrict((colour.red-darkdown),0,255),_movieScript.restrict((colour.green-darkdown),0,255),_movieScript.restrict((colour.blue-darkdown),0,255));
if ((_movieScript.global_gcustomcolor != LingoGlobal.VOID)) {
colour = me.addcustomcolor(colour,q,_movieScript.global_c,fogfac,0,gtps.locv,fgdp);
}
if ((lr == 1)) {
_global.member(@"finalfg").image.setpixel((q-1),(_movieScript.global_c-1),((colour*(new LingoDecimal(1)-fogfac))+(_movieScript.global_gfogcolor*fogfac)));
_global.member(@"finalbg").image.setpixel((q-1),(_movieScript.global_c-1),_global.color(255,255,255));
recolored = 1;
}
else if ((lr == 2)) {
_global.member(@"finalbg").image.setpixel((q-1),(_movieScript.global_c-1),((colour*(new LingoDecimal(1)-fogfac))+(_movieScript.global_gfogcolor*fogfac)));
recolored = 1;
}
if ((((clr1fac < new LingoDecimal(1)) & (_movieScript.global_gloprops.colglows[1] == 2)) & (cl == 4))) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),(_movieScript.global_geffectpalettea[gtps.loch]*(new LingoDecimal(1)-clr1fac)));
}
if ((((clr2fac < new LingoDecimal(1)) & (_movieScript.global_gloprops.colglows[2] == 2)) & (cl == 5))) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),(_movieScript.global_geffectpaletteb[gtps.loch]*(new LingoDecimal(1)-clr2fac)));
}
if ((shdw == 0)) {
gtps = LingoGlobal.point(dp,cl);
colour = _movieScript.global_gpalette[(gtps.loch+4)][gtps.locv];
onestepfurthercolor = _movieScript.global_gpalette[((gtps.loch+LingoGlobal.op_lt(gtps.loch,4))+4)][gtps.locv];
if ((cl == 4)) {
colour = ((colour*clr1fac)+(_movieScript.global_geffectpalettea[(gtps.loch+4)]*(new LingoDecimal(1)-clr1fac)));
onestepfurthercolor = ((onestepfurthercolor*clr1fac)+(_movieScript.global_geffectpalettea[((gtps.loch+LingoGlobal.op_lt(gtps.loch,4))+4)]*(new LingoDecimal(1)-clr1fac)));
}
else if ((cl == 5)) {
colour = ((colour*clr2fac)+(_movieScript.global_geffectpaletteb[(gtps.loch+4)]*(new LingoDecimal(1)-clr2fac)));
onestepfurthercolor = ((onestepfurthercolor*clr2fac)+(_movieScript.global_geffectpaletteb[((gtps.loch+LingoGlobal.op_lt(gtps.loch,4))+4)]*(new LingoDecimal(1)-clr2fac)));
}
colour = ((colour*(new LingoDecimal(1)-usenextcolorfac))+(onestepfurthercolor*usenextcolorfac));
colour = _global.color(((colour.red*(new LingoDecimal(1)-whiteness))+(250*whiteness)),((colour.green*(new LingoDecimal(1)-whiteness))+(250*whiteness)),((colour.blue*(new LingoDecimal(1)-whiteness))+(250*whiteness)));
colour = _global.color(_movieScript.restrict((colour.red-darkdown),0,255),_movieScript.restrict((colour.green-darkdown),0,255),_movieScript.restrict((colour.blue-darkdown),0,255));
if ((_movieScript.global_gcustomcolor != LingoGlobal.VOID)) {
colour = me.addcustomcolor(colour,q,_movieScript.global_c,fogfac,1,gtps.locv,fgdp);
}
if ((_movieScript.global_gbluroptions.blurlight == 0)) {
if ((((clr1fac < new LingoDecimal(1)) & (_movieScript.global_gloprops.colglows[1] == 1)) & (cl == 4))) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),((_movieScript.global_geffectpalettea[gtps.loch]*(new LingoDecimal(1)-clr1fac))*new LingoDecimal(0.5)));
}
if ((((clr2fac < new LingoDecimal(1)) & (_movieScript.global_gloprops.colglows[2] == 1)) & (cl == 5))) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),((_movieScript.global_geffectpaletteb[gtps.loch]*(new LingoDecimal(1)-clr2fac))*new LingoDecimal(0.5)));
}
}
if ((lr == 1)) {
_global.member(@"finalfgLight").image.setpixel((q-1),(_movieScript.global_c-1),((colour*(new LingoDecimal(1)-fogfac))+(_movieScript.global_gfogcolor*fogfac)));
me.changelightrect(1,LingoGlobal.point(q,_movieScript.global_c));
if (LingoGlobal.ToBool(_movieScript.global_gbluroptions.blurlight)) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),((colour*(new LingoDecimal(1)-fogfac))+(_movieScript.global_gfogcolor*fogfac)));
}
}
else if ((lr == 2)) {
_global.member(@"finalbgLight").image.setpixel((q-1),(_movieScript.global_c-1),((colour*(new LingoDecimal(1)-fogfac))+(_movieScript.global_gfogcolor*fogfac)));
me.changelightrect(2,LingoGlobal.point(q,_movieScript.global_c));
if (LingoGlobal.ToBool(_movieScript.global_gbluroptions.blurlight)) {
_global.member(@"blurredLight").image.setpixel((q-1),(_movieScript.global_c-1),((colour*(new LingoDecimal(1)-fogfac))+(_movieScript.global_gfogcolor*fogfac)));
}
}
}
}
}
_movieScript.global_c = (_movieScript.global_c+1);
if ((_movieScript.global_c > 800)) {
foreach (dynamic tmp_mem in new LingoList(new dynamic[] { @"finalfg",@"finalbg",@"finalbgLight",@"finalfgLight" })) {
mem = tmp_mem;
inv = _movieScript.makesilhouttefromimg(_global.member(mem).image,1);
_global.member(@"tempRainBowImage").image.copypixels(_global.member(@"rainBowImage").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800));
_global.member(@"tempRainBowImage").image.copypixels(inv,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(@"tempRainBowImage").image.copypixels(_global.member(@"rainBowMask").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("color")] = _global.color(255,255,255)});
_global.member(mem).image.copypixels(_global.member(@"tempRainBowImage").image,LingoGlobal.rect(0,0,1040,800),LingoGlobal.rect(0,0,1040,800),new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("blend")] = 10});
}
if ((_movieScript.global_gloprops.pals[_movieScript.global_gloprops.pal].name == @"Dark Light Sky")) {
_global.member(@"blurredLight").image = _movieScript.bluronblack(_global.member(@"blurredLight").image,40);
for (int tmp_q = 1; tmp_q <= 20; tmp_q++) {
q = tmp_q;
_global.member(@"blurredLight").image = _movieScript.blurimage(_global.member(@"blurredLight").image,40);
}
}
else {
_global.member(@"blurredLight").image = _movieScript.bluronblack(_global.member(@"blurredLight").image,20);
for (int tmp_q = 1; tmp_q <= 10; tmp_q++) {
q = tmp_q;
_global.member(@"blurredLight").image = _movieScript.blurimage(_global.member(@"blurredLight").image,40);
}
}
_movieScript.global_c = 1;
_movieScript.global_keeplooping = 0;
}

return null;
}
public dynamic changelightrect(dynamic me,dynamic lr,dynamic pnt) {
if ((pnt.loch < _movieScript.global_lightrects[lr].left)) {
_movieScript.global_lightrects[lr].left = pnt.loch;
}
if ((pnt.loch > _movieScript.global_lightrects[lr].right)) {
_movieScript.global_lightrects[lr].right = pnt.loch;
}
if ((pnt.locv < _movieScript.global_lightrects[lr].top)) {
_movieScript.global_lightrects[lr].top = pnt.locv;
}
if ((pnt.locv > _movieScript.global_lightrects[lr].bottom)) {
_movieScript.global_lightrects[lr].bottom = pnt.locv;
}
_global.sprite((10+lr)).rect = (_movieScript.global_lightrects[lr]+LingoGlobal.rect(-8,-16,-8,-16));

return null;
}
public dynamic addcustomcolor(dynamic me,dynamic colour,dynamic q,dynamic c,dynamic fogfac,dynamic lit,dynamic tnt,dynamic fgdp) {
dynamic origcl = null;
dynamic pxl = null;
dynamic customcol = null;
dynamic mlt = null;
if (((LingoGlobal.op_eq(_movieScript.global_gcustomcolor[2].getpos(fgdp),0) == LingoGlobal.op_eq(_movieScript.global_gcustomcolor[1],0)) | (_movieScript.global_gcustomcolor[3].getpos(fgdp) > 0))) {
origcl = _global.color(colour.red,colour.green,colour.blue);
pxl = (_movieScript.depthpnt(LingoGlobal.point(q,c),(15-(fogfac*45)))+LingoGlobal.point((tnt*1),(tnt*2)));
if (LingoGlobal.ToBool(pxl.inside(LingoGlobal.rect(1,1,1040,800)))) {
customcol = _global.member(@"previewImprt").image.getpixel((pxl.loch-1),(pxl.locv-1));
if ((customcol == _global.color(255,0,255))) {
_global.member(@"rainBowMask").image.setpixel((q-1),(c-1),_global.color(255,255,255));
}
else if ((customcol != _global.color(0,0,0))) {
mlt = _global.color((_movieScript.restrict(((colour.red/new LingoDecimal(255))*(customcol.red/new LingoDecimal(255))),0,new LingoDecimal(1))*255),(_movieScript.restrict(((colour.green/new LingoDecimal(255))*(customcol.green/new LingoDecimal(255))),0,new LingoDecimal(1))*255),(_movieScript.restrict(((colour.blue/new LingoDecimal(255))*(customcol.blue/new LingoDecimal(255))),0,new LingoDecimal(1))*255));
if (LingoGlobal.ToBool(lit)) {
colour = _global.color((((((colour.red*2)+mlt.red)*new LingoDecimal(0.5))+customcol.red)/new LingoDecimal(3.5)),(((((colour.green*2)+mlt.green)*new LingoDecimal(0.5))+customcol.green)/new LingoDecimal(3.5)),(((((colour.blue*2)+mlt.blue)*new LingoDecimal(0.5))+customcol.blue)/new LingoDecimal(3.5)));
}
else {
colour = _global.color((((((colour.red*2)+mlt.red)*2)+customcol.red)/new LingoDecimal(5)),(((((colour.green*2)+mlt.green)*2)+customcol.green)/new LingoDecimal(5)),(((((colour.blue*2)+mlt.blue)*2)+customcol.blue)/new LingoDecimal(5)));
}
if (LingoGlobal.ToBool(_movieScript.global_gcustomcolor[3].getpos(fgdp))) {
colour = _global.color(((colour.red+origcl.red)*new LingoDecimal(0.5)),((colour.green+origcl.green)*new LingoDecimal(0.5)),((colour.blue+origcl.blue)*new LingoDecimal(0.5)));
}
}
}
}
return colour;

}
}
}
