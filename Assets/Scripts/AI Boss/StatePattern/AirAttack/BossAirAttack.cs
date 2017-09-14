using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAirAttack : MonoBehaviour {

    public Transform positionOneExplosion;
    public Transform positionTwoExplosion;
    public Transform positionThreeExplosion;
    public Transform PositionFourExplosion;
    public Transform PositionFiveExplosion;
    public Transform PositionSixExplosion;

    private bool flagAttack = false;
    private BacteriaExplosionsPool bacteriaExplosionsPool;
    FoodAttackManager foodManager;
    // Use this for initialization
    void Start()
    {

        bacteriaExplosionsPool = GameObject.FindGameObjectWithTag("BacteriaExpPool").GetComponent<BacteriaExplosionsPool>();
        foodManager = GameObject.FindGameObjectWithTag("Player").GetComponent<FoodAttackManager>(); ;
    }

    public void CreateExplosions()
    {
        GameObject explosion;

        explosion = bacteriaExplosionsPool.GetBacteriaExplosion(positionOneExplosion);
        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();

        explosion = bacteriaExplosionsPool.GetBacteriaExplosion(positionTwoExplosion);
        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();

        explosion = bacteriaExplosionsPool.GetBacteriaExplosion(positionThreeExplosion);
        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();

        explosion = bacteriaExplosionsPool.GetBacteriaExplosion(PositionFourExplosion);
        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();

        explosion = bacteriaExplosionsPool.GetBacteriaExplosion(PositionFiveExplosion);
        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();

        explosion = bacteriaExplosionsPool.GetBacteriaExplosion(PositionSixExplosion);
        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();

        foodManager.ResetAttack();
        foodManager.waves = 1;
        foodManager.timeBetweenAttacks = 3;
        foodManager.startAttack = true;
    } 
}
