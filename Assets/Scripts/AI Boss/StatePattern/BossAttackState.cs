
using UnityEngine;

public class BossAttackState : BossState
{
    private bool callAnimation = false;

    public override void Handle(BossContext context)
    {
       context.stateString = "BossAttack";
       Attack(context);
        //AnimationControl(context);
       Transition(context);
    }

    //private void AnimationControl(BossContext context)
    //{
    //    if (callAnimation == false)
    //    {
    //        context.animator.SetTrigger("Melee2");
    //        callAnimation = true;
    //    }
    //}

    private void Attack(BossContext context)
    {
        context.nav.enabled = false;

        Vector3 direction = (context.target.position - context.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, lookRotation, Time.deltaTime * 4);

        bool canAttack = context.playerHealth.currentHealth > 0 && context.playerInSight == true;

        if (canAttack==true)
        {
            switch (context.BossColor)
            {
                case Context.EnemyType.VIRUS:
                    
                    break;

                case Context.EnemyType.PARASITE:

                    if (context.CallMeleeAttackAnimation == false)
                    {
                        int randomChoice = Random.Range(1, 3);

                        switch (randomChoice)
                        {
                            case 1:
                                context.animator.SetTrigger("Melee1");
                                break;

                            case 2:
                                context.animator.SetTrigger("Melee2");
                                break;
                        }

                        context.CallMeleeAttackAnimation = true;
                    }
                    break;

                case Context.EnemyType.BACTERIA:

                    if (callAnimation == false)
                    {
                       context.animator.SetTrigger("DistanceAttack");
                       callAnimation = true;
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

        if (dist > context.attackDistance)
        {
           
            context.state = new BossChaseState();
            return;
        }
    }

}
