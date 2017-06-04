﻿using UnityEngine;

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
        context.attackDamage = context.attackDamage * 5;
        context.GetComponent<VirusAttack>().bulletSpeed = 50;
        //change bullet color
        //change enemy color with shader
        Renderer rend = context.GetComponent<Renderer>();
        rend.material.color = Color.magenta;
    }

    private void bacteriaMutation(Context context)
    {
        context.nav.speed = 15;
        //change enemy color with shader
        Renderer rend = context.GetComponent<Renderer>();
        rend.material.color = Color.yellow;
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