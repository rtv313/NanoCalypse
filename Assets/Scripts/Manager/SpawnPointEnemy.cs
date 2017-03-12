using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointEnemy : MonoBehaviour {

    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public int numberEnemies = 5;
    private int counter = 0;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn()
    {
     

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, transform.position, transform.rotation);

        counter++;

        if (counter >= numberEnemies) {
            Destroy(transform.gameObject);
        }

    }
}
