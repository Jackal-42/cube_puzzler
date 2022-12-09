using UnityEngine;
using System.Runtime.InteropServices;

public class CopyPasteObjectScript : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void PasteHereWindow(string gettext);

    public string sometext = " xxxxxx ";
    private EditorManager em;

    void Start()
    {
        em = GameObject.Find("EditorManager").GetComponent<EditorManager>();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 8 * em.GUIScale, 200 * em.GUIScale, 50 * em.GUIScale), "Selected Tile: " + em.drawableTiles[em.tileIndex].name);
        GUI.Label(new Rect(10, 30 * em.GUIScale, 200 * em.GUIScale, 50 * em.GUIScale), "Selected Variant: " + em.variant.ToString());
        if (GUI.Button(new Rect(10, 54 * em.GUIScale, 150 * em.GUIScale, 30 * em.GUIScale), "Level Code"))
        {
            //print(GameObject.Find("Editor3").GetComponent<Editor3>().GetLevelCode());
            PasteHereWindow(GameObject.Find("Editor3").GetComponent<Editor3>().GetLevelCode());
        }
    }

    public void GetPastedText(string newpastedtext)
    {
        sometext = newpastedtext;
        GameObject.Find("Editor3").GetComponent<Editor3>().LoadLevelCode(sometext);
    }
}