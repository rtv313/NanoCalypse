using UnityEngine;

public class VirusAttack : MonoBehaviour
{

    public GameObject virusBullet;
    public ParticleSystem muzzleFlash;
    public float bulletSpeed=10;

    public void Attack()
    {
        ParticleSystem flash = Instantiate(muzzleFlash, transform.position, transform.rotation);
        var bullet = (GameObject)Instantiate(virusBullet, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        Destroy(bullet, 2.0f);
        Destroy(flash, 0.3f);
    }
}
