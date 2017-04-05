
using UnityEngine;

public class ChasePlayer : MonoBehaviour {
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    public float attackDistance = 1.5f;
    // Use this for initialization
    void Awake () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        Chase();
    }

    void Chase()
    {
        if (nav.remainingDistance < attackDistance)
        {
            nav.enabled = false;
        }
        else {
            nav.enabled = true;
        }
        nav.SetDestination(player.position);
    }
}
