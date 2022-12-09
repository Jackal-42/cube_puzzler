using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    Vector2 force = new Vector2(0, -0.5f);
    public int value = 0;

    void Start()
    {
        float sin = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad * -1);
        float cos = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad * -1);
        force = new Vector2(force.x * cos - (((float)(value - 3) / 8) - 0.125f) * sin, force.x * sin + (((float)(value - 3) / 8) - 0.125f) * cos);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PhysicsObject po;
        if (po = col.GetComponent<PhysicsObject>())
        {
            po.velocity = force;
            po.noPushTimer = 8;
            col.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
}
