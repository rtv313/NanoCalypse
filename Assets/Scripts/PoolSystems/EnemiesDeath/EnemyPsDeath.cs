using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPsDeath : MonoBehaviour {

    public float disableTime = 1.0f;

    public void EnablePS()
    {
        gameObject.SetActive(true);
        transform.gameObject.GetComponent<ParticleSystem>().Emit(1);
        Invoke("DisablePS", disableTime);
    }

    private void DisablePS()
    {
        gameObject.SetActive(false);
    }
}

