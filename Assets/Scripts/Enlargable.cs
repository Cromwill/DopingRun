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

    private float _additionalScale => _step * _scalePerStep;
    private Vector3 _nexSteptScale => new Vector3(_initialScale.x + _additionalScale, _initialScale.y + _additionalScale, _initialScale.z + _additionalScale);
    private Vector3 _enlargeScale => _nexSteptScale *_scaleCoefficient;

    private void Start()
    {
        _initialScale = transform.localScale;   
    }

    public void Reset()
    {
        transform.localScale = _initialScale;
        _step = 0;
    }

    public void EnalargeAnimation()
    {
        if (_step < _maxSteps)
        {
            _step++;
            if (_coroutine == null)
                _coroutine = StartCoroutine(Enlarge());
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
