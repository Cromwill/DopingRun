using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private RagdollHandler _ragdollHandler;
    private Animator _animator;
    private HitCounter _hitCounter;

    private void Awake()
    {
        _ragdollHandler = GetComponentInChildren<RagdollHandler>();
        Error.CheckOnNull(_ragdollHandler, nameof(RagdollHandler));
        
        _animator = _ragdollHandler.GetComponent<Animator>();
        Error.CheckOnNull(_animator, nameof(Animator));

        _hitCounter = GetComponentInChildren<HitCounter>();
    }

    public void Die()
    {
        _animator.enabled = false;
        _ragdollHandler.EnableRagdoll();

        if(_hitCounter != null)
            _hitCounter.OnDeath();
    }
}
