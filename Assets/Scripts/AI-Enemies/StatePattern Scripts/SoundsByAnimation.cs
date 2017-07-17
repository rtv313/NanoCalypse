using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsByAnimation : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip attackAudio;
    private Context cont;

    // Use this for initialization
    void Start () {
        cont = transform.parent.GetComponent<Context>();
        audioSource = transform.parent.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAttackSound()
    {
        if (cont.mutaded == false)
        {
            cont.animFlagAttack = true;
            audioSource.clip = attackAudio;
            audioSource.Play();
        }
    }
}
