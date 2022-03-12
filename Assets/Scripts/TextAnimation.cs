using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation
{
    public IEnumerator EnlargeAnimation(TMP_Text text, float MaxFontSize, float _minFontSize)
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

    public IEnumerator ColorLerpAnimation(TMP_Text text, Color targetColor, float time)
    {
        float elapsedTime = 0;

        while(text.color != targetColor)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(text.color, targetColor, elapsedTime / time);

            yield return null;
        }
    }
}
