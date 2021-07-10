
on giveGridPos(pos)
  -- pos = pos + point(8, 8)
  return point(((pos.locH.float/20.0)+0.4999).integer, ((pos.locV.float/20.0)+0.4999).integer)
end

on giveMiddleOfTile(pos)
  --pos = pos - point(8, 8)
  return point((pos.locH*20)-10, (pos.locV*20)-10)-- - point(8,8)
end 

on restrict(val, low, high)
  if val<low then
    return low
  else if val > high then
    return high
  else
    return val
  end if
end

on restrictWithFlip(val, low, high)
  if val<low then
    return val + (high-low)+1
  else if val > high then
    return val - (high-low)-1
  else
    return val
  end if
end


on afaMvLvlEdit(pos, layer)
  global gLEProps, gLOprops
  if pos.inside(rect(1,1,gLOprops.size.loch+1,gLOprops.size.locv+1)) then
    --put gMatrix[pos.locH][pos.locV]
    return gLEProps.matrix[pos.locH][pos.locV][layer][1]
  else
    return 1
  end if
end



on solidAfaMv(pos, layer)
  global solidMtrx, gLOprops
  if pos.inside(rect(1,1,gLOprops.size.loch+1,gLOprops.size.locv+1)) then
    --put gMatrix[pos.locH][pos.locV]
    return solidMtrx[pos.locH][pos.locV][layer]
  else
    return 1
  end if
end


on drawGraph()
  member("grafImg").image = image(300, 100, 1)
  repeat with q = 1 to 300 then
    fc = q/300.0
    
    fc = 1.0-fc
    fc = fc*fc
    fc = 1.0-fc
    
    val = sin(fc*PI)
    
    
    
    member("grafImg").image.setPixel(q, 100-val*100, color(0,0,0))
  end repeat
end


on depthPnt(pnt, dpt)
  --return pnt + point(-dpt, -dpt)
    dpt = dpt * -0.025
    pnt = pnt - point(1400/2, 800/3)
    dpt = (10 - dpt)*0.1
    pnt = pnt / dpt
    pnt = pnt + point(1400/2, 800/3)
    return pnt
end

on antiDepthPnt(pnt, dpt)
  --return pnt - point(dpt, dpt)
    dpt = dpt * -0.025
    pnt = pnt - point(1400/2, 800/3)
    dpt = (10 - dpt)*0.1
    pnt = pnt * dpt
    pnt = pnt + point(1400/2, 800/3)
    return pnt
end

on seedForTile(tile, effectSeed)
  global gLEprops
  return effectSeed + tile.locH + tile.locV * gLEprops.matrix.count
end



