using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InjectionBarAnimator : MonoBehaviour
{
    private Animator _animator;
    private InjectorBarPresenter _injectionBarPresenter;

    private const string Disapear = "Disapear";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _injectionBarPresenter = FindObjectOfType<InjectorBarPresenter>();

        if(_injectionBarPresenter == null)
            throw new NullReferenceException($"FindObjectOfType did not find {nameof(InjectorBarPresenter)}");

        _injectionBarPresenter.OnValueZero += PlayDisapear;
    }

    private void OnDisable()
    {
        _injectionBarPresenter.OnValueZero -= PlayDisapear;
    }

    private void PlayDisapear()
    {
        _animator.Play(Disapear);
    }
}
