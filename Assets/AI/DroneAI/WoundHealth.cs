using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundHealth : MonoBehaviour {

    public bool startTimer = false;
    public float timeToHeal = 60f;
    public bool finishedHealing = false;
    public float time = 0;
    
    // Update is called once per frame
	void Update () {

        if (startTimer == true)
        {
            timer();
        }
	}

    void timer()
    {
        time += Time.deltaTime;

        if (time >= timeToHeal)
        {
            finishedHealing = true;
            Destroy(gameObject, 3.0f);
        }
    }

}
