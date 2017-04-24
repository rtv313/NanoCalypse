using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public struct score
{
	public int points;
	public int pointsMutated;
};
public struct timeValues
{
	public float notDidDamage;			// Ammount of time that has to pass without the player doing damage for the multiplier to decrement.
	public float inmunityTime; 			// During this time if the player takes damage again the multipler wont be decremented
	public float dronesDidntTakeDamage; // The ammount of time drones have to be active and not take damage for the multiplier to increase
};
public class ScoreManager : MonoBehaviour
{
	private interfaceManager playerInterface;
	private score[] scoreEnemies;
	private timeValues timeVal;
	private float timeSinceDidDamage, timeSinceTookDamage, timeSinceDronesTookDamage;
	private int noMutatedCounter;
	private bool dronesActive;

    void Awake()
    {
		playerInterface = GameObject.Find ("player UI").GetComponent<interfaceManager> () as interfaceManager;
		scoreEnemies = new score[3];
		// Virus
		scoreEnemies [0].points = 200;
		scoreEnemies [0].pointsMutated = 100;
		// Bacteria
		scoreEnemies [1].points = 100;
		scoreEnemies [1].pointsMutated = 50;
		//Parasite
		scoreEnemies [2].points = 50;
		scoreEnemies [2].pointsMutated = 25;

		// Time Values
		timeVal.notDidDamage = 10.0f;
		timeVal.inmunityTime = 2.0f;
		timeVal.dronesDidntTakeDamage = 5.0f;

		noMutatedCounter = 0;
		timeSinceDidDamage = Time.time;
		timeSinceDronesTookDamage = Time.time;
		noMutatedCounter = 0;

		dronesActive = false;

    }


    void Update()
    {
		if(Time.time - timeSinceDidDamage >= timeVal.notDidDamage)// If the player did not do damage for 10s multiplier decreases by 1
		{
			playerInterface.decreaseMultiplier (1);
			timeSinceDidDamage = Time.time;
			Debug.Log ("not damage in 10");
		}
		if (noMutatedCounter >= 5) { // Player kills 5 consecutive enemies that are not mutated;
			noMutatedCounter = 0;
			playerInterface.increaseMultiplier (1);
			Debug.Log ("Killed 5 no mutated");
		}
		if(dronesActive && (Time.time - timeSinceDronesTookDamage > timeVal.dronesDidntTakeDamage)){
			timeSinceDronesTookDamage = Time.time;
			playerInterface.increaseMultiplier (1);

		}
    }

	public void enemyKilledByPlayer(int type, bool mutated )
	{
		// Check if enemy is mutated
		if (mutated) {
			playerInterface.addScore (scoreEnemies [type].pointsMutated);
			noMutatedCounter = 0;
		}
		if (!mutated) {
			playerInterface.addScore (scoreEnemies [type].points);
			noMutatedCounter++;
		}
		// Start coroutine to increase multiplier (Multiplier increases if player kills 5 enemies in 10s)
		playerInterface.increaseKillCount(1);
		StartCoroutine (checkEnemiesKilledInTenSeconds(playerInterface.getKillCount()));
	}

	public void playerDidDamage(){
		timeSinceDidDamage = Time.time;
	}

	public void playerTookDamage(){
		
		if (Time.time - timeSinceTookDamage > timeVal.inmunityTime) {
			timeSinceTookDamage = Time.time;
			playerInterface.decreaseMultiplier (1);
			Debug.Log ("Mult decreased Player took damage");
		}
	}

	public void playerDied(){
		playerInterface.setMultiplier (1);
	}

	public void dronesDied(){
		playerInterface.setMultiplier (1);
	}

	public void dronesTookDamage(){
		if (Time.time - timeSinceDronesTookDamage > timeVal.inmunityTime) {
			timeSinceDronesTookDamage = Time.time;
			playerInterface.decreaseMultiplier (1);
			Debug.Log ("Mult decreased");
		}
	}

	public void activateDrones(){
		dronesActive = true;
	}
	public void deactivateDrones(){
		dronesActive = false;
	}

	public IEnumerator checkEnemiesKilledInTenSeconds(int enemiesKilledAtStart){
		yield return new WaitForSeconds (5.0f);

		if(playerInterface.getKillCount() - enemiesKilledAtStart >= 3){
			playerInterface.increaseMultiplier (1);
			Debug.Log("killed 3 y 5");
			StopAllCoroutines ();
		}
	}
}