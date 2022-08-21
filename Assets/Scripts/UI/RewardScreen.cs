using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardScreen : MonoBehaviour
{
    [SerializeField] private RewardedButton _button;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private float _pauseTime;

    private Coroutine _coroutine;

    public event Action OfferRejected;
    public event Action Rewarded;

    private void OnEnable()
    {
        _button.Rewarded += OnRewarded;
        _button.Clicked += OnClick;
    }

    private void OnDisable()
    {
        _button.Rewarded -= OnRewarded;
        _button.Clicked -= OnClick;
    }

    public void Show()
    {
        _button.gameObject.SetActive(true);
        _coroutine = StartCoroutine(Unpause(_pauseTime));

#if VK_GAMES
        Analytics.Instance.RewardedShown("DangerZone", "VK_games");
#endif
    }

    private void OnRewarded()
    {
        Hide();
        Rewarded?.Invoke();

#if VK_GAMES
        Analytics.Instance.RewardedStart("DangerZone", "VK_games");
#endif
    }

    private void Hide()
    {
        _button.gameObject.SetActive(false);
    }

    private void OnClick()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Unpause(float delay)
    {
        float elapsed = 0;

        while (true)
        {
            elapsed += Time.unscaledDeltaTime;

            if (elapsed >= delay)
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                Hide();
                OfferRejected?.Invoke();
                yield return null;
            }

            _counter.text = Mathf.RoundToInt(delay - elapsed).ToString();

            yield return new WaitForEndOfFrame();
        }
    }
}
