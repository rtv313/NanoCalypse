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
        foodAttackPool = GameObject.FindGameObjectWithTag("Player").GetComponent<FoodAttackPool>();
    }

    public void callAttack(float timeAttack,float coolDownTime)
    {
        this.coolDownTime = coolDownTime;
        Invoke("Attack", timeAttack);
    }

    void Attack()
    {
        available = false;
        foodAttackPool.GetFood(transform);
    }
}
