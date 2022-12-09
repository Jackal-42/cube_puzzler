using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Editor3 : MonoBehaviour
{
    public EditorSection[] segments = new EditorSection[6];
    private EditorManager em;
    private string[] backups = new string[16];
    private int backupIndex = 0;
    private int backIndex = 0;

    void Start()
    {
        em = GameObject.Find("EditorManager").GetComponent<EditorManager>();
        StartCoroutine(Load());
        backups = em.backupSaves;
        backupIndex = em.backupIndex;
        backIndex = em.backIndex;
        if(backups[0] == "")
        {
            backups[0] = "225a/225a/225a/225a/225a/225a";
            backupIndex++;
        }
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(0.05f);
        LoadLevelCode(em.savedCode);
    }

    public string boolToStr(bool theBool)
    {
        if (theBool)
        {
            return "1";
        }
        return "0";
    }

    public string GetLevelCode()
    {
        string result = "";
        result += boolToStr(em.canJump);
        result += boolToStr(em.canPound);
        result += "|";
        foreach (var segment in segments)
        {
            result += segment.GetLevelData() + "/";
        }
        result = result.Remove(result.Length - 1, 1);
        return result;
    }

    public bool charToBool(char ch)
    {
        if(ch == '1')
        {
            return true;
        }
        return false;
    }

    public void LoadLevelCode(string code)
    {
        if (code.Contains("|"))
        {
            string[] globals = code.Split("|");
            code = globals[1];
            int index = 0;
            foreach(char ch in globals[0])
            {
                if(index == 0)
                {
                    em.canJump = charToBool(ch);
                }else if (index == 1)
                {
                    em.canPound = charToBool(ch);
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


        int faceIndex = 0;
        foreach (string s in splitCode)
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
                    segments[faceIndex].GetComponent<EditorSection>().specificGravity = gravityLookup[Array.IndexOf(gravityIndicators, c_s)];
                    continue;
                }
                if (numbers.Contains(c_s))
                {
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
                    if (finalNum == 0)
                    {
                        finalNum = 1;
                    }
                    for (int i = 0; i < finalNum; i++)
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
            //print(values[0]);
            valueIndex = 0;
            foreach (Transform row in segments[faceIndex].transform)
            {
                if (row.tag != "EditableTile") { continue; }
                foreach (Transform segment in row.transform)
                {
                    foreach (Transform child in segment.transform)
                    {
                        var dt = em.drawableTiles[values[valueIndex]];
                        var et = child.GetComponent<EditorTile>();
                        child.rotation = Quaternion.Euler(0, 0, rots[valueIndex]);
                        child.localRotation = Quaternion.Euler(0, 0, rots[valueIndex]);
                        et.value = values[valueIndex];
                        et.rotation = rots[valueIndex];
                        et.variant = variants[valueIndex];
                        et.sprite.sprite = dt.editorTexture;
                        if (dt.tintable)
                        {
                            et.sprite.color = em.vc[variants[valueIndex]];
                        }
                        
                        valueIndex++;
                    }
                }
            }
            faceIndex++;
        }
    }

    public void CopyToClipboard(string s)
    {
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
    }

    int WrapIndex(int index)
    {
        if(index > 127)
        {
            return 0;
        }
        if(index < 0)
        {
            return 127;
        }
        return index;
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            em.savedCode = GetLevelCode();
            em.backupSaves = backups;
            em.backupIndex = backupIndex;
            em.backIndex = backIndex;
            SceneManager.LoadScene("Main (backup)");
            //print(GetLevelCode());
        }
        if (Input.GetKeyDown("l"))
        {
            em.savedCode = GetLevelCode();
            em.backupSaves = backups;
            em.backupIndex = backupIndex;
            em.backIndex = backIndex;
            SceneManager.LoadScene("Main (backup)");
            //print(GetLevelCode());
        }
        if (Input.GetMouseButtonUp(0))
        {
            string newCode = GetLevelCode();
            if(newCode != backups[WrapIndex(backupIndex - 1)])
            {
                backups[backupIndex] = newCode;
                
                backupIndex = WrapIndex(backupIndex + 1);
                backIndex = WrapIndex(backupIndex + 1);
            }
        }
        if(Input.GetKeyDown("z") && Input.GetKey(KeyCode.LeftControl))
        {
            if(backupIndex - 1 != backIndex && backups[WrapIndex(backupIndex - 1)] != "")
            {
                backupIndex = WrapIndex(backupIndex - 1);
                LoadLevelCode(backups[WrapIndex(backupIndex - 1)]);
            }
        }
        if (Input.GetKeyDown("y") && Input.GetKey(KeyCode.LeftControl))
        {
            if (backupIndex + 1 != backIndex && backups[backupIndex] != "")
            {
                LoadLevelCode(backups[backupIndex]);
                backupIndex = WrapIndex(backupIndex + 1);
            }
        }
        /*
        if (Input.GetKeyDown("l"))
        {
            //string loadable = em.gameObject.transform.GetChild(0).GetChild(1).GetComponent<InputFinder>().value;
            string loadable = GameObject.Find("CopyPasteObject").GetComponent<CopyPasteObjectScript>().sometext;
            LoadLevelCode(loadable);
        }
        
        if (Input.GetKeyDown("c"))
        {
            print(GetLevelCode());
        }
        */
    }
}
