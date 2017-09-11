using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour {

    SphereCollider collider;
    public GameObject ps;
    public GameObject psLighting;
    public GameObject psShockWave;
    public GameObject psSmoke;
    public GameObject psLight;

    public float explosionLifeTime = 2.0f;
    private float resetTime = 0f;
    private bool explodeFlag = false;

    void Awake()
    {
        resetTime = explosionLifeTime;
    }

    void Update()
    {

        if (explodeFlag == true)
            explosionLifeTime -= Time.deltaTime;

        if (explosionLifeTime <= 0)
            DeactivateExplosion();
    }

    void Start()
    {
        collider = gameObject.GetComponent<SphereCollider>();
        collider.enabled = false;
        Invoke("activateTrigger", 0.4f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2.5)
        {
            other.gameObject.GetComponent<Context>().life = 0;
        }

        Debug.Log("Collision");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2.5)
        {
             other.gameObject.GetComponent<Context>().life = 0;
        }

        Debug.Log("Collision");
    }

    void activateTrigger()
    {
        collider.enabled = true;
        gameObject.GetComponent<Renderer>().enabled = false;
        explodeFlag = true;
        explosionLifeTime = resetTime;
    }

    void DeactivateExplosion()
    {
        gameObject.SetActive(false);
    }

    public void ResetExplosionLifeTime()
    {
        explosionLifeTime = resetTime;
        explodeFlag = false;
        gameObject.SetActive(true);
    }

    public void EnableExplosion()
    {
        ps.GetComponent<ParticleSystem>().Emit(1);
        psLighting.GetComponent<ParticleSystem>().Emit(1);
        psShockWave.GetComponent<ParticleSystem>().Emit(1);
        psSmoke.GetComponent<ParticleSystem>().Emit(1);
        psLight.GetComponent<ParticleSystem>().Emit(1);

        explodeFlag = true;
    } 
}
