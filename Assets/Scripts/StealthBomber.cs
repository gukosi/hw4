using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthBomber : Enemy
{
    public float bombDamage = 50.0f;
    public float bombRadius = 5.0f;
    public float flySpeed = 10.0f;
    private bool hasBombed = false;

    void Update()
    {
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);

        if (transform.position.z > 0 && !hasBombed)
        {
            Bomb();
            hasBombed = true;
        }
    }

    void Bomb()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, bombRadius);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(bombDamage);
            }
        }
    }
}

