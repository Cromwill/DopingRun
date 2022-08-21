using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

[RequireComponent(typeof(Button))]
public class OpenShopButton : MonoBehaviour
{
    private Button _button;
    private Player _player;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (SystemInfo.deviceType == UnityEngine.DeviceType.Desktop)
        {
            LevelsHandler.Instance.Load("Shop");
            return;
        }
#endif
        _player.Save();
        LevelsHandler.Instance.Load("Shop");
    }
}
