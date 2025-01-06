# survival-game


## Github page: 

https://jond2003.github.io/survival-game/builds/index.html

# Instructions:

Survive a set amount of days and nights to win the game. 
Do this by defending yourself from robots with weapons that you can craft, and by crafting better armour.
Health or hunger, when fully depleted will cause the player to die so hunting the animals in the forest are essential to winning.
Crafting can be done when the crafting menu is enabled and that the player is close to the merchant.
Player controls are mappable to any key which the player wants to use. 


# Attributions: 


### Ethans attributions:
These are sources that explained concepts/helped give inspiration for my code. Code has not been copied.

1) PlayerMovement.cs

https://www.youtube.com/watch?v=q-VfsQQlji0 - Taught the new input system

https://www.reddit.com/r/Unity3D/comments/timq6o/why_is_my_rigidbody_falling_sooooo_slow/ - Explanation of why player gravity wasn't working as intended
2) PlayerLook.cs

https://www.youtube.com/watch?v=w4IMYgpqgdQ - Helped with the player camera

3) EnemyAI.cs

https://www.youtube.com/watch?v=u2EQtrdgfNs - Taught the nav system

4) GameTimer.cs 

https://www.youtube.com/watch?v=POq1i8FyRyQ - Helped create the timer 

5) PlayerArmour.cs

https://gamedevbeginner.com/singletons-in-unity-the-right-way/ - explained singletons

6) SoundManager.cs

https://stackoverflow.com/questions/46529147/how-to-set-a-mixers-volume-to-a-sliders-volume-in-unity - explained how to properly set volumes using a slider
### Jonathan's attributions:

1) AutomaticGun.cs & Target.cs

https://www.youtube.com/watch?v=THnivyG0Mvo - Taught raycasts and how to call a method on a target script

2) Billboard.cs

https://www.youtube.com/watch?v=GuWEXBeHEy8  - Taught about canvases in the world-space

3) UI

https://www.youtube.com/watch?v=IuuKUaZQiSU - Helped understand some basic features of unity's UI
https://www.youtube.com/watch?v=nTLgzvklgU8 - Helped understand UI slider
https://www.youtube.com/watch?v=AyuQXfgVk3U - Inspired design for the gun reload indicator on the HUD
https://www.youtube.com/watch?v=oDBVRcHhc0M - Helped understand scroll rect

https://discussions.unity.com/t/fitting-a-parent-ui-element-to-a-child-texts-content/160492 & https://discussions.unity.com/t/auto-resize-text-rect/553277/6 - Helped fixed bugs with scrolling

4) Difficulty parameters

https://www.youtube.com/watch?v=aPXvoWVabPY&t=313s - Helped understand scriptable objects

5) Animation

https://www.youtube.com/watch?v=vApG8aYD5aI - Helped understand basics of animation in unity so I could add animations to the merchant
https://www.youtube.com/watch?v=JVFg9g4f-ME - Helped understand how to use unity's animator for gun animations

6) Interactor.cs

https://www.youtube.com/watch?v=K06lVKiY-sY&t=4s - Helped to implement basic interaction system which Interactor.cs was built on top of

7) PlayerVacuum.cs

https://docs.unity3d.com/2017.4/Documentation/Manual/CollidersOverview.html - Helped understand how colliders work and need for rigidbodies 

### Jaden's attributions:

1) Door.cs
https://www.youtube.com/watch?v=cPltQK5LlGE&list=WL&index=16 - Helped provide a foundational idea of how doors should function with their different types and potential drawbacks

2) DayNightCycle.cs
https://www.youtube.com/watch?v=babgYCTyw3Y&list=PLQfpcxgSc8PERrHaLiuNISJuynw6eBGz3&index=1 - Taught how to move create a basic day and night cycle
https://www.youtube.com/watch?v=IeCDhYqyH50&list=PLQfpcxgSc8PERrHaLiuNISJuynw6eBGz3&index=3 - Taught how to integrate an imported sky box. Taught how to finetune the day and night cycle

3) LightFlicker.cs
https://www.youtube.com/watch?v=5rxMdiCkQGk&t=512s - Taught the importance of different forms of lighting and how to implement them
https://www.youtube.com/watch?v=L0U-r_xu4xM&t=94s - Provided a basic idea of how to create a flickering light
4) Level design
https://www.youtube.com/watch?v=xnRvoHttz34&list=WL&index=18 - Helped with level design
5) Forest Biome
https://www.youtube.com/watch?v=ddy12WHqt-M - Taught me how to use Unity’s terrain system
6) Laboratory biome
https://www.youtube.com/watch?v=CBa_opm3_GM&t=1199s - Taught me how to utilise probuilder
https://www.youtube.com/watch?v=uSZUgs8UGEs - Taught me how to utilise probuilder
https://www.youtube.com/watch?v=L_edNOpC6YY&t=274s - Taught me how to utilise probuilder
https://www.youtube.com/watch?v=Brg5_Vj7X_c&t=222s - Taught me how to utilise probuilder


### Joseph's attributions
1) GameSettingsManager.cs

https://www.youtube.com/watch?v=yhlyoQ2F-NM - helped with creating a singleton class

2) GameTimer.cs

https://www.youtube.com/watch?v=x9IFMcwqkPY - taught and helped in making the timer

3) InventorySlotUI

https://www.youtube.com/watch?v=LnAJ4HQGR7I&list=PLS6sInD7ThM2W9qpLv8VcD3gOiGJcr0lw&index=4 - Helped with Inventory UI Visuals

https://www.youtube.com/watch?v=NN8BX12D6D0&list=PLS6sInD7ThM2W9qpLv8VcD3gOiGJcr0lw&index=3 - Helped with Inventory UI code

