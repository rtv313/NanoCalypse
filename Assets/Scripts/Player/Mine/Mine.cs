using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int explosionTime = 1;
    public GameObject MinePrefab;
   
    // Update is called once per frame
	void Start ()
    {
        Invoke("createMine", explosionTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 1)
        {
            Instantiate(MinePrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        }
    }
}

