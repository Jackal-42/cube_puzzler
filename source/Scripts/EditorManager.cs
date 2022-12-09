using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DrawableTile
{
    public string name;
    public Sprite editorTexture;
    public GameObject tile;
    public bool rotatable;
    public bool variantable;
    public bool tintable;
    public bool global;
}

public class EditorManager : MonoBehaviour
{
    public string[] stringValues = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    public DrawableTile[] drawableTiles;
    public int tileIndex = 0;
    public int rotation = 0;
    public int variant = 0;
    public bool canJump = true;
    public bool canPound = true;
    [SerializeField]
    public Color[] vc = new Color[4];
    public string savedCode = "225a/225a/225a/225a/225a/225a";
    public string[] backupSaves = new string[128];
    public int backupIndex = 0;
    public int backIndex = 0;

    private float sliderValue = 1080;

    public float GUIScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    void OnGUI()
    {
        if (SceneManager.GetActiveScene().name == "Editor")
        {
            int index = 0;
            var centeredStyle = GUI.skin.GetStyle("Label");

            centeredStyle.alignment = TextAnchor.UpperCenter;
            centeredStyle.wordWrap = true;
            centeredStyle.fontSize = (int)(18 * GUIScale);


            float width = 60 * GUIScale;
            float spacing = 120 * GUIScale;
            Color savedColor = GUI.backgroundColor;


            Rect globalRect = new Rect(Screen.width - 100, 8, 100, 20);
            canJump = (GUI.Toggle(globalRect, canJump, "Can Jump?"));
            globalRect.y += 24;
            canPound = (GUI.Toggle(globalRect, canPound, "Can Pound?"));
            /*
            globalRect.y += 24;
            sliderValue = GUI.HorizontalSlider(globalRect, sliderValue, 108, 1080);
            Screen.SetResolution((int)sliderValue, (int)(sliderValue/1.5), false);
            */


            foreach (Color col in vc)
            {
                GUI.backgroundColor = col;
                Rect rect = new Rect(10 + (index % 4) * (width + 10), 30 * GUIScale + width + Mathf.Floor(index / 4) * spacing, width, width);
                if (GUI.Button(rect, index.ToString()))
                {
                    variant = index;
                }
                index++;
            }
            GUI.backgroundColor = savedColor;
            index = 0;
            foreach (DrawableTile tile in drawableTiles)
            {
                Rect rect = new Rect(10 + (index % 4) * (width + 10), 40 * GUIScale + width * 2 + Mathf.Floor(index / 4) * spacing * 0.9f, width, width);
                GUI.DrawTexture(rect, tile.editorTexture.texture);
                if (GUI.Button(rect, ""))
                {
                    SwapToTile(index);
                }

                rect.y += width;
                GUI.Label(rect, tile.name, centeredStyle);
                index++;
            }

        }
    }

    void SwapToTile(int value)
    {
        rotation = 0;
        tileIndex = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("="))
        {
            GUIScale += 0.1f;
        }
        if (Input.GetKeyDown("-"))
        {
            GUIScale -= 0.1f;
        }
        if (Input.GetKeyDown("r") && SceneManager.GetActiveScene().name == "Editor")
        {
            rotation += 90;
            if (rotation == 360)
            {
                rotation = 0;
            }
        }
        if (Input.GetKeyDown("e"))
        {
            SwapToTile(0);
        }

        if (Input.GetKeyDown("1"))
        {
            SwapToTile(0);
        }
        if (Input.GetKeyDown("2"))
        {
            SwapToTile(1);
        }
        if (Input.GetKeyDown("3"))
        {
            SwapToTile(2);
        }
        if (Input.GetKeyDown("4"))
        {
            SwapToTile(3);
        }
        if (Input.GetKeyDown("5"))
        {
            SwapToTile(4);
        }
        if (Input.GetKeyDown("6"))
        {
            SwapToTile(5);
        }
        if (Input.GetKeyDown("7"))
        {
            SwapToTile(6);
        }
        if (Input.GetKeyDown("8"))
        {
            SwapToTile(7);
        }
    }
}
