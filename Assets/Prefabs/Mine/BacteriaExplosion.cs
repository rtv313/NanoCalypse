using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaExplosion : MonoBehaviour {

    public float explosionLifeTime = 2.0f;
    private float resetTime = 0f;
    private bool explodeFlag = false;

    private CameraShake cam;

    void Awake()
    {
        resetTime = explosionLifeTime;
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
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
      Invoke("activateTrigger", 0.4f);
    }

    void activateTrigger()
    {
        explodeFlag = true;
        explosionLifeTime = resetTime;
        cam.fireShake(1);
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
        explodeFlag = true;
    }
}
