using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpringTrapAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string _stepped = "Stepped";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            _animator.SetTrigger(_stepped);
    }
}
