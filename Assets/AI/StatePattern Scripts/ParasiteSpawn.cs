using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteSpawn : MonoBehaviour {

    public GameObject parasite;
    public bool canInstantiate=true;

    public void CreateParasites()
    {
       GameObject parasiteOne = Instantiate(parasite, transform.position, transform.rotation);
       parasiteOne.GetComponent<Context>().mutaded = true;
       parasiteOne.GetComponent<Context>().life = 50;
       Destroy(gameObject);
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

}
