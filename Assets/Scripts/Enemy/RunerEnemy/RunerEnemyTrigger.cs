using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class RunerEnemyTrigger : MonoBehaviour
{
    [SerializeField] private float _triggerRadius;

    private SphereCollider _sphereCollider;

    public Action<Vector3> PlayerInTriggerZone;

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _triggerRadius;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            PlayerInTriggerZone?.Invoke(direction);
        }
    }
}
