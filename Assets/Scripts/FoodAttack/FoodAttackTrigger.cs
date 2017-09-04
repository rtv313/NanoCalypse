using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttackTrigger : MonoBehaviour {
    public int waves = 5;
    public int timeBetweenAttacks = 5;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            FoodAttackManager foodManager = GameObject.FindGameObjectWithTag("Player").GetComponent<FoodAttackManager>();
            foodManager.ResetAttack();
            foodManager.waves = waves;
            foodManager.timeBetweenAttacks = timeBetweenAttacks;
            foodManager.startAttack = true;
            Destroy(gameObject);
        }
    }

}
