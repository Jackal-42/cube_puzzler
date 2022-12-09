using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFaceManager : MonoBehaviour
{
    public GameObject[] tiles = new GameObject[9];
    private int tileIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "LevelTile")
            {
                tiles[tileIndex] = child.gameObject;
                tileIndex++;
            }
        }
    }

    /*

    // Update is called once per frame
    void Update()
    {
        
    }

    */
}
