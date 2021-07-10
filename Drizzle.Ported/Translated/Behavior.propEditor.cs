using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: propEditor
//
public sealed class propEditor : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic q = null;
dynamic l = null;
dynamic stretchspeed = null;
dynamic actn1 = null;
dynamic actn2 = null;
dynamic mn = null;
dynamic dir = null;
dynamic perp = null;
dynamic lastclosest = null;
dynamic qd = null;
dynamic offsetpnt = null;
dynamic var = null;
dynamic mem = null;
dynamic scalefac = null;
dynamic viewrope = null;
dynamic ropeframes = null;
_movieScript.global_lastpemouse = _movieScript.global_pemousepos;
_movieScript.global_gpeblink = (_movieScript.global_gpeblink+1);
if ((_movieScript.global_gpeblink > 800)) {
_movieScript.global_gpeblink = 0;
}
if ((_movieScript.global_gpecounter > 0)) {
_movieScript.global_gpecounter = (_movieScript.global_gpecounter-1);
}
if ((_movieScript.global_editsettingsprop < 0)) {
_global.member(@"tileMenu").alignment = new LingoSymbol("left");
}
else {
_global.member(@"tileMenu").alignment = new LingoSymbol("center");
}
if ((((isdecal(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv]) == 0) & (propplacelayer() <= 5)) & ((propplacelayer()+_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].depth) >= 6))) {
_global.member(@"Prop editor warning text").text = @"WARNING - this prop will intersect with the play layer!";
if ((_movieScript.global_gpeblink < 600)) {
_global.sprite(18).visibility = LingoGlobal.TRUE;
}
else {
_global.sprite(18).visibility = LingoGlobal.FALSE;
}
if ((_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp == @"antimatter")) {
_global.member(@"Prop editor warning text").text = @"Antimatter prop intersecting play layer - remember to use a restore effect on affected play relevant terrain";
_global.sprite(17).color = _global.color(255,255,255);
_global.sprite(18).color = _global.color(255,255,255);
}
else {
_global.member(@"Prop editor warning text").text = @"WARNING - this prop will intersect with the play layer!";
_global.sprite(17).color = _global.color(255,0,0);
_global.sprite(18).color = _global.color(255,0,0);
}
}
else {
_global.sprite(18).visibility = LingoGlobal.FALSE;
_global.sprite(17).color = _global.color(255,255,255);
}
for (int tmp_q = 1; tmp_q <= 4; tmp_q++) {
q = tmp_q;
if ((LingoGlobal.ToBool(_global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q])) & (_movieScript.global_gdirectionkeys[q] == 0))) {
_movieScript.global_gleprops.campos = ((_movieScript.global_gleprops.campos+new LingoList(new dynamic[] { LingoGlobal.point(-1,0),LingoGlobal.point(0,-1),LingoGlobal.point(1,0),LingoGlobal.point(0,1) })[q])*((1+9)*_global._key.keypressed(83)));
for (int tmp_l = 1; tmp_l <= 3; tmp_l++) {
l = tmp_l;
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),l);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),l);
}
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16);
renderpropsimage();
}
_movieScript.global_gdirectionkeys[q] = _global._key.keypressed(new LingoList(new dynamic[] { 86,91,88,84 })[q]);
}
_global.sprite(8).visibility = LingoGlobal.FALSE;
_global.script(@"levelOverview").gotoeditor();
if ((_movieScript.global_editsettingsprop == -1)) {
if (LingoGlobal.ToBool(_global._key.keypressed(LingoGlobal.SPACE))) {
if ((LingoGlobal.ToBool(_global._key.keypressed(@"W")) & LingoGlobal.ToBool(_global._key.keypressed(@"A")))) {
_movieScript.global_gpeprops.proprotation = (270+45);
_movieScript.global_gpecounter = 100;
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(@"W")) & LingoGlobal.ToBool(_global._key.keypressed(@"D")))) {
_movieScript.global_gpeprops.proprotation = 45;
_movieScript.global_gpecounter = 100;
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(@"S")) & LingoGlobal.ToBool(_global._key.keypressed(@"D")))) {
_movieScript.global_gpeprops.proprotation = (90+45);
_movieScript.global_gpecounter = 100;
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(@"S")) & LingoGlobal.ToBool(_global._key.keypressed(@"A")))) {
_movieScript.global_gpeprops.proprotation = (180+45);
_movieScript.global_gpecounter = 100;
}
else if ((_movieScript.global_gpecounter == 0)) {
if (LingoGlobal.ToBool(_global._key.keypressed(@"W"))) {
_movieScript.global_gpeprops.proprotation = 0;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"A"))) {
_movieScript.global_gpeprops.proprotation = 270;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"S"))) {
_movieScript.global_gpeprops.proprotation = 180;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"D"))) {
_movieScript.global_gpeprops.proprotation = 90;
}
}
}
else if (LingoGlobal.ToBool(checkkey(@"W"))) {
updatepropmenu(LingoGlobal.point(0,-1));
}
if (LingoGlobal.ToBool(checkkey(@"S"))) {
updatepropmenu(LingoGlobal.point(0,1));
}
if (LingoGlobal.ToBool(checkkey(@"A"))) {
updatepropmenu(LingoGlobal.point(-1,0));
}
if (LingoGlobal.ToBool(checkkey(@"D"))) {
updatepropmenu(LingoGlobal.point(1,0));
}
}
else if ((_movieScript.global_editsettingsprop > _movieScript.global_gpeprops.props.count)) {
_movieScript.global_editsettingsprop = -1;
updatepropmenu(LingoGlobal.point(0,0));
}
else if (LingoGlobal.ToBool(checkkey(@"W"))) {
updatepropsettings(LingoGlobal.point(0,-1));
}
if (LingoGlobal.ToBool(checkkey(@"S"))) {
updatepropsettings(LingoGlobal.point(0,1));
}
if (LingoGlobal.ToBool(checkkey(@"A"))) {
updatepropsettings(LingoGlobal.point(-1,0));
}
if (LingoGlobal.ToBool(checkkey(@"D"))) {
updatepropsettings(LingoGlobal.point(1,0));
}
if (LingoGlobal.ToBool(checkkey(@"Z"))) {
_movieScript.global_gpeprops.color = (_movieScript.global_gpeprops.color+1);
if ((_movieScript.global_gpeprops.color > _movieScript.global_gpecolors.count)) {
_movieScript.global_gpeprops.color = 0;
}
if ((_movieScript.global_gpeprops.color == 0)) {
_global.member(@"Prop Color Text").text = LingoGlobal.concat(@"PROP COLOR: ",@"NONE");
_global.sprite(21).color = _global.color(150,150,150);
}
else {
_global.member(@"Prop Color Text").text = LingoGlobal.concat(@"PROP COLOR: ",_movieScript.global_gpecolors[_movieScript.global_gpeprops.color][1]);
_global.sprite(21).color = _movieScript.global_gpecolors[_movieScript.global_gpeprops.color][2];
}
}
if ((_movieScript.global_gpeprops.color == 1)) {
_global.sprite(21).color = _global.color(_global.random(255),_global.random(255),_global.random(255));
}
if (LingoGlobal.ToBool(checkkey(@"N"))) {
if ((_movieScript.global_editsettingsprop == -1)) {
_movieScript.global_editsettingsprop = 0;
updatepropsettings(LingoGlobal.point(0,0));
}
else {
_movieScript.global_editsettingsprop = -1;
updatepropmenu(LingoGlobal.point(0,0));
}
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"Q"))) {
_movieScript.global_gpeprops.proprotation = (_movieScript.global_gpeprops.proprotation-new LingoDecimal(0.01));
if (LingoGlobal.ToBool(_global._key.keypressed(LingoGlobal.SPACE))) {
_movieScript.global_gpeprops.proprotation = (_movieScript.global_gpeprops.proprotation-new LingoDecimal(0.1));
}
_movieScript.global_mousestill = 0;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"E"))) {
_movieScript.global_gpeprops.proprotation = (_movieScript.global_gpeprops.proprotation+new LingoDecimal(0.01));
if (LingoGlobal.ToBool(_global._key.keypressed(LingoGlobal.SPACE))) {
_movieScript.global_gpeprops.proprotation = (_movieScript.global_gpeprops.proprotation+new LingoDecimal(0.1));
}
_movieScript.global_mousestill = 0;
}
if ((_movieScript.global_gpeprops.proprotation < 0)) {
_movieScript.global_gpeprops.proprotation = (_movieScript.global_gpeprops.proprotation+360);
}
else if ((_movieScript.global_gpeprops.proprotation >= 360)) {
_movieScript.global_gpeprops.proprotation = (_movieScript.global_gpeprops.proprotation-360);
}
if (LingoGlobal.ToBool(_global._key.keypressed(LingoGlobal.SPACE))) {
if (LingoGlobal.ToBool(_global._key.keypressed(@"Y"))) {
_movieScript.global_gpeprops.propflipy = 1;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"H"))) {
_movieScript.global_gpeprops.propflipy = -1;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"G"))) {
_movieScript.global_gpeprops.propflipx = 1;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"J"))) {
_movieScript.global_gpeprops.propflipx = -1;
}
}
else {
stretchspeed = new LingoDecimal(0.002);
if (LingoGlobal.ToBool(_global._key.keypressed(@"Y"))) {
_movieScript.global_gpeprops.propstretchy = (_movieScript.global_gpeprops.propstretchy+stretchspeed);
_movieScript.global_mousestill = 0;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"H"))) {
_movieScript.global_gpeprops.propstretchy = (_movieScript.global_gpeprops.propstretchy-stretchspeed);
_movieScript.global_mousestill = 0;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"G"))) {
_movieScript.global_gpeprops.propstretchx = (_movieScript.global_gpeprops.propstretchx-stretchspeed);
_movieScript.global_mousestill = 0;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"J"))) {
_movieScript.global_gpeprops.propstretchx = (_movieScript.global_gpeprops.propstretchx+stretchspeed);
_movieScript.global_mousestill = 0;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"T"))) {
_movieScript.global_gpeprops.propstretchx = 1;
_movieScript.global_gpeprops.propstretchy = 1;
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"R"))) {
_movieScript.global_gpeprops.propstretchx = 1;
_movieScript.global_gpeprops.propstretchy = 1;
_movieScript.global_gpeprops.propflipx = 1;
_movieScript.global_gpeprops.propflipy = 1;
_movieScript.global_gpeprops.proprotation = 0;
}
if ((_movieScript.global_gpeprops.propstretchy < new LingoDecimal(0.1))) {
_movieScript.global_gpeprops.propstretchy = new LingoDecimal(0.1);
}
else if ((_movieScript.global_gpeprops.propstretchy > 20)) {
_movieScript.global_gpeprops.propstretchy = 20;
}
if ((_movieScript.global_gpeprops.propstretchx < new LingoDecimal(0.1))) {
_movieScript.global_gpeprops.propstretchx = new LingoDecimal(0.1);
}
else if ((_movieScript.global_gpeprops.propstretchx > 20)) {
_movieScript.global_gpeprops.propstretchx = 20;
}
}
actn1 = 0;
actn2 = 0;
_movieScript.global_gpeprops.keys.m1 = _global._mouse.mousedown;
if ((LingoGlobal.ToBool(_movieScript.global_gpeprops.keys.m1) & (_movieScript.global_gpeprops.lastkeys.m1 == 0))) {
actn1 = 1;
}
_movieScript.global_gpeprops.lastkeys.m1 = _movieScript.global_gpeprops.keys.m1;
_movieScript.global_gpeprops.keys.m2 = _global._mouse.rightmousedown;
if ((LingoGlobal.ToBool(_movieScript.global_gpeprops.keys.m2) & (_movieScript.global_gpeprops.lastkeys.m2 == 0))) {
actn2 = 1;
}
_movieScript.global_gpeprops.lastkeys.m2 = _movieScript.global_gpeprops.keys.m2;
if (LingoGlobal.ToBool(_global._key.keypressed(@"F"))) {
if (((_movieScript.global_propsettings.findpos(new LingoSymbol("variation")) != LingoGlobal.VOID) & (LingoGlobal.ToBool(actn1) | LingoGlobal.ToBool(actn2)))) {
_movieScript.global_propsettings.variation = ((_movieScript.global_propsettings.variation+actn1)-actn2);
mn = (1-_movieScript.global_settingsproptype.random);
if ((_movieScript.global_propsettings.variation < mn)) {
_movieScript.global_propsettings.variation = _movieScript.global_settingsproptype.vars;
}
else if ((_movieScript.global_propsettings.variation > _movieScript.global_settingsproptype.vars)) {
_movieScript.global_propsettings.variation = mn;
}
updatevariedpreview(_movieScript.global_settingsproptype,_movieScript.global_propsettings.variation);
updatecursortext();
}
actn1 = 0;
actn2 = 0;
}
if (LingoGlobal.ToBool(actn2)) {
if (LingoGlobal.ToBool(_global._key.keypressed(LingoGlobal.SPACE))) {
_movieScript.global_gpeprops.depth = (_movieScript.global_gpeprops.depth-1);
}
else {
_movieScript.global_gpeprops.depth = (_movieScript.global_gpeprops.depth+1);
}
if ((_movieScript.global_gpeprops.depth < 0)) {
_movieScript.global_gpeprops.depth = 9;
}
else if ((_movieScript.global_gpeprops.depth > 9)) {
_movieScript.global_gpeprops.depth = 0;
}
updateworklayertext();
}
if (LingoGlobal.ToBool(_global._key.keypressed(@"C"))) {
_global.sprite(19).visible = LingoGlobal.TRUE;
_global.sprite(19).color = _global.color(_global.random(255),0,0);
if ((LingoGlobal.ToBool(actn1) & LingoGlobal.ToBool(_global._mouse.mouseloc.inside(LingoGlobal.rect(25,25,52,52))))) {
clearallprops();
}
actn1 = 0;
}
else {
_global.sprite(19).visible = LingoGlobal.FALSE;
}
if (LingoGlobal.ToBool(checkkey(@"L"))) {
_movieScript.global_gpeprops.worklayer = (_movieScript.global_gpeprops.worklayer+1);
if ((_movieScript.global_gpeprops.worklayer > 3)) {
_movieScript.global_gpeprops.worklayer = 1;
}
if ((_movieScript.global_gpeprops.worklayer == 2)) {
_global.sprite(1).blend = 40;
_global.sprite(2).blend = 40;
_global.sprite(3).blend = 90;
_global.sprite(4).blend = 90;
_global.sprite(5).blend = 10;
_global.sprite(6).blend = 10;
}
else if ((_movieScript.global_gpeprops.worklayer == 1)) {
_global.sprite(1).blend = 20;
_global.sprite(2).blend = 20;
_global.sprite(3).blend = 40;
_global.sprite(4).blend = 40;
_global.sprite(5).blend = 90;
_global.sprite(6).blend = 90;
}
else {
_global.sprite(1).blend = 90;
_global.sprite(2).blend = 90;
_global.sprite(3).blend = 10;
_global.sprite(4).blend = 10;
_global.sprite(5).blend = 10;
_global.sprite(6).blend = 10;
}
updateworklayertext();
renderpropsimage();
}
if ((_movieScript.global_gpeprops.proprotation == 0)) {
dir = LingoGlobal.point(0,-1);
perp = LingoGlobal.point(1,0);
}
else if ((_movieScript.global_gpeprops.proprotation == 90)) {
dir = LingoGlobal.point(1,0);
perp = LingoGlobal.point(0,1);
}
else if ((_movieScript.global_gpeprops.proprotation == 180)) {
dir = LingoGlobal.point(0,1);
perp = LingoGlobal.point(-1,0);
}
else if ((_movieScript.global_gpeprops.proprotation == 270)) {
dir = LingoGlobal.point(-1,0);
perp = LingoGlobal.point(0,-1);
}
else {
dir = _movieScript.degtovec(_movieScript.global_gpeprops.proprotation);
perp = _movieScript.givedirfor90degrtoline(-dir,dir);
}
if ((((((_global._key.keypressed(@"U") == 0) & (_global._key.keypressed(@"I") == 0)) & (_global._key.keypressed(@"O") == 0)) & (_global._key.keypressed(@"P") == 0)) & (_global._key.keypressed(@"X") == 0))) {
_movieScript.global_pemousepos = _global._mouse.mouseloc;
if (LingoGlobal.ToBool(_movieScript.global_snaptogrid)) {
_movieScript.global_pemousepos.loch = (((_movieScript.global_pemousepos.loch/new LingoDecimal(16))-new LingoDecimal(0.4999)).integer*16);
_movieScript.global_pemousepos.locv = (((_movieScript.global_pemousepos.locv/new LingoDecimal(16))-new LingoDecimal(0.4999)).integer*16);
}
}
if ((_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp == @"long")) {
if ((_movieScript.global_longpropplacepos == LingoGlobal.VOID)) {
_movieScript.global_gpeprops.proprotation = 0;
_movieScript.global_gpeprops.propstretchx = 1;
_movieScript.global_gpeprops.propstretchy = 1;
_movieScript.global_gpeprops.propflipx = 1;
_movieScript.global_gpeprops.propflipy = 1;
if (LingoGlobal.ToBool(actn1)) {
_movieScript.global_longpropplacepos = (_movieScript.global_pemousepos+((LingoGlobal.point(-16,-16)+_movieScript.global_gleprops.campos)*16));
actn1 = 0;
}
}
else {
_movieScript.global_gpeprops.proprotation = (_movieScript.lookatpoint((_movieScript.global_longpropplacepos-((LingoGlobal.point(-16,-16)+_movieScript.global_gleprops.campos)*16)),_movieScript.global_pemousepos)+90);
_movieScript.global_gpeprops.propstretchx = (_movieScript.diag((_movieScript.global_longpropplacepos-((LingoGlobal.point(-16,-16)+_movieScript.global_gleprops.campos)*16)),_movieScript.global_pemousepos)/(new LingoDecimal(200)*(new LingoDecimal(16)/new LingoDecimal(20))));
_movieScript.global_pemousepos = ((_movieScript.global_longpropplacepos-(((LingoGlobal.point(-16,-16)+_movieScript.global_gleprops.campos)*16)+_movieScript.global_pemousepos))/new LingoDecimal(2));
_movieScript.global_gpeprops.propstretchy = 1;
_movieScript.global_gpeprops.propflipx = 1;
_movieScript.global_gpeprops.propflipy = 1;
if (LingoGlobal.ToBool(actn1)) {
_movieScript.global_longpropplacepos = LingoGlobal.VOID;
}
}
}
lastclosest = _movieScript.global_closestprop;
_movieScript.global_closestprop = 0;
if (((_movieScript.global_gpeprops.props.count > 0) & ((LingoGlobal.ToBool(_global._key.keypressed(@"V")) | LingoGlobal.ToBool(_global._key.keypressed(@"B"))) | LingoGlobal.ToBool(_global._key.keypressed(@"M"))))) {
_movieScript.global_closestprop = findclosestprop();
}
if ((_movieScript.global_editsettingsprop > 0)) {
_movieScript.global_closestprop = _movieScript.global_editsettingsprop;
}
if (((_movieScript.global_closestprop != lastclosest) & (_movieScript.global_closestprop < 1))) {
if ((new LingoList(new dynamic[] { @"variedDecal",@"variedSoft",@"variedStandard" }).getpos(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp) > 0)) {
updatevariedpreview(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv],_movieScript.global_propsettings.variation);
}
}
if ((_movieScript.global_closestprop > 0)) {
qd = _movieScript.global_gpeprops.props[_movieScript.global_closestprop][4];
offsetpnt = (LingoGlobal.point(16,16)-(_movieScript.global_gleprops.campos*16));
qd = (qd+new LingoList(new dynamic[] { offsetpnt,offsetpnt,offsetpnt,offsetpnt }));
if ((_movieScript.global_closestprop != lastclosest)) {
_global.sprite(15).member = proppreviewmember(_movieScript.global_gprops[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].loch].prps[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].locv]);
if ((new LingoList(new dynamic[] { @"variedDecal",@"variedSoft",@"variedStandard" }).getpos(_movieScript.global_gprops[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].loch].prps[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].locv].tp) > 0)) {
if ((_movieScript.global_closestprop == 0)) {
var = _movieScript.global_propsettings.variation;
}
else {
var = _movieScript.global_gpeprops.props[_movieScript.global_closestprop][5].settings.variation;
}
updatevariedpreview(_movieScript.global_gprops[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].loch].prps[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].locv],var);
}
}
_global.sprite(17).loc = LingoGlobal.point(-100,-100);
if (LingoGlobal.ToBool(_global._key.keypressed(@"V"))) {
_global.sprite(15).color = _global.color(255,0,0);
_global.sprite(15).forecolor = 6;
if (LingoGlobal.ToBool(actn1)) {
_movieScript.global_gpeprops.props.deleteat(_movieScript.global_closestprop);
renderpropsimage();
}
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"B"))) {
_global.sprite(15).color = _global.color(0,255,255);
_global.sprite(15).forecolor = _global.color(255,255,255);
if (LingoGlobal.ToBool(actn1)) {
_movieScript.global_gpeprops.pmpos = _movieScript.global_gpeprops.props[_movieScript.global_closestprop][3];
updatepropmenu(LingoGlobal.point(0,0));
_movieScript.global_propsettings = _movieScript.global_gpeprops.props[_movieScript.global_closestprop][5].settings;
_movieScript.global_settingsproptype = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv];
_movieScript.global_editsettingsprop = -1;
}
}
else if ((_movieScript.global_editsettingsprop > 0)) {
_global.sprite(15).color = _global.color(0,255,0);
_global.sprite(15).forecolor = 187;
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"M"))) {
_global.sprite(15).color = _global.color(0,0,255);
_global.sprite(15).forecolor = 62;
if (LingoGlobal.ToBool(actn1)) {
_movieScript.global_editsettingsprop = _movieScript.global_closestprop;
_movieScript.global_propsettings = _movieScript.global_gpeprops.props[_movieScript.global_closestprop][5].settings;
_movieScript.global_settingsproptype = _movieScript.global_gprops[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].loch].prps[_movieScript.global_gpeprops.props[_movieScript.global_closestprop][3].locv];
updatepropsettings(LingoGlobal.point(0,0));
}
}
if ((_movieScript.global_editsettingsprop < 1)) {
_global.sprite(15).blend = _movieScript.restrict(((50+50)*LingoGlobal.sin((((_movieScript.global_gpeblink/new LingoDecimal(800))*LingoGlobal.PI)*new LingoDecimal(4)))),0,100);
}
else {
_global.sprite(15).blend = _movieScript.restrict(((50+50)*LingoGlobal.sin((((_movieScript.global_gpeblink/new LingoDecimal(800))*LingoGlobal.PI)*new LingoDecimal(8)))),0,100);
}
}
else {
mem = proppreviewmember(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv]);
scalefac = (new LingoDecimal(16)/new LingoDecimal(20));
qd = new LingoList(new dynamic[] { _movieScript.global_pemousepos,_movieScript.global_pemousepos,_movieScript.global_pemousepos,_movieScript.global_pemousepos });
qd[1] = ((qd[1]+(((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy))-(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx));
qd[2] = ((qd[2]+(((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy))+(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx));
qd[3] = (qd[3]-((((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy)+(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx)));
qd[4] = ((qd[4]-(((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy))-(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx));
if (LingoGlobal.ToBool(_global._key.keypressed(@"U"))) {
_movieScript.global_pefreequad[1] = (_global._mouse.mouseloc-qd[1]);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"I"))) {
_movieScript.global_pefreequad[2] = (_global._mouse.mouseloc-qd[2]);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"O"))) {
_movieScript.global_pefreequad[3] = (_global._mouse.mouseloc-qd[3]);
}
else if (LingoGlobal.ToBool(_global._key.keypressed(@"P"))) {
_movieScript.global_pefreequad[4] = (_global._mouse.mouseloc-qd[4]);
}
else if ((LingoGlobal.ToBool(_global._key.keypressed(@"K")) | LingoGlobal.ToBool(_global._key.keypressed(@"R")))) {
_movieScript.global_pefreequad = new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(0,0),LingoGlobal.point(0,0),LingoGlobal.point(0,0) });
}
qd = (qd+_movieScript.global_pefreequad);
if ((LingoGlobal.ToBool(actn1) & LingoGlobal.ToBool(_global._mouse.mouseloc.inside(LingoGlobal.rect(16,16,848,656))))) {
offsetpnt = ((LingoGlobal.point(-16,-16)+_movieScript.global_gleprops.campos)*16);
placeprop((qd+new LingoList(new dynamic[] { offsetpnt,offsetpnt,offsetpnt,offsetpnt })));
}
_global.sprite(15).blend = 50;
_global.sprite(15).color = _global.color(0,0,0);
_global.sprite(15).forecolor = 255;
_global.sprite(15).member = mem;
_global.sprite(17).loc = (_movieScript.global_pemousepos+LingoGlobal.point(40,20));
}
_global.sprite(15).quad = qd;
if ((_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp == @"rope")) {
viewrope = LingoGlobal.FALSE;
if ((_movieScript.global_lastpemouse != _movieScript.global_pemousepos)) {
_movieScript.global_mousestill = 0;
}
else {
_movieScript.global_mousestill = (_movieScript.global_mousestill+1);
}
if (((_movieScript.global_editsettingsprop > 0) | LingoGlobal.ToBool(_global._key.keypressed(@"M")))) {
_movieScript.global_mousestill = 0;
}
if ((_global._key.keypressed(@"X") == 0)) {
if ((_movieScript.global_mousestill == 10)) {
ropeframes = 0;
resetropeprop();
}
else if ((_movieScript.global_mousestill > 10)) {
ropeframes = (ropeframes+1);
updatecursortext();
_global.script(@"ropeModel").modelropeupdate(1,_movieScript.global_gleprops.campos,(new LingoDecimal(16)/new LingoDecimal(20)));
viewrope = LingoGlobal.TRUE;
}
}
else {
viewrope = LingoGlobal.TRUE;
}
_global.sprite(20).visibility = viewrope;
}
else {
ropeframes = 0;
_global.sprite(20).visibility = LingoGlobal.FALSE;
}
_global.go(_global.the_frame);

return null;
}
public dynamic findclosestprop() {
dynamic smallestdist = null;
dynamic offsetmousepnt = null;
dynamic p = null;
dynamic pos = null;
_movieScript.global_closestprop = 0;
smallestdist = 10000;
offsetmousepnt = (_movieScript.global_pemousepos-((LingoGlobal.point(16,16)+_movieScript.global_gleprops.campos)*new LingoDecimal(16)));
for (int tmp_p = 1; tmp_p <= _movieScript.global_gpeprops.props.count; tmp_p++) {
p = tmp_p;
pos = ((((_movieScript.global_gpeprops.props[p][4][1]+_movieScript.global_gpeprops.props[p][4][2])+_movieScript.global_gpeprops.props[p][4][3])+_movieScript.global_gpeprops.props[p][4][4])/new LingoDecimal(4));
if ((_movieScript.diag(offsetmousepnt,pos) < smallestdist)) {
smallestdist = _movieScript.diag(offsetmousepnt,pos);
_movieScript.global_closestprop = p;
}
}
return _movieScript.global_closestprop;

}
public dynamic updateworklayertext() {
dynamic txt = null;
txt = LingoGlobal.concat_space(@"Work Layer:",_global.@string(_movieScript.global_gpeprops.worklayer));
txt += txt.ToString();
txt += txt.ToString();
_global.member(@"layerText").text = txt;
if ((_movieScript.global_gpeprops.pmpos.loch > _movieScript.global_gprops.count)) {
_movieScript.global_gpeprops.pmpos.loch = 1;
}
if ((_movieScript.global_gpeprops.pmpos.locv > _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps.count)) {
_movieScript.global_gpeprops.pmpos.locv = 1;
}
updatecursortext();

return null;
}
public dynamic updatecursortext() {
dynamic txt = null;
txt = LingoGlobal.concat(LingoGlobal.concat(LingoGlobal.concat(@"Prop depth: ",propplacelayer()),@" to "),(propplacelayer()+_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].depth));
if ((_movieScript.global_propsettings != LingoGlobal.VOID)) {
if ((_movieScript.global_propsettings.findpos(new LingoSymbol("variation")) != LingoGlobal.VOID)) {
txt += txt.ToString();
txt += txt.ToString();
if ((_movieScript.global_propsettings.variation == 0)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
}
}
_global.member(@"Prop Depth Text").text = txt;

return null;
}
public dynamic propplacelayer() {
return (((_movieScript.global_gpeprops.worklayer-1)*10)+_movieScript.global_gpeprops.depth);

}
public dynamic placeprop(dynamic qd) {
dynamic prop = null;
dynamic q = null;
prop = new LingoList(new dynamic[] { -propplacelayer(),_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].nm,_movieScript.global_gpeprops.pmpos,qd,new LingoPropertyList {[new LingoSymbol("settings")] = _movieScript.global_propsettings.duplicate()} });
if ((prop[5].settings.findpos(new LingoSymbol("color")) != LingoGlobal.VOID)) {
if ((_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tags.getpos(@"customColorRainBow") > 0)) {
_movieScript.global_gpeprops.color = 1;
}
prop[5].settings.color = _movieScript.global_gpeprops.color;
}
switch (_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp) {
case @"rope":
prop[5].addprop(new LingoSymbol("points"),new LingoPropertyList {});
for (int tmp_q = 1; tmp_q <= _movieScript.global_ropemodel.segments.count; tmp_q++) {
q = tmp_q;
prop[5].points.add(_global.script(@"ropeModel").smoothedpos(q));
}
break;
case @"long":
break;
case @"variedDecal":
case @"variedSoft":
case @"variedStandard":
if ((prop[5].settings.variation == 0)) {
prop[5].settings.variation = _global.random(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].vars);
}
break;
}
_movieScript.global_gpeprops.props.add(prop);
_movieScript.global_gpeprops.props.sort();
renderpropsimage();
if ((((_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp == @"variedDecal") | (_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp == @"variedSoft")) | (_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp == @"variedStandard"))) {
if ((_movieScript.global_propsettings.variation == 0)) {
updatevariedpreview(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv],0);
}
}
applytransformationtags();

