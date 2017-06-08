using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsByAnimation : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip attackAudio;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAttackSound()
    {
        audioSource.clip = attackAudio;
        audioSource.Play();
    }
}
