using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScreenManager : MonoBehaviour {
	private Text FinalScore, FinalKillCount, FinalHighestMult, FinalTime;
	private interfaceManager MainInterface;
	// Use this for initialization
	void Start () {
		FinalScore = GameObject.Find ("SS_score").GetComponent<Text> ();
		FinalKillCount = GameObject.Find ("SS_KO_Count").GetComponent<Text> ();
		FinalHighestMult = GameObject.Find ("SS_highest_mult").GetComponent<Text> ();
		FinalTime = GameObject.Find ("SS_time").GetComponent<Text> ();
		MainInterface = GameObject.Find ("GUI").GetComponent<interfaceManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.GetComponent<Canvas> ().enabled == true) {
			FinalScore.text = "Score: \t\t\t\t\t\t\t" + MainInterface.getScore ().ToString ();
			FinalKillCount.text = "Kills: \t\t\t\t\t\t\t" + MainInterface.getKillCount().ToString();
			FinalHighestMult.text = "Highest Multiplier: \t" + MainInterface.getHighestMult ().ToString ();
			int GameTime = (int)Mathf.Floor(MainInterface.getTimeInLevel ());
			FinalTime.text = "Time: \t\t\t\t\t\t\t" + Mathf.Floor(GameTime/60).ToString("00") + " : " + (GameTime%60).ToString("00");
		}	
	}
}