return null;
}
public dynamic clearallprops() {
_movieScript.global_gpeprops.props = new LingoPropertyList {};
renderpropsimage();

return null;
}
public dynamic renderpropsimage() {
dynamic camposquad = null;
dynamic displaylayer = null;
dynamic layer = null;
dynamic p = null;
dynamic prop = null;
dynamic propdata = null;
dynamic proplayer = null;
dynamic mem = null;
dynamic blnd = null;
dynamic q = null;
dynamic clr = null;
dynamic adaptedpos = null;
_global.member(@"propsImage").image = _global.image((52*16),(40*16),16);
_global.member(@"propsImage2").image = _global.image((52*16),(40*16),16);
camposquad = new LingoList(new dynamic[] { (_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16),(_movieScript.global_gleprops.campos*16) });
displaylayer = ((_movieScript.global_gpeprops.worklayer-1)*10);
layer = 29;
for (int tmp_p = 1; tmp_p <= _movieScript.global_gpeprops.props.count; tmp_p++) {
p = tmp_p;
prop = _movieScript.global_gpeprops.props[p];
propdata = _movieScript.global_gprops[prop[3].loch].prps[prop[3].locv];
proplayer = -prop[1];
mem = proppreviewmember(propdata);
blnd = (100-(isdecal(propdata)*40));
if ((proplayer >= displaylayer)) {
if ((proplayer < layer)) {
for (int tmp_q = 1; tmp_q <= (layer-proplayer); tmp_q++) {
q = tmp_q;
_global.member(@"propsImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,(52*16),(40*16)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 10,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
layer = proplayer;
}
clr = _global.color(0,0,0);
if ((propdata.settings.findpos(new LingoSymbol("color")) != LingoGlobal.VOID)) {
if ((propdata.settings.color > 0)) {
clr = _movieScript.global_gpecolors[propdata.settings.color];
}
}
switch (propdata.tp) {
case @"rope":
_global.member(@"propsImage").image.copypixels(mem.image,(prop[4]-camposquad),mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("blend")] = blnd});
q = 1;
while (LingoGlobal.ToBool(LingoGlobal.op_lt(q,prop[5].points.count))) {
adaptedpos = prop[5].points[q];
adaptedpos = (adaptedpos-(_movieScript.global_gleprops.campos*new LingoDecimal(20)));
adaptedpos = ((adaptedpos*new LingoDecimal(16))/new LingoDecimal(20));
_global.member(@"propsImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect((adaptedpos-LingoGlobal.point(1,1)),(adaptedpos+LingoGlobal.point(2,2))),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = propdata.previewcolor});
q = (q+propdata.previewevery);
}
break;
case @"variedDecal":
case @"variedSoft":
case @"variedStandard":
updatevariedpreview(propdata,prop[5].settings.variation);
_global.member(@"propsImage").image.copypixels(mem.image,(prop[4]-camposquad),mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("blend")] = blnd,[new LingoSymbol("color")] = clr});
break;
default:
_global.member(@"propsImage").image.copypixels(mem.image,(prop[4]-camposquad),mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("blend")] = blnd,[new LingoSymbol("color")] = clr});
break;
}
}
else {
_global.member(@"propsImage2").image.copypixels(mem.image,(prop[4]-camposquad),mem.image.rect,new LingoPropertyList {[new LingoSymbol("ink")] = 36,[new LingoSymbol("blend")] = blnd,[new LingoSymbol("color")] = clr});
}
}
for (int tmp_q = displaylayer; tmp_q <= layer; tmp_q++) {
q = tmp_q;
_global.member(@"propsImage").image.copypixels(_global.member(@"pxl").image,LingoGlobal.rect(0,0,(52*16),(40*16)),LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("blend")] = 10,[new LingoSymbol("color")] = _global.color(255,255,255)});
}
if ((new LingoList(new dynamic[] { @"variedDecal",@"variedSoft",@"variedStandard" }).getpos(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].tp) > 0)) {
updatevariedpreview(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv],_movieScript.global_propsettings.variation);
}

