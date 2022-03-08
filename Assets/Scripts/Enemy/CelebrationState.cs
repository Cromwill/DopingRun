using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CelebrationState : State
{
    [SerializeField] private PlayerAnimator _animator;
    private void OnEnable()
    {
        _animator.OnVictory();
    }
}
