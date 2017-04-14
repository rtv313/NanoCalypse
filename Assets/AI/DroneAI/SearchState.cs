using UnityEngine;

public class SearchState : DroneState {

    public override void Handle(DroneContext context)
    {
        context.stateString = "SearchWound";
        SearchWound(context);
        Transition(context);
    }

    private void SearchWound(DroneContext context)
    {
        context.nav.enabled = true;
        context.nav.SetDestination(context.woundPosition.transform.position);
    }

    private void Transition(DroneContext context)
    {
        if (context.life <= 0)
        {
            context.state = new DeathDroneState();
            return;
        }

        float dist = Vector3.Distance(context.woundPosition.transform.position, context.transform.position);

        if (dist < context.healthDistance)
        {
            context.state = new HealthState();
            return;
        }
    }
}