return null;
}
public dynamic checkkey(dynamic key) {
dynamic rtrn = null;
rtrn = 0;
_movieScript.global_gpeprops.keys[LingoGlobal.symbol(key)] = _global._key.keypressed(key);
if ((LingoGlobal.ToBool(_movieScript.global_gpeprops.keys[LingoGlobal.symbol(key)]) & (_movieScript.global_gpeprops.lastkeys[LingoGlobal.symbol(key)] == 0))) {
rtrn = 1;
}
_movieScript.global_gpeprops.lastkeys[LingoGlobal.symbol(key)] = _movieScript.global_gpeprops.keys[LingoGlobal.symbol(key)];
return rtrn;

}
public dynamic isdecal(dynamic prop) {
if (((prop.tp == @"simpleDecal") | (prop.tp == @"variedDecal"))) {
return 1;
}
else {
return 0;
}

return null;
}
public dynamic updatepropmenu(dynamic mv) {
dynamic txt = null;
dynamic pr = null;
dynamic nt = null;
_movieScript.global_gpeprops.pmpos = (_movieScript.global_gpeprops.pmpos+mv);
if ((mv.loch != 0)) {
if ((_movieScript.global_gpeprops.pmpos.loch < 1)) {
_movieScript.global_gpeprops.pmpos.loch = _movieScript.global_gprops.count;
}
else if ((_movieScript.global_gpeprops.pmpos.loch > _movieScript.global_gprops.count)) {
_movieScript.global_gpeprops.pmpos.loch = 1;
}
_movieScript.global_gpeprops.pmpos.locv = _movieScript.global_gpeprops.pmsavposl[_movieScript.global_gpeprops.pmpos.loch];
}
else if ((mv.locv != 0)) {
if ((_movieScript.global_gpeprops.pmpos.locv < 1)) {
_movieScript.global_gpeprops.pmpos.locv = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps.count;
}
else if ((_movieScript.global_gpeprops.pmpos.locv > _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps.count)) {
_movieScript.global_gpeprops.pmpos.locv = 1;
}
_movieScript.global_gpeprops.pmsavposl[_movieScript.global_gpeprops.pmpos.loch] = _movieScript.global_gpeprops.pmpos.locv;
}
if (((_movieScript.global_gpeprops.pmpos.locv-5) < _movieScript.global_pescrollpos)) {
_movieScript.global_pescrollpos = (_movieScript.global_gpeprops.pmpos.locv-5);
}
else if (((_movieScript.global_gpeprops.pmpos.locv-15) > _movieScript.global_pescrollpos)) {
_movieScript.global_pescrollpos = (_movieScript.global_gpeprops.pmpos.locv-15);
}
_movieScript.global_pescrollpos = _movieScript.restrict(_movieScript.global_pescrollpos,0,_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps.count);
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_pr = (1+_movieScript.global_pescrollpos); tmp_pr <= (21+_movieScript.global_pescrollpos); tmp_pr++) {
pr = tmp_pr;
if ((pr > _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps.count)) {
break;
}
else if ((pr == _movieScript.global_gpeprops.pmpos.locv)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
}
if ((_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].notes.count > 0)) {
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
foreach (dynamic tmp_nt in _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].notes) {
nt = tmp_nt;
txt += txt.ToString();
txt += txt.ToString();
}
}
_global.member(@"tileMenu").text = txt;
newpropselected();

