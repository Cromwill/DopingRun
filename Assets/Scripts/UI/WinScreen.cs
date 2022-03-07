using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private LevelsHandler _levelsHandler;

    public void ShowLevelNumber()
    {
        _text.text = $"Level {_levelsHandler.Counter}";
    }
}
