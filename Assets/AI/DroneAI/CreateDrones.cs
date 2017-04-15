using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDrones : MonoBehaviour {

    public GameObject Drone;

    public GameObject spawnPointOne;
    public GameObject spawnPointTwo;
    public GameObject spawnPointThree;


    public GameObject wound;
    public GameObject healthPointOne;
    public GameObject healthPointTwo;
    public GameObject healthPointThree;

    public GameObject spawnParticleSystemOne;
    public GameObject spawnParticleSystemTwo;
    public GameObject spawnParticleSystemThree;
    public float timeToCreateDrones=1.5f;
    public float timeToDeactivateParticles = 3.0f;

    public bool spawnedDrones = false;
    public bool canDeployDrones = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        CallSpawnDrones();

    }

    void CallSpawnDrones()
    {
        if (Input.GetKeyDown(KeyCode.F) && spawnedDrones== false && canDeployDrones==true)
        {
            spawnParticleSystemOne.SetActive(true);
            spawnParticleSystemTwo.SetActive(true);
            spawnParticleSystemThree.SetActive(true);
            Invoke("SpawnDrones", timeToCreateDrones);
            spawnedDrones = true;
        }
    }

    void SpawnDrones()
    {
        GameObject drone = Instantiate(Drone, spawnPointOne.transform.position, spawnPointOne.transform.rotation);
        drone.GetComponent<DroneContext>().wound = wound;
        drone.GetComponent<DroneContext>().woundPosition = healthPointOne;
        drone.GetComponent<DroneContext>().player = transform.gameObject;

        drone = Instantiate(Drone, spawnPointTwo.transform.position, spawnPointOne.transform.rotation);
        drone.GetComponent<DroneContext>().wound = wound;
        drone.GetComponent<DroneContext>().woundPosition = healthPointTwo;
        drone.GetComponent<DroneContext>().player = transform.gameObject;

        drone = Instantiate(Drone, spawnPointThree.transform.position, spawnPointOne.transform.rotation);
        drone.GetComponent<DroneContext>().wound = wound;
        drone.GetComponent<DroneContext>().woundPosition = healthPointThree;
        drone.GetComponent<DroneContext>().player = transform.gameObject;

        Invoke("deactivateParticles", timeToDeactivateParticles);
    }

    void deactivateParticles()
    {
        spawnParticleSystemOne.SetActive(false);
        spawnParticleSystemTwo.SetActive(false);
        spawnParticleSystemThree.SetActive(false);
    }
}
