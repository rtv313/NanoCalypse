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
    public int numberOfEnemiesForSpawn = 10;
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

    // ActivateMesh
    public  GameObject activationMesh;

    //Enemy Pool 
    private EnemiesPool enemiesPool;

    private interfaceManager ifaceManager;
    private CameraShake cam;

    //Dead Sound Effect
    public GameObject deadSoundEffect;

    // Use this for initialization
    void Start ()
    {
        animatorControl = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        enemiesPool = GameObject.FindGameObjectWithTag("EnemiesPool").GetComponent<EnemiesPool>();
        ifaceManager = GameObject.Find("GUI").GetComponent<interfaceManager>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
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

            if (flagCreate == true && numberOfEnemiesForSpawn > 0 && finishOpen == true)
            {
                createEnemyPrevious();
                numberOfEnemiesForSpawn--;
            }
        }
        else
        {
            ifaceManager.updateSpawnPointsRemaining();
            GameObject deathPsRef= Instantiate(deathPs, transform.position, transform.rotation);
            activationMesh.GetComponent<ActivateNavMesh>().DestroyActivationMesh();
            cam.fireShake(0);
            Destroy(deathPsRef, 2.0f);
            Destroy(gameObject);
            GameObject soundDead= Instantiate(deadSoundEffect, transform.position, transform.rotation);
            Destroy(soundDead, 3.0f);
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
                enemySelected = enemiesPool.GetEnemy(spawnPos.transform, Context.EnemyType.VIRUS);
                break;

            case EnemyType.BACTERIA:
                enemySelected = enemiesPool.GetEnemy(spawnPos.transform, Context.EnemyType.BACTERIA);
                break;

            case EnemyType.PARASITE:
                enemySelected = enemiesPool.GetEnemy(spawnPos.transform, Context.EnemyType.PARASITE);
                break;

            case EnemyType.ALL:

                int randomEnemy = Random.Range(0, 3);
                switch (randomEnemy)
                {
                    case 0:
                        enemySelected = enemiesPool.GetEnemy(spawnPos.transform, Context.EnemyType.VIRUS);
                        break;

                    case 1:
                        enemySelected = enemiesPool.GetEnemy(spawnPos.transform, Context.EnemyType.BACTERIA);
                        break;

                    case 2:
                        enemySelected = enemiesPool.GetEnemy(spawnPos.transform, Context.EnemyType.PARASITE);
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
        GameObject newEnemy = enemySelected;
        newEnemy.GetComponent<Context>().enabled = false;
        newEnemy.GetComponent<SphereCollider>().enabled = false;
        newEnemy.GetComponent<CapsuleCollider>().enabled = false;
        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        newEnemy.GetComponent<Rigidbody>().isKinematic = false;
        newEnemy.GetComponent<Rigidbody>().velocity = newEnemy.transform.forward * speed;
        float timeActivateColliders = newEnemy.GetComponent<ActivateEnemyCollidersNav>().timeToActivate;
        newEnemy.GetComponent<ActivateEnemyCollidersNav>().flagEnter=false;
        newEnemy.GetComponent<ActivateEnemyCollidersNav>().CallActivateColliders();
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
