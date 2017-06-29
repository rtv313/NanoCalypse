using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierKill : MonoBehaviour {
    public int numSpawnsLinked;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
       
        if (numSpawnsLinked <= 0) {
            Debug.Log("DESTROYING ");
            Destroy(this.gameObject);
        }
	}
}
