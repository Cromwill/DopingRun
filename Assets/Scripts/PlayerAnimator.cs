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

    public float AnimationTime => _gainMuscleAnimationTime;
    public float TransformationAnimationTime => _transformationAnimationTime;
    public float InjectionAnimationTime => _injectionAnimationTime;
    private void Awake()
    {
        _clipName = new AnimationClipNames();

        AnimationClip[] _animationClips = _animator.runtimeAnimatorController.animationClips;

        foreach (var animationClip in _animationClips)
        {
            if(animationClip.name == _clipName.Injection)
            {
                _gainMuscleAnimationTime += animationClip.length;
            }

            if (animationClip.name == _clipName.Transformation)
            {
                _transformationAnimationTime = animationClip.length;
                _gainMuscleAnimationTime += _transformationAnimationTime;
            }
        }
    }

    private void OnEnable()
    {
        _hustleZone.CollidedWithTouchable += OnCollideWithPushable;
        _pushable.PushStart += OnPushed;
    }

    private void OnDisable()
    {
        _hustleZone.CollidedWithTouchable -= OnCollideWithPushable;
        _pushable.PushStart -= OnPushed;
    }
    public void GainMuscle()
    {
        _animator.SetTrigger(_clipName.GainMuscle);
    }

    private void OnCollideWithPushable()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName.Run))
            _animator.SetTrigger(_clipName.Attack);

        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName.RunSumo))
            _animator.SetTrigger(_clipName.Attack);
    }

    private void OnPushed()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_clipName.RunSumo))
            _animator.SetTrigger(_clipName.Hitted);
    }
}
