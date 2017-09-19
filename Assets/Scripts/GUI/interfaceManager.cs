using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interfaceManager : MonoBehaviour {
	private Slider HealthBar, HeatBar;
	private Image WeaponSelector;
	private int multiplier, scoreNumber, spRemainingNumber, killNumber, multiplierBank, scoreNumberBank, killNumberBank, multiplierNegativeBank, highestMult;
	private Text KillCount, ComboMultiplier, Score, SpawnPointsRemaining;
	private float angle, timmerScoreScound,timmerMultiplierScound, lowAlpha, highAlpha, TimeInLevel;
	private ScoreManager scoreManager;
	private AudioSource sourceScore, sourceMultiplierIncrease,sourceMultiplierDecrease, sourceWeaponSelector;
	private AudioSource [] sounds;
	private imageFadin red_img, blue_img, green_img;
	public AudioClip scoreSound, MultIncSound, MultDecSound, WeaponSelSound;
	// Use this for initialization
	void Start () {

		// Weapon images
		red_img = GameObject.Find("red_image").GetComponent<imageFadin>();
		blue_img = GameObject.Find("blue_image").GetComponent<imageFadin>();
		green_img = GameObject.Find("green_image").GetComponent<imageFadin>();
		// Hide Mouse Cursor
		//Cursor.visible = false;

		HealthBar = GameObject.Find("Healthbar").GetComponent<Slider> ();
		HeatBar = GameObject.Find("HeatBar").GetComponent<Slider> ();
		WeaponSelector = GameObject.Find("WeaponSelector").GetComponent<Image>();
		KillCount = GameObject.Find ("KillCount").GetComponent<Text> ();
		ComboMultiplier = GameObject.Find ("ComboMultiplier").GetComponent<Text> ();
		Score = GameObject.Find ("Score").GetComponent<Text> ();
        SpawnPointsRemaining = GameObject.Find("SpawnPointsRemaining").GetComponent<Text>();
        scoreManager = GameObject.Find ("GUI").GetComponent<ScoreManager> () as ScoreManager;
		// Operating values
		multiplier = 1;
		highestMult = 1;
		scoreNumber = 0;
		killNumber = 0;
        spRemainingNumber = 14; // Needs to be set to correct amount. Should be 14.

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

		lowAlpha = 0.1f;
		highAlpha = 1.0f;

		green_img.SetPositionZ (0);
		blue_img.SetPositionZ (1);
		red_img.SetPositionZ (2);


		red_img.changeAlpha (highAlpha);
		blue_img.changeAlpha (lowAlpha);
		green_img.changeAlpha (lowAlpha);
		TimeInLevel = 0.0f;


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
		if (multiplier > highestMult)
			highestMult = multiplier;
		
		KillCount.text = killNumber.ToString() + " K.O.";
		ComboMultiplier.text = "x  " + multiplier.ToString();
		Score.text = scoreNumber.ToString();

		TimeInLevel += Time.deltaTime * Time.timeScale;
			
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
    public void updateSpawnPointsRemaining() {
        --spRemainingNumber;
        SpawnPointsRemaining.text = ""+spRemainingNumber;
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
				red_img.changeAlpha (highAlpha);
				red_img.SetPositionZ (2);
				blue_img.changeAlpha (lowAlpha);
				blue_img.SetPositionZ (1);
				green_img.changeAlpha (lowAlpha);
				green_img.SetPositionZ (0);
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);

			}

			break;
		case 2:
			HeatBar.gameObject.SetActive (false);
			if (angle != 225) {
				angle = 225;
				red_img.changeAlpha (lowAlpha);
				red_img.SetPositionZ (1);
				blue_img.changeAlpha (highAlpha);
				blue_img.SetPositionZ (2);
				green_img.changeAlpha (lowAlpha);
				green_img.SetPositionZ (0);
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);
			}

			break;
		case 3:
			HeatBar.gameObject.SetActive (false);
			if (angle != 105) {
				angle = 105;
				red_img.changeAlpha (lowAlpha);
				red_img.SetPositionZ (0);
				blue_img.changeAlpha (lowAlpha);
				blue_img.SetPositionZ (1);
				green_img.changeAlpha (highAlpha);
				green_img.SetPositionZ (2);
				sourceWeaponSelector.PlayOneShot(WeaponSelSound);
			}
			break;
		default:
			HeatBar.gameObject.SetActive (true);
			if (angle != -15) {
				angle = -15;
				red_img.changeAlpha (highAlpha);
				red_img.SetPositionZ (2);
				blue_img.changeAlpha (lowAlpha);
				blue_img.SetPositionZ (1);
				green_img.changeAlpha (lowAlpha);
				green_img.SetPositionZ (0);
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
	public float getTimeInLevel(){
		return TimeInLevel;
	}
	public int getScore(){
		return scoreNumber + scoreNumberBank;
	}
	public int getHighestMult (){
		return highestMult;
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

    public int getRemainingSpawnPoints()
    {
        return spRemainingNumber;
    }
}
