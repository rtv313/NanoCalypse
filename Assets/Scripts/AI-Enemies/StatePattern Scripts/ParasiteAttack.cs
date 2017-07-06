using UnityEngine;

public class ParasiteAttack : MonoBehaviour {

    public void Attack(Context context)
    {
        if (context.target.tag == "Player")
        {
            context.playerHealth.TakeDamage(context.attackDamage);
            return;
        }

        if (context.target.tag == "Drone")
        {
            context.target.GetComponent<DroneContext>().life -= context.attackDamage;
            return;
        }
    }
}
