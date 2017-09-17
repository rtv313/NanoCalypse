using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBloodEffects : MonoBehaviour {
    public float heightPosition = 1.0f;
    private GameObject bloodPool;
    private BossContext bossContext;
    // Use this for initialization
    void Start ()
    {
        bloodPool = GameObject.FindGameObjectWithTag("BloodPsPool");
        bossContext = GetComponent<BossContext>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            CreateBlood(other);
        }
    }

    private void CreateBlood(Collision other)
    {
        BulletDamage bulletDamage = other.gameObject.GetComponent<BulletDamage>();
        Vector3 spawnPosition = other.transform.position;
        spawnPosition.y += heightPosition;
        Quaternion rotateSpawnPosition = Quaternion.LookRotation(other.transform.position - transform.position);

        switch (bossContext.bossColor)
        {
            case BossContext.BossStateColor.VIRUS:

                if (bulletDamage.bulletType == BulletDamage.BulletType.ASSAULT)
                {
                    var newBlood = bloodPool.GetComponent<BloodPool>().GetBloodEffect(spawnPosition, rotateSpawnPosition, BloodPool.BloodType.VIRUS);
                    newBlood.transform.parent = transform;
                }
                break;

            case BossContext.BossStateColor.PARASITE:
                if (bulletDamage.bulletType == BulletDamage.BulletType.SHOOTGUN)
                {
                    var newBlood2 = bloodPool.GetComponent<BloodPool>().GetBloodEffect(spawnPosition, rotateSpawnPosition, BloodPool.BloodType.PARASYTE);
                    newBlood2.transform.parent = transform;
                }
                break;

            case BossContext.BossStateColor.BACTERIA:
                if (bulletDamage.bulletType == BulletDamage.BulletType.SNIPER)
                {
                    var newBlood3 = bloodPool.GetComponent<BloodPool>().GetBloodEffect(spawnPosition, rotateSpawnPosition, BloodPool.BloodType.BACTERIA);
                    newBlood3.transform.parent = transform;
                }
                break;

            case BossContext.BossStateColor.INMUNE:

                break;

        }
    }
}
