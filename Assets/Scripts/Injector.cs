using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Injector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InjectorCollectionZone injectorCollectionZone))
        {
            this.gameObject.SetActive(false);
        }
    }
}
