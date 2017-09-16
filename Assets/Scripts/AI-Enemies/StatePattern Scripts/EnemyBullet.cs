using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public int damage = 5;
    private PlayerHealth pH;
    void Start()
    {
        pH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Destroy(gameObject, 10.0F);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag=="Drone" || other.gameObject.tag=="BulletSpawnPlayer")
        {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "BulletSpawnPlayer")
            {
                pH.TakeDamage(damage);
                Destroy(transform.gameObject);
                return;
            }
            else if (other.gameObject.tag == "Drone")
            {
                other.gameObject.GetComponent<DroneContext>().life -= damage;
                Destroy(transform.gameObject);
                return;
            }
        }

        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "AgentDetector" && other.gameObject.tag != "Wound" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "SpawnFood" && other.gameObject.tag != "Boss")
        {
            Destroy(transform.gameObject);
        }

    }
}
