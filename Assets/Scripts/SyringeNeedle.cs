using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeNeedle : MonoBehaviour
{
    [SerializeField] private Enlargable _enlargable;

    private const float _initialScaleValue = 3.5f;
    private Vector3 _initialScale = new Vector3(_initialScaleValue, _initialScaleValue, _initialScaleValue);
    private float _scalePerStep => (_initialScaleValue / _enlargable.MaxStep);
    private const float  _modelCorrection = 0.68f;
    private void Start()
    {
        transform.localScale = _initialScale;
    }

    private void OnEnable()
    {
        _enlargable.StepChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _enlargable.StepChanged -= ChangeValue;
    }

    public void ChangeValue(int currentStep, int maxStep)
    {
        int step = maxStep - currentStep;

        float targetScale = _scalePerStep * step - _modelCorrection;

        if (currentStep != 0)
            transform.localScale = new Vector3(targetScale, targetScale, targetScale);

    }
}
