global gTEprops, gLEProps, gTiles, gEEProps, gEffects, gLightEProps, geverysecond, firstFrame, glgtimgQuad, gDirectionKeys


on exitFrame me
  --  if checkKey("N") then
  --    gLightEProps.lightObjects.add([#ps:point(1040/2, 800/2), #sz:point(50, 70), #rt:0, #clr:0])
  --    gLightEProps.edObj = gLightEProps.lightObjects.count
  --  end if
  
  repeat with q = 1 to 4 then
    if (_key.keyPressed([86, 91, 88, 84][q]))and(gDirectionKeys[q] = 0) and _movie.window.sizeState <> #minimized then
      gLEProps.camPos = gLEProps.camPos + [point(-1, 0), point(0,-1), point(1,0), point(0,1)][q] * (1 + 9 * _key.keyPressed(83) + 34 * _key.keyPressed(85))
      if not _key.keyPressed(92) then
        if gLEProps.camPos.loch < -26 then
          gLEProps.camPos.loch = -26
        end if
        if gLEProps.camPos.locv < -18 then
          gLEProps.camPos.locv = -18
        end if  
        if gLEProps.camPos.loch > gLEprops.matrix.count-56 then
          gLEProps.camPos.loch = gLEprops.matrix.count-56
        end if
        if gLEProps.camPos.locv > gLEprops.matrix[1].count-37 then
          gLEProps.camPos.locv = gLEprops.matrix[1].count-37
        end if
      end if
    end if
    gDirectionKeys[q] = _key.keyPressed([86, 91, 88, 84][q])
  end repeat
  
  
  
  if me.checkKey("Z") then
    gLightEProps.col = (1-gLightEProps.col)
  end if
  if _mouse.rightmouseDown and _movie.window.sizeState <> #minimized then
    gLightEProps.rot = lookAtPoint(gLightEProps.pos, _mouse.mouseLoc)
  else
    gLightEProps.pos = _mouse.mouseLoc
  end if
  
  if _key.keyPressed("C") and _movie.window.sizeState <> #minimized then
    glgtimgQuad[1] = _mouse.mouseLoc + gLEProps.camPos*20
  else if _key.keyPressed("V") and _movie.window.sizeState <> #minimized then
    glgtimgQuad[2] = _mouse.mouseLoc + gLEProps.camPos*20
  else if _key.keyPressed("B") and _movie.window.sizeState <> #minimized then
    glgtimgQuad[3] = _mouse.mouseLoc + gLEProps.camPos*20
  else if _key.keyPressed("N") and _movie.window.sizeState <> #minimized then
    glgtimgQuad[4] = _mouse.mouseLoc + gLEProps.camPos*20
  end if
  
  if me.checkKey("M") then
    dupl = image(member("lightImage").image.width, member("lightImage").image.height, 1)--member("lightImage").image.duplicate()
    dupl.copypixels(  member("lightImage").image, dupl.rect, dupl.rect)
    --  put glgtimgQuad+[point(108,108),point(108,108),point(108,108),point(108,108)]
    member("lightImage").image.copypixels(dupl, glgtimgQuad, dupl.rect)
    glgtimgQuad = [point(0,0), point(member("lightImage").image.width,0), point(member("lightImage").image.width,member("lightImage").image.height), point(0,member("lightImage").image.height)]
    
  end if
  
  if _system.milliseconds -  gLightEProps.lastTm > 10 then
    if _key.keyPressed("W") and _movie.window.sizeState <> #minimized then
      gLightEProps.sz.locV = gLightEProps.sz.locV + 1
    else if _key.keyPressed("S") and _movie.window.sizeState <> #minimized then
      gLightEProps.sz.locV = gLightEProps.sz.locV - 1
    end if
    
    if _key.keyPressed("D") and _movie.window.sizeState <> #minimized then
      gLightEProps.sz.locH = gLightEProps.sz.locH + 1
    else if _key.keyPressed("A") and _movie.window.sizeState <> #minimized then
      gLightEProps.sz.locH = gLightEProps.sz.locH - 1
    end if
    
    if _key.keyPressed("Q") and _movie.window.sizeState <> #minimized then
      gLightEProps.rot = gLightEProps.rot - 1
    else if _key.keyPressed("E") and _movie.window.sizeState <> #minimized then
      gLightEProps.rot = gLightEProps.rot + 1
    end if
    
    if _key.keyPressed("J") and _movie.window.sizeState <> #minimized then
      gLightEProps.lightAngle = restrict( gLightEProps.lightAngle -1, 90, 180)
    else if _key.keyPressed("L") and _movie.window.sizeState <> #minimized then
      gLightEProps.lightAngle = restrict( gLightEProps.lightAngle +1, 90, 180)
    end if
    
    if geverysecond then
      if _key.keyPressed("I") and _movie.window.sizeState <> #minimized then
        gLightEProps.flatness = restrict( gLightEProps.flatness - 1, 1, 10)
      else if _key.keyPressed("K") and _movie.window.sizeState <> #minimized then
        gLightEProps.flatness = restrict( gLightEProps.flatness + 1, 1, 10)
      end if
      geverysecond = 0
    else 
      geverysecond = 1
    end if
    
    gLightEProps.lastTm = _system.milliseconds
  end if
  
  
  l = ["pxl", "bigCircle", "leaves", "oilyLight", "directionalLight", "blobLight1", "blobLight2", "wormsLight", "crackLight", "squareishLight", "holeLight", "roundedRectLight"]
  curr = 1
  repeat with s = 1 to l.count then
    if l[s]=gLightEProps.paintShape then
      curr = s
      exit repeat
    end if 
  end repeat
  
  mv = 0
  if me.checkKey("r") then
    mv = - 1
  else if me.checkKey("f") then
    mv = 1
  end if
  
  if mv <> 0 then
    curr = restrict(curr + mv, 1, l.count)
    sprite(11).member = member(l[curr])
    sprite(12).member = member(l[curr])
    gLightEProps.paintShape = l[curr]
  end if
  
  
  --    
  --  end if
  --  
  --  member("lightImage").image.copypixels(member("pxl").image, rect(0,0,1040,800), rect(0,0,1,1))
  --  
  --  repeat with obj in gLightEProps.lightObjects then
  dir1 = degToVec(gLightEProps.rot)
  dir2 = degToVec(gLightEProps.rot+90)
  
  angleAdd = degToVec(gLightEProps.lightAngle)*(gLightEProps.flatNess*10)--*2.8*(gLightEProps.flatNess+1)*10
  
  dspPos = point((1024/2)-(member("lightImage").image.width/2), (768/2)-(member("lightImage").image.height/2))
  
  dspPos = dspPos - point(150, 150)
  
  q = [gLightEProps.pos, gLightEProps.pos, gLightEProps.pos, gLightEProps.pos] + [gLEProps.camPos*20, gLEProps.camPos*20,gLEProps.camPos*20, gLEProps.camPos*20]
  q = q + [-dir2*gLightEProps.sz.locH - dir1*gLightEProps.sz.locV, dir2*gLightEProps.sz.locH - dir1*gLightEProps.sz.locV, dir2*gLightEProps.sz.locH + dir1*gLightEProps.sz.locV, -dir2*gLightEProps.sz.locH + dir1*gLightEProps.sz.locV]
  --    
  --    member("lightImage").image.copypixels(member("pxl").image, q, rect(0,0,1,1), {#color:obj.clr})
  --  end repeat
  
  gLightEProps.keys.m1 = _mouse.mouseDown and _movie.window.sizeState <> #minimized
  if (gLightEProps.keys.m1)and(firstFrame<>1) then
    member("lightImage").image.copypixels(member(gLightEProps.paintShape).image, q - [dspPos, dspPos, dspPos, dspPos], member(gLightEProps.paintShape).image.rect, {#color:gLightEProps.col*255, #ink:36})
  end if
  if gLightEProps.keys.m1 = 0 then
    firstFrame = 0
  end if
  gLightEProps.lastKeys.m1 = gLightEProps.keys.m1
  
  
  
  sprite(11).quad = q - [gLEProps.camPos*20, gLEProps.camPos*20,gLEProps.camPos*20, gLEProps.camPos*20]
  sprite(12).quad = q - [gLEProps.camPos*20, gLEProps.camPos*20,gLEProps.camPos*20, gLEProps.camPos*20]
  sprite(11).color = color((1-gLightEProps.col)*255, (1-gLightEProps.col)*255, (1-gLightEProps.col)*255)
  
  sprite(10).quad = glgtimgQuad - [gLEProps.camPos*20, gLEProps.camPos*20,gLEProps.camPos*20, gLEProps.camPos*20] + [dspPos, dspPos, dspPos, dspPos]
  
  sprite(15).rect = rect(point(850, 700), point(850, 700)) + rect(-50, -50, 50, 50)
  
  rad = gLightEProps.flatNess*10--*0.1*50
  
  sprite(16).rect = rect(point(850, 700), point(850, 700)) + rect(-rad, -rad, rad, rad)
  sprite(17).loc = point(850, 700) - degToVec(gLightEProps.lightAngle)*rad
  
  
  sprite(6).loc = point(1024/2, 768/2) - point(150, 150) + (angleAdd*2) - gLEProps.camPos*20
  sprite(9).loc = point(1024/2, 768/2) - point(150, 150) - gLEProps.camPos*20
  
  sprite(5).loc = point(1024/2, 768/2) - gLEProps.camPos*20 
  sprite(8).loc = point(1024/2, 768/2) - gLEProps.camPos*20
  
  -- sprite(18).rect = 
  
  script("levelOverview").goToEditor()
  go the frame
end





on checkKey me, key
  rtrn = 0
  gLightEProps.keys[symbol(key)] = _key.keyPressed(key) and _movie.window.sizeState <> #minimized
  if (gLightEProps.keys[symbol(key)])and(gLightEProps.lastKeys[symbol(key)]=0) then
    rtrn = 1
  end if
  gLightEProps.lastKeys[symbol(key)] = gLightEProps.keys[symbol(key)]
  return rtrn
end




























