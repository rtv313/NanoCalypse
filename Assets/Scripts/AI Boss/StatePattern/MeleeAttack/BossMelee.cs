using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour {
    private int damage = 1;
    private PlayerHealth playerHealth;
    // Use this for initialization
    void Start () {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            playerHealth.TakeDamage(damage);
    }
}
