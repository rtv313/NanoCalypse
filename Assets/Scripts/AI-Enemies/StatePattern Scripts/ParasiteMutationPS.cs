using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteMutationPS : MonoBehaviour {

    public GameObject mutationPSBubbles;
    public GameObject mutationPSFog;
    public float timeToDeactivatePS = 3.0f;

    public bool activate = false;

    void Start()
    {
        mutationPSBubbles.SetActive(false);
        mutationPSFog.SetActive(false);
    }

    void Update()
    {
        if (activate)
        {
            ActivatePSMutation();
            activate = false;
        }
    }

    public void ActivatePSMutation()
    {
        mutationPSBubbles.SetActive(true);
        mutationPSBubbles.GetComponent<ParticleSystem>().Emit(1);

        mutationPSFog.SetActive(true);
        mutationPSFog.GetComponent<ParticleSystem>().Emit(1);

        Invoke("DeactivatePS", timeToDeactivatePS);
    }

    private void DeactivatePS()
    {
        mutationPSBubbles.SetActive(false);
        mutationPSFog.SetActive(false);
    }

}
