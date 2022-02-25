using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enlargable : MonoBehaviour
{
    [SerializeField] private float _scalePerStep;
    [SerializeField] private int _maxSteps;
    [SerializeField] private float _changeSpeed;
    [SerializeField] private float _scaleCoefficient;

    private int _step;
    private Vector3 _initialScale;
    private Coroutine _coroutine;

    public int Step => _step;
    private float _additionalScale => _step * _scalePerStep;
    private Vector3 _nexSteptScale => new Vector3(_initialScale.x + _additionalScale, _initialScale.y + _additionalScale, _initialScale.z + _additionalScale);
    private Vector3 _enlargeScale => _nexSteptScale *_scaleCoefficient;

    public event Action<int, int> StepChanged;

    private void Start()
    {
        _initialScale = transform.localScale;
        StepChanged?.Invoke(_step, _maxSteps);
    }

    public void Reset()
    {
        _step = 0;
        ShrinkAnimation();
    }

    public void EnalargeAnimation()
    {
        if (_step < _maxSteps)
        {
            _step++;

            StepChanged?.Invoke(_step, _maxSteps);

            if (_coroutine == null)
                _coroutine = StartCoroutine(Enlarge());
        }
    }

    public void ShrinkAnimation()
    {
        if (_step >= 0)
        {
            _step--;

            StepChanged?.Invoke(_step, _maxSteps);

            StartCoroutine(Shrink());
        }
    }

    private IEnumerator Enlarge()
    {
        while (transform.localScale.x < _enlargeScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _enlargeScale, _changeSpeed * Time.deltaTime);
            yield return null;
        }

        _coroutine = StartCoroutine(Shrink());
    }

    private IEnumerator Shrink()
    {
        while (transform.localScale.x > _nexSteptScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _nexSteptScale, _changeSpeed * Time.deltaTime);

            yield return null;
        }

        _coroutine = null;
    }
}
