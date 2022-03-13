using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private TMP_Text _syringeCounterText;
    [SerializeField] private float _maxFontSize;
    [SerializeField] private Color _allSyringeCollectedCollor;
    [SerializeField] private LevelsHandler _levelsHandler;
    [SerializeField] private SyringeCounter _syringeCounter;

    private TextAnimation _textAnimation = new TextAnimation();

    public void ShowLevelNumber()
    {
        _levelNumber.text = $"Level {_levelsHandler.Counter}";
    }

    private IEnumerator CountSyringe()
    {
        int count = 0;

        while (count< _syringeCounter.EndCount)
        {
            count++;
            _syringeCounterText.text = $"{count}/{_syringeCounter.IntilCount}";

            yield return new WaitForSeconds(0.05f);
        }

        if (count >= _syringeCounter.IntilCount)
        {
            _syringeCounterText.color = _allSyringeCollectedCollor;
            StartCoroutine(_textAnimation.EnlargeAnimation(_syringeCounterText, _maxFontSize, _syringeCounterText.fontSize));
        }
    }
}
