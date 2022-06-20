using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;
    [SerializeField] private bool _initialLevel;

    private WinnerDecider _winnerDecider;
    private LoseTrigger[] _loseTriggers;
    private SaveSystem _saveSystem = new SaveSystem();
    private float _timePassed;

    public int Counter { get; private set; }

    private void Start()
    {
        _timePassed = Time.time;

        Counter = _saveSystem.LoadLevelsProgression();
    }

    private void OnEnable()
    {
        _winnerDecider = FindObjectOfType<WinnerDecider>();
        _loseTriggers = FindObjectsOfType<LoseTrigger>();

        if (_winnerDecider != null)
            _winnerDecider.Victory += OnLevelCompleted;
    }

    private void OnDisable()
    {
        if (_winnerDecider != null)
            _winnerDecider.Victory -= OnLevelCompleted;
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
        var scene = _levelList.GetCurrentScene();

        Addressables.LoadSceneAsync(scene);
    }

    public void OnLevelCompleted()
    {
        Counter++;

        _saveSystem.SaveLevelsProgression(Counter);
    }

    private int GetTime()
    {
        return (int)(Time.time - _timePassed);
    }
}
