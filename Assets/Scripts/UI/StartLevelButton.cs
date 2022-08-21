using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartLevelButton : MonoBehaviour, IPointerDownHandler
{
    public event Action RunStarted;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick();
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
        RunStarted?.Invoke();
    }
}
