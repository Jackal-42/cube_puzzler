using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject phantom;
    public Portal partner;
    public bool shiftedRight = false;

    public Transform host;

    public bool acceptPlayer = false;
    public int value = -1;
    public SpriteRenderer phantomSprite;

    private bool justWarped = false;
    // Start is called before the first frame update
    void Start()
    {
        phantomSprite = phantom.GetComponent<SpriteRenderer>();
        if (value >= 0 && partner == null)
        {
            host = transform.parent.parent.parent;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        bool isPhysics = col.GetComponent<PhysicsObject>();
        bool isPlayer = col.GetComponent<PlayerController>();
        if (isPhysics || isPlayer)
        {
            if (!justWarped)
            {
                phantomSprite.sprite = null;
            }
           
            partner.phantomSprite.sprite = null;
        }
    }

    void Warp(Collider2D col, bool isPhysics)
    {
        float sin = Mathf.Sin((partner.transform.eulerAngles.z - transform.eulerAngles.z) * Mathf.Deg2Rad * -1);
        float cos = Mathf.Cos((partner.transform.eulerAngles.z - transform.eulerAngles.z) * Mathf.Deg2Rad * -1);

        col.transform.position = partner.phantom.transform.position;

        if (isPhysics)
        {
            col.transform.rotation = Quaternion.Euler(new Vector3(0, 0, partner.host.transform.eulerAngles.z - col.transform.parent.eulerAngles.z + col.transform.eulerAngles.z));
            col.transform.parent = partner.host;
            GravityField gf;
            PhysicsObject po = col.GetComponent<PhysicsObject>();
            if (gf = partner.host.GetComponent<GravityField>())
            {
                po.gravity = gf.gravity;
            }
            
            Vector2 vel = po.velocity;
            po.velocity = new Vector2(vel.x * cos - vel.y * sin, vel.x * sin + vel.y * cos);
        }
        else
        {
            var rb = col.GetComponent<Rigidbody2D>();
            Vector3 vel = rb.velocity;
            rb.velocity = new Vector3(vel.x * cos - vel.y * sin, (vel.x * sin + vel.y * cos), 0);
            if(rb.velocity.y > 0 && rb.velocity.y < 25 && (partner.transform.eulerAngles.z == 90 || partner.transform.eulerAngles.z == 270))
            {
                rb.velocity = new Vector3(rb.velocity.x, 25, 0);
            }
        }
        justWarped = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (value >= 0 && partner == null)
        {
            Portal[] others = (Portal[])GameObject.FindObjectsOfType(typeof(Portal));
            foreach (Portal portal in others)
            {
                if (portal.value == value && portal.shiftedRight != shiftedRight)
                {
                    partner = portal;
                    partner.partner = this;
                }
            }
        }

        justWarped = false;
        bool isPhysics = col.GetComponent<PhysicsObject>();
        if (isPhysics && col.transform.parent != host) { return; }

        bool isPlayer = col.GetComponent<PlayerController>();

        if (isPhysics || (isPlayer && acceptPlayer))
        {
            var trans = col.transform.position;
            var theSprite = col.GetComponent<SpriteRenderer>().sprite;

            float sin = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad * -1);
            float cos = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad * -1);
            float px = (trans.x - transform.position.x);
            float py = (trans.y - transform.position.y);

            Vector3 phantomPos = new Vector3(px*cos - py*sin, px*sin + py*cos, 0);
            phantom.transform.localPosition = phantomPos;
            phantomSprite.sprite = theSprite;
            partner.phantom.transform.localPosition = phantomPos;
            partner.phantomSprite.sprite = theSprite;
            if (isPhysics)
            {
                partner.phantom.transform.rotation = Quaternion.Euler(new Vector3(0, 0, partner.host.transform.eulerAngles.z - col.transform.parent.eulerAngles.z + col.transform.eulerAngles.z));
            }
            else
            {
                partner.phantom.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            if (shiftedRight && phantomPos.x < 0)
            {
                Warp(col, isPhysics);
                //col.transform.position = new Vector3(trans.x + difference.x, trans.y + difference.y, partner.transform.position.z);
            }
            else if (!shiftedRight && phantomPos.x > 0)
            {
                Warp(col, isPhysics);
                //col.transform.position = new Vector3(trans.x + difference.x, trans.y + difference.y, partner.transform.position.z);
            }

            /*
            if (isPhysics)
            {
                
            }
            */
        }
    }
}
