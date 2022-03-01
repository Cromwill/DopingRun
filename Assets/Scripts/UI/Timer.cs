using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private float timer = 0;
    private void Update()
    {
        timer = Time.time;

        _text.text = timer.ToString();
    }
}
