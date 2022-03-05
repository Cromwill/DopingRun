using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;

    private WinnerDecider _winnderDecider;
    public int Counter { get; private set; }

    private SaveSystem _saveSystem = new SaveSystem();

    private void Start()
    {
        Counter = _saveSystem.LoadLevelsProgression();
    }

    private void OnEnable()
    {
        _winnderDecider = FindObjectOfType<WinnerDecider>();

        if(_winnderDecider != null)
            _winnderDecider.Victory += OnLevelCompleted;
    }

    private void OnDisable()
    {
        if (_winnderDecider != null)
            _winnderDecider.Victory -= OnLevelCompleted;
    }

    public void LoadNextLevel()
    {
        if (Counter > _levelList.SceneCount)
            _levelList.GetRandomScene(Counter).LoadSceneAsync();
        else
            _levelList.GetScene(Counter).LoadSceneAsync();
    }

    public void RestartLevel()
    {
        var scene = _levelList.GetScene(Counter);

        if (scene != null)
            scene.ReleaseAsset();

        scene.LoadSceneAsync();
    }

    private void OnLevelCompleted()
    {
        Counter++;

        _saveSystem.SaveLevelsProgression(Counter);
    }
}
