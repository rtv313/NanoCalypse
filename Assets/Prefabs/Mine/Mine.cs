using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
   
    public GameObject Explosion;
    public float lifeTime = 30f;
    private float resetTime = 0f;
    private bool droopFlag = false;

    void Awake()
    {
        resetTime = lifeTime;
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
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Vector3.Distance(other.transform.position, transform.position) <= 2)
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    void Destroy()
    {
        Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
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

