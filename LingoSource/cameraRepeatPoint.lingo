global gCurrentRenderCamera
global gCameraProps
global gRenderCameraTilePos
global gRenderCameraPixelPos

global gPrioCam
global firstCamRepeat

on exitFrame me
  put "camera repeat point"
  
  if(firstCamRepeat)then
    if(gPrioCam = 0)then
      gCurrentRenderCamera = 1
    else
      gCurrentRenderCamera = gPrioCam
    end if
    
    
    firstCamRepeat = false
  else 
    if(gCurrentRenderCamera = gPrioCam)then
      gCurrentRenderCamera = 0
    end if
    
    gCurrentRenderCamera = gCurrentRenderCamera + 1
    if(gCurrentRenderCamera = gPrioCam)then
      gCurrentRenderCamera = gCurrentRenderCamera + 1
    end if
  end if
  
  gRenderCameraTilePos = point(((gCameraProps.cameras[gCurrentRenderCamera].locH / 20.0) - 0.49999).integer , ((gCameraProps.cameras[gCurrentRenderCamera].locV / 20.0) - 0.49999).integer )
  gRenderCameraPixelPos = gCameraProps.cameras[gCurrentRenderCamera] - (gRenderCameraTilePos*20)
  gRenderCameraPixelPos.locH = gRenderCameraPixelPos.locH.integer
  gRenderCameraPixelPos.locV = gRenderCameraPixelPos.locV.integer
  
  gRenderCameraTilePos = gRenderCameraTilePos + point(-15, -10)
end