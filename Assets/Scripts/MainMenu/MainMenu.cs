using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int buttonIndex = 0;
	private Canvas TitleCanvas, CanvasHelp, CanvasCredit;
	// Use this for initialization
	void Start () {
		CanvasHelp = GameObject.Find ("CanvasHelp").GetComponent<Canvas>();
		TitleCanvas = GameObject.Find ("TitleCanvas").GetComponent<Canvas>();
		CanvasCredit = GameObject.Find ("CanvasCredit").GetComponent<Canvas>();
		CanvasHelp.enabled = false;
		TitleCanvas.enabled = true;
		CanvasCredit.enabled = false;
    }

    public void buttonWasClicked()
    {
        if (buttonIndex == 0)// New Game
        {
            SceneManager.LoadScene("LevelStomach", LoadSceneMode.Single);
        }
        else if (buttonIndex == 1) // Help
        {
			CanvasHelp.enabled = true;
			TitleCanvas.enabled = false;
			CanvasCredit.enabled = false;
			Debug.Log ("Deactive");
        }
        else if (buttonIndex == 2) // Credits
        {
			CanvasHelp.enabled = false;
			TitleCanvas.enabled = false;
			CanvasCredit.enabled = true;
        }
		else if (buttonIndex == 3) // Quit
		{
			Application.Quit();
		}
    }
	public void backToMain(){
		CanvasHelp.enabled = false;
		TitleCanvas.enabled = true;
		CanvasCredit.enabled = false;
	}
    // Update is called once per frame
    void Update () {

    }
}
