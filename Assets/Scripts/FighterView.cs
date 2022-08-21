using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterView : MonoBehaviour
{
    [SerializeField] private List<SoundEffect> _soundEffects;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnAttack()
    {
        AudioClip clip = _soundEffects.Find(effect => effect.SoundEffectType == SoundEffectType.Attack).AudioClip;
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
