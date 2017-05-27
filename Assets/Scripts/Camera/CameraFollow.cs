using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;
    private bool debug = false;
    private Camera cam;

    void OnPreRender()
    {
        if (debug)
        {
            GL.wireframe = true;
        }
    }

	void OnPostRender()
    {
        GL.wireframe = false;
    }

    void Start()
    {
        offset = transform.position - target.position;
        cam = this.gameObject.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F11))
        {
            debug = !debug;
            if (debug)
            {
                cam.clearFlags = CameraClearFlags.SolidColor;
                           }
            if (!debug)
            {
                cam.clearFlags = CameraClearFlags.Skybox;
            }
        }
    }
 }
