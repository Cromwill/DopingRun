using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    [SerializeField] private List<SoundEffect> _soundEffects;

    private Rigidbody[] _rigidbodys;
    private Vector3[] _initialPositions;
    private AudioSource _audioSource;

    private List<Collider> _colliders = new List<Collider>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _rigidbodys = GetComponentsInChildren<Rigidbody>();
        _initialPositions = new Vector3[_rigidbodys.Length];

        for (int i = 0; i < _rigidbodys.Length; i++)
        {
            _initialPositions[i] = _rigidbodys[i].position;
        }

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = true;

            if (rigidbody.TryGetComponent(out CharacterJoint characterJoint))
                characterJoint.enableProjection = true;

            if (rigidbody.TryGetComponent(out Collider collider))
            {
                _colliders.Add(collider);
                collider.enabled = false;
            }
        }
    }

    public void EnableRagdoll()
    {
        if (_soundEffects.Count > 0)
        {
            AudioClip clip = _soundEffects.Find(effect => effect.SoundEffectType == SoundEffectType.Fall).AudioClip;
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = false;

            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public void DisableRagdoll()
    {
        for (int i = 0; i < _rigidbodys.Length; i++)
        {
            _rigidbodys[i].position = _initialPositions[i];
        }

        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = true;

            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
