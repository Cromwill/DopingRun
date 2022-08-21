using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float _loadingTime;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        float elapsed = 0;

        while (elapsed < _loadingTime)
        {
            _slider.value = Mathf.Lerp(_slider.value, 1f, elapsed / _loadingTime * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
