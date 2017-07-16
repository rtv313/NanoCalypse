using UnityEngine;

public class VirusAttack : MonoBehaviour
{

    public GameObject virusBullet;
    public GameObject muzzleFlash;
    public Transform firePoint;
    public float bulletSpeed=10;
    private Context context;
   

    public void Start()
    {
       // context debe ser usado para hacer un mejor apuntado de los projectiles
    }

    public void Attack()
    {
        GameObject flash = Instantiate(muzzleFlash, transform.position, transform.rotation);
        var bullet = Instantiate(virusBullet, firePoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 2.0f);
        Destroy(flash, 0.3f);
    }    
}
