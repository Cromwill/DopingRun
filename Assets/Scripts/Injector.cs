using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SyringeCollectionZone injectorCollectionZone))
        {
            gameObject.SetActive(false);
        }
    }
}