using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public bool active = false;
    public bool canPound = false;
    public bool onlyGround = false;
    public GameObject storedCol;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "NoPoundGround")
        {
            Collide(other);
        }
        else if (other.tag == "Platform" && Mathf.Abs(other.transform.eulerAngles.z) < 1)
        {
            Collide(other);
        }
        else if (other.tag == "Ground")
        {
            Collide(other);
            canPound = true;
        }else if (!onlyGround)
        {
            Collide(other);
        }
    }

    void Collide(Collider2D other)
    {
        active = true;

        if (storedCol != null)
        {
            storedCol.GetComponent<PhysicsObject>().attatched = null;
            storedCol = null;
        }

        PhysicsObject po;

        if (po = other.GetComponent<PhysicsObject>())
        {
            storedCol = other.gameObject;
            po.attatched = transform.parent;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        active = false;
        canPound = false;
        StartCoroutine(Unattatch());
    }

    IEnumerator Unattatch()
    {
        yield return new WaitForSeconds(0.05f);

        if (!active)
        {

            if (storedCol != null)
            {
                storedCol.GetComponent<PhysicsObject>().attatched = null;
                storedCol = null;
            }
        }
    }
}
