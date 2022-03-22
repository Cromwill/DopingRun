using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MergeMan : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _currentHands;
    [SerializeField] private SkinnedMeshRenderer _currentHead;
    [SerializeField] private SkinnedMeshRenderer _currentTorso;
    [SerializeField] private SkinnedMeshRenderer _currentLegs;
    [SerializeField] private SkinnedMeshRenderer _currentWings;
    [SerializeField] private SkinnedMeshRenderer _currentTail;
    [SerializeField] private SkinnedMeshRenderer _currentBackpart;
    [Space(5)]
    [SerializeField] private SkinnedMeshRenderer _newHand;
    [SerializeField] private SkinnedMeshRenderer _newHead;
    [SerializeField] private SkinnedMeshRenderer _newWings;

    public event Action MergeEnded;

    public void StartMerge()
    {
        StartCoroutine(ChangePart(BodyPart.Hand, _newHand));
        StartCoroutine(ChangePart(BodyPart.Head, _newHead));
        StartCoroutine(ChangePart(BodyPart.Wings, _newWings));
    }

    private IEnumerator ChangePart(BodyPart bodyPart, SkinnedMeshRenderer newLimbMesh)
    {
        yield return new WaitForSeconds(3.5f);

        SkinnedMeshRenderer currentLimb = GetLimbByObstacleType(bodyPart);
        float timeBeforeGrow = 0f;
        BodyPartGrow bodyPartGrowComponent;

        if (currentLimb != null)
        {
            bodyPartGrowComponent = GetBodyPartGrowComponent(currentLimb);
            timeBeforeGrow = bodyPartGrowComponent.AnimationTime;
            bodyPartGrowComponent.ReducePartSize();
        }

        yield return new WaitForSeconds(timeBeforeGrow);

        TrySetAnimationLayerWeightByBodyPart(newLimbMesh);
        ChangeLimb(bodyPart, newLimbMesh);

        bodyPartGrowComponent = GetBodyPartGrowComponent(newLimbMesh);
        timeBeforeGrow = bodyPartGrowComponent.AnimationTime;
        bodyPartGrowComponent.GrowBodyPart();

        yield return new WaitForSeconds(timeBeforeGrow);

        MergeEnded?.Invoke();
    }

    private void TrySetAnimationLayerWeightByBodyPart(SkinnedMeshRenderer newLimbMesh)
    {
        var bodyPart = GetBodyPartGrowComponent(newLimbMesh);
    }

    private BodyPartGrow GetBodyPartGrowComponent(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        BodyPartGrow bodyPartGrowComponent = skinnedMeshRenderer.GetComponent<BodyPartGrow>();

        if (bodyPartGrowComponent == null)
            throw new NullReferenceException($"{nameof(bodyPartGrowComponent)} Component must have BodyPartGrow script!");

        return bodyPartGrowComponent;
    }

    private void ChangeLimb(BodyPart bodyPart, SkinnedMeshRenderer newLimbMesh)
    {
        SkinnedMeshRenderer limbMesh = GetLimbByObstacleType(bodyPart);

        if (limbMesh != null)
            limbMesh.gameObject.SetActive(false);

        limbMesh = newLimbMesh;
        SetLimbByObstacleType(bodyPart, newLimbMesh);
        limbMesh.gameObject.SetActive(true);
    }

    private SkinnedMeshRenderer GetLimbByObstacleType(BodyPart bodyPart)
    {
        return bodyPart switch
        {
            BodyPart.Hand => _currentHands,
            BodyPart.Head => _currentHead,
            BodyPart.Tail => _currentTail,
            BodyPart.Wings => _currentWings,
            BodyPart.Leg => _currentLegs,
            BodyPart.Torso => _currentTorso,
            BodyPart.BackPart => _currentBackpart,
            _ => throw new ArgumentOutOfRangeException(nameof(bodyPart), $"{bodyPart} is an unknown obstacle type!"),
        };
    }

    private void SetLimbByObstacleType(BodyPart bodyPart, SkinnedMeshRenderer skinnedMeshRenderer)
    {
        switch (bodyPart)
        {
            case BodyPart.Hand:
                _currentHands = skinnedMeshRenderer;
                break;
            case BodyPart.Head:
                _currentHead = skinnedMeshRenderer;
                break;
            case BodyPart.Tail:
                _currentTail = skinnedMeshRenderer;
                break;
            case BodyPart.Wings:
                _currentWings = skinnedMeshRenderer;
                break;
            case BodyPart.Leg:
                _currentLegs = skinnedMeshRenderer;
                break;
            case BodyPart.Torso:
                _currentTorso = skinnedMeshRenderer;
                break;
            case BodyPart.BackPart:
                _currentBackpart = skinnedMeshRenderer;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(bodyPart), "Unknown obstacle type!");
        }
    }
}

public enum BodyPart
{
    Head,
    Torso,
    Hand,
    Leg,
    Tail,
    Wings,
    BackPart
}
