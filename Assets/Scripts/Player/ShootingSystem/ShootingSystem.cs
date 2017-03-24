using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {

    public GameObject assaultRifleBullet;
    public GameObject sniperBullet;
    public GameObject shootgunBullet;
    public Transform bulletSpawn;
    public int bulletSpeedRifle=35;
    public int bulletSpeedShootgun = 35;
    public int bulletSpeedSniper = 35;
    public int fireMode=1;
    public float timer=1;
    public float timeBetweenBulletsAssaultRifle = 0.0f;
    public float timeBetweenBulletsShootgun = 0.0f;
    public float timeBetweenBulletSniperRifle = 0.0f;
    public float rifleBulletLifeTime = 2.0f;
    public float shootgunBulletLifeTime = 0.5f;
    public float sniperBulletLifeTime = 3.0f;
    public AudioClip rifleSound;
    public AudioClip shootgunSound;
    public AudioClip sniperSound;
    public GameObject muzzleFlash;
    private AudioSource gunAudio;
    private ParticleSystem gunParticles;                    // Reference to the particle system.


    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        gunParticles = muzzleFlash.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        SelectWeapon();

        if (Input.GetButton("Fire1"))
        {
            if (fireMode == 1 && timer >= timeBetweenBulletsAssaultRifle)
            {
                Fire();
            }

            if (fireMode == 2 && timer >= timeBetweenBulletsShootgun)
            {
                Fire();
            }

            if (fireMode == 3 && timer >= timeBetweenBulletSniperRifle)
            {
                Fire();
            }

        }
    }


    void SelectWeapon()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireMode = 1;
            timer = 10.0f;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fireMode = 2;
            timer = 10.0f;
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fireMode = 3;
            timer = 10.0f;
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        if (fireMode == 1)
        {
            shootAssaultRifle();
        }
        else if (fireMode == 2)
        {
            shootShootgun();
        }
        else if (fireMode == 3){
            shootSniperRifle();
        }

        gunParticles.Stop();
        gunParticles.Play();
    }

    void shootAssaultRifle()
    {
        var main = gunParticles.main;
        main.startColor = new Color(1.0F, 0.0F, 0.0F, 1.0F);

        var bullet = (GameObject)Instantiate(assaultRifleBullet, bulletSpawn.position, bulletSpawn.rotation);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedRifle;
        timer = 0.0f;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, rifleBulletLifeTime);
        gunAudio.clip = rifleSound;
        gunAudio.Play();

    }

    void shootShootgun()
    {

        var main = gunParticles.main;
        main.startColor =  new Color(0.0F, 0.0F, 1.0F, 1.0F);

        var bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bullet.transform.rotation.x, 5, bullet.transform.rotation.z);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedShootgun;
        Destroy(bullet, shootgunBulletLifeTime);

        bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bullet.transform.rotation.x, 10, bullet.transform.rotation.z);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedShootgun;
        Destroy(bullet, shootgunBulletLifeTime);

        bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bullet.transform.rotation.x, -5, bullet.transform.rotation.z);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedShootgun;
        Destroy(bullet, shootgunBulletLifeTime);

        bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bullet.transform.rotation.x, -10, bullet.transform.rotation.z);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedShootgun;
        Destroy(bullet, shootgunBulletLifeTime);

        bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
        bullet.transform.Rotate(bullet.transform.rotation.x, bullet.transform.rotation.y, bullet.transform.rotation.z);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedShootgun;
        Destroy(bullet, shootgunBulletLifeTime);
        gunAudio.clip = shootgunSound;
        gunAudio.Play();

        timer = 0.0f;
    }


    void shootSniperRifle()
    {

        var main = gunParticles.main;
        main.startColor = new Color(0.0F, 1.0F, 0.0F, 1.0F);
      
        var bullet = (GameObject)Instantiate(sniperBullet, bulletSpawn.position, bulletSpawn.rotation);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeedSniper;
        timer = 0.0f;
        // Destroy the bullet after 2 seconds
        Destroy(bullet, sniperBulletLifeTime);
        gunAudio.clip = sniperSound;
        gunAudio.Play();
    }
}
