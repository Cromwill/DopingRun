using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelOpeningAnimation : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _panel;

    private float _duration = 0.5f;
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
        transform.localScale = new Vector3(0f, 0f, 1);

        transform.DOScale(1, _duration);
        transform.DOMove(_endPosition, _duration);
        StartCoroutine(Activate(_isOpen, 0));
    }

    private void Stop()
    {
        _isOpen = false;

        transform.localScale = Vector3.one;
        transform.position = _endPosition;

        transform.DOScale(0, _duration);
        transform.DOMove(_openButton.transform.position, _duration);
        StartCoroutine(Activate(_isOpen, _duration));
    }

    private IEnumerator Activate(bool value, float delay)
    {
        yield return new WaitForSeconds(delay);

        _panel.SetActive(value);
    }
}
