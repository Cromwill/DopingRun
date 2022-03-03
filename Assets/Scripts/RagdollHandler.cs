using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Rigidbody[] _rigidbodys;
    private const int _untuchableLayer = 9;

    private void Start()
    {
        _rigidbodys = GetComponentsInChildren<Rigidbody>();

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = false;
            rigidbody.gameObject.layer = _untuchableLayer;
        }
    }
}
