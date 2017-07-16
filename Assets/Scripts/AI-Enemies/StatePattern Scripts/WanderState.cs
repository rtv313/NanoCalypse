using UnityEngine;

public class WanderState : State
{
    private float time = 0;
    private bool callAnimation = false;
    public override void Handle(Context context)
    {
        context.stateString = "Wander";
        int idleState = Animator.StringToHash("Idle");
        if (context.animator.GetCurrentAnimatorStateInfo(0).nameHash == idleState)
        {
            callAnimation = false;
        }
        AnimationControl(context);
        Wander(context);
        Transition(context);
    }

    private void AnimationControl(Context context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("Walk");
            callAnimation = true;
        }
    }

    private void Wander(Context context)
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

    private void Transition(Context context)
    {
        time += Time.deltaTime;

        if (context.resetTimeForIdle == true)
        {
            time = 0;
        }

        if (context.life <= 0)
        {
            context.state = new DeathState();
            return;
        }

        if (context.playerInSight == true && context.playerHealth.currentHealth > 0)
        {
            context.state = new ChaseState();
            return;
        }

        if (context.wander == false)
        {
            context.state = new PatrolState();
            return;
        }

        if (time >= context.idleTimer)
        {
            context.state = new IdleState();
            return;
        }
    }
}