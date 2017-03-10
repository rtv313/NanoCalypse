using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.
=======
public class EnemyMovement : MonoBehaviour {
>>>>>>> parent of 159b291... Basic Spawn Added

    Transform player;
    NavMeshAgent nav;

	// Use this for initialization
	void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
<<<<<<< HEAD
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    void Update()
=======
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
>>>>>>> parent of 159b291... Basic Spawn Added
    {
        nav.SetDestination(player.position);    

	}
}
