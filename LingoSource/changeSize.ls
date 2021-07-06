
global gLOProps, newSize, extraBufferTiles, gLEprops
on exitFrame me
  if _key.keyPressed("A") then
    if (gLOprops.size <> point(newSize[1], newSize[2]))or(newSize[3]>0)or(newSize[4]>0) then
      resizeLevel(point(newSize[1], newSize[2]),newSize[3],newSize[4] )
    end if
    gLOProps.extraTiles = extraBufferTiles.duplicate()
    _movie.go(9)
  else if _key.keyPressed("C") then
    _movie.go(9)
  else
    go the frame
  end if
end