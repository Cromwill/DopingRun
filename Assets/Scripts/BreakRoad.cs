using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(MeshCollider))]
public class BreakRoad : MonoBehaviour
{
    private MeshCollider _collider;
    private Rigidbody _rigidbody;

    private const int BreakeableRoadLayer = 7;

    private void Awake()
    {
        _collider = GetComponent<MeshCollider>();
        _collider.convex = false;

        gameObject.layer = BreakeableRoadLayer;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Break()
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = false;
    }
}
