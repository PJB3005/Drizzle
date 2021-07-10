
global gLOprops, gEditLizard

on mouseWithin(me)
  sprite(me.spriteNum).color = color(255,0,0)
  if(gLOprops.mouseClick) then
    -- put sprite(me.spriteNum).member.name
    script("levelOverview").buttonClicked(sprite(me.spriteNum).member.name)
    gLOprops.mouseClick = 0
  end if
  
  case sprite(me.spriteNum).member.name of
    "button geometry editor":
      member("buttonText").text = "(2) Edit the geometry of the level. After this step is finished you can do a TEST RENDER"
    "button tile editor":
      member("buttonText").text = "(3) Edit the tiles. Decide what materials and objects the level will be made of"
    "button effects editor":
      member("buttonText").text = "(4) Add effects and additional objects such as rust, plants, and chains"
    "button light editor":
      member("buttonText").text = "(5) Edit the light source and shadows"
    "button render level":
      member("buttonText").text = "Render and save as a Rain World playable level"
    "button test render":
      member("buttonText").text = "Outputs the level geometry without re-rendering the images"
    "button save project":
      member("buttonText").text = "Save as a Level Editor Project, that can be openened and edited further"
    "button load project":
      member("buttonText").text = "Load a project"
    "button super more time":
      gEditLizard[3] = restrict(gEditLizard[3]+1, 0, 4000)
      member("addLizardTime").text = string(gEditLizard[3])
    "button super less time":
      gEditLizard[3] = restrict(gEditLizard[3]-1, 0, 4000)
      member("addLizardTime").text = string(gEditLizard[3])
    "button lizard hole":
      sprite(60).visibility = random(2)-1
    "button standard medium":
      global gLevel
      if gLevel.defaultTerrain then
        member("buttonText").text = "The level is surrounded by a solid wall"
      else
        member("buttonText").text = "It's possible to fall out of the level"
      end if
    "button add lizard":
      member("buttonText").text = "Add a dragon"
    "button delete Lizard":
      member("buttonText").text = "Delete the last dragon"
    "button light type":
      if(gLOprops.light )then
        member("buttonText").text = "Sunlight: ON" 
      else
        member("buttonText").text = "Sunlight: OFF" 
      end if
    "button sound editor":
      member("buttonText").text = "(6) Edit music and ambient sounds."
    "button mass render":
      member("buttonText").text = "Render multiple levels in one go."
    "button level size":
      member("buttonText").text = "Change the proportions of the level"
    "button cameras":
      member("buttonText").text = "Set camera positions viewing this level"
    "button environment editor":
      member("buttonText").text = "Edit water and environmental elements"
    "button prop editor":
      member("buttonText").text = "Add props"
    "button update preview":
      global gLASTDRAWWASFULLANDMINI
      if(gLASTDRAWWASFULLANDMINI = 1) then
        member("buttonText").text = "Update the room preview (Updated)"
      else
        member("buttonText").text = "Update the room preview (Needs to be refreshed)"
      end if
    "button prio cam":
      member("buttonText").text = "Select a specific camera to render first"
  end case
end mouseWithin

on mouseLeave(me)
  sprite(me.spriteNum).color = color(0,0,0)
  member("buttonText").text = ""
end mouseLeave