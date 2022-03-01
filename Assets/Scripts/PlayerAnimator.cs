using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    private const string _injection = "Injection";
    private const string _gainMuscle = "GainMuscle";
    private const string _transformation = "Transformation";
    private const string _run = "Run";

    private float _GainMuscleAnimationTime;
    private float _transformationAnimationTime;
    private float _injectionAnimationTime =0.75f;

    public float AnimationTime => _GainMuscleAnimationTime;
    public float TransformationAnimationTime => _transformationAnimationTime;
    public float InjectionAnimationTime => _injectionAnimationTime;
    private void Awake()
    {
        AnimationClip[] _animationClips = _animator.runtimeAnimatorController.animationClips;

        foreach (var animationClip in _animationClips)
        {
            if(animationClip.name != _run)
                _GainMuscleAnimationTime += animationClip.length;

            if (animationClip.name == _transformation)
                _transformationAnimationTime = animationClip.length;
        }
    }
    public void GainMuscle()
    {
        _animator.SetTrigger(_gainMuscle);
    }
}
