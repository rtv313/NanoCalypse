using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
public class interfaceManager : MonoBehaviour {
	private Slider HealthBar, HeatBar;
	private Image WeaponSelector;
	private int multiplier, scoreNumber, killNumber, multiplierBank, scoreNumberBank, killNumberBank, multiplierNegativeBank;
	private Text KillCount, ComboMultiplier, Score;
	private float angle, timmerScoreScound,timmerMultiplierScound;
	private ScoreManager scoreManager;

	private AudioSource sourceScore, sourceMultiplierIncrease,sourceMultiplierDecrease, sourceWeaponSelector;
	private AudioSource [] sounds;
	public AudioClip scoreSound, MultIncSound, MultDecSound, WeaponSelSound;
	// Use this for initialization
	void Start () {
		// Hide Mouse Cursor
		//Cursor.visible = false;

		HealthBar = GameObject.Find("Healthbar").GetComponent<Slider> ();
		HeatBar = GameObject.Find("HeatBar").GetComponent<Slider> ();
		WeaponSelector = GameObject.Find("WeaponSelector").GetComponent<Image>();
		KillCount = GameObject.Find ("KillCount").GetComponent<Text> ();
		ComboMultiplier = GameObject.Find ("ComboMultiplier").GetComponent<Text> ();
		Score = GameObject.Find ("Score").GetComponent<Text> ();
		scoreManager = GameObject.Find ("GUI").GetComponent<ScoreManager> () as ScoreManager;
		// Operating values
		multiplier = 1;
		scoreNumber = 0;
		killNumber = 0;

		//Banks
		multiplierBank = 0;
		scoreNumberBank = 0; 
		killNumberBank = 0;
		multiplierNegativeBank = 0;

		HealthBar.value = 1.0f;
		angle = -15;


		// Sounds

		gameObject.AddComponent<AudioSource> ();
		gameObject.AddComponent<AudioSource> ();
		gameObject.AddComponent<AudioSource> ();
		gameObject.AddComponent<AudioSource> ();
		sounds = GetComponents<AudioSource> ();

		sourceScore = sounds [0];
		sourceMultiplierIncrease = sounds [1];
		sourceMultiplierDecrease = sounds [2];
		sourceWeaponSelector = sounds [3];

		sourceScore.loop = true;
		sourceScore.playOnAwake = false;
		timmerScoreScound = Time.time;
		timmerMultiplierScound = Time.time;





	}
	void Update(){
		
		WeaponSelector.transform.rotation = Quaternion.Slerp (WeaponSelector.transform.rotation, Quaternion.Euler (0, 0, angle), Time.deltaTime*5); // Updates the rotation of the weapon selector

		// Updating the scores using the banks
		// Using banks gets the effect that the score increases overtime instead of instantly changing the number
		if (multiplierBank > 0) {
			multiplierBank--;
			multiplier++;
			sourceMultiplierIncrease.PlayOneShot(MultIncSound);
		}
		if (multiplierNegativeBank < 0) {
			multiplierNegativeBank++;
			if (multiplier > 1) {
				multiplier--;
				playMultiplierDecreaseSound ();
			}
		}
		if (scoreNumberBank > 0) {
			playScoreIncreaseSound ();
			if (scoreNumberBank > 300) {
				scoreNumberBank -= 10;
				scoreNumber += 10;
			} else if (scoreNumberBank > 100) {
				scoreNumberBank -= 5;
				scoreNumber += 5;
			} else {
				scoreNumberBank--;
				scoreNumber++;
			}
		} else {
			if (sourceScore.volume > 0.0f) 
				StartCoroutine (fadeAudio(sourceScore));
		}
		if (killNumberBank > 0) {
			killNumberBank--;
			killNumber++;
		}

		KillCount.text = killNumber.ToString() + " K.O.";
		ComboMultiplier.text = "X  " + multiplier.ToString();
		Score.text = scoreNumber.ToString() + "  points";

			
	}
	public void updateHealthBar(float currentHealth, float maxHealth){ // Updates the HealthBar
		HealthBar.value = currentHealth / maxHealth;
	}
	public void updateHeatBar(float currentHeat, float maxHeat){ // Updates the Heat Bar 
		HeatBar.value = currentHeat / maxHeat;
	}
	public void addScore(int points){
		scoreNumberBank += multiplier * points;
	}
	public void setMultiplier(int mult){
		if(mult >= 1)
		{
			multiplier = mult;
		}
	}
	public void increaseMultiplier (int multInc){
		multiplierBank += multInc;
		scoreManager.playerDidDamage ();
	}
	public void decreaseMultiplier (int multInc){
		multiplierNegativeBank -= Mathf.Abs(multInc);
	}
	public void increaseKillCount(int numKills){
		killNumberBank += numKills;
	}
	public void scoreReset(){
		killNumber = 0;
		multiplier = 1;
		scoreNumber = 0;
	}
	public int getKillCount(){
		return killNumber + killNumberBank;
	}

	public void selectWeapon(int number){
		// Set the angle for the weapon selector
		// -15 -> Assault Rifle
		// 105 -> Sniper
		// 225 -> Shotgun+
		//sourceWeaponSelector.PlayOneShot(WeaponSelSound);
		switch (number) {
		case 1:
			HeatBar.gameObject.SetActive (true);
			if (angle != -15) {
				angle = -15;
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);
			}

			break;
		case 2:
			HeatBar.gameObject.SetActive (false);
			if (angle != 225) {
				angle = 225;
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);
			}

			break;
		case 3:
			HeatBar.gameObject.SetActive (false);
			if (angle != 105) {
				angle = 105;
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);
			}
			break;
		default:
			HeatBar.gameObject.SetActive (true);
			if (angle != -15) {
				angle = -15;
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);
			}
			break;
		}
	
	}
	private void playScoreIncreaseSound(){
		if (Time.time - timmerScoreScound > 2.5f) {
			sourceScore.volume = 0.3f;
			sourceScore.Play ();
			timmerScoreScound = Time.time;
		}
	}
	private void playMultiplierDecreaseSound(){
		if (Time.time - timmerMultiplierScound > 2.5f) {
			sourceMultiplierDecrease.PlayOneShot (MultDecSound);
			timmerMultiplierScound = Time.time;
		}
	}
	public IEnumerator fadeAudio(AudioSource audioToFade){
		while(audioToFade.volume > 0.0f){
			audioToFade.volume -= Time.deltaTime;
			yield return new WaitForSeconds (0.2f);
			//Debug.Log (audioToFade.volume);
		}
	}
}
