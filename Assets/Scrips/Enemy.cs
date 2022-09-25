using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public GameObject Gold;
    protected Animator anim;
    protected Rigidbody2D rb;
    public AudioSource death;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        death = GetComponent<AudioSource>();

    }
    public void JumpOn()
    {
        //+ 50 score kill enemy
        ManagerUI.perm.score+=30;
        ManagerUI.perm.scoreText.text = ManagerUI.perm.score.ToString();
        //Instantiate(Gold, transform.position,Quaternion.identity);

        death.Play();
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
    private void Death()
    {
        Destroy(this.gameObject);    
    }
}
