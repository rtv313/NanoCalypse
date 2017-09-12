using UnityEngine;

public class BossWanderState : BossState
{
    private float time = 0;
    private bool callAnimation = false;

    public override void Handle(BossContext context)
    {
        context.stateString = "Wander";
        AnimationControl(context);
        Wander(context);
        Transition(context);
    }

    private void AnimationControl(BossContext context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("BossWalk");
            callAnimation = true;
        }
    }

    private void Wander(BossContext context)
    {
        context.nav.enabled = true;

        if (context.nav.remainingDistance < context.wanderObjectiveDistance)
        {

            context.patrol_wavePoint = Random.Range(0, context.path_objs_Wander.Length);

            while (context.patrol_wavePoint <= 0)
            {
                context.patrol_wavePoint = Random.Range(0, context.path_objs_Wander.Length);
            }
        }
        context.nav.SetDestination(context.path_objs_Wander[context.patrol_wavePoint].transform.position);
    }


    private void Transition(BossContext context)
    {
        time += Time.deltaTime;

        if (context.resetTimeForIdle == true)
        {
            time = 0;
        }

        if (context.life <= 0)
        {
            //context.state = new DeathState();
            return;
        }

        if (context.playerInSight == true && context.playerHealth.currentHealth > 0)
        {
            //context.state = new ChaseState();
            return;
        }

        if (context.wander == false)
        {
            //context.state = new PatrolState();
            return;
        }

        if (time >= context.idleTimer)
        {
            context.state = new BossIdleState();
            return;
        }
    }

}
