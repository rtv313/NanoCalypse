using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushTrigger : MonoBehaviour {

    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Ambush Activated!");
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].SetActive(true);
            }
            Destroy(transform.gameObject);
        }
    }
}
