using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WinerDecider))]
public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private CameraTransition _cameraTransition;

    private WinerDecider _winerDecider;
    private SumoControlls _sumoControlls;

    private Player _player;
    private JoystickCanvas _joystickCanvas;

    private void Start()
    {
        _sumoControlls = new SumoControlls();
    }

    private void OnEnable()
    {
        _winerDecider = GetComponent<WinerDecider>();
        _winerDecider.Victory += OnVictory;
    }

    private void OnDisable()
    {
        _winerDecider.Victory -= OnVictory;
    }

    private void OnVictory()
    {
        Init();
        _sumoControlls.Disable(_player, _joystickCanvas);
        _cameraTransition.Transit();
    }

    private void Init()
    {
        _player = FindObjectOfType<Player>();

        Error.CheckOnNull(_player, nameof(Player));

        _joystickCanvas = FindObjectOfType<JoystickCanvas>();

        Error.CheckOnNull(_joystickCanvas, nameof(JoystickCanvas));
    }
}
