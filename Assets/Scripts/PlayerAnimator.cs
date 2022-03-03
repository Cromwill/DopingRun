using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Pushable _pushable;
    [SerializeField] private HustleZone _hustleZone;

    private AnimationClipNames _clipName;
    private float _gainMuscleAnimationTime;
    private float _transformationAnimationTime;
    private float _injectionAnimationTime =0.75f;
    private float _animationSpeed = 1.6f;

    public float AnimationTime => _gainMuscleAnimationTime;
    public float TransformationAnimationTime => _transformationAnimationTime;
    public float InjectionAnimationTime => _injectionAnimationTime;

    private void Awake()
    {
        _clipName = new AnimationClipNames();

        AnimationClip[] _animationClips = _animator.runtimeAnimatorController.animationClips;

        foreach (var animationClip in _animationClips)
        {
            if (animationClip.name == _clipName.Injection)
            { 
                _gainMuscleAnimationTime += animationClip.length / _animationSpeed;
            }

            if (animationClip.name == _clipName.Transformation)
            {
                _transformationAnimationTime = animationClip.length / _animationSpeed;
                _gainMuscleAnimationTime += _transformationAnimationTime;
            }
        } 

        Debug.Log(_gainMuscleAnimationTime);
    }

    private void OnEnable()
    {
        _hustleZone.CollidedWithTouchable += OnCollideWithTouchable;
        _hustleZone.CollidedWithPushable += OnCollidedWithPushable;
        _pushable.PushStart += OnPushed;
    }

    private void OnDisable()
    {
        _hustleZone.CollidedWithTouchable -= OnCollideWithTouchable;
        _hustleZone.CollidedWithPushable -= OnCollidedWithPushable;
        _pushable.PushStart -= OnPushed;
    }

    public void GainMuscle()
    {
        _animator.SetTrigger(_clipName.GainMuscle);
    }

    private void OnCollideWithTouchable()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName.Run))
            _animator.SetTrigger(_clipName.Hitted);
    }

    private void OnCollidedWithPushable()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName.SumoRun))
            _animator.SetTrigger(_clipName.Attack);
    }

    private void OnPushed()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName.SumoRun))
            _animator.SetTrigger(_clipName.Hitted);
    }
}
