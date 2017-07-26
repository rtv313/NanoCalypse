using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

    SphereCollider collider;
    public GameObject ps;
    void Start()
    {
        collider = gameObject.GetComponent<SphereCollider>();
        collider.enabled = false;
        ps.GetComponent<ParticleSystem>().Emit(1);
        Invoke("activateTrigger", 0.4f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2.5)
        {
            other.gameObject.GetComponent<Context>().life = 0;
        }

        Debug.Log("Collision");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2.5)
        {
            other.gameObject.GetComponent<Context>().life = 0;
        }

        Debug.Log("Collision");
    }

    void activateTrigger()
    {
        collider.enabled = true;
        gameObject.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 2.0f);
    }
}
