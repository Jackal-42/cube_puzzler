using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorText : MonoBehaviour
{
    private TextMeshProUGUI textmesh;
    public EditorManager em;
    // Start is called before the first frame update
    void Start()
    {
        em = GameObject.Find("EditorManager").GetComponent<EditorManager>();
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textmesh.SetText("Tile Selected: " + em.drawableTiles[em.tileIndex].name);
    }
}
