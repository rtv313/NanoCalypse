using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDrop : MonoBehaviour {
    public GameObject PsExplosion;
    public GameObject target;
    private bool targetFlag =false;
    private GameObject targetRef;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().currentHealth -= 30;
        }
        Instantiate(PsExplosion, transform.position, transform.rotation);
        Destroy(targetRef);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (targetFlag == false)
        {
            targetFlag = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                Vector3 targetPos = hit.point;
                targetPos.y += 0.1f;
                targetRef=Instantiate(target, targetPos, Quaternion.identity);
            }
        }
    }
}
