using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsHandler : MonoBehaviour
{
    public static LevelsHandler Instance = null;

    public int Counter { get; private set; }

    public event Action<int> LevelLoaded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextLevel()
    {
        if (Counter >= SceneManager.sceneCountInBuildSettings)
            Counter = 1;
        else
            Counter++;

        LevelLoaded?.Invoke(Counter - 1);
        SceneManager.LoadScene(Counter);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
