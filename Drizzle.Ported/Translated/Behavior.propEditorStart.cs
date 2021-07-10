using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: propEditorStart
//
public sealed class propEditorStart : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic l = null;
dynamic q = null;
dynamic actualsettings = null;
dynamic idealsettings = null;
dynamic i = null;
dynamic smbl = null;
dynamic propsettings = null;
_global.member(@"TEimg1").image = _global.image((52*16),(40*16),16);
_global.member(@"TEimg2").image = _global.image((52*16),(40*16),16);
_global.member(@"TEimg3").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage1").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage2").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImage3").image = _global.image((52*16),(40*16),16);
_global.member(@"levelEditImageShortCuts").image = _global.image((52*16),(40*16),16);
_global.member(@"ropePreview").image = _global.image((52*16),(40*16),1);
_global.sprite(20).loc = LingoGlobal.point(432,336);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),1);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),2);
_movieScript.tedraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),3);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),1);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),2);
_movieScript.lvleditdraw(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),3);
_movieScript.drawshortcutsimg(LingoGlobal.rect(1,1,_movieScript.global_gloprops.size.loch,_movieScript.global_gloprops.size.locv),16);
_movieScript.global_gdirectionkeys = new LingoList(new dynamic[] { 0,0,0,0 });
_global.sprite(1).blend = 20;
_global.sprite(2).blend = 20;
_global.sprite(3).blend = 40;
_global.sprite(4).blend = 40;
_global.sprite(5).blend = 90;
_global.sprite(6).blend = 90;
_global.sprite(8).blend = 80;
_global.sprite(8).visibility = 0;
_movieScript.global_gpeprops.worklayer = 1;
_global.sprite(1).loc = LingoGlobal.point(432,336);
_global.sprite(2).loc = LingoGlobal.point(432,336);
_global.sprite(3).loc = LingoGlobal.point(432,336);
_global.sprite(4).loc = LingoGlobal.point(432,336);
_global.sprite(5).loc = LingoGlobal.point(432,336);
_global.sprite(6).loc = LingoGlobal.point(432,336);
_global.sprite(11).loc = LingoGlobal.point(432,336);
_global.sprite(13).loc = LingoGlobal.point(432,336);
l = new LingoPropertyList {[new LingoSymbol("w")] = 0,[new LingoSymbol("a")] = 0,[new LingoSymbol("s")] = 0,[new LingoSymbol("d")] = 0,[new LingoSymbol("l")] = 0,[new LingoSymbol("n")] = 0,[new LingoSymbol("m1")] = 0,[new LingoSymbol("m2")] = 0,[new LingoSymbol("c")] = 0,[new LingoSymbol("z")] = 0};
_movieScript.global_gpeprops.lastkeys = l.duplicate();
_movieScript.global_gpeprops.keys = l.duplicate();
_movieScript.global_pesavedflip = LingoGlobal.point(0,0);
_movieScript.global_pesavedstretch = LingoGlobal.point(0,0);
_movieScript.global_pescrollpos = 0;
_movieScript.global_gpeblink = 0;
_movieScript.global_pefreequad = new LingoList(new dynamic[] { LingoGlobal.point(0,0),LingoGlobal.point(0,0),LingoGlobal.point(0,0),LingoGlobal.point(0,0) });
_movieScript.global_settingcursor = 1;
_movieScript.global_gpeprops.pmsavposl = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gprops.count; tmp_q++) {
q = tmp_q;
_movieScript.global_gpeprops.pmsavposl.add(1);
}
_movieScript.global_gpeprops.pmpos = LingoGlobal.point(1,1);
foreach (dynamic tmp_q in _movieScript.global_gpeprops.props) {
q = tmp_q;
actualsettings = q[5].settings;
idealsettings = _movieScript.global_gprops[q[3].loch].prps[q[3].locv].settings;
for (int tmp_i = 1; tmp_i <= idealsettings.count; tmp_i++) {
i = tmp_i;
smbl = idealsettings.getpropat(i);
if ((actualsettings.findpos(smbl) == LingoGlobal.VOID)) {
actualsettings.addprop(smbl,idealsettings[i]);
}
}
}
_movieScript.global_editsettingsprop = -1;
_movieScript.global_settingsproptype = LingoGlobal.VOID;
propsettings = LingoGlobal.VOID;
if ((_movieScript.global_gpeprops.color == 0)) {
_global.member(@"Prop Color Text").text = LingoGlobal.concat(@"PROP COLOR: ",@"NONE");
_global.sprite(21).color = _global.color(150,150,150);
}
else {
_global.member(@"Prop Color Text").text = LingoGlobal.concat(@"PROP COLOR: ",_movieScript.global_gpecolors[_movieScript.global_gpeprops.color][1]);
_global.sprite(21).color = _movieScript.global_gpecolors[_movieScript.global_gpeprops.color][2];
}
_global.script(@"propEditor").updateworklayertext();
_global.script(@"propEditor").renderpropsimage();
_global.call(new LingoSymbol("updatepropmenu"),LingoGlobal.point(0,0));

return null;
}
}
}
