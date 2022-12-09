using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCamera : MonoBehaviour
{
    public Transform target;
    public GameObject player;
    public LevelManager lm;
    public float distance = 30;
    public float intensity = 1;
    public float transition = 3;

    private CameraPivot pivot;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private Camera cam;
    Transform targets;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, target.position.z + distance);

        transform.LookAt(target);
        cam = GetComponent<Camera>();
        pivot = transform.parent.GetComponent<CameraPivot>();
        targets = new GameObject().transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            targets.rotation = transform.rotation;
            if (lm.isLite)
            {
                foreach (Camera cam in lm.lc)
                {
                    cam.Render();
                }
            }
            else
            {
                foreach (SegmentCamera seg in lm.sc)
                {
                    seg.isActive = true;
                }
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            targets.position = target.position;
            targets.Rotate(targets.rotation * new Vector3(1, 0, 0), rotationAroundXAxis, Space.World);
            targets.Rotate(targets.rotation * new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            targets.Translate(new Vector3(0, 0, -distance * 1.4f));

            previousPosition = newPosition;
            if (transition < 16)
            {
                transition += 0.5f;
            }
            transform.position = Vector3.Lerp(transform.position, targets.position, Time.deltaTime * transition);
            transform.rotation = Quaternion.Lerp(transform.rotation, targets.rotation, Time.deltaTime * transition);
        }
        else
        {
            transition = 3;
            targets.position = new Vector3(((float)lm.playerX * lm.faceSize - player.transform.position.x) * -1 * intensity, ((float)lm.playerY * lm.faceSize - player.transform.position.y) * -1 * intensity, distance);
            targets.rotation = Quaternion.Euler(targets.position.y * intensity * 4, targets.position.x * intensity * 4 + 180, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targets.position, Time.deltaTime * transition);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targets.rotation, Time.deltaTime * transition);
        }
        //transform.LookAt(transform.parent);
    }
}
