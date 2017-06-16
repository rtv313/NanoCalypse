using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganExplosion : MonoBehaviour {
    public GameObject PSexplosion;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate(PSexplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
