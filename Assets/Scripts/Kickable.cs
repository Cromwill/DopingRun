using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class Kickable : MonoBehaviour, IPushable
{
    [SerializeField] private float _forceStrength;

    private Rigidbody _rigidBody;

    public Action Pushed;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Push(Vector3 direction, float pushSpeed)
    {
        Pushed?.Invoke();
        _rigidBody.AddForce(direction * _forceStrength, ForceMode.VelocityChange);
    }
}
