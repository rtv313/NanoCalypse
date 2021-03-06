﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirAttackInterface : MonoBehaviour {


    public GameObject boss;

    public void CreateExplosions()
    {
        boss.GetComponent<BossAirAttack>().CreateExplosions();
        boss.GetComponent<BossContext>().FlagAirAttack = true;
        boss.GetComponent<BossContext>().AnimationInProcess = false;
    }
}
