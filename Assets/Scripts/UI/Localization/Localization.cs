using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public static Localization Instance = null;

    public Language Language { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SDKIntegration.Instance.Initialized += OnInitialized;
    }

    private void OnDisable()
    {
        SDKIntegration.Instance.Initialized -= OnInitialized;
    }

    private void OnInitialized()
    {
#if YANDEX_GAMES
        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case "ru":
                Language = Language.ru;
                break;

            case "en":
                Language = Language.en;
                break;

            case "tr":
                Language = Language.tr;
                break;

            default:
                break;
        }
#endif

#if VK_GAMES || UNITY_EDITOR
        Language = Language.ru;
#endif
    }
}
