using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    private void Start()
    {
        _levelsHandler.LoadNextLevel();
    }
}
