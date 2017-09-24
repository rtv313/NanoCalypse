using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganExplosionSound : MonoBehaviour {

    public void EnableSound()
    {
        Invoke("DisableSound", 3.0f);
    }

    private void DisableSound()
    {
        gameObject.SetActive(false);
    }
}