return null;
}
public dynamic updatepropsettings(dynamic mv) {
dynamic editedproptemplate = null;
dynamic adress = null;
dynamic mn = null;
dynamic txt = null;
dynamic st = null;
dynamic nm = null;
dynamic p = null;
dynamic t = null;
if ((_movieScript.global_editsettingsprop == 0)) {
editedproptemplate = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv];
}
else {
adress = _movieScript.global_gpeprops.props[_movieScript.global_editsettingsprop][3];
editedproptemplate = _movieScript.global_gprops[adress.loch].prps[adress.locv];
}
if ((_movieScript.global_propsettings == LingoGlobal.VOID)) {
duplicatepropsettings();
}
_movieScript.global_settingcursor = (_movieScript.global_settingcursor+mv.locv);
if ((mv.locv != 0)) {
if ((_movieScript.global_settingcursor < 1)) {
_movieScript.global_settingcursor = _movieScript.global_propsettings.count;
}
else if ((_movieScript.global_settingcursor > _movieScript.global_propsettings.count)) {
_movieScript.global_settingcursor = 1;
}
}
if ((mv.loch != 0)) {
switch (_movieScript.global_propsettings.getpropat(_movieScript.global_settingcursor).@string) {
case @"release":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = _movieScript.restrict((_movieScript.global_propsettings[_movieScript.global_settingcursor]+mv.loch),-1,1);
break;
case @"renderOrder":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = (_movieScript.global_propsettings[_movieScript.global_settingcursor]+mv.loch);
break;
case @"seed":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = (_global._system.milliseconds%1000);
break;
case @"renderTime":
if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] == 0)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = 1;
}
else {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = 0;
}
break;
case @"thickness":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = _movieScript.restrict(((_movieScript.global_propsettings[_movieScript.global_settingcursor]+mv.loch)*new LingoDecimal(0.25)),1,5);
break;
case @"variation":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = (_movieScript.global_propsettings[_movieScript.global_settingcursor]+mv.loch);
mn = (1-editedproptemplate.random);
if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] < mn)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = _movieScript.global_settingsproptype.vars;
}
else if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] > _movieScript.global_settingsproptype.vars)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = mn;
}
updatevariedpreview(_movieScript.global_settingsproptype,_movieScript.global_propsettings[_movieScript.global_settingcursor]);
break;
case @"customDepth":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = (_movieScript.global_propsettings[_movieScript.global_settingcursor]+mv.loch);
if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] < 1)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = 30;
}
else if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] > 30)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = 1;
}
break;
case @"applyColor":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = (1-_movieScript.global_propsettings[_movieScript.global_settingcursor]);
break;
case @"color":
_movieScript.global_propsettings[_movieScript.global_settingcursor] = (_movieScript.global_propsettings[_movieScript.global_settingcursor]+mv.loch);
if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] < 0)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = _movieScript.global_gpecolors.count;
}
else if ((_movieScript.global_propsettings[_movieScript.global_settingcursor] > _movieScript.global_gpecolors.count)) {
_movieScript.global_propsettings[_movieScript.global_settingcursor] = 0;
}
break;
}
}
txt = @"";
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
txt += txt.ToString();
for (int tmp_st = 1; tmp_st <= _movieScript.global_propsettings.count; tmp_st++) {
st = tmp_st;
nm = _movieScript.global_propsettings.getpropat(st).@string;
txt += txt.ToString();
txt += txt.ToString();
p = _movieScript.global_propsettings[st];
t = @"";
switch (nm) {
case @"release":
if ((p == -1)) {
t = @"left";
}
else if ((p == 1)) {
t = @"right";
}
else {
t = @"none";
}
break;
case @"renderTime":
if ((p == 0)) {
t = @"Pre Effcts";
}
else {
t = @"Post Effcts";
}
break;
case @"variation":
if ((_movieScript.global_propsettings[st] == 0)) {
t = @"random";
}
else {
t = _movieScript.global_propsettings[st].@string;
}
break;
case @"applyColor":
if ((_movieScript.global_propsettings[st] == 0)) {
t = @"NO";
}
else {
t = @"YES";
}
break;
case @"color":
if ((_movieScript.global_propsettings[st] == 0)) {
t = @"NONE";
}
else {
t = _movieScript.global_gpecolors[_movieScript.global_propsettings[st]][1];
}
break;
default:
t = _movieScript.global_propsettings[st].@string;
break;
}
if ((st == _movieScript.global_settingcursor)) {
txt += txt.ToString();
}
else {
txt += txt.ToString();
}
txt += txt.ToString();
txt += txt.ToString();
}
_global.member(@"tileMenu").text = txt;

