using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    private IntegrationMetric _integrationMetric = new IntegrationMetric();

    private void Awake()
    {
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("c4d2e95fea6a747a1b92ed2d50b8be0c");
    }

    private void Start()
    {
        _integrationMetric.OnGameStart();
        _levelsHandler.LoadNextLevel();
    }
}
