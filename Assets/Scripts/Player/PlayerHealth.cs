using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;                         
    public float currentHealth;
    public Image HealthBar;
    public Image damageImage;                                   
    public AudioClip deathClip;                                 
    public float flashSpeed = 5f;                              
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    

    Animator anim;                                             
    AudioSource playerAudio;                                    
    PlayerMovement playerMovement;                              
    //PlayerShooting playerShooting;                              
    bool isDead;                                                
    bool damaged;                                              


    void Awake()
    {
       
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
       // playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }


    void Update()
    {
       
        if (damaged)
        {
           damageImage.color = flashColour;
        }
    
        else
        {
           damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        
        damaged = true;
        currentHealth -= amount;
        float healthRatio = currentHealth / startingHealth;
        HealthBar.rectTransform.localScale = new Vector3(healthRatio, 1, 1);
        playerAudio.Play();

        
        if (currentHealth <= 0 && !isDead)
        {
         Death();
        }
    }


    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerMovement.enabled = false;
    }
}