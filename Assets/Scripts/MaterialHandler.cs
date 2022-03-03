using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHandler
{
    public IEnumerator ChangeAnimation(SkinnedMeshRenderer meshRenderer, Material material2, float time)
    {
        float timePassed = 0;

        while(meshRenderer.material != material2)
        {
            timePassed += Time.deltaTime;
            meshRenderer.material.Lerp(meshRenderer.material, material2, timePassed / time);
            Debug.Log(timePassed);

            yield return null;
        }
    }
}
