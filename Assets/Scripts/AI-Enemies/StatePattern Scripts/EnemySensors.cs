using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensors : MonoBehaviour {

    public float fieldOfViewAnagle = 110f;
    public bool playerInSight=false;
    public Vector3 personalLastSighting;

    private UnityEngine.AI.NavMeshAgent nav;
    private SphereCollider col;
    private GameObject player;
    private Context context;

    private bool flagAttackDrone = false;
    public bool AllowDroneAttack = false;
    public int droneRandomAttack = 3;

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
        context = GetComponent<Context>();

        if(AllowDroneAttack==true)
            DecideAttack();
  }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player || other.gameObject.tag =="Drone")
        {
            if (other.gameObject.tag == "Drone" && flagAttackDrone)
            {
                context.target = other.transform;
            }
            else {
                context.target = player.transform;
            }

            playerInSight = true;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            Debug.DrawRay(transform.position, direction, Color.green);

            if (angle < fieldOfViewAnagle * 0.5f)
            {
                playerInSight = true;
                Debug.DrawRay(transform.position, direction, Color.red);
            }

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

    void DecideAttack()
    {
        int randNum = Random.Range(0, 9);

        if (randNum <= droneRandomAttack)
        {
            flagAttackDrone = true;
            return;
        }

        flagAttackDrone = false;
    }
}


