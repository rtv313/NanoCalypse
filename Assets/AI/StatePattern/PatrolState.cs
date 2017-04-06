﻿using UnityEngine;

public class PatrolState : State
{
    private float time = 0;
    public override void Handle(Context context)
    {
        context.stateString = "Patrol";
        Patrol(context);
        Transition(context);
    }

    private void Patrol(Context context)
    {
        float distance = context.nav.remainingDistance;

        if (distance== 0f) // avoid the reset that ocurred in the idle state
        {
            distance = context.remainingDistanceBeforeIdle;
        }

        if (distance < 0.5f)
        {
            switch (context.patrolMode)
            {
                case Context.PatrolMode.LOOP:
                    ++context.patrol_wavePoint;
                    if (context.patrol_wavePoint >= context.path_objs_Patrol.Length)
                    {
                        context.patrol_wavePoint = 1;
                    }
                    break;

                case Context.PatrolMode.PING_PONG:
                    // going up
                    if (context.pingPongUp == true)
                    {
                        if (context.patrol_wavePoint < context.path_objs_Patrol.Length)
                        {
                            ++context.patrol_wavePoint;

                            if (context.patrol_wavePoint >= context.path_objs_Patrol.Length)
                            {
                                context.pingPongUp = false;
                                context.patrol_wavePoint = context.path_objs_Patrol.Length - 1;
                            }
                        }
                    }
                    else
                    {
                        if (context.patrol_wavePoint > 1)
                        {
                            --context.patrol_wavePoint;
                        }
                        else
                        {
                            context.pingPongUp = true;
                            context.patrol_wavePoint = 1;
                        }
                    }
                 break;
            }
        }

       

        context.nav.SetDestination(context.path_objs_Patrol[context.patrol_wavePoint].transform.position);

    } // Patrol

    private void Transition(Context context)
    {
        time += Time.deltaTime;

        if (context.life <= 0)
        {
            context.state = new DeathState();
            return;
        }

        if (context.playerInSight == true)
        {
            context.state = new ChaseState();
            return;
        }

        if (context.wander == true)
        {
            context.state = new WanderState();
            return;
        }

        if (time >= context.idleTimer)
        {
            context.remainingDistanceBeforeIdle = context.nav.remainingDistance;
            context.state = new IdleState();
            return;
        }
    }
}
