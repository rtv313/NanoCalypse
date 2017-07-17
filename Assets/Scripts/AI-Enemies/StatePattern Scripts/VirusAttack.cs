using UnityEngine;

public class VirusAttack : MonoBehaviour
{

    public GameObject virusBullet;
    public GameObject muzzleFlash;
    public Transform firePoint;
    public float bulletSpeed=10;
    public float angleForAttack = 60f;
    private Context context;
   

    public void Start()
    {
        // context debe ser usado para hacer un mejor apuntado de los projectiles
        context = GetComponent<Context>();
    }

    public void Attack()
    {

        float targetDistance = Vector3.Distance(context.target.position, firePoint.position);
        Vector3 dirPlayer = (context.target.position - firePoint.position).normalized * targetDistance;
        float angle = Vector3.Angle(dirPlayer, transform.forward);

        if (angle < angleForAttack)
        {
            Vector3 dir = (context.target.position - firePoint.position).normalized;
            GameObject flash = Instantiate(muzzleFlash, transform.position, transform.rotation);
            var bullet = Instantiate(virusBullet, firePoint.position, transform.rotation);
            bullet.transform.rotation = Quaternion.LookRotation(dir);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            Destroy(bullet, 2.0f);
            Destroy(flash, 0.3f);
        }
    }    
}
