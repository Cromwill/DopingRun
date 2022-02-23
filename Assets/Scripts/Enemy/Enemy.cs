using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _triggerRadius;
    [SerializeField] private LayerMask _enemyLayerMask;

    [SerializeField]private bool IsAlive = true;
    [SerializeField] private Enemy _target;

    public Enemy Target => _target;

    public event Action<Enemy> Dead;
    public event Action AllEnemyDead;

    private void FixedUpdate()
    {
        if(_target == null)
        {
            FindTarget();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(1,0,0,0.1f);
        //Gizmos.DrawSphere(transform.position, _triggerRadius);
    }

    public void Init(Enemy target)
    {
        if(target == null)
        {
            AllEnemyDead?.Invoke();
            return;
        }

        _target = target;
    }

    public void FindTarget()
    {
        Collider[] enemysColliders = Physics.OverlapSphere(transform.position, _triggerRadius, _enemyLayerMask);

        Collider enemyCollider = GetClosestEnemyCollider(enemysColliders);

        Enemy enemy = null;

        if (enemyCollider != null)
        {
            enemy = enemyCollider.GetComponent<Enemy>();
            enemy.Dead += ResetTarget;
        }

        Init(enemy);
    }

    public Collider GetClosestEnemyCollider(Collider[] enemiesColliders)
    {
        Collider enemyCollider = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var enemy in enemiesColliders)
        {
            if (enemy.TryGetComponent(out Enemy tempEnemy) && tempEnemy != this && tempEnemy.IsAlive)
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
        Dead?.Invoke(this);
    }

    private void ResetTarget(Enemy tempEnemy)
    {
        if(IsAlive)
            FindTarget();

        tempEnemy.Dead -= ResetTarget;
    }
}
