
public class ChaseState : State
{
    public override void Handle(Context context)
    {
        context.stateString = "Chase";
        ChasePlayer(context);
        Transition(context);
    }

    private void ChasePlayer(Context context)
    {
      context.nav.SetDestination(context.player.position);
    }

    private void Transition(Context context)
    {
        if (context.life <= 0)
        {
            context.state = new DeathState();
            return;
        }

        //if (context.nav.remainingDistance < context.attackDistance)
        //{
        //    context.state = new AttackState();
        //}

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

