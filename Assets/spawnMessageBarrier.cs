using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMessageBarrier : MonoBehaviour {
    public BarrierKill barrier;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDisable() {
        --barrier.numSpawnsLinked;
    }
}
