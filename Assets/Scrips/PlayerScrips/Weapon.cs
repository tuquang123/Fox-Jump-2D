using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shurikenPrefab;
    private float TimeBtwShots;
    public float StartTimeBtwShots;

    void Update()
    {
        if (TimeBtwShots <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                TimeBtwShots = StartTimeBtwShots;
                Player player = GetComponent<Player>();
                player.anim.SetTrigger("shooting");
            }
        }
        else
        {
            TimeBtwShots -= Time.deltaTime;
        }

    }
    void Shoot()
    {
        Instantiate(shurikenPrefab, firePoint.position, firePoint.rotation);
    }
}
