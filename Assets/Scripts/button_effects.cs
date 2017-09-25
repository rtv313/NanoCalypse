using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_effects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void buttonOver ()
	{
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (160f, 28f);

	}
	public void buttonOut(){
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (160f, 30f);
	}
}
