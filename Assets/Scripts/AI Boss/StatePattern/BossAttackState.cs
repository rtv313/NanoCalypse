
using UnityEngine;

public class BossAttackState : BossState
{
    public override void Handle(BossContext context)
    {
       context.stateString = "BossAttack";
        Transition(context);
        Attack(context);
   
       
    }

    private void Attack(BossContext context)
    {
        context.nav.enabled = false;

        Vector3 direction = (context.target.position - context.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, lookRotation, Time.deltaTime * 4);

        bool canAttack = context.playerHealth.currentHealth > 0 && context.playerInSight == true;

        if (canAttack==true)
        {
            switch (context.bossColor)
            {
                case BossContext.BossStateColor.INMUNE:

                    if (context.FlagMeleeAttack == false)
                    {
                        context.animator.SetTrigger("Melee1");
                        context.FlagMeleeAttack = true;
                        context.AnimationInProcess = true;
                    }

                    break;

                case BossContext.BossStateColor.VIRUS:

                    if (context.FlagMeleeAttack == false)
                    {
                        context.animator.SetTrigger("Melee1");
                        context.FlagMeleeAttack = true;
                        context.AnimationInProcess = true;
                    }
                    break;

                case BossContext.BossStateColor.PARASITE:

                    if (context.FlagSpawnAttack == false)
                    {
                        context.animator.SetTrigger("Melee2");
                        context.FlagSpawnAttack = true;
                        context.AnimationInProcess = true;
                    }

                    break;

                case BossContext.BossStateColor.BACTERIA:
                    if (context.FlagAirAttack == false)
                    {
                        context.animator.SetTrigger("DistanceAttack");
                        context.FlagAirAttack = true;
                        context.AnimationInProcess = true;
                    }
                    break;
            }
        }
    }

    private void Transition(BossContext context)
    {

        if (context.life <= 0)
        {
           
            context.state = new BossDeathState();
            return;
        }

        if (context.playerHealth.currentHealth <= 0)
        {
           
            context.state = new BossIdleState();
            return;
        }

        context.nav.enabled = true;

        float dist = Vector3.Distance(context.target.position, context.transform.position);

        if (dist > context.attackDistance && context.AnimationInProcess == false)
        {
           
            context.state = new BossChaseState();
            return;
        }
    }

}
