using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Rigidbody[] _rigidbodys;
    private Vector3[] _initialPositions;

    private List<Collider> _colliders = new List<Collider>();

    private void Start()
    {
        _rigidbodys = GetComponentsInChildren<Rigidbody>();
        _initialPositions = new Vector3[_rigidbodys.Length];

        for (int i = 0; i < _rigidbodys.Length; i++)
        {
            _initialPositions[i] = _rigidbodys[i].position;
        }

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = true;

            if (rigidbody.TryGetComponent(out CharacterJoint characterJoint))
                characterJoint.enableProjection = true;

            if (rigidbody.TryGetComponent(out Collider collider))
            {
                _colliders.Add(collider);
                collider.enabled = false;
            }
        }
    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = false;

            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public void DisableRagdoll()
    {
        for (int i = 0; i < _rigidbodys.Length; i++)
        {
            _rigidbodys[i].position = _initialPositions[i];
        }

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = true;

            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
