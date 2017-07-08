using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextureManager : MonoBehaviour {
    public GameObject mesh;
    public Texture2D rifleTexture;
    public Texture2D shootgunTexture;
    public Texture2D sniperTexture;
    public Color red;
    public Color blue;
    public Color green;
    public int fireMode = 1;

    public void SetAssaulRifleTexture()
    {
        fireMode = 1;
        Material mat = mesh.GetComponent<Renderer>().material;
        mat.mainTexture = rifleTexture;
        mat.SetColor("_EmissionColor", red);
    }

    public void SetShootgunTexture()
    {
        fireMode = 2;
        Material mat = mesh.GetComponent<Renderer>().material;
        mat.mainTexture = shootgunTexture;
        mat.SetColor("_EmissionColor", blue);
    }

    public void SetSniperTexture()
    {
        fireMode = 3;
        Material mat = mesh.GetComponent<Renderer>().material;
        mat.mainTexture = sniperTexture;
        mat.SetColor("_EmissionColor", green);
    }
   
}
