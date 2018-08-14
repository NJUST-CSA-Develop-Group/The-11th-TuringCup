Cover Rifle Animset Pro 1.1
-----------------------

This is a set of 61 cover animations, for for a character armed with a rifle. It also contains 32 additive aiming animations. A mecanim graph, showing how to blend the animations together, is also included.

This pack is compatible, and is ment to be used along with Rifle Animset Pro - a pack of 88 mocap animations for a character with rifle. Get it here:

https://www.assetstore.unity3d.com/en/#!/content/15098


This pack doesn't include a code controller.

1.
List of animations:
-------------------

Rifle_Idle
CoverHi_Idle2CoverR
CoverHi_Idle2CoverL
CoverHi_CoverR
CoverHi_CoverL
CoverHi_CoverR2CoverL
CoverHi_CoverL2CoverR
CoverHi_CoverR2AimR
CoverHi_AimR
CoverHi_AimR2CoverR
CoverHi_CoverR2Idle_Right
CoverHi_CoverL2AimL
CoverHi_AimL
CoverHi_AimL2CoverL
CoverHi_CoverL2Idle_Left
CoverHi_ReloadR
CoverHi_ReloadL
CoverHi_CoverL2Idle
CoverHi_CoverR2Idle
CoverLo_Idle2CoverR
CoverLo_CoverR
CoverLo_CoverR2AimR
CoverLo_AimR
CoverLo_ReloadR
CoverLo_AimR2CoverR
CoverLo_Idle2CoverL
CoverLo_CoverL
CoverLo_ReloadL
CoverLo_CoverL2AimL
CoverLo_AimL
CoverLo_AimL2CoverL
CoverLo_CoverR2CoverL
CoverLo_CoverL2CoverR
CoverLo_CoverR2Idle_Right
CoverLo_CoverL2Idle_Left
CoverLo_CoverR2Idle
CoverLo_CoverL2Idle
CoverLo_CoverR2AimU
CoverLo_AimU
CoverLo_AimU2CoverR
CoverLo_CoverL2AimU
CoverLo_AimU2CoverL

CoverHi_WalkL
CoverHi_WalkLStart
CoverHi_WalkLStop_LU
CoverHi_WalkLStop_RU
CoverHi_WalkR
CoverHi_WalkRStart
CoverHi_WalkRStop_LU
CoverHi_WalkRStop_RU
CoverLo_WalkL
CoverLo_WalkLStart
CoverLo_WalkLStop_LU
CoverLo_WalkLStop_RU
CoverLo_WalkR
CoverLo_WalkRStart
CoverLo_WalkRStop_LU
CoverLo_WalkRStop_RU

CoverHi_AimR_CC
CoverHi_AimR_U
CoverHi_AimR_D
CoverHi_AimR_L
CoverHi_AimR_R
CoverHi_AimR_LU
CoverHi_AimR_LD
CoverHi_AimR_RU
CoverHi_AimR_RD
CoverHi_AimL_CC
CoverHi_AimL_U
CoverHi_AimL_D
CoverHi_AimL_L
CoverHi_AimL_R
CoverHi_AimL_LU
CoverHi_AimL_LD
CoverHi_AimL_RU
CoverHi_AimL_RD
CoverLo_AimR_CC
CoverLo_AimR_U
CoverLo_AimR_D
CoverLo_AimR_R
CoverLo_AimR_L
CoverLo_AimR_RU
CoverLo_AimR_RD
CoverLo_AimR_LU
CoverLo_AimR_LD
CoverLo_AimL_CC
CoverLo_AimL_U
CoverLo_AimL_D
CoverLo_AimL_R
CoverLo_AimL_L
CoverLo_AimL_RU
CoverLo_AimL_RD
CoverLo_AimL_LU
CoverLo_AimL_LD

CoverHi_AimR_Burst_Add
CoverHi_AimL_Burst_Add
CoverLo_AimR_Burst_Add
CoverLo_AimL_Burst_Add
----------------


2.
The mecanim graph variables:

IsHiR - (short for "Is High cover Right pose") if true, the character enters and stays in high cover, facing right side. If false, the character leaves the cover and returns to Idle.

IsHiL - (short for "Is High cover Left pose") if true, the character enters and stays in high cover, facing left side. If false, the character leaves the cover and returns to Idle.

IsLoL - (short for "Is Low cover Left pose") if true, the character enters and stays in low cover, facing left side. If false, the character leaves the cover and returns to Idle.

IsLoR - (short for "Is Low cover Right pose") if true, the character enters and stays in low cover, facing right side. If false, the character leaves the cover and returns to Idle.

RunOutTrigger - if triggered, the character will run out from cover to the left or right (depending on the side he was facing before)

ReloadTrigger - if triggered, character will reload the rifle

IsAim - if true, the character will lean out of cover to the left or right (depending on the side he was facing before) and aim the rifle. If false, he will return to cover.

IsAimTop - if true, the character will stand up from low cover and aim the rifle. If false, he will return to cover.

HorAimAngle, VerAimAngle - floats used for blending the additive aim animations. You can get them by measuring the angles between character's forward axis and the desired aim direction.

IsFire - if true, and if the character is aiming, then he will play additive fire animation. If false, he will stop firing.



---------------------
visit:
http://www.kubold.com
kuboldgames@gmail.com