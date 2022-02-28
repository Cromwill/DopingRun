using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private EnlargePlayer _enlargePlayer;
    [SerializeField] private float _timeToGainMuscle;
    [SerializeField] private float _targetScale;

    private float _maxWeight = 100f;
    public float AnimationTime => _timeToGainMuscle;

    private float _changeSpeed;

    public void GainMuscle()
    {
        _changeSpeed = _maxWeight / _timeToGainMuscle;
        _enlargePlayer.Enlarge(_timeToGainMuscle, _targetScale);
        StartCoroutine(GainMuscleAnimation());
    }

    private IEnumerator GainMuscleAnimation()
    {
        float currentWeight = 0;

        while(currentWeight < _maxWeight)
        {
            currentWeight = Mathf.MoveTowards(currentWeight, _maxWeight, _changeSpeed * Time.deltaTime);

            _skinnedMeshRenderer.SetBlendShapeWeight(0, currentWeight);

            yield return null;
        }
    }
}
