using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour {
   
    GameObject player;
    public GameObject boss;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interfaceManager ifaceManager = GameObject.Find("GUI").GetComponent<interfaceManager>();
            int spawnPoints = ifaceManager.getRemainingSpawnPoints();

            if (spawnPoints <= 0)
            {
                GetComponent<AudioSource>().Play();
                CameraShake cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
                cam.fireShake(0);
                boss.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
