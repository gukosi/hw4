using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrete : MonoBehaviour
{
    [SerializeField] private TurretData _turretData;
    [SerializeField] private Transform[] _shellOut;
    [SerializeField] private Transform _pylon;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _fireAllGuns;

    private float _lastShoot;
    private int _shellOutIndex;

    public TurretData TurretData => _turretData;

    public Transform Target { get => _target; set => _target = value; }

    void Update()
    {
        FindColosestTarget();
    }

    private void FindColosestTarget()
    {
        if (EnemyManager.Instance == null) return;

        if (_target)
        {
            if (!_target.gameObject.activeSelf)
            {
                _target = null;
            }
            else
            {
                var dis = (_target.position - transform.position).magnitude;

                if (dis <= _turretData.FireRange)
                {
                    if (Time.time > _lastShoot + _turretData.FireRate)
                    {
                        Shoot(++_shellOutIndex % _shellOut.Length, _fireAllGuns);
                        _lastShoot = Time.time;
                    }
                }
            }
        }

        foreach (var enemy in EnemyManager.Instance.EnemyList)
        {
            if (!_target)
            {
                if (enemy.gameObject.activeSelf)
                {
                    var dis = (enemy.transform.position - transform.position).magnitude;

                    Debug.Log(dis);

                    if (dis < _turretData.FireRange)
                    {
                        _target = enemy.transform;
                    }
                }
            }
        }

        if (!_target) return;

        Vector3 dir = (_target.position - _pylon.position).normalized;
        dir.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        _pylon.rotation = Quaternion.Lerp(_pylon.rotation, lookRotation, Time.deltaTime * _turretData.RotationSpeed);
    }

    private void Shoot(int index, bool fireAllGuns = false)
    {
        GameObject bullet = null;

        if (!fireAllGuns)
        {
            bullet = Instantiate(_turretData.Bullet, _shellOut[index].transform.position, Quaternion.identity);
            var bul = bullet.GetComponent<Bullet>();
            bul.Init(_turretData.Damage);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(_shellOut[index].transform.right * _turretData.ShootForce, ForceMode.Impulse);
        }
        else
        {
            foreach (var shellOut in _shellOut)
            {
                bullet = Instantiate(_turretData.Bullet, shellOut.transform.position, Quaternion.identity);
                var bul = bullet.GetComponent<Bullet>();
                bul.Init(_turretData.Damage);
                var rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(shellOut.transform.right * _turretData.ShootForce, ForceMode.Impulse);
            }
        }

        Destroy(bullet, 0.5f);
    }
}