global gLEProps, gLOprops, gDirectionKeys

on exitFrame me
  
  cols = gLOprops.size.loch
  rows = gLOprops.size.locv
  member("levelEditImage1").image = image(52*16, 40*16, 16)
  member("levelEditImage2").image = image(52*16, 40*16, 16)
  member("levelEditImage3").image = image(52*16, 40*16, 16)
  member("levelEditImageShortCuts").image = image(52*16, 40*16, 16)
  lvlEditDraw(rect(1,1,cols,rows), 1)
  lvlEditDraw(rect(1,1,cols,rows), 2)
  lvlEditDraw(rect(1,1,cols,rows), 3)
  drawShortCutsImg(rect(1,1,cols,rows), 16)
  
  gDirectionKeys = [0,0,0,0]
  
  repeat with q = 800 to 820 then
    sprite(q).visibility = 1
  end repeat
  sprite(2).visibility = 1
  sprite(8).visibility = 1
  
  member("toolsImage").image = image(gLEProps.toolMatrix[1].count*32, gLEProps.toolMatrix.count*32, 16)
  repeat with q = 1 to gLEProps.toolMatrix.count then
    repeat with c = 1 to gLEProps.toolMatrix[1].count then
      rct = rect((c-1)*32, (q-1)*32, c*32, q*32)
      nm = "icon"&gLEProps.toolMatrix[q][c]
      member("toolsImage").image.copyPixels(member("icon"&gLEProps.toolMatrix[q][c]).image, rct, rect(0,0,32,32))
    end repeat
  end repeat
  
 gLEprops.levelEditors[1].p.mirrorPos = gLOprops.size.loch/2
end