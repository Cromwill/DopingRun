using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScaleBodyPartGrow : BodyPartGrow
{
    [SerializeField] private List<Transform> _bones = new List<Transform>();

    public override void GrowBodyPart()
    {
        //foreach (var bone in _bones)
        //{
        //    bone.transform.localScale = Vector3.zero;
        //}

        StartCoroutine(ChangePartScale(Vector3.zero, Vector3.one));
    }

    public override void ReducePartSize()
    {
        //foreach (var bone in _bones)
        //{
        //    bone.transform.localScale = Vector3.one;
        //}

        StartCoroutine(ChangePartScale(Vector3.one, Vector3.zero));
    }

    private IEnumerator ChangePartScale(Vector3 from, Vector3 to)
    {
        Vector3 scaleValue = from;
        float currentTime = 0f;

        while (scaleValue != to)
        {
            currentTime += Time.deltaTime / AnimationTime;
            scaleValue = Vector3.Lerp(from, to, currentTime);

            foreach (var bone in _bones)
            {
                bone.transform.localScale = scaleValue;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
