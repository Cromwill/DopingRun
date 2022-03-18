using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class SumoFighter : MonoBehaviour
{
    [SerializeField] private float _triggerRadius;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private SumoFighter _target;

    private bool IsAlive = true;

    private Collider _collider;
    public SumoFighter Target => _target;

    public event Action<SumoFighter> Dead;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if(_target == null)
        {
            FindTarget();
        }
    }

    public void Init(SumoFighter target)
    {
        if(target == null)
            return;

        _target = target;
    }

    public void FindTarget()
    {
        
        Collider[] enemysColliders = Physics.OverlapSphere(transform.position, _triggerRadius, _enemyLayerMask);

        Collider enemyCollider = GetClosestEnemyCollider(enemysColliders);

        SumoFighter enemy = null;

        if (enemyCollider != null)
        {
            enemy = enemyCollider.GetComponent<SumoFighter>();
            enemy.Dead += ResetTarget;
        }

        Init(enemy);
        
    }

    private Collider GetClosestEnemyCollider(Collider[] enemiesColliders)
    {
        Collider enemyCollider = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var enemy in enemiesColliders)
        {
            if (enemy.TryGetComponent(out SumoFighter tempEnemy) && tempEnemy != this && tempEnemy.IsAlive)
            {
                float dist = Vector3.Distance(enemy.transform.position, currentPosition);

                if (dist < minDistance)
                {
                    enemyCollider = enemy;
                    minDistance = dist;
                }
            }
        }

        return enemyCollider;
    }

    public void OnDying()
    {
        IsAlive = false;
        _collider.enabled = false;
        Dead?.Invoke(this);
    }

    private void ResetTarget(SumoFighter tempEnemy)
    {
        if(IsAlive)
            FindTarget();

        tempEnemy.Dead -= ResetTarget;
    }
}
