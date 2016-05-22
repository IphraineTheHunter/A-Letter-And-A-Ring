# Animations in Blender and Unity

## Present issues
Currently, Blender doesn't export the different animation layers as I have been expecting it to. The .fbx file doesn't include the animation frames in a way to appropriately separate the idle animation from the walking animation. This will continue to be an issue with the running animation when that is implemented.

As it stands, it exports the animations with the end frame matching what is in the animator view in Blender, which is limiting all animations to 32 frames.

## Possible Fixes
**Export the Mesh separately from the animations**
- Blender official tutorials use the animations from a stock mesh in a .fbx
  - These animations can be imported into our models if they follow a humanoid form
- If we implement this, then we can just worry about making the models and reuse the animations from other models stored in an .fbx
  - This might be better than the current plan of just stealing over the skeleton and animations from one .blend file and bringing them into another after a few tweaks
**Animate from inside Unity**
- Unsure of how to do this
- General advice online seems to be, do the animations in Blender and import them into Unity.

## TODO
Finish writing these Notes

-Dast
