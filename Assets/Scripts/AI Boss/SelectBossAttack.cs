using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBossAttack : MonoBehaviour {

    public GameObject meshBody;
    public float brightness = 1.6f;
    public float changeTime = 10.0f;

    public float laserAttackDistance = 15.0f;
    public float airAttackDistance = 18.0f;
    public float meleeAttackDistance = 7.0f;

    private bool  flagChangeAttack = false;
    private BossContext context;
    private float controlTime =0.0f;
    // Use this for initialization
    void Start ()
    {
        context = GetComponent<BossContext>();
    }
	
	// Update is called once per frame
	void Update () {

        if (flagChangeAttack == true)
        {
            SelectAttackType();
            flagChangeAttack = false;
        }
        else 
        {
            controlTime += Time.deltaTime;
            if (controlTime >= changeTime)
            {
                flagChangeAttack = true;
                controlTime = 0.0f;
            }
        }
    }

    private void SelectAttackType()
    {
        int random = Random.Range(1,5);
        Material mat = meshBody.GetComponent<Renderer>().material;

        switch (random)
        {
            case 1:
                context.BossColor = Context.EnemyType.VIRUS;
                context.attackDistance = laserAttackDistance;
                mat.SetColor("_EmissionColor", Color.red * brightness);
                break;

            case 2:
                context.BossColor = Context.EnemyType.PARASITE;
                context.attackDistance = meleeAttackDistance;
                mat.SetColor("_EmissionColor", Color.blue * brightness);
                break;

            case 3:
                context.BossColor = Context.EnemyType.BACTERIA;
                context.attackDistance = airAttackDistance;
                mat.SetColor("_EmissionColor", Color.green * brightness);
                break;
         }
     }

}
