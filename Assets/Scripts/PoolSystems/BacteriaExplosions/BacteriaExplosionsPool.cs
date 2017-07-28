using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaExplosionsPool : MonoBehaviour {

    public GameObject bacteriaExplosion;
    public int bacteriaExplosionsAmount = 10;
    private List<GameObject> bacteriaExplosions;

    // Use this for initialization
    void Start ()
    {
        bacteriaExplosions = new List<GameObject>();
        GameObject bacExplosionRef;

        for (int i = 0; i < bacteriaExplosionsAmount; i++)
        {
            bacExplosionRef = Instantiate(bacteriaExplosion);
            bacExplosionRef.SetActive(false);
            bacteriaExplosions.Add(bacExplosionRef);
        }
    }

    private void PrepareExplosion(GameObject bacteriaExplosion, Transform positionReference)
    {
        bacteriaExplosion.transform.position = positionReference.position;
        bacteriaExplosion.transform.rotation = positionReference.rotation;
        bacteriaExplosion.GetComponent<BacteriaExplosion>().ResetExplosionLifeTime();
    }

    public GameObject GetBacteriaExplosion(Transform positionReference)
    {
        for (int i = 0; i < bacteriaExplosions.Count; i++)
        {
            if (!bacteriaExplosions[i].activeInHierarchy)
            {
                PrepareExplosion(bacteriaExplosions[i], positionReference);
                return bacteriaExplosions[i];
            }
        }

        GameObject newBacteriaExplosion = Instantiate(bacteriaExplosion);
        bacteriaExplosions.Add(newBacteriaExplosion);
        PrepareExplosion(newBacteriaExplosion, positionReference);
        return newBacteriaExplosion;
    }
}
