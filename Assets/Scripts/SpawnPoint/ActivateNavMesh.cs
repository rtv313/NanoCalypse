using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNavMesh : MonoBehaviour {

    // Use this for initialization

    void Start()
    {
        transform.parent = null;
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<ActivateEnemyCollidersNav>().flagEnter==false)
        {
            other.gameObject.GetComponent<Context>().enabled = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            other.gameObject.GetComponent<ActivateEnemyCollidersNav>().flagEnter = true;
            other.gameObject.GetComponent<SphereCollider>().enabled = true;

            Vector3 capCenter = other.gameObject.GetComponent<CapsuleCollider>().center;
            other.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(capCenter.x, 0.0f, capCenter.z);
        }
    }

    public void DestroyActivationMesh()
    {
        Destroy(gameObject, 10.0f);
    }
}
