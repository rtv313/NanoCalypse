using UnityEngine;
using System.Collections;

public class InstantiateRipple : MonoBehaviour {
    public GameObject ripple;
  
 
    // Use this for initialization
	void Start ()
    {
	    
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider collision)
    {
       if (collision.gameObject.tag == "Floor") {
            Vector3 contact = new Vector3(transform.position.x,collision.transform.position.y, transform.position.z);
            Instantiate(ripple, contact, Quaternion.identity);
           
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Destroy(gameObject);
    }
}
