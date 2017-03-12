using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreeMovement : MonoBehaviour {

    public float speed = 5.0f;
    Vector3 movement;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime; // makes speed in diagonal the same
        transform.position = transform.position + movement;
    }
}
