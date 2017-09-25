using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public int buttonIndex = 0;
	public AudioClip clickSound, overSound;
	private AudioSource sourceClick, sourceOver;
	private AudioSource [] sounds;
	// Use this for initialization
	void Start () {
		gameObject.AddComponent<AudioSource> ();
		gameObject.AddComponent<AudioSource> ();

		sounds = GetComponents<AudioSource> ();

		sourceClick = sounds [0];
		sourceOver = sounds [1];

		sourceClick.clip = clickSound;
		sourceClick.playOnAwake = false;
		sourceOver.clip = overSound;
		sourceOver.playOnAwake = false;

		sourceOver.volume = 0.2f;
		sourceOver.pitch = 2;

		sourceClick.volume = 1.0f;
		sourceClick.pitch = 3;

    }

    public void buttonWasClicked()
    {
		sourceClick.PlayOneShot (clickSound);
		
        if (buttonIndex == 0) // Back To Main Menu
        {
            SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
        }
        else if (buttonIndex == 1) // Quit
        {
            Application.Quit();
        }
		if (buttonIndex == 2) // Back To Level 1
		{
			SceneManager.LoadScene("Level1", LoadSceneMode.Single);
		}
    }

	public void buttonOver ()
	{
		sourceOver.PlayOneShot (overSound);
	}
}
