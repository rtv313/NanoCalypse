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
    public AudioClip damageClip;                          
    public float flashSpeed = 5f;                              
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	private interfaceManager playerInterface;
	private ScoreManager scoreManager;
	public bool dead;
    

    public Animator animator;                                             
    private AudioSource playerAudio;                                    
    private PlayerMovement playerMovement;                              
    //PlayerShooting playerShooting;                              
    private bool isDead = false;                                                
    private bool damaged, godmode, devTools;  
	public Text godModeText, devToolsText;


    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
		playerInterface = GameObject.Find ("GUI").GetComponent<interfaceManager> () as interfaceManager;
		scoreManager = GameObject.Find ("GUI").GetComponent<ScoreManager> () as ScoreManager;
		// playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
		dead = false;
		godmode = false;
    }


    void Update()
    {
		if (Input.GetKeyDown (KeyCode.F12)||Input.GetKeyDown (KeyCode.Delete))
			devTools = !devTools;

		if (!devTools)
			godmode = false;
		
		if (devTools) {
			if (Input.GetKeyDown (KeyCode.Alpha0))
				godmode = !godmode;
			if (Input.GetKeyDown (KeyCode.Alpha9))
				currentHealth += 20;
			if (Input.GetKeyDown (KeyCode.Alpha8))
				currentHealth -= 20;
			if (Input.GetKeyDown (KeyCode.Alpha7)) {
				this.transform.position = GameObject.Find ("end_TP").transform.position;
			}
			if (Input.GetKeyDown (KeyCode.Alpha6)) {
				this.transform.position = GameObject.Find ("mid_TP").transform.position;
			}
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				this.transform.position = GameObject.Find ("start_TP").transform.position;
			}
		
		}
		godModeText.enabled = godmode;
		devToolsText.enabled = devTools;
		
		playerInterface.updateHealthBar (currentHealth, startingHealth);
        //if (damaged)
        //{
        //   damageImage.color = flashColour;
        //}

        //else
        //{
        //   damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}
        if (currentHealth <= 0 && isDead == false)
        {
            Death();
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
		if(!godmode){
	        damaged = true;
	        currentHealth -= amount;
			scoreManager.playerTookDamage();
	        playerAudio.clip = damageClip;
	        //playerAudio.volume = 0.7f;
	        playerAudio.Play();

	        if (currentHealth <= 0 && isDead == false )
	        {
			 	dead = true;
	         	Death();
	        }
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