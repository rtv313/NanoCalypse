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
  }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
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

    float CalculatePathLength(Vector3 targetPosition)
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLenght = 0f;

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLenght += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLenght;
    }
}


