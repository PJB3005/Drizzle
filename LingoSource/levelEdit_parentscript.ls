global gLEProps, gLOprops

property p

on newUpdate me
  
  p.lastWorkPos = p.workPos
  p.lastInput = p.input
  p.Input = [point(0, 0), 0, 0]
  currTool = gLEProps.toolMatrix[p.toolPos.locV][p.toolPos.locH]--EXCEPTION!!!!
  
  case p.playerNum of
    1:
      p.Input[1].locH =  p.Input[1].locH - (_key.keyPressed(123) and _movie.window.sizeState <> #minimized)
      p.Input[1].locH =  p.Input[1].locH + (_key.keyPressed(124) and _movie.window.sizeState <> #minimized)
      --if p.Input[1].locH = 0 then
      p.Input[1].locV =  p.Input[1].locV - (_key.keyPressed(126) and _movie.window.sizeState <> #minimized)
      p.Input[1].locV =  p.Input[1].locV + (_key.keyPressed(125) and _movie.window.sizeState <> #minimized)
      --end if
      p.Input[2] = _key.keyPressed("K") and _movie.window.sizeState <> #minimized
      p.Input[3] = _key.keyPressed("L") and _movie.window.sizeState <> #minimized
      
      msTile = (_mouse.mouseLoc/point(16.0, 16.0))+point(0.4999, 0.4999)
      msTile = point(msTile.loch.integer, msTile.locV.integer)+point(-11, -1) + gLEprops.camPos
      if msTile.inside(rect(1,1,gLOprops.size.loch+1,gLOprops.size.locv+1)) then
        p.workPos = msTile
        p.Input[3] = (p.Input[3])or(_mouse.mouseDown)
      end if
      
    2:
      msTile = point(-100, -100)
      p.Input[1].locH =  p.Input[1].locH - (_key.keyPressed("S") and _movie.window.sizeState <> #minimized)
      p.Input[1].locH =  p.Input[1].locH + (_key.keyPressed("F") and _movie.window.sizeState <> #minimized)
      p.Input[1].locV =  p.Input[1].locV - (_key.keyPressed("E") and _movie.window.sizeState <> #minimized)
      p.Input[1].locV =  p.Input[1].locV + (_key.keyPressed("D") and _movie.window.sizeState <> #minimized)
      p.Input[2] = _key.keyPressed("Q") and _movie.window.sizeState <> #minimized
      p.Input[3] = _key.keyPressed("W") and _movie.window.sizeState <> #minimized
  end case
  
  
  mv = point(0,0)
  if (p.lastInput[1].locH <> p.Input[1].locH) then
    mv.locH = p.Input[1].locH
  end if
  if (p.lastInput[1].locV <> p.Input[1].locV) then
    mv.locV = p.Input[1].locV
  end if
  if mv <> point(0,0) then
    -- if (p.lastInput[1] <> p.Input[1])and((p.lastInput[1].locH=0)or(p.lastInput[1].locV=0)) then
    if (p.input[2])or(msTile.inside(rect(1,1,gLOprops.size.loch+1,gLOprops.size.locv+1))) then
      
      p.toolPos = p.toolPos + mv
      p.toolPos.locH = restrictWithFlip(p.toolPos.locH, 1, 4)
      p.toolPos.locV = restrictWithFlip(p.toolPos.locV, 1, gLEProps.toolMatrix.count)
      currTool = gLEProps.toolMatrix[p.toolPos.locV][p.toolPos.locH]--EXCEPTION!!!!
      me.updateToolText()
      
    else
      case currTool of 
        "move":
          me.moveCanvas(mv)
        "setMirrorPoint":
          p.mirrorPos = restrict(p.mirrorPos+mv.locH, 1, gLOprops.size.loch)
        otherwise:
          p.workPos = p.workPos + mv
          p.workPos.locH = restrictWithFlip(p.workPos.locH, 1, gLOprops.size.loch)
          p.workPos.locV = restrictWithFlip(p.workPos.locV, 1, gLOprops.size.locv)
      end case
    end if
  end if
  
  if (["squareWall", "squareAir", "copyBack", "flip"].getPos(currTool)>0)then
    affectRect = rect(p.workPos, p.rectPos)
    if affectRect.top > affectRect.bottom then
      sav = affectRect.bottom
      affectRect.bottom = affectRect.top 
      affectRect.top = sav
    end if
    if affectRect.left > affectRect.right then
      sav = affectRect.right
      affectRect.right = affectRect.left 
      affectRect.left = sav
    end if
    
  else
    p.rectFollow = 1
    affectRect = rect(p.workPos.locH,p.workPos.locV,p.workPos.locH,p.workPos.locV)
  end if
  
  if p.rectFollow then
    p.rectPos = p.workPos
  end if
  
  changeTo = "NOCHANGE"
  
  if (p.Input[3]) then
    
    
    
    --  put tool
    case currTool of
      "inverse":
        if (p.lastInput[3]=0) then
          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][1] = 0 then
            changeTo = 1
          else
            changeTo = 0
          end if
        end if
      "paintWall":
        changeTo = 1
      "paintAir":
        changeTo = 0
      "floor":
        changeTo = 6
      "slope":
        if (p.lastInput[3]=0) then
          me.slopeTile(p.workPos)
          if p.mirror then
            wrkPos = me.mirrorRect(rect(p.workPos,p.workPos))
            wrkPos = point(wrkPos.left, wrkPos.top)
            me.slopeTile(wrkPos)
            lvlEditDraw(rect(wrkPos,wrkPos), p.workLayer)
          end if
        end if
        
      "lizardHole":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].getPos(7) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].add(7)
        --          else
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].deleteOne(7)
        --          end if
        --        end if
        me.addRemoveFeature(7)
        
      "playerSpawn":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].getPos(6) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].add(6)
        --          else
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].deleteOne(6)
        --          end if
        --        end if
        me.addRemoveFeature(6)
        
        
      "squareWall", "squareAir":
        if (p.lastInput[3]=0) then
          if p.rectFollow then
            p.rectFollow = 0
          else
            p.rectFollow = 1
            changeTo = (currTool="squareWall")
          end if
        end if
      "copyBack":
        if (p.lastInput[3]=0) then
          if p.rectFollow then
            p.rectFollow = 0
          else
            p.rectFollow = 1
            changeTo = "copyBack"
          end if
        end if
        --      "flipHor", "flipVer":
        --        if (p.lastInput[3]=0) then
        --          if p.rectFollow then
        --            p.rectFollow = 0
        --          else if rectFollow = 0 then
        --            p.rectFollow = 
        --          end if
        --        end if
      "flip":
        if (p.lastInput[3]=0) then
          if p.rectFollow then
            p.rectFollow = 0
          else
            p.rectFollow = 1
            changeTo = "flip"
          end if
        end if
        
      "horbeam":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].getPos(1) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].add(1)
        --          else 
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].deleteOne(1)
        --          end if
        --        end if
        me.addRemoveFeature(1)
      "verBeam":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].getPos(2) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].add(2)
        --          else 
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].deleteOne(2)
        --          end if
        --        end if
        me.addRemoveFeature(2)
      "glass":
        if (p.lastInput[3]=0)or(p.lastWorkPos<>p.workPos) then
          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][1] = 9 then
            changeTo = 0
          else
            changeTo = 9
          end if
        end if
      "shortCutEntrance":
        --        if (p.lastInput[3]=0) then
        --          gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][1] = 0
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].getPos(4) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].add(4)
        --            if gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].getPos(5) > 0 then
        --              gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].deleteOne(5)
        --            end if
        --          else
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].deleteOne(4)
        --          end if
        --        end if
        me.addRemoveFeature(4)
        drawShortCutsImg(affectRect+ rect(-1, -1, 1, 1), 16)
      "shortCut":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].getPos(5) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].add(5)
        --            if gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].getPos(4) > 0 then
        --              gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].deleteOne(4)
        --            end if
        --          else
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][1][2].deleteOne(5)
        --          end if
        --        end if
        me.addRemoveFeature(5)
        drawShortCutsImg(affectRect+ rect(-1, -1, 1, 1), 16)
      "hive":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].getPos(3) = 0 then
        --            if (afaMvLvlEdit(p.workPos+point(0,1), p.workLayer) = 1)and(gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][1]<>1) then
        --              gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].add(3)
        --            end if
        --          else
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].deleteOne(3)
        --          end if
        --        end if
        me.addRemoveFeature(3)
      "workLayer":
        if (p.lastInput[3]=0) then
          --          if p.workLayer = 1 then
          --            p.workLayer = 2
          --          else
          --            p.workLayer = 1
          --          end if
          --        end if
          p.workLayer =  p.workLayer +1
          if  p.workLayer > 3 then
            p.workLayer = 1
          end if
        end if
        me.updateToolText()
      "mirrorToggle":
        if (p.lastInput[3]=0) then
          p.mirror = 1-p.mirror
        end if
      "rock":
        -- if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].getPos(9) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].add(9)
        --          else 
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].deleteOne(9)
        --          end if
        me.addRemoveFeature(9)
        -- end if
      "spear":
        --        if (p.lastInput[3]=0) then
        --          if gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].getPos(10) = 0 then
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].add(10)
        --          else 
        --            gLEProps.matrix[p.workPos.locH][p.workPos.locV][p.workLayer][2].deleteOne(10)
        --          end if
        --        end if
        me.addRemoveFeature(10)
        
      "crack":
        me.addRemoveFeature(11)
      "forbidbats":
        me.addRemoveFeature(12)
      "garbageHole":
        me.addRemoveFeature(13)
      "waterfall":
        me.addRemoveFeature(18)
      "WHAMH":
        me.addRemoveFeature(19)
      "wormGrass":
        me.addRemoveFeature(20)
      "scavengerHole":
        me.addRemoveFeature(21)
    end case
    
    --  sv = 
    
    
    if changeTo <> "NOCHANGE" then
      repeat with q = affectRect.left to affectRect.right then
        repeat with c = affectRect.top to affectRect.bottom then
          case changeTo of 
            "copyBack":
              gLEProps.matrix[q][c][p.workLayer+1-(p.workLayer=3)][1] = gLEProps.matrix[q][c][p.workLayer][1]
              lvlEditDraw(rect(q,c,q,c), p.workLayer+1-(p.workLayer=3))
            "flip":
              gLEProps.matrix[q][c] = [[0, []], [0, []], [0, []]]
            otherwise:
              gLEProps.matrix[q][c][p.workLayer][1] = changeTo
          end case
        end repeat
      end repeat
    end if
    lvlEditDraw(affectRect, p.workLayer)
    drawShortCutsImg(affectRect+ rect(-1, -1, 1, 1), 16)
  end if
  
  if changeTo = "flip" then
    lvlEditDraw(affectRect, 1)
    lvlEditDraw(affectRect, 2)
    lvlEditDraw(affectRect, 3)
  end if
  
  
  
  drwRect = affectRect - rect(gLEprops.camPos, gLEprops.camPos)
  rct1 = rect((drwRect.left-1)*16, (drwRect.top-1)*16, drwRect.right*16, drwRect.bottom*16) + rect(176, 16, 176, 16)
  rct2 = rect((p.toolPos.locH-1)*32, (p.toolPos.locV-1)*32, p.toolPos.locH*32, p.toolPos.locV*32) + rect(16, 16, 16, 16)
  
  if(p.playerNum = 1)then
    if(affectRect.width = 0)and(affectRect.height = 0)then
      member("rulerText").text = "x:" & affectRect.left & " y:" & affectRect.top
    else
      member("rulerText").text = "w:" & (affectRect.width + 1) & " h:" & (affectRect.height + 1)
    end if
    sprite(10).loc = point(rct1.right+10, rct1.bottom-10)
  end if
  
  sprite(p.SprL[1]).rect = rct1
  sprite(p.SprL[2]).rect = rct2
  
  if p.mirror then
    sprite(p.SprL[3]).rect = rect(((p.mirrorPos-gLEprops.camPos.locH)*16)-1, 0, ((p.mirrorPos-gLEprops.camPos.locH)*16)+1, 40*16) + rect(176, 16, 176, 16)--rect(16, 0, 16, 0)
    sprite(p.SprL[3]).member = member("pxl")
    
    affectRect2 = me.mirrorRect(affectRect) 
    drwaffrct = affectRect2- rect(gLEprops.camPos, gLEprops.camPos)
    sprite(p.SprL[4]).rect = rect((drwaffrct.left-1)*16, (drwaffrct.top-1)*16, drwaffrct.right*16, drwaffrct.bottom*16) + rect(176, 16, 176, 16)
    --  if (p.Input[3]) then
    
    if changeTo <> "NOCHANGE" then
      repeat with q = affectRect2.left to affectRect2.right then
        repeat with c = affectRect2.top to affectRect2.bottom then
          if point(q, c).inside(rect(1,1,gLOprops.size.loch+1,gLOprops.size.locv+1)) then
            if changeTo = "copyBack" then
              gLEProps.matrix[q][c][p.workLayer+1-(p.workLayer=3)][1] =  gLEProps.matrix[q][c][p.workLayer][1]
              lvlEditDraw(rect(q,c,q,c), p.workLayer+1-(p.workLayer=3))
            else  if changeTo <> "flip" then
              gLEProps.matrix[q][c][p.workLayer][1] = changeTo
            end if
          end if
        end repeat
      end repeat
      lvlEditDraw(affectRect2, p.workLayer)
      drawShortCutsImg(affectRect2+ rect(-1, -1, 1, 1), 16)
    end if
    
    -- end if
  else
    sprite(p.SprL[3]).rect = rect(-100, -100, -100, -100)
    sprite(p.SprL[4]).rect = rect(-100, -100, -100, -100)
  end if
  
  
  pnt1 = closestPntInRect(rct1, point(rct2.left+rct2.width*0.5, rct2.top+rct2.height*0.5))
  pnt2 = closestPntInRect(rct2, point(rct1.left+rct1.width*0.5, rct1.top+rct1.height*0.5))
  sprite(p.SprL[5]).member = member("line"&(1+(pnt1.locV<pnt2.locV)))
  sprite(p.SprL[5]).rect = rect(pnt1, pnt2+point(0,1))
  
  sprite(p.SprL[6]).loc = point(rct1.right, rct1.bottom)+point(32,32)-point(8,8)
