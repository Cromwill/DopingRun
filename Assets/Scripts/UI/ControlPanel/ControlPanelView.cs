using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelView : MonoBehaviour
{
    [SerializeField] private StartLevelButton _startLevelButton;

    private List<Transform> _childs = new List<Transform>();

    private void Awake()
    {
        foreach (var child in GetComponentsInChildren<Transform>())
            if (child != transform)
                _childs.Add(child);
    }

    private void OnEnable()
    {
        _startLevelButton.RunStarted += OnRunStart;
    }

    private void OnDisable()
    {
        _startLevelButton.RunStarted -= OnRunStart;
    }

    private void OnRunStart()
    {
        foreach (var child in _childs)
            child.gameObject.SetActive(false);
    }
}
