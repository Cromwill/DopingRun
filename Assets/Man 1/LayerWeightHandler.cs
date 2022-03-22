using System.Collections.Generic;
using UnityEngine;

public class LayerWeightHandler : MonoBehaviour
{
    [SerializeField] private List<BodyPartGrow> _bodyParts = new List<BodyPartGrow>();
    [SerializeField] private List<Animator> _animators;

    private Dictionary<int, float> _animationLayerValues = new Dictionary<int, float>();

    public IReadOnlyDictionary<int, float> AnimationLayerValues => _animationLayerValues;

    public void SetAnimationLayerWeightByBodyPart(BodyPartGrow bodyPart)
    {
        var part = _bodyParts.Find(part => part == bodyPart);

        if (part != null)
        {
            foreach (var animator in _animators)
            {
                animator.SetLayerWeight(part.AnimationLayer, part.AnimationLayerWeight);

                UpdateAnimationLayersValues(part.AnimationLayer, part.AnimationLayerWeight);
            }
        }

    }

    private void UpdateAnimationLayersValues(int animationLayer, float animationLayerWeight)
    {
        if (_animationLayerValues.ContainsKey(animationLayer))
        {
            _animationLayerValues[animationLayer] = animationLayerWeight;
            return;
        }

        _animationLayerValues.Add(animationLayer, animationLayerWeight);
    }
}
