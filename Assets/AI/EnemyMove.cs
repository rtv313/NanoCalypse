using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent nav;
    public GameObject target;
    public Transform patrolPath;
    Transform[] path_objs;
    int patrol_wavePoint=0;
    // Use this for initialization
    void Start ()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        path_objs = patrolPath.GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        // nav.SetDestination(target.transform.position);
        Patrol();
    }

    void Patrol()
    {
        float distance = Vector3.Distance(path_objs[patrol_wavePoint].transform.position, transform.position);
        if (distance == 0)
        {
            ++patrol_wavePoint;
            if (patrol_wavePoint > path_objs.Length)
            {
                patrol_wavePoint = 0;
            }
            
        }
       
       nav.SetDestination(path_objs[patrol_wavePoint].transform.position);
     }
}
