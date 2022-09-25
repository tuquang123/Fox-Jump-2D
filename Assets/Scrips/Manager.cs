using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject gameStart;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    
    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    public void Started()
    {
        this.gameStart.SetActive(false);
        Time.timeScale = 1;
    }
}
