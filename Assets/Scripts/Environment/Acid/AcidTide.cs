using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTide : MonoBehaviour {
    public float speed = 1.0f;
    public float height = 10f;
    public bool flagGoUp = false;

    private Vector3 from;
    private Vector3 to;
    private float percentage=0.0f;
    private Vector3 result;
  
    // Use this for initialization
    void Start () {
        from = transform.position;
        to = transform.position;
        to.y += height;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (flagGoUp == true)
        {
            tideUp();
        }
        else
        {
            tideDown();
        }
    }

    void tideUp()
    {
        percentage +=  speed * Time.deltaTime;
        if (percentage >= 1.0f)
            percentage = 1.0f;
        result = Vector3.Lerp(from, to, percentage);
        transform.position = result;
    }

    void tideDown()
    {
        percentage -= speed * Time.deltaTime;
        if (percentage <= 0.0f)
         percentage = 0.0f;
        result = Vector3.Lerp(from, to, percentage);
        transform.position = result;
    }
}
