using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBossAttack : MonoBehaviour {

    public GameObject meshBody;
    public float brightness = 1.6f;
    public float changeTime = 10.0f;

    public float laserAttackDistance = 15.0f;
    public float airAttackDistance = 18.0f;


    private bool flagAirAttack = false;
    private BossContext context;
    private float controlTime = 0.0f;

    private Material mat;
    // Use this for initialization
    void Start()
    {
        context = GetComponent<BossContext>();
        mat = meshBody.GetComponent<Renderer>().material;
        SetInmmune();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SelectSpecialAttack()
    {

        int random = Random.Range(1, 3);
        random = 1;
        switch (random)
        {
            case 1: // por tiempo
                context.bossColor = BossContext.BossStateColor.INMUNE;
                mat.SetColor("_EmissionColor", Color.red * brightness);
                break;

            case 2: // cuando la animacion acabe 
                context.bossColor = BossContext.BossStateColor.BACTERIA;
                mat.SetColor("_EmissionColor", Color.green * brightness);
                flagAirAttack = true;
                break;

            case 3:
                context.bossColor = BossContext.BossStateColor.PARASITE;
                mat.SetColor("_EmissionColor", Color.blue * brightness);

                break;
        }
    }

    public void BossAirAttackAnimationFinished() // la animacion de ataque aereo a terminado
    {
        flagAirAttack = false;
    }

    public void SetInmmune()
    {
        context.attackDistance = context.meleeAttackDistance;
        context.bossColor = BossContext.BossStateColor.INMUNE;
        mat.SetColor("_EmissionColor", Color.yellow * brightness);
    } 

}
