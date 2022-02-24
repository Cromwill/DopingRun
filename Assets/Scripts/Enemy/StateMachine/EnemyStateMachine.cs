using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SumoFighter))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private SumoFighter _target;
    private SumoFighter _self;
    private State _currentState;

    public State Current => _currentState;

    private void Start()
    {
        _self = GetComponent<SumoFighter>();
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNext();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        _self.FindTarget();
        _target = _self.Target;

        _currentState = startState;
        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if(_currentState != null)
            _currentState.Enter(_target);
    }
}
