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
        amplitude.init("20f7406d23a1b7b97854c2620608e1b1");
    }

    private void Start()
    {
        _integrationMetric.OnGameStart();
        _levelsHandler.LoadNextLevel();
    }
}
