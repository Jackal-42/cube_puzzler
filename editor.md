# Editor Samples

This guide shows various samples of level design

## Tile Info

| Tile         | Use                                                                 | Additional Info                         | Variants                   |
|--------------|---------------------------------------------------------------------|-----------------------------------------|----------------------------|
| Ground       | Solid block tile                                                    | None                                    | None                       |
| Spawn Point  | Indicates the player spawn                                          | Only works on face `0`                  | None                       |
| Goal         | Ends the level when the player touches it                           | Player has to be touching the ground    | None                       |
| Push Button  | Toggles gates and anti gates when touched                           | Can be toggled by boxes and fall blocks | Corresponds to gate colors |
| Gate         | Toggled on/off by push and hold buttons                             | None                                    | Colors                     |
| Hollow       | No-collision ground block                                           | Used to optimize levels                 | None                       |
| Anti Gate    | Has a state opposite of the regular gate                            | None                                    | Colors                     |
| Pound Ground | Allows for the ground to be pounded (or shuffled) when the player   | None                                    | None                       |
| Fly Goal     | Works like a regular goal except you can trigger it while in midair | None                                    | None                       |