https://www.youtube.com/watch?v=4P9xjzLLbx4&list=PLS6sInD7ThM2W9qpLv8VcD3gOiGJcr0lw&index=2 - Taught with Inventory UI logic

4) PlayerHealth.cs

https://youtu.be/0tDPxNB2JNs?si=rFRnr5-OrrN1M3op - Helped with creating the healthbar

https://youtu.be/ftWHJ2OUSBU?si=xs9MJbckVziQOFaH - Helped with the health bar system

https://youtu.be/1OwQflHq5kg?si=ZMN2W6Z6LuHwK9L1 - Helped with the use of canvases 

5) MainMenu.cs

https://www.youtube.com/watch?v=-GWjA6dixV4 - Taught with creating the UI and logic for mainmenu and settings

6) PauseMenuManager.cs

https://www.youtube.com/watch?v=G1AQxNAQV8g - helped with pausing the game

7) RebindMenuManager.cs

https://www.youtube.com/watch?v=puapjyopT9Y&t=315s - Taught with creating the rebinding of keys / input mapping

8) SensitivityManager.cs

https://www.youtube.com/watch?v=h9beyqPe3jY - Taught with creating the sensitivity slider

9) SoundManager.cs

https://www.youtube.com/watch?v=yWCHaTwVblk - Helped with creating the sound slider


## Assets

1. All music created and performed by Ethan Yeung (contributor to the project)

2. Exploding robot and grenade explosion 
https://assetstore.unity.com/packages/vfx/particles/war-fx-5669 

3. Robot model
https://assetstore.unity.com/packages/3d/characters/robots/sleek-toon-bot-free-34490

4. Armour icon 
https://assetstore.unity.com/packages/2d/gui/icons/fantasy-inventory-icons-free-143805

5. Explosion sound for robot and grenade 
https://assetstore.unity.com/packages/audio/sound-fx/grenade-sound-fx-147490

6. Footstep sounds for player, robots, and animal 
https://assetstore.unity.com/packages/audio/sound-fx/classic-footstep-sfx-173668

7. Robot animation 
https://assetstore.unity.com/packages/3d/animations/free-32-rpg-animations-215058

8. Food model 
https://assetstore.unity.com/packages/3d/props/food/simple-foods-207032

9. Food icon 
https://assetstore.unity.com/packages/2d/gui/icons/free-meat-and-skin-icons-196219

10. Weapon models https://assetstore.unity.com/packages/3d/props/guns/low-poly-weapons-vol-1-151980#content

11. Weapon and ammo icons https://assetstore.unity.com/packages/2d/gui/icons/fps-icons-pack-45240

12. Merchant character model https://assetstore.unity.com/packages/3d/characters/survival-stylized-characters-5-weapons-115559

13. Merchant animations https://assetstore.unity.com/packages/3d/animations/basic-motions-free-154271

14. Trash, Arrow, Spring, Steel, Gunpowder, Heart, Hunger icons
https://www.flaticon.com/free-icons/trash
https://www.flaticon.com/free-icons/arrows
https://www.flaticon.com/free-icons/metal
https://www.flaticon.com/free-icons/powder
https://www.flaticon.com/free-icons/heart
https://www.flaticon.com/free-icons/meat

15. Ammo models https://assetstore.unity.com/packages/3d/ammo-types-model-textures-63679

16. Merchant voicelines https://assetstore.unity.com/packages/audio/sound-fx/voices/the-alerted-npc-male-voice-pack-301220

17. Bird sounds https://assetstore.unity.com/packages/audio/ambient/nature/nature-essentials-208227

18. Trees
https://assetstore.unity.com/packages/3d/vegetation/trees/low-poly-tree-pack-57866

19. Skybox
https://assetstore.unity.com/packages/2d/textures-materials/sky/fantasy-skybox-free-18353

20. Tools for terrain building
https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808

21. Forest biome ground textures (grass and mud)
https://assetstore.unity.com/packages/2d/textures-materials/nature/grass-and-flowers-pack-1-17100

22. Forest biome ground textures (pathing)
https://assetstore.unity.com/packages/2d/textures-materials/floors/outdoor-ground-textures-12555

23. Furniture (shelves, lab/ mechanical equipment, tables, a lamp and some railing)
https://assetstore.unity.com/packages/3d/props/outdoor-wall-lamp-259394https://assetstore.unity.com/packages/3d/props/industrial/measuring-meter-843
https://assetstore.unity.com/packages/3d/props/industrial/industrial-props-pbr-130438
https://assetstore.unity.com/packages/3d/props/furniture/small-pack-furniture-56628
https://assetstore.unity.com/packages/3d/props/shelves01-pack-289927
https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-old-rusty-props-pbr-200267

24. Textures (All textures taken from polyhaven.com are in 1K resolution and in the format glTF. Only the diffuse map is used for each)
https://polyhaven.com/a/asphalt_04
https://polyhaven.com/a/brick_wall_10
https://polyhaven.com/a/brushed_concrete_2
https://polyhaven.com/a/concrete_wall_008
https://polyhaven.com/a/dark_brick_wall
https://polyhaven.com/a/dark_wood
https://polyhaven.com/a/factory_wall
https://polyhaven.com/a/old_wood_floor
https://polyhaven.com/a/painted_concrete_02
https://polyhaven.com/a/painted_plaster_wall
https://polyhaven.com/a/pavement_03
https://polyhaven.com/a/pebble_cemented_floor
https://polyhaven.com/a/plaster_brick_pattern
https://polyhaven.com/a/plastered_wall_02
https://polyhaven.com/a/red_brick
https://polyhaven.com/a/rusty_metal_sheet
https://polyhaven.com/a/stone_tile_wall
https://polyhaven.com/a/stone_tiles_02
https://polyhaven.com/a/tiled_floor_001
https://polyhaven.com/a/worn_corrugated_iron



