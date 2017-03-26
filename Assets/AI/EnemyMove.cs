using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public enum PatrolMode { PING_PONG , LOOP };

    UnityEngine.AI.NavMeshAgent nav;
    public GameObject target;
    public Transform patrolPath;
    public PatrolMode patrolMode = PatrolMode.LOOP;
    Transform[] path_objs;
    int patrol_wavePoint=0;

    bool pingPongUp = true;


    // Use this for initialization
    void Start ()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        path_objs = patrolPath.GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Patrol();
    }

    void Patrol()
    {
        if (nav.remainingDistance < 0.5f)
        {
           switch (patrolMode)
            {
                case PatrolMode.LOOP:
                    ++patrol_wavePoint;
                    if (patrol_wavePoint > path_objs.Length)
                    {
                        patrol_wavePoint = 1;
                    }
                    break;

                case PatrolMode.PING_PONG:
                    // going up
                    if (pingPongUp == true)
                    {
                        if (patrol_wavePoint < path_objs.Length)
                        {
                            ++patrol_wavePoint;
                        }
                        else {
                            pingPongUp = false;
                            patrol_wavePoint = path_objs.Length - 1;
                        }
                    }
                    else {
                        if (patrol_wavePoint > 1)
                        {
                            --patrol_wavePoint;
                        }
                        else {
                            pingPongUp = true;
                            patrol_wavePoint = 1;
                        }
                    }
                    break;
            }
            
        }
       
       nav.SetDestination(path_objs[patrol_wavePoint].transform.position);
     }
}
