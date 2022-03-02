using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargePlayer : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private float _timeToGainMuscle;
    [SerializeField] private float _targetScaleValue;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private const float _maxWeight = 100f;
    private Vector3 _targetScale;
    private Enlargable _enlargable;
    private InjectorBarPresenter _barPresenter;
    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _targetScale = Vector3.one * _targetScaleValue;
        _enlargable = FindObjectOfType<Enlargable>();
        Error.CheckOnNull(_enlargable, nameof(Enlargable));

        _barPresenter = FindObjectOfType<InjectorBarPresenter>();
        Error.CheckOnNull(_barPresenter, nameof(InjectorBarPresenter));

        _playerAnimator = FindObjectOfType<PlayerAnimator>();
        Error.CheckOnNull(_playerAnimator, nameof(PlayerAnimator));
    }

    private void OnInjection()
    {
        _barPresenter.SetTimeToErase(_playerAnimator.InjectionAnimationTime);
        _enlargable.Reset();
    }

    private void OnFlexing()
    {
        StartCoroutine(EnlargeAnimation());
        StartCoroutine(GainMuscleAnimation());
    }

    private IEnumerator EnlargeAnimation()
    {
        float changeSpeed = (_targetScale.x - transform.localScale.x) / _timeToGainMuscle;

        while (transform.localScale.x<= _targetScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _targetScale, changeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator Hulkization()
    {
        float timePassed =0;

        while (timePassed != _timeToGainMuscle)
        {
            _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, Color.green, timePassed/ _timeToGainMuscle);
            _meshRenderer.material.SetColor("_ColorShaded", _meshRenderer.material.color);

            timePassed += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator GainMuscleAnimation()
    {
        float currentWeight = 0;
        float changeSpeed = _maxWeight / _timeToGainMuscle;

        while (currentWeight < _maxWeight)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, _maxWeight, changeSpeed * Time.deltaTime);

            _skinnedMeshRenderer.SetBlendShapeWeight(0, currentWeight);

            yield return null;
        }
    }
}
