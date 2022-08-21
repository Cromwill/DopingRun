using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stretchable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Slider _slider;

    private float _startValue;

    private SectionView _sectionView;
    private Coroutine _coroutine;

    private void Awake()
    {
        _sectionView = GetComponent<SectionView>();
        _startValue = _slider.value;
    }

    private void OnEnable()
    {
        _sectionView.Unselected += OnUnselect;
        _sectionView.Selected += OnSelect;
    }

    private void OnDisable()
    {
        _sectionView.Unselected -= OnUnselect;
        _sectionView.Selected -= OnSelect;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_sectionView.IsSelected)
            return;

        OnUnselect();
    }

    private void OnUnselect()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Stretch(_startValue, 0.5f));
    }

    private void OnSelect()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Stretch(1f, 0.5f));
    }

    private IEnumerator Stretch(float end, float time)
    {
        float elapsed = 0;

        while (elapsed < time)
        {
            elapsed += Time.unscaledDeltaTime;
            _slider.value = Mathf.Lerp(_slider.value, end, time);

            yield return new WaitForEndOfFrame();
        }

        _coroutine = null;
    }
}
