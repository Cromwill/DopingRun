using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    [SerializeField] private StartLevelButton _startLevelButton;

    private void OnEnable()
    {
        if (_startLevelButton != null)
            _startLevelButton.RunStarted += OnRunStart;
    }

    private void OnDisable()
    {
        if (_startLevelButton != null)
            _startLevelButton.RunStarted -= OnRunStart;
    }

    private void OnRunStart()
    {
        gameObject.SetActive(false);
    }
}
