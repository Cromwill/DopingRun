using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RewardedTrigger : MonoBehaviour
{
    [SerializeField] private RewardScreen _rewardScreen;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Time.timeScale = 0;
            _rewardScreen.Show();
            _collider.enabled = false;
        }
    }
}
