using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(MeshCollider))]
public class BreakRoad : MonoBehaviour
{
    private MeshCollider _collider;
    private Rigidbody _rigidbody;
    private MeshFilter _mesh;

    private const int BreakeableRoadLayer = 7;

    private void Awake()
    {
        _mesh = GetComponent<MeshFilter>();
        _collider = GetComponent<MeshCollider>();
        _collider.convex = false;

        _collider.sharedMesh = _mesh.mesh;

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
