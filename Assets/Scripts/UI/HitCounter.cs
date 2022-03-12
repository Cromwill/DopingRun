using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCounter : MonoBehaviour
{
    [SerializeField] private HustleZone _hustleZone;
    [SerializeField] private Pushable _pushable;
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private TMP_Text _hitText;
    [SerializeField] private TMP_Text _gettingHitText;
    [SerializeField] private TMP_Text _deathText;
    [SerializeField] private float MaxFontSize;
    [SerializeField] private float _minFontSize;
    [SerializeField] private float _timeBeforeFade;

    private int _counter = 0;
    private Coroutine _enlargeCoroutine;
    private List<Coroutine> _fadeCoroutines = new List<Coroutine>();
    private DeathPhrases _deathPhrases = new DeathPhrases();
    private void OnEnable()
    {
        _hustleZone.CollidedWithPushable += OnHit;
        _pushable.PushStart += OnGettingHit;

        _deathText.color = SetColorAlpha(_deathText.color, 0);
        _counterText.color = SetColorAlpha(_counterText.color, 0);
        _hitText.color = SetColorAlpha(_hitText.color, 0);
        _gettingHitText.color = SetColorAlpha(_gettingHitText.color, 0);
    }

    private void OnDisable()
    {
        _hustleZone.CollidedWithPushable -= OnHit;
        _pushable.PushStart -= OnGettingHit;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void OnDeath()
    {
        if (_enlargeCoroutine != null)
            StopCoroutine(_enlargeCoroutine);

        if (_fadeCoroutines.Count > 0)
            DisableCoroutines(_fadeCoroutines);

        _gettingHitText.color = SetColorAlpha(_gettingHitText.color, 0);

        _deathText.text = _deathPhrases.GetRandomPhrase();

        _deathText.color = SetColorAlpha(_deathText.color, 1);
        _fadeCoroutines.Add(StartCoroutine(DelayedFade(_deathText, 0.5f)));

        _counterText.color = SetColorAlpha(_counterText.color, 0);
        _hitText.color = SetColorAlpha(_hitText.color, 0);
    }

    private void OnGettingHit()
    {
        if (_fadeCoroutines.Count > 0)
            DisableCoroutines(_fadeCoroutines);

        _gettingHitText.color = SetColorAlpha(_gettingHitText.color, 1);

        if (_enlargeCoroutine != null)
            StopCoroutine(_enlargeCoroutine);

        _enlargeCoroutine = StartCoroutine(Animation(_gettingHitText));
        _fadeCoroutines.Add(StartCoroutine(DelayedFade(_gettingHitText, _timeBeforeFade)));

        _counterText.color = SetColorAlpha(_counterText.color, 0);
        _hitText.color = SetColorAlpha(_hitText.color, 0);

        _counter = 0;
    }

    private void OnHit()
    {
        _counterText.color = SetColorAlpha(_counterText.color,1);
        _hitText.color = SetColorAlpha(_hitText.color, 1);
        _gettingHitText.color = SetColorAlpha(_gettingHitText.color, 0);

        _counter++;

        _counterText.text =$"x{_counter}";
        _hitText.text = "Hit!";

        if (_enlargeCoroutine != null)
            StopCoroutine(_enlargeCoroutine);

        _enlargeCoroutine =StartCoroutine(Animation(_counterText));

        if (_fadeCoroutines.Count > 0)
            DisableCoroutines(_fadeCoroutines);

        _fadeCoroutines.Add(StartCoroutine(DelayedFade(_counterText, _timeBeforeFade)));
        _fadeCoroutines.Add(StartCoroutine(DelayedFade(_hitText, _timeBeforeFade)));
    }

    private IEnumerator Animation(TMP_Text text)
    {
        float changeSpeed = (MaxFontSize - _minFontSize) / 0.2f;

        while (text.fontSize < MaxFontSize)
        {
            text.fontSize = Mathf.MoveTowards(text.fontSize, MaxFontSize, changeSpeed * Time.deltaTime);
            yield return null;
        }

        while (text.fontSize > _minFontSize)
        {
            text.fontSize = Mathf.MoveTowards(text.fontSize, _minFontSize, changeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DelayedFade(TMP_Text text, float elapsedTime)
    {
        while(elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;

            yield return null;
        }

        _fadeCoroutines.Add(StartCoroutine(Fade(text, elapsedTime)));
    }

    private IEnumerator Fade(TMP_Text text, float elapseTime)
    {
        float changeSpeed = 1 / 0.2f;

        while (text.color.a > 0)
        {
            Color color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
            color.a = Mathf.MoveTowards(color.a, 0, changeSpeed * Time.deltaTime);
            text.color = color;

            yield return null;
        }

        _counter = 0;
    }
    private void DisableCoroutines(List<Coroutine> coroutines)
    {
        foreach (var coroutine in coroutines)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
    }

    private Color SetColorAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
}
