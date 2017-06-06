using UnityEngine;
using System.Collections;

public class ParticleSystemControl : MonoBehaviour {
    public float destroySystemTime = 10.0f;
    

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(destroySystemTime);
        Destroy(gameObject);
    }
}
