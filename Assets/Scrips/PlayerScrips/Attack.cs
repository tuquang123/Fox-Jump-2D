using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Attack : MonoBehaviour
{
    public Animator anim;
    public bool Attacking = false;
    public static Attack instance;

    public Transform AttackPoint;
    public LayerMask enemyLayers;
    public LayerMask bossLayer;

    public int damage = 1;
    public float attackRage = 0.5f;
    public float attackRate = 1f;
    float nextAttackTime = 0f;

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRage);
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Attacks();
    }
    void Attacks()
    {
        if (Time.time >= nextAttackTime)
        {

            if (Input.GetButtonDown("Fire2") && !Attacking)
            {
                Attacking = true;
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRage, enemyLayers);

                AttackingA();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void AttackingA()
    {
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

}
