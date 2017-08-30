using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeTrigger : MonoBehaviour {
    public Vector3 cameraPosition;
    public CameraFollow.CameraMode mode;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (mode == CameraFollow.CameraMode.FOLLOW) Camera.main.GetComponent<CameraFollow>().SetCameraModeFollow();
            else if (mode == CameraFollow.CameraMode.FIXED) Camera.main.GetComponent<CameraFollow>().SetCameraModeFixed(cameraPosition);
            else if (mode == CameraFollow.CameraMode.TOPDOWN) Camera.main.GetComponent<CameraFollow>().SetCameraModeTopdown();
        }
    }
}
