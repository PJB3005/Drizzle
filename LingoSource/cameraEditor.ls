global gLOprops, gCameraProps, fac, SHOWQUADS

on exitFrame me
  
  if gLOprops.size.locH > gLOprops.size.locV then
    fac = 1024.0/gLOprops.size.locH
    --  tilefac = gLOprops.size.locH/20
  else
    fac = 768.0/gLOprops.size.locV
    --tilefac = gLOprops.size.locV/20
  end if
  if(fac > 16) then fac = 16
  
  rct = rect(1024/2, 768/2, 1024/2, 768/2) + rect(-gLOprops.size.locH*0.5*fac, -gLOprops.size.locV*0.5*fac, gLOprops.size.locH*0.5*fac, gLOprops.size.locV*0.5*fac)
  
  sprite(90).rect = rct + rect(gLOprops.extraTiles[1]*fac, gLOprops.extraTiles[2]*fac, -gLOprops.extraTiles[3]*fac, -gLOprops.extraTiles[4]*fac)
  
  repeat with q = 2 to 8 then
    sprite(q).rect = rct
  end repeat
  
  sprite(13).rect = rct
  
  if (checkKey("n"))and( gCameraProps.cameras.count < 20) then
    gCameraProps.cameras.add(point(gLOprops.size.locH*10, gLOprops.size.locV*10))
    gCameraProps.quads.add([[0,0],[0,0],[0,0],[0,0]])
    gCameraProps.selectedCamera = gCameraProps.cameras.count
  end if
  
  if(gCameraProps.selectedCamera > 0) then
    mouseOverCamera = gCameraProps.selectedCamera
  else
    mouseOverCamera = 0
    smallstDist = 10000
    repeat with q = 1 to gCameraProps.cameras.count then
      pos = point(1024/2, 768/2) + point(-gLOprops.size.locH*0.5*fac, -gLOprops.size.locV*0.5*fac) + ((gCameraProps.cameras[q]/20)*fac) + point(35, 20)*fac
      if (diag(pos,_mouse.mouseLoc) < smallstDist) then
        mouseOverCamera = q
        smallstDist = diag(pos,_mouse.mouseLoc)
      end if
    end repeat
  end if
  
  if(mouseOverCamera > 0)then
    pos = point(1024/2, 768/2) + point(-gLOprops.size.locH*0.5*fac, -gLOprops.size.locV*0.5*fac) + (gCameraProps.cameras[mouseOverCamera]/20)*fac
    smallstDist = 10000
    closestCorner = 0
    repeat with q = 1 to 4 then
      if(diag(_mouse.mouseLoc, pos + [point(0,0), point(70,0)*fac, point(70, 40)*fac, point(0, 40)*fac][q]) < smallstDist) then
        smallstDist = diag(_mouse.mouseLoc, pos + [point(0,0), point(70,0)*fac, point(70, 40)*fac, point(0, 40)*fac][q])
        closestCorner = q
      end if
    end repeat
    
    if(closestCorner > 0) then
      cornerPos = pos + [point(0,0), point(70,0)*fac, point(70, 40)*fac, point(0, 40)*fac][closestCorner]
      
      linePos = pos+point(35, 20)*fac
      
      if(SHOWQUADS > 0) then SHOWQUADS = SHOWQUADS - 1
      
      if(_key.keyPressed("J"))then
        gCameraProps.quads[mouseOverCamera][closestCorner][1] = gCameraProps.quads[mouseOverCamera][closestCorner][1] - 2
        linePos = cornerPos
        SHOWQUADS = 20
      else if (_key.keyPressed("L"))then
        gCameraProps.quads[mouseOverCamera][closestCorner][1] = gCameraProps.quads[mouseOverCamera][closestCorner][1] + 2
        linePos = cornerPos
        SHOWQUADS = 20
      end if
      if (_key.keyPressed("I")) then
        gCameraProps.quads[mouseOverCamera][closestCorner][2] = gCameraProps.quads[mouseOverCamera][closestCorner][2] + (1.0/20.0)
        if ( gCameraProps.quads[mouseOverCamera][closestCorner][2] > 1)then 
          gCameraProps.quads[mouseOverCamera][closestCorner][2] = 1
        end if
        SHOWQUADS = 20
      else if (_key.keyPressed("K")) then
        gCameraProps.quads[mouseOverCamera][closestCorner][2] = gCameraProps.quads[mouseOverCamera][closestCorner][2] - (1.0/20.0)
        if ( gCameraProps.quads[mouseOverCamera][closestCorner][2] < 0)then 
          gCameraProps.quads[mouseOverCamera][closestCorner][2] = 0
        end if
        SHOWQUADS = 20
      end if
      
      
      cornerPos = cornerPos + DegToVec(gCameraProps.quads[mouseOverCamera][closestCorner][1])*4*fac*gCameraProps.quads[mouseOverCamera][closestCorner][2]
      
      
      sprite(89).rect = rect(linePos, cornerPos)
      sprite(89).member.lineDirection = ((linePos.locH > cornerPos.locH)or(linePos.locV > cornerPos.locV))and((linePos.locH < cornerPos.locH)or(linePos.locV < cornerPos.locV))
    end if
  end if
  
  if (gCameraProps.selectedCamera <> 0)then
    gCameraProps.cameras[gCameraProps.selectedCamera] = (_mouse.mouseloc/point(1024.0, 768.0))*point(gLOprops.size.locH*20, gLOprops.size.locV*20) - point(35.0*20, 20.0*20)--map(_mouse.mouseLoc, rct, rect(0,0,gLOprops.size.locH*20, gLOprops.size.locV*20))--(_mouse.mouseLoc*16.0/fac)-point(rct.left/fac, rct.top/fac)
    -- gCameraProps.cameras[gCameraProps.selectedCamera].loch = restrict(gCameraProps.cameras[gCameraProps.selectedCamera].loch, 0, gLOprops.size.loch*20)
    -- gCameraProps.cameras[gCameraProps.selectedCamera].locv = restrict(gCameraProps.cameras[gCameraProps.selectedCamera].locv, 0, gLOprops.size.locv*20)
    
    if (checkKey("d"))and( gCameraProps.cameras.count > 1) then
      gCameraProps.cameras.Deleteat(gCameraProps.selectedCamera)
      gCameraProps.quads.Deleteat(gCameraProps.selectedCamera)
      gCameraProps.selectedCamera = 0
      
      -- else
      -- pos = point(1024/2, 768/2) + point(-gLOprops.size.locH*0.5*fac, -gLOprops.size.locV*0.5*fac) + (gCameraProps.cameras[gCameraProps.selectedCamera]/20)*fac
      -- sprite(23+gCameraProps.selectedCamera).rect =  rect(pos, pos)+rect(-35, -20, 35, 20)*fac
    end if
    
    if (checkKey("p")) then
      gCameraProps.selectedCamera = 0
    end if
    
  else if (checkKey("e"))and(mouseOverCamera>0) then
    gCameraProps.selectedCamera =  mouseOverCamera
  end if
  
  me.drawAll()
  
  script("levelOverview").goToEditor()
  
  go the frame
