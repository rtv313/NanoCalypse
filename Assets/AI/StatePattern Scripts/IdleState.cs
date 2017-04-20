using UnityEngine;

public class IdleState : State {
    public float time = 0;
    private bool callAnimation = false;

    public override void Handle(Context context)
    {
        AnimationControl(context);
        context.stateString = "Idle";
        Idle(context);
        Transition(context);
    }

    private void AnimationControl(Context context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("Idle");
            callAnimation = true;
        }
    }

    private void Idle(Context context)
    {
        context.nav.enabled = false;
    }

    private void Transition(Context context)
    {
        time += Time.deltaTime;

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

        if (time >= context.idleTime && context.wander==true)
        {
            context.state = new WanderState();
        }

        if (time >= context.idleTime && context.wander == false)
        {
           
            context.state = new PatrolState();
        }
    }
}
