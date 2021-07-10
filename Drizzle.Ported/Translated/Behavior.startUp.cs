using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: startUp
//
public sealed class startUp : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic g = null;
dynamic fac = null;
dynamic screenresolutionpoint = null;
dynamic screensize = null;
dynamic midpos = null;
dynamic windowrect = null;
dynamic cols = null;
dynamic rows = null;
dynamic q = null;
dynamic ql = null;
dynamic c = null;
dynamic l = null;
dynamic sav = null;
dynamic ptpos = null;
dynamic vl = null;
dynamic ad = null;
dynamic debugline = null;
dynamic sav2 = null;
dynamic calculatedheight = null;
dynamic rct = null;
dynamic strng = null;
dynamic dp = null;
dynamic i = null;
dynamic tl = null;
dynamic t = null;
dynamic mem = null;
_global._global.clearglobals();
_movieScript.global_gfullrender = 1;
_movieScript.global_gviewrender = 1;
_movieScript.global_gmassrenderl = new LingoPropertyList {};
_movieScript.global_gloadpath = new LingoPropertyList {};
_movieScript.global_glevel = new LingoPropertyList {[new LingoSymbol("timelimit")] = 4800,[new LingoSymbol("defaultterrain")] = 1,[new LingoSymbol("maxflies")] = 10,[new LingoSymbol("flyspawnrate")] = 50,[new LingoSymbol("lizards")] = new LingoPropertyList {},[new LingoSymbol("ambientsounds")] = new LingoPropertyList {},[new LingoSymbol("music")] = @"NONE",[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("lighttype")] = @"Static",[new LingoSymbol("waterdrips")] = 1,[new LingoSymbol("lightrect")] = LingoGlobal.rect(0,0,1040,800),[new LingoSymbol("matrix")] = new LingoPropertyList {}};
_global._movie.window.appearanceoptions.border = new LingoSymbol("none");
_global._movie.window.resizable = LingoGlobal.FALSE;
_movieScript.global_gloadedname = @"New Project";
_global.member(@"level Name").text = @"New Project";
_movieScript.global_gimgxtra = _global.xtra(@"ImgXtra").@new();
g = 21;
if ((g == 2)) {
_movieScript.global_gsaveprops = new LingoList(new dynamic[] { _global.bascreeninfo(@"width"),_global.bascreeninfo(@"height"),_global.bascreeninfo(@"depth") });
fac = (LingoGlobal.floatmember_helper(_movieScript.global_gsaveprops[1])/LingoGlobal.floatmember_helper(_movieScript.global_gsaveprops[2]));
screenresolutionpoint = LingoGlobal.point(1024,768);
_global.basetdisplay(screenresolutionpoint.loch,screenresolutionpoint.locv,32,@"temp",LingoGlobal.FALSE);
screensize = (LingoGlobal.point(1024,768)/2);
midpos = (screenresolutionpoint/2);
windowrect = LingoGlobal.rect((midpos-screensize),(midpos+screensize));
_global._movie.window.rect = windowrect;
_global._movie.stage.drawrect = LingoGlobal.rect(0,0,1024,768);
}
else {
_movieScript.global_gsaveprops = new LingoList(new dynamic[] { 1,1,1 });
}
_movieScript.global_solidmtrx = new LingoPropertyList {};
cols = 72;
rows = 43;
_movieScript.global_gleprops = new LingoPropertyList {[new LingoSymbol("matrix")] = new LingoPropertyList {},[new LingoSymbol("leveleditors")] = new LingoPropertyList {},[new LingoSymbol("toolmatrix")] = new LingoPropertyList {},[new LingoSymbol("campos")] = LingoGlobal.point(0,0)};
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"inverse",@"paintWall",@"paintAir",@"slope" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"floor",@"squareWall",@"squareAir",@"move" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"rock",@"spear",@"crack",@"" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"horBeam",@"verBeam",@"glass",@"copyBack" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"shortCutEntrance",@"shortCut",@"lizardHole",@"playerSpawn" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"forbidbats",@"spawnfly",@"hive",@"waterFall" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"scavengerHole",@"WHAMH",@"garbageHole",@"wormGrass" }));
_movieScript.global_gleprops.toolmatrix.add(new LingoList(new dynamic[] { @"workLayer",@"flip",@"mirrorToggle",@"setMirrorPoint" }));
_movieScript.resetgenveditorprops();
for (int tmp_q = 1; tmp_q <= cols; tmp_q++) {
q = tmp_q;
ql = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= rows; tmp_c++) {
c = tmp_c;
ql.add(new LingoList(new dynamic[] { new LingoList(new dynamic[] { 1,new LingoPropertyList {} }),new LingoList(new dynamic[] { 1,new LingoPropertyList {} }),new LingoList(new dynamic[] { 0,new LingoPropertyList {} }) }));
}
_movieScript.global_gleprops.matrix.add(ql);
}
_movieScript.global_gbluroptions = new LingoPropertyList {[new LingoSymbol("blurlight")] = 0,[new LingoSymbol("blursky")] = 0};
_movieScript.global_gteprops = new LingoPropertyList {[new LingoSymbol("lastkeys")] = new LingoPropertyList {},[new LingoSymbol("keys")] = new LingoPropertyList {},[new LingoSymbol("worklayer")] = 1,[new LingoSymbol("lstmsps")] = LingoGlobal.point(0,0),[new LingoSymbol("tlmatrix")] = new LingoPropertyList {},[new LingoSymbol("defaultmaterial")] = @"Concrete",[new LingoSymbol("tooltype")] = @"material",[new LingoSymbol("tooldata")] = @"Big Metal",[new LingoSymbol("tmPos")] = LingoGlobal.point(1,1),[new LingoSymbol("tmsavposl")] = new LingoPropertyList {},[new LingoSymbol("specialedit")] = 0};
for (int tmp_q = 1; tmp_q <= cols; tmp_q++) {
q = tmp_q;
l = new LingoPropertyList {};
for (int tmp_c = 1; tmp_c <= rows; tmp_c++) {
c = tmp_c;
l.add(new LingoList(new dynamic[] { new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0},new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0},new LingoPropertyList {[new LingoSymbol("tp")] = @"default",[new LingoSymbol("data")] = 0} }));
}
_movieScript.global_gteprops.tlmatrix.add(l);
}
_global.member(@"layerText").text = @"Layer:1";
_movieScript.global_gtiles = new LingoPropertyList {};
_movieScript.global_gtiles.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Materials",[new LingoSymbol("tls")] = new LingoPropertyList {}});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Standard",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(150,150,150)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Concrete",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(150,255,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"RainStone",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(0,0,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Bricks",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(200,150,100)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"BigMetal",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(255,0,0)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Tiny Signs",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(255,255,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Scaffolding",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(60,60,40)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Dense Pipes",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"densePipeType",[new LingoSymbol("color")] = _global.color(10,10,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"SuperStructure",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(160,180,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"SuperStructure2",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(190,160,0)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Tiled Stone",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"tiles",[new LingoSymbol("color")] = _global.color(100,0,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Chaotic Stone",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"tiles",[new LingoSymbol("color")] = _global.color(255,0,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Small Pipes",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"pipeType",[new LingoSymbol("color")] = _global.color(255,255,0)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Trash",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"pipeType",[new LingoSymbol("color")] = _global.color(90,255,0)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Invisible",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"unified",[new LingoSymbol("color")] = _global.color(200,200,200)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"LargeTrash",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"largeTrashType",[new LingoSymbol("color")] = _global.color(150,30,255)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"3DBricks",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"tiles",[new LingoSymbol("color")] = _global.color(255,150,0)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Random Machines",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"tiles",[new LingoSymbol("color")] = _global.color(72,116,80)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Dirt",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"dirtType",[new LingoSymbol("color")] = _global.color(124,72,52)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Ceramic Tile",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"ceramicType",[new LingoSymbol("color")] = _global.color(60,60,100)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Temple Stone",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"tiles",[new LingoSymbol("color")] = _global.color(0,120,180)});
_movieScript.global_gtiles[1].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Circuits",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("rendertype")] = @"densePipeType",[new LingoSymbol("color")] = _global.color(15,200,15)});
_movieScript.global_gtiles.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Special",[new LingoSymbol("tls")] = new LingoPropertyList {}});
_movieScript.global_gtiles[2].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Rect Clear",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("placemethod")] = @"rect",[new LingoSymbol("color")] = _global.color(255,0,0)});
_movieScript.global_gtiles[2].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"SH pattern box",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("placemethod")] = @"rect",[new LingoSymbol("color")] = _global.color(210,0,255)});
_movieScript.global_gtiles[2].tls.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"SH grate box",[new LingoSymbol("sz")] = LingoGlobal.point(1,1),[new LingoSymbol("specs")] = new LingoList(new dynamic[] { 0 }),[new LingoSymbol("placemethod")] = @"rect",[new LingoSymbol("color")] = _global.color(160,0,255)});
sav = _global.member(@"initImport");
_global.member(@"initImport").importfileinto(@"Graphics\init.txt");
sav.name = @"initImport";
_global.member(@"previewTiles").image = _global.image(60000,500,1);
ptpos = 1;
for (int tmp_q = 1; tmp_q <= LingoGlobal.thenumberoflines_helper(sav.text); tmp_q++) {
q = tmp_q;
if ((sav.text.line[q] != @"")) {
if ((LingoGlobal.charmember_helper(sav.text.line[q])[1] == @"-")) {
vl = _global.value(_global.slice_helper(LingoGlobal.charmember_helper(sav.text.line[q]),2,sav.text.line[q].length));
_movieScript.global_gtiles.add(new LingoPropertyList {[new LingoSymbol("nm")] = vl[1],[new LingoSymbol("clr")] = vl[2],[new LingoSymbol("tls")] = new LingoPropertyList {}});
}
else {
ad = _global.value(sav.text.line[q]);
debugline = sav.text.line[q];
sav2 = _global.member(@"previewImprt");
_global.member(@"previewImprt").importfileinto(LingoGlobal.concat(LingoGlobal.concat(@"Graphics\",ad.nm),@".png"));
sav2.name = @"previewImprt";
calculatedheight = sav2.image.rect.height;
if ((ad.tp == @"voxelStruct")) {
calculatedheight = ((1+(16*ad.sz.locv))+((20*(ad.sz.locv+(ad.bftiles*2)))*ad.repeatl.count));
}
rct = LingoGlobal.rect(0,(calculatedheight-(16*ad.sz.locv)),(16*ad.sz.loch),calculatedheight);
_global.member(@"previewTiles").image.copypixels(sav2.image,LingoGlobal.rect(ptpos,0,(ptpos+(16*ad.sz.loch)),(16*ad.sz.locv)),rct);
ad.ptpos = ptpos;
ad.addprop(new LingoSymbol("category"),_movieScript.global_gtiles.count);
_movieScript.global_gtiles[_movieScript.global_gtiles.count].tls.add(ad);
ptpos = ((ptpos+(16*ad.sz.loch))+1);
}
}
}
_movieScript.global_gprops = new LingoPropertyList {};
_movieScript.resetpropeditorprops();
_movieScript.global_gpecolors = new LingoPropertyList {};
sav = _global.member(@"initImport");
_global.member(@"initImport").importfileinto(@"Props\propColors.txt");
sav.name = @"initImport";
for (int tmp_q = 1; tmp_q <= LingoGlobal.thenumberoflines_helper(sav.text); tmp_q++) {
q = tmp_q;
if ((sav.text.line[q] != @"")) {
_movieScript.global_gpecolors.add(_global.value(sav.text.line[q]));
}
}
sav = _global.member(@"initImport");
_global.member(@"initImport").importfileinto(@"Props\init.txt");
sav.name = @"initImport";
for (int tmp_q = 1; tmp_q <= 2000; tmp_q++) {
q = tmp_q;
_global.member(q,2).erase();
}
for (int tmp_q = 1; tmp_q <= LingoGlobal.thenumberoflines_helper(sav.text); tmp_q++) {
q = tmp_q;
if ((sav.text.line[q] != @"")) {
if ((LingoGlobal.charmember_helper(sav.text.line[q])[1] == @"-")) {
vl = _global.value(_global.slice_helper(LingoGlobal.charmember_helper(sav.text.line[q]),2,sav.text.line[q].length));
_movieScript.global_gprops.add(new LingoPropertyList {[new LingoSymbol("nm")] = vl[1],[new LingoSymbol("clr")] = vl[2],[new LingoSymbol("prps")] = new LingoPropertyList {}});
}
else {
ad = _global.value(sav.text.line[q]);
strng = sav.text.line[q];
ad.addprop(new LingoSymbol("category"),_movieScript.global_gprops.count);
if (((ad.tp == @"standard") | (ad.tp == @"variedStandard"))) {
dp = 0;
for (int tmp_i = 1; tmp_i <= ad.repeatl.count; tmp_i++) {
i = tmp_i;
dp = (dp+ad.repeatl[i]);
}
ad.addprop(new LingoSymbol("depth"),dp);
}
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(ad);
}
}
}
_movieScript.global_gprops.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Tiles as props",[new LingoSymbol("clr")] = _global.color(255,0,0),[new LingoSymbol("prps")] = new LingoPropertyList {}});
for (int tmp_q = 3; tmp_q <= _movieScript.global_gtiles.count; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gtiles[q].tls.count; tmp_c++) {
c = tmp_c;
tl = _movieScript.global_gtiles[q].tls[c];
if (((tl.tp == @"voxelStruct") & (tl.tags.getpos(@"notProp") == 0))) {
ad = new LingoPropertyList {[new LingoSymbol("nm")] = tl.nm,[new LingoSymbol("tp")] = @"standard",[new LingoSymbol("colortreatment")] = @"standard",[new LingoSymbol("sz")] = (tl.sz+LingoGlobal.point((tl.bftiles*2),(tl.bftiles*2))),[new LingoSymbol("depth")] = ((10+LingoGlobal.op_ne(tl.specs2,new LingoPropertyList {}))*10),[new LingoSymbol("repeatl")] = tl.repeatl,[new LingoSymbol("tags")] = new LingoList(new dynamic[] { @"Tile" }),[new LingoSymbol("layerexceptions")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoList(new dynamic[] { @"Tile as prop" })};
ad.addprop(new LingoSymbol("category"),_movieScript.global_gprops.count);
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(ad);
}
}
}
_movieScript.global_gprops.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Rope type props",[new LingoSymbol("clr")] = _global.color(0,255,0),[new LingoSymbol("prps")] = new LingoPropertyList {}});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Wire",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 3,[new LingoSymbol("collisiondepth")] = 0,[new LingoSymbol("segrad")] = 1,[new LingoSymbol("grav")] = new LingoDecimal(0.5),[new LingoSymbol("friction")] = new LingoDecimal(0.5),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 0,[new LingoSymbol("previewcolor")] = _global.color(255,0,0),[new LingoSymbol("previewevery")] = 4,[new LingoSymbol("edgedirection")] = 0,[new LingoSymbol("rigid")] = 0,[new LingoSymbol("selfpush")] = 0,[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Tube",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 4,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 10,[new LingoSymbol("collisiondepth")] = 2,[new LingoSymbol("segrad")] = new LingoDecimal(4.5),[new LingoSymbol("grav")] = new LingoDecimal(0.5),[new LingoSymbol("friction")] = new LingoDecimal(0.5),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(0,0,255),[new LingoSymbol("previewevery")] = 2,[new LingoSymbol("edgedirection")] = 5,[new LingoSymbol("rigid")] = new LingoDecimal(1.6),[new LingoSymbol("selfpush")] = 0,[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"ThickWire",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 3,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 4,[new LingoSymbol("collisiondepth")] = 1,[new LingoSymbol("segrad")] = 2,[new LingoSymbol("grav")] = new LingoDecimal(0.5),[new LingoSymbol("friction")] = new LingoDecimal(0.8),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,255,0),[new LingoSymbol("previewevery")] = 2,[new LingoSymbol("edgedirection")] = 0,[new LingoSymbol("rigid")] = new LingoDecimal(0.2),[new LingoSymbol("selfpush")] = 0,[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"RidgedTube",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 4,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 5,[new LingoSymbol("collisiondepth")] = 2,[new LingoSymbol("segrad")] = 5,[new LingoSymbol("grav")] = new LingoDecimal(0.5),[new LingoSymbol("friction")] = new LingoDecimal(0.3),[new LingoSymbol("airfric")] = new LingoDecimal(0.7),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,0,255),[new LingoSymbol("previewevery")] = 2,[new LingoSymbol("edgedirection")] = 0,[new LingoSymbol("rigid")] = new LingoDecimal(0.1),[new LingoSymbol("selfpush")] = 0,[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Fuel Hose",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 5,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 16,[new LingoSymbol("collisiondepth")] = 1,[new LingoSymbol("segrad")] = 7,[new LingoSymbol("grav")] = new LingoDecimal(0.5),[new LingoSymbol("friction")] = new LingoDecimal(0.8),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,150,0),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(1.4),[new LingoSymbol("rigid")] = new LingoDecimal(0.2),[new LingoSymbol("selfpush")] = 0,[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Broken Fuel Hose",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 6,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 16,[new LingoSymbol("collisiondepth")] = 1,[new LingoSymbol("segrad")] = 7,[new LingoSymbol("grav")] = new LingoDecimal(0.5),[new LingoSymbol("friction")] = new LingoDecimal(0.8),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,150,0),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(1.4),[new LingoSymbol("rigid")] = new LingoDecimal(0.2),[new LingoSymbol("selfpush")] = 0,[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Large Chain",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 9,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 28,[new LingoSymbol("collisiondepth")] = 3,[new LingoSymbol("segrad")] = new LingoDecimal(9.5),[new LingoSymbol("grav")] = new LingoDecimal(0.9),[new LingoSymbol("friction")] = new LingoDecimal(0.8),[new LingoSymbol("airfric")] = new LingoDecimal(0.95),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(0,255,0),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(0),[new LingoSymbol("rigid")] = new LingoDecimal(0),[new LingoSymbol("selfpush")] = new LingoDecimal(6.5),[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Large Chain 2",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 9,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 28,[new LingoSymbol("collisiondepth")] = 3,[new LingoSymbol("segrad")] = new LingoDecimal(9.5),[new LingoSymbol("grav")] = new LingoDecimal(0.9),[new LingoSymbol("friction")] = new LingoDecimal(0.8),[new LingoSymbol("airfric")] = new LingoDecimal(0.95),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(20,205,0),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(0),[new LingoSymbol("rigid")] = new LingoDecimal(0),[new LingoSymbol("selfpush")] = new LingoDecimal(6.5),[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Bike Chain",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 9,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 38,[new LingoSymbol("collisiondepth")] = 3,[new LingoSymbol("segrad")] = new LingoDecimal(16.5),[new LingoSymbol("grav")] = new LingoDecimal(0.9),[new LingoSymbol("friction")] = new LingoDecimal(0.8),[new LingoSymbol("airfric")] = new LingoDecimal(0.95),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(100,100,100),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(0),[new LingoSymbol("rigid")] = new LingoDecimal(0),[new LingoSymbol("selfpush")] = new LingoDecimal(16.5),[new LingoSymbol("sourcepush")] = 0});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Zero-G Tube",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 4,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 10,[new LingoSymbol("collisiondepth")] = 2,[new LingoSymbol("segrad")] = new LingoDecimal(4.5),[new LingoSymbol("grav")] = 0,[new LingoSymbol("friction")] = new LingoDecimal(0.5),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(0,255,0),[new LingoSymbol("previewevery")] = 2,[new LingoSymbol("edgedirection")] = 0,[new LingoSymbol("rigid")] = new LingoDecimal(0.6),[new LingoSymbol("selfpush")] = 2,[new LingoSymbol("sourcepush")] = new LingoDecimal(0.5)});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Zero-G Wire",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 8,[new LingoSymbol("collisiondepth")] = 0,[new LingoSymbol("segrad")] = 1,[new LingoSymbol("grav")] = 0,[new LingoSymbol("friction")] = new LingoDecimal(0.5),[new LingoSymbol("airfric")] = new LingoDecimal(0.9),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,0,0),[new LingoSymbol("previewevery")] = 2,[new LingoSymbol("edgedirection")] = new LingoDecimal(0.3),[new LingoSymbol("rigid")] = new LingoDecimal(0.5),[new LingoSymbol("selfpush")] = new LingoDecimal(1.2),[new LingoSymbol("sourcepush")] = new LingoDecimal(0.5)});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Fat Hose",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 6,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 40,[new LingoSymbol("collisiondepth")] = 3,[new LingoSymbol("segrad")] = 20,[new LingoSymbol("grav")] = new LingoDecimal(0.9),[new LingoSymbol("friction")] = new LingoDecimal(0.6),[new LingoSymbol("airfric")] = new LingoDecimal(0.95),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(0,100,150),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(0.1),[new LingoSymbol("rigid")] = new LingoDecimal(0.2),[new LingoSymbol("selfpush")] = 10,[new LingoSymbol("sourcepush")] = new LingoDecimal(0.1)});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Wire Bunch",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 9,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 50,[new LingoSymbol("collisiondepth")] = 3,[new LingoSymbol("segrad")] = 20,[new LingoSymbol("grav")] = new LingoDecimal(0.9),[new LingoSymbol("friction")] = new LingoDecimal(0.6),[new LingoSymbol("airfric")] = new LingoDecimal(0.95),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,100,150),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(0.1),[new LingoSymbol("rigid")] = new LingoDecimal(0.2),[new LingoSymbol("selfpush")] = 10,[new LingoSymbol("sourcepush")] = new LingoDecimal(0.1)});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Wire Bunch 2",[new LingoSymbol("tp")] = @"rope",[new LingoSymbol("depth")] = 9,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {},[new LingoSymbol("segmentlength")] = 50,[new LingoSymbol("collisiondepth")] = 3,[new LingoSymbol("segrad")] = 20,[new LingoSymbol("grav")] = new LingoDecimal(0.9),[new LingoSymbol("friction")] = new LingoDecimal(0.6),[new LingoSymbol("airfric")] = new LingoDecimal(0.95),[new LingoSymbol("stiff")] = 1,[new LingoSymbol("previewcolor")] = _global.color(255,100,150),[new LingoSymbol("previewevery")] = 1,[new LingoSymbol("edgedirection")] = new LingoDecimal(0.1),[new LingoSymbol("rigid")] = new LingoDecimal(0.2),[new LingoSymbol("selfpush")] = 10,[new LingoSymbol("sourcepush")] = new LingoDecimal(0.1)});
_movieScript.global_gprops.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Long props",[new LingoSymbol("clr")] = _global.color(0,255,0),[new LingoSymbol("prps")] = new LingoPropertyList {}});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Cabinet Clamp",[new LingoSymbol("tp")] = @"long",[new LingoSymbol("depth")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {}});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Drill Suspender",[new LingoSymbol("tp")] = @"long",[new LingoSymbol("depth")] = 5,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {}});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Thick Chain",[new LingoSymbol("tp")] = @"long",[new LingoSymbol("depth")] = 0,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {}});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Drill",[new LingoSymbol("tp")] = @"long",[new LingoSymbol("depth")] = 10,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {}});
_movieScript.global_gprops[_movieScript.global_gprops.count].prps.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Piston",[new LingoSymbol("tp")] = @"long",[new LingoSymbol("depth")] = 4,[new LingoSymbol("tags")] = new LingoPropertyList {},[new LingoSymbol("notes")] = new LingoPropertyList {}});
_movieScript.global_gtrashpropoptions = new LingoPropertyList {};
for (int tmp_q = 1; tmp_q <= _movieScript.global_gprops.count; tmp_q++) {
q = tmp_q;
for (int tmp_c = 1; tmp_c <= _movieScript.global_gprops[q].prps.count; tmp_c++) {
c = tmp_c;
_movieScript.global_gprops[q].prps[c].addprop(new LingoSymbol("settings"),new LingoPropertyList {});
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("renderorder"),0);
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("seed"),500);
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("rendertime"),0);
switch (_movieScript.global_gprops[q].prps[c].tp) {
case @"standard":
case @"variedStandard":
if ((_movieScript.global_gprops[q].prps[c].colortreatment == @"bevel")) {
_movieScript.global_gprops[q].prps[c].notes.add(@"The highlights and shadows on this prop are generated by code, so it can be rotated to any degree and they will remain correct.");
}
else {
_movieScript.global_gprops[q].prps[c].notes.add(@"Be aware that shadows and highlights will not rotate with the prop, so extreme rotations may cause incorrect shading.");
}
if ((_movieScript.global_gprops[q].prps[c].tp == @"variedStandard")) {
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("variation"),LingoGlobal.op_eq(_movieScript.global_gprops[q].prps[c].random,0));
if (LingoGlobal.ToBool(_movieScript.global_gprops[q].prps[c].random)) {
_movieScript.global_gprops[q].prps[c].notes.add(@"Will put down a random variation. A specific variation can be selected from settings ('N' key).");
}
else {
_movieScript.global_gprops[q].prps[c].notes.add(@"This prop comes with many variations. Which variation can be selected from settings ('N' key).");
}
}
else if (((_movieScript.global_gprops[q].prps[c].sz.loch < 5) & (_movieScript.global_gprops[q].prps[c].sz.locv < 5))) {
_movieScript.global_gtrashpropoptions.add(LingoGlobal.point(q,c));
if (((_movieScript.global_gprops[q].prps[c].sz.loch < 3) | (_movieScript.global_gprops[q].prps[c].sz.locv < 3))) {
_movieScript.global_gtrashpropoptions.add(LingoGlobal.point(q,c));
}
}
break;
case @"rope":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("release"),0);
break;
case @"variedDecal":
case @"variedSoft":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("variation"),LingoGlobal.op_eq(_movieScript.global_gprops[q].prps[c].random,0));
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("customdepth"),_movieScript.global_gprops[q].prps[c].depth);
if (LingoGlobal.ToBool(_movieScript.global_gprops[q].prps[c].random)) {
_movieScript.global_gprops[q].prps[c].notes.add(@"Will put down a random variation. A specific variation can be selected from settings ('N' key).");
}
else {
_movieScript.global_gprops[q].prps[c].notes.add(@"This prop comes with many variations. Which variation can be selected from settings ('N' key).");
}
if ((_movieScript.global_gprops[q].prps[c].tp == @"variedSoft")) {
if (LingoGlobal.ToBool(_movieScript.global_gprops[q].prps[c].colorize)) {
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("applycolor"),1);
_movieScript.global_gprops[q].prps[c].notes.add(@"It's recommended to render this prop after the effects if the color is activated, as the effects won't affect the color layers.");
}
}
break;
case @"simpleDecal":
case @"soft":
case @"antimatter":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("customdepth"),_movieScript.global_gprops[q].prps[c].depth);
break;
}
if (((_movieScript.global_gprops[q].prps[c].tp == @"soft") | (_movieScript.global_gprops[q].prps[c].tp == @"variedSoft"))) {
if ((_movieScript.global_gprops[q].prps[c].selfshade == 1)) {
_movieScript.global_gprops[q].prps[c].notes.add(@"The highlights and shadows on this prop are generated by code, so it can be rotated to any degree and they will remain correct.");
}
else {
_movieScript.global_gprops[q].prps[c].notes.add(@"Be aware that shadows and highlights will not rotate with the prop, so extreme rotations may cause incorrect shading.");
}
}
switch (_movieScript.global_gprops[q].prps[c].nm) {
case @"wire":
case @"Zero-G Wire":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("thickness"),2);
_movieScript.global_gprops[q].prps[c].notes.add(@"The thickness of the wire can be set in settings.");
break;
case @"Zero-G Tube":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("applycolor"),0);
_movieScript.global_gprops[q].prps[c].notes.add(@"The tube can be colored white through the settings.");
break;
}
foreach (dynamic tmp_t in _movieScript.global_gprops[q].prps[c].tags) {
t = tmp_t;
switch (t) {
case @"customColor":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("color"),0);
_movieScript.global_gprops[q].prps[c].notes.add(@"Custom color available");
break;
case @"customColorRainBow":
_movieScript.global_gprops[q].prps[c].settings.addprop(new LingoSymbol("color"),1);
_movieScript.global_gprops[q].prps[c].notes.add(@"Custom color available");
break;
}
}
}
}
_movieScript.global_geffects = new LingoPropertyList {};
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Natural",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Slime"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Melt"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Rust"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Barnacles"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Rubble"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"DecalsOnlySlime"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Erosion",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Roughen"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"SlimeX3"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Super Melt"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Destructive Melt"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Erode"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Super Erode"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"DaddyCorruption"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Artificial",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Wires"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Chains"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Plants",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Root Grass"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Seed Pods"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Growers"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Cacti"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Rain Moss"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Hang Roots"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Grass"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Plants2",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Arm Growers"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Horse Tails"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Circuit Plants"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Feather Plants"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Thorn Growers"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Rollers"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Garbage Spirals"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Plants3",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Thick Roots"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Shadow Plants"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Plants (Individual)",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Fungi Flowers"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Lighthouse Flowers"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Fern"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Giant Mushroom"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Sprawlbush"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"featherFern"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Fungus Tree"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Paint Effects",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"BlackGoo"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"DarkSlime"});
_movieScript.global_geffects.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Restoration",[new LingoSymbol("efs")] = new LingoPropertyList {}});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Restore As Scaffolding"});
_movieScript.global_geffects[_movieScript.global_geffects.count].efs.add(new LingoPropertyList {[new LingoSymbol("nm")] = @"Ceramic Chaos"});
_movieScript.global_geeprops = new LingoPropertyList {[new LingoSymbol("lastkeys")] = new LingoPropertyList {},[new LingoSymbol("keys")] = new LingoPropertyList {},[new LingoSymbol("lstmsps")] = LingoGlobal.point(0,0),[new LingoSymbol("effects")] = new LingoPropertyList {},[new LingoSymbol("emPos")] = LingoGlobal.point(1,1),[new LingoSymbol("editeffect")] = 0,[new LingoSymbol("selectediteffect")] = 0,[new LingoSymbol("mode")] = @"createNew",[new LingoSymbol("brushsize")] = 5};
_movieScript.global_glighteprops = new LingoPropertyList {[new LingoSymbol("pos")] = LingoGlobal.point((1040/2),(800/2)),[new LingoSymbol("rot")] = 0,[new LingoSymbol("sz")] = LingoGlobal.point(50,70),[new LingoSymbol("col")] = 1,[new LingoSymbol("keys")] = 0,[new LingoSymbol("lastkeys")] = 0,[new LingoSymbol("lasttm")] = 0,[new LingoSymbol("lightangle")] = 180,[new LingoSymbol("flatness")] = 1,[new LingoSymbol("lightrect")] = LingoGlobal.rect(1000,1000,-1000,-1000),[new LingoSymbol("paintshape")] = @"pxl"};
_movieScript.global_gloprops = new LingoPropertyList {[new LingoSymbol("mouse")] = 0,[new LingoSymbol("lastmouse")] = 0,[new LingoSymbol("mouseclick")] = 0,[new LingoSymbol("pal")] = 1,[new LingoSymbol("pals")] = new LingoList(new dynamic[] { new LingoPropertyList {[new LingoSymbol("detcol")] = _global.color(255,0,0)} }),[new LingoSymbol("ecol1")] = 1,[new LingoSymbol("ecol2")] = 2,[new LingoSymbol("totecols")] = 5,[new LingoSymbol("tileseed")] = _global.random(400),[new LingoSymbol("colglows")] = new LingoList(new dynamic[] { 0,0 }),[new LingoSymbol("size")] = LingoGlobal.point(cols,rows),[new LingoSymbol("extratiles")] = new LingoList(new dynamic[] { 12,3,12,5 }),[new LingoSymbol("light")] = 1};
_global.new_script(@"levelEdit_parentscript",new LingoList(new dynamic[] { 1 }));
_global.new_script(@"levelEdit_parentscript",new LingoList(new dynamic[] { 2 }));
_movieScript.global_gcameraprops = new LingoPropertyList {[new LingoSymbol("cameras")] = new LingoList(new dynamic[] { (LingoGlobal.point((_movieScript.global_gloprops.size.loch*10),(_movieScript.global_gloprops.size.locv*10))-LingoGlobal.point((35*20),(20*20))) }),[new LingoSymbol("selectedcamera")] = 0,[new LingoSymbol("quads")] = new LingoList(new dynamic[] { new LingoList(new dynamic[] { new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }),new LingoList(new dynamic[] { 0,0 }) }) }),[new LingoSymbol("keys")] = new LingoPropertyList {[new LingoSymbol("n")] = 0,[new LingoSymbol("d")] = 0,[new LingoSymbol("e")] = 0,[new LingoSymbol("p")] = 0},[new LingoSymbol("lastkeys")] = new LingoPropertyList {[new LingoSymbol("n")] = 0,[new LingoSymbol("d")] = 0,[new LingoSymbol("e")] = 0,[new LingoSymbol("p")] = 0}};
_movieScript.global_gseprops = new LingoPropertyList {[new LingoSymbol("sounds")] = LingoGlobal.VOID,[new LingoSymbol("ambientsounds")] = new LingoPropertyList {},[new LingoSymbol("songs")] = new LingoPropertyList {},[new LingoSymbol("rects")] = new LingoPropertyList {},[new LingoSymbol("pickedupsound")] = @"NONE"};
foreach (dynamic tmp_mem in new LingoList(new dynamic[] { @"rainBowMask",@"blackOutImg1",@"blackOutImg2" })) {
mem = tmp_mem;
_global.member(mem).image = _global.image(1,1,1);
}
_global.member(@"lightImage").image = _global.image(((_movieScript.global_gloprops.size.loch*20)+300),((_movieScript.global_gloprops.size.locv*20)+300),1);
for (int tmp_i = 0; tmp_i <= 29; tmp_i++) {
i = tmp_i;
_global.member(LingoGlobal.concat(@"layer",i)).image = _global.image(1,1,1);
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",i),@"sh")).image = _global.image(1,1,1);
_global.member(LingoGlobal.concat(@"gradientA",i)).image = _global.image(1,1,1);
_global.member(LingoGlobal.concat(@"gradientB",i)).image = _global.image(1,1,1);
_global.member(LingoGlobal.concat(LingoGlobal.concat(@"layer",i),@"dc")).image = _global.image(1,1,1);
_global.member(@"dumpImage").image = _global.image(1,1,1);
_global.member(@"finalDecalImage").image = _global.image(1,1,1);
}

return null;
}
}
}
