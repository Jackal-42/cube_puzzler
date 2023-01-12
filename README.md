# Cube Puzzler

[Play the game here](https://jackal-42.github.io/cube_puzzler/)

[Levels made by the devs and community]levels.md

[Editor documentation](editor.md)

![Screen Shot 2023-01-10 at 9 10 34 AM](https://user-images.githubusercontent.com/77758464/211573529-95a2f5d1-fb21-430d-b64b-50ada0dbe551.png)


### Controls

**(p)lay/pause** (switch from editor to game mode and back)<br>
**(r)otate** (In editor, rotate the selected tile 90 degrees)<br>
**(r)estart** (In play mode, reload the level)<br>
**+/-** (In editor, scale the GUI)<br>
**ctrl + z / ctrl + y** (In editor, undo / redo)<br></br>
Click on the `Level Code` button to view/copy the current level code or to paste a new one in. DO NOT hit "cancel" or the application will freeze.
<br>
**Left click & drag:** Draw on the cube while in-editor with the selected tile<br>
""Left Shift** (in editor) show gravity<br>
**Shift + Left Click** (in editor) rotate the gravity of the clicked face<br>
**Left click & drag (in game mode):** freecam during gameplay<br>
**Right click & drag:** rotate the cube in-editor<br>

<br>
WASD: move (in game mode).<br>
S: POUND (self explanatory)<br>
SPACE: push/pull boxes (make sure to face them, one box at a time)
<br>

### Portal & Box Etiquitte
<img width="119" alt="image" src="https://user-images.githubusercontent.com/91426054/205152738-887700b7-7bed-4379-b6dd-7133acfcfd29.png">
Portals should be placed in this configuration to work correctly, unless testing. The hollow is not optional, but the ground may be swapped out for no-pound.<br>
Boxes may go through portals but NOT with the player<br>
Boxes pass through grates and the player does not<br>
The springs and boxes have variants but do not have color modifications. Mess around with it and see how gravity/force changes<br>

### Falling Platforms
Standing on them for 40 frames will trigger a fall<br>
They can be stood on and fall again in another direction once they hit a solid surface<br>
They cannot be pushed and pulled like other physicsObjects<br>

### Notes
v0.0 is finished and plays quite well in my opinion. However, you will most likely find bugs. If you do, report them and please, please try and find a consistent setup and adequate description.<br>
Sometimes, the cube face will "corrupt". This is very rare. You will know if it has happened if some of the tiles on the cube appear a very bright pink, the "undefined" texture. If this happens to you, please try and find a setup.<br>
You may place multiple spawn points, but the player will only spawn at the one closest to the bottom right. I didn't see the point in wasting development time on limiting placements on certain tiles.<br>
<br>
The team-member created level codes are now found at https://github.com/Jackal-42/cube_puzzler/blob/main/levels.md
