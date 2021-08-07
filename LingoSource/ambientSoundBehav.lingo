
global gLOprops, gSEProps

on mouseWithin(me)
  if gSEProps.pickedUpSound<>"NONE" then
    sprite(me.spriteNum).visibility = random(2)-1
    
    nm =  me.spriteNum - 38
    
    --  if gSEProps.pickedUpSound = "None" then
    --    member("AmbienceSound"&nm).text="No Ambience Sound"
    --  else
    --    member("AmbienceSound"&nm).text=gSEProps.pickedUpSound
    --  end if
    
    
    if (_mouse.mouseDown) then
      if gSEProps.pickedUpSound="QUIET" then
        gSEProps.sounds[nm].mem = "None"
        sav = member("amb"&nm)
        member("AmbienceSound"&nm).text="No Ambience Sound"
        member("amb"&nm).importFileInto("music\" & "overwrite" &".mp3")
        sav.name = "amb"&nm
        sound(nm).pan = gSEProps.sounds[nm].pan
        sound(nm).volume = gSEProps.sounds[nm].vol
        sound(nm).stop()
      else
        gSEProps.sounds[nm].mem = gSEProps.pickedUpSound
        member("AmbienceSound"&nm).text=gSEProps.sounds[nm].mem
        sav = member("amb"&nm)
        member("amb"&nm).importFileInto("music\ambience\" & gSEProps.sounds[nm].mem &".mp3")
        sav.name = "amb"&nm
        sound(nm).pan = gSEProps.sounds[nm].pan
        sound(nm).volume = gSEProps.sounds[nm].vol
        sound(nm).play([#member:sav, #loopCount:0])
      end if
      gSEProps.pickedUpSound = "NONE"
      member("ButtonText").text=""
    end if
  end if
end mouseWithin

on mouseLeave(me)
  sprite(me.spriteNum).visibility = 1
  nm =  me.spriteNum - 38
  
  if gSEProps.sounds[nm].mem = "None" then
    member("AmbienceSound"&nm).text="No Ambience Sound"
  else
    member("AmbienceSound"&nm).text=gSEProps.sounds[nm].mem
  end if
  
end mouseLeave