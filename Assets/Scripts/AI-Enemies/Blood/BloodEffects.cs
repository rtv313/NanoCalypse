using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffects : MonoBehaviour
{
    public GameObject bloodPrefab;
    public float heightPosition=1.0f;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y += heightPosition;
            Quaternion rotateSpawnPosition = Quaternion.LookRotation(other.transform.position - transform.position ) ;
            var newBlood = Instantiate(bloodPrefab, spawnPosition, rotateSpawnPosition);
            newBlood.transform.parent = transform;
        }
    }

}
