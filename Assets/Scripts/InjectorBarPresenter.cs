using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LiquidVolumeFX;

[RequireComponent(typeof(Slider))]
public class InjectorBarPresenter : MonoBehaviour
{
    [SerializeField] private Enlargable _enlargable;
    [SerializeField] private LiquidVolume _liquid;

    private Slider _slider;
    private float _currentValue;
    private float _changeSpeed = 3f;
    private float _minimalLiquidValue = 0.2f;

    public event Action OnValueZero;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();

        _enlargable.StepChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _enlargable.StepChanged -= ChangeValue;
    }

    public void SetTimeToErase(float time)
    {
        _changeSpeed = _slider.value / time;
    }

    public void ChangeValue(int value, int maxValue)
    {
        _currentValue = (float)value/ maxValue;
        StartCoroutine(ChangeCroutine(_changeSpeed));
    }

    private IEnumerator ChangeCroutine(float changeSpeed)
    {
        while (_slider.value != _currentValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _currentValue, changeSpeed * Time.deltaTime);
            _liquid.level = _slider.value;

            yield return null;
        }

        if(_slider.value <= 0)
        {
            OnValueZero?.Invoke();
        }
    }
}
