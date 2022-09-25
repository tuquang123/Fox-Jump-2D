using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerTele : MonoBehaviour
{
    // Dịch chuyển hiện tại 
    private GameObject currentTeleporter;
    public GameObject Buttontele;

    private void Awake()
    {
        
        Buttontele.SetActive(false);
    }
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Tele"))
        {
            if(currentTeleporter != null)
            {
                StartCoroutine(Tele());
            }
        }
    }
    private IEnumerator Tele()
    {
        yield return new WaitForSeconds(1f);
        transform.position = currentTeleporter.GetComponent<Tele>().GetDestion().position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tele"))
        {
            currentTeleporter = collision.gameObject;
            Buttontele.SetActive(true);
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
