using UnityEngine;

public class BossIdleState : BossState {

    public float time = 0;
    private bool callAnimation = false;

    public override void Handle(BossContext context)
    {
        AnimationControl(context);
        context.stateString = "BossIdle";
        Idle(context);
        Transition(context);
    }

    private void AnimationControl(BossContext context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("Idle");
            callAnimation = true;
        }
    }

    private void Idle(BossContext context)
    {
        context.nav.enabled = false;
    }

    private void Transition(BossContext context)
    {
        time += Time.deltaTime;

        if (context.life <= 0)
        {
            context.state = new BossDeathState();
            return;
        }

        if (context.playerInSight == true && context.playerHealth.currentHealth > 0)
        {
            context.state = new BossChaseState();
            return;
        }

        if (time >= context.idleTime && context.wander == true)
        {
            context.state = new BossWanderState();
        }
    }

}
