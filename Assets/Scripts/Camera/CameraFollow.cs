using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;
    private bool debug = false;
    private Camera cam;

    public enum CameraMode {
        FOLLOW,
        FIXED,
        TOPDOWN
    };
    private CameraMode cameraMode;
    private Vector3 fixedPosition;
    private Quaternion originalRotation;

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
        cameraMode = CameraMode.FOLLOW;
        fixedPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (cameraMode == CameraMode.FOLLOW)
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = originalRotation;
        }
        else if (cameraMode == CameraMode.FIXED)
        {
            transform.position = Vector3.Lerp(transform.position, fixedPosition, smoothing * Time.deltaTime);
            transform.rotation = originalRotation; // ?
        }
        else if (cameraMode == CameraMode.TOPDOWN)
        {
            // ---
        }

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

    public void SetCameraModeFollow()
    {
        cameraMode = CameraMode.FOLLOW;
    }

    public void SetCameraModeFixed(Vector3 position)
    {
        cameraMode = CameraMode.FIXED;
        fixedPosition = position;
    }

    public void SetCameraModeTopdown()
    {
        cameraMode = CameraMode.TOPDOWN;
        // ---
    }
}
