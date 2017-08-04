using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttackSpawn : MonoBehaviour
{
    private GameObject food;
    public bool available = false;
    private float coolDownTime;

    public void callAttack(float timeAttack,float coolDownTime, GameObject selectedFood)
    {
        this.coolDownTime = coolDownTime;
        food = selectedFood;
        Invoke("Attack", timeAttack);
    }

    void Attack()
    {
        available = false;
        Instantiate(food, transform.position, food.transform.rotation);
    }
}
