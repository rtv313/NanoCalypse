using UnityEngine;

public class BacteriaAttack : MonoBehaviour {

    public float radius = 5.0F;
    public float power = 100.0F;
    private BacteriaExplosionsPool bacteriaExplosionsPool;
    private CameraShake cam;

    void Start()
    {
        bacteriaExplosionsPool = GameObject.FindGameObjectWithTag("BacteriaExpPool").GetComponent<BacteriaExplosionsPool>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    public void Attack(Context context)
    {
        GameObject explosion = bacteriaExplosionsPool.GetBacteriaExplosion(transform);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        cam.fireShake(1);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                context.playerHealth.TakeDamage(context.attackDamage);
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

                if(hit.tag=="Player")
                    hit.GetComponent<PlayerMovement>().CallResetRb();
            }
        }

        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();
        gameObject.SetActive(false);
       
    }
}
