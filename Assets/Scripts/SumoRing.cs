using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;
using System;

public class SumoRing : MonoBehaviour
{
    [SerializeField] private ShakeData _shakeData;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    private FightScreenEnabler _fightScreenEnabler;
    private BreakRoad _breakRoad;
    private CameraShaking _cameraShaking;
    private CameraShaker _cameraShaker;
    private bool _isShaked;

    private void Awake()
    {
        _capsuleCollider.enabled = false;
        _fightScreenEnabler = FindObjectOfType<FightScreenEnabler>();
        Error.CheckOnNull(_fightScreenEnabler, nameof(FightScreenEnabler));
    }

    private void Start()
    {
        _cameraShaking = FindObjectOfType<CameraShaking>();
        Error.CheckOnNull(_cameraShaking, nameof(CameraShaking));

        _cameraShaker = FindObjectOfType<CameraShaker>();
        Error.CheckOnNull(_cameraShaker, nameof(CameraShaker));

        _breakRoad = FindObjectOfType<BreakRoad>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out TargetSwitcher targetSwitcher) && _isShaked == false)
        {
            _cameraShaking.Shake(_shakeData);
            _fightScreenEnabler.OnSumoFightBegun();
            _isShaked = true;
            _capsuleCollider.enabled = true;

            if(_breakRoad != null)
                _breakRoad.Break();
        }
    }
}
