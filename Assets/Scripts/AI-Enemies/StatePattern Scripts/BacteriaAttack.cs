using UnityEngine;

public class BacteriaAttack : MonoBehaviour {

    public GameObject explosionParticles;
    public float radius = 5.0F;
    public float power = 100.0F;
    private BacteriaExplosionsPool bacteriaExplosionsPool;

    void Start()
    {
        bacteriaExplosionsPool = GameObject.FindGameObjectWithTag("BacteriaExpPool").GetComponent<BacteriaExplosionsPool>();
    }

    public void Attack(Context context)
    {
        GameObject explosion = bacteriaExplosionsPool.GetBacteriaExplosion(transform);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                context.playerHealth.TakeDamage(context.attackDamage);
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

                if(hit.tag=="Player")
                    hit.GetComponent<PlayerMovement>().CallResetRb();

                if (hit.tag == "Drone")
                    hit.GetComponent<DroneContext>().life -= context.attackDamage;
            }
        }

        explosion.GetComponent<BacteriaExplosion>().EnableExplosion();
        Destroy(transform.gameObject);
    }
}
