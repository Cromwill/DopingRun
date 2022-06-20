using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Pushable _pushable;
    [SerializeField] private HustleZone _hustleZone;
    [SerializeField] private EnlargePlayer _enlargePlayer;

    private StartLevelButton _startLevel;
    private float _gainMuscleAnimationTime;
    private float _transformationAnimationTime;
    private float _injectionAnimationTime =0.75f;
    private float _animationSpeed = 1.6f;

    public float AnimationTime => _gainMuscleAnimationTime;
    public float TransformationAnimationTime => _transformationAnimationTime;
    public float InjectionAnimationTime => _injectionAnimationTime;

    private void Awake()
    {
        _startLevel = FindObjectOfType<StartLevelButton>();
        Error.CheckOnNull(_startLevel, nameof(StartLevelButton));

        AnimationClip[] _animationClips = _animator.runtimeAnimatorController.animationClips;

        foreach (var animationClip in _animationClips)
        {
            if (animationClip.name == AnimationClipNames.Injection)
            { 
                _gainMuscleAnimationTime += animationClip.length / _animationSpeed;
            }

            if (animationClip.name == AnimationClipNames.Transformation)
            {
                _transformationAnimationTime = animationClip.length / _animationSpeed;
                _gainMuscleAnimationTime += _transformationAnimationTime;
            }
        } 
    }

    private void OnEnable()
    {
        _startLevel.RunStarted += Run;
        _hustleZone.CollidedWithTouchable += OnCollideWithTouchable;
        _hustleZone.CollidedWithPushable += OnCollidedWithPushable;
        _pushable.PushStart += OnPushed;
    }

    private void OnDisable()
    {
        _startLevel.RunStarted -= Run;
        _hustleZone.CollidedWithTouchable -= OnCollideWithTouchable;
        _hustleZone.CollidedWithPushable -= OnCollidedWithPushable;
        _pushable.PushStart -= OnPushed;
    }

    public void GainMuscle()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationClipNames.Run))
            _animator.SetTrigger(AnimationClipNames.GainMuscle);
    }

    private void OnCollideWithTouchable()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationClipNames.Run))
            _animator.SetTrigger(AnimationClipNames.Hitted);
    }

    private void OnCollidedWithPushable()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationClipNames.SumoRun))
        {
            _animator.SetTrigger(AnimationClipNames.Attack);
        }
    }

    private void OnPushed()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationClipNames.SumoRun))
        {
            _animator.SetTrigger(AnimationClipNames.Hitted);
        } 
    }

    private IEnumerator ResetTrigger(string name)
    {
        yield return new WaitForSeconds(0.01f);

        _animator.ResetTrigger(name);
    }


    public void Run()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationClipNames.Idle))
            _animator.SetTrigger(AnimationClipNames.Run);
    }

    public void OnVictory()
    {
        _animator.SetLayerWeight(1, 0);
        _animator.SetTrigger(AnimationClipNames.Victory);
    }
}
