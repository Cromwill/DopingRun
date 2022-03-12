using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Rigidbody[] _rigidbodys;

    private List<Collider> _colliders = new List<Collider>();
    private void Start()
    {
        _rigidbodys = GetComponentsInChildren<Rigidbody>();

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
}
