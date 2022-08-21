using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RewardedTrigger : MonoBehaviour
{
    [SerializeField] private RewardScreen _rewardScreen;
    [SerializeField] private Collider _safeTrigger;
    [SerializeField] private Collider[] _unsafeTriggers;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _rewardScreen.OfferRejected += OnOfferReject;
        _rewardScreen.Rewarded += OnRewarded;
    }

    private void OnDisable()
    {
        _rewardScreen.Rewarded -= OnRewarded;
        _rewardScreen.OfferRejected -= OnOfferReject;
    }

    private void OnOfferReject()
    {
        _safeTrigger.enabled = false;
    }

    private void OnRewarded()
    {
        foreach (var trigger in _unsafeTriggers)
        {
            trigger.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            _rewardScreen.Show();
            _collider.enabled = false;
        }
    }
}
