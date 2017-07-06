using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemyCollidersNav : MonoBehaviour {
    public float timeToActivate = 0.5f;
    public bool flagEnter = false;
    // Use this for initialization
    void Start () {
        Invoke("ActivateColliders", timeToActivate);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivateColliders()
    {
        GetComponent<CapsuleCollider>().enabled = true;
    }
 }
