using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainMuscleTrigger : MonoBehaviour
{
    private GainMuscleTrigger[] _gainMuscleTriggers;
    private CameraTarget _cameraTarget;

    private void Awake()
    {
        _gainMuscleTriggers = FindObjectsOfType<GainMuscleTrigger>();

        _cameraTarget = FindObjectOfType<CameraTarget>();
        Error.CheckOnNull(_cameraTarget, nameof(CameraTarget));
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
            _cameraTarget.gameObject.SetActive(false);

            foreach (var gainMuscleTrigger in _gainMuscleTriggers)
            {
                gainMuscleTrigger.enabled = false;
            }

        }
    }
}
