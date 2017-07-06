using UnityEngine;

public class WoundHealth : MonoBehaviour {

    public bool startTimer = false;
    public float timeToHeal = 60f;
    public bool finishedHealing = false;
    public float time = 0;
    public GameObject healthPointOne;
    public GameObject healthPointTwo;
    public GameObject healthPointThree;
    private bool flagEnter = false;
    // Update is called once per frame
    void Update () {

        if (startTimer == true)
        {
            timer();
        }
	}

    void timer()
    {
        time += Time.deltaTime;

        if (time >= timeToHeal)
        {
            finishedHealing = true;
            Invoke("EnableDronesCreation", 3.0f); 
        }
    }

    void EnableDronesCreation()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CreateDrones>().spawnedDrones = false;
        player.GetComponent<CreateDrones>().canDeployDrones = false;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && flagEnter==false)
        {
            CreateDrones script = other.gameObject.GetComponent<CreateDrones>();
            script.canDeployDrones = true;
            script.wound = transform.gameObject;
            script.healthPointOne = healthPointOne;
            script.healthPointTwo = healthPointTwo;
            script.healthPointThree = healthPointThree;
            flagEnter = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           other.gameObject.GetComponent<CreateDrones>().canDeployDrones = false;
        }
    }
}
