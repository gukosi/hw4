using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _turretName;
    [SerializeField] private Button _button;

    private CameraController _controller;
    private Turrete _turret;

    public void Init(Turrete turret, Transform _target, CameraController controller)
    {
        _turret = turret;
        _turret.Target = _target;
        _controller = controller;
        _turretName.text = turret.TurretData.TurretName;
        _button.onClick.AddListener(PlaceTurret);
    }

    private void PlaceTurret()
    {
        if (_turret != null)
        {
            _controller.Pointer = Instantiate(_turret, _turret.TurretData.PlacePosition, Quaternion.identity).gameObject;
            _controller.Pointer.transform.position = new Vector3(0, 50, 0);
        }
    }
}