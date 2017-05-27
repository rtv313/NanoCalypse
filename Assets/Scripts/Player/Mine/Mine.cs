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

    void createMine()
    {
        Instantiate(MinePrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
    }
}

