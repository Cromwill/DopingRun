using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexSDKIntegration : MonoBehaviour
{
    public static YandexSDKIntegration Instance = null;

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
        InterestialAd.Show(OnAdOpen);
#pragma warning restore CS0162 // Обнаружен недостижимый код
    }

    private void OnAdOpen()
    {
        
    }
}
