using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent nav;
    public GameObject target;
    public Transform patrolPath;
    Transform[] path_objs;
    int patrol_wavePoint = 0;



    // Use this for initialization
    void Awake() {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        path_objs = patrolPath.GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Wandering();
    }

    void Wandering()
    {
        if (nav.remainingDistance < 0.5f)
        {
          
            patrol_wavePoint = Random.Range(0, path_objs.Length);

            while (patrol_wavePoint <= 0)
            {
                patrol_wavePoint = Random.Range(0, path_objs.Length);
            }
        }

        nav.SetDestination(path_objs[patrol_wavePoint].transform.position);
    }
}
