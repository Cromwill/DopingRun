using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMuscleTrigger : MonoBehaviour
{
    private GainMuscleTrigger[] _gainMuscleTriggers;

    private void Awake()
    {
        _gainMuscleTriggers = FindObjectsOfType<GainMuscleTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAnimator animator))
            animator.GainMuscle();

        if (other.TryGetComponent(out Player player))
        {
            Enlargable enlargable = player.GetComponentInChildren<Enlargable>();
            HustleZone hustleZone = player.GetComponentInChildren<HustleZone>();
            hustleZone.AddPushSpeed(enlargable.Step);
            Pushable pushable = player.GetComponent<Pushable>();
            pushable.SetPushTime(enlargable.Step);
            hustleZone.IncreaseCollider();

            foreach (var gainMuscleTrigger in _gainMuscleTriggers)
            {
                gainMuscleTrigger.enabled = false;
            }

        }
    }
}
