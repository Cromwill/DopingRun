using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;
    [SerializeField] private TMP_Text _text;

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
        if (_counter > _levelList.SceneCount)
            _levelList.GetRandomScene(_counter).LoadSceneAsync();
        else
        {
            AssetReference scene = _levelList.GetScene(_counter);
            scene.ReleaseAsset();
            scene.LoadSceneAsync();
        }
    }

    public void RestartLevel()
    {
        var scene = _levelList.GetScene(_counter);

        if(scene != null)
            scene.ReleaseAsset();

        scene.LoadSceneAsync();
    }

    private void OnLevelCompleted()
    {
        _counter++;

        _saveSystem.SaveLevelsProgression(_counter);
    }
}
