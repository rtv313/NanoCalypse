using UnityEngine;

public class HealthState : DroneState {
    public float time = 0;
    private bool targetOnSight = false;
    private bool healingActive = false;
    public override void Handle(DroneContext context)
    {
        context.stateString = "Health";
        Health(context);
        Transition(context);
    }

    private void Health(DroneContext context)
    {
        context.nav.enabled = false;

        if (targetOnSight == false)
        {
            Vector3 direction = (context.wound.transform.position - context.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            context.transform.rotation = Quaternion.Slerp(context.transform.rotation, lookRotation, Time.deltaTime * 3);

            if (context.transform.rotation == lookRotation)
                targetOnSight = true;
        }
        else 
        {
            context.healParticleSystem.SetActive(true);
            context.wound.GetComponent<WoundHealth>().startTimer = true;
        }
   }

    private void Transition(DroneContext context)
    {
       

        if (context.life <= 0)
        {
            context.healParticleSystem.SetActive(false);
            context.state = new DeathDroneState();
            return;
        }

        if (context.wound.GetComponent<WoundHealth>().finishedHealing==true)
        {
            context.healParticleSystem.SetActive(false);
            context.state = new ReturnPlayerState();
        }
    }
}
