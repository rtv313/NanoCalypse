using UnityEngine;

public class ChaseState : State
{
    private bool callAnimation = false;

    public override void Handle(Context context)
    {
        context.stateString = "Chase";
        AnimationControl(context);
        ChasePlayer(context);
        Transition(context);
    }

    private void AnimationControl(Context context)
    {
        if (callAnimation == false)
        {
            if (context.enemyType == Context.EnemyType.BACTERIA && context.mutaded==true)
            {
                context.animator.SetTrigger("Run");
            }
            else
            {
                context.animator.SetTrigger("Walk");
            }
            callAnimation = true;
        }
    }

    private void ChasePlayer(Context context)
    {
        context.nav.enabled = true;
        context.nav.SetDestination(context.target.position);
    }

    private void Transition(Context context)
    {
        if (context.life <= 0)
        {
            context.state = new DeathState();
            return;
        }

        float dist = Vector3.Distance(context.target.position, context.transform.position);

        if (dist < context.attackDistance)
        {
            context.state = new AttackState();
        }

        if (context.playerInSight == false && context.wander == true)
        {
            context.state = new WanderState();
        }

        if (context.playerInSight == false && context.wander == false)
        {
            context.state = new PatrolState();
        }
    }
}

