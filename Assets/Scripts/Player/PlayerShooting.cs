using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot1 = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets1 = 0.15f;        // The time between each shot.
    public float range1 = 100f;                      // The distance the gun can fire.

    public int damagePerShot2 = 5;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets2 = 0.05f;        // The time between each shot.
    public float range2 = 100f;                      // The distance the gun can fire.
    public int pellets = 5;
    public float spreadAngle = 30.0f;

    public int damagePerShot3 = 100;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets3 = 1.0f;        // The time between each shot.
    public float range3 = 100f;                      // The distance the gun can fire.

    float timer;                                    // A timer to determine when to fire.
    Ray shootRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    LineRenderer gunLine;                           // Reference to the line renderer.
    AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    int fireMode = 1;

    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireMode = 1;
            timer = 0.0f;
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fireMode = 2;
            timer = 0.0f;
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fireMode = 3;
            timer = 0.0f;
        }

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetButton("Fire1")) // && timer >= timeBetweenBullets)
        {
            // ... shoot the gun.
            if (fireMode == 1 && timer >= timeBetweenBullets1)
                Shoot();
            if (fireMode == 2 && timer >= timeBetweenBullets2)
                Shoot();
            if (fireMode == 3 && timer >= timeBetweenBullets3)
                Shoot();
        }

        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets1 * effectsDisplayTime)
        {
            // ... disable the effects.
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        float range = 0.0f;
        int damagePerShot = 0;
        if (fireMode == 1)
        {
            range = range1;
            damagePerShot = damagePerShot1;
        }
        if (fireMode == 2)
        {
            range = range2;
            damagePerShot = damagePerShot2;
        }
        if (fireMode == 3)
        {
            range = range3;
            damagePerShot = damagePerShot3;
        }

        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop();
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null)
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}