using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private Canvas canvas;

    private bool paused = false;

    void handlePause()
    {
        if (paused)
        {
            paused = false;
            canvas.enabled = false;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
                Time.timeScale = 1.0f;
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
                Time.timeScale = 0.0f;
            }
        }
    }

    public void OnPressResume()
    {
        handlePause();
    }

    public void OnPressBackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
        Destroy(this);
    }

    // Use this for initialization
    void Start () {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.P))
        {
            handlePause();
        }
	}
}
