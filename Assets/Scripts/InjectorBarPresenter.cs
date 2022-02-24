using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class InjectorBarPresenter : MonoBehaviour
{
    [SerializeField] private Enlargable _enlargable;

    private Slider _slider;
    private float _currentValue;
    private float _changeSpeed = 3f;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();

        _enlargable.StepChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _enlargable.StepChanged -= ChangeValue;
    }

    private void ChangeValue(int value, int maxValue)
    {
        _slider.maxValue = maxValue;
        _currentValue = value;

        StartCoroutine(ChangeCroutine());
    }

    private IEnumerator ChangeCroutine()
    {
        while (_slider.value != _currentValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _currentValue, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
