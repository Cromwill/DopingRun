using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    [SerializeField] private Button _openButton;

    private bool _isOpen;

    public event Action Opened;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
    }

    private void Open()
    {
        if (_isOpen)
            return;

        _isOpen = true;
        Opened?.Invoke();
    }
}
