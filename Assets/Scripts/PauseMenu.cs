using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    private Canvas canvas;

    private bool paused = false;

	// Use this for initialization
	void Start () {
        canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.P))
        {
            if (paused)
            {
                paused = false;
                canvas.enabled = false;
                Object[] objects = FindObjectsOfType(typeof(GameObject));
                foreach (GameObject go in objects)
                {
                    go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                paused = true;
                canvas.enabled = true;
                Object[] objects = FindObjectsOfType(typeof(GameObject));
                foreach (GameObject go in objects)
                {
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
	}
}
