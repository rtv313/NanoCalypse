using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour {

    public GameObject virus;
    public GameObject parasite;
    public GameObject bacteria;

    public int virusAmount = 1;
    public int parasiteAmount = 1;
    public int bacteriaAmount = 1;

    public List<GameObject> virusList;
    public List<GameObject> parasiteList;
    public List<GameObject> bacteriaList;

    public GameObject dummiePath;

	// Use this for initialization
	void Start ()
    {
        virusList    = new List<GameObject>();
        parasiteList = new List<GameObject>();
        bacteriaList = new List<GameObject>();

        GameObject virusRef, parasiteRef, bacteriaRef;

        for (int i = 0; i < virusAmount; i++)
        {
            virusRef = Instantiate(virus);
            virusRef.SetActive(false);
            virusRef.GetComponent<Context>().path_objs_Patrol = dummiePath.GetComponentsInChildren<Transform>();
            virusRef.GetComponent<Context>().path_objs_Wander = dummiePath.GetComponentsInChildren<Transform>();
            virusList.Add(virusRef);
        }

        for (int i = 0; i < parasiteAmount; i++)
        {
            parasiteRef = Instantiate(parasite);
            parasiteRef.SetActive(false);
            parasiteRef.GetComponent<Context>().path_objs_Patrol = dummiePath.GetComponentsInChildren<Transform>();
            parasiteRef.GetComponent<Context>().path_objs_Wander = dummiePath.GetComponentsInChildren<Transform>();
            parasiteList.Add(parasiteRef);
        }

        for (int i = 0; i < bacteriaAmount; i++)
        {
            bacteriaRef = Instantiate(bacteria);
            bacteriaRef.SetActive(false);
            bacteriaRef.GetComponent<Context>().path_objs_Patrol = dummiePath.GetComponentsInChildren<Transform>();
            bacteriaRef.GetComponent<Context>().path_objs_Wander = dummiePath.GetComponentsInChildren<Transform>();
            bacteriaList.Add(bacteriaRef);
        }

    }

    private void PrepareEnemy(GameObject enemy, Transform positionReference)
    {
        enemy.transform.position = positionReference.position;
        enemy.transform.rotation = positionReference.rotation;
        Context enemyContext = enemy.GetComponent<Context>();

        if (enemyContext.mutaded && enemyContext.enemyType != Context.EnemyType.PARASITE)
        {
            enemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Transform ChildWithRender = enemyContext.gameObject.transform.GetChild(1).GetChild(0);
            Renderer rend = ChildWithRender.gameObject.GetComponent<Renderer>();
            rend.material.mainTexture = enemyContext.originalTexture;
        }

        if (enemy.GetComponent<Context>().enemyType == Context.EnemyType.BACTERIA)
        {
            enemy.GetComponent<Context>().nav.angularSpeed = 60;
            enemy.GetComponent<Context>().nav.speed = 5;
            enemy.GetComponent<TrailRenderer>().enabled = false;
        }

        enemy.GetComponent<Context>().state = new WanderState();
        enemy.GetComponent<Context>().life = 100;
        enemy.GetComponent<Context>().mutaded = false;
        enemy.GetComponent<Context>().animFlagAttack = false;
        enemy.SetActive(true);
    }

    public GameObject GetEnemy(Transform positionReference, Context.EnemyType type )
    {
        switch (type) {

            case Context.EnemyType.VIRUS:

                for (int i = 0; i < virusList.Count; i++)
                {
                    if (!virusList[i].activeInHierarchy)
                    {
                        PrepareEnemy(virusList[i], positionReference);
                        return virusList[i];
                    }
                }

                GameObject newVirus = Instantiate(virus);
                virusList.Add(newVirus);
                PrepareEnemy(newVirus, positionReference);
                return newVirus;

          

            case Context.EnemyType.PARASITE:

                for (int i = 0; i < parasiteList.Count; i++)
                {
                    if (!parasiteList[i].activeInHierarchy)
                    {
                        PrepareEnemy(parasiteList[i], positionReference);
                        return parasiteList[i];
                    }
                }

                GameObject newParasite = Instantiate(parasite);
                virusList.Add(newParasite);
                PrepareEnemy(newParasite, positionReference);
                return newParasite;
                

            case Context.EnemyType.BACTERIA:

                for (int i = 0; i < bacteriaList.Count; i++)
                {
                    if (!bacteriaList[i].activeInHierarchy)
                    {
                        PrepareEnemy(bacteriaList[i], positionReference);
                        return bacteriaList[i];
                    }
                }

                GameObject newBacteria= Instantiate(bacteria);
                bacteriaList.Add(newBacteria);
                PrepareEnemy(newBacteria, positionReference);
                return newBacteria;
        };
        return null;
    }

    public void RecicleEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        Context.EnemyType enemyType = enemy.GetComponent<Context>().enemyType;

        switch (enemyType)
        {
            case Context.EnemyType.VIRUS:

                enemy.GetComponent<VirusLaser>().DeactivateLaserLight();

                if (!virusList.Contains(enemy))
                {
                    virusList.Add(enemy);
                }
                break;

            case Context.EnemyType.PARASITE:

                if (!parasiteList.Contains(enemy))
                {
                    parasiteList.Add(enemy);
                }
                break;

            case Context.EnemyType.BACTERIA:

                if (!bacteriaList.Contains(enemy))
                {
                    bacteriaList.Add(enemy);
                }
                break;
        }
    }
	
}
