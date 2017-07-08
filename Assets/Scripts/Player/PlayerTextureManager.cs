using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextureManager : MonoBehaviour {
    public GameObject mesh;
    public Texture2D rifleTexture;
    public Texture2D shootgunTexture;
    public Texture2D sniperTexture;
    public int fireMode = 1;

    public void SetAssaulRifleTexture()
    {
        fireMode = 1;
        mesh.GetComponent<Renderer>().material.mainTexture = rifleTexture;
    }

    public void SetShootgunTexture()
    {
        fireMode = 2;
        mesh.GetComponent<Renderer>().material.mainTexture = shootgunTexture;
    }

    public void SetSniperTexture()
    {
        fireMode = 3;
        mesh.GetComponent<Renderer>().material.mainTexture = sniperTexture;
    }
   
}
