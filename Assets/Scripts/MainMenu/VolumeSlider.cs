using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeSlider(float value)
    {
        AudioListener.volume = Mathf.Clamp(value / 15.0f, 0.0f, 1.0f);
    }
}
