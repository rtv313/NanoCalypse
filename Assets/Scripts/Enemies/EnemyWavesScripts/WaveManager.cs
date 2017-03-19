using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPointListWrapper
{
    public string name;
    public  List<GameObject> spawnPoints;
}

public class WaveManager : MonoBehaviour {
    public bool activate = true;
    public List<SpawnPointListWrapper> wavesSpawnPointsLists;

    public void activateLists(List<int> activateWavesList)
    {
        foreach (int x in activateWavesList)
        {
            SpawnPointListWrapper listSpawnPoints = wavesSpawnPointsLists[x];

            foreach(GameObject i in listSpawnPoints.spawnPoints)
            {
                i.SetActive(true);
            }
        }
    }

    public void deactivateLists(List<int> deactivateFaseList)
    {
        foreach (int x in deactivateFaseList)
        {
            SpawnPointListWrapper listSpawnPoints = wavesSpawnPointsLists[x];

            foreach (GameObject i in listSpawnPoints.spawnPoints)
            {
                i.SetActive(false);
            }
        }
    }

    public void destroyWavesSystem()
    {
        foreach (SpawnPointListWrapper spawnPointList in wavesSpawnPointsLists)
        {
            foreach (GameObject spawnPoint in spawnPointList.spawnPoints)
            {
                Destroy(spawnPoint);
            }
        }
    }

    void Update() //for testing only
    {
        List<int> waves = new List<int>();
        waves.Add(0);
        waves.Add(1);

        if (activate) {
             activateLists(waves);
        } else{
            deactivateLists(waves);  
        }
    }
}
