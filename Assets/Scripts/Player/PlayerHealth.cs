﻿using UnityEngine;
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
		playerInterface = GameObject.Find ("player UI").GetComponent<interfaceManager> () as interfaceManager;
		scoreManager = GameObject.Find ("player UI").GetComponent<ScoreManager> () as ScoreManager;
		// playerShooting = GetComponentInChildren<PlayerShooting>();
        currentHealth = startingHealth;
    }


    void Update()
    {
		
		playerInterface.updateHealthBar (startingHealth, currentHealth);
        //if (damaged)
        //{
        //   damageImage.color = flashColour;
        //}
    
        //else
        //{
        //   damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        //}

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
        //anim.SetTrigger("Die");
        //playerAudio.clip = deathClip;
        //playerAudio.Play();
        //playerMovement.enabled = false;
        //SceneManager.LoadScene("TitleMenu", LoadSceneMode.Single);
    }
}