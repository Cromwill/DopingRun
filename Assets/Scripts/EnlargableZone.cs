using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enlargable))]
public class EnlargableZone : MonoBehaviour
{
    [SerializeField] private HustleZone _hustleZone;

    private Enlargable _enlaragable;

    private const int _pushableStepCount = 1;
    private int _breakableStepCount => (int)(_enlaragable.Step * 0.6f);
    private void Start()
    {
        _enlaragable = GetComponent<Enlargable>();
    }
    private void OnEnable()
    {
        _hustleZone.CollidedWithPushable += OnCollideWithEnemy;
        _hustleZone.CollidedWithBreakable += OnCollideWithBreakable;
    }

    private void OnDisable()
    {
        _hustleZone.CollidedWithPushable -= OnCollideWithEnemy;
        _hustleZone.CollidedWithBreakable -= OnCollideWithBreakable;
    }

    private void OnCollideWithEnemy()
    {
        _enlaragable.ShrinkAnimation(_pushableStepCount);
    }

    private void OnCollideWithBreakable()
    {
        _enlaragable.ShrinkAnimation(_breakableStepCount);
    }
}
