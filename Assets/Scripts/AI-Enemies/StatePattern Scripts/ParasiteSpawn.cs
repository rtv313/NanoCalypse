using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteSpawn : MonoBehaviour {

    public GameObject parasite;
    public bool canInstantiate=true;
    private GameObject parasiteRef;

    public void CreateParasites()
    {
       EnemiesPool enemiesPool = GameObject.FindGameObjectWithTag("EnemiesPool").GetComponent<EnemiesPool>(); 
       GameObject parasiteOne = enemiesPool.GetEnemy(transform.transform, Context.EnemyType.PARASITE);
       parasiteOne.GetComponent<Context>().mutaded = true;
       parasiteOne.GetComponent<Context>().life = 15;
       parasiteOne.GetComponent<CapsuleCollider>().isTrigger = false;
       parasiteRef = parasiteOne;
       Invoke("PStime", 0.05f);
        // Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
          canInstantiate = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            canInstantiate = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            canInstantiate = true;
    }

    void PStime()
    {
        parasiteRef.GetComponent<ParasiteMutationPS>().ActivatePSMutation();
    }
}
