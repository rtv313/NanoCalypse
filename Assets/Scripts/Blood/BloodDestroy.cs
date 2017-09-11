using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDestroy : MonoBehaviour {

    public float destroyTime = 1.0f;
    public GameObject subPs;
    public GameObject subPs2;

    public void PrepareBlood()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<ParticleSystem>().Emit(1);
        subPs.GetComponent<ParticleSystem>().Emit(1);
        subPs2.GetComponent<ParticleSystem>().Emit(1);
        Invoke("DeactivateBlood", destroyTime);
    }

    void DeactivateBlood()
    {
        gameObject.SetActive(false);
    }
}
