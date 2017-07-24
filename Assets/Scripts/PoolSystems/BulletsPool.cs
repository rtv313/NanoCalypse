using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour {

    public GameObject rifleBullet;
    public GameObject shootgunBullet;
    public GameObject sniperBullet;

    public int rifleBulletsAmount = 20;
    public int shootgunBulletsAmount = 20;
    public int sniperBulletsAmount = 20;

    public List<GameObject> rifleBullets;
    public List<GameObject> shootgunBullets;
    public List<GameObject> sniperBullets;

    // Use this for initialization
    void Start()
    {
        rifleBullets = new List<GameObject>();
        shootgunBullets = new List<GameObject>();
        sniperBullets = new List<GameObject>();
        GameObject bullet;
        for (int i = 0; i < rifleBulletsAmount; i++)
        {
            //Rifle bullets
            bullet = Instantiate(rifleBullet);
            bullet.SetActive(false);
            rifleBullets.Add(bullet);
        }

        for (int i = 0; i < shootgunBulletsAmount; i++)
        {
            //Shootgun bullets
            bullet = Instantiate(shootgunBullet);
            bullet.SetActive(false);
            shootgunBullets.Add(bullet);
        }

        for (int i = 0; i < sniperBulletsAmount; i++)
        {
            //Sniper bullets
            bullet = Instantiate(sniperBullet);
            bullet.SetActive(false);
            sniperBullets.Add(bullet);
        }
    }

    private void ResetBullet(GameObject bullet,Transform positionReference)
    {
        bullet.transform.position = positionReference.position;
        bullet.transform.rotation = positionReference.rotation;
        bullet.GetComponent<TrailRenderer>().Clear();
        bullet.GetComponent<BulletDamage>().ResetBulletLifeTime();
        bullet.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
        bullet.SetActive(true);
    } 

    public GameObject GetRifleBullet(Transform positionReference)
    {
        for (int i = 0; i < rifleBullets.Count; i++)
        {
            if (!rifleBullets[i].activeInHierarchy)
            {
                ResetBullet(rifleBullets[i], positionReference);
                return rifleBullets[i];
            }
        }

        // if we need more bullets we create them
        GameObject newBullet = Instantiate(rifleBullet);
        rifleBullets.Add(newBullet);
        return newBullet;
    }

    public GameObject GetShootgunBullet(Transform positionReference)
    {
        for (int i = 0; i < shootgunBullets.Count; i++)
        {
            if (!shootgunBullets[i].activeInHierarchy)
            {
                ResetBullet(shootgunBullets[i], positionReference);
                return shootgunBullets[i];
            }
        }

        // if we need more bullets we create them
        GameObject newBullet = Instantiate(shootgunBullet);
        shootgunBullets.Add(newBullet);
        return newBullet;
    }

    public GameObject GetSniperBullet(Transform positionReference)
    {
        for (int i = 0; i < sniperBullets.Count; i++)
        {
            if (!sniperBullets[i].activeInHierarchy)
            {
                ResetBullet(sniperBullets[i], positionReference);
                return sniperBullets[i];
            }
        }

        // if we need more bullets we create them
        GameObject newBullet = Instantiate(sniperBullet);
        sniperBullets.Add(newBullet);
        return newBullet;
    }

}
