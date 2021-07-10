using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: renderColors
//
public sealed class renderColors : LingoBehaviorScript {
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
dynamic q = null;
dynamic layer = null;
dynamic getcolor = null;
dynamic lowresdepth = null;
dynamic fgdp = null;
dynamic fogfac = null;
dynamic rainbowfac = null;
dynamic dsplc = null;
dynamic otherfogfac = null;
dynamic col = null;
dynamic transp = null;
dynamic palcol = null;
dynamic effectcolor = null;
dynamic dark = null;
dynamic greencol = null;
dynamic decalcolor = null;
dynamic dcget = null;
_global.sprite(59).locv = (_movieScript.global_c-8);
for (int tmp_q = 1; tmp_q <= 1400; tmp_q++) {
q = tmp_q;
layer = 1;
getcolor = _global.member(@"finalImage").image.getpixel((q-1),(_movieScript.global_c-1));
if (((getcolor.green > 7) & (getcolor.green < 11))) {
}
else if ((getcolor == _global.color(0,11,0))) {
_global.member(@"finalImage").image.setpixel((q-1),(_movieScript.global_c-1),_global.color(10,0,0));
}
else if ((getcolor == _global.color(255,255,255))) {
layer = 0;
}
lowresdepth = _movieScript.global_dptsl.getpos(_global.member(@"dpImage").image.getpixel((q-1),(_movieScript.global_c-1)));
fgdp = _movieScript.global_fogdptsl.getpos(_global.member(@"fogImage").image.getpixel((q-1),(_movieScript.global_c-1)));
fogfac = ((255-_global.member(@"fogImage").image.getpixel((q-1),(_movieScript.global_c-1)).red)/new LingoDecimal(255));
fogfac = ((fogfac-new LingoDecimal(0.0275))*(new LingoDecimal(1)/new LingoDecimal(0.9411)));
rainbowfac = 0;
if ((fogfac <= new LingoDecimal(0.2))) {
foreach (dynamic tmp_dsplc in new LingoList(new dynamic[] { LingoGlobal.point(-2,0),LingoGlobal.point(0,-2),LingoGlobal.point(2,0),LingoGlobal.point(0,2),LingoGlobal.point(-1,-1),LingoGlobal.point(1,-1),LingoGlobal.point(1,1),LingoGlobal.point(-1,1) })) {
dsplc = tmp_dsplc;
otherfogfac = ((255-_global.member(@"fogImage").image.getpixel(_movieScript.restrict((q-(1+dsplc.loch)),0,1339),_movieScript.restrict((_movieScript.global_c-(1+dsplc.locv)),0,799)).red)/new LingoDecimal(255));
otherfogfac = ((otherfogfac-new LingoDecimal(0.0275))*(new LingoDecimal(1)/new LingoDecimal(0.9411)));
rainbowfac = ((rainbowfac+LingoGlobal.op_gt(LingoGlobal.abs((fogfac-otherfogfac)),new LingoDecimal(0.0333)))*(_movieScript.restrict((fogfac-otherfogfac),0,1)+1));
if ((rainbowfac > 5)) {
break;
}
}
rainbowfac = LingoGlobal.op_gt(rainbowfac,5);
}
col = _global.color(0,0,0);
transp = LingoGlobal.FALSE;
palcol = 2;
effectcolor = 0;
dark = 0;
switch (_global.@string(getcolor)) {
case @"color( 255, 0, 0 )":
palcol = 1;
break;
case @"color( 0, 255, 0 )":
palcol = 2;
break;
case @"color( 0, 0, 255 )":
palcol = 3;
break;
case @"color( 255, 0, 255 )":
palcol = 2;
effectcolor = 1;
break;
case @"color( 0, 255, 255 )":
palcol = 2;
effectcolor = 2;
break;
case @"color( 150, 0, 0 )":
palcol = 1;
dark = 1;
break;
case @"color( 0, 150, 0 )":
palcol = 2;
dark = 1;
break;
case @"color( 0, 0, 150 )":
palcol = 3;
dark = 1;
break;
}
if (((getcolor.green == 255) & (getcolor.blue == 150))) {
palcol = 1;
effectcolor = 3;
}
col.red = (((palcol-1)*30)+fgdp);
if ((_global.member(@"shadowImage").image.getpixel((q-1),(_movieScript.global_c-1)) != _global.color(0,0,0))) {
col.red = (col.red+90);
}
greencol = effectcolor;
if (LingoGlobal.ToBool(rainbowfac)) {
greencol = (greencol+4);
me.rainbowifypixel(LingoGlobal.point(q,_movieScript.global_c));
}
else if ((_global.member(@"rainBowMask").image.getpixel((q-1),(_movieScript.global_c-1)) != _global.color(0))) {
greencol = (greencol+4);
}
if ((effectcolor > 0)) {
if ((effectcolor == 3)) {
col.blue = getcolor.red;
}
else {
col.blue = (255-_global.member(LingoGlobal.concat(@"flattenedGradient",new LingoList(new dynamic[] { @"A",@"B" })[(effectcolor%4)])).image.getpixel((q-1),(_movieScript.global_c-1)).red);
}
}
else {
decalcolor = 0;
if (LingoGlobal.ToBool(_movieScript.global_ganydecals)) {
dcget = _global.member(@"finalDecalImage").image.getpixel((q-1),(_movieScript.global_c-1));
if (((dcget != _global.color(255,255,255)) & (dcget != _global.color(0,0,0)))) {
if ((dcget == _movieScript.global_gpecolors[1][2])) {
if ((doesgreenvaluemeanrainbow(greencol) == 0)) {
greencol = (greencol+4);
}
}
else {
decalcolor = _movieScript.global_gdecalcolors.getpos(dcget);
if (((decalcolor == 0) & (_movieScript.global_gdecalcolors.count < 255))) {
_movieScript.global_gdecalcolors.add(dcget);
decalcolor = _movieScript.global_gdecalcolors.count;
}
col.blue = (256-decalcolor);
greencol = (greencol+8);
}
}
}
}
col.green = (greencol+(dark*16));
if ((layer == 0)) {
_global.member(@"finalImage").image.setpixel((q-1),(_movieScript.global_c-1),_global.color(255,255,255));
}
else {
_global.member(@"finalImage").image.setpixel((q-1),(_movieScript.global_c-1),col);
}
}
_movieScript.global_c = (_movieScript.global_c+1);
if ((_movieScript.global_c > 800)) {
_movieScript.global_c = 1;
_movieScript.global_keeplooping = 0;
}

return null;
}
public dynamic rainbowifypixel(dynamic me,dynamic pxl) {
dynamic currcol = null;
if (((pxl.loch < 2) | (pxl.locv < 2))) {
return null;
}
if ((ispixelinfinalimagerainbowed((pxl+LingoGlobal.point(-1,0))) == 0)) {
currcol = _global.member(@"finalImage").image.getpixel(((pxl.loch-1)-1),(pxl.locv-1));
_global.member(@"finalImage").image.setpixel(((pxl.loch-1)-1),(pxl.locv-1),_global.color(currcol.red,(currcol.green+4),currcol.blue));
}
if ((ispixelinfinalimagerainbowed((pxl+LingoGlobal.point(0,-1))) == 0)) {
currcol = _global.member(@"finalImage").image.getpixel((pxl.loch-1),((pxl.locv-1)-1));
_global.member(@"finalImage").image.setpixel((pxl.loch-1),((pxl.locv-1)-1),_global.color(currcol.red,(currcol.green+4),currcol.blue));
}
_global.member(@"rainBowMask").image.setpixel((pxl.loch-(1+1)),(pxl.locv-1),_global.color(0,0,0));
_global.member(@"rainBowMask").image.setpixel((pxl.loch-1),(pxl.locv-(1+1)),_global.color(0,0,0));

return null;
}
public dynamic ispixelinfinalimagerainbowed(dynamic pxl) {
dynamic grn = null;
if (((pxl.loch < 1) | (pxl.locv < 1))) {
return 0;
}
else if ((_global.member(@"finalImage").image.getpixel((pxl.loch-1),(pxl.locv-1)) == _global.color(255,255,255))) {
return 0;
}
else {
grn = _global.member(@"finalImage").image.getpixel((pxl.loch-1),(pxl.locv-1)).green;
return doesgreenvaluemeanrainbow(grn);
}

return null;
}
public dynamic doesgreenvaluemeanrainbow(dynamic grn) {
if (((grn > 3) & (grn < 8))) {
return 1;
}
else if (((grn > 11) & (grn < 16))) {
return 1;
}
else {
return 0;
}

return null;
}
}
}
