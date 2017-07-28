using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsMinePool : MonoBehaviour {

    public GameObject explosion;
    public int explosionsAmount = 5;
    private List<GameObject> mineExplosions;

    // Use this for initialization
    void Start ()
    {

        mineExplosions = new List<GameObject>();
        GameObject mineExpRef;

        for (int i = 0; i < explosionsAmount; i++)
        {
            mineExpRef = Instantiate(explosion);
            mineExpRef.SetActive(false);
            mineExplosions.Add(mineExpRef);
        }

	}

    private void PrepareExplosion(GameObject mineExplosion, Transform positionReference)
    {
        mineExplosion.transform.position = positionReference.position;
        mineExplosion.transform.rotation = positionReference.rotation;
        mineExplosion.GetComponent<ShockWave>().ResetExplosionLifeTime();
    }

    public GameObject GetMineExplosion(Transform positionReference)
    {
        for (int i = 0; i < mineExplosions.Count; i++)
        {
            if (!mineExplosions[i].activeInHierarchy)
            {
                PrepareExplosion(mineExplosions[i], positionReference);
                return mineExplosions[i];
            }
        }

        GameObject newMineExplosion = Instantiate(explosion);
        mineExplosions.Add(newMineExplosion);
        PrepareExplosion(newMineExplosion, positionReference);
        return newMineExplosion;
    }
	
}
