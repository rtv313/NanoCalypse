using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffects : MonoBehaviour
{
    public float heightPosition=1.0f;
    public BloodPool.BloodType bloodType = BloodPool.BloodType.VIRUS;
    private GameObject bloodPool;


    void Start()
    {
        bloodPool = GameObject.FindGameObjectWithTag("BloodPsPool");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y += heightPosition;
            Quaternion rotateSpawnPosition = Quaternion.LookRotation(other.transform.position - transform.position);
            var newBlood = bloodPool.GetComponent<BloodPool>().GetBloodEffect(spawnPosition, rotateSpawnPosition, bloodType);
            newBlood.transform.parent = transform;
        }
    }

}
