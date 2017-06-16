using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
   
    public GameObject Explosion;
   
    // Update is called once per frame
	void Start ()
    {
        Invoke("Destroy", 30.0f);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2)
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2)
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }

    void Destroy()
    {
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
    }
}

