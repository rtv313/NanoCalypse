using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShockWave : MonoBehaviour
{
    public GameObject MinePrefab;
    public int minesLimit=5;
    public float cooldown = 0.3f;
    public GameObject MinePosition;
    private float mineHeat = 1f;


    void Update()
    {
        CreateShockWave();
        CoolDown();
    }

    void CreateShockWave()
    {
        if(Input.GetKeyDown(KeyCode.Q) && mineHeat < minesLimit)
        {
            Instantiate(MinePrefab, MinePosition.transform.position,Quaternion.identity);
            mineHeat++;
        }
    }

    void CoolDown()
    {
        mineHeat -= cooldown * Time.deltaTime;
        if (mineHeat <= 1)
        {
            mineHeat = 1.0f;
        }
    }
}