return null;
}
public dynamic newpropselected() {
dynamic prop = null;
dynamic q = null;
resettransformation();
duplicatepropsettings();
prop = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv];
if ((_movieScript.global_pesavedrotat != -1)) {
_movieScript.global_gpeprops.proprotation = _movieScript.global_pesavedrotat;
}
if ((_movieScript.global_pesavedstretch.loch != 0)) {
_movieScript.global_gpeprops.propstretchx = _movieScript.global_pesavedstretch.loch;
}
if ((_movieScript.global_pesavedstretch.locv != 0)) {
_movieScript.global_gpeprops.propstretchy = _movieScript.global_pesavedstretch.locv;
}
if ((_movieScript.global_pesavedflip.loch != 0)) {
_movieScript.global_gpeprops.propflipx = _movieScript.global_pesavedflip.loch;
}
if ((_movieScript.global_pesavedflip.locv != 0)) {
_movieScript.global_gpeprops.propflipy = _movieScript.global_pesavedflip.locv;
}
_movieScript.global_propsettings.rendertime = 0;
_movieScript.global_snaptogrid = 0;
for (int tmp_q = 1; tmp_q <= prop.tags.count; tmp_q++) {
q = tmp_q;
switch (prop.tags[q]) {
case @"postEffects":
_movieScript.global_propsettings.rendertime = 1;
break;
case @"snapToGrid":
_movieScript.global_snaptogrid = 1;
break;
}
}
applytransformationtags();
if ((prop.tp == @"rope")) {
resetropeprop();
}
if ((new LingoList(new dynamic[] { @"variedDecal",@"variedSoft",@"variedStandard" }).getpos(prop.tp) > 0)) {
updatevariedpreview(prop,_movieScript.global_propsettings.variation);
}
updateworklayertext();

