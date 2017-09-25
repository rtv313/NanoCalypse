using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_travelling : MonoBehaviour {


	private float transitionDuration;
	public Transform screenPosition, tablePosition;
	bool moving;
	float time_static, time_for_glitch;
	private Kino.AnalogGlitch glitch;
	private Vector3 current_rotation;

	void Start(){
		moving = false;
		transitionDuration = 1.0f;
		time_static = Time.time;
		glitch = gameObject.GetComponent<Kino.AnalogGlitch> ();
		glitch.enabled = false;
		time_for_glitch = Random.Range (1, 7);
		current_rotation = transform.eulerAngles;
	}
	void Update(){
		if(Time.time - time_static > time_for_glitch){
			StartCoroutine (StartGlitch ());
			time_for_glitch = Random.Range (1, 7);
		}
		if (!moving) {
			float angle = Mathf.LerpAngle (current_rotation.y - 5, current_rotation.y + 5, Mathf.PingPong (Time.time / 20, 1.0f));
			transform.eulerAngles = new Vector3(current_rotation.x,angle,current_rotation.z);
		}
	}
	IEnumerator Transition(Transform destinationPoint)
	{
		moving = true;
		time_static = Time.time;
		float t = 0.0f;
		Vector3 startingPos = transform.position;
		Quaternion startingRot = transform.rotation;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale/transitionDuration);


			transform.position = Vector3.Lerp(startingPos, destinationPoint.position, t);
			transform.rotation = Quaternion.Lerp (startingRot, destinationPoint.rotation, t);
			yield return 0;
		}
		current_rotation = transform.eulerAngles;
		moving = false;

	}
	IEnumerator StartGlitch()
	{
		glitch.enabled = true;
		yield return new WaitForSeconds (Random.Range (0.25f, 1.5f));
		glitch.enabled = false;
		time_static = Time.time;
	}

	public void moveCameraToScreen () {
		if (!moving) {
			StartCoroutine (Transition(screenPosition));
		}
	}
	public void moveCameraToTable () {
		if (!moving) {
			StartCoroutine (Transition(tablePosition));
		}
	}
	public float getTransitionDuration(){
		return transitionDuration;
	}
}
