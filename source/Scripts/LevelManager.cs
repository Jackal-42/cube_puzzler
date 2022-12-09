using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] segments = new GameObject[6];
    public SegmentCamera[] sc = new SegmentCamera[6];
    public Camera[] lc = new Camera[6];
    public bool isLite = false;
    public GameObject topView;
    public CubeManager cubeManager;
    //The ID of the face of the cube the player is currently in; its index in the segments array
    public int focusedSegment = 0;
    //A matrix which contains the ids of the above, right, below, left, and back faces for any face in that order
    public int[][] faceValues = new int[6][]
    {
        new int[5] { 4, 1, 5, 3, 2},
        new int[5] { 4, 2, 5, 0, 3},
        new int[5] { 4, 3, 5, 1, 0},
        new int[5] { 4, 0, 5, 2, 1},
        new int[5] { 2, 1, 0, 3, 5},
        new int[5] { 0, 1, 2, 3, 4}
    };
    //The ids of the tiles in the order they appear on the cube, used for the segment rotation
    private int[][] rowValues = new int[3][]
    {
        new int[3] { 0, 1, 2},
        new int[3] { 3, 4, 5},
        new int[3] { 6, 7, 8}
    };
    public float faceSize = 30;
    public GameObject player;
    public CameraPivot pivot;
    public int playerX = 0;
    public int playerY = 0;
    private EditorManager em;

    public int[] adjacent;

    public Transform portalManager;
    public Portal[] portals = new Portal[14];

    void Start()
    {
        em = GameObject.Find("EditorManager").GetComponent<EditorManager>();
        LoadCode(em.savedCode);

        int drift = (int)(segments[focusedSegment].transform.eulerAngles.y / 90) * -1;
        //Rearranges which sides the faces appear on depending on rotation of the focused face. Gives the illusion of changing gravity
        adjacent = new int[6] { faceValues[focusedSegment][mod(drift, 4)], faceValues[focusedSegment][mod(1 + drift, 4)], faceValues[focusedSegment][mod(2 + drift, 4)], faceValues[focusedSegment][mod(3 + drift, 4)], faceValues[focusedSegment][4], focusedSegment };
        segments[adjacent[4]].transform.rotation *= Quaternion.Euler(0, 180, 0);
    }
    public bool charToBool(char ch)
    {
        if (ch == '1')
        {
            return true;
        }
        return false;
    }


    public void LoadCode(string code)
    {
        if (code.Contains("|"))
        {
            string[] globals = code.Split("|");
            code = globals[1];
            int index = 0;
            foreach (char ch in globals[0])
            {
                if (index == 0)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().canJump = charToBool(ch);
                }
                else if (index == 1)
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().canPound = charToBool(ch);
                }
                index++;
            }
        }

        string[] splitCode = code.Split("/");
        string[] numbers = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] rotIndicators = new string[4] { "", "!", "@", "#" };
        string[] variantIndicators = new string[4] { "", "$", "%", "^" };
        Vector2[] gravityLookup = new Vector2[4] { new Vector2(0, -1), new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0) };
        string[] gravityIndicators = new string[4] { "{", "}", "[", "]" };

        EditorManager em = GameObject.Find("EditorManager").GetComponent<EditorManager>();
        int faceIndex = 0;
        foreach(string s in splitCode)
        {
            int[] values = new int[225];
            int[] rots = new int[225];
            int[] variants = new int[225];
            int valueIndex = 0;
            int finalNum = 0;
            int rot = 0;
            int variant = 0;
            foreach (char c in s)
            {
                string c_s = c.ToString();

                if (gravityIndicators.Contains(c_s))
                {
                    segments[faceIndex].GetComponent<GravityField>().gravity = gravityLookup[Array.IndexOf(gravityIndicators, c_s)];
                    continue;
                }

                if (numbers.Contains(c_s)){
                    finalNum *= 10;
                    finalNum += Int32.Parse(c_s);
                }
                else if (rotIndicators.Contains(c_s))
                {
                    rot = Array.IndexOf(rotIndicators, c_s) * 90;
                }
                else if (variantIndicators.Contains(c_s))
                {
                    variant = Array.IndexOf(variantIndicators, c_s);
                }
                else
                {
                    int index = Array.IndexOf(em.stringValues, c_s);
                    if(finalNum == 0)
                    {
                        finalNum = 1;
                    }
                    for(int i = 0; i < finalNum; i++)
                    {
                        values[valueIndex] = index;
                        rots[valueIndex] = rot;
                        variants[valueIndex] = variant;
                        valueIndex++;
                    }
                    variant = 0;
                    rot = 0;
                    finalNum = 0;
                }
            }
            valueIndex = 0;
            for(int i = 0; i < 9; i++)
            {
                for(int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if(values[valueIndex] != 0)
                        {
                            //TO DO:
                            //If tile is global, set the parent to the segment rather than the segment tile
                            var dt = em.drawableTiles[values[valueIndex]];
                            var cfmt = segments[faceIndex].GetComponent<CubeFaceManager>().tiles[i].transform;
                            GameObject newTile;
                            if (dt.global)
                            {
                                newTile = Instantiate(dt.tile, segments[faceIndex].transform);
                            }
                            else
                            {
                                newTile = Instantiate(dt.tile, cfmt);
                            }
                            newTile.transform.position = new Vector3(x*-2 + 4f, y*-2 + 4f, 0.1f);
                            newTile.transform.position += cfmt.position;
                            newTile.transform.rotation = Quaternion.Euler(0, 0, rots[valueIndex] * -1);

                            if (dt.tintable)
                            {
                                newTile.GetComponent<SpriteRenderer>().color = em.vc[variants[valueIndex]];
                            }

                            if (dt.variantable)
                            {
                                if(dt.name == "Push Button" || dt.name == "Hold Button")
                                {
                                    newTile.GetComponent<PushButton>().environmentValue = variants[valueIndex];
                                }
                                else if(dt.name == "Gate" || dt.name == "Anti Gate")
                                {
                                    newTile.GetComponent<GateScript>().environmentValue = variants[valueIndex];
                                }
                                else if (dt.name == "Portal In" || dt.name == "Portal Out")
                                {
                                    newTile.transform.GetChild(0).GetComponent<Portal>().value = variants[valueIndex];
                                }
                                else if (dt.name == "Spring")
                                {
                                    newTile.GetComponent<Spring>().value = variants[valueIndex];
                                }
                                else if (dt.name == "Crate" || dt.name == "Large Crate")
                                {
                                    newTile.GetComponent<PhysicsObject>().value = variants[valueIndex];
                                }
                            }
                        }
                        valueIndex++;
                    }
                }
            }
            faceIndex++;
        }
    }

    public void Pound()
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 30, player.transform.position.z);

        int col = 0;
        //center the level around the player to simulate a space which loops back on itself
        float centerX = playerX * faceSize;
        float centerY = playerY * faceSize;
        //an integer which corresponds with the face's rotation. 0 for no rotation, 1 for 90 deg, etc.
        int drift = (int)(segments[focusedSegment].transform.eulerAngles.y / 90) * -1;
        //Rearranges which sides the faces appear on depending on rotation of the focused face. Gives the illusion of changing gravity
        adjacent = new int[6] { faceValues[focusedSegment][mod(drift, 4)], faceValues[focusedSegment][mod(1 + drift, 4)], faceValues[focusedSegment][mod(2 + drift, 4)], faceValues[focusedSegment][mod(3 + drift, 4)], faceValues[focusedSegment][4], focusedSegment };
        int[][] tileIds = new int[4][];
        GameObject[][] tiles = new GameObject[4][]
        {
            new GameObject[3],
            new GameObject[3],
            new GameObject[3],
            new GameObject[3]
        };
        print("POUND");
        //checks the player's position to determine which column of the cube they are in
        if(player.transform.position.x + 15 < centerX + 10)
        {
            col = 2;
        }
        else if (player.transform.position.x + 15 < centerX + 20)
        {
            col = 1;
        }
        else
        {
            col = 0;
        }

        if(col == 2)
        {
            segments[adjacent[1]].transform.rotation *= Quaternion.Euler(0, -90, 0);
            cubeManager.faces[adjacent[1]].transform.rotation *= Quaternion.Euler(0, -90, 0);

            var saved = faceValues[adjacent[1]][3];
            faceValues[adjacent[1]][3] = faceValues[adjacent[1]][2];
            faceValues[adjacent[1]][2] = faceValues[adjacent[1]][1];
            faceValues[adjacent[1]][1] = faceValues[adjacent[1]][0];
            faceValues[adjacent[1]][0] = saved;
            
        }

        if (col == 0)
        {
            segments[adjacent[3]].transform.rotation *= Quaternion.Euler(0, 90, 0);
            cubeManager.faces[adjacent[3]].transform.rotation *= Quaternion.Euler(0, 90, 0);

            var saved = faceValues[adjacent[3]][3];
            faceValues[adjacent[3]][3] = faceValues[adjacent[3]][0];
            faceValues[adjacent[3]][0] = faceValues[adjacent[3]][1];
            faceValues[adjacent[3]][1] = faceValues[adjacent[3]][2];
            faceValues[adjacent[3]][2] = saved;
            
        }

        //place the IDs of all the tiles which are affected by the pound rotation into an array, in a specific order
        //according to their relation to the focused face
        int[] segmentIds = new int[4] { focusedSegment, adjacent[2], adjacent[4], adjacent[0] };

        /*
        tileIds[0] = getTiles(segmentIds[0], col);
        tileIds[1] = getTiles(adjacent[2], col);
        tileIds[2] = getTiles(adjacent[4], col);
        tileIds[3] = getTiles(adjacent[0], col);
        */

        for(int i = 0; i < 4; i++)
        {
            tileIds[i] = getTiles(drift, segmentIds[i], col);
            //int index = 0;
            //if(i == 0) {index = focusedSegment}else if(i == 1) {index = adjacent[2]}else if(i == 2) { index = adjacent[4]} else { index = adjacent[0]};
            CubeFaceManager cfm = segments[segmentIds[i]].GetComponent<CubeFaceManager>();
            int tileReached = 0;
            foreach (int id in tileIds[i])
            {
                //Also create an array of the tiles associated with the stored IDs because we are about to shuffle their order, which makes the IDs no longer valid.
                tiles[i][tileReached] = cfm.tiles[id];
                tileReached++;
            }
            if (Mathf.Abs(segments[segmentIds[i]].transform.eulerAngles.y / 90 - segments[segmentIds[mod(i + 1, 4)]].transform.eulerAngles.y / 90) > 1)
            {
                Array.Reverse(tiles[i]);
                print(i.ToString());
                //print(tiles[i][0].name + ", " + tiles[i][1].name + ", " + tiles[i][2].name);
            }
            //mod((int)segments[segmentIds[i]].transform.eulerAngles.y / 90, 2)
        }
        Quaternion focusedRot = segments[segmentIds[0]].transform.rotation;
        Quaternion[] savedRots = new Quaternion[4];
        for(int i = 0; i < 4; i++)
        {
            savedRots[i] = segments[segmentIds[i]].transform.rotation;
            //segments[segmentIds[i]].transform.rotation = focusedRot;
        }
        for (int i = 0; i < 4; i++)
        {
            CubeFaceManager cfm = segments[segmentIds[i]].GetComponent<CubeFaceManager>();
            int tileReached = 0;
            foreach (int id in tileIds[i])
            {
                cfm.tiles[id] = tiles[mod(i - 1, 4)][tileReached];
                if(i == 3)
                {
                    cfm.tiles[id].transform.position = new Vector3(cfm.tiles[id].transform.position.x, cfm.tiles[id].transform.position.y + 90, cfm.tiles[id].transform.position.z);
                }
                else
                {
                    cfm.tiles[id].transform.position = new Vector3(cfm.tiles[id].transform.position.x, cfm.tiles[id].transform.position.y - 30, cfm.tiles[id].transform.position.z);
                }
                cfm.tiles[id].transform.parent = segments[segmentIds[i]].transform;
                tileReached++;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            segments[segmentIds[i]].transform.rotation = savedRots[i];
        }

    }

    int[] getTiles(int drift, int id, int col)
    {
        //an integer which corresponds with the face's rotation. 0 for no rotation, 1 for 90 deg, etc.

        //DEBUG: always use the main face's drift value
        drift = (int)(segments[id].transform.eulerAngles.y / 90) * -1;
        int[] tileIds = new int[3] {0, 0, 0};
        if(drift == 0)
        {
            //No modification
            tileIds[0] = rowValues[0][col];
            tileIds[1] = rowValues[1][col];
            tileIds[2] = rowValues[2][col];
            
        }
        else if (drift == -1)
        {
            //Swap top & bottom rows
            tileIds = rowValues[(col - 2) * -1];
        }
        else if (drift == -2)
        {
            //Swap left & right columns

            
            tileIds[0] = rowValues[0][(col - 2) * -1];
            tileIds[1] = rowValues[1][(col - 2) * -1];
            tileIds[2] = rowValues[2][(col - 2) * -1];

        }
        else
        {
            //Take corresponding row
            tileIds = rowValues[col];
        }

        //If the rotation is 180 or 270 reverse the array's order
        if(drift < -1)
        {
            //Array.Reverse(tileIds);
        }

        return new int[3] { tileIds[0], tileIds[1], tileIds[2] };
    }

    //Returns a positive result regardless of the sign of the original values
    int mod(int a, int b)
    {
        int m = a % b;
        if (m < 0)
        {
            m = (b < 0) ? m - b : m + b;
        }
        return m;
    }

    void RedefinePortalHosts()
    {
        int[][] hosts = new int[5][]
        {
            new int[3]{0, 2, 13},
            new int[3]{3, 4, 6},
            new int[2]{7, 8},
            new int[3]{9, 10, 12},
            new int[3]{1, 5, 11}
        };
        int index = 0;
        foreach(Portal p in portals)
        {
            int hostIndex = 0;
            foreach(int[] i1 in hosts)
            {
                if (i1.Contains(index))
                {
                    p.host = segments[adjacent[hostIndex]].transform;
                }
                hostIndex++;
            }
            index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("l"))
        {
            SceneManager.LoadScene("Editor");
        }
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("Main (backup)");
        }


        //center the level around the player to simulate a space which loops back on itself
        float centerX = playerX * faceSize;
        float centerY = playerY * faceSize;
        //an integer which corresponds with the face's rotation. 0 for no rotation, 1 for 90 deg, etc.
        int drift = (int)(segments[focusedSegment].transform.eulerAngles.y / 90) * -1;
        //Rearranges which sides the faces appear on depending on rotation of the focused face. Gives the illusion of changing gravity
        adjacent = new int[6] { faceValues[focusedSegment][mod(drift, 4)], faceValues[focusedSegment][mod(1 + drift, 4)], faceValues[focusedSegment][mod(2 + drift, 4)], faceValues[focusedSegment][mod(3 + drift, 4)], faceValues[focusedSegment][4], focusedSegment };
        RedefinePortalHosts();
        //If the player has walked off the edge of the focused face, change the focused face to the one the player is currently on
        //Also rotate the levels which are located perpendicular to the direction of the player's movement, because this is how they
        //appear to change on the surface of a cube
        if (player.transform.position.x + 15 < centerX)
        {
            playerX--;
            pivot.targets.y -= 90;
            segments[adjacent[0]].transform.rotation *= Quaternion.Euler(0, 90, 0);
            segments[adjacent[2]].transform.rotation *= Quaternion.Euler(0, -90, 0);
            segments[adjacent[3]].transform.rotation *= Quaternion.Euler(0, 180, 0);
            segments[adjacent[4]].transform.rotation *= Quaternion.Euler(0, 180, 0);
            focusedSegment = adjacent[1];
        }else if (player.transform.position.x + 15 > centerX + faceSize)
        {
            playerX++;
            pivot.targets.y += 90;
            segments[adjacent[0]].transform.rotation *= Quaternion.Euler(0, -90, 0);
            segments[adjacent[2]].transform.rotation *= Quaternion.Euler(0, 90, 0);
            segments[adjacent[1]].transform.rotation *= Quaternion.Euler(0, 180, 0);
            segments[adjacent[4]].transform.rotation *= Quaternion.Euler(0, 180, 0);
            focusedSegment = adjacent[3];
        }else if (player.transform.position.y + 15 < centerY)
        {
            playerY--;
            pivot.targets.x += 90;
            segments[adjacent[1]].transform.rotation *= Quaternion.Euler(0, 90, 0);
            segments[adjacent[3]].transform.rotation *= Quaternion.Euler(0, -90, 0);
            //THE CODE IN THIS IF STATEMENT AND THE FOLLOWING ONE IS PROBLEMATIC
            //it causes weird face shuffling and rotation, but is essential in correctly simulating the cube.
            if(false)
            {
                segments[adjacent[4]].transform.rotation *= Quaternion.Euler(0, 180, 0);
                segments[adjacent[0]].transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
            focusedSegment = adjacent[2];
        }else if (player.transform.position.y + 15 > centerY + faceSize)
        {
            playerY++;
            pivot.targets.x -= 90;
            segments[adjacent[1]].transform.rotation *= Quaternion.Euler(0, -90, 0);
            segments[adjacent[3]].transform.rotation *= Quaternion.Euler(0, 90, 0);
            if (false)
            {
                segments[adjacent[4]].transform.rotation *= Quaternion.Euler(0, 180, 0);
                segments[adjacent[2]].transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
            focusedSegment = adjacent[0];
        }

        float relativeX = player.transform.position.x + 15 - centerX;
        float relativeY = player.transform.position.y + 15 - centerY;
        if (!isLite)
        {
            sc[focusedSegment].isActive = true;
            if (relativeX < 10)
            {
                sc[adjacent[1]].isActive = true;
            }
            if (relativeX > 20)
            {
                sc[adjacent[3]].isActive = true;
            }
            if (relativeY < 10)
            {
                sc[adjacent[2]].isActive = true;
            }
            if (relativeY > 20)
            {
                sc[adjacent[0]].isActive = true;
            }
        }
        else
        {
            foreach(Camera cam in lc)
            {
                cam.enabled = false;
            }
            lc[focusedSegment].enabled = true;
            if (relativeX < 10)
            {
                lc[adjacent[1]].enabled = true;
            }
            if (relativeX > 20)
            {
                lc[adjacent[3]].enabled = true;
            }
            if (relativeY < 10)
            {
                lc[adjacent[2]].enabled = true;
            }
            if (relativeY > 20)
            {
                lc[adjacent[0]].enabled = true;
            }
        }

        //Redefine adjacent because it may have changed
        //this next line may cause a bug
        //adjacent = new int[5] { faceValues[focusedSegment][mod(drift, 4)], faceValues[focusedSegment][mod(1 + drift, 4)], faceValues[focusedSegment][mod(2 + drift, 4)], faceValues[focusedSegment][mod(3 + drift, 4)], faceValues[focusedSegment][4] };
        //Position the levels around the player. Unload the back face by placing it at the origin and lowering the z value so none of the other cameras render it.
        topView.transform.position = new Vector3(centerX, centerY - 15, 10);
        segments[focusedSegment].transform.position = new Vector3(centerX, centerY, 0);
        segments[adjacent[0]].transform.position = new Vector3(centerX, centerY + 30, 0);
        segments[adjacent[1]].transform.position = new Vector3(centerX - 30, centerY, 0);
        segments[adjacent[2]].transform.position = new Vector3(centerX, centerY - 30, 0);
        segments[adjacent[3]].transform.position = new Vector3(centerX + 30, centerY, 0);
        segments[adjacent[4]].transform.position = new Vector3(centerX, centerY - 60, 0);

        portalManager.position = new Vector3(centerX, centerY, 0);
    }
}
