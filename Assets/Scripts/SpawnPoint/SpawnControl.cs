using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour {

    //Life elements
    public int health = 100;

    //Animation variables
    private Animator animatorControl;
    public bool riseSpawn = false;
    public bool openSpawn = false;
    public bool closeSpawn = false;
    public bool finishRise = false;
    public bool finishOpen = false;
    public bool finishClose = true;
    private bool riseFlag = false;
    private bool openFlag = false;
    private bool closeFlag = false;

    //Spawn variables
    public GameObject spawnPos;
    public GameObject deathPs;
    public int enemyPool = 5;
    public int timeToSpawnMin;
    public int timeToSpawnMax;
    public int timeToSpawnEnemyInEffect = 1;
    public int coolDownTime = 5;
    public bool flagCreate = true;
    public Transform patrolPath;
    public Transform wanderPath;
    public GameObject spawnEnemyEffect;
    public GameObject enemyVirus;
    public GameObject enemyBacteria;
    public GameObject enemyParasite;
    public enum EnemyType { VIRUS, BACTERIA, PARASITE, ALL };
    public EnemyType enemyType = EnemyType.ALL;
    private GameObject spawnEnemyEffectForDestroy;
    private GameObject enemySelected;
    public float speed = 6.0f;

    //Audio Variables
    private AudioSource audio;
    public AudioClip riseAudio;
    public AudioClip openAudio;
    public AudioClip closeAudio;
    public AudioClip spawnAudio;

    // Use this for initialization
    void Start ()
    {
        animatorControl = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void ActivateSpawn()
    {
        riseSpawn = true;
        openSpawn = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (health > 0)
        {
            AnimationControl();

            if (flagCreate == true && enemyPool > 0 && finishOpen == true)
            {
                createEnemyPrevious();
            }
        }
        else
        {
           GameObject deathPsRef= Instantiate(deathPs, transform.position, transform.rotation);
            Destroy(deathPsRef, 2.0f);
            Destroy(gameObject); 
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
        spawnEnemyEffectForDestroy = Instantiate(spawnEnemyEffect, spawnPos.transform.position, spawnPos.transform.rotation);

        switch (enemyType)
        {
            case EnemyType.VIRUS:
                enemySelected = enemyVirus;
                break;

            case EnemyType.BACTERIA:
                enemySelected = enemyBacteria;
                break;

            case EnemyType.PARASITE:
                enemySelected = enemyParasite;
                break;

            case EnemyType.ALL:

                int randomEnemy = Random.Range(0, 3);
                switch (randomEnemy)
                {
                    case 0:
                        enemySelected = enemyVirus;
                        break;

                    case 1:
                        enemySelected = enemyBacteria;
                        break;

                    case 2:
                        enemySelected = enemyParasite;
                        break;
                }

                break;

        }

        Invoke("createEnemy", timeToSpawnEnemyInEffect);
        return;
    }

    void createEnemy()
    {
        audio.clip = spawnAudio;
        audio.Play();
        Context context = enemySelected.GetComponent<Context>();
        context.patrolPath = patrolPath;
        context.wanderPath = wanderPath;
        context.wander = true;
        GameObject newEnemy = Instantiate(enemySelected, spawnPos.transform.position, spawnPos.transform.rotation);
        newEnemy.GetComponent<CapsuleCollider>().enabled = false;
        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        newEnemy.GetComponent<Rigidbody>().velocity = newEnemy.transform.forward * speed;
        Invoke("coolDown", coolDownTime);
    }


    void coolDown()
    {
        flagCreate = true;
        closeSpawn = true;
        Destroy(spawnEnemyEffectForDestroy);
    }

    void createCertainEnemy(EnemyType setEnemyType)
    {
        enemyType = setEnemyType;
    }

   

    //Animation functions
    void AnimationControl()
    {
        if (riseSpawn == true && riseFlag == false) // activate spawn rising
        {
            audio.clip = riseAudio;
            audio.Play();
            animatorControl.SetTrigger("RiseSpawn");
            riseFlag = true;
        }

        if (openSpawn == true && openFlag==false && riseFlag == true && finishRise == true && closeSpawn == false && finishClose == true)// create enemy
        {
            audio.clip = openAudio;
            audio.Play();
            animatorControl.SetTrigger("OpenSpawn");
            openSpawn = false;
            openFlag = true;
            closeFlag = false;
            finishClose = false;
        }

        if (closeSpawn == true && closeFlag==false && openSpawn == false && riseFlag == true && openSpawn==false && finishOpen == true) // coolDown
        {
            audio.clip = closeAudio;
            audio.Play();
            animatorControl.SetTrigger("CloseSpawn");
            closeSpawn = false;
            openFlag = false;
            closeFlag = true;
            finishOpen = false;
        }
    }

    void FinishRise()
    {
        finishRise = true;
    }

    void SpawnClose()
    {
        finishClose = true;
        openSpawn = true;
    }

    void SpawnOpen()
    {
        finishOpen = true;
    }
}
