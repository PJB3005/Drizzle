global gLOprops, gEnvEditorProps


on exitFrame me
  script("levelOverview").goToEditor()
  
  if gLOprops.size.locH > gLOprops.size.locV then
    fac = 1024.0/gLOprops.size.locH
  else
    fac = 768.0/gLOprops.size.locV
  end if
  rct = rect(1024/2, 768/2, 1024/2, 768/2) + rect(-gLOprops.size.locH*0.5*fac, -gLOprops.size.locV*0.5*fac, gLOprops.size.locH*0.5*fac, gLOprops.size.locV*0.5*fac)
  
  
  
  repeat with q = 1 to 2 then
    sprite(q).rect = rct
  end repeat
  
  
  sprite(4).rect = rct
  
  if gEnvEditorProps.waterLevel >= 0 then
    sprite(3).visibility = true
    sprite(5).visibility = true
    h = rct.bottom - (gEnvEditorProps.waterLevel+gLOprops.extraTiles[4]+0.5)*fac
    sprite(3).rect = rect(rct.left-2,  h-1, rct.right+2, 768)
    sprite(5).rect = rect(rct.left-2, h-1, rct.right+2, 768)
    if gEnvEditorProps.waterInFront then
      sprite(3).blend = 5
      sprite(5).blend = 50
    else
       sprite(3).blend = 50
      sprite(5).blend = 5
    end if
  else
    sprite(3).visibility = false
    sprite(5).visibility = false
  end if
  
  if (_key.keyPressed("l") and _movie.window.sizeState <> #minimized)then
    waterLevel = gLOprops.size.locv - gLOprops.extraTiles[2] - gLOprops.extraTiles[4] - (_mouse.mouseLoc.locV/fac).integer
    gEnvEditorProps.waterLevel = waterLevel
  end if
  
  if(script("envEditorStart").checkKey("w")) then
    if gEnvEditorProps.waterLevel = -1 then
      gEnvEditorProps.waterLevel = gLOprops.size.locv/2
    else
      gEnvEditorProps.waterLevel = -1
    end if
  end if
  
  if(script("envEditorStart").checkKey("f")) then gEnvEditorProps.waterInFront = 1 - gEnvEditorProps.waterInFront
  go the frame
end




