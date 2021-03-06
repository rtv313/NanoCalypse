﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContext : MonoBehaviour {
    public enum BossStateColor { VIRUS, BACTERIA, PARASITE, INMUNE };
    public BossContext.BossStateColor bossColor = BossStateColor.INMUNE;
    public BossState state;
    public string stateString = "Attack";
    public int life = 500;
    public Animator animator;

    public CapsuleCollider capsuleCollider;
    public Rigidbody rigidbody;
    public UnityEngine.AI.NavMeshAgent nav;

    //Path
    public Transform wanderPath;
    public Transform[] path_objs_Wander;
    public float wanderObjectiveDistance = 0.5f;
    public int patrol_wavePoint = 1;

    //Chase Player
    public bool playerInSight = false;
    public Transform target;
    public float meleeAttackDistance = 7.0f;
    public float laserAttackDistance = 14.0f;
    public float spawnAttackDistance = 14.0f;
    public float attackDistance = 1.5f;

    //Idle Player
    public float idleTimer = 10f;
    public float idleTime = 3f;
    public float remainingDistanceBeforeIdle = 0;
    public bool resetTimeForIdle = false;

    // Sensors
    private BossSensors sensors;

    //Attack Player
    public int attackDamage = 10;
    public PlayerHealth playerHealth;
    

    // Score
    public ScoreManager scoreManager;

    // Damage Feedback
    private DamageFeedback dmgFeedback;


    //AnimationsFlags
    public bool FlagMeleeAttack = false;
    public bool FlagSpawnAttack = false;
    public bool FlagAirAttack = false;
    public bool AnimationInProcess = false;

    // Use this for initialization
    void Start () {
		
	}

    void Awake()
    {
        scoreManager = GameObject.Find("GUI").GetComponent<ScoreManager>() as ScoreManager;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        if (wanderPath != null)
            path_objs_Wander = wanderPath.GetComponentsInChildren<Transform>();
        else
            path_objs_Wander = null;

        sensors = GetComponent<BossSensors>();
        state = new BossWanderState();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = target.GetComponent<PlayerHealth>();
        dmgFeedback = GetComponent<DamageFeedback>();
    }

    private void Request()
    {
        state.Handle(this);
    }

    public void Update()
    {
        playerInSight = sensors.playerInSight;
        Request();
    }

    public void receiveDamage()
    {
        dmgFeedback.receiveDamage();
    }

    public void receiveDamageMutate()
    {
        dmgFeedback.receiveDamageMutate();
    }
}
