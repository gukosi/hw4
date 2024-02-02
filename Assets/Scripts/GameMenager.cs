using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameMenager : MonoBehaviour
{
    public event Action OnGameStart;

    [SerializeField] private CameraController _controller;
    [SerializeField] private Turrete[] _turretes;
    [SerializeField] private RectTransform _turretsBtnRoot;
    [SerializeField] private TurretButton _turretButton;
    [SerializeField] private Transform _target;

    private void Start()
    {
        InitTurretUI();
        StartGame();
    }

    private void StartGame()
    {
        OnGameStart?.Invoke();
    }

    private void InitTurretUI()
    {
        foreach (var turret in _turretes)
        {
            var btn = Instantiate(_turretButton, _turretsBtnRoot);
            btn.Init(turret, _target, _controller);
        }
    }
}