using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeInterface : MonoBehaviour {
    public BossContext context;
    public GameObject meleeAttackCollider;

    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public GameObject SpawnPoint3;

    public GameObject SpawnPointPSBubbles1;
    public GameObject SpawnPointPSBubbles2;
    public GameObject SpawnPointPSBubbles3;

    public GameObject SpawnPointPSFog1;
    public GameObject SpawnPointPSFog2;
    public GameObject SpawnPointPSFog3;


    public AudioSource meleeSoundEffect;
    public AudioSource spawnSoundEffect;
    private EnemiesPool enemiesPool;

    void Start()
    {

        SpawnPointPSBubbles1.SetActive(false);
        SpawnPointPSBubbles2.SetActive(false);
        SpawnPointPSBubbles3.SetActive(false);

        SpawnPointPSFog1.SetActive(false);
        SpawnPointPSFog2.SetActive(false);
        SpawnPointPSFog3.SetActive(false);

        meleeAttackCollider.GetComponent<Collider>().enabled = false;
        enemiesPool = GameObject.FindGameObjectWithTag("EnemiesPool").GetComponent<EnemiesPool>();
    }

    public void ActivateMeleeCollider()
    {
        meleeAttackCollider.GetComponent<Collider>().enabled = true;
    }

    public void DeActivateMeleeCollider()
    {
        meleeAttackCollider.GetComponent<Collider>().enabled = false;
    }

    public void SpawnEnemiesBoss()
    {
        CreateEnemy(SpawnPoint1.transform);
        CreateEnemy(SpawnPoint2.transform);
        CreateEnemy(SpawnPoint2.transform);
    }



    public void CallAnimationEnded()
    {
        context.FlagMeleeAttack = false;
        context.FlagSpawnAttack = false;
        context.AnimationInProcess = false;
    }

    private void CreateEnemy(Transform spawnPos)
    {
        int randomEnemy = Random.Range(0, 3);
        GameObject enemySelected;

        SpawnPointPSBubbles1.SetActive(true);
        SpawnPointPSBubbles1.GetComponent<ParticleSystem>().Emit(1);
        SpawnPointPSFog1.SetActive(true);
        SpawnPointPSFog1.GetComponent<ParticleSystem>().Emit(1);

        SpawnPointPSBubbles2.SetActive(true);
        SpawnPointPSBubbles2.GetComponent<ParticleSystem>().Emit(1);
        SpawnPointPSFog2.SetActive(true);
        SpawnPointPSFog2.GetComponent<ParticleSystem>().Emit(1);

        SpawnPointPSBubbles3.SetActive(true);
        SpawnPointPSBubbles3.GetComponent<ParticleSystem>().Emit(1);
        SpawnPointPSFog3.SetActive(true);
        SpawnPointPSFog3.GetComponent<ParticleSystem>().Emit(1);

        Invoke("DeactivatePS", 2.5f);

        switch (randomEnemy)
        {
            case 0:
                enemySelected = enemiesPool.GetEnemy(spawnPos, Context.EnemyType.VIRUS);
                PrepareEnemy(enemySelected, spawnPos);
                break;

            case 1:
                enemySelected = enemiesPool.GetEnemy(spawnPos, Context.EnemyType.BACTERIA);
                PrepareEnemy(enemySelected, spawnPos);
                break;

            case 2:
                enemySelected = enemiesPool.GetEnemy(spawnPos, Context.EnemyType.PARASITE);
                PrepareEnemy(enemySelected, spawnPos);
                break;
        }
    }


    private void PrepareEnemy(GameObject enemySelected,Transform spawnPos)
    {
       
        enemySelected.GetComponent<CapsuleCollider>().isTrigger = false;
        Context contextSpawnedEnemy = enemySelected.GetComponent<Context>();
        contextSpawnedEnemy.patrolPath = context.wanderPath;
        contextSpawnedEnemy.wanderPath = context.wanderPath;
        contextSpawnedEnemy.wander = true;
        enemySelected.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        enemySelected.gameObject.GetComponent<Context>().enabled = true;
        enemySelected.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        enemySelected.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        enemySelected.gameObject.GetComponent<SphereCollider>().enabled = true;
        enemySelected.GetComponent<CapsuleCollider>().enabled = true;
        contextSpawnedEnemy.path_objs_Wander = context.wanderPath.GetComponentsInChildren<Transform>();
        contextSpawnedEnemy.path_objs_Wander = context.wanderPath.GetComponentsInChildren<Transform>();
    }

    public void MeleeSoundEffect()
    {
        meleeSoundEffect.Play();
    }

    public void SpawnSoundEffect()
    {
        spawnSoundEffect.Play();
    }

    private void DeactivatePS()
    {
        SpawnPointPSBubbles1.SetActive(false);
        SpawnPointPSFog1.SetActive(false);

        SpawnPointPSBubbles2.SetActive(false);
        SpawnPointPSFog2.SetActive(false);

        SpawnPointPSBubbles3.SetActive(false);
        SpawnPointPSFog3.SetActive(false);
    }
}
