using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TutorialEnabler _tutorialEnabler;
    public void OnPointerDown(PointerEventData eventData)
    {
        _tutorialEnabler.Close();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            _tutorialEnabler.Close();
    }
}
