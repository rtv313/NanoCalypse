using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNavMesh : MonoBehaviour {

    // Use this for initialization
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<PlayerCollidersNav>().flagEnter==false)
        {
            other.gameObject.GetComponent<Context>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            other.gameObject.GetComponent<PlayerCollidersNav>().flagEnter = true;
            other.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
