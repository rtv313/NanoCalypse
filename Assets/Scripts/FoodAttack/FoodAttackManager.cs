using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttackManager : MonoBehaviour
{

    public GameObject[] spawnFoodPositions;
    public GameObject[] food;
    public float[] spawnTimes;
    public float timeBetweenAttacks;
    public bool startAttack = false;
    public int waves = 5;
    private int waveCounter = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }


    void Attack()
    {

        if (startAttack == true && waveCounter < waves)
        {
            waveCounter++;
            Debug.Log("FOOD ATTACK WAVE:" + waveCounter);
            float biggestTime = 0;
            float biggetsCooldown = 0;
            for (int i = 0; i < spawnFoodPositions.Length; i++)
            {
                GameObject selectedFood = food[Random.Range(0, food.Length)];
                GameObject spawnPoint = spawnFoodPositions[i];
                float timeToStart = spawnTimes[Random.RandomRange(0, spawnTimes.Length)];
                float timeCooldown = spawnTimes[Random.RandomRange(0, spawnTimes.Length)];

                if (biggestTime < timeToStart)
                {
                    biggestTime = timeToStart;
                }

                if (biggetsCooldown < timeCooldown)
                {
                    biggetsCooldown = timeCooldown;
                }

                spawnPoint.GetComponent<FoodAttackSpawn>().callAttack(timeToStart, timeCooldown, selectedFood);
            }
            startAttack = false;
            Invoke("ActivateAttack", timeBetweenAttacks + biggestTime + biggetsCooldown);
        }
    }

    void ActivateAttack()
    {
        startAttack = true;
    }

    public void ResetAttack()
    {
        startAttack = false;
        waveCounter = 0;
    }

}
