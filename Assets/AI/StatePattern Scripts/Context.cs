
using UnityEngine;

public class Context : MonoBehaviour
{
    public enum EnemyType { VIRUS, BACTERIA, PARASITE };
    public EnemyType enemyType = EnemyType.VIRUS;
    public State state;
    public string stateString="Patrol";
    public int life = 100;
    public Animator animator;
  
    //Colliders /Triggers/ Rigidbodies
    public CapsuleCollider capsuleCollider;
    public Rigidbody rigidbody;

    //Patrol State variables
    public enum PatrolMode { PING_PONG, LOOP };
    public UnityEngine.AI.NavMeshAgent nav;
    
    //Patrol Path
    public Transform patrolPath;
    public PatrolMode patrolMode = PatrolMode.LOOP;
    public Transform[] path_objs_Patrol;
    public int patrol_wavePoint = 1;
    public bool pingPongUp = true;

    //Wander path
    public Transform wanderPath;
    public Transform[] path_objs_Wander;
    public float wanderObjectiveDistance = 0.5f;

    // Sensors
    public EnemySensors sensors;
  
    //Transitions
    public bool wander = false;
    public bool playerInSight = false;

    //Chase Player
    public Transform target;
    public float attackDistance = 1.5f;

    //Attack Player
   
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;
    public PlayerHealth playerHealth;

    //Idle Player
    public float idleTimer = 10f;
    public float idleTime = 3f;
    public float remainingDistanceBeforeIdle=0;
    public bool resetTimeForIdle = false;

    //Death
    public float sinkSpeed = 3f;
    public float timeDestroy = 5f;

    //Mutation
    public bool mutaded = false ;
    public GameObject parasite;

	// Score
	public ScoreManager scoreManager;

    void Awake()
    {
		scoreManager = GameObject.Find ("player UI").GetComponent<ScoreManager> () as ScoreManager;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        path_objs_Patrol = patrolPath.GetComponentsInChildren<Transform>();
        path_objs_Wander = wanderPath.GetComponentsInChildren<Transform>();
        sensors = GetComponent<EnemySensors>();
        state = new WanderState();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = target.GetComponent<PlayerHealth>();
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
}
