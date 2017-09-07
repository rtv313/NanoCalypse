using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextureManager : MonoBehaviour {

    public float brightness = 2.0f;

    public GameObject meshBody;

    public Texture2D rifleTexture;
    public Texture2D shootgunTexture;
    public Texture2D sniperTexture;

    public Texture2D rifleEmission;
    public Texture2D shootgunEmission;
    public Texture2D sniperEmission;

    public Color red;
    public Color blue;
    public Color green;
    public int fireMode = 1;

    public void SetAssaulRifleTexture()
    {
        fireMode = 1;

        Material mat = meshBody.GetComponent<Renderer>().material;
        mat.mainTexture = rifleTexture;
        mat.SetTexture("_EmissionMap", rifleEmission);
        mat.SetColor("_EmissionColor", red * brightness);
    }

    public void SetShootgunTexture()
    {
        fireMode = 2;
        Material mat = meshBody.GetComponent<Renderer>().material;
        mat.mainTexture = shootgunTexture;
        mat.SetTexture("_EmissionMap", shootgunEmission);
        mat.SetColor("_EmissionColor", blue * brightness);
    }

    public void SetSniperTexture()
    {
        fireMode = 3;
        Material mat = meshBody.GetComponent<Renderer>().material;
        mat.mainTexture = sniperTexture;
        mat.SetTexture("_EmissionMap", sniperEmission);
        mat.SetColor("_EmissionColor", Color.green * brightness);
 }
   
}
