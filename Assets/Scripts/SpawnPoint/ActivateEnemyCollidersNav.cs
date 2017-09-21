using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemyCollidersNav : MonoBehaviour {
    public float timeToActivate = 0.5f;
    public bool flagEnter = false;
    // Use this for initialization
    void Start () {
        Invoke("ActivateColliders", timeToActivate);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivateColliders()
    {

        Vector3 capCenter = gameObject.GetComponent<CapsuleCollider>().center; ;

        switch (gameObject.GetComponent<Context>().enemyType) // avoid the enemy get stuck in the spawn
        {
            case Context.EnemyType.VIRUS:
                gameObject.GetComponent<CapsuleCollider>().center = new Vector3(capCenter.x, 3.29f, capCenter.z);
                break;

            case Context.EnemyType.PARASITE:
                gameObject.GetComponent<CapsuleCollider>().center =new Vector3(capCenter.x, 2.5f, capCenter.z);
                break;

            case Context.EnemyType.BACTERIA:
                gameObject.GetComponent<CapsuleCollider>().center = new Vector3(capCenter.x, 2.68f, capCenter.z);
                break;
        }

        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<CapsuleCollider>().isTrigger = false;

    }

    public void CallActivateColliders()
    {
        Invoke("ActivateColliders", timeToActivate);
    }
 }
