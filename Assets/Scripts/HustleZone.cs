using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HustleZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pushable pushable))
        {
            Vector3 direcation = (other.transform.position - transform.position).normalized;
            pushable.Push(direcation);
        }
    }
}