end

on mirrorRect me, rct
  lft = p.mirrorPos - (rct.left - p.mirrorPos)
  rght = p.mirrorPos - (rct.right - p.mirrorPos)
  
  if lft < rght then
    rct = rect(lft, rct.top, rght, rct.bottom)
  else
    rct = rect(rght, rct.top, lft, rct.bottom)
  end if
  return rct+rect(1,0,1,0)
end


on slopeTile me, ps
  nbs = ""
  repeat with dir in [point(-1, 0), point(0, -1), point(1, 0), point(0, 1)] then
    nbs = nbs & afaMvLvlEdit(ps+dir, p.workLayer)
  end repeat
  case nbs of
    "1001":
      rslt = 2
    "0011":
      rslt = 3
    "1100":
      rslt = 4
    "0110":
      rslt = 5
    otherwise:
      rslt = 0
  end case
  if rslt <> 0 then
    gLEProps.matrix[ps.locH][ps.locV][p.workLayer][1] = rslt
  end if
end

on addRemoveFeature me, ft
  ps = p.workPos
  if (p.lastInput[3]=0)or(p.lastWorkPos<>p.workPos) then
    if gLEProps.matrix[ps.locH][ps.locV][p.workLayer][2].getPos(ft) = 0 then
      gLEProps.matrix[ps.locH][ps.locV][p.workLayer][2].add(ft)
    else 
      gLEProps.matrix[ps.locH][ps.locV][p.workLayer][2].deleteOne(ft)
    end if
    if p.mirror then
      ps = me.mirrorRect(rect(p.workPos,p.workPos))
      ps = point(ps.left, ps.top)
      if gLEProps.matrix[ps.locH][ps.locV][p.workLayer][2].getPos(ft) = 0 then
        gLEProps.matrix[ps.locH][ps.locV][p.workLayer][2].add(ft)
      else 
        gLEProps.matrix[ps.locH][ps.locV][p.workLayer][2].deleteOne(ft)
      end if
    end if
    lvlEditDraw(rect(ps,ps), p.workLayer)
    drawShortCutsImg(rect(ps,ps)+ rect(-1, -1, 1, 1), 16)
  end if
