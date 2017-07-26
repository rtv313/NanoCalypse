using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesPool : MonoBehaviour {

    public GameObject mine;
    //public GameObject shockWave;
    public int minesAmount = 5;

    private List<GameObject> mines;
    //private List<GameObject> shockWaves;

    // Use this for initialization
    void Start ()
    {
        mines = new List<GameObject>();
        //shockWaves = new List<GameObject>();

        GameObject mineRef, shockRef;

        for (int i = 0; i < minesAmount; i++)
        {
            mineRef = Instantiate(mine);
            mineRef.SetActive(false);
            mines.Add(mineRef);
        }
    }

    private void PrepareMine(GameObject mine, Transform positionReference)
    {
        mine.transform.position = positionReference.position;
        mine.transform.rotation = positionReference.rotation;
        mine.GetComponent<Mine>().Reset();
    }

    public GameObject GetMine(Transform positionReference)
    {
        for (int i = 0; i < mines.Count; i++)
        {
            if (!mines[i].activeInHierarchy)
            {
                PrepareMine(mines[i], positionReference);
                return mines[i];
            }
        }

        GameObject newMine = Instantiate(mine);
        mines.Add(newMine);
        PrepareMine(newMine, positionReference);
        return newMine;
    }
}
