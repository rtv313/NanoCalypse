using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manage_level_load : MonoBehaviour {
	private AsyncOperation level_load_operation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startLoad(){
		level_load_operation = SceneManager.LoadSceneAsync ("Level1", LoadSceneMode.Single);
		level_load_operation.allowSceneActivation = false;
	}
	public void setActivation(bool status){
		level_load_operation.allowSceneActivation = true;
	}
	public float checkProgress(){
		return level_load_operation.progress;
	}
}
