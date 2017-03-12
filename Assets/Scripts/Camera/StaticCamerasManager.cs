using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCamerasManager : MonoBehaviour {

    public int index;
    public List<Camera> cameras = new List<Camera>();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SelectCamera();
    }

    void activateCamera()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].enabled = false;

            if (i == index)
            {
                cameras[i].enabled = true;
            }
            
        }
    }

    void SelectCamera()
    {
        if (Input.GetKeyDown("p"))
        {
            index++;
            if (index >= cameras.Count)
                index = 0;
            activateCamera();
        }

        if (Input.GetKeyDown("o"))
        {
            index--;
            if (index < 0)
                index = 0;
            activateCamera();
        }
           
     }
}
