Play the game [here](https://jackal-42.github.io/cube_puzzler/)

### Controls

**(p)lay/pause** (switch from editor to game mode and back)<br>
**(l)ite rendering** mode (alternate play mode for lower-end devices; use instead of (p)lay in the editor)<br>
**(r)otate** (In editor, rotate the selected tile 90 degrees)<br>
**(r)estart** (In play mode, reload the level)<br>
Click on the `Level Code` button to view/copy the current level code or to paste a new one in. DO NOT hit "cancel" or the application will freeze.
<br>
Left click & drag: draw on the editor cube with the selected tile<br>
Left click & drag (in game mode): freecam<br>
Right click & drag: rotate the editor cube<br>
Number keys: Set the tool to the corresponding number<br>
| Number | Tool       |
|--------|------------|
| 1      | Erase      |
| 2      | Ground     |
| 3      | Spawnpoint |
| 4      | Goal       |
| 5      | Button     |
| 6      | Gate       |
| 7      | Hollow     |

A GUI solution with hotkeys has been suggested in https://github.com/Jackal-42/cube_puzzler/issues/6 <br>
<br>
WASD: move (in game mode). There is no option to disable jumping at the moment, so if you wish to design a level around that, then just mention that jumping is not allowed.<br>
S: POUND (self explanatory)
<br>
### Notes
v0.0 is finished and plays quite well in my opinion. However, you will most likely find bugs. If you do, report them and please, please try and find a consistent setup and adequate description.<br>
Sometimes, the cube face will "corrupt". This is very rare. You will know if it has happened if some of the tiles on the cube appear a very bright pink, the "undefined" texture. If this happens to you, please try and find a setup.<br>
You may place multiple spawn points, but the player will only spawn at the one closest to the bottom right. I didn't see the point in wasting development time on limiting placements on certain tiles.<br>
The goal will give no indication when you have reached it, it is only a texture for the moment. If a goal is placed on a wall, assume that you must stand on that wall to complete the level.<br>
<br>
The team-member created level codes are now found at https://github.com/Jackal-42/cube_puzzler/blob/main/levels.md
