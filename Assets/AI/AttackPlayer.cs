using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour {
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    public float attackDistance = 1.5f;
    PlayerHealth playerHealth;
    float timer;
    bool playerInRange;

    // Use this for initialization
    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Conditions to attack
    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player && nav.remainingDistance < 0.5f)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    // Condition to chase
    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player && nav.remainingDistance > 0.5f)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }



    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && 10 > 0) // 10 is just the player health
        {
            // ... attack.
            Attack();
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            
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
