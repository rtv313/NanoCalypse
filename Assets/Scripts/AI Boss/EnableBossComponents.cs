using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBossComponents : MonoBehaviour {

    public AudioClip BossOst;
    public GameObject boss;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interfaceManager ifaceManager = GameObject.Find("GUI").GetComponent<interfaceManager>();
            int spawnPoints = ifaceManager.getRemainingSpawnPoints();

            if (spawnPoints <= 0)
            {

                boss.GetComponent<BossContext>().nav.enabled = true;
                boss.GetComponent<BossContext>().enabled = true;
                boss.GetComponent<SelectBossAttack>().enabled = true;
                boss.GetComponent<BossLaser>().enabled = true;
                boss.GetComponent<BossAirAttack>().enabled = true;
                boss.GetComponent<BossBulletAttack>().enabled = true;
                boss.GetComponent<BossBloodEffects>().enabled = true;
                boss.GetComponent<DamageFeedback>().enabled = true;

                GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
                cam.GetComponent<AudioSource>().clip = BossOst;
                cam.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }
    }
}
