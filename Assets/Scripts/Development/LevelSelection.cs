using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.TMP_Dropdown;

public class LevelSelection : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            _dropdown.options.Add(new OptionData($"Level {i}"));
        }

        _dropdown.SetValueWithoutNotify(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnSelect);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnSelect);
    }

    private void OnSelect(int level)
    {
        LevelsHandler.Instance.Load(level + 1);
    }
}
