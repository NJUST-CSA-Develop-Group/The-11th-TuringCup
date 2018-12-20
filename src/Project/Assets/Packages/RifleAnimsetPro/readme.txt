Rifle Animset Pro v.1.3
--------------------


This is a complete set of motion capture animations, to build a seamless, third person perspective character with rifle movement for your game.

It comes with examples of Mecanim Controller graphs. It also includes an example controllers, made with Playmaker (Playmaker is needed to use it; If you do not have Playmaker, you will have to script your own controller for included Mecanim graph, or just use the animations however you like).

It consists of over 80 animations from which you can make game mechanics for:

- Walking
- Running
- Shooting
- Getting hit and dying
- Grenade throwing
- Aiming with Additive animations
- Melee attacks
- Jumping
- and more!

It also contains:
- Skinned model of a modern day soldier (26,536 tris, 13,813 verts)
- Skinned model of a dummy, to preview animations
- Simple mesh of a rifle, a placeholder

It also contains:
- example of a TPP shooter game with Player and Enemies working "out of the box" controllers
- example of a sidescrolling platformer game with Player and Enemies working "out of the box" controllers

--------------------

Animations are cut to ready-to-use clips. They have an animated Root bone, so you can use Root Motion and move the character around just with animations. Animations use Unity's Humanoid rig (they can be played on any humanoid character), but they can be easly converted to Generic or Legacy rig. If you wish to have all those animations in place (with no Root Motion), just uncheck "Enable Root Motion" in your character prefab in the scene, or delete the animation from Root bone (Legacy).

The skeleton that animations are baked on to is a standard, Motionbuilder compatible skeleton. It has Motionbuilder hierarchy and naming convention. Hips, Neck and Head have Z axes pointing forward, so you can use Look At constraints etc.

--------------------

Few tips:
- All animations can be retargeted to any Humanoid
- XBox 360 controller is supported
- Not all animations are used by the controller - build your own or expand this one! You are the game developer :)
- You can switch weapons in the game. The functionality of each weapon is different (rifle, shotgun, pistol), but to get different animations you need to buy additional aniamtion packs. You can then replace the Rifle animations in the animtree (check the included switchweapons.png). The controller will do the switching animations for you.
- Enemies use Unity Navigation, so you need to create a Navmesh on your level (go to: Window\Navigation\Bake. Navmesh will only bake on "Static" objects).


--------------------


See AnimationDescriptions.pdf to get full description of each animation.




--------------------
Created by Kubold
kuboldgames@gmail.com
http://www.kubold.com
https://www.facebook.com/kuboldgames