using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    public Camera gameCamera;
    public Camera freeCamera;
    public Camera fixedCamera1;

    private int currentCamera = 0;
    // public Camera fixedCamera2;
    // ...

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1))
        {
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
            }
            currentCamera = 0;
            gameCamera.enabled = true;
            freeCamera.enabled = false;
            fixedCamera1.enabled = false;

        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
            currentCamera = 1;
            freeCamera.transform.position = gameCamera.transform.position;
            freeCamera.transform.rotation = gameCamera.transform.rotation;
            gameCamera.enabled = false;
            freeCamera.enabled = true;
            fixedCamera1.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
            currentCamera = 2;
            gameCamera.enabled = false;
            freeCamera.enabled = false;
            fixedCamera1.enabled = true;

        }
    }
}
