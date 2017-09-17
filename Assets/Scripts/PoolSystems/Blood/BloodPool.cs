using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour {


    public enum BloodType {VIRUS,PARASYTE,BACTERIA }

    public GameObject redBlood;
    public GameObject blueBlood;
    public GameObject greenBlood;

    public int bloodAmount = 10;
    private List<GameObject> redBloodPool;
    private List<GameObject> bluedBloodPool;
    private List<GameObject> greenBloodPool;

    
    // Use this for initialization
	void Start ()
    {
        redBloodPool = new List<GameObject>();
        bluedBloodPool = new List<GameObject>();
        greenBloodPool = new List<GameObject>();

        GameObject bloodRef;

        for (int i = 0; i < bloodAmount; i++)
        {
            bloodRef = Instantiate(redBlood);
            bloodRef.SetActive(false);
            redBloodPool.Add(bloodRef);

            bloodRef = Instantiate(blueBlood);
            bloodRef.SetActive(false);
            bluedBloodPool.Add(bloodRef);

            bloodRef = Instantiate(greenBlood);
            bloodRef.SetActive(false);
            greenBloodPool.Add(bloodRef);

        }


	}

    private void PrepareBlood(GameObject blood, Vector3 positionReference ,Quaternion rotation)
    {
        blood.transform.position = positionReference;
        blood.transform.rotation = rotation;
        blood.GetComponent<BloodDestroy>().PrepareBlood();
        // call references
    }

    public GameObject GetBloodEffect(Vector3 positionReference,Quaternion rotation ,BloodType bloodType )
    {
        
        switch (bloodType)
        {
            case BloodType.VIRUS:

                for (int i = 0; i < redBloodPool.Count; i++)
                {
					if (redBloodPool [i] != null) {
						if (!redBloodPool [i].activeInHierarchy) {
							PrepareBlood (redBloodPool [i], positionReference, rotation);
							return redBloodPool [i];
						}
					}
                }

                GameObject newRedBlood = Instantiate(redBlood);
                redBloodPool.Add(newRedBlood);
                PrepareBlood(newRedBlood, positionReference,rotation);
                return newRedBlood;

             case BloodType.PARASYTE:

                for (int i = 0; i < bluedBloodPool.Count; i++)
                {
                    if (!bluedBloodPool[i].activeInHierarchy)
                    {
                        PrepareBlood(bluedBloodPool[i], positionReference, rotation);
                        return bluedBloodPool[i];
                    }
                }

                GameObject newBlueBlood = Instantiate(blueBlood);
                redBloodPool.Add(newBlueBlood);
                PrepareBlood(newBlueBlood, positionReference, rotation);
                return newBlueBlood;

            case BloodType.BACTERIA:

                for (int i = 0; i < greenBloodPool.Count; i++)
                {
                    if (!greenBloodPool[i].activeInHierarchy)
                    {
                        PrepareBlood(greenBloodPool[i], positionReference, rotation);
                        return greenBloodPool[i];
                    }
                }

                GameObject newGreenBlood = Instantiate(greenBlood);
                redBloodPool.Add(newGreenBlood);
                PrepareBlood(newGreenBlood, positionReference, rotation);
                return newGreenBlood;
        }

        return null;
    }
}
