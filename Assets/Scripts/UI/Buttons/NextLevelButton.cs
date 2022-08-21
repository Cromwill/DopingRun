using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NextLevelButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
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
#if UNITY_EDITOR
        LevelsHandler.Instance.LoadNextLevel();
        return;
#endif
        SDKIntegration.Instance.AdShow(() => LevelsHandler.Instance.LoadNextLevel());
    }
}
