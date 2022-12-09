using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFinder : MonoBehaviour
{
    public TMP_InputField input;
    public string value;
    void Start()
    {
        input = GetComponent<TMP_InputField>();
    }
    

    void Update()
    {
        value = input.text;
    }
}
