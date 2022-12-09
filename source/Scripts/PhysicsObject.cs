using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector2 gravity = new Vector2();
    public Vector2 velocity = new Vector2();
    public Transform attatched;
    public float maxVelocity = 0.5f;
    public float weight = 1;

    public int noPushTimer = 0;

    public int timeSinceGround = 0;

    public int value = 0;

    public Rigidbody2D m_rb;

    void Start()
    {
        GravityField gf;
        if (gf = transform.parent.GetComponent<GravityField>())
        {
            gravity = gf.gravity;
        }
        maxVelocity = 0.3f + ((float)value + 0.01f) / 10;
        weight = 0.5f + ((float)value + 0.01f) / 4;
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.useFullKinematicContacts = true;
    }

    float CapValue(float value, float max)
    {
        if(Mathf.Abs(value) > max)
        {
            return max * Mathf.Sign(value);
        }
        return value;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(Mathf.Abs(velocity.x) > 0.1 && (col.gameObject.tag == "Ground" || col.gameObject.tag == "NoPoundGround" || col.gameObject.tag == "Platform"))
        {
            velocity = new Vector2(0, velocity.y);
            m_rb.velocity = Vector3.zero;
        }
        if(col.gameObject.tag == "Player" && col.rigidbody.mass > 2)
        {
            velocity = Vector2.zero;
            m_rb.velocity = Vector3.zero;
        }
        timeSinceGround = 2;
    }

    void FixedUpdate()
    {
        if(noPushTimer > 0)
        {
            noPushTimer--;
            m_rb.velocity = Vector3.zero;
        }
        velocity = new Vector2(CapValue(velocity.x + (gravity.x * weight) / 100, maxVelocity), CapValue(velocity.y + (gravity.y * weight) / -100, maxVelocity));
        Vector2 savedCoords = new Vector2(transform.position.x, transform.position.y);
        transform.localPosition = new Vector3(transform.localPosition.x + velocity.x, transform.localPosition.y, transform.localPosition.z + velocity.y);

        if (timeSinceGround > 0)
        {
            timeSinceGround--;
        }

        if(timeSinceGround == 0 && attatched != null)
        {
            attatched.position = new Vector3(attatched.position.x + transform.position.x - savedCoords.x, attatched.position.y + transform.position.y - savedCoords.y, attatched.position.z);
        }
    }
}
