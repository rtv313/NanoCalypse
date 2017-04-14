using UnityEngine;

public class DeathDroneState : DroneState
{
    private bool isSinking = false;

    public override void Handle(DroneContext context)
    {
        context.stateString = "Death";
        if (isSinking == true)
        {
            Death(context);
        }
        else
        {
            StartSinking(context);
        }
    }

    private void Death(DroneContext context)
    {
        context.transform.Translate(-Vector3.up * context.sinkSpeed * Time.deltaTime);
    }

    private void StartSinking(DroneContext context)
    {
        context.rigidbody.isKinematic = true;
        context.nav.enabled = false;
        context.capsuleCollider.isTrigger = true;
        isSinking = true;
        DestroyInterfaze destroyInterface = new DestroyInterfaze();
        destroyInterface.DestroyObject(context.gameObject, context.timeDestroy);
    }
}
