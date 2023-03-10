# Editor Guide

This guide shows the ins and outs of the level editor. The editor can be accessed from the title screen by completing the game, or by hitting `CTRL+I` if you're in a hurry or something.

**_This isn't done_**

## Tile Info

Detailed info about every tile

### Essentials 

| Tile         | Use                                                                                    | Additional Info                                      | Variants |
|--------------|----------------------------------------------------------------------------------------|------------------------------------------------------|----------|
| Ground       | Solid block tile                                                                       | None                                                 | None     |
| Hollow       | No-collision ground block                                                              | Used for level optimization                          | None     |
| Spawn        | Indicates player spawn                                                                 | Only works on face `0`                               | None     |
| Goal         | Ends the level when the player touches it                                              | Player has to be touching the ground                 | None     |
| Fly Goal     | Works like a regular goal except you can trigger it while in midair                    | None                                                 | None     |
| Pound Ground | Allows for the ground to be pounded (or shuffled) when the player presses the down key | Only works when `Can Pound?` is toggled on in-editor | None     |
| One Way      | Self explainatory                                                                      | Sides other than the top cannot be jumped off of     | None     |

### Logic

| Tile        | Use                                                     | Additional Info                         | Variants |
|-------------|---------------------------------------------------------|-----------------------------------------|----------|
| Push Button | Toggles gates and anti gates when touched               | Can also be toggled by boxes and fall blocks | Colors   |
| Hold Button | Toggles gates and anti gates when touched and held down | Can also be toggled by boxes and fall blocks | Colors   |
| Gate        | Toggled on/off by push and hold buttons                 | None                                    | Colors   |
| Anti Gate   | Has a state opposite of the regular gate                | None                                    | Colors   |
|Hint|Used in levels for players who have softlocked themselves|Toggled *only* by boxes with the same variant number|Colors|

### Physics

|Tile|Use|Additional Info|Variants|
|-|-|-|-|
|Crate|1x1 physics object that you can move around|Can activate buttons and will bounce on springs|Gravity speed|
|Large Crate|1x3 version of the crate|Can activate buttons and will bounce on springs|Gravity speed|
|Fall Block|Falls after the player steps on it|Cannot be pushed or pulled|
|Spring|Makes crates bounce|None|Bounces less with higher variant numbers|
|Grate|Block that is solid for the player, but physics blocks can fall through.|None|None
|Portal In|Portal that the player enters|None|Colors|
|Portal Out|Portal that the player exits out of|None|Colors|

### Decorative
|Tile|Size|Additional Info|Produces Light?|
|-|-|-|-|
|Clock|1x2|None|No|
|Web|1x1|None|No|
|Sofa|1x2|None|No|
|Chandelier|1x4|None|Yes|
|Short Candle|1x1|None|Yes|
|Tall Candle|1x2|None|Yes|
|Table|1x1|None|No|
|Wall Clock|1x1|None|No|
|Text|N/A|Depending on variant, changes between multiple texts used in the campaign.|No|
|Bookcase|1x2|Eyes will blink out of the black space in the top of the bookcase if you are not in the same face as it.|No|

## Editor Mechanics

Guide on using the editor

### Controls

|Key|Use|
|-|-|
|1-9|Switch between the first 9 tiles|
|`R`|Rotate the selected tile|
|`F`|Sets view to face `0`|
|`+` and `-`|Scale the editor GUI|

### Change Gravity

Press and hold `shift` and it should look like this:

![gravity_arrow](https://user-images.githubusercontent.com/77758464/211035695-505d4d3a-6d1c-4e7f-864f-76904bc56d00.png)

Just left-click the face to change the gravity, this will only affect the movement of physics objects such as boxes. This gravity is indicated in-game by the player's beard and by the orientation of the background landscape.

### Variants
![image](https://user-images.githubusercontent.com/91426054/211079451-4bdf8f36-953b-46bf-98a8-b50a48639483.png)

Some tiles have multiple variants. You can see what the variants do by consulting the tables earlier in this document.<br>
Not every variant comes with a color change. It may sometimes be difficult to tell as there is currently no indicator.<br>
Swap variant using the four numbered buttons at the top of the editor.<br>
The current variant is shown just below the selected tile.<br>

## Etiquette

### Portal Encasing
<img width="134" alt="image" src="https://user-images.githubusercontent.com/91426054/211056694-02df3109-96d1-4ffa-9c6c-e81c8cb59cd6.png">
Portals are meant to be encased in a configuration such as this one to prevent awkward teleportations and graphical glitches. The hollow tiles are essential to the portal's functionality

### Hollow Corners
<img width="65" alt="image" src="https://user-images.githubusercontent.com/91426054/211078312-2b2cf6f8-88c8-46b1-8f74-8b65df0f365a.png">
If the corner of a wall is out of bounds and the player will never interact with it, swap it out for a hollow tile to save processing power

### Minimal `physicsObject` interaction
<img width="96" alt="image" src="https://user-images.githubusercontent.com/91426054/211078797-4c4cc15c-12ff-482f-a1cd-efa0725af7e4.png">
`physicsObjects` such as boxes and fall blocks may have weird behavior when contacting each other due to the physics system. You can try to combine them, but if it doesn't work, try something else

## Level Idea Concepts 
