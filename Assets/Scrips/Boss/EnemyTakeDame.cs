using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDame: MonoBehaviour
{
   
    public GameObject popupText;
    public HPBar hpBar;
	public Animator anim;
	public int maxHealth = 100;
	int currentHealth;

	public GameObject deathEffect;

    private void Start()
    {
        
		currentHealth = maxHealth;
        hpBar.SetMaxHealth(maxHealth);

    }
    public void TakeDamage(int damage)
	{
       
        ShowDamage(damage.ToString());
        anim.SetTrigger("hurt");
        currentHealth -= damage;
		StartCoroutine(DamageAnimation());
        hpBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
		{
			Die();
		}
	}
    void ShowDamage(string text)
    {
        if (popupText)
        {
            GameObject prefab = Instantiate(popupText, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
    public void AddScore()
    {
        ManagerUI.perm.score += 50;
        ManagerUI.perm.scoreText.text = ManagerUI.perm.score.ToString();
    }

    public void Die()
	{
        //AddScore();
        //Enemy enemy = GetComponent<Enemy>();
        //enemy.death.Play();
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }

}