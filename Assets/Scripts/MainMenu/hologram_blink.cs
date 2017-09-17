using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hologram_blink : MonoBehaviour {
	public bool active_state, can_blink;
	private int blink_stages;
	// Use this for initialization
	void Start () {
		active_state = true;
		can_blink = true;
		blink_stages = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (blink_stages < 10) {
			switch (blink_stages) {
			case 0:
				if (can_blink) {
					can_blink = false;
					StartCoroutine (Blink (0.5f, 1.0f));
					blink_stages++;
				}
				break;
			case 1:
				if (can_blink) {
					can_blink = false;
					StartCoroutine (Blink (0.25f, 1.25f));
					blink_stages++;
				}
				break;
			case 2:
				if (can_blink) {
					can_blink = false;
					StartCoroutine (Blink (0.05f, 0.75f));
					blink_stages++;
				}
				break;
			default:
				if (gameObject.GetComponent<MeshRenderer> () == null) {
					gameObject.GetComponent<Light> ().enabled = true;
				} else {
					gameObject.GetComponent<MeshRenderer> ().enabled = true;
				}
				active_state = true;
				blink_stages = 10;
				break;
			}
		} else {
			if (gameObject.GetComponent<MeshRenderer> () == null) {
				gameObject.GetComponent<Light> ().enabled = true;
			} else {
				gameObject.GetComponent<MeshRenderer> ().enabled = true;
			}
		}
			


	}
	IEnumerator Blink( float time_between_blinks, float time_blinking){
		

		for(int i = 0; i<= time_blinking/time_between_blinks;i++){
			if(gameObject.GetComponent<MeshRenderer> () == null){
				gameObject.GetComponent<Light> ().enabled = !active_state;
			}else{
				gameObject.GetComponent<MeshRenderer> ().enabled = !active_state;
			}
			active_state = !active_state;
			yield return new WaitForSeconds(time_between_blinks);
		}

		/*if(gameObject.GetComponent<MeshRenderer> () == null){
			gameObject.GetComponent<Light> ().enabled = true;
		}else{
			gameObject.GetComponent<MeshRenderer> ().enabled = true;
		}
		active_state = true;*/
		can_blink = true;
	}
}
