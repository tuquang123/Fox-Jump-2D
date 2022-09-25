using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry: MonoBehaviour
{
    protected Animator anim;
    protected AudioSource death;

    // Start is called before the first frame update
    void Start()
    {     
        anim = GetComponent<Animator>();
        death = GetComponent<AudioSource>();
    }

    public void Touch()
    {
        anim.SetTrigger("Destroy");
        death.Play();
        GetComponent<Collider2D>().enabled = false;
        //touch cherry + 1
        ManagerUI.perm.Mushroom += 1;
        ManagerUI.perm.MushroomText.text = ManagerUI.perm.Mushroom.ToString();
        // touch score + 10
        ManagerUI.perm.score += 10;
        ManagerUI.perm.scoreText.text = ManagerUI.perm.score.ToString();

        if (ManagerUI.perm.Mushroom == 30 )
        {
            ManagerUI.perm.health += 1;
            ManagerUI.perm.healthAmount.text = ManagerUI.perm.health.ToString();
        }
        if (ManagerUI.perm.Mushroom == 90)
        {
            ManagerUI.perm.health += 1;
            ManagerUI.perm.healthAmount.text = ManagerUI.perm.health.ToString();
        }
        if (ManagerUI.perm.Mushroom == 180)
        {
            ManagerUI.perm.health += 1;
            ManagerUI.perm.healthAmount.text = ManagerUI.perm.health.ToString();
        }
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
