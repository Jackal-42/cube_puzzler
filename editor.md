# Editor Samples

This guide shows various samples of level design

## Tile Info

| Tile         | Use                                                                 | Additional Info                                 | Variants                                      |
|--------------|---------------------------------------------------------------------|-------------------------------------------------|-----------------------------------------------|
| Ground       | Solid block tile                                                    | None                                            | None                                          |
| Spawn Point  | Indicates the player spawn                                          | Only works on face `0`                          | None                                          |
| Goal         | Ends the level when the player touches it                           | Player has to be touching the ground            | None                                          |
| Push Button  | Toggles gates and anti gates when touched                           | Can be toggled by boxes and fall blocks         | Corresponds to gate colors                    |
| Hold Button  | Toggles gates and anti gates when touched and held down             | Can be toggled by boxes and fall blocks         | Corresponds to gate colors                    |
| Gate         | Toggled on/off by push and hold buttons                             | None                                            | Colors                                        |
| Hollow       | No-collision ground block                                           | Used to optimize levels                         | None                                          |
| Anti Gate    | Has a state opposite of the regular gate                            | None                                            | Colors                                        |
| Pound Ground | Allows for the ground to be pounded (or shuffled) when the player   | None                                            | None                                          |
| Fly Goal     | Works like a regular goal except you can trigger it while in midair | None                                            | None                                          |
| Crate        | 1x1 physics object that you can move around                         | Can activate buttons and will bounce on springs | Gravity speed changes based on variant number |
| Large Crate  | 1x3 version of the crate                                            | Can activate buttons and will bounce on springs | Gravity speed changes based on variant number |
| Spring       | Makes crates bounce                                                 | None                                            | Bounces less based on variant number          |
| Grate        | Block that only grates can pass through                             | None                                            | None                                          |
| One Way      |                                                                     |                                                 |                                               |
| Portal In    |                                                                     |                                                 |                                               |
| Portal Out   |                                                                     |                                                 |                                               |
|              |                                                                     |                                                 |                                               |
