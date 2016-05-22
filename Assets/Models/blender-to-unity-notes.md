# Notes for importing from Blender to Unity3D

## Importing the models into Unity3D from Blender
By default, Unity3D imports .blend files automatically, with little to no issues. If the model has no armature nor animations, such as a building or a rock, simply saving the file as a .blend does the job. Sometimes there are issues importing the .blend file into Unity. In which case, it is easiest to just export it to a .fbx

**While direct imports of .blend files works most of the time, I will be using .fbx files for the majority of my models, to avoid any import issues**

However, if you need animations or any sort of rigging to be imported into Unity3D, you will **have** to export it to a .fbx
- At times, all of the normals are flipped when exporting to a .fbx, resulting in the model being "see through" in Unity3D. It's an effect similar to when you glitch inside of a building and look at stuff from inside of it.
- The fix for this is to simply open and select the mesh in Blender, go into edit mode, under the "Shading / UVs" tab in Blender, and click the "reverse direction" button under normals.

## Rigging and Animation
In Blender, it is possible to separate out different animation layers. However, due to the models' need to be exported as an .fbx, I have found that the only reliable way to get all of the animations imported properly is to put them all on one working channel. Afterwards, you can go into the animations tab in Unity3D and splice out the different sequences by selecting the frame ranges.
- There are guides online that show exporting to .fbx will bring in all of the animation channels. However, I wasn't able to get it working between different versions of Blender and other options, such as different .fbx versions, removing of the default keyframes, and a handful of other options.
- **Putting all the animation keyframes into one channel has been the most reliable method I have found.**

I will test this out with future models, and see if I can make it import all the animation layers. In the mean time, this will suffice, as it is a method that is implemented by others on a fairly regular basis in their games.

All animations are held in seperate .fbx files under `Assets/Models/Characters/Animations/`

## Issues with Spinning during animations on a Nav Mesh Agent
The fix is simple. Untick the "Apply root motion" under the animator component in the objects inspector view.

-Dast
