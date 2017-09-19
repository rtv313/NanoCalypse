using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endgame_trigger : MonoBehaviour {
	private Canvas ScoreScreen, GUI;
	private Object[] objects;
	// Use this for initialization
	void Start () {
		ScoreScreen = GameObject.Find ("ScoreScreen").GetComponent<Canvas>();
		GUI = GameObject.Find ("GUI").GetComponent<Canvas>();
		objects = FindObjectsOfType(typeof(GameObject));
	}


    public void CallEndLevel()
    {
        Invoke("EndLevel", 3.0f);
    }

	public void EndLevel() {

		
			GUI.enabled = false;
			ScoreScreen.enabled = true;
			objects = FindObjectsOfType(typeof(GameObject));
			foreach (GameObject go in objects)
			{
				go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
				Time.timeScale = 0.0f;
			}
		
	}


}
