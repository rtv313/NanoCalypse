using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneContext : MonoBehaviour
{

    public DroneState state;
    public string stateString = "SearchState";
    public int life = 100;
    public int healthPower = 1;
    public UnityEngine.AI.NavMeshAgent nav;
    public float healthDistance = 2;
    public float sinkSpeed = 3f;
    public Rigidbody rigidbody;
    public GameObject wound;
    public GameObject woundPosition;
    public GameObject healParticleSystem;
    public GameObject player;
    public CapsuleCollider capsuleCollider;
    public float timeDestroy = 2.0f;
    public bool activateHealing = false;
    // Use this for initialization
    void Awake()
    {
        healParticleSystem.SetActive(false);
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        state = new SearchState();
    }

    private void Request()
    {
        state.Handle(this);
    }

    // Update is called once per frame
    void Update()
    {

        Request();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().resetRb = 0.5f;
            collision.gameObject.GetComponent<PlayerMovement>().CallResetRb();
            collision.gameObject.GetComponent<PlayerMovement>().resetRb = 1.5f;
        }
    }
}
