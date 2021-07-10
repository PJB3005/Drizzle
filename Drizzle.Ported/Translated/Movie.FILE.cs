using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: FILE
//
public sealed partial class MovieScript {
public dynamic exportanimage(dynamic img,dynamic flnm) {
dynamic raw_size = null;
dynamic ms = null;
dynamic enc = null;
dynamic data = null;
raw_size = ((img.width*img.height)*3);
ms = _global.the_milliseconds;
enc = _global.script(@"PNG_encode").@new();
data = enc.png_encode(img);
enc = 0;
file_put_contents(LingoGlobal.concat(LingoGlobal.concat(_global.the_moviepath,flnm),@".png"),data);

return null;
}
public dynamic file_put_contents(dynamic tfile,dynamic tstring) {
dynamic fp = null;
dynamic err = null;
fp = _global.xtra(@"fileIO").@new();
if (!LingoGlobal.ToBool(_global.objectp(fp))) {
return -1;
}
fp.openfile(tfile,1);
err = fp.status();
if (!LingoGlobal.ToBool(err)) {
fp.delete();
}
else if ((LingoGlobal.ToBool(err) & !(err == -37))) {
return err;
}
fp.createfile(tfile);
err = fp.status();
if (LingoGlobal.ToBool(err)) {
return err;
}
fp.openfile(tfile,2);
err = fp.status();
if (LingoGlobal.ToBool(err)) {
return err;
}
fp.writestring(tstring);
fp.closefile();
fp = 0;
return 1;

}
}
}
