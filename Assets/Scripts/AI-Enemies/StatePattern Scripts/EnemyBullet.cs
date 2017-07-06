using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag=="Drone")
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerHealth pH = other.gameObject.GetComponent<PlayerHealth>();
                pH.TakeDamage(damage);
                Destroy(transform.gameObject);
                return;
            }else if (other.gameObject.tag == "Drone")
            {
                other.gameObject.GetComponent<DroneContext>().life -= damage;
                Destroy(transform.gameObject);
                return;
            }
        }

        if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "AgentDetector" && other.gameObject.tag != "Wound")
        {
            Destroy(transform.gameObject);
        }

    }
}
