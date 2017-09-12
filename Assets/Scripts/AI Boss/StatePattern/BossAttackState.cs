
using UnityEngine;

public class BossAttackState : BossState
{
    private bool callAnimation = false;

    public override void Handle(BossContext context)
    {
        AnimationControl(context);
        context.stateString = "BossAttack";
        Attack(context);
        Transition(context);
    }

    private void AnimationControl(BossContext context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("Melee2");
            callAnimation = true;
        }
    }

    private void Attack(BossContext context)
    {

        context.stateString = "Attack";
        context.nav.enabled = false;

        Vector3 direction = (context.target.position - context.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, lookRotation, Time.deltaTime * 4);

        bool canAttack = context.playerHealth.currentHealth > 0 && context.playerInSight == true && context.animFlagAttack == true;

        if (canAttack==true)
        {
            switch (context.BossColor)
            {
                case Context.EnemyType.VIRUS:
                    
                    break;

                case Context.EnemyType.PARASITE:
                   
                    break;

                case Context.EnemyType.BACTERIA:

                    break;
            }

            context.animFlagAttack = false;
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
