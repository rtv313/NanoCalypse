using UnityEngine;

public class MutationState : State {

    public override void Handle(Context context)
    {
        context.stateString = "Mutate";
        Mutate(context);
        Transition(context);
    }

    private void Mutate(Context context)
    {
        switch (context.enemyType)
        {
            case Context.EnemyType.VIRUS:
                virusMutation(context);
                break;

            case Context.EnemyType.BACTERIA:
                bacteriaMutation(context);
                break;

            case Context.EnemyType.PARASITE:
                parasiteMutation(context);
                break;
        }

    }

    private void virusMutation(Context context)
    {
        context.attackDamage = context.attackDamage * 5;
        //change bullet color
        //change enemy color with shader
    }

    private void bacteriaMutation(Context context)
    {
        context.nav.speed = context.nav.speed * 5;
        //change enemy color with shader
    }

    private void parasiteMutation(Context context)
    {
        CreateParasite createParasite = new CreateParasite();
        createParasite.createParasite(context.parasite);
        
    }

    private void Transition(Context context)
    {
        context.state = new ChaseState();
    }
}

public class CreateParasite:MonoBehaviour
{
    public void createParasite(GameObject parasitePrefab)
    {
        Vector3 position = transform.position;
        position.z -= 2;
        Instantiate(parasitePrefab, position, transform.rotation);
        position.z = 0;
        position.z += 2;
        Instantiate(parasitePrefab,position, transform.rotation);
    }
}