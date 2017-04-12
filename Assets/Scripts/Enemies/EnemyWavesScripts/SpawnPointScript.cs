using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour {

    public int timeToSpawnMin;
    public int timeToSpawnMax;
    public int timeToSpawnEnemyInEffect=1;
    public int coolDownTime = 5;
    public bool flagCreate = true;
    public Transform patrolPath;
    public Transform wanderPath;
    public GameObject spawnEnemyEffect;
    public GameObject[] enemies;
    public enum  EnemyType { VIRUS, BACTERIA, PARASITE, ALL };
    public EnemyType  enemyType = EnemyType.ALL;
   
    private int enemySelector=0;
    private GameObject spawnEnemyEffectForDestroy;

    void Update()
    {
        if (flagCreate)
        {
            createEnemyPrevious();
        }
    }

    void createEnemyPrevious()
    {
        flagCreate = false;
        int randomTime = Random.Range(timeToSpawnMin, timeToSpawnMax);
        Invoke("enemyCreationEffect", randomTime);
    }

    void enemyCreationEffect()
    {
        spawnEnemyEffectForDestroy = Instantiate(spawnEnemyEffect, transform.position, transform.rotation); 

        if (enemyType == EnemyType.ALL)
        {
            enemySelector = Random.Range(0, enemies.Length);
            Invoke("createEnemy", timeToSpawnEnemyInEffect);
            return;
        }

        if (enemyType == EnemyType.VIRUS)
        {
            enemySelector = 0;
            Invoke("createEnemy", timeToSpawnEnemyInEffect);
            return;
        }

        if (enemyType == EnemyType.BACTERIA)
        {
            enemySelector = 1;
            Invoke("createEnemy", timeToSpawnEnemyInEffect);
            return;
        }

        if (enemyType == EnemyType.PARASITE)
        {
            enemySelector = 2;
            Invoke("createEnemy", timeToSpawnEnemyInEffect);
            return;
        }

    }

    void createEnemy()
    {
        GameObject enemy = enemies[enemySelector];
        Context context = enemy.GetComponent<Context>();
        context.patrolPath = patrolPath;
        context.wanderPath = wanderPath;
        context.wander = true;
        GameObject newEnemy= Instantiate(enemy, transform.position, transform.rotation);
        
      
        Invoke("coolDown", coolDownTime);
    }

    void createCertainEnemy(EnemyType setEnemyType)
    {
        enemyType = setEnemyType;
    }

    void destroySpawnPoint()
    {
        Destroy(gameObject);
    }

    void coolDown()
    {
        flagCreate = true;
        Destroy(spawnEnemyEffectForDestroy);
    }
}
