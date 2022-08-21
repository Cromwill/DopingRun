using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class SoundEffect
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private SoundEffectType _soundEffectType;

    public AudioClip AudioClip => _audioClips[Random.Range(0, _audioClips.Count - 1)];
    public SoundEffectType SoundEffectType => _soundEffectType;
}
