using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutCar : Enemy
{
    public float markDuration = 5.0f;
    public float speedIncreaseFactor = 1.5f;
    private bool hasMarked = false;

    void Start()
    {
        _agent.speed *= speedIncreaseFactor;
    }

    void Update()
    {
        if (!hasMarked)
        {
            MarkTarget();
            hasMarked = true;
        }
    }

    void MarkTarget()
    {
        // Example marking logic
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _agent.stoppingDistance))
        {
            var enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply a marked status effect to the enemy
                // This could involve a debuff or making the enemy take more damage
                // For demonstration, let's just reduce the enemy's speed
                enemy.ReduceSpeed(0.5f, markDuration);
            }
        }
    }
}

