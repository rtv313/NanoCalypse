using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class interfaceManager : MonoBehaviour {
	private Slider HealthBar, HeatBar;
	private Image WeaponSelector;
	private int multiplier, scoreNumber, killNumber, multiplierBank, scoreNumberBank, killNumberBank;
	private Text KillCount, ComboMultiplier, Score;
	private float angle;
	// Use this for initialization
	void Start () {
		HealthBar = GameObject.Find("Healthbar").GetComponent<Slider> ();
		HeatBar = GameObject.Find("HeatBar").GetComponent<Slider> ();
		WeaponSelector = GameObject.Find("WeaponSelector").GetComponent<Image>();
		KillCount = GameObject.Find ("KillCount").GetComponent<Text> ();
		ComboMultiplier = GameObject.Find ("ComboMultiplier").GetComponent<Text> ();
		Score = GameObject.Find ("Score").GetComponent<Text> ();
		multiplier = 1;
		scoreNumber = 0;
		killNumber = 0;
		multiplierBank = 0;
		scoreNumberBank = 0; 
		killNumberBank = 0;
		HealthBar.value = 1.0f;
		angle = -15;
	}
	void Update(){
		WeaponSelector.transform.rotation = Quaternion.Slerp (WeaponSelector.transform.rotation, Quaternion.Euler (0, 0, angle), Time.deltaTime*5); // Updates the rotation of the weapon selector

		if (multiplierBank > 0) {
			multiplierBank--;
			multiplier++;
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
		multiplier = mult;
	}
	public void increaseMultiplier (int multInc){
		multiplierBank += multInc;
	}
	public void increaseKillCount(int numKills){
		killNumberBank += numKills;
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
