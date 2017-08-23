using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageEffects : MonoBehaviour {

    public GameObject [] sparksObjects;
    public bool activateSparks = false;
    public AudioSource audioSource;
    

    void Start ()
    {
        DisableSparkEffects();
    }


    void Update()
    {
        if (activateSparks)
        {
            EnableSparkEffects();
        }
    }

    public void ActivateSparks()
    {
        activateSparks = true;
    }

    void EnableSparkEffects()
    {
        audioSource.Play();

        for (int i = 0; i < sparksObjects.Length; i++)
        {
            ParticleSystem ps = sparksObjects[i].GetComponent<ParticleSystem>();
            var em = ps.emission;
            em.enabled = true;
        }
        activateSparks = false;
        Invoke("DisableSparkEffects", 0.5f);
    }

    void DisableSparkEffects()
    {
        for (int i = 0; i < sparksObjects.Length; i++)
        {
            ParticleSystem ps = sparksObjects[i].GetComponent<ParticleSystem>();
            var em = ps.emission;
            em.enabled = false;
        }
    }
}
