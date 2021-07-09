using System;
using Drizzle.Lingo.Runtime;
namespace Drizzle.Ported {
//
// Movie script: fiffigt
//
public sealed partial class Movie {
public dynamic diag(dynamic point1,dynamic point2) {
dynamic RectHeight = null;
dynamic RectWidth = null;
dynamic diagonal = null;
RectHeight = LingoGlobal.abs((point1.locV-point2.locV));
RectWidth = LingoGlobal.abs((point1.locH-point2.locH));
diagonal = LingoGlobal.sqrt(((RectHeight*RectHeight)+(RectWidth*RectWidth)));
return diagonal;

}
public dynamic diagwi(dynamic point1,dynamic point2,dynamic dig) {
dynamic RectHeight = null;
dynamic RectWidth = null;
RectHeight = LingoGlobal.abs((point1.locV-point2.locV));
RectWidth = LingoGlobal.abs((point1.locH-point2.locH));
return (((RectHeight*RectHeight)+(RectWidth*RectWidth))<(dig*dig));

}
public dynamic diagnosqrt(dynamic point1,dynamic point2) {
dynamic RectHeight = null;
dynamic RectWidth = null;
dynamic diagonal = null;
RectHeight = LingoGlobal.abs((point1.locV-point2.locV));
RectWidth = LingoGlobal.abs((point1.locH-point2.locH));
diagonal = ((RectHeight*RectHeight)+(RectWidth*RectWidth));
return diagonal;

}
public dynamic vertfliprect(dynamic rct) {
return new LingoList(new dynamic[] { _global.point(rct.right,rct.top),_global.point(rct.left,rct.top),_global.point(rct.left,rct.bottom),_global.point(rct.right,rct.bottom) });

}
public dynamic movetopoint(dynamic pointA,dynamic pointB,dynamic theMovement) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic returnrelativepoint(dynamic p1,dynamic p2) {
_global.new((_global.X==(-Integer { Value = 1 }*(p1.locH-p2.locH))));
_global.new((_global.Y==(p1.locV-p2.locV)));
return _global.point(_global.new(_global.X),_global.new(_global.Y));

}
public dynamic returnabsolutepoint(dynamic p1,dynamic p2) {
dynamic realX = null;
dynamic realY = null;
realX = (p1.locH+p2.locH);
realY = (p1.locV-p2.locV);
return _global.point(realX,realY);

}
public dynamic lerp(dynamic A,dynamic B,dynamic val) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic givecrosspoint(dynamic line1PntA,dynamic line1PntB,dynamic line2PntA,dynamic line2PntB) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic givehitsurf(dynamic obstRect,dynamic movLine) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic lookatpoint(dynamic pos,dynamic lookAtpoint) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic degtovec(dynamic deg) {
dynamic rad = null;
deg = (deg+Integer { Value = 90 });
deg = -deg;
rad = (((deg/new LingoGlobal(360)).float*LingoGlobal.PI)*Integer { Value = 2 });
return _global.point(-_global.cos(rad),_global.sin(rad));

}
public dynamic closestpointonline(dynamic pnt,dynamic A,dynamic B) {
return giveCrossPoint(pnt,(pnt+giveDirFor90degrToLine(A,B)),A,B);

}
public dynamic givedirfor90degrtoline(dynamic pnt1,dynamic pnt2) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic lnpntdist(dynamic pnt,dynamic lineA,dynamic lineB) {
return diag(pnt,giveCrossPoint(pnt,(pnt+giveDirFor90degrToLine(lineA,lineB)),lineA,lineB));

}
public dynamic givecirclecolltime(dynamic pos1,dynamic r1,dynamic vel1,dynamic pos2,dynamic r2,dynamic vel2) {
dynamic x1 = null;
dynamic y1 = null;
dynamic x2 = null;
dynamic y2 = null;
dynamic vx1 = null;
dynamic vy1 = null;
dynamic vx2 = null;
dynamic vy2 = null;
dynamic A = null;
dynamic B = null;
dynamic C = null;
dynamic D = null;
dynamic E = null;
dynamic T = null;
x1 = pos1.locH;
y1 = pos1.locV;
x2 = pos2.locH;
y2 = pos2.locV;
vx1 = vel1.locH;
vy1 = vel1.locV;
vx2 = vel2.locH;
vy2 = vel2.locV;
A = ((((-x1*vx1)-(((((((y1*vy1)+vx1)*x2)+vy1)*y2)+x1)*vx2))-(((x2*vx2)+y1)*vy2))-(y2*vy2));
B = ((((-x1*vx1)-(((((((y1*vy1)+vx1)*x2)+vy1)*y2)+x1)*vx2))-(((x2*vx2)+y1)*vy2))-(y2*vy2));
C = (((_global.power(vx1,Integer { Value = 2 })+_global.power(vy1,Integer { Value = 2 }))-(((Integer { Value = 2 }*vx1)*vx2)+_global.power(vx2,Integer { Value = 2 })))-(((Integer { Value = 2 }*vy1)*vy2)+_global.power(vy2,Integer { Value = 2 })));
D = ((((((_global.power(x1,Integer { Value = 2 })+_global.power(y1,Integer { Value = 2 }))-_global.power(r1,Integer { Value = 2 }))-(((Integer { Value = 2 }*x1)*x2)+_global.power(x2,Integer { Value = 2 })))-(((Integer { Value = 2 }*y1)*y2)+_global.power(y2,Integer { Value = 2 })))-((Integer { Value = 2 }*r1)*r2))-_global.power(r2,Integer { Value = 2 }));
E = (((_global.power(vx1,Integer { Value = 2 })+_global.power(vy1,Integer { Value = 2 }))-(((Integer { Value = 2 }*vx1)*vx2)+_global.power(vx2,Integer { Value = 2 })))-(((Integer { Value = 2 }*vy1)*vy2)+_global.power(vy2,Integer { Value = 2 })));
T = (((new LingoGlobal(2)*A)-LingoGlobal.sqrt((_global.power((-new LingoGlobal(2)*B),Integer { Value = 2 })-((new LingoGlobal(4)*C)*D))))/(new LingoGlobal(2)*E));
return T;

}
public dynamic lnpntdistnonabs(dynamic pnt,dynamic lnPnt1,dynamic lnPnt2) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic closestpntinrect(dynamic rct,dynamic pnt) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic anglebetweenlines(dynamic pnt1,dynamic pnt2,dynamic pnt3,dynamic pnt4) {
return (lookAtPoint(pnt1,pnt2)-lookAtPoint(pnt3,pnt4));

}
public dynamic compareangles(dynamic origo,dynamic pnt1,dynamic pnt2) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic rotatepntfromorigo(dynamic pnt,dynamic org,dynamic rotat) {
dynamic realDir = null;
dynamic diag = null;
dynamic vec = null;
dynamic rotatedPnt = null;
realDir = lookAtPoint(org,pnt);
diag = diag(org,pnt);
_global.new((_global.Dir==(realDir-rotat)));
vec = degToVec(_global.new(_global.Dir));
rotatedPnt = (org+(vec*diag));
return rotatedPnt;

}
public dynamic customadd(dynamic L,dynamic val) {
L.add(val);
return L;

}
public dynamic customsort(dynamic L) {
L.sort();
return L;

}
public dynamic insideline(dynamic pnt,dynamic A,dynamic B,dynamic rad) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic newmakelevel(dynamic lvlName) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic lerpvector(dynamic A,dynamic B,dynamic l) {
return _global.point(lerp(A.locH,B.locH,l),lerp(A.locV,B.locV,l));

}
public dynamic seedoftile(dynamic tile) {
throw new System.NotImplementedException("Compilation failed");
}
public dynamic bezier(dynamic A,dynamic cA,dynamic B,dynamic cB,dynamic f) {
dynamic middleControl = null;
middleControl = LerpVector(cA,cB,f);
cA = LerpVector(A,cA,f);
cB = LerpVector(cB,B,f);
cA = LerpVector(cA,middleControl,f);
cB = LerpVector(middleControl,cB,f);
return LerpVector(cA,cB,f);

}
}
}
