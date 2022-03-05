using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargePlayer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private float _timeToGainMuscle;
    [SerializeField] private float _targetScaleValue;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Material _targetMaterial;

    private Vector3 _maxScale;
    private float _scalePerStep;
    private float _weightPerStep;
    private const float _initialScale = 1f;
    private const float _maxWeight = 100f;
    private const string _outline = "_OutlineWidth";
    private const float _maxOutlineWidth = 3f;
    private float _targetWeight;
    private Enlargable _enlargable;
    private InjectorBarPresenter _barPresenter;
    private PlayerAnimator _playerAnimator;

    public event Action<EnlargePlayer> AnimationEnd;

    private void Awake()
    {
        _maxScale = Vector3.one * _targetScaleValue;
        _enlargable = FindObjectOfType<Enlargable>();
        Error.CheckOnNull(_enlargable, nameof(Enlargable));

        StepCalculation();

        _barPresenter = FindObjectOfType<InjectorBarPresenter>();
        Error.CheckOnNull(_barPresenter, nameof(InjectorBarPresenter));

        _playerAnimator = FindObjectOfType<PlayerAnimator>();
        Error.CheckOnNull(_playerAnimator, nameof(PlayerAnimator));
    }

    private void StepCalculation()
    {
        float sizeDiffrence = _targetScaleValue - _initialScale;
        _scalePerStep = sizeDiffrence / _enlargable.MaxStep;
        _weightPerStep = Mathf.Ceil(_maxWeight / _enlargable.MaxStep);
    }

    private void TargetSizeCalculation()
    {
        _maxScale = (_scalePerStep * _enlargable.Step + _initialScale) * Vector3.one;
        _targetWeight = _weightPerStep * _enlargable.Step;
        _targetWeight = Mathf.Clamp(_targetWeight, 0, _maxWeight);
    } 

    private void OnInjection()
    {
        StepCalculation();
        TargetSizeCalculation();

        _barPresenter.SetTimeToErase(_playerAnimator.InjectionAnimationTime);
        _enlargable.Reset();
    }

    private void OnFlexing()
    {
        StartCoroutine(EnlargeAnimation());
        StartCoroutine(GainMuscleAnimation());
        StartCoroutine(Hulkization());
    }

    private void OnFlexEnd()
    {
        AnimationEnd?.Invoke(this);
    }

    private IEnumerator EnlargeAnimation()
    {
        float changeSpeed = (_maxScale.x - transform.localScale.x) / _timeToGainMuscle;

        while (transform.localScale.x<= _maxScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _maxScale, changeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator GainMuscleAnimation()
    {
        float currentWeight = 0;
        float changeSpeed = _targetWeight / _timeToGainMuscle;

        while (currentWeight < _targetWeight)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, _targetWeight, changeSpeed * Time.deltaTime);

            _skinnedMeshRenderer.SetBlendShapeWeight(0, currentWeight);

            yield return null;
        }
    }


    private IEnumerator Hulkization()
    {
        float changeSpeed = _maxOutlineWidth / _timeToGainMuscle;
        float width = _meshRenderer.material.GetFloat(_outline);

        while (width != _maxOutlineWidth)
        {
            width = Mathf.MoveTowards(width, _maxOutlineWidth, changeSpeed * Time.deltaTime);

            _meshRenderer.material.SetFloat(_outline, width);

            yield return null;
        }
    }
}
