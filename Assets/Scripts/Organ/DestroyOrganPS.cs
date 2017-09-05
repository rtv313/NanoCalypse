using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOrganPS : MonoBehaviour {
    public float timeToDestroy = 1.5f;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDestroy);
	}

}
