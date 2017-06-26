using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDestroy : MonoBehaviour {
    public float destroyTime = 3.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject,destroyTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
