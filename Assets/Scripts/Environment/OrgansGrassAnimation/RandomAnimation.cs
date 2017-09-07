using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour {

    public int [] randomTimes;
    private Animator animatorControl;
    private bool canCallAnimation = true;
   
    // Use this for initialization
    void Start ()
    {
        animatorControl = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (canCallAnimation == true)
        {
            canCallAnimation = false;
            Invoke("CallAnimation", randomTimes[Random.Range(0, randomTimes.Length)]);
        }
	}

    void CallAnimation()
    {
        animatorControl.SetTrigger("activateAnimation");
       
    }

    public void AnimationFinished()
    {
        canCallAnimation = true;
    }
}
