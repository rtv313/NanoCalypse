using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganExplosion : MonoBehaviour {
    public GameObject PSexplosion;
    private CameraShake cam;
    private OrganExplosions organsExplosionsPool;
    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        organsExplosionsPool = GameObject.FindGameObjectWithTag("OrgansExplosionSoundPool").GetComponent<OrganExplosions>();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(PSexplosion, transform.position, transform.rotation);
            cam.fireShake(0);
            organsExplosionsPool.GetDeadSound(transform.position);
            gameObject.SetActive(false);
        }
    }
}
