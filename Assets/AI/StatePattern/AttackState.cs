using UnityEngine;
public class AttackState : State {
    private float timer=10;

    public override void Handle(Context context)
    {
        Transition(context);
        Attack(context);
    }

    private void Attack(Context context)
    {
        context.stateString = "Attack";
        context.nav.enabled = false;
        timer += Time.deltaTime;

        if (context.playerHealth.currentHealth > 0 && timer >= context.timeBetweenAttacks)
        {
          context.playerHealth.TakeDamage(context.attackDamage);
          timer = 0;
        }
    }

    private void Transition(Context context)
    {
       
        if (context.life <= 0)
        {
            context.state = new DeathState();
            return;
        }

        if (context.playerHealth.currentHealth <= 0)
        {
            context.state = new IdleState();
            return;
        }

        context.nav.enabled = true;

        if (context.nav.remainingDistance > context.attackDistance)
        {
            context.state = new ChaseState();
            return;
        }

        
    }
}
