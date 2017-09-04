using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDeathPSpool : MonoBehaviour
{
    public GameObject deathVirus;
    public GameObject deathParasite;
    public int amount;
    private List<GameObject> deathVirusPSs;
    private List<GameObject> deathParasitePSs;
    

    // Use this for initialization
    void Start ()
    {
        deathVirusPSs = new List<GameObject>();
        deathParasitePSs = new List<GameObject>();
        

        GameObject deathPS;

        for (int i = 0; i < amount; i++)
        {
            deathPS = Instantiate(deathVirus);
            deathPS.SetActive(false);
            deathVirusPSs.Add(deathPS);

            deathPS = Instantiate(deathParasite);
            deathPS.SetActive(false);
            deathParasitePSs.Add(deathPS);

          }

    }

    private void PrepareDeathEffect(GameObject deathPS,Transform positionReference)
    {
        deathPS.transform.position = positionReference.transform.position;
        deathPS.transform.rotation = positionReference.rotation;
        deathPS.GetComponent<EnemyPsDeath>().EnablePS();

    }

    public GameObject GetDeathEffect(Transform positionReference, Context.EnemyType deathType)
    {

        switch (deathType)
        {
            case Context.EnemyType.VIRUS:

                for (int i = 0; i < deathVirusPSs.Count; i++)
                {
                    if (!deathVirusPSs[i].activeInHierarchy)
                    {
                        PrepareDeathEffect(deathVirusPSs[i], positionReference);
                        return deathVirusPSs[i];
                    }
                }

                GameObject newDeathVirus = Instantiate(deathVirus);
                deathVirusPSs.Add(newDeathVirus);
                PrepareDeathEffect(newDeathVirus, positionReference);
                return newDeathVirus;

            case Context.EnemyType.PARASITE:

                for (int i = 0; i < deathParasitePSs.Count; i++)
                {
                    if (!deathParasitePSs[i].activeInHierarchy)
                    {
                        PrepareDeathEffect(deathParasitePSs[i], positionReference);
                        return deathParasitePSs[i];
                    }
                }

                GameObject newDeathParasite = Instantiate(deathParasite);
                deathParasitePSs.Add(newDeathParasite);
                PrepareDeathEffect(newDeathParasite, positionReference);
                return newDeathParasite;

            default:
                return null;
        }
        
    }
	
}
