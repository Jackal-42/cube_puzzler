### Controls

(p)lay/pause (switch from editor to game mode and back)<br>
(l)ite rendering mode (alternate play mode for lower-end devices; use instead of (p)lay in the editor)<br>
(r)otate (In editor, rotate the selected tile 90 degrees)<br>
(r)estart (In play mode, reload the level)<br>
Click on the "Level Code" button to view/copy the current level code or to paste a new one in. DO NOT hit "cancel" or the application will freeze.
<br>
Left click & drag: draw on the editor cube with the selected tile<br>
Left click & drag (in game mode): freecam<br>
Right click & drag: rotate the editor cube<br>
1/2/3/4/5/6/7: Set the tool to erase / ground / spawn point / goal / button / gate / hollow<br>
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
### Example Level Codes
Goal On The Wall:<br> ```54ab4ab4ab4ab4ab54ab3adb3adb3adb4ab20a5b12ac7a5b4ab4ab4ab4a6b/225a/225a/225a/225a/225a```<br>
Infinite Staircase:<br> ```140a5b25a5b5a5b40a/35a5b35a5b145a/3ab4ab4ab4ab4ab77ab4ab4ab4ab4ab57ab4ab4ab4ab4ab25a/195a5b20a5b/54ab4ab4ab4ab4ab150a/152ab4ab4ab4ab4ab52a```<br>
