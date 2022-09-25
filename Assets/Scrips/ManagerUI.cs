using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerUI: MonoBehaviour
{
    //Player State ;
    public int Mushroom = 0;
    public int score = 0;
    [Range(0, 10)] public int health = 5;
    public int numOfHeart;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public TextMeshProUGUI MushroomText;
    public Text healthAmount;
    public TextMeshProUGUI scoreText;
    public static ManagerUI perm;

    private void Update()
    {
        if(health > numOfHeart)
        {
            health = numOfHeart;
        }
       for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        } 
    }
    private void Start()
    {
        //do not destroy
        DontDestroyOnLoad(gameObject);

        MushroomText.text = Mushroom.ToString();
        healthAmount.text = health.ToString();
        scoreText.text = score.ToString();

        //Singleton
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //reset hp vs cherry
    public void Reset()
    {
        Mushroom = 0;
        MushroomText.text = Mushroom.ToString();
        health = 3;
        healthAmount.text = health.ToString();
        score = 0;
        scoreText.text = score.ToString();
    }

}
