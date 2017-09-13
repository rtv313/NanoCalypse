using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour {

    public Transform firePoint;
    public int damage = 1;
    public float angleForAttack = 60f;
    public float timerBetweenDamage = 0.5f;
    public float timerAudioLaser = 1.0f;
    private float timeAudioLaser = 0.0f;
    public AudioClip laserAudio;
    private AudioSource audioSource;
    private BossContext context;
    private LineRenderer lineRenderer;
    private PlayerHealth playerHealth;
    public GameObject ImpactLaserEffect;
    private GameObject instanceImpactLaser;

    private float timeForDamage = 0f;


    // Use this for initialization
    void Start ()
    {
        context = GetComponent<BossContext>();
        lineRenderer = GetComponent<LineRenderer>();
        playerHealth = context.target.GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        timeAudioLaser = timerAudioLaser;
        instanceImpactLaser = Instantiate(ImpactLaserEffect);
        instanceImpactLaser.active = false;

    }

    void FixedUpdate()
    {
        if (context.stateString == "BossAttack" && context.BossColor == Context.EnemyType.VIRUS)
        {
            LaserAttack();
        }
        else
        {
            instanceImpactLaser.active = false;
            lineRenderer.enabled = false;
        }
    }


    void LaserAttack()
    {
        float targetDistance = Vector3.Distance(context.target.position, firePoint.position);
        RaycastHit hit;
        Vector3 dir = (context.target.position - firePoint.position).normalized * targetDistance;
        float angle = Vector3.Angle(dir, transform.forward);
        Debug.DrawRay(firePoint.position, dir, Color.blue);

        if (Physics.Raycast(firePoint.position, dir, out hit, targetDistance) && targetDistance <= context.attackDistance)
        {
            instanceImpactLaser.transform.position = hit.point;
            if (timeAudioLaser >= timerAudioLaser)
            {
                audioSource.clip = laserAudio;
                audioSource.Play();
                timeAudioLaser = 0.0f;
            }

            timeAudioLaser += Time.deltaTime;

            lineRenderer.enabled = true;
            instanceImpactLaser.active = true;
            if (hit.transform.gameObject.tag == "Player" && angle < angleForAttack)
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, context.target.position);
                timeForDamage += Time.deltaTime;

                if (timeForDamage >= timerBetweenDamage)
                {
                    playerHealth.TakeDamage(damage);
                    timeForDamage = 0;
                    Debug.Log("Matando player con Laser");
                }
            }
            else
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            timeAudioLaser = timerAudioLaser;
        }
    }

    public void DeactivateLaserLight()
    {
        instanceImpactLaser.active = false;
        lineRenderer.enabled = false;
    }
}
