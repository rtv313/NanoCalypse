
using UnityEngine;

public class BossChaseState : BossState
{

    private bool callAnimation = false;

    public override void Handle(BossContext context)
    {
        context.stateString = "BossChase";
        AnimationControl(context);
        ChasePlayer(context);
        Transition(context);
    }

    private void AnimationControl(BossContext context)
    {
        if (callAnimation == false)
        {
            float dist = Vector3.Distance(context.target.position, context.transform.position);

            if (dist > context.attackDistance)
                context.animator.SetTrigger("BossWalk");

            callAnimation = true;
        }
    }


    private void ChasePlayer(BossContext context)
    {
        context.nav.enabled = true;
        context.nav.SetDestination(context.target.position);
    }

    private void Transition(BossContext context)
    {
        if (context.life <= 0)
        {
            context.state = new BossDeathState();
            return;
        }

        float dist = Vector3.Distance(context.target.position, context.transform.position);

        if (dist < context.attackDistance)
        {
            context.state = new BossAttackState();
            return;
        }

        if (context.playerInSight == false)
        {
            context.state = new BossWanderState();
            return;
        }
    }

}
