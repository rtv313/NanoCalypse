using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float lifeTime = 30f;
    private float resetTime = 0f;
    private bool droopFlag = false;
    private ExplosionsMinePool explosionsMinePool;

    void Awake()
    {
        resetTime = lifeTime;
    }

    void Start()
    {
        explosionsMinePool = GameObject.FindGameObjectWithTag("Player").GetComponent<ExplosionsMinePool>();
    }

    void Update()
    {
        if (droopFlag == true)
            lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
            Destroy();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2)
        {
            GameObject explosion = explosionsMinePool.GetMineExplosion(gameObject.transform);
            explosion.GetComponent<ShockWave>().EnableExplosion();
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2)
        {
            GameObject explosion = explosionsMinePool.GetMineExplosion(gameObject.transform);
            explosion.GetComponent<ShockWave>().EnableExplosion();
            gameObject.SetActive(false);
        }
    }

    void Destroy()
    {
        GameObject explosion = explosionsMinePool.GetMineExplosion(gameObject.transform);
        explosion.GetComponent<ShockWave>().EnableExplosion();
        gameObject.SetActive(false);
    }

    public void ActiveMine()
    {
        droopFlag = true;
    }

    public void Reset()
    {
        droopFlag = false;
        lifeTime = resetTime;
        gameObject.SetActive(true);
    }
}

