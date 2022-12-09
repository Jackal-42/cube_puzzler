using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentModifier : MonoBehaviour
{
    private LevelManager lm;

    public int inner;
    public int outer;

    void Start()
    {
        lm = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(0.5f);

        print(lm.segments[lm.adjacent[inner]].transform.eulerAngles.y.ToString());
    }

    void ChangeParent(Collider2D col, GameObject targetSegment)
    {
        float sin2 = Mathf.Sin((targetSegment.transform.eulerAngles.y - col.transform.parent.eulerAngles.y) * Mathf.Deg2Rad);
        float cos2 = Mathf.Cos((targetSegment.transform.eulerAngles.y - col.transform.parent.eulerAngles.y) * Mathf.Deg2Rad);

        col.transform.parent = targetSegment.transform;

        
        PhysicsObject po = col.GetComponent<PhysicsObject>();

        Vector2 vel = po.velocity;
        po.velocity = new Vector2(vel.x * cos2 - vel.y * sin2, vel.x * sin2 + vel.y * cos2);
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        bool isPhysics = col.GetComponent<PhysicsObject>();

        if (isPhysics)
        {
            var trans = col.transform.position;

            float sin = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad * -1);
            float cos = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad * -1);

            float px = (trans.x - transform.position.x);
            float py = (trans.y - transform.position.y);

            GameObject targetSegment;
            //If the object with the collider is past the halfway point
            if(px * cos - py * sin < 0)
            {
                targetSegment = lm.segments[lm.adjacent[outer]];
                ChangeParent(col, targetSegment);
            }
            else
            {
                targetSegment = lm.segments[lm.adjacent[inner]];
                ChangeParent(col, targetSegment);
            }

            GravityField gf;
            if (gf = targetSegment.GetComponent<GravityField>())
            {
                col.GetComponent<PhysicsObject>().gravity = gf.gravity;
            }
        }
    }
}
