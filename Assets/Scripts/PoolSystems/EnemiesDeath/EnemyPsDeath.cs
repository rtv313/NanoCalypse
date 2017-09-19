using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPsDeath : MonoBehaviour {

    public float disableTime = 1.0f;
    private CameraShake cam;

    void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    public void EnablePS()
    {
        gameObject.SetActive(true);
        transform.gameObject.GetComponent<ParticleSystem>().Emit(1);
        Invoke("DisablePS", disableTime);
        cam.fireShake(0);
    }

    private void DisablePS()
    {
        gameObject.SetActive(false);
    }
}

