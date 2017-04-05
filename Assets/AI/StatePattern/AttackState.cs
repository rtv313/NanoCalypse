using UnityEngine;
public class AttackState : State {
    private float timer;

    public override void Handle(Context context)
    {
        Transition(context);
        Attack(context);
    }

    private void Attack(Context context)
    {
        context.nav.enabled = false;
        timer += Time.deltaTime;

        if (context.playerHealth.currentHealth > 0 && timer >= context.timeBetweenAttacks)
        {
          context.playerHealth.TakeDamage(context.attackDamage);
        }

        if (context.playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.

        }


    }

    private void Transition(Context context)
    {
        if (context.nav.remainingDistance > context.attackDistance)
        {
            context.nav.enabled = true;
            context.state = new ChaseState();
        }

        if (context.playerHealth.currentHealth <= 0)
        {
            context.state = new IdleState();
        }
    }
}
