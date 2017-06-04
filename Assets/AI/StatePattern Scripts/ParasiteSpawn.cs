using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteSpawn : MonoBehaviour {

    public GameObject parasite;
    public bool canInstantiate=true;

	// Use this for initialization
	 void Update()
    {
       
    }

    public void CreateParasites()
    {
       GameObject parasiteOne = Instantiate(parasite, transform.position, transform.rotation);
       parasiteOne.GetComponent<Context>().mutaded = true;
       parasiteOne.GetComponent<Context>().life = parasiteOne.GetComponent<Context>().life / 2;
       Destroy(gameObject);
     }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag!="Bullet")
          canInstantiate = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag != "Bullet")
            canInstantiate = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Bullet")
            canInstantiate = true;
    }

}
