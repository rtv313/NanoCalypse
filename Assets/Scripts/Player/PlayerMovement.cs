using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public float playerGravity = -9.8f;
    public float speed = 6f;
    public float resetRb = 1.5f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
	private Transform firingPoint;

	// Dash
	private bool dashing = false;
	public float dashTime = 0.4f;
	public float dashSpeed = 15f;
	public AudioClip dashingSound;
	private float currentDashTime = 0.0f;
	private Vector3 dashMovement;
	private TrailRenderer tr;
	public float trailTimeToLive = 0.2f;
	private float trailTimer = 0.0f;

    //Animations
    public Animator animator;
	private bool dead;

    // Mouse/Controller
    bool usingMouse = true;
    Vector3 lastControllerPosition = new Vector3(0,0,0);
    float turningSpeep = 20.0f;

    private bool paused = false;

    void OnPauseGame()
    {
        paused = true;
    }

    void OnResumeGame()
    {
        paused = false;
    }

    void Awake()
    {
		firingPoint = GameObject.Find ("bulletSpawn").GetComponent<Transform>();

        floorMask = LayerMask.GetMask("AimPlane");
        playerRigidbody = GetComponent<Rigidbody>();

		tr = GetComponent<TrailRenderer> ();
    }

	void Start(){
		dead = GameObject.Find ("Player").GetComponent<PlayerHealth> ().dead;
	}
    void FixedUpdate()
    {
		dead = GameObject.Find ("Player").GetComponent<PlayerHealth> ().dead;
        if (!paused && !dead)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

			DashCheck (h, v);

			if (dashing)
				Dash ();
			else {
				Move (h, v);
			}

			Turning ();
			Animating (h, v);
			
        }
    }

	void DashCheck(float h, float v) {
		if (!dashing && trailTimer <= 0.0f && (Input.GetAxis("Dash") > 0.2f || Input.GetKeyDown(KeyCode.Space))) {
			AudioSource.PlayClipAtPoint (dashingSound, transform.position);
			dashing = true;
			tr.enabled = true;
			tr.Clear();
			currentDashTime = dashTime;
			dashMovement.Set(h, playerGravity * Time.deltaTime, v);
		}

		if (trailTimer > 0.0f) {
			trailTimer -= Time.deltaTime;
			if (trailTimer <= 0.0f) {
				tr.enabled = false;
			}
		}
	}

	void Dash()
	{
        Vector3 velocity = dashMovement.normalized * dashSpeed * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + velocity);
		currentDashTime -= Time.deltaTime;
		if (currentDashTime < 0.0f) {
			dashing = false;
			trailTimer = trailTimeToLive;
		}
	}

    void Move(float h, float v)
    {
		movement.Set(h, playerGravity * Time.deltaTime, v);
        movement = movement.normalized*speed*Time.deltaTime; // makes speed in diagonal the same
		Rigidbody rb = GetComponent<Rigidbody>();
        playerRigidbody.MovePosition(transform.position + movement);
    }


    void Turning()
    {
        // Controller Axis giving values
        if (Mathf.Abs(Input.GetAxis("AimHorizontal")) > 0.3f || Mathf.Abs(Input.GetAxis("AimVertical")) > 0.3f)
        {
            usingMouse = false;
            Vector3 playerToMouse = new Vector3(Input.GetAxis("AimHorizontal"), 0.0f, Input.GetAxis("AimVertical")) - new Vector3(0.0f,0.0f,0.0f);
            playerToMouse.y = 0f;
            lastControllerPosition = playerToMouse;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turningSpeep));
        }
        else if (!usingMouse) // Controller Axis giving no response
        {
            Quaternion newRotation = Quaternion.LookRotation(lastControllerPosition);
            playerRigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turningSpeep));
        }
        else // Using mouse for aim
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turningSpeep));
            }
        }

        // Back to using mouse/keyboard
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            usingMouse = true;
        }
    }


    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        animator.SetBool("IsWalking", walking);
    }

	public void CallResetRb()
    {
        Invoke("ResetRb", resetRb);
    }

    private void ResetRb()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //Debug.Log("Reset rigidbody");
    }
}
