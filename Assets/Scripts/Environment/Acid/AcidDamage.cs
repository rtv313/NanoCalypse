using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidDamage : MonoBehaviour {

    GameObject player;                         
    PlayerHealth playerHealth;               
    EnemyHealth enemyHealth;
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;
    float timer;

    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
      
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks)
            {
                // ... attack.
                Attack();
                timer = 0f;
            }
           
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
