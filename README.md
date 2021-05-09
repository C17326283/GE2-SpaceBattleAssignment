# GE2-SpaceBattleAssignment


# Source:
Mass Effect 1
[![YouTube](https://www.youtube.com/static?gl=GB&template=terms)](https://youtu.be/bNnd6oUEQ2I)

# Description
* A recreation of a mass effect scene in unity with autonomous agents. The ships are controlled with behaviour trees and some of them were modelled by myself in blender. The scene runs entired unassisted and the sequence manager will handle the sequences. I wrote all the code myself.
* Ryan Byrne
* C17326283
* TU857/4

# Most proud of


# How it works

## AI
The AI is handled

## Shooting
* Targeting
* shooter script
* Projectiles

## Sequence
* There is a sequence manager that has a method for each individual sequence section. They are stored and triggered from an array of unity events allowing them to be triggered from anywhere such as in world triggers or sequentially after a time.
* The point manager is used for paths, trigger and camera points which allows access to cycling through or selecting individual objects from the lists.
* Spawn manager
* Audio manager

## Design
* Particle system
* Fog
* Post processing
* skybox
* Trail renders
* particle effects
* Textures: Normal maps were applied to all models after they were uv unwrapped in blender. The normandy has a custom painted texture. Normal maps and textures were got online or from sample scenes included and were repurposed.

## Audio
* All the audio sources are listed below
* Audacity was used to isolate specific sound clips and some voice clips were cleaned with voice isolation.
* The audio manager is used to choose which audio clip to play
* An audio mixer is used on all audio sources to control the master sound level


## Behaviours:
  * Path Following
  * Seek
  * Arrive
  * Divert
  * Offset Pursue
  * HarmonicSway
  * Obstacle Avoidance

## Animations

# Storyboard
![Storyboard](/images/Storyboard1.png)
![Storyboard](/images/Storyboard2.png)
![Storyboard](/images/Storyboard3.png)
![Storyboard](/images/Storyboard4.png)
![Storyboard](/images/Storyboard5.png)
![Storyboard](/images/Storyboard6.png)
![Storyboard](/images/Storyboard7.png)


# Resources
* Audio clips cut with audacity from: https://youtu.be/bNnd6oUEQ2I
* Mass effect music: https://youtu.be/Bzc1M5wmi84 , https://youtu.be/9sxzrEORGPs , https://youtu.be/oMed0AQt6g4 , https://youtu.be/0rpExCO94LU
* Various sound effects: https://youtu.be/khezJzbt9wY , https://www.youtube.com/watch?v=hq3yt1CyYg0&t=26s , https://www.youtube.com/watch?v=bhZs3ALdL7Y

* Assets:
  * Explosions & teleport effects: https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325
  * Behaviour tree engine: https://assetstore.unity.com/packages/tools/ai/panda-bt-free-33057
  * Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/starfield-skybox-92717

## Models
Some models were downloaded had to be heavilty edited by editing/adding verticies, changed the flat faces to smooth, splitting the mesh into multiple objects and UV unwrapping.
* Models made by me: Normandy, Citadel, Alliance ships
* Downloaded/edited models: https://www.thingiverse.com/thing:702830/comments, https://www.thingiverse.com/thing:877304, https://www.thingiverse.com/thing:878702/files, https://www.thingiverse.com/thing:81436

