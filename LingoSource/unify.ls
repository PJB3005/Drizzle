global c, gSkyColor, lightRects, gLevel

on exitFrame me
--  if _key.keyPressed(48) then
--    _movie.go(9)
--  end if
--  if (gLevel.lightType<>"No Light")and(1<>1) then
--    repeat with c = 1 to 800 then
--      repeat with q = 1 to 1040 then
--        cl = member("shadowImage").image.getPixel(q-1, c-1)
--        if cl = color( 255, 255, 255 ) then
--          repeat with dir in [point(-1, 0), point(0,-1), point(1,0), point(0,1)]then--,point(-1, -1), point(1,-1), point(1,1), point(-1,1)]then
--            if member("shadowImage").image.getPixel(q-1 + dir.locH*2, c-1 + dir.locV*2) = cl then
--              --   if member("shadowImg").image.getPixel(q-1 + dir.locH, c-1 + dir.locV) <> color(255, 255, 255) then
--              member("shadowImage").image.setPixel(q-1 + dir.locH, c-1 + dir.locV, cl)
--              --  end if
--            end if
--          end repeat
--        end if
--        
--      end repeat
--      
--    end repeat
--  end if
--  
--  sprite(3).rect = rect(0, c-8, 1040, c+1-8)
--  
--  
--  -- c = c + 1 + (1000*(gLevel.lightType="No Light"))
--  member("rainBowImage").image = image(1040, 800, 32)
--  member("rainBowMask").image = image(1040, 800, 1)
--  member("tempRainBowImage").image = image(1040, 800, 32)
--  
--  member("rainBowImage").image.copyPixels(member("pxl").image, rect(0,0,1040,800),rect(0,0,1,1), {#color:color(255,255,255)})
--  member("rainBowMask").image.copyPixels(member("pxl").image, rect(0,0,1040,800),rect(0,0,1,1), {#color:color(0,0,0)})
--  
--  
--  -- if c > 800 then
--  c = 1
--  -- gSkyColor = color(122, 122, 122)
--  sprite(1).color = gSkyColor
--  
--  member("blurredLight").image = image(1040,800,32)
--  --  member("blurredLight").image.copypixels(member("pxl").image, rect(0,0,1040,800), rect(0,0,1,1))
--  
--  lightRects = [rect(1000, 1000, -1000, -1000), rect(1000, 1000, -1000, -1000)]
--  --  else
--  --    go the frame
--  --  end if
  
end





