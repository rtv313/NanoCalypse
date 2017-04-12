using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth pH = other.gameObject.GetComponent<PlayerHealth>();
            pH.TakeDamage(damage);
            Destroy(transform.gameObject);
        }

        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "AgentDetector")
        {
            Destroy(transform.gameObject);
        }

    }
}
