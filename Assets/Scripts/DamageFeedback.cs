using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback : MonoBehaviour {

    public GameObject mesh;

    private float blinkCD = 0.1f;
    private float blinkTimer = 0.0f;
    private bool blinking;

    private Color defaultColor;
    private Color blinkColor;
    private Color blinkColorMutate;

    // Use this for initialization
    void Start () {
        defaultColor = mesh.GetComponent<Renderer>().material.GetColor("_Color");
        blinkColor = new Color(2.0f, 0.0f, 0.0f, 1.0f);
        blinkColorMutate = new Color(2.0f, 2.0f, 0.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (blinking)
        {
            if (blinkTimer < 0.0f)
            {
                mesh.GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
                blinking = false;
            }
            else blinkTimer -= Time.deltaTime;
        } 
	}

    public void receiveDamage ()
    {
        blinkTimer = blinkCD;
        mesh.GetComponent<Renderer>().material.SetColor("_Color", blinkColor);
        mesh.GetComponent<Renderer>().material.SetColor("_DiffuseColor", blinkColor);
        blinking = true;
    }

    public void receiveDamageMutate()
    {
        blinkTimer = blinkCD;
        mesh.GetComponent<Renderer>().material.SetColor("_Color", blinkColorMutate);
        mesh.GetComponent<Renderer>().material.SetColor("_DiffuseColor", blinkColorMutate);
        blinking = true;
    }
}
