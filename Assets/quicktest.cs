using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quicktest : MonoBehaviour {
	Transform tran;
	// Use this for initialization
	void Start () {
		tran = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		tran.Translate (0.0f, Mathf.Sin (Time.time)/10, 0.0f);
	}
}
