using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public GameObject Fx;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyTakeDame enemy = collision.GetComponent<EnemyTakeDame>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

       /* BossHealth boss = collision.GetComponent<BossHealth>();
        if (enemy != null)
        {
            boss.TakeDamage(damage);
        }
*/
        Instantiate(Fx, transform.position, transform.rotation);
;       Destroy(gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        BossHealth boss = collision.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        Instantiate(Fx, transform.position, transform.rotation);
        ; Destroy(gameObject);
    }
}
