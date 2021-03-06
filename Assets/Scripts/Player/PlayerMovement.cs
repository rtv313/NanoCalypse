﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float playerGravity = -9.8f;
    public float speed = 6f;
    public float resetRb = 1.5f;
    Vector3 movement;
    Animator anim;
    // Rigidbody playerRigidbody;
    CharacterController playerCharCont;
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
    Vector3 lastControllerPosition = new Vector3(0, 0, 0);
    float turningSpeed = 50.0f;

    private bool paused = false;
    private Vector3 aimPos = new Vector3(0, 0, 0);
    public float mouseAimDeadzone = 1.0f;

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
        firingPoint = GameObject.Find("bulletSpawn").GetComponent<Transform>();

        floorMask = LayerMask.GetMask("AimPlane");
        // playerRigidbody = GetComponent<Rigidbody>();
        playerCharCont = GetComponent<CharacterController>();

        tr = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        dead = GameObject.Find("Player").GetComponent<PlayerHealth>().dead;
    }
    void FixedUpdate()
    {
        dead = GameObject.Find("Player").GetComponent<PlayerHealth>().dead;
        if (!paused && !dead)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            DashCheck(h, v);

            if (dashing)
                Dash();
            else {
                Move(h, v);
            }

            Turning();
            Animating(h, v);

            UpdateWalkingAnimation(h, v);

            if (transform.position.y > 6.0f) {
                transform.position = new Vector3 (transform.position.x, 6.0f, transform.position.z); // Force player to stay bellow 5.5f
            }
        }
    }

    private void UpdateWalkingAnimation(float h, float v)
    {
        Vector2 playerDirection = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 playerMovement = new Vector2(h, v);
        float angle = Vector2.Angle(playerDirection, playerMovement);
        // Checking for back walking
        bool backwards = (angle >= 135.0 && angle < 225.0);
        animator.SetBool("WalkingBack", backwards);
        // Checking for strafing
        if (angle > 45.0 && angle < 135.0)
        {
            float dot = playerMovement.x * (-playerDirection.y) + playerMovement.y * playerDirection.x;
            if (dot > 0) // Left
            {
                animator.SetBool("WalkingRight", false);
                animator.SetBool("WalkingLeft", true);
            }
            else // Right
            {
                animator.SetBool("WalkingRight", true);
                animator.SetBool("WalkingLeft", false);
            }
        }
        else
        {
            animator.SetBool("WalkingRight", false);
            animator.SetBool("WalkingLeft", false);
        }

    }

    void DashCheck(float h, float v)
    {
        if (!dashing && trailTimer <= 0.0f && (Input.GetAxis("Dash") > 0.2f || Input.GetKeyDown(KeyCode.Space)))
        {
            AudioSource.PlayClipAtPoint(dashingSound, transform.position);
            dashing = true;
            tr.enabled = true;
            tr.Clear();
            currentDashTime = dashTime;
            dashMovement.Set(h, playerGravity * Time.deltaTime, v);
        }

        if (trailTimer > 0.0f)
        {
            trailTimer -= Time.deltaTime;
            if (trailTimer <= 0.0f)
            {
                tr.enabled = false;
            }
        }
    }

    void Dash()
    {
        Vector3 velocity = dashMovement.normalized * dashSpeed * Time.deltaTime;
        // playerRigidbody.MovePosition(transform.position + velocity);
        velocity.y = -9.8f * Time.deltaTime;
        playerCharCont.Move(velocity);
        currentDashTime -= Time.deltaTime;
        if (currentDashTime < 0.0f)
        {
            dashing = false;
            trailTimer = trailTimeToLive;
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, playerGravity * Time.deltaTime, v);
        movement = movement.normalized * speed * Time.deltaTime; // makes speed in diagonal the same
                                                                 // Rigidbody rb = GetComponent<Rigidbody>();
                                                                 // playerRigidbody.MovePosition(transform.position + movement);
        movement.y = -9.8f * Time.deltaTime;
        playerCharCont.Move(movement);
    }


    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        // ---
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // create a plane at 0,0,0 whose normal points to +Y:
        Plane hPlane = new Plane(Vector3.up, new Vector3(0.0f, transform.position.y, 0.0f));
        // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
        float distance = 0;
        // if the ray hits the plane...
        if (hPlane.Raycast(camRay, out distance))
        {
            // get the hit point:
            Vector3 floorHitPoint = camRay.GetPoint(distance);
            floorHitPoint += -transform.right * 0.5f;
            Vector3 playerToMouse = floorHitPoint - transform.position;
            playerToMouse.y = 0.0f;

            // Mouse Aim Deadzone
            if (playerToMouse.magnitude > 1.0f)
            {
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                transform.rotation = (Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turningSpeed));
            }
        }
        // ---

        //if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        //{
        //    Vector3 floorHitPoint = floorHit.point;
        //    floorHitPoint += -transform.right * 0.5f;
        //    Vector3 playerToMouse = floorHitPoint - transform.position;
        //    playerToMouse.y = 0.0f;

        //    // Mouse Aim Deadzone
        //    if (playerToMouse.magnitude > 1.0f)
        //    {
        //        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        //        transform.rotation = (Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turningSpeed));
        //    }
        //}
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
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        //Debug.Log("Reset rigidbody");
    }
}
