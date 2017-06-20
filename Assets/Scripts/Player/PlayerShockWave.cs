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
    private bool releaseButton = false;


    void Update()
    {
        CreateShockWave();
        CoolDown();
    }

    void CreateShockWave()
    {
        if(Input.GetAxis("Fire2") > 0.2f && mineHeat < minesLimit && releaseButton)
        {
            releaseButton = false;
            Instantiate(MinePrefab, MinePosition.transform.position,MinePosition.transform.rotation);
            mineHeat++;
        }
        if (Input.GetAxis("Fire2") < 0.2f) releaseButton = true;
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
