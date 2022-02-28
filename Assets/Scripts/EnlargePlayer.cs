using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargePlayer : MonoBehaviour
{
    private float _changeSpeed;
    private Vector3 _targetScale;

    public void Enlarge(float time, float size)
    {
        _changeSpeed = (size - transform.localScale.x) / time ;
        _targetScale = new Vector3(size, size, size);

        StartCoroutine(EnlargeAnimation());
    }

    private IEnumerator EnlargeAnimation()
    {
        while(transform.localScale.x<= _targetScale.x)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, _targetScale, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
