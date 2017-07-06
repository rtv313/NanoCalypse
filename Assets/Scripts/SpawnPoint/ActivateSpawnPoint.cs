using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnPoint : MonoBehaviour {

    public GameObject spawnPoint;

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			Debug.Log ("Spawn Point Activated!");
            spawnPoint.GetComponent<SpawnControl>().riseSpawn = true;
			Destroy(transform.gameObject);
		}
    }
}
