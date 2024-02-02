using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] private Transform _moveTarget;
    [SerializeField] private Image _healthBar; 
    [SerializeField] private float _health;

    private float _maxHealth;

    private void OnEnable()
    {
        _maxHealth = _health;
    }

    public void SetDestination(Vector3 targetPosition)
    {
        _agent.SetDestination(targetPosition);
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;

        _healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ReduceSpeed(float factor, float duration) //scoutcar
    {
        StartCoroutine(ReduceSpeedCoroutine(factor, duration));
    }

    private IEnumerator ReduceSpeedCoroutine(float factor, float duration)
    {
        float originalSpeed = _agent.speed;
        _agent.speed *= factor;
        yield return new WaitForSeconds(duration);
        _agent.speed = originalSpeed;
    }
}

public interface IEnemy
{
    void TakeDamage(float dmg);
}