on copyPixelsToEffectColor(gdLayer, lr, rct, getMember, gtRect, zbleed, blnd)
  
  if(blnd = void) then
    blnd = 1.0
  end if
  
  if (gdLayer <> "C")and(blnd > 0)then
    lr = lr.integer
    if lr < 0 then lr = 0
    else if lr > 29 then lr = 29
    
    gtImg = member(getMember).image
    if (blnd <> 0)and(blnd <> void) then
      dmpImg = gtImg.duplicate()
      dmpImg.copyPixels(member("pxl").image, dmpImg.rect, rect(0,0,1,1), {#blend:100.0*(1.0-blnd), #color:color(255, 255, 255)})
      gtImg = dmpImg
    end if
    
    member("gradient"&gdLayer&string(lr)).image.copyPixels(gtImg, rct, gtRect, {#ink:39})
    
    if(zbleed > 0) then
      if (zbleed < 1)then
        dmpImg = gtImg.duplicate()
        dmpImg.copyPixels(member("pxl").image, dmpImg.rect, rect(0,0,1,1), {#blend:100.0*(1.0-zbleed), #color:color(255, 255, 255)})
        gtImg = dmpImg
      end if
      nxt = lr + 1
      if nxt > 29 then nxt = 29
      member("gradient"&gdLayer&string(nxt)).image.copyPixels(gtImg, rct, gtRect, {#ink:39})
      nxt = lr - 1
      if nxt < 0 then nxt = 0
      member("gradient"&gdLayer&string(nxt)).image.copyPixels(gtImg, rct, gtRect, {#ink:39})
    end if
  end if
end










on recolor(img, palOffset)
  -- member("currentPalette").image = image(member("palette"&string(pal)).image.width, member("palette"&string(pal)).image.height, 16)
  -- member("currentPalette").image.copyPixels(member("palette"&string(pal)).image,  member("currentPalette").image.rect, member("palette"&string(pal)).image.rect)
  
  -- member("currentPalette").image.copyPixels(member("palette"&string(pal2)).image,  member("currentPalette").image.rect, member("palette"&string(pal2)).image.rect, {#blend:blnd})
  
  -- cXtraGEAdjustRGB("currentPalette","",rgbChange[1].integer,rgbChange[2].integer,rgbChange[3].integer)
  
  -- img = mem.image
  col1 = member("currentPalette").image.getPixel(0+palOffset, 0)
  col2 = member("currentPalette").image.getPixel(1+palOffset, 0)
  col3 = member("currentPalette").image.getPixel(2+palOffset, 0)
  col4 = member("currentPalette").image.getPixel(3+palOffset, 0)
  col5 = member("currentPalette").image.getPixel(4+palOffset, 0)
  --put col1
  repeat with c = 1 to img.width then
    repeat with r = 1 to img.height then
      --  put string(img.getPixel(c-1, r-1))
      case string(img.getPixel(c-1, r-1)) of
        "color( 248, 0, 0 )":
          img.setPixel(c-1, r-1, col1)
        "color( 0, 248, 0 )":
          img.setPixel(c-1, r-1, col2)
        "color( 0, 0, 248 )":
          img.setPixel(c-1, r-1, col3)
        "color( 248, 248, 0 )":
          img.setPixel(c-1, r-1, col4)
        "color( 0, 248, 248 )":
          img.setPixel(c-1, r-1, col5)
      end case
      -- col = img.getPixel(c, r)
      -- put col
    end repeat
  end repeat
  
  return img
end



on txtToImg(txt)
  totChars = txt.length
  
  rws = ((totChars/1040.0)+0.5).integer
  img = image(1040, rws, 32)
  
  pos = point(0,1)
  repeat with q = 1 to txt.length then
    pos.locH = pos.locH + 1
    if pos.locH > 1040 then
      pos.locH = 1
      pos.locV = pos.locV + 1
    end if
    --  put txt.char[q..q].charToNum
    img.setPixel(pos.locH-1, pos.locV-1,  color((txt.char[q..q]).charToNum, 0, 0) )
    -- put (txt.char[q..q]).charToNum
  end repeat
  
  return img
end

on imgToTxt(img)
  --pos = point(1,1)
  txt = ""
  repeat with c = 1 to img.rect.height then
    repeat with q = 1 to 1040 then
      col = img.getPixel(q-1, c-1)
      if col = color(255, 255, 255) then
        exit repeat
      else
        --put col.red
        put numToChar(col.red) after txt 
      end if
    end repeat
  end repeat
  
  return txt
end



on giveDpFromLr(lr)
  rtrn = 1
  if lr >= 12 then
    rtrn = 4
  else if lr >= 8 then
    rtrn = 3
  else if lr >= 4 then
    rtrn = 2
  end if
  return rtrn
end 


on makeSilhoutteFromImg(img, inverted)
  
  inv = image(img.width, img.height, 1)
  inv.copyPixels(member("pxl").image, img.rect, rect(0,0,1,1), {#color:255})
  inv.copyPixels(img,img.rect,img.rect, {#ink:36, #color:color(255,255,255)})
  if inverted=0 then
    inv = makeSilhoutteFromImg( inv, 1)
  end if
  return inv
end


on blurImage(img, blurBlend)
  opImg = image(img.width, img.height, 32)
  -- opImg.copyPixels(img, img.rect, img+rect(h,v,h,v), {#blend:(member("blurShape").image.getPixel(h+2,v+2).red/255.0)*100})
  pnt = point(-2,-2)
  -- l = [point(-2,-2), point(-1,-2), point(0,-2), point(1,-2), point(2,-2), point(2,-1), point(2,0), point(2,1), point(2,2), point(1,2), point(0,2),\
  point(-1,2),point(-2,2),point(-2,1),point(-2,0),point(-2,-1),point(-1,-1),point(0,-1),point(1,-1),point(1,0),point(0,0)]
  l = []
  repeat with h = -3 to 3 then
    repeat with v = -3 to 3 then
      l.add([100-(diag(point(h,v), point(0,0))*10)+random(9), point(h,v)])
    end repeat
  end repeat
  l.sort()
  repeat with pnt in l then
    opImg.copyPixels(img, img.rect, img.rect+rect(pnt[2],pnt[2]), {#blend:(member("blurShape").image.getPixel(pnt[2].locH+3,pnt[2].locV+3).red/255.0)*blurBlend})
  end repeat
  -- end repeat
  return opImg
end

on blurOnBlack(img, blurBlend)
  opImg = image(img.width, img.height, 32)
  opImg.copyPixels(member("pxl").image, opImg.rect, rect(0,0,1,1))
  -- opImg.copyPixels(img, img.rect, img+rect(h,v,h,v), {#blend:(member("blurShape").image.getPixel(h+2,v+2).red/255.0)*100})
  pnt = point(-2,-2)
  -- l = [point(-2,-2), point(-1,-2), point(0,-2), point(1,-2), point(2,-2), point(2,-1), point(2,0), point(2,1), point(2,2), point(1,2), point(0,2),\
point(-1,2),point(-2,2),point(-2,1),point(-2,0),point(-2,-1),point(-1,-1),point(0,-1),point(1,-1),point(1,0),point(0,0)]
  l = []
  repeat with h = -3 to 3 then
    repeat with v = -3 to 3 then
      l.add([100-(diag(point(h,v), point(0,0))*10)+random(9), point(h,v)])
    end repeat
  end repeat
  l.sort()
  repeat with pnt in l then
    opImg.copyPixels(img, img.rect, img.rect+rect(pnt[2],pnt[2]), {#ink:36, #blend:(member("blurShape").image.getPixel(pnt[2].locH+3,pnt[2].locV+3).red/255.0)*blurBlend})
  end repeat
  -- end repeat
  return opImg
end



on rotateToQuad(rct, deg)
  dir = degToVec(deg.float)
  
  midPnt = point((rct.left+rct.right)*0.5, (rct.top+rct.bottom)*0.5)
  topPnt = midPnt + (dir*rct.height*0.5)
  bottomPnt = midPnt - (dir*rct.height*0.5)
  
  crossDir = giveDirFor90degrToLine(-dir, dir)
  pnt1 = topPnt + (crossDir*rct.width*0.5)
  pnt2 = topPnt - (crossDir*rct.width*0.5)
  pnt3 = bottomPnt - (crossDir*rct.width*0.5)
  pnt4 = bottomPnt + (crossDir*rct.width*0.5)
  
  return [pnt1, pnt2, pnt3, pnt4]
end



on flipQuadH(qd)
  return [qd[2], qd[1], qd[4], qd[3]]
end

on InverseKinematic(va, vc, A, B, flip)
  R = diag(va, vc)
  
  
  alph = acos(restrict(((R * R) + (A * A) - (B * B)) / (2.0 * R * A), 0.1, 0.99)) * (flip * 180.0 / PI)
  return va + degToVec(lookAtPoint(va, vc) + alph) * A
  
end


on acos(a) 
  return 2*atan( sqrt(1-a*a)/(1+a) )
end

on depthChangeImage(img, dp)
  newImg = image(img.rect.width, img.rect.height, dp)
  newImg.copyPixels(img, img.rect, img.rect)
  return newImg
end




on pasteShortCutHole(mem, pnt, dp, cl)
  global gLEProps, gLOprops, gCameraProps, gCurrentRenderCamera, gRenderCameraTilePos, gRenderCameraPixelPos
  rct = giveMiddleOfTile(pnt) - (gRenderCameraTilePos*20) - gRenderCameraPixelPos
  rct = depthPnt(rct, dp)
  rct = rect(rct,rct)+rect(-10, -10, 10, 10)
  
  idString = ""
  repeat with dr in [point(-1,0), point(0,-1), point(1,0), point(0,1)] then
    if (pnt+dr).inside(rect(1,1,gLOprops.size.loch,gLOprops.size.locv)) then
      if (gLEProps.matrix[pnt.locH+dr.locH][pnt.locV+dr.locV][1][2].getPos(5)>0)or(gLEProps.matrix[pnt.locH+dr.locH][pnt.locV+dr.locV][1][2].getPos(4)>0)then
        idString = idString & "1"
      else
        idString = idString & "0"
      end if
    else
      idString = idString & "0"
    end if
  end repeat
  
  ps = ["0101", "1010", "1111", "1100", "0110", "0011", "1001", "1110", "0111", "1011", "1101", "0000"].getPos(idString)
  
  if cl = "BORDER" then
    clL = []
    clL.add([color(255, 0, 0), point(-1,0)])
    clL.add([color(255, 0, 0), point(0,-1)])
    clL.add([color(255, 0, 0), point(-1,-1)])
    clL.add([color(255, 0, 0), point(-2,0)])
    clL.add([color(255, 0, 0), point(0,-2)])
    clL.add([color(255, 0, 0), point(-2,-2)])
    
    clL.add([color(0, 0, 255), point(1,0)])
    clL.add([color(0, 0, 255), point(0,1)])
    clL.add([color(0, 0, 255), point(1,1)])
    clL.add([color(0, 0, 255), point(2,0)])
    clL.add([color(0, 0, 255), point(0,2)])
    clL.add([color(0, 0, 255), point(2,2)])
  else
    clL = [[cl, point(0,0)]]
  end if
  
  repeat with c in clL then
    member(mem).image.copyPixels(member("shortCutsGraf").image, rct+rect(c[2], c[2]), rect(20*(ps-1),1,20*ps,21), {#ink:36, #color:c[1]})
  end repeat
end




on resizeLevel(sze, addTilesLeft, addTilesTop)
  global gLEprops, gLOProps, gTEprops, gEEprops
  newMatrix = []
  newTEmatrix = []
  
  repeat with q = 1 to sze.locH + addTilesLeft then
    ql = []
    repeat with c = 1 to sze.locV + addTilesTop then
      if (q-addTilesLeft<=gLEprops.matrix.count)and(c-addTilesTop<=gLEprops.matrix[1].count)and(q-addTilesLeft>0)and(c-addTilesTop>0)then
        adder = gLEprops.matrix[q-addTilesLeft][c-addTilesTop]
      else
        adder = [[1, []], [1, []], [1, []]]
      end if
      ql.add(adder)
    end repeat
    newMatrix.add(ql)
  end repeat
  
  repeat with q = 1 to sze.locH + addTilesLeft then
    ql = []
    repeat with c = 1 to sze.locV + addTilesTop then
      if (q+addTilesLeft<=gTEprops.tlMatrix.count)and(c+addTilesTop<=gTEprops.tlMatrix[1].count)and(q-addTilesLeft>0)and(c-addTilesTop>0)then
        adder = gTEprops.tlMatrix[q-addTilesLeft][c-addTilesTop]
      else
        adder = [[#tp:"default", #data:0], [#tp:"default", #data:0], [#tp:"default", #data:0]]
      end if
      ql.add(adder)
    end repeat
    newTEmatrix.add(ql)
  end repeat
  
  
  repeat with effect in gEEprops.effects then
    newEffMtrx = []
    
    repeat with q = 1 to sze.locH + addTilesLeft then
      ql = []
      repeat with c = 1 to sze.locV + addTilesTop then
        if (q+addTilesLeft<=effect.mtrx.count)and(c+addTilesTop<=effect.mtrx[1].count)and(q-addTilesLeft>0)and(c-addTilesTop>0)then
          adder = effect.mtrx[q-addTilesLeft][c-addTilesTop]
        else
          adder = 0
        end if
        ql.add(adder)
      end repeat
      newEffMtrx.add(ql)
    end repeat
    
    effect.mtrx = newEffMtrx
  end repeat
  
  
  gLEprops.matrix = newMatrix
  gTEprops.tlMatrix = newTEmatrix
  gLOprops.size = sze + point(addTilesLeft, addTilesTop)
  
  global gLASTDRAWWASFULLANDMINI
  gLASTDRAWWASFULLANDMINI = 0
  
  oldimg = member("lightImage").image.duplicate()
  member("lightImage").image = image((gLOprops.size.locH*20)+300,(gLOprops.size.locV*20)+300, 1)
  member("lightImage").image.copypixels(oldimg, oldimg.rect, oldimg.rect)
end






on ResetgEnvEditorProps()
  
  global gEnvEditorProps
  gEnvEditorProps = [#waterLevel:-1, #waterInFront:1, #waveLength:60, #waveAmplitude:5, #waveSpeed:10]
  
  
end

on resetPropEditorProps()
  global gPEprops
  gPEprops = [#props:[], #lastKeys:[], #keys:[], #workLayer:1, #lstMsPs:point(0,0), pmPos:point(1,1), #pmSavPosL:[], #propRotation:0, #propStretchX:1, #propStretchY:1, #propFlipX:1, #propFlipY:1, #depth:0, #color:0]
end













