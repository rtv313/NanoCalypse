using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int buttonIndex = 0;
	public AudioClip clickSound, overSound;
	private Canvas TitleCanvas, CanvasHelp, CanvasCredit;
	private AudioSource sourceClick { get { return GetComponent<AudioSource> (); } }
	private AudioSource sourceOver { get { return GetComponent<AudioSource> (); } }
	// Use this for initialization
	void Start () {
		CanvasHelp = GameObject.Find ("CanvasHelp").GetComponent<Canvas>();
		TitleCanvas = GameObject.Find ("TitleCanvas").GetComponent<Canvas>();
		CanvasCredit = GameObject.Find ("CanvasCredit").GetComponent<Canvas>();
		CanvasHelp.enabled = false;
		TitleCanvas.enabled = true;
		CanvasCredit.enabled = false;
		gameObject.AddComponent<AudioSource> ();
		sourceClick.clip = clickSound;
		sourceClick.playOnAwake = false;
		gameObject.AddComponent<AudioSource> ();
		sourceOver.clip = overSound;
		sourceOver.playOnAwake = false;
		sourceOver.volume = 0.2f;
		sourceOver.pitch = 2;

    }

    public void buttonWasClicked()
    {
		sourceClick.PlayOneShot (clickSound);
		
        if (buttonIndex == 0)// New Game
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
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
		sourceClick.PlayOneShot (clickSound);
		CanvasHelp.enabled = false;
		TitleCanvas.enabled = true;
		CanvasCredit.enabled = false;
	}
	public void buttonOver ()
	{
		sourceOver.PlayOneShot (overSound);
	}
}
