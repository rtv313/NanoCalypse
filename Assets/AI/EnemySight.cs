﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAnagle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private UnityEngine.AI.NavMeshAgent nav;
    private SphereCollider col;
    private GameObject player;
 


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAnagle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        //lastPlayerSighting.position = player.transform.position; //alert other enemies, is a manager
                        
                    }
                }
            }

            // ESCUCHA RUIDO CALCULA LA DISSTANCIA HACIA EL PLAYER

            if (true)
            {
                if (CalculatePathLength(player.transform.position) <= col.radius)
                {
                       //personalLastSighting = player.transform.position;
                }
            }

          }
      }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
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

        for(int i = 0; i < path.corners.Length; i++)
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
