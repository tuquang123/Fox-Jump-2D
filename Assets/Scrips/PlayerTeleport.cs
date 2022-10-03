using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerTeleport : MonoBehaviour
{
    //vi tri dich chuyen
    private GameObject currentTeleporter;
    public GameObject Buttontele;

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Tele") || Input.GetKeyDown(KeyCode.Space))
        {
            if(currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleport>().GetDestion().position;
            }
        }
        
    }
    private void Awake()
    {
        Buttontele = GetComponent<GameObject>();
        Buttontele.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tele"))
        {
            Buttontele.SetActive(true);
            currentTeleporter = collision.gameObject;
        }  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tele"))
        {
            Buttontele.SetActive(false);
            currentTeleporter = null;
        }
    }
}
