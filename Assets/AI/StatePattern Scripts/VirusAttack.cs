using UnityEngine;

public class VirusAttack : MonoBehaviour
{

    public GameObject virusBullet;
    public GameObject muzzleFlash;
    public float bulletSpeed=10;

   
    public void Attack()
    {
        GameObject flash = Instantiate(muzzleFlash, transform.position, transform.rotation);
        var bullet = Instantiate(virusBullet, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 2.0f);
        Destroy(flash, 0.3f);
    }
}
