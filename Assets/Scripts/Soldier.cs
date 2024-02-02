using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    public float shootingRange = 10.0f;
    public float shootingDamage = 10.0f;
    public float shootingCooldown = 2.0f;
    private float lastShootTime = 0.0f;

    void Update()
    {
        if (Time.time > lastShootTime + shootingCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, shootingRange))
        {
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(shootingDamage);
            }
        }
    }
}

