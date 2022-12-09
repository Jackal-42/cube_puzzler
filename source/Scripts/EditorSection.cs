using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSection : MonoBehaviour
{
    private int currentValue = 0;
    private int currentRot = 0;
    private int currentVariant;
    private int consecutive = 0;
    private string[] stringValues;
    private string[] rotIndicators = new string[4] {"", "!", "@", "#"};
    private string[] variantIndicators = new string[4] { "", "$", "%", "^" };
    private Vector2[] gravityLookup = new Vector2[4] {new Vector2(0, -1),new Vector2(1, 0),new Vector2(0, 1),new Vector2(-1, 0)};
    private string[] gravityIndicators = new string[4] { "{", "}", "[", "]" };

    public Vector2 specificGravity = new Vector2(0, -1);
    public GameObject indicator;

    void Start()
    {
        stringValues = GameObject.Find("EditorManager").GetComponent<EditorManager>().stringValues;
    }

    //fix indicator rotation
    public void Rotate()
    {
        specificGravity = new Vector2(specificGravity.y * -1, specificGravity.x);
        indicator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(specificGravity.y, specificGravity.x * -1) * Mathf.Rad2Deg) + 90));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            indicator.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(specificGravity.y, specificGravity.x * -1) * Mathf.Rad2Deg) + 90));
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    public string GetLevelData()
    {
        currentValue = -1;
        currentRot = 0;
        currentVariant = 0;
        consecutive = 0;
        string result = "";

        int index = 0;
        foreach(Vector2 v2 in gravityLookup)
        {
            if(v2 == specificGravity)
            {
                result += gravityIndicators[index];
                break;
            }
            index++;
        }

        foreach(Transform row in transform)
        {
            if(row.tag != "EditableTile") { continue; }
            foreach(Transform segment in row.transform)
            {
                foreach(Transform child in segment.transform)
                {
                    var et = child.GetComponent<EditorTile>();
                    int value = et.value;
                    int rot = et.rotation;
                    int variant = et.variant;
                    if (value != currentValue || rot != currentRot || variant != currentVariant)
                    {
                        if(currentValue != -1)
                        {
                            if(consecutive > 1)
                            {
                                result += variantIndicators[currentVariant] + rotIndicators[currentRot / 90] + consecutive.ToString() + stringValues[currentValue];
                            }
                            else
                            {
                                result += variantIndicators[currentVariant] + rotIndicators[currentRot / 90] + stringValues[currentValue];
                            }
                        }
                        currentValue = value;
                        currentRot = rot;
                        currentVariant = variant;
                        consecutive = 1;
                    }
                    else
                    {
                        consecutive++;
                    }
                    //result += stringValues[child.GetComponent<EditorTile>().value + 1];
                }
            }
        }
        if (consecutive > 1)
        {
            result += variantIndicators[currentVariant] + rotIndicators[currentRot / 90] + consecutive.ToString() + stringValues[currentValue];
        }
        else
        {
            result += variantIndicators[currentVariant] + rotIndicators[currentRot / 90] + stringValues[currentValue];
        }
        return result;
    }
}
