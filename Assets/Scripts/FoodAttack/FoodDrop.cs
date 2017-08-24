using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDrop : MonoBehaviour {

    private bool targetFlag =false;
    private GameObject targetRef;
    private FoodAttackPool foodAttackPool;
    private bool explosionFlag = false;
    // Use this for initialization
    void Start () {

        foodAttackPool = GameObject.FindGameObjectWithTag("Player").GetComponent<FoodAttackPool>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(30);
        }

        if (explosionFlag == false)
        {
            GameObject foodExp = foodAttackPool.GetFoodExplosion(transform);
            foodExp.GetComponent<FoodExplosion>().StartCall();
            explosionFlag = true;
        }
        
        targetRef.SetActive(false);
        gameObject.SetActive(false);
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
                targetRef = foodAttackPool.GetTarget(targetPos);
            }
        }
    }

    public void ResetFlag()
    {
        targetFlag = false;
        explosionFlag = false;
    }
}
