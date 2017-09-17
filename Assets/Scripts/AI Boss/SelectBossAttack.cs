using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBossAttack : MonoBehaviour {

    public GameObject meshBody;
    public float brightness = 1.6f;
    public float changeTime = 10.0f;

    public float laserAttackDistance = 15.0f;
    public float airAttackDistance = 18.0f;


  
    private BossContext context;
    private float controlTime = 0.0f;

    public float timeInInmmune = 10.0f;
    public float timeInRed = 5.0f;
    private float changeStateTime = 0.0f;
    private float timer = 0.0f;

    private Material mat;
    private Material matOutlined;
    // Use this for initialization
    void Start()
    {
        context = GetComponent<BossContext>();
        mat = meshBody.GetComponent<Renderer>().material;
        matOutlined = meshBody.GetComponent<Renderer>().materials[1];
        SetInmmune();
    }

    // Update is called once per frame
    void Update()
    {
        //if (context.AnimationInProcess == true)
        //    return;

        if (timer >= changeStateTime)
        {
            if (context.bossColor == BossContext.BossStateColor.INMUNE)
            {
                SelectSpecialAttack();
            }
            else
            {
                AnyStateToInmmune();
            }
            timer = 0.0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void SelectSpecialAttack()
    {
        gameObject.GetComponent<BossBulletAttack>().enabled = false;
        int random = Random.Range(1, 4);
       
        switch (random)
        {
            case 1: // por tiempo
                context.bossColor = BossContext.BossStateColor.VIRUS;
                mat.SetColor("_EmissionColor", Color.red * brightness);
                matOutlined.SetColor("_OutlineColor", Color.red);
                changeStateTime = timeInRed;
                context.attackDistance = context.laserAttackDistance;
                break;

            case 2: // cuando la animacion acabe 
                context.bossColor = BossContext.BossStateColor.BACTERIA;
                mat.SetColor("_EmissionColor", Color.green * brightness);
                matOutlined.SetColor("_OutlineColor", Color.green);
                context.FlagAirAttack = false;
                context.attackDistance = context.laserAttackDistance;
                break;

            case 3:
                context.bossColor = BossContext.BossStateColor.PARASITE;
                mat.SetColor("_EmissionColor", Color.blue * brightness);
                matOutlined.SetColor("_OutlineColor", Color.blue);
                context.attackDistance = context.laserAttackDistance;
                break;
        }
    }

    public void SetInmmune()
    {
        context.attackDistance = context.meleeAttackDistance;
        context.bossColor = BossContext.BossStateColor.INMUNE;
        mat.SetColor("_EmissionColor", Color.yellow * brightness);
        matOutlined.SetColor("_OutlineColor", Color.yellow);
        changeStateTime = timeInInmmune;
    }


    private void AnyStateToInmmune()
    {
        changeStateTime = timeInInmmune;
        context.attackDistance = context.meleeAttackDistance;
        SetInmmune();
        gameObject.GetComponent<BossBulletAttack>().enabled = true;
    }
}
