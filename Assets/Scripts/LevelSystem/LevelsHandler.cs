using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using Agava.VKGames;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private bool _isInitial;
    [SerializeField] private float _loadingTime;

    public static LevelsHandler Instance = null;

    public int Counter { get; private set; }

    public event Action<int> LevelLoaded;
    public event Action LevelRestarted;

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
        ProgressSaver.Instance.ProgressLoaded += OnProgressLoad;
    }

    private void OnDisable()
    {
        ProgressSaver.Instance.ProgressLoaded -= OnProgressLoad;
    }

    public void LoadNextLevel()
    {
        if (Counter >= SceneManager.sceneCountInBuildSettings - 2)
            Counter = 1;
        else
            Counter++;

        LevelLoaded?.Invoke(Counter);
        Load(Counter);
    }

    public void RestartLevel()
    {
        LevelRestarted?.Invoke();
        Analytics.Instance.RestartLevel(Counter);

        Load(Counter);
    }

    public void Load(int level)
    {
        Counter = level;

        Analytics.Instance.StartLevel(Counter);
        SceneManager.LoadScene(Counter);
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnProgressLoad(Saving saving)
    {
#if UNITY_EDITOR
        Load(saving.Level);
        return;
#endif

        if (_isInitial)
        {
            StartCoroutine(StartLoading(_loadingTime, saving.Level));
        }
        else
        {
            Load(saving.Level);
        }
    }

    private IEnumerator StartLoading(float delay, int level)
    {
        yield return new WaitForSeconds(delay);

        Load(level);
    }
}
