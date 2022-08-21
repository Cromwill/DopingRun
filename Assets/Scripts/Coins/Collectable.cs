using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Collectable : MonoBehaviour
{
    private Transform[] _childs;
    private AudioSource _audioSource;
    private Collider _collider;

    private void Awake()
    {
        _childs = GetComponentsInChildren<Transform>();
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider>();
    }

    public void Disable()
    {
        foreach (var child in _childs)
        {
            if (child != transform)
                child.gameObject.SetActive(false);
        }
        _collider.enabled = false;

        _audioSource.Play();
        StartCoroutine(Disable(_audioSource.clip.length));
    }

    private IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
