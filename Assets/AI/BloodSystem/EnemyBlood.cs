using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlood : MonoBehaviour
{
    public GameObject blood;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(blood, other.transform.position, other.transform.rotation);
        }
    }

  }
