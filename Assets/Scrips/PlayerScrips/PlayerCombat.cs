using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask enemyLayers;
    public LayerMask bossLayer;

    public int damage = 1;
    public float attackRage = 0.5f;
    public float attackRate = 1f;
    float nextAttackTime = 0f;

    

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                AttAnim();
                Attack();
                
                nextAttackTime = Time.time + 1f / attackRate;

            }
        }

    }
    void AttAnim()
    {

        //play attack animation
        animator.SetTrigger("attacking");
    }
    void Attack()
    {
        /*//play attack animation
        animator.SetTrigger("attacking");*/
        //Nhan dien enemy va attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRage, enemyLayers);

        //dame them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyTakeDame>().TakeDamage(damage);
        }

        // attacking boss
        Collider2D[] hitboss = Physics2D.OverlapCircleAll(AttackPoint.position, attackRage, bossLayer);

        //dame them
        foreach (Collider2D boss in hitboss)
        {
            boss.GetComponent<BossHealth>().TakeDamage(damage);
        }
    }
    
    // draw radius att
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRage);
    }
    
}
