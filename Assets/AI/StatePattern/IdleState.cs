using UnityEngine;

public class IdleState : State {
    public float time = 0;
    public override void Handle(Context context)
    {
        context.stateString = "Idle";
        Idle(context);
        Transition(context);
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

        if (time >= context.idleTime && context.wander==true)
        {
            context.nav.enabled = true;
            context.state = new WanderState();
        }

        if (time >= context.idleTime && context.wander == false)
        {
            context.nav.enabled = true;
            context.state = new PatrolState();
        }
    }
}
