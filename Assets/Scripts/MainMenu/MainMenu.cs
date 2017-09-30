using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public int buttonIndex = 0;
	public AudioClip clickSound, overSound;
	private Canvas TitleCanvas, CanvasHelp, CanvasCredit,CanvasControls, VideoCanvas;
	private AudioSource sourceClick, sourceOver;
	private AudioSource [] sounds;
	private camera_travelling camtravel;
	private MeshRenderer plane_bacteria, plane_virus, plane_parasite;
	private VideoPlayer video_plane;
	private manage_level_load levelManagement;
	private bool playing_video;

	// Use this for initialization
	void Start () {
		CanvasHelp = GameObject.Find ("CanvasHelp").GetComponent<Canvas>();
		TitleCanvas = GameObject.Find ("TitleCanvas").GetComponent<Canvas>();
		CanvasCredit = GameObject.Find ("CanvasCredit").GetComponent<Canvas>();
		CanvasControls = GameObject.Find ("CanvasControls").GetComponent<Canvas>();
		VideoCanvas = GameObject.Find ("Video_canvas").GetComponent<Canvas>();

		plane_bacteria = GameObject.Find ("Plane_bacteria").GetComponent<MeshRenderer> ();
		plane_virus = GameObject.Find ("Plane_virus").GetComponent<MeshRenderer> ();
		plane_parasite = GameObject.Find ("Plane_parasite").GetComponent<MeshRenderer> ();

		video_plane = GameObject.Find ("video_plane").GetComponent<VideoPlayer> ();
		playing_video = false;

		if(GameObject.Find ("Main Camera").GetComponent<camera_travelling>() != null)
			camtravel = GameObject.Find ("Main Camera").GetComponent<camera_travelling>();

		levelManagement = GameObject.Find ("Video_canvas").GetComponent<manage_level_load> ();
		
		deactivateHelpPlanes ();
		CanvasHelp.enabled = false;
		TitleCanvas.enabled = true;
		CanvasCredit.enabled = false;
		CanvasControls.enabled = false;
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
	void Update () {
		if (playing_video) {
			if (!video_plane.isPlaying) {
				levelManagement.setActivation (true);
			} else if (levelManagement.checkProgress() == 0.9f) {
				GameObject.Find ("skipVideo").GetComponent<Image> ().enabled = true;
			}
		}
	}
    public void buttonWasClicked()
    {
		sourceClick.PlayOneShot (clickSound);
		
        if (buttonIndex == 0)// New Game
        {
            //SceneManager.LoadScene("Level1", LoadSceneMode.Single);
			GameObject.Find ("Main Camera").GetComponent<AudioSource>().Stop();
			video_plane.Play();
			VideoCanvas.enabled = true;
			GameObject.Find ("skipVideo").GetComponent<Image> ().enabled = false;
			playing_video = true;
			levelManagement.startLoad ();
        }
        else if (buttonIndex == 1) // Help
        {
			
			TitleCanvas.enabled = false;
			CanvasCredit.enabled = false;
			CanvasControls.enabled = false;
			camtravel.moveCameraToScreen ();
			Invoke ("activeHelp", camtravel.getTransitionDuration ());
        }
        else if (buttonIndex == 2) // Credits
        {
			CanvasHelp.gameObject.SetActive (false);
			TitleCanvas.enabled = false;
			CanvasControls.enabled = false;
			camtravel.moveCameraToScreen ();
			Invoke ("activeCredit", camtravel.getTransitionDuration ());
        }
		else if (buttonIndex == 3) // Quit
		{
			Application.Quit();
		}
        else if (buttonIndex == 4) // Back To Main Menu
        {
            SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);

        }
		else if (buttonIndex == 5) // Credits
		{
			CanvasHelp.gameObject.SetActive (false);
			TitleCanvas.enabled = false;
			CanvasCredit.enabled = false;
			camtravel.moveCameraToScreen ();
			Invoke ("activeControls", camtravel.getTransitionDuration ());
		}else if (buttonIndex == 6) // Skip Video
		{
			GameObject.Find ("Video_canvas").GetComponent<manage_level_load> ().setActivation (true);

		}
    }
	public void backToMain(){
		sourceClick.PlayOneShot (clickSound);
		CanvasHelp.gameObject.SetActive (false);
		CanvasCredit.enabled = false;
		CanvasControls.enabled = false;
		camtravel.moveCameraToTable ();
		Invoke ("activeTitle", camtravel.getTransitionDuration ());
	}
	public void buttonOver ()
	{
		if (gameObject.GetComponent<Image> ().enabled) {
			sourceOver.PlayOneShot (overSound);
			gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (160f, 28f);
		}
	}
	public void buttonOut(){
		if (gameObject.GetComponent<Image> ().enabled) {
			gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (160f, 30f);
		}
	}

	private void activeTitle(){
		TitleCanvas.enabled = true;
	}
	private void activeCredit(){
		CanvasCredit.enabled = true;
	}
	private void activeHelp(){
		activateHelpPlanes ();
		CanvasHelp.enabled = true;
		CanvasHelp.gameObject.SetActive (true);
	}
	private void activeControls(){
		CanvasControls.enabled = true;
	}
	private void deactivateHelpPlanes(){
		plane_bacteria.enabled = false;
		plane_parasite.enabled = false;
		plane_virus.enabled = false;
		CanvasHelp.enabled = false;
	}
	private void activateHelpPlanes(){
		plane_bacteria.enabled = true;
		plane_parasite.enabled = true;
		plane_virus.enabled = true;
		CanvasHelp.enabled = true;
	}
}
