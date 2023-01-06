# Editor Guide

This guide shows the ins and outs of the level editor

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
| One Way      | Self explainatory                                                                      | None                                                 | None     |

### Logic

| Tile        | Use                                                     | Additional Info                         | Variants |
|-------------|---------------------------------------------------------|-----------------------------------------|----------|
| Push Button | Toggles gates and anti gates when touched               | Can be toggled by boxes and fall blocks | Colors   |
| Hold Button | Toggles gates and anti gates when touched and held down | Can be toggled by boxes and fall blocks | Colors   |
| Gate        | Toggled on/off by push and hold buttons                 | None                                    | Colors   |
| Anti Gate   | Has a state opposite of the regular gate                | None                                    | Colors   |

### Physics

|Tile|Use|Additional Info|Variants|
|-|-|-|-|
|Crate|1x1 physics object that you can move around|Can activate buttons and will bounce on springs|Gravity speed|
|Large Crate|1x3 version of the crate|Can activate buttons and will bounce on springs|Gravity speed|
|Fall Block|Falls after the player steps on it|
|Spring|Makes crates bounce|None|Bounces less with higher variant numbers|
|Grate|Block that is solid for the player, but physics blocks can fall through.|None|None
|Portal In|Portal that the player enters|None|Colors|
|Portal Out|Portal that the player exits out of|None|Colors|

### Decorative
