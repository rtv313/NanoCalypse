using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mine_ui_counter : MonoBehaviour {
	private PlayerShockWave mines;
	public Image[] mine_images;
	private int num_mines;
	// Use this for initialization
	void Start () {
		mines = GameObject.Find ("Player").GetComponent<PlayerShockWave> ();
		num_mines = mines.minesLimit - mines.mineCounter;
	}
	
	// Update is called once per frame
	void Update () {
		num_mines = mines.minesLimit - mines.mineCounter;
		for (int i = 0; i < 5; i++) {
			mine_images [i].color = new Color (0.4f,0.4f,0.4f,0.4f);
		}
		for (int i = 0; i < num_mines; i++) {
			mine_images [i].color = new Color (1.0f,1.0f,1.0f,1.0f);
		}
	}
}
