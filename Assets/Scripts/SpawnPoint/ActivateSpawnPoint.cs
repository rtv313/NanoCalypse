using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnPoint : MonoBehaviour {

    public GameObject[] spawnPoints;

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player") {
			Debug.Log ("Spawn Point Activated!");

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].GetComponent<SpawnControl>().riseSpawn = true;
            }
            Destroy(transform.gameObject);
		}
    }
}
