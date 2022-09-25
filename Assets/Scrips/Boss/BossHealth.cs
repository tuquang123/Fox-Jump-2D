using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	public GameObject TextPopUp;
	public HPBar hPBar;
	public int maxHealth = 500;
	int currentHealth;
	private Rigidbody2D rb;

	public GameObject deathEffect;
	//public Animator anm;

	public bool isInvulnerable = false;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		currentHealth = maxHealth;
		hPBar.SetMaxHealth(maxHealth);
	}
    
    public void TakeDamage(int damage)
	{
		//anm.SetTrigger("hurt");
		ShowDamage(damage.ToString());
		if (isInvulnerable)
			return;

		currentHealth -= damage;
		//StartCoroutine(DamageAnimation());
		hPBar.SetHealth(currentHealth);

		if (currentHealth <= 200)
		{
			GetComponent<Animator>().SetBool("isenraged", true);
		}

		if (currentHealth <= 0)
		{
			Die();
		}
	}
	void ShowDamage(string text)
    {
        if (TextPopUp)
        {
			GameObject prefab = Instantiate(TextPopUp, transform.position, Quaternion.identity);
			prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
	void Die()
	{
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