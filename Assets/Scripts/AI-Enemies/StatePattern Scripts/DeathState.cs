
using UnityEngine;

public class DestroyInterfaze : MonoBehaviour
{
    public void DestroyObject(GameObject gameObject, float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
    }
}

public class DeathState : State
{

    private GameObject explosionPS;
    private bool isSinking = false;
    private bool callAnimation = false;
    private bool exploded = false;

    public override void Handle(Context context)
    {
        explosionPS = context.explosion;
        context.stateString = "Death";
        AnimationControl(context);
        if (isSinking == true)
        {
            Death(context);
        }
        else
        {
            if (context.enemyType == Context.EnemyType.BACTERIA && exploded == false)
            {
                exploded = true;
                GameObject explosion = GameObject.FindGameObjectWithTag("BacteriaExpPool").GetComponent<BacteriaExplosionsPool>().GetBacteriaExplosion(context.transform);
                explosion.GetComponent<BacteriaExplosion>().EnableExplosion();
                DestroyInterfaze destroyInterface = new DestroyInterfaze();
                destroyInterface.DestroyObject(context.gameObject,0.0f);
                return;
            }
            StartSinking(context);
        }
    }

    private void AnimationControl(Context context)
    {
        if (callAnimation == false)
        {
            context.animator.SetTrigger("Death");
            callAnimation = true;
        }
    }


    private void Death(Context context)
    {
        context.transform.Translate(-Vector3.up * context.sinkSpeed * Time.deltaTime);
    }

    private void StartSinking(Context context)
    {
		context.scoreManager.enemyKilledByPlayer (context.enemyType.GetHashCode (), context.mutaded);
        context.rigidbody.isKinematic = true;
        context.nav.enabled = false;
        context.capsuleCollider.isTrigger = true;
        isSinking = true;
        DestroyInterfaze destroyInterface = new DestroyInterfaze();
        destroyInterface.DestroyObject(context.gameObject, context.timeDestroy);
    }
    
}
