using UnityEngine;

public class DestroyDrone : MonoBehaviour
{
    public void destroyDrone(GameObject drone)
    {
        Destroy(drone);
    }
}

public class ReturnPlayerState : DroneState
{

    public override void Handle(DroneContext context)
    {
        context.stateString = "Returning";
        ReturnToPlayer(context);
    }

    private void ReturnToPlayer(DroneContext context)
    {
        context.nav.enabled = true;
        context.nav.SetDestination(context.player.transform.position);

        float dist = Vector3.Distance(context.player.transform.position, context.transform.position);

        if (dist < 1)
        {
            DestroyDrone destroy = new DestroyDrone();
            destroy.destroyDrone(context.transform.gameObject);
        }
    }
}
