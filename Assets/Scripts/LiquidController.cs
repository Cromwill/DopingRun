using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class LiquidController : MonoBehaviour
{
    [SerializeField] private Enlargable _enlargable;
    [SerializeField] private LiquidVolume _liquid;

    private float _liquidStep;
    private float _currentLiquidLevel;
    private const float _maxLiquidLevel = 1f;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        _enlargable.StepChanged += ChangeLiquidLevel;
    }

    private void OnDisable()
    {
        _enlargable.StepChanged -= ChangeLiquidLevel;
    }


    private void ChangeLiquidLevel(int step, int maxLevel)
    {
        
        _liquidStep = _maxLiquidLevel / maxLevel;

        _currentLiquidLevel = step * _liquidStep;

        StartCoroutine(ChangeLiquidLevelAnimation());
    }

    private IEnumerator ChangeLiquidLevelAnimation()
    {
        while (_liquid.level != _currentLiquidLevel)
        {
            _liquid.level = Mathf.MoveTowards(_liquid.level, _currentLiquidLevel,  Time.deltaTime);

            yield return null;
        }
    }
}
