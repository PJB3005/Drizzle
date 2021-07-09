using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: spelrelarat
//
public sealed partial class Movie {
public dynamic givegridpos(dynamic pos) {
return _global.point(((pos.locH.float/new LingoGlobal(20))+new LingoGlobal(0.4999)).integer,((pos.locV.float/new LingoGlobal(20))+new LingoGlobal(0.4999)).integer);

}
public dynamic givemiddleoftile(dynamic pos) {
return _global.point(((pos.locH*Integer { Value = 20 })-Integer { Value = 10 }),((pos.locV*Integer { Value = 20 })-Integer { Value = 10 }));

}
public dynamic restrict(dynamic val,dynamic low,dynamic high) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic restrictwithflip(dynamic val,dynamic low,dynamic high) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic afamvlvledit(dynamic pos,dynamic layer) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic solidafamv(dynamic pos,dynamic layer) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic drawgraph() {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic depthpnt(dynamic pnt,dynamic dpt) {
dpt = (dpt*-new LingoGlobal(0.025));
pnt = (pnt-_global.point((Integer { Value = 1400 }/Integer { Value = 2 }),(Integer { Value = 800 }/Integer { Value = 3 })));
dpt = ((Integer { Value = 10 }-dpt)*new LingoGlobal(0.1));
pnt = (pnt/dpt);
pnt = (pnt+_global.point((Integer { Value = 1400 }/Integer { Value = 2 }),(Integer { Value = 800 }/Integer { Value = 3 })));
return pnt;

}
public dynamic antidepthpnt(dynamic pnt,dynamic dpt) {
dpt = (dpt*-new LingoGlobal(0.025));
pnt = (pnt-_global.point((Integer { Value = 1400 }/Integer { Value = 2 }),(Integer { Value = 800 }/Integer { Value = 3 })));
dpt = ((Integer { Value = 10 }-dpt)*new LingoGlobal(0.1));
pnt = (pnt*dpt);
pnt = (pnt+_global.point((Integer { Value = 1400 }/Integer { Value = 2 }),(Integer { Value = 800 }/Integer { Value = 3 })));
return pnt;

}
public dynamic seedfortile(dynamic tile,dynamic effectSeed) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic copypixelstoeffectcolor(dynamic gdLayer,dynamic lr,dynamic rct,dynamic getMember,dynamic gtRect,dynamic zbleed,dynamic blnd) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic recolor(dynamic img,dynamic palOffset) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic txttoimg(dynamic txt) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic imgtotxt(dynamic img) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic givedpfromlr(dynamic lr) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic makesilhouttefromimg(dynamic img,dynamic inverted) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic blurimage(dynamic img,dynamic blurBlend) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic bluronblack(dynamic img,dynamic blurBlend) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic rotatetoquad(dynamic rct,dynamic deg) {
dynamic dir = null;
dynamic midPnt = null;
dynamic topPnt = null;
dynamic bottomPnt = null;
dynamic crossDir = null;
dynamic pnt1 = null;
dynamic pnt2 = null;
dynamic pnt3 = null;
dynamic pnt4 = null;
dir = _movieScript.degToVec(deg.float);
midPnt = _global.point(((rct.left+rct.right)*new LingoGlobal(0.5)),((rct.top+rct.bottom)*new LingoGlobal(0.5)));
topPnt = (midPnt+((dir*rct.height)*new LingoGlobal(0.5)));
bottomPnt = (midPnt-((dir*rct.height)*new LingoGlobal(0.5)));
crossDir = _movieScript.giveDirFor90degrToLine(-dir,dir);
pnt1 = (topPnt+((crossDir*rct.width)*new LingoGlobal(0.5)));
pnt2 = (topPnt-((crossDir*rct.width)*new LingoGlobal(0.5)));
pnt3 = (bottomPnt-((crossDir*rct.width)*new LingoGlobal(0.5)));
pnt4 = (bottomPnt+((crossDir*rct.width)*new LingoGlobal(0.5)));
return new LingoList(new dynamic[] { pnt1,pnt2,pnt3,pnt4 });

}
public dynamic flipquadh(dynamic qd) {
return new LingoList(new dynamic[] { qd[Integer { Value = 2 }],qd[Integer { Value = 1 }],qd[Integer { Value = 4 }],qd[Integer { Value = 3 }] });

}
public dynamic inversekinematic(dynamic va,dynamic vc,dynamic A,dynamic B,dynamic flip) {
dynamic R = null;
dynamic alph = null;
R = _movieScript.diag(va,vc);
alph = (acos(restrict(((((R*R)+(A*A))-(B*B))/((new LingoGlobal(2)*R)*A)),new LingoGlobal(0.1),new LingoGlobal(0.99)))*((flip*new LingoGlobal(180))/LingoGlobal.PI));
return ((va+_movieScript.degToVec((_movieScript.lookAtPoint(va,vc)+alph)))*A);

}
public dynamic acos(dynamic a) {
return (Integer { Value = 2 }*_global.atan((LingoGlobal.sqrt((Integer { Value = 1 }-(a*a)))/(Integer { Value = 1 }+a))));

}
public dynamic depthchangeimage(dynamic img,dynamic dp) {
_global.new((_global.Img==_global.image(img.rect.width,img.rect.height,dp)));
_global.new(_global.Img.copyPixels(img,img.rect,img.rect));
return _global.new(_global.Img);

}
public dynamic pasteshortcuthole(dynamic mem,dynamic pnt,dynamic dp,dynamic cl) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic resizelevel(dynamic sze,dynamic addTilesLeft,dynamic addTilesTop) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic resetgenveditorprops() {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic resetpropeditorprops() {
throw new System.NotImplementedException("Compilation failed");
}
}
}
