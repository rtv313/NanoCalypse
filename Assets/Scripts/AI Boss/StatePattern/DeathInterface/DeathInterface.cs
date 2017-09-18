﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInterface : MonoBehaviour {

    public GameObject deathPs;
    public Transform explosion1;
    public Transform explosion2;
    public Transform explosion3;
    public Transform explosion4;
    public Transform explosion5;

    private GameObject fatherObject;

	// Use this for initialization
	void Start () {

        fatherObject = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Death()
    {
        GameObject deathPsRef;

        deathPsRef = Instantiate(deathPs, explosion1.position, explosion1.rotation);
        Destroy(deathPsRef, 2.0f);

        deathPsRef = Instantiate(deathPs, explosion2.position, explosion2.rotation);
        Destroy(deathPsRef, 2.0f);

        deathPsRef = Instantiate(deathPs, explosion3.position, explosion3.rotation);
        Destroy(deathPsRef, 2.0f);

        deathPsRef = Instantiate(deathPs, explosion4.position, explosion4.rotation);
        Destroy(deathPsRef, 2.0f);

        deathPsRef = Instantiate(deathPs, explosion5.position, explosion5.rotation);
        Destroy(deathPsRef, 2.0f);

        Destroy(fatherObject);
    }
}