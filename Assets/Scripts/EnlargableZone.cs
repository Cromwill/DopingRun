using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enlargable))]
public class EnlargableZone : MonoBehaviour
{
    [SerializeField] private HustleZone _hustleZone;

    private Enlargable _enlaragable;

    private void Start()
    {
        _enlaragable = GetComponent<Enlargable>();
    }
    private void OnEnable()
    {
        _hustleZone.CollidedWithPushable += OnCollideWithEnemy;
    }

    private void OnDisable()
    {
        _hustleZone.CollidedWithPushable -= OnCollideWithEnemy;
    }

    private void OnCollideWithEnemy()
    {
        _enlaragable.ShrinkAnimation();
    }
}
