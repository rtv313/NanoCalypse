using UnityEngine;

[RequireComponent(typeof(DamageFeedback))]

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
    public int attackDamage = 10;
    public PlayerHealth playerHealth;
    public bool animFlagAttack = false;

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

	// Score
	public ScoreManager scoreManager;

    // Damage Feedback
    private DamageFeedback dmgFeedback;
    public GameObject explosion;

    //Textures for mutation
    public Texture2D mutationTexture;
    public Texture2D originalTexture;

    void Awake()
    {
		scoreManager = GameObject.Find ("GUI").GetComponent<ScoreManager> () as ScoreManager;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        path_objs_Patrol = patrolPath.GetComponentsInChildren<Transform>();
        path_objs_Wander = wanderPath.GetComponentsInChildren<Transform>();
        sensors = GetComponent<EnemySensors>();
        state = new WanderState();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = target.GetComponent<PlayerHealth>();
        dmgFeedback = GetComponent<DamageFeedback>();

        if (enemyType == EnemyType.VIRUS) // for break monotony
        {
            float[] variableAttackPosition = new float[4];
            variableAttackPosition[0] = attackDistance - 5;
            variableAttackPosition[1] = attackDistance - 3;
            variableAttackPosition[2] = attackDistance - 1;
            variableAttackPosition[3] = attackDistance;
            attackDistance = variableAttackPosition[Random.Range(0, 4)];
        }
        else
        {
            float[] variableSpeed = new float[4];
            variableSpeed[0] = nav.speed + 0.5f;
            variableSpeed[1] = nav.speed - 1.0f;
            variableSpeed[2] = nav.speed - 0.5f;
            variableSpeed[3] = nav.speed;
            nav.speed = variableSpeed[Random.Range(0, 4)];
        }

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
