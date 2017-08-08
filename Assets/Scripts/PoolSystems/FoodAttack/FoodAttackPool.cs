using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAttackPool : MonoBehaviour {
    
    public GameObject foodOne;
    public GameObject foodTwo;
    public GameObject foodThree;
    public GameObject target;
    public GameObject foodExplosion;

    private List<GameObject> foodOneList,foodTwoList,foodThreeList, targetList,foodExplosionList;

	// Use this for initialization
	void Start () {

        foodOneList = new List<GameObject>();
        foodTwoList = new List<GameObject>();
        foodThreeList = new List<GameObject>();
        targetList = new List<GameObject>();
        foodExplosionList = new List<GameObject>();

        GameObject foodRef,targetRef,foodExplosionRef;

        for (int i = 0; i < 5; i++)
        {
            foodRef = Instantiate(foodOne);
            foodRef.SetActive(false);
            foodOneList.Add(foodRef);

            foodRef = Instantiate(foodTwo);
            foodRef.SetActive(false);
            foodTwoList.Add(foodRef);

            foodRef = Instantiate(foodThree);
            foodRef.SetActive(false);
            foodThreeList.Add(foodRef);

            targetRef = Instantiate(target);
            targetRef.SetActive(false);
            targetList.Add(targetRef);

            foodExplosionRef = Instantiate(foodExplosion);
            foodExplosionRef.SetActive(false);
            foodExplosionList.Add(foodExplosionRef);
        }

    }

    private void PrepareFood(GameObject food, Transform positionReference)
    {
        food.transform.position = positionReference.position;
        food.GetComponent<FoodDrop>().ResetFlag();
        food.SetActive(true);
    }

    public GameObject GetFood(Transform positionReference)
    {

        int randomNumber = Random.Range(0, 3);
        GameObject newFood;

        switch (randomNumber)
        {
            case 0:

                for (int i = 0; i < foodOneList.Count; i++)
                {
                    if (!foodOneList[i].activeInHierarchy)
                    {
                        PrepareFood(foodOneList[i], positionReference);
                        return foodOneList[i];
                    }
                }

                newFood = Instantiate(foodOne);
                foodOneList.Add(newFood);
                PrepareFood(newFood, positionReference);
                return newFood;

            case 1:

                for (int i = 0; i < foodTwoList.Count; i++)
                {
                    if (!foodTwoList[i].activeInHierarchy)
                    {
                        PrepareFood(foodTwoList[i], positionReference);
                        return foodTwoList[i];
                    }
                }

                newFood = Instantiate(foodTwo);
                foodTwoList.Add(newFood);
                PrepareFood(newFood, positionReference);
                return newFood;
                

            case 2:

                for (int i = 0; i < foodThreeList.Count; i++)
                {
                    if (!foodThreeList[i].activeInHierarchy)
                    {
                        PrepareFood(foodThreeList[i], positionReference);
                        return foodThreeList[i];
                    }
                }

                newFood = Instantiate(foodThree);
                foodThreeList.Add(newFood);
                PrepareFood(newFood, positionReference);
                return newFood;
        }

        return null;
    }

    private void PrepareTarget(GameObject target, Vector3 positionReference)
    {
        target.transform.position = positionReference;
        target.SetActive(true);
    }

    public GameObject GetTarget(Vector3 positionReference)
    {
       
        for (int i = 0; i < targetList.Count; i++)
        {
            if (!targetList[i].activeInHierarchy)
            {
                PrepareTarget(targetList[i], positionReference);
                return targetList[i];
            }
        }
        GameObject newTarget;
        newTarget = Instantiate(target);
        targetList.Add(newTarget);
        PrepareTarget(newTarget, positionReference);
        return newTarget;
    }


    private void PrepareFoodExplosion(GameObject foodExp, Transform positionReference)
    {
        foodExp.transform.position = positionReference.position;
        foodExp.GetComponent<FoodExplosion>().ResetExplosionLifeTime();
        foodExp.SetActive(true);
    }

    public GameObject GetFoodExplosion(Transform positionReference)
    {
        for (int i = 0; i < foodExplosionList.Count; i++)
        {
            if (!foodExplosionList[i].activeInHierarchy)
            {
                PrepareFoodExplosion(foodExplosionList[i], positionReference);
                return foodExplosionList[i];
            }
        }
        GameObject newFoodExp;
        newFoodExp = Instantiate(foodExplosion);
        foodExplosionList.Add(newFoodExp);
        PrepareFoodExplosion(newFoodExp, positionReference);
        return newFoodExp;
    }
}
