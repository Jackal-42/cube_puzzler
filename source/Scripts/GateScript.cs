using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public int environmentValue = 0;
    public bool inverted = false;
    public Environment env;
    public Sprite active;
    public Sprite inactive;
    private Collider2D m_col;
    private SpriteRenderer m_sr;

    void Start()
    {
        env = GameObject.Find("Environment").GetComponent<Environment>();
        m_col = GetComponent<Collider2D>();
        m_sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (env.values[environmentValue] == inverted)
        {
            m_col.enabled = true;
            m_sr.sprite = active;
        }
        else
        {
            m_col.enabled = false;
            m_sr.sprite = inactive;
        }
    }
}
