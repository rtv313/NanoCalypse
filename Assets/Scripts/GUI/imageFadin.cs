using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageFadin : MonoBehaviour {
	private Color auxColor, mycolor;
	private float scaleSmall, scaleBig, scaleX, scaleY, scaleZ;
	private RectTransform rectAux;
	// Use this for initialization
	void Start () {
		mycolor = GetComponent<Image> ().color;
		scaleSmall = 0.9f;
		scaleBig = 1.5f;
	}
	void Awake(){

		rectAux = this.GetComponent<RectTransform> ();
	}
	void Update(){
		gameObject.GetComponent<Image> ().color = mycolor;
		rectAux = this.GetComponent<RectTransform> ();
		if( rectAux.GetSiblingIndex() == 2 && this.GetComponent<RectTransform> ().localScale.x <= scaleBig){
			scaleX = Mathf.Lerp (this.GetComponent<RectTransform> ().localScale.x, scaleBig, Time.deltaTime * 3);
			scaleY = Mathf.Lerp (this.GetComponent<RectTransform> ().localScale.y, scaleBig, Time.deltaTime * 3);
			scaleZ = Mathf.Lerp (this.GetComponent<RectTransform> ().localScale.z, scaleBig, Time.deltaTime * 3);
			rectAux.localScale = new Vector3(scaleX, scaleY, scaleZ);
		}else if(rectAux.GetSiblingIndex() != 2 && this.GetComponent<RectTransform> ().localScale.x >= scaleSmall){
			rectAux.localScale = new Vector3(scaleSmall, scaleSmall, scaleSmall);
		}
	}
	public void changeAlpha(float alpha){
		auxColor = mycolor;
		auxColor.a = alpha;
		mycolor = auxColor;
	
	}
	public void SetPositionZ(int pos){
		this.GetComponent<RectTransform> ().SetSiblingIndex (pos);
		if (pos == 2) {
			rectAux.localScale = new Vector3(scaleSmall/2, scaleSmall/2, scaleSmall/2);
		}

	}
}
