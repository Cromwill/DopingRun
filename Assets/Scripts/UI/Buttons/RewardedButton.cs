using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedButton : MonoBehaviour
{
    private Button _button;
    private bool _isRewarded;

    public event Action Rewarded;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        YandexSDKIntegration.Instance.Rewarded += OnRewarded;
        YandexSDKIntegration.Instance.VideoClosed += OnVideoClosed;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        YandexSDKIntegration.Instance.Rewarded -= OnRewarded;
        YandexSDKIntegration.Instance.VideoClosed -= OnVideoClosed;
    }

    private void OnClick()
    {
        YandexSDKIntegration.Instance.VideoAdShow();
    }

    private void OnRewarded()
    {
        _isRewarded = true;
    }

    private void OnVideoClosed()
    {
        if (_isRewarded)
        {
            Time.timeScale = 1;
            Rewarded?.Invoke();
        }
    }
}
