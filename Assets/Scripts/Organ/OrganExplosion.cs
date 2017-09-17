using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganExplosion : MonoBehaviour {
    public GameObject PSexplosion;
    CameraShake cam;

    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(PSexplosion, transform.position, transform.rotation);
            cam.fireShake(0);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
