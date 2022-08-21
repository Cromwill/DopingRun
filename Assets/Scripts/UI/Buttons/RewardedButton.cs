using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedButton : MonoBehaviour
{
    private Button _button;
    private bool _isRewarded;

    public event Action Rewarded;
    public event Action Clicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        SDKIntegration.Instance.Rewarded += OnRewarded;
        SDKIntegration.Instance.VideoClosed += OnVideoClosed;
        SDKIntegration.Instance.VideoOpened += OnVideoOpened;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        SDKIntegration.Instance.Rewarded -= OnRewarded;
        SDKIntegration.Instance.VideoClosed -= OnVideoClosed;
        SDKIntegration.Instance.VideoOpened -= OnVideoOpened;
    }

    private void OnClick()
    {
        Clicked?.Invoke();
        SDKIntegration.Instance.VideoAdShow();

#if VK_GAMES
        OnVideoOpened();
#endif
    }

    private void OnRewarded()
    {
        _isRewarded = true;

#if VK_GAMES
        OnVideoClosed();
#endif
    }

    private void OnVideoClosed()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;

        if (_isRewarded)
        {
            Rewarded?.Invoke();
        }
    }

    private void OnVideoOpened()
    {
        AudioListener.pause = true;
    }
}
