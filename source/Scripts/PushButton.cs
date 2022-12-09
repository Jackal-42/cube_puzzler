using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public int environmentValue = 0;
    public Environment env;

    public bool hold = false;

    private int held = 0;

    void Start()
    {
        env = GameObject.Find("Environment").GetComponent<Environment>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.GetComponent<PlayerController>() || other.gameObject.GetComponent<PhysicsObject>()))
        {
            if(held == 0)
            {
                env.values[environmentValue] = !env.values[environmentValue];
            }
            if (hold)
            {
                held++;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!hold) { return; }
        if ((other.gameObject.GetComponent<PlayerController>() || other.gameObject.GetComponent<PhysicsObject>()) && held == 1)
        {
            if(held == 1)
            {
                env.values[environmentValue] = !env.values[environmentValue];
            }
            
            held--;
        }
        
    }
}
