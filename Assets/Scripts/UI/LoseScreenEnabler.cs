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
        _deathTriggers = FindObjectsOfType<Trigger>();
        Error.CheckOnNull(_deathTriggers, nameof(Trigger));

        _loseScreen = FindObjectOfType<LoseScreen>();
        Error.CheckOnNull(_loseScreen, nameof(LoseScreen));

        foreach (var deathTrigger in _deathTriggers)
        {
            deathTrigger.PlayerHasLost += OnLose;
        }
        Debug.Log(_deathTriggers.Length);
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
        StartCoroutine(Delay());
        _winDecider.Disable();
        Debug.Log("asd");
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _loseScreen.gameObject.SetActive(true);
    }
}
