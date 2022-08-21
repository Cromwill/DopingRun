using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    private SectionView[] _sections;
    private SectionView _previous;

    public event Action<SectionView> SectionChanged;

    private void Awake()
    {
        _sections = GetComponentsInChildren<SectionView>();
    }

    private void Start()
    {
        OnClick(_sections[0]);
    }

    private void OnEnable()
    {
        foreach (var section in _sections)
            section.Clicked += OnClick;
    }

    private void OnDisable()
    {
        foreach (var section in _sections)
            section.Clicked -= OnClick;
    }

    private void OnClick(SectionView section)
    {
        if (section != _previous)
        {
            if (_previous != null)
                _previous.Unselect();

            _previous = section;
        }

        SectionChanged?.Invoke(section);
        section.Select();
    }
}
