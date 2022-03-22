using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BodyPartGrow : MonoBehaviour
{
    [SerializeField] private int _animationLayer = 0;
    [SerializeField] private float _animationLayerWeight = 1f;
    [SerializeField] private float _animationTime = 1f;
    [SerializeField] private float _initialGrowBlendShapeIndex = 75f;
    [SerializeField] private float _initialReduceBlendShapeIndex = 0f;
    [SerializeField] private int[] _blendShapeIndexes;

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    public float AnimationTime => _animationTime;
    public int AnimationLayer => _animationLayer;
    public float AnimationLayerWeight => _animationLayerWeight;

    private void OnValidate()
    {
        _animationLayer = Mathf.Clamp(_animationLayer, 0, int.MaxValue);
        _animationLayerWeight = Mathf.Clamp(_animationLayerWeight, 0f, 1f);
        _animationTime = Mathf.Clamp(_animationTime, 0f, float.MaxValue);
        _initialGrowBlendShapeIndex = Mathf.Clamp(_initialGrowBlendShapeIndex, 0f, 100f);
        _initialReduceBlendShapeIndex = Mathf.Clamp(_initialReduceBlendShapeIndex, 0f, 100f);
    }

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    public virtual void GrowBodyPart()
    {
        SetInitialScaleValue(_initialGrowBlendShapeIndex);

        StartCoroutine(ChangeBodyPartsScale(100f, 0f, _blendShapeIndexes.Length - 1, 0, -1));
    }

    public virtual void ReducePartSize()
    {
        SetInitialScaleValue(_initialReduceBlendShapeIndex);

        StartCoroutine(ChangeBodyPartsScale(0f, 100f, 0, _blendShapeIndexes.Length - 1, 1));
    }

    private void SetInitialScaleValue(float initialScaleValue)
    {
        for (int index = 0; index < _blendShapeIndexes.Length; index++)
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(_blendShapeIndexes[index], initialScaleValue);
        }
    }

    private IEnumerator ChangeBodyPartsScale(float from, float to, int initialIndex, int finalIndex, int incrementValue)
    {
        float animationTimePerPart = _animationTime / _blendShapeIndexes.Length;
        int index = initialIndex;

        while (index != finalIndex + incrementValue)
        {
            if (index - 1 >= 0)
                StartCoroutine(ChangeBodyPartScale(_blendShapeIndexes[index - 1], to, from, animationTimePerPart));

            StartCoroutine(ChangeBodyPartScale(_blendShapeIndexes[index], from, to, animationTimePerPart));
                        
            index += incrementValue;

            yield return new WaitForSeconds(animationTimePerPart);
        }
    }

    private IEnumerator ChangeBodyPartScale(int blendShapeIndex, float from, float to, float animationTime)
    {
        float scaleValue = from;
        float currentTime = 0f;

        while (scaleValue != to)
        {
            currentTime += Time.deltaTime / animationTime;
            scaleValue = Mathf.Lerp(from, to, currentTime);
            _skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, scaleValue);

            yield return new WaitForEndOfFrame();
        }
    }
}
