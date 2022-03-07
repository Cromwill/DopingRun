using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private RagdollHandler _ragdollHandler;
    private Animator _animator;

    private void Awake()
    {
        _ragdollHandler = GetComponentInChildren<RagdollHandler>();
        Error.CheckOnNull(_ragdollHandler, nameof(RagdollHandler));
        
        _animator = _ragdollHandler.GetComponent<Animator>();
        Error.CheckOnNull(_animator, nameof(Animator));
    }

    public void Die()
    {
        _animator.enabled = false;
        _ragdollHandler.EnableRagdoll();
    }
}
