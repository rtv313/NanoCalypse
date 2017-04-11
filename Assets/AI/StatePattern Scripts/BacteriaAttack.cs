using UnityEngine;

public class BacteriaAttack : MonoBehaviour {

    public GameObject explosionParticles;
    public float radius = 5.0F;
    public float power = 100.0F;
    public float explosionTime = 0.2F;

    public void Attack(Context context)
    {
        GameObject explosion = Instantiate(explosionParticles,transform.position,transform.localRotation);
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
            }
        }

        Destroy(explosion, explosionTime);
        Destroy(transform.gameObject);
    }
}