return null;
}
public dynamic applytransformationtags() {
dynamic prop = null;
dynamic q = null;
resettransformation();
_movieScript.global_pesavedrotat = -1;
_movieScript.global_pesavedflip = LingoGlobal.point(0,0);
_movieScript.global_pesavedstretch = LingoGlobal.point(0,0);
prop = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv];
for (int tmp_q = 1; tmp_q <= prop.tags.count; tmp_q++) {
q = tmp_q;
switch (prop.tags[q]) {
case @"randomRotat":
_movieScript.global_pesavedrotat = _movieScript.global_gpeprops.proprotation;
_movieScript.global_gpeprops.proprotation = _global.random(360);
break;
case @"randomFlipX":
if ((_global.random(2) == 1)) {
_movieScript.global_pesavedflip.loch = _movieScript.global_gpeprops.propflipx;
_movieScript.global_gpeprops.propflipx = -_movieScript.global_gpeprops.propflipx;
}
break;
case @"randomFlipY":
if ((_global.random(2) == 1)) {
_movieScript.global_pesavedflip.locv = _movieScript.global_gpeprops.propflipy;
_movieScript.global_gpeprops.propflipy = -_movieScript.global_gpeprops.propflipy;
}
break;
}
}
switch (prop.tp) {
case @"long":
_movieScript.global_pesavedstretch = LingoGlobal.point(_movieScript.global_gpeprops.propstretchx,_movieScript.global_gpeprops.propstretchy);
_movieScript.global_gpeprops.proprotation = 0;
_movieScript.global_gpeprops.propflipx = 1;
_movieScript.global_gpeprops.propflipy = 1;
_movieScript.global_gpeprops.propstretchx = 1;
_movieScript.global_gpeprops.propstretchy = 1;
break;
}

