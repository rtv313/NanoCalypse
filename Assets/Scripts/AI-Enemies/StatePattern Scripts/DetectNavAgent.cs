using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectNavAgent : MonoBehaviour {
    public Context context;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            context.resetTimeForIdle = true;
            context.wanderObjectiveDistance = 2.0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            context.resetTimeForIdle = false;
            context.wanderObjectiveDistance = 0.5f;
        }
    }
}
