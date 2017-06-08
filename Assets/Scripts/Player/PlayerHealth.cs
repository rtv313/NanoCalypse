using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;                         
    public float currentHealth;
    public Image HealthBar;
    public Image damageImage;                                   
    public AudioClip deathClip;                                 
    public float flashSpeed = 5f;                              
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	private interfaceManager playerInterface;
	private ScoreManager scoreManager;
    

    public Animator animator;                                             
    AudioSource playerAudio;                                    
    PlayerMovement playerMovement;                              
    //PlayerShooting playerShooting;                              
    bool isDead;                                                
    bool damaged;                                              


    void Awake()
    {
       
        
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
		playerInterface = GameObject.Find ("GUI").GetComponent<interfaceManager> () as interfaceManager;
		scoreManager = GameObject.Find ("GUI").GetComponent<ScoreManager> () as ScoreManager;
		// playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }


    void Update()
    {
		
		playerInterface.updateHealthBar (currentHealth, startingHealth);
        //if (damaged)
        //{
        //   damageImage.color = flashColour;
        //}

        //else
        //{
        //   damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}
        if (currentHealth <= 0)
        {
            Death();
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        
        damaged = true;
        currentHealth -= amount;
		scoreManager.playerTookDamage();
        
        if (currentHealth <= 0 && !isDead)
        {
         Death();
        }
    }


    void Death()
    {
        isDead = true;
		scoreManager.playerDied ();
        animator.SetTrigger("Die");
        //playerAudio.clip = deathClip;
        //playerAudio.Play();
        //playerMovement.enabled = false;
		StartCoroutine(returnToTitleScreen());
    }

	public IEnumerator returnToTitleScreen(){
		//Renderer rend = GetComponent<Renderer> ();
		yield return new WaitForSeconds (2.0f);
		SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        Destroy(this);
    }

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "KillPlane")
		{
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            Destroy(this);
        }
	}
}