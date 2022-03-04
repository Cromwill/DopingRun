using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WinnerDecider), typeof(VictoryCameraTranstion))]
public class VictoryTrigger : MonoBehaviour
{
    private CameraTransition _cameraTransition;
    private WinnerDecider _winerDecider;
    private SumoControls _sumoControlls;

    private Player _player;
    private JoystickCanvas _joystickCanvas;

    private void Start()
    {
        _cameraTransition = GetComponent<VictoryCameraTranstion>();
        _sumoControlls = new SumoControls();
    }

    private void OnEnable()
    {
        _winerDecider = GetComponent<WinnerDecider>();
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