return null;
}
public dynamic resettransformation() {
if ((_movieScript.global_pesavedrotat != -1)) {
_movieScript.global_gpeprops.proprotation = _movieScript.global_pesavedrotat;
}
if ((_movieScript.global_pesavedflip.loch != 0)) {
_movieScript.global_gpeprops.propflipx = _movieScript.global_pesavedflip.loch;
}
if ((_movieScript.global_pesavedflip.locv != 0)) {
_movieScript.global_gpeprops.propflipy = _movieScript.global_pesavedflip.locv;
}
if ((_movieScript.global_pesavedstretch.loch != 0)) {
_movieScript.global_gpeprops.propstretchx = _movieScript.global_pesavedstretch.loch;
}
if ((_movieScript.global_pesavedstretch.locv != 0)) {
_movieScript.global_gpeprops.propstretchx = _movieScript.global_pesavedstretch.locv;
}

return null;
}
public dynamic resetropeprop() {
dynamic prop = null;
dynamic dir = null;
dynamic perp = null;
dynamic mem = null;
dynamic scalefac = null;
dynamic qd = null;
dynamic offsetpnt = null;
dynamic pa = null;
dynamic pb = null;
dynamic colldep = null;
dynamic cd = null;
prop = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv];
if ((_movieScript.global_gpeprops.proprotation == 0)) {
dir = LingoGlobal.point(0,-1);
perp = LingoGlobal.point(1,0);
}
else if ((_movieScript.global_gpeprops.proprotation == 90)) {
dir = LingoGlobal.point(1,0);
perp = LingoGlobal.point(0,1);
}
else if ((_movieScript.global_gpeprops.proprotation == 180)) {
dir = LingoGlobal.point(0,1);
perp = LingoGlobal.point(-1,0);
}
else if ((_movieScript.global_gpeprops.proprotation == 270)) {
dir = LingoGlobal.point(-1,0);
perp = LingoGlobal.point(0,-1);
}
else {
dir = _movieScript.degtovec(_movieScript.global_gpeprops.proprotation);
perp = _movieScript.givedirfor90degrtoline(-dir,dir);
}
mem = proppreviewmember(_movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv]);
scalefac = (new LingoDecimal(16)/new LingoDecimal(20));
qd = new LingoList(new dynamic[] { _movieScript.global_pemousepos,_movieScript.global_pemousepos,_movieScript.global_pemousepos,_movieScript.global_pemousepos });
qd[1] = ((qd[1]+(((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy))-(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx));
qd[2] = ((qd[2]+(((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy))+(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx));
qd[3] = (qd[3]-((((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy)+(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx)));
qd[4] = ((qd[4]-(((((dir*mem.rect.height)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchy)*scalefac)*_movieScript.global_gpeprops.propflipy))-(((((perp*mem.rect.width)*new LingoDecimal(0.5))*_movieScript.global_gpeprops.propstretchx)*scalefac)*_movieScript.global_gpeprops.propflipx));
offsetpnt = ((LingoGlobal.point(-16,-16)+_movieScript.global_gleprops.campos)*16);
qd = (qd+new LingoList(new dynamic[] { offsetpnt,offsetpnt,offsetpnt,offsetpnt }));
qd = (qd*(new LingoDecimal(20)/new LingoDecimal(16)));
pa = ((qd[1]+qd[4])/new LingoDecimal(2));
pb = ((qd[2]+qd[3])/new LingoDecimal(2));
colldep = ((((_movieScript.global_gpeprops.worklayer-1)*10)+_movieScript.global_gpeprops.depth)+prop.collisiondepth);
if ((colldep < 10)) {
cd = 1;
}
else if ((colldep < 20)) {
cd = 2;
}
else {
cd = 3;
}
_global.script(@"ropeModel").resetropemodel(pa,pb,prop,_movieScript.global_gpeprops.propstretchy,cd,_movieScript.global_propsettings.release);

return null;
}
public dynamic updatevariedpreview(dynamic prop,dynamic var) {
dynamic mem = null;
dynamic imprtmem = null;
dynamic sz = null;
dynamic v2 = null;
mem = proppreviewmember(prop);
imprtmem = _global.member(@"previewImprt");
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Props\",prop.nm),@".png"));
imprtmem.name = @"previewImprt";
if ((prop.tp == @"variedStandard")) {
sz = (prop.sz*new LingoDecimal(20));
}
else {
sz = prop.pxlsize;
}
mem.image = _global.image(sz.loch,sz.locv,16);
if ((var == 0)) {
for (int tmp_v2 = 1; tmp_v2 <= prop.vars; tmp_v2++) {
v2 = tmp_v2;
mem.image.copypixels(imprtmem.image,mem.image.rect,(LingoGlobal.rect((sz.loch*(v2-1)),0,(sz.loch*v2),sz.locv)+LingoGlobal.rect(0,1,0,1)),new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
}
else {
mem.image.copypixels(imprtmem.image,mem.image.rect,(LingoGlobal.rect((sz.loch*(var-1)),0,(sz.loch*var),sz.locv)+LingoGlobal.rect(0,1,0,1)));
}

return null;
}
public dynamic proppreviewmember(dynamic prop) {
dynamic q = null;
dynamic tileasprop = null;
dynamic sav2 = null;
dynamic newmem = null;
dynamic c = null;
dynamic c2 = null;
dynamic getrect = null;
if ((_movieScript.global_loadedproppreviews == LingoGlobal.VOID)) {
_movieScript.global_loadedproppreviews = new LingoPropertyList {};
}
for (int tmp_q = 1; tmp_q <= _movieScript.global_loadedproppreviews.count; tmp_q++) {
q = tmp_q;
if ((_movieScript.global_loadedproppreviews[q] == prop.nm)) {
return _global.member(LingoGlobal.concat_space(@"propPreview",prop.nm));
}
}
tileasprop = 0;
for (int tmp_q = 1; tmp_q <= prop.tags.count; tmp_q++) {
q = tmp_q;
if ((prop.tags[q] == @"Tile")) {
tileasprop = 1;
break;
}
}
sav2 = _global.member(@"previewImprt");
if (LingoGlobal.ToBool(tileasprop)) {
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Graphics\",prop.nm),@".png"));
}
else {
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Props\",prop.nm),@".png"));
}
sav2.name = @"previewImprt";
newmem = _global.new_castlib(new LingoSymbol("bitmap"),@"customMems");
switch (prop.tp) {
case @"standard":
newmem.image = _global.image((prop.sz.loch*20),(prop.sz.locv*20),16);
for (int tmp_c = 1; tmp_c <= prop.repeatl.count; tmp_c++) {
c = tmp_c;
c2 = ((prop.repeatl.count+1)-c);
getrect = (LingoGlobal.rect(0,(((c2-1)*prop.sz.locv)*20),(prop.sz.loch*20),((c2*prop.sz.locv)*20))+LingoGlobal.rect(0,1,0,1));
newmem.image.copypixels(_global.member(@"pxl").image,newmem.image.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("blend")] = (new LingoDecimal(80)/prop.repeatl.count)});
newmem.image.copypixels(_global.member(@"previewImprt").image,newmem.image.rect,getrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
break;
case @"simpleDecal":
case @"soft":
case @"antimatter":
newmem.image = _global.image(_global.member(@"previewImprt").image.width,_global.member(@"previewImprt").image.height,16);
newmem.image.copypixels(_global.member(@"previewImprt").image,newmem.image.rect,_global.member(@"previewImprt").image.rect);
break;
case @"variedDecal":
case @"variedSoft":
newmem.image = _global.image(prop.pxlsize.loch,prop.pxlsize.locv,16);
newmem.image.copypixels(_global.member(@"previewImprt").image,newmem.image.rect,LingoGlobal.rect(0,0,prop.pxlsize.loch,prop.pxlsize.locv));
break;
case @"variedStandard":
newmem.image = _global.image(((prop.sz.loch*20)*prop.vars),(prop.sz.locv*20),16);
for (int tmp_c = 1; tmp_c <= prop.repeatl.count; tmp_c++) {
c = tmp_c;
c2 = ((prop.repeatl.count+1)-c);
getrect = (LingoGlobal.rect(0,(((c2-1)*prop.sz.locv)*20),((prop.sz.loch*20)*prop.vars),((c2*prop.sz.locv)*20))+LingoGlobal.rect(0,1,0,1));
newmem.image.copypixels(_global.member(@"pxl").image,newmem.image.rect,LingoGlobal.rect(0,0,1,1),new LingoPropertyList {[new LingoSymbol("color")] = _global.color(255,255,255),[new LingoSymbol("blend")] = (new LingoDecimal(80)/prop.repeatl.count)});
newmem.image.copypixels(_global.member(@"previewImprt").image,newmem.image.rect,getrect,new LingoPropertyList {[new LingoSymbol("ink")] = 36});
}
break;
case @"rope":
case @"long":
newmem.image = _global.image(_global.member(@"previewImprt").image.width,_global.member(@"previewImprt").image.height,16);
newmem.image.copypixels(_global.member(@"previewImprt").image,newmem.image.rect,_global.member(@"previewImprt").image.rect);
break;
}
newmem.name = LingoGlobal.concat_space(@"propPreview",prop.nm);
_movieScript.global_loadedproppreviews.add(prop.nm);
return newmem;

}
public dynamic duplicatepropsettings() {
dynamic doit = null;
doit = LingoGlobal.op_eq(_movieScript.global_settingsproptype,LingoGlobal.VOID);
if ((doit == 0)) {
if ((_movieScript.global_settingsproptype.nm != _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].nm)) {
doit = 1;
}
}
if (LingoGlobal.ToBool(doit)) {
_movieScript.global_propsettings = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv].settings.duplicate();
_movieScript.global_propsettings.seed = _global.random(1000);
_movieScript.global_settingsproptype = _movieScript.global_gprops[_movieScript.global_gpeprops.pmpos.loch].prps[_movieScript.global_gpeprops.pmpos.locv];
}

return null;
}
}
}
