using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Graber : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;

    private RunerEnemyAnimator _runerEnemyAnimator;
    private RunerEnemyMover _runerEnemyMover;
    private AudioSource _audioSource;

    private void Awake()
    {
        _runerEnemyAnimator = GetComponent<RunerEnemyAnimator>();
        _runerEnemyMover = GetComponent<RunerEnemyMover>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.55f;
        _audioSource.pitch = 1.3f;
    }

    public void Grab()
    {
        _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length - 1)], 0.5f);
        _runerEnemyAnimator.GrabAnimation();
        _runerEnemyMover.Disable();
    }
}
