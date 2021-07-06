--****************************************************************************
--
-- simple wrapper for file write xtras
--
--****************************************************************************

on exportAnImage(img, flNm)
  
  raw_size = img.width*img.height*3
  
  -- put "UNCOMPRESSED SIZE:" && raw_size && "Bytes"
  
  ms = the milliseconds
  
  enc = script("PNG_encode").new()
  data = enc.PNG_encode(img)
  enc = 0
  
  -- put "PNG ENCODING:" && (the milliseconds-ms) && "ms"
  -- put "COMPRESSED SIZE:" && data.length && "Bytes (" & data.length*100/raw_size && "%)"
  
  file_put_contents (the moviepath & flNm  & ".png", data)
  -- put "FILE WAS SAVED AS 'test_24bit.png'"
  -- put
end


----------------------------------------
-- saves (binary) string as file
----------------------------------------
on file_put_contents (tFile, tString)
  
  -- A) fileIO Xtra
  
  fp = xtra("fileIO").new()
  if not objectP(fp) then return -1
  
  fp.openFile(tFile, 1)
  err = fp.status()
  if not (err) then fp.delete()
  else if (err and not (err = -37)) then return err
  
  fp.createFile(tFile)
  err = fp.status()
  if (err) then return err
  
  fp.openFile(tFile, 2)
  err = fp.status()
  if (err) then return err
  fp.writeString(tString)
  fp.closeFile()
  fp=0
  return 1
  
  
  -- B) BinFile Xtra
  --  return bx_file_put_contents (tFile, tString)
  
  
  -- C) Crypto Xtra
  --  return cx_file_put_contents (tFile, tString)
  
end

