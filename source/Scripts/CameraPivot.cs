using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Vector3 targets = new Vector3 ( 0, 0, 0 );
    public Quaternion targetRot;
    // Start is called before the first frame update
    void Start()
    {
        targetRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = transform.rotation;
        transform.rotation = targetRot;
        transform.Rotate(targets);
        targetRot = transform.rotation;
        transform.rotation = rot;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 4);

        targets = new Vector3(0, 0, 0);
    }
}
