using UnityEngine;

public class ParasiteAttack : MonoBehaviour {

    public void Attack(Context context)
    {
        context.playerHealth.TakeDamage(context.attackDamage);
    }
}
