using UnityEngine;

public class BacteriaAttack : MonoBehaviour {

    public GameObject explosionParticles;
    public float radius = 5.0F;
    public float power = 100.0F;
  

    public void Attack()
    {
        GameObject explosion = Instantiate(explosionParticles,transform.position,transform.localRotation);
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

        Destroy(explosion, 0.2f);
        Destroy(transform.gameObject);
    }
}
