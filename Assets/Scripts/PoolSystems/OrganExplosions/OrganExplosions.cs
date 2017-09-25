using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OrganExplosions : MonoBehaviour {
    public GameObject SoundEffect;
    public int amount = 3;
    private List<GameObject> deadSounds;
    // Use this for initialization
    void Start()
    {
        deadSounds = new List<GameObject>();
        GameObject soundEffect;
        for (int i = 0; i < amount; i++)
        {
            soundEffect = Instantiate(SoundEffect);
            soundEffect.SetActive(false);
            deadSounds.Add(soundEffect);
        }
    }

    private void PrepareSound(GameObject sound, Vector3 positionReference)
    {
        sound.SetActive(true);
        sound.transform.position = positionReference;
        sound.GetComponent<OrganExplosionSound>().EnableSound();
        sound.GetComponent<AudioSource>().Play();
    }

    public GameObject GetDeadSound(Vector3 positionReference)
    {

        for (int i = 0; i < deadSounds.Count; i++)
        {
            if (!deadSounds[i].activeInHierarchy)
            {
                PrepareSound(deadSounds[i], positionReference);
                return deadSounds[i];
            }
        }

        GameObject newSound = Instantiate(SoundEffect);
        deadSounds.Add(newSound);
        PrepareSound(newSound, positionReference);
        return newSound;
    }
}
