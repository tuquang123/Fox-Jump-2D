using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLv : MonoBehaviour
{
    public Animator transition;
    public float trasiton = 1f;

    //public string SceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Loadnext();
        }
    }
    public void Loadnext()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1 ));
        // SceneManager.LoadScene(SceneName);
    }
    IEnumerator LoadLevel(int lv)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(trasiton);
        SceneManager.LoadScene(lv);
    }
}
