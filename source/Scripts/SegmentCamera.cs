using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCamera : MonoBehaviour
{
    public Camera camera;
    int height = 200;
    int width = 200;
    int depth = 24;
    public Sprite[] sprites = new Sprite[9];
    public Texture2D texture;
    public bool isActive = true;
    RenderTexture renderTexture;

    void Start()
    {
        texture = new Texture2D(width * 3, height * 3, TextureFormat.RGBA32, false);
        camera = GetComponent<Camera>();
        renderTexture = new RenderTexture(width * 3, height * 3, depth);
        camera.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;
        StartCoroutine(Initialize());
    }

    
    WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();

    public IEnumerator Initialize()
    {
        yield return frameEnd;

        //camera.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;
        camera.Render();

        texture.ReadPixels(new Rect(0, 0, width * 3, height * 3), 0, 0);
        texture.Apply();

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Rect rect = new Rect(x * width, y * height, width, height);
                sprites[(2-y) * 3 + 2 - x] = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            }
        }
    }

    public IEnumerator GetSlices()
    {
        yield return frameEnd;

        //camera.targetTexture = renderTexture;

        RenderTexture.active = renderTexture;
        //camera.Render();

        texture.ReadPixels(new Rect(0, 0, width * 3, height * 3), 0, 0);
        texture.Apply();
    }


    //method to render from camera
    
    void Update()
    {
        if (isActive)
        {
            camera.enabled = true;
            StartCoroutine(GetSlices());
        }
        else
        {
            camera.enabled = false;
        }
        isActive = false;
    }
    
}
