using UnityEngine;

public class AttackState : State {
   
    private bool callAnimation = false;
   
    public override void Handle(Context context)
    {
        AnimationControl(context);
        Attack(context);
        Transition(context);
    }

    private void AnimationControl(Context context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("Attack");
            callAnimation = true;
        }
    }

    private void Attack(Context context)
    {
        
        context.stateString = "Attack";
        context.nav.enabled = false;
        
        Vector3 direction = (context.target.position - context.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        context.transform.rotation = Quaternion.Slerp(context.transform.rotation, lookRotation, Time.deltaTime * 4);

        bool canAttack = context.playerHealth.currentHealth > 0 && context.playerInSight == true && context.animFlagAttack == true;

        if (canAttack && context.enemyType != Context.EnemyType.BACTERIA)
        {
            switch (context.enemyType)
            {
                case Context.EnemyType.VIRUS:
                    virusAttack(context);
                    break;
                case Context.EnemyType.PARASITE:
                    parasiteAttack(context);
                    break;
            }

            context.animFlagAttack = false;
        }
        else if (context.enemyType == Context.EnemyType.BACTERIA)
        {
            bacteriaAttack(context);
        }
    }

    private void virusAttack(Context context)
    {
        VirusAttack componentAttack = context.GetComponent<VirusAttack>();
        componentAttack.Attack();
    }

    private void bacteriaAttack(Context context)
    {
        BacteriaAttack componentAttack = context.GetComponent<BacteriaAttack>();
        componentAttack.Attack(context);
    }

    private void parasiteAttack(Context context)
    {
        ParasiteAttack componentAttack = context.GetComponent<ParasiteAttack>();
        componentAttack.Attack(context);
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

        float dist = Vector3.Distance(context.target.position, context.transform.position);

        if (dist > context.attackDistance)
        {
            context.state = new ChaseState();
            return;
        }
    }

    
}
