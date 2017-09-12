
using UnityEngine;

public class BossDeathState : BossState {

    private bool callAnimation = false;

    public override void Handle(BossContext context)
    {
        context.stateString = "BossDeath";
        context.nav.enabled = false;
        AnimationControl(context);

    }

    private void AnimationControl(BossContext context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("BossDead");
            callAnimation = true;
        }
    }
}
