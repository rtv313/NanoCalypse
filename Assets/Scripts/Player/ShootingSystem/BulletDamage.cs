using UnityEngine;

public class BulletDamage : MonoBehaviour {

    public enum BulletType { ASSAULT, SHOOTGUN, SNIPER };
    public float bulletLifeTime = 2.0f;
    public BulletType bulletType = BulletType.ASSAULT;
    public AudioClip bulletHitHard;
    public AudioClip bulletHitSoft;
    public AudioClip enemyDamage;
    public AudioClip enemyScreech;
    public int assaultRifleDamage = 10;
    public int shootgunDamage = 5;
    public int sniperDamage = 30;
    private float resetTime = 0f;
    private bool shootFlag = false;

    void Awake()
    {
        resetTime = bulletLifeTime;
    }

    void Update()
    {

        if (shootFlag == true)
            bulletLifeTime -= Time.deltaTime;

        if (bulletLifeTime <= 0)
            DeactivateBullet();

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Context context = other.gameObject.GetComponent<Context>();

            switch (bulletType)
            {
                case BulletType.ASSAULT:
                    context.life -= assaultRifleDamage / 2;

                    if (context.enemyType != Context.EnemyType.VIRUS && checkPropMutation())
                    {
                        context.state = new MutationState();
                    }


                    if (context.enemyType != Context.EnemyType.VIRUS)
                    {
                        context.receiveDamageMutate();
                    }
                    else
                        context.receiveDamage();

                    break;

                case BulletType.SHOOTGUN:
                    context.life -= shootgunDamage / 2;

                    if (context.enemyType != Context.EnemyType.PARASITE && checkPropMutation())
                    {
                        context.state = new MutationState();
                    }

                    if (context.enemyType != Context.EnemyType.PARASITE)
                    {
                        context.receiveDamageMutate();
                    }
                    else context.receiveDamage();

                    break;

                case BulletType.SNIPER:

                    if (context.enemyType != Context.EnemyType.BACTERIA && checkPropMutation())
                    {
                        context.state = new MutationState();
                    }
                    else
                    {
                        context.life -= sniperDamage / 2;
                    }

                    if (context.enemyType != Context.EnemyType.BACTERIA)
                    {
                        context.receiveDamageMutate();
                    }
                    else context.receiveDamage();

                    break;
            }

            AudioSource.PlayClipAtPoint(bulletHitHard, transform.position);
            if (context.life <= 0)
            {
                AudioSource.PlayClipAtPoint(enemyScreech, transform.position, 4.0f);
            }
            else
                AudioSource.PlayClipAtPoint(enemyDamage, transform.position, 4.0f);

            DeactivateBullet();
        }
        // Hit on spawn Point
        else if (other.gameObject.tag == "SpawnPoint")
        {
            SpawnDamage(other.gameObject);
        }
        else if (other.gameObject.tag == "Boss")
        {
            BossDamage(other.gameObject);
        }

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy")
        {
            AudioSource.PlayClipAtPoint(bulletHitSoft, transform.position);
            DeactivateBullet();
        }
    }

    bool checkPropMutation()
    {
        int mutation = Random.Range(0, 9);

        if (mutation <= 2)
            return true;
        else
            return false;

    }

    void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }

    public void ResetBulletLifeTime()
    {
        bulletLifeTime = resetTime;
        shootFlag = false;
    }

    public void EnableShoot()
    {
        shootFlag = true;
    }

    private void BossDamage(GameObject other)
    {
        BossContext bossContext = other.gameObject.GetComponent<BossContext>();

        if (bossContext.bossColor != BossContext.BossStateColor.INMUNE)
        {
            switch (bulletType)
            {
                case BulletType.ASSAULT:
                    if (bossContext.bossColor == BossContext.BossStateColor.VIRUS)
                    {
                        bossContext.life -= assaultRifleDamage / 2;
                        other.gameObject.GetComponent<DamageFeedback>().receiveDamage();
                    }
                    break;

                case BulletType.SHOOTGUN:
                    if (bossContext.bossColor == BossContext.BossStateColor.PARASITE)
                    {
                        bossContext.life -= shootgunDamage / 2;
                        other.gameObject.GetComponent<DamageFeedback>().receiveDamage();
                    } 
                    break;

                case BulletType.SNIPER:
                    if (bossContext.bossColor == BossContext.BossStateColor.BACTERIA)
                    {
                        bossContext.life -= sniperDamage / 2;
                        other.gameObject.GetComponent<DamageFeedback>().receiveDamage();
                    }
                        
                    break;
            }
        }
    }

    private void SpawnDamage(GameObject other)
    {
        other.gameObject.GetComponent<DamageFeedback>().receiveDamage();
        SpawnControl spawnControl = other.gameObject.GetComponent<SpawnControl>();

        if (spawnControl.riseSpawn == false)
            return;

        switch (bulletType)
        {
            case BulletType.ASSAULT:
                spawnControl.health -= assaultRifleDamage / 2;
                break;

            case BulletType.SHOOTGUN:
                spawnControl.health -= shootgunDamage / 2;
                break;

            case BulletType.SNIPER:
                spawnControl.health -= sniperDamage / 2;
                break;
        }
    }

}
