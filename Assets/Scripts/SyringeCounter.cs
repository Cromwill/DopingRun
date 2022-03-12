using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeCounter : MonoBehaviour
{
    [SerializeField] private WinnerDecider _winnerDecider;

    private Injector[] _injectors;
    public int IntilCount {get; private set;}
    public int EndCount { get; private set;}
    private void Start()
    {
        _injectors = FindObjectsOfType<Injector>();
        IntilCount = _injectors.Length;
    }

    private void OnEnable()
    {
        _winnerDecider.Victory += OnVictory;
    }

    private void OnDisable()
    {
        _winnerDecider.Victory -= OnVictory;
    }

    private void OnVictory()
    {
        _injectors = FindObjectsOfType<Injector>();
        EndCount = IntilCount - _injectors.Length;
    }
}
