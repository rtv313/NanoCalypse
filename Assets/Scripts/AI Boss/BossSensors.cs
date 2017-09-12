using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSensors : MonoBehaviour {

    public bool playerInSight = false;
    private UnityEngine.AI.NavMeshAgent nav;
    private GameObject player;
    private BossContext context;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (transform != null && context != null)
        {
            Gizmos.DrawWireSphere(transform.position, context.attackDistance);
        }
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        context = GetComponent<BossContext>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player )
        {
            context.target = player.transform;
            playerInSight = true;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            Debug.DrawRay(transform.position, direction, Color.green);
            return;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }
}
