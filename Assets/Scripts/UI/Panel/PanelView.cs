using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Transform _panel;

    public event Action Opened;
    public event Action Closed;

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }

    private void Open()
    {
        if (_panel.gameObject.activeInHierarchy)
            return;

        _panel.gameObject.SetActive(true);
        Opened?.Invoke();
    }

    private void Close()
    {
        _panel.gameObject.SetActive(false);
        Closed?.Invoke();
    }
}
