using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletAttack : MonoBehaviour {

    public GameObject bossBullet;
    public float timeBetweenBullets = 0.6f;
    public Transform firePoint;
    public float bulletSpeed = 15;
    public float angleForAttack = 60f;
    private BossContext context;
    private float timer = 0.0f;
    public bool allowAttack = true;
    public AudioSource bulletSound;
    private float targetDistance = 0.0f;
    void Start()
    {
        context = GetComponent<BossContext>();
    }

    void Update()
    {
        targetDistance = Vector3.Distance(context.target.position, firePoint.position);

        if (targetDistance <= context.meleeAttackDistance)
            return;

        if (timer >= timeBetweenBullets && context.playerInSight == true)
        {
            Attack();
            timer = 0.0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void Attack()
    {
        
        Vector3 dirPlayer = (context.target.position - firePoint.position).normalized * targetDistance;
        float angle = Vector3.Angle(dirPlayer, transform.forward);

        if (angle < angleForAttack)
        {
            bulletSound.Play();
            Vector3 dir = (context.target.position - firePoint.position).normalized;
            var bullet = Instantiate(bossBullet, firePoint.position, transform.rotation);
            bullet.transform.rotation = Quaternion.LookRotation(dir);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 2.0f);
        }
    }
}