end

on new me, playNm
  -- p.Type = "obj"
  p = [:]
  offst = 800
  if playNm = 1 then
    offst = 810
  end if
  p.addProp(#SprL, [6,7,4,5, 8, 9])
  p.SprL = p.SprL + offst
  p.addProp(#playerNum, playNm)
  --  --channel(p.SprL[1]).makeScriptedSprite(member("pxl"), point(-100, -100))
  --  sprite(p.SprL[1]).ink = 36
  --  channel(p.SprL[2]).makeScriptedSprite(member("pxl"), point(-100, -100))
  --  sprite(p.SprL[2]).ink = 36
  --  
  --  channel(p.SprL[3]).makeScriptedSprite(member("pxl"), point(-100, -100))
  --  channel(p.SprL[4]).makeScriptedSprite(member("pxl"), point(-100, -100))
  --  sprite(p.SprL[4]).ink = 36
  
  --  sprite(p.SprL[1]).member = member("square")
  --  sprite(p.SprL[2]).member = member("square")
  --  sprite(p.SprL[4]).member = member("square")
  
  sprite(p.SprL[1]).lineSize = 2
  sprite(p.SprL[2]).lineSize = 2
  sprite(p.SprL[4]).lineSize = 2
  
  
  if playNm=1 then
    cl = color(100, 100, 100)
  else
    cl = color(255, 0, 0)
  end if
  repeat with q = 1 to 5 then
    sprite(p.SprL[q]).color = cl
  end repeat
  
  p.addProp(#input, [point(0,0), 0, 0])
  p.addProp(#lastInput, [point(0,0), 0, 0])
  
  p.addProp(#toolPos, point(1,1))
  p.addProp(#workPos, point(1,1))
  
  p.addProp(#rectPos, point(1,1))
  p.addProp(#rectFollow, 0)
  
  p.addProp(#toolProps, "")
  
  p.addProp(#workLayer, 1)
  
  p.addProp(#lastWorkPos, point(1,1))
  
  p.addProp(#mirror, 0)
  p.addProp(#mirrorPos, gLOprops.size.loch/2)
  
  --member("editor"&string(p.playerNum)&"tool").text = "Inverse"--gLEProps.toolMatrix[p.toolPos.locV][p.toolPos.locH]
  me.updateToolText()
  
  
  gLEProps.levelEditors.add(me)
end








on moveCanvas me, mv
  global gTEprops, gEEprops
  
  if mv.locH < 0 then
    sav = gLEProps.matrix[1]
    gLEProps.matrix.deleteAt(1)
    gLEProps.matrix.add(sav)
    
    sav = gTEprops.tlMatrix[1]
    gTEprops.tlMatrix.deleteAt(1)
    gTEprops.tlMatrix.add(sav)
    
    repeat with eff in gEEprops.effects then
      sav = eff.mtrx[1]
      eff.mtrx.deleteAt(1)
      eff.mtrx.add(sav)
    end repeat
    
  else if mv.locH > 0 then
    sav = gLEProps.matrix[gLOprops.size.loch]
    gLEProps.matrix.deleteAt(gLOprops.size.loch)
    gLEProps.matrix.addAt(1, sav)
    
    sav = gTEprops.tlMatrix[gLOprops.size.loch]
    gTEprops.tlMatrix.deleteAt(gLOprops.size.loch)
    gTEprops.tlMatrix.addAt(1, sav)
    
    repeat with eff in gEEprops.effects then
      sav = eff.mtrx[gLOprops.size.loch]
      eff.mtrx.deleteAt(gLOprops.size.loch)
      eff.mtrx.addAt(1, sav)
    end repeat
    
  end if
  
  if mv.locV < 0 then
    repeat with q = 1 to gLOprops.size.loch then
      sav = gLEProps.matrix[q][1]
      gLEProps.matrix[q].deleteAt(1)
      gLEProps.matrix[q].add(sav)
      
      sav = gTEprops.tlMatrix[q][1]
      gTEprops.tlMatrix[q].deleteAt(1)
      gTEprops.tlMatrix[q].add(sav)
      
      repeat with eff in gEEprops.effects then
        sav = eff.mtrx[q][1]
        eff.mtrx[q].deleteAt(1)
        eff.mtrx[q].add(sav)
      end repeat
    end repeat
    
  else if mv.locV > 0 then
    repeat with q = 1 to gLOprops.size.loch then
      sav = gLEProps.matrix[q][gLOprops.size.locv]
      gLEProps.matrix[q].deleteAt(gLOprops.size.locv)
      gLEProps.matrix[q].addAt(1, sav)
      
      sav = gTEprops.tlMatrix[q][gLOprops.size.locv]
      gTEprops.tlMatrix[q].deleteAt(gLOprops.size.locv)
      gTEprops.tlMatrix[q].addAt(1, sav)
      
      repeat with eff in gEEprops.effects then
        sav = eff.mtrx[q][gLOprops.size.locv]
        eff.mtrx[q].deleteAt(gLOprops.size.locv)
        eff.mtrx[q].addAt(1, sav)
      end repeat
    end repeat
  end if
  
  lvlEditDraw(rect(1,1,gLOprops.size.loch,gLOprops.size.locv), 1)
  lvlEditDraw(rect(1,1,gLOprops.size.loch,gLOprops.size.locv), 2)
  lvlEditDraw(rect(1,1,gLOprops.size.loch,gLOprops.size.locv), 3)
  drawShortCutsImg(rect(1,1,gLOprops.size.loch,gLOprops.size.locv), 16)
end






on updateToolText me
  txt = ""
  case gLEProps.toolMatrix[p.toolPos.locV][p.toolPos.locH] of 
    "copyBack":
      txt = "Copy Backwards"
    "inverse":
      txt = "Inverse"
    "paintWall":
      txt = "Paint Walls"
    "paintAir":
      txt = "Paint Air"
    "slope":
      txt = "Make Slope"
    "floor":
      txt = "Make Floor"
    "squareWall":
      txt = "Rect Wall"
    "squareAir":
      txt = "Rect Air"
    "move":
      txt = "Move Level"
    "horBeam":
      txt = "Horizontal Beam"
    "verBeam":
      txt = "Vertical Beam"
    "glass":
      txt = "Glass Wall"
    "hive":
      txt = "Hive"
    "shortCutEntrance":
      txt = "Short Cut Entrance"
    "shortCut":
      txt = "Short Cut"
    "playerSpawn":
      txt = "Entrance"
    "lizardHole":
      txt = "Dragon Den"
    "rock":
      txt = "Place Rock"
    "spear":
      txt = "Place Spear"
    "mirrorToggle":
      txt = "Toggle Mirror"
    "setMirrorPoint":
      txt = "Move Mirror"
    "workLayer":
      txt = "Work Layer: " & string(p.workLayer)
    "Crack":
      txt = "Crack Terrain"
    "flip":
      txt = "Clear All"
    "forbidbats":
      txt = "Forbid Fly Chains"
    "spawnfly":
      txt = "Spawn Fly"
    "bubble":
      txt = "Bubble"
    "Egg":
      txt = "Dragon Egg"
    "Waterfall":
      txt = "Waterfall"
    "WHAMH":
      txt = "Whack A Mole Hole"
    "wormGrass":
      txt =  "Worm Grass"
    "scavengerHole":
      txt = "Scavenger Hole"
  end case
  member("editor"&string(p.playerNum)&"tool").text = txt
  sprite(p.SprL[6]).member = member("icon"&gLEProps.toolMatrix[p.toolPos.locV][p.toolPos.locH])
end




















