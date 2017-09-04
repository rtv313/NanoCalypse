using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttackSpawn : MonoBehaviour
{
    public bool available = false;
    private float coolDownTime;
    private FoodAttackPool foodAttackPool;

    void Start()
    {
        foodAttackPool = GameObject.FindGameObjectWithTag("Player").GetComponent<FoodAttackPool>(); // Invoke("Attack", timeAttack);
    }

    public void callAttack(float timeAttack,float coolDownTime)
    {
        this.coolDownTime = coolDownTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            
            if (hit.collider.tag == "MainScenario" || hit.collider.tag == "Player")
            {
                Invoke("Attack", timeAttack);
            }
        }
    }

    void Attack()
    {

        available = false;
        foodAttackPool.GetFood(transform);
    }
}
