﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteSpawnManager : MonoBehaviour {

    public GameObject spawnPointOne;
    public GameObject spawnPointTwo;
    public GameObject spawnPointThree;
   
    public void CreateParasites()
    {
        
        if (spawnPointOne.GetComponent<ParasiteSpawn>().canInstantiate == true)
        {
            spawnPointOne.GetComponent<ParasiteSpawn>().CreateParasites();
        }

        if (spawnPointTwo.GetComponent<ParasiteSpawn>().canInstantiate)
        {
            spawnPointTwo.GetComponent<ParasiteSpawn>().CreateParasites();
        }

        if (spawnPointThree.GetComponent<ParasiteSpawn>().canInstantiate)
        {
            spawnPointThree.GetComponent<ParasiteSpawn>().CreateParasites();
        }
    }
}