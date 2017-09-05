using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroopFoodSensor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<DropFood>().enabled = false;
    }
	
    void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
            gameObject.GetComponent<DropFood>().enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            gameObject.GetComponent<DropFood>().enabled = false;
    }

}
