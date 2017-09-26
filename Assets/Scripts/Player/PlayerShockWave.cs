using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShockWave : MonoBehaviour
{
    public int minesLimit=5;
    //public float cooldown = 0.3f;
    public GameObject MinePosition;
    //private float mineHeat = 1f;
    private bool releaseButton = false;
    private MinesPool minePool;
    public int mineCounter = 0;

    void Start()
    {
        minePool = GetComponent<MinesPool>();
    }


    void Update()
    {
        CreateShockWave();
    }

    void CreateShockWave()
    {
        if(Input.GetAxis("Fire2") > 0.2f && mineCounter < minesLimit && releaseButton)
        {
            releaseButton = false;
            var mine = minePool.GetMine(MinePosition.transform);
            mine.GetComponent<Mine>().ActiveMine();
            ++mineCounter;
        }

        if (Input.GetAxis("Fire2") < 0.2f)
            releaseButton = true;
    }

}
