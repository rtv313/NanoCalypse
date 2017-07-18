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
        if (context.mutaded == true)
            return;

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
        context.mutaded = true;
    }

    private void virusMutation(Context context)
    {
        //Change enemy color with shader
        Transform ChildWithRender = context.gameObject.transform.GetChild(1).GetChild(0);
        Renderer rend = ChildWithRender.gameObject.GetComponent<Renderer>();
        rend.material.mainTexture = context.mutationTexture;
        context.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        
    }

    private void bacteriaMutation(Context context)
    {
        context.nav.speed = 12;
        //Change enemy color with shader
        Transform ChildWithRender =  context.gameObject.transform.GetChild(1).GetChild(0);
        Renderer rend = ChildWithRender.gameObject.GetComponent<Renderer>();
        rend.material.mainTexture = context.mutationTexture;
        context.GetComponent<TrailRenderer>().enabled = true;
        context.nav.angularSpeed = 200;
        context.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void parasiteMutation(Context context)
    {
        CreateParasite createParasite = new CreateParasite();
        createParasite.createParasite(context);
        
    }

    private void Transition(Context context)
    {
        context.state = new ChaseState();
    }
}

public class CreateParasite:MonoBehaviour
{
    public void createParasite(Context context)
    {
        ParasiteSpawnManager manager= context.gameObject.GetComponent<ParasiteSpawnManager>();
        manager.CreateParasites();

    }
}