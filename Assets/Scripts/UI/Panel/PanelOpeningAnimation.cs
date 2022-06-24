using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelOpeningAnimation : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private Vector3 _endPosition;
    private bool _isOpen;

    private void Awake()
    {
        _endPosition = transform.position;
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Play);
        _closeButton.onClick.AddListener(Stop);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Play);
        _closeButton.onClick.RemoveListener(Stop);
    }

    private void Play()
    {
        if (_isOpen)
            return;
        
        _isOpen = true;

        transform.position = _openButton.transform.position;
        transform.localScale = new Vector3(0.1f, 0.1f, 1);

        transform.DOMove(_endPosition, 0.5f);
        transform.DOScale(1, 0.5f);
    }

    private void Stop()
    {
        _isOpen = false;

        transform.localScale = Vector3.one;
        transform.position = _endPosition;

        transform.DOScale(0, 0.5f);
    }
}
