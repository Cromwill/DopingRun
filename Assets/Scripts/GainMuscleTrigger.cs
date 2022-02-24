using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMuscleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAnimator animator))
            animator.GainMuscleAnimation();

        if (other.TryGetComponent(out Enlargable enlargable))
            enlargable.Reset();
    }
}
