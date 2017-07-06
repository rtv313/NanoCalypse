using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFood : MonoBehaviour {

    public GameObject[] food;
    public int[] randomTimes;
    private bool flagCreate = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (flagCreate == true)
        {
            int selectRandom = Random.Range(0, randomTimes.Length);
            Invoke("CreateFoodDrop", randomTimes[selectRandom]);
            flagCreate = false;
        }
	}

    void CreateFoodDrop()
    {
        int selectRandom = Random.Range(0, food.Length);
        Instantiate(food[selectRandom], transform.position, Quaternion.identity);
        flagCreate = true;
    }
   
}
