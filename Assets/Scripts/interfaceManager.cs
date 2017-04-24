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
	private float angle;
	private ScoreManager scoreManager;
	// Use this for initialization
	void Start () {
		HealthBar = GameObject.Find("Healthbar").GetComponent<Slider> ();
		HeatBar = GameObject.Find("HeatBar").GetComponent<Slider> ();
		WeaponSelector = GameObject.Find("WeaponSelector").GetComponent<Image>();
		KillCount = GameObject.Find ("KillCount").GetComponent<Text> ();
		ComboMultiplier = GameObject.Find ("ComboMultiplier").GetComponent<Text> ();
		Score = GameObject.Find ("Score").GetComponent<Text> ();
		scoreManager = GameObject.Find ("player UI").GetComponent<ScoreManager> () as ScoreManager;
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
	}
	void Update(){
		WeaponSelector.transform.rotation = Quaternion.Slerp (WeaponSelector.transform.rotation, Quaternion.Euler (0, 0, angle), Time.deltaTime*5); // Updates the rotation of the weapon selector

		// Updating the scores using the banks
		// Using banks gets the effect that the score increases overtime instead of instantly changing the number
		if (multiplierBank > 0) {
			multiplierBank--;
			multiplier++;
		}
		if (multiplierNegativeBank < 0) {
			multiplierNegativeBank++;
			if (multiplier > 1) {
				multiplier--;
			}
		}
		if (scoreNumberBank > 0) {
			scoreNumberBank--;
			scoreNumber++;
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
		// 225 -> Shotgun
		switch (number) {
		case 1:
			HeatBar.gameObject.SetActive (true);
			angle = -15;
			break;
		case 2:
			HeatBar.gameObject.SetActive (false);
			angle = 225;
			break;
		case 3:
			HeatBar.gameObject.SetActive (false);
			angle = 105;
			break;
		default:
			HeatBar.gameObject.SetActive (true);
			angle = -15;
			break;
		}
	
	}
}
