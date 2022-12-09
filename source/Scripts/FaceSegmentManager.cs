using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSegmentManager : MonoBehaviour
{
    public SpriteRenderer[] faceSegments = new SpriteRenderer[9];
    public SegmentCamera sc;
    // Start is called before the first frame update
    void Update()
    {
        for(int i = 0; i < faceSegments.Length; i++)
        {
            faceSegments[i].sprite = sc.sprites[i];
        }
    }
}
