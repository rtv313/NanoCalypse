using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_travelling : MonoBehaviour {


	private float transitionDuration;
	public Transform screenPosition, tablePosition;
	bool moving;

	void Start(){
		moving = false;
		transitionDuration = 1.0f;
	}
	/*void Update(){
		Debug.Log (moving);
	}*/
	IEnumerator Transition(Transform destinationPoint)
	{
		moving = true;
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

		moving = false;

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
}
