using UnityEngine;

public class BulletDamage : MonoBehaviour {

    public enum BulletType { ASSAULT, SHOOTGUN,SNIPER };
    public BulletType bulletType = BulletType.ASSAULT;
	public AudioClip bulletHitHard;
	public AudioClip bulletHitSoft;
	public AudioClip enemyDamage;
	public AudioClip enemyScreech;
    public int assaultRifleDamage = 10;
    public int shootgunDamage = 5;
    public int sniperDamage = 30;
    public GameObject bloodPS;

	void Awake() {

	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Context context = other.gameObject.GetComponent<Context>();
            Instantiate(bloodPS, transform.position, transform.rotation);

            switch (bulletType)
            {
                case BulletType.ASSAULT:
                    context.life -= assaultRifleDamage / 2;

                    if (context.enemyType != Context.EnemyType.VIRUS && checkPropMutation())
                    {
                        context.state = new MutationState();
                    }


                    if (context.enemyType != Context.EnemyType.VIRUS)
                    {
                        context.receiveDamageMutate();
                    }
                    else context.receiveDamage();

                    break;

                case BulletType.SHOOTGUN:
                    context.life -= shootgunDamage / 2;

                    if (context.enemyType != Context.EnemyType.PARASITE && checkPropMutation())
                    {
                        context.state = new MutationState();
                    }

                    if (context.enemyType != Context.EnemyType.PARASITE)
                    {
                        context.receiveDamageMutate();
                    }
                    else context.receiveDamage();

                    break;

                case BulletType.SNIPER:

                    if (context.enemyType != Context.EnemyType.BACTERIA && checkPropMutation())
                    {
                        context.state = new MutationState();
                    }
                    else
                    {
                        context.life -= sniperDamage / 2;
                    }

                    if (context.enemyType != Context.EnemyType.BACTERIA)
                    {
                        context.receiveDamageMutate();
                    }
                    else context.receiveDamage();

                    break;
            }

			AudioSource.PlayClipAtPoint (bulletHitHard, transform.position);
			if (context.life <= 0) {
				AudioSource.PlayClipAtPoint (enemyScreech, transform.position, 4.0f);
			}
			else AudioSource.PlayClipAtPoint (enemyDamage, transform.position, 4.0f);
			Destroy (transform.gameObject);
         }
        // Hit on spawn Point
        else if (other.gameObject.tag == "SpawnPoint")
        {
            switch (bulletType)
            {
                case BulletType.ASSAULT:
                    other.gameObject.GetComponent<SpawnControl>().health = assaultRifleDamage / 2;
                    break;

                case BulletType.SHOOTGUN:
                    other.gameObject.GetComponent<SpawnControl>().health = shootgunDamage / 2;
                    break;

                case BulletType.SNIPER:
                    other.gameObject.GetComponent<SpawnControl>().health = sniperDamage / 2;
                    break;
            }
         }
        
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy") {
			AudioSource.PlayClipAtPoint (bulletHitSoft, transform.position);
			Destroy (transform.gameObject);
		}
    }

    bool checkPropMutation()
    {
        int mutation = Random.Range(0, 9);

        if (mutation <= 2)
            return true;
        else
            return false;

    }
}
