using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;

    private WinnerDecider _winnderDecider;
    private int _counter;

    private SaveSystem _saveSystem = new SaveSystem();

    private void Start()
    {
        _counter = _saveSystem.LoadLevelsProgression();
    }

    private void OnEnable()
    {
        _winnderDecider = FindObjectOfType<WinnerDecider>();
        Error.CheckOnNull(_winnderDecider, nameof(WinnerDecider));
        _winnderDecider.Victory += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _winnderDecider.Victory -= OnLevelCompleted;
    }

    public void LoadNextLevel()
    {
        if (_counter > _levelList.SceneCount)
            _levelList.GetRandomScene(_counter).LoadSceneAsync();
        else
            _levelList.GetScene(_counter).LoadSceneAsync();
    }

    public void RestartLevel()
    {
        var scene = _levelList.GetScene(_counter);

        scene.LoadSceneAsync();
    }

    private void OnLevelCompleted()
    {
        _counter++;

        _saveSystem.SaveLevelsProgression(_counter);
    }
}
