using System;
using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexSDKIntegration : MonoBehaviour
{
    public static YandexSDKIntegration Instance = null;

    public event Action Rewarded;
    public event Action VideoOpened;
    public event Action VideoClosed;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;

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
#pragma warning disable CS0162 // Обнаружен недостижимый код
        yield return YandexGamesSdk.WaitForInitialization();
#pragma warning restore CS0162 // Обнаружен недостижимый код

        if (!PlayerAccount.IsAuthorized)
        {
            PlayerAccount.Authorize();
        }

        while (true)
        {
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }

    public void AdShow()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif

#pragma warning disable CS0162 // Обнаружен недостижимый код
        InterestialAd.Show();
#pragma warning restore CS0162 // Обнаружен недостижимый код
    }

    public void VideoAdShow()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        OnRewardedCallback();
        OnVideoCloseCallback();
        return;
#endif

#pragma warning disable CS0162 // Обнаружен недостижимый код
        VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback, OnVideoErrorCallback);
#pragma warning restore CS0162 // Обнаружен недостижимый код
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
