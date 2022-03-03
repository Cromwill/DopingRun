using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Touchable : MonoBehaviour, IPushable
{
    [SerializeField] private RunerEnemyTrigger _enemyTriggerZone;
    [SerializeField] private RunerEnemyMover _enemyMover;
    [SerializeField] private RagdollHandler _ragdollHandler;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _forceMultiplier;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 direction, float pushSpeed)
    {
        _enemyTriggerZone.gameObject.SetActive(false);
        _animator.enabled = false;
        _enemyMover.enabled = false;
        _ragdollHandler.EnableRagdoll();
        _rigidbody.AddForce(-transform.parent.transform.forward * pushSpeed * _forceMultiplier, ForceMode.VelocityChange);
    }
}
