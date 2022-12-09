using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Goal : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void YouWin();

    public bool requireGrounded = true;

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController pc;
        if (pc = other.gameObject.GetComponent<PlayerController>())
        {
            if (pc.jumpBox.active || !requireGrounded)
            {
                YouWin();
                //print("You Win!");
                Destroy(gameObject);
            }
        }
    }
}
