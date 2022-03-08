using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreenEnabler : MonoBehaviour
{
    [SerializeField] private float _delay;

    private ILosable[] _deathTriggers;
    private LoseScreen _loseScreen;
    private WinnerDecider _winDecider;

    private void Awake()
    {
        _winDecider = FindObjectOfType<WinnerDecider>();
        Error.CheckOnNull(_winDecider, nameof(WinnerDecider));
    }
    private void Start()
    {
        _loseScreen.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _deathTriggers = FindObjectsOfType<LoseTrigger>();
        Error.CheckOnNull(_deathTriggers, nameof(LoseTrigger));

        _loseScreen = FindObjectOfType<LoseScreen>();
        Error.CheckOnNull(_loseScreen, nameof(LoseScreen));

        foreach (var deathTrigger in _deathTriggers)
        {
            deathTrigger.PlayerHasLost += OnLose;
        }
    }

    private void OnDisable()
    {
        foreach (var deathTrigger in _deathTriggers)
        {
            deathTrigger.PlayerHasLost -= OnLose;
        }
    }

    private void OnLose()
    {
        _winDecider.Disable();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _loseScreen.gameObject.SetActive(true);
    }
}
