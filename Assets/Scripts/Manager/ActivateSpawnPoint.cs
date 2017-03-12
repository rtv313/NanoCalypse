using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnPoint : MonoBehaviour {

    public GameObject spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            spawnPoint.SetActive(true);
            Destroy(transform.gameObject);
        }
    }
}
