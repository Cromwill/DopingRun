using UnityEngine;
using System;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    private const string _regDay = "regDay";

    private void Start()
    {
        _levelsHandler.LoadNextLevel();

        if (PlayerPrefs.HasKey(_regDay) == false)
        {
            PlayerPrefs.SetInt(_regDay, DateTime.Now.Day);
        }
        else
        {
            int firstDay = PlayerPrefs.GetInt(_regDay);
            int daysInGame = DateTime.Now.Day - firstDay;
        }
    }
}