end

on drawAll me
  repeat with q = 1 to gCameraProps.cameras.count then
    pos = point(1024/2, 768/2) + point(-gLOprops.size.locH*0.5*fac, -gLOprops.size.locV*0.5*fac) + (gCameraProps.cameras[q]/20)*fac
    sprite(23+q).rect =  rect(pos, pos)+rect(0, 0, 70, 40)*fac
    sprite(44+q).rect =  rect(pos, pos)+(rect(0, 0, 70, 40)+rect(9.3, 0.8, -9.3, -0.8))*fac
    
    QD = [pos, pos+point(70,0)*fac, pos+point(70, 40)*fac, pos+point(0, 40)*fac]
    repeat with c = 1 to 4 then
      QD[c] = QD[c] + DegToVec(gCameraProps.quads[q][c][1])*4*fac*gCameraProps.quads[q][c][2]
    end repeat
    
    sprite(67+q).quad = QD
    sprite(67+q).blend = 15 + (SHOWQUADS/20.0) * 40
  end repeat
  repeat with q = gCameraProps.cameras.count+1 to 10 then
    sprite(23+q).rect =  rect(-100, -100, -100, -100)
    sprite(44+q).rect =  rect(-100, -100, -100, -100)
    sprite(67+q).rect =  rect(-100, -100, -100, -100)
  end repeat
end


on checkKey(key)
  rtrn = 0
  gCameraProps.keys[symbol(key)] = _key.keyPressed(key)
  if (gCameraProps.keys[symbol(key)])and(gCameraProps.lastKeys[symbol(key)]=0) then
    rtrn = 1
  end if
  gCameraProps.lastKeys[symbol(key)] = gCameraProps.keys[symbol(key)]
  return rtrn
end