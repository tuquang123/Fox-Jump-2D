using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    public GameObject boss;
    private void Star()
    {
        boss = GetComponent<GameObject>();
        boss.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);

        }
    }
    
}

