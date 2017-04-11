using UnityEngine;

public class BulletDamage : MonoBehaviour {

    public enum BulletType { ASSAULT, SHOOTGUN,SNIPER };
    public BulletType bulletType = BulletType.ASSAULT;
    public int assaultRifleDamage = 10;
    public int shootgunDamage = 5;
    public int sniperDamage = 30;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Context context = other.gameObject.GetComponent<Context>();

            switch (bulletType)
            {
                case BulletType.ASSAULT:
                    context.life -= assaultRifleDamage / 2;
                    break;

                case BulletType.SHOOTGUN:
                    context.life -= shootgunDamage / 2;
                    break;

                case BulletType.SNIPER:
                    context.life -= sniperDamage / 2;
                    break;
            }

            Destroy(transform.gameObject);
        }
    }
}
