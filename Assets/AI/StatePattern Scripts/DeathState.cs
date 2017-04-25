
using UnityEngine;

public class DestroyInterfaze : MonoBehaviour
{
    public void DestroyObject(GameObject gameObject, float timeToDestroy)
    {
        Destroy(gameObject, timeToDestroy);
    }
}

public class DeathState : State {

    private bool isSinking = false;
    private bool callAnimation = false;
    public override void Handle(Context context)
    {
        context.stateString = "Death";
        AnimationControl(context);
        if (isSinking == true)
        {
            Death(context);
        }
        else {
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
