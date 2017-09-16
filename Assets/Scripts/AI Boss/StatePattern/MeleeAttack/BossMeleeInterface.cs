using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeInterface : MonoBehaviour {
    public BossContext context;
    public GameObject meleeAttackCollider;
    public GameObject meleeAttackCollider2;

    void Start()
    {
        meleeAttackCollider.GetComponent<Collider>().enabled = false;
        meleeAttackCollider2.GetComponent<Collider>().enabled = false;
    }

    public void ActivateMeleeCollider()
    {
        meleeAttackCollider.GetComponent<Collider>().enabled = true;
    }

    public void DeActivateMeleeCollider()
    {
        meleeAttackCollider.GetComponent<Collider>().enabled = false;
    }

    public void ActivateMeleeCollider2()
    {
        meleeAttackCollider2.GetComponent<Collider>().enabled = true;
    }

    public void DeActivateMeleeCollider2()
    {
        meleeAttackCollider2.GetComponent<Collider>().enabled = false;
    }

    public void CallAnimationEnded()
    {
        context.FlagMeleeAttack = false;
        context.AnimationInProcess = false;
    }
}
