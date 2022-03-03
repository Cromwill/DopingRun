using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BreakablePiece : MonoBehaviour, IPushable
{
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;
    }

    public void Push(Vector3 direction, float pushSpeed)
    {
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(direction * pushSpeed, ForceMode.VelocityChange);
    }

    public void EnableRigidbody()
    {
        _rigidBody.isKinematic = false;
    }
}
