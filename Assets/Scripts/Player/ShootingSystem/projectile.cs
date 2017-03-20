using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public GameObject assaultRifleBullet;
    public GameObject sniperBullet;
    public GameObject shootgunBullet;
    public Transform bulletSpawn;
    public int bulletSpeed = 10;
    public int fireMode=1;
    public float timer=1;
    public float timeBetweenBulletsAssaultRifle = 0.0f;
    public float timeBetweenBulletsShootgun = 0.0f;
    public float timeBetweenBulletSniperRifle = 0.0f;

    void Update()
    {
       

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
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
            timer = 0.0f;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fireMode = 2;
            timer = 0.0f;
        }


        if (Input.GetKeyDown(KeyCode.Alpha3)){
            fireMode = 3;
            timer = 0.0f;
        }
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        if (fireMode == 1)
        {
            var bullet = (GameObject)Instantiate(assaultRifleBullet, bulletSpawn.position, bulletSpawn.rotation);
            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            timer = 0.0f;
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);

        }
        else if (fireMode == 2)
        {
            Quaternion rotationQuaternion;
            rotationQuaternion = bulletSpawn.rotation;
            var bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 2.0f);

            rotationQuaternion.y = 0;
            rotationQuaternion.y += 30;
            bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 2.0f);

            rotationQuaternion.y = 0;
            rotationQuaternion.y -= 30;

            bullet = (GameObject)Instantiate(shootgunBullet, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 2.0f);

            timer = 0.0f;
        }
        else if (fireMode == 3){
             
            var bullet = (GameObject)Instantiate(sniperBullet, bulletSpawn.position, bulletSpawn.rotation);
            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            timer = 0.0f;
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);
        }
       
    }
}
