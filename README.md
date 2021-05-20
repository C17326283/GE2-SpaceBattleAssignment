# GE2-SpaceBattleAssignment


# Description
* Ryan Byrne
* C17326283
* TU857/4
* A recreation of a mass effect scene in unity with autonomous agents. The ships are controlled with behaviour trees and some of them were modelled by myself in blender. The scene runs entired unassisted and the sequence manager will handle the sequences. I wrote all the code myself.

# Videos:
Video Demo:

  [![Source](https://img.youtube.com/vi/o8OacDj2oX8/0.jpg)](https://youtu.be/o8OacDj2oX8)
  
Source scene: Mass Effect 1 - Battle for the citadel - Paragon

[![Source](https://img.youtube.com/vi/bNnd6oUEQ2I/0.jpg)](https://youtu.be/bNnd6oUEQ2I)

# Most proud of
Im most proud of the sequencing managers that is easy to add to and can seemlessly trigger sequences and camera functions. Im also proud of the ships ai and behaviours which seemlessy handle what the ship is doing at any time and moves it around naturally.


# Events summary
1. Reapers arrive and camera follows them as they move toward citadel
2. Spawn Geth after reaper
3. Reaper Side view
4. Destiny ascension tries to escape
5. Citadel Starts closing
6. Reaper ramming ships
7. Reaper inside citadel
8. Watch battle outside citadel
9. Reaper attaching to spire
10. Focus on teleporter Arrives
11. Normandy Arrives
12. Alliance Arrives
13. Watch geth around alliance blow up
14. Alliance flies in through explosions
15. Ascension starts leaving
16. Citadel opens and ships move in
17. Sovereign fights back as its getting attacked while attached
18. Sovereign shields down
19. Everyone starts firing
20. Normandy dips for big attack
21. Normandy shoots big rocket
22. Sovereign explodes
23. End speach as ships fly away
24. End



# How it works

## Sequence
* There is a sequence manager that has a method for each individual sequence section. They are stored and triggered from an array of unity events allowing them to be triggered from anywhere such as in world triggers or sequentially after a time.
* Triggering: There are world triggers that can cause specific sequences to start when the desired object hits it. Sequences can also trigger the following sequence once its done.
* The point manager is used for paths, trigger and camera points which allows access to cycling through or selecting individual objects from the lists.
* Spawn manager: The spawn manager triggers the specific teleporting group when the sequence manager needs it, which also handles the teleporting trail & spawn effect. Has a no overlap spawning system.
* Audio manager: All the voice and music is managed with the audio manager. More detail below
* Camera Targetting: the camera targetting script has a lot of methods and behaviours for controlling camera such as moving/aligning to fixed point, panning, look obj, follow object pos with offset.

## AI
* The AI is handled with a behaviour tree which controls the flow of the ships ai and will trigger functions in the specific behaviour scripts.
* The behaviours add forces to the ship boid script which handles applying the actual forces that are applied to the rigidbody as well as the turning & banking.
* There are multiple abstracted behaviour scripts for handling specific behaviours but they all pull from the BaseShipBehaviour abstract class. Some behaviours are listed below.
* There are multiple behaviour tree structure for different functions like the fighter behaviour tree and the scene ship path following behaviour tree.
* There is also a life class which handles the health, turning off of ai and the explosions on death.
![BT](/images/BehaviourTrees.png)

## Behaviours:
  * Path Following: Cycle through path waypoints and seek the next point.
  * Seek: Add force towards the target.
  * Arrive: Add force but slow as the ship arrives.
  * ChaseTarget: Target and follow an enemy ship as it moves so the gun can fire at it.
  * Divert: After a shooting run the ship will fly back to a divert point to get ready for another shooting run.
  * Offset Pursue: Follow a point with a specific offset for squadron or defence behaviour.
  * HarmonicSway: Make the ship sway left and right as its moving to seem more natural
  * Obstacle Avoidance: Prevent the ship from crashing when it gets too close to an object by adding a force in the opposite direction

## Weapons
* Targeting: The targeting works independently from the behaviour targeting to allow firing at multiple ships.
* Shooting: If the ship can get a target it will fire a shot in the direction of the enemy.
* Projectiles: There are multiple projectiles that act differently that all pull from the projectile class.
    * Laser: This has an initial force and goes straight.
    * Rocket: This has a continuous force forward as it moves. These will also explode after a set amount of time if it doesnt hit anything.
    * SeekingRocket: Pulls from rocket base class. This will move forward but also turn towards its target as it moves to ensure more accuracy.
* These all have a trail renderer to visually show the lasers or display the moving trajectory of the rockets.
* There are multiple explosion effects for the different ships and weapons.
* The reaper also has multiple weapons that are controlled from the reaperweapon manageement script. The normandy also has a super weapon thats enabled when it flanks.

## Design
* Particle system: There are multiple particle systems used for filling the scene with a fog/smoke effect and adding space particles.
* Post processing: Post processing is used to make the scene look visually better such as colour grading and most obviously the bloom which make the trails and stars look much better.
* Skybox: The skybox adds the space visuals in the background.
* Trail renders: The ships and projectiles have vibrant emissive trails.
* Textures: Normal maps were applied to all models after they were uv unwrapped in blender. The normandy has a custom painted texture. Normal maps and textures were got online or from sample scenes included that were repurposed.

## Audio
* The audio manager triggers the correct voice lines and music to play when the sequence manager needs them. This can be done in a cycle or for specific lines. Voice lines can be triggered with a delay and music has a volume fade on start and end.
* Audacity was used to isolate specific sound clips and some voice clips were cleaned with voice isolation.
* An audio mixer is used on all audio sources to control the master sound level.
* All the audio sources are listed below.
* Audio variance is added to repetetive noises such as laser firing to change the pitch everytime which sounds more natural.
* There are sound effects triggered for firing weapons and explosions such as a laser, beam and rocket fire sound effect.
* There is also a sound for ships teleporting in and a repeating sound on the relay station.

## Animations
* I made animations for the reaper and citadel with the unity animation system;
* The citadel has 3 animations which are triggered from the sequence. These will open and close all the arms.
* The reaper moves its legs randomly around since the ship is actually a large lifeform.

## Other
* The debris script spawns a random number of debris items in a set random scale range when a ship dies. This makes the scene look like a warzone by the end.
* The reaper has a ramming script so it will destroy alliance ships it crashes into.
* The game manager handles the closing of the application at the end as well as when the user presses escape.
* A scale chart was used to compare the models to ensure everything was sized correctly.

# How to use
* You can load the project into unity or just run the exe in the build
* It runs autonomously and will close when it ends
* Press escape to close it at any time

# Classes
* AudioManager: Made myself with inspiration for fading audio from https://gamedevbeginner.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
* AudioVariance: Wrote Enirely Myself
* CameraTargeting: Wrote Enirely Myself
* DebrisSpawner: Wrote Enirely Myself
* DestroyAfterTime: Wrote Enirely Myself
* GameManager: Wrote Enirely Myself
* GunTargeting: Wrote Enirely Myself
* Laser: Wrote Enirely Myself
* Life: Wrote Enirely Myself
* PointManager: Wrote Enirely Myself
* Projectile: Wrote Enirely Myself
* Ramming: Wrote Enirely Myself
* ReaperWeaponManager: Wrote Enirely Myself
* Rocket: Wrote Enirely Myself
* Rotator: Wrote Enirely Myself
* SeekingRocket: Wrote Enirely Myself
* SequenceManager: Wrote Enirely Myself
* ShipBoid: Wrote myself but partially adapted from course code
* Shooting: Wrote Enirely Myself
* SpawnManager: Wrote Enirely Myself
* TeleportSpawner: Wrote Enirely Myself
* TriggerSequence: Wrote Enirely Myself
* BaseShipBehaviour: Wrote Enirely Myself
* CombatBehaviour: Wrote Enirely Myself
* HarmonicSwayBehaviour: Wrote Enirely Myself
* ObstacleAvoidBehaviour: Wrote Enirely Myself
* OffsetPursueBehaviour: Wrote Enirely Myself
* PathFollowBehaviour: Wrote Enirely Myself
* CreatePandaScriptMenu: From PandaBT asset
* PandaBehaviourEditor: From PandaBT asset
* PandaBehaviour: From PandaBT asset
**BT trees**
* FighterShip: Wrote Enirely Myself
* PathingShip: Wrote Enirely Myself


# Storyboard
![Storyboard](/images/Storyboard1.png)
![Storyboard](/images/Storyboard2.png)
![Storyboard](/images/Storyboard3.png)
![Storyboard](/images/Storyboard4.png)
![Storyboard](/images/Storyboard5.png)
![Storyboard](/images/Storyboard6.png)
![Storyboard](/images/Storyboard7.png)

# Models
Some models were made by me in blender and some were downloaded but had to be heavilty edited by editing/adding verticies, changed the flat faces to smooth, splitting the mesh into multiple objects for animations and UV unwrapping.
* Models made by me: Normandy, Citadel, Alliance frigates, Rockets & Debris.
* Downloaded/edited models: https://www.thingiverse.com/thing:702830/comments, https://www.thingiverse.com/thing:877304, https://www.thingiverse.com/thing:878702/files, https://www.thingiverse.com/thing:81436

Normandy, Geth, Reaper
![Models](/images/Models1.png)
Citadel open, Citadel closed, Teleport relay
![Models](/images/Models2.png)
Destiny Ascension(Alliance Flagship), Alliance Frigate
![Models](/images/Models3.png)



# Resources
* Audio clips cut with audacity from: https://youtu.be/bNnd6oUEQ2I
* Mass effect music: https://youtu.be/Bzc1M5wmi84 , https://youtu.be/9sxzrEORGPs , https://youtu.be/oMed0AQt6g4 , https://youtu.be/0rpExCO94LU
* Various sound effects: https://youtu.be/khezJzbt9wY , https://www.youtube.com/watch?v=hq3yt1CyYg0&t=26s , https://www.youtube.com/watch?v=bhZs3ALdL7Y

* Assets:
  * Explosions & teleport effects: https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325
  * Behaviour tree engine: https://assetstore.unity.com/packages/tools/ai/panda-bt-free-33057
  * Skybox: https://assetstore.unity.com/packages/2d/textures-materials/sky/starfield-skybox-92717

