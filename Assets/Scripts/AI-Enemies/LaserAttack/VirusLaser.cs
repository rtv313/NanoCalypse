using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusLaser : MonoBehaviour {

    public Transform firePoint;
    public int damage = 1;
    public float angleForAttack = 60f;
    public float timerBetweenDamage = 0.5f;
    private Context context;
    private LineRenderer lineRenderer;
    private PlayerHealth playerHealth;
  
    private float timeForDamage = 0f;
	// Use this for initialization
	void Start ()
    {
        context = GetComponent<Context>();
        lineRenderer = GetComponent<LineRenderer>();
        playerHealth = context.target.GetComponent<PlayerHealth>();
	}
    void FixedUpdate()
    {
        if (context.mutaded == true && context.stateString == "Attack")
            LaserAttack();
        else
            lineRenderer.enabled = false;
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
            lineRenderer.enabled = true;
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
    }
}


