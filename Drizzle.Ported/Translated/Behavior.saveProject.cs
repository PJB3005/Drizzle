using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Behavior script: saveProject
//
public sealed class saveProject : LingoBehaviorScript {
public dynamic exitframe(dynamic me) {
dynamic str = null;
dynamic objfileio = null;
dynamic pth = null;
dynamic f = null;
dynamic gimgxtra = null;
dynamic nwimg = null;
dynamic props = null;
dynamic ok = null;
if (LingoGlobal.ToBool(_global._key.keypressed(36))) {
str = @"";
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
str += str.ToString();
objfileio = _global.@new(_global.xtra(@"fileio"));
pth = LingoGlobal.concat(_global.the_moviePath,@"LevelEditorProjects\");
foreach (dynamic tmp_f in _movieScript.global_gloadpath) {
f = tmp_f;
pth = LingoGlobal.concat(LingoGlobal.concat(pth,f),@"\");
}
_global.createfile(objfileio,LingoGlobal.concat(LingoGlobal.concat(pth,_movieScript.global_levelname),@".txt"));
objfileio.openfile(LingoGlobal.concat(LingoGlobal.concat(pth,_movieScript.global_levelname),@".txt"),0);
objfileio.writestring(str);
objfileio.closefile();
_global.member(@"lightImage").image.setpixel(0,0,_global.color(0,0,0));
_global.member(@"lightImage").image.setpixel((_global.member(@"lightImage").rect.width-1),(_global.member(@"lightImage").rect.height-1),_global.color(0,0,0));
gimgxtra = _global.xtra(@"ImgXtra").@new();
nwimg = _global.image(_global.member(@"lightImage").image.rect.width,_global.member(@"lightImage").image.rect.height,32);
nwimg.copypixels(_global.member(@"lightImage").image,LingoGlobal.rect(0,0,_global.member(@"lightImage").image.rect.width,_global.member(@"lightImage").image.rect.height),LingoGlobal.rect(0,0,_global.member(@"lightImage").image.rect.width,_global.member(@"lightImage").image.rect.height));
props = new LingoPropertyList {[@"image"] = nwimg,[@"filename"] = LingoGlobal.concat(LingoGlobal.concat(pth,_movieScript.global_levelname),@".png")};
ok = gimgxtra.ix_saveimage(props);
_movieScript.global_gloadedname = _movieScript.global_levelname;
_global.member(@"Level Name").text = _movieScript.global_gloadedname;
_global._movie.go(7);
}
else {
_global.go(_global.the_frame);
}

return null;
}
}
}
