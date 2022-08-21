using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Agava.YandexGames;

[RequireComponent(typeof(TMP_Text))]
public class LocalizationText : MonoBehaviour
{
    [SerializeField] private LocalizationLabel[] _options;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();

#if !UNITY_WEBGL || UNITY_EDITOR
        _text.text = LocalizeLabel(Language.ru);
        return;
#endif
        _text.text = LocalizeLabel(Localization.Instance.Language);
    }

    private string LocalizeLabel(Language localization)
    {
        foreach (var option in _options)
        {
            if (option.Language == localization)
                return option.Text;
        }

        throw new InvalidOperationException("No such localization found: " + nameof(localization));
    }
}
