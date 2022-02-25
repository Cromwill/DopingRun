using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMuscleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAnimator animator))
            animator.GainMuscleAnimation();

        if (other.TryGetComponent(out Player player))
        {
            Enlargable enlargable = player.GetComponentInChildren<Enlargable>();
            HustleZone hustleZone = player.GetComponentInChildren<HustleZone>();
            hustleZone.AddPushSpeed(enlargable.Step);
            enlargable.Reset();
        }
    }
}
