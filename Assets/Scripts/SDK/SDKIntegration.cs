using System;
using System.Collections;
using Agava.YandexGames;
using Agava.VKGames;
using UnityEngine;

public class SDKIntegration : MonoBehaviour
{
    public static SDKIntegration Instance = null;

    public event Action Rewarded;
    public event Action VideoOpened;
    public event Action VideoClosed;
    public event Action Initialized;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif

#if YANDEX_GAMES
        yield return YandexGamesSdk.Initialize(OnYandexSDKInitialize);
#endif

#if VK_GAMES
        yield return VKGamesSdk.Initialize(OnVKSDKInitialize);
#endif
    }

    private void OnYandexSDKInitialize()
    {
        Initialized?.Invoke();
    }

    private void OnVKSDKInitialize()
    {
        Initialized?.Invoke();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (MuteButton.IsMuted && focus)
            return;

        AudioListener.volume = focus ? 1 : 0;
    }

    public void AdShow(Action onVideoClosed)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

#if YANDEX_GAMES
        Agava.YandexGames.InterstitialAd.Show(OnAdOpened, (wasShown) =>
        {
            AudioListener.pause = false;
            Debug.Log("Closed Interestial Ad " + AudioListener.pause);
            onVideoClosed?.Invoke();
        });
#endif

#if VK_GAMES
        Interstitial.Show();
        onVideoClosed();
        Analytics.Instance.InterstitialStart("start_level", "VK_games");
#endif
    }

    public void VideoAdShow()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnRewardedCallback();
        OnVideoCloseCallback();
        return;
#endif

#if YANDEX_GAMES
        Agava.YandexGames.VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback, OnVideoErrorCallback);
#endif

#if VK_GAMES
        Agava.VKGames.VideoAd.Show(OnRewardedCallback);
#endif
    }

    private void OnAdOpened()
    {
        AudioListener.pause = true;
        Debug.Log("Opened Interestial Ad " + AudioListener.pause);
    }

    private void OnVideoOpenCallback()
    {
        VideoOpened?.Invoke();
    }

    private void OnVideoCloseCallback()
    {
        VideoClosed?.Invoke();
    }

    private void OnRewardedCallback()
    {
        Rewarded?.Invoke();
    }

    private void OnVideoErrorCallback(string message)
    {
        Debug.LogError(message);
    }
}