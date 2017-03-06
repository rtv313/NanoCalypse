using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int buttonIndex = 0;

	// Use this for initialization
	void Start () {
		
	}

    void onClick()
    {
        if (buttonIndex == 0)
        {
            SceneManager.LoadScene("LevelStomach", LoadSceneMode.Single);
        }
        else if (buttonIndex == 1)
        {
            Debug.Log("About pressed.");
        }
        else if (buttonIndex == 2)
        {
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update () {

    }
}
