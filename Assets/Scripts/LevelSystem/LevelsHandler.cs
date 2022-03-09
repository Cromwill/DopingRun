using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;
    [SerializeField] private bool _InitialLevel;

    private WinnerDecider _winnderDecider;
    private LoseTrigger[] _loseTriggers;
    private SaveSystem _saveSystem = new SaveSystem();
    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private float _timePassed;
    public int Counter { get; private set; }

    private void Start()
    {
        _timePassed = Time.time;

        Counter = _saveSystem.LoadLevelsProgression();

        if (_InitialLevel == false)
            _integrationMetric.OnLevelStart(Counter);
    }

    private void OnEnable()
    {
        _winnderDecider = FindObjectOfType<WinnerDecider>();
        _loseTriggers = FindObjectsOfType<LoseTrigger>();

        foreach (var loseTrigger in _loseTriggers)
        {
            if (loseTrigger != null)
                loseTrigger.PlayerHasLost += OnLevelFailed;
        }

        if (_winnderDecider != null)
            _winnderDecider.Victory += OnLevelCompleted;
    }

    private void OnDisable()
    {
        if (_winnderDecider != null)
            _winnderDecider.Victory -= OnLevelCompleted;

        foreach (var loseTrigger in _loseTriggers)
        {
            if (loseTrigger != null)
                loseTrigger.PlayerHasLost -= OnLevelFailed;
        }
    }

    public void LoadNextLevel()
    {
        if (Counter >= _levelList.SceneCount)
            _levelList.GetRandomScene(Counter).LoadSceneAsync();
        else
            _levelList.GetScene(Counter).LoadSceneAsync();
    }

    public void RestartLevel()
    {
        _integrationMetric.OnRestartLevel(Counter);

        var scene = _levelList.GetCurrentScene();

        Addressables.LoadSceneAsync(scene);
    }

    public void OnLevelCompleted()
    {
        _integrationMetric.OnLevelComplete(GetTime(), Counter);

        Counter++;

        _saveSystem.SaveLevelsProgression(Counter);

        Debug.Log(Counter);
    }

    private void OnLevelFailed()
    {
        _integrationMetric.OnLevelFail(GetTime(), Counter);
    }

    private int GetTime()
    {
        return (int)(Time.time - _timePassed);
    }
}